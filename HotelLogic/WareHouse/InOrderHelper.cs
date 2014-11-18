using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.WareHouse
{
    public class InOrderHelper
    {
        //生成整个入库单,一个入库单对应多个商品信息
        //RecordList为空则只生成入库单信息,对应表 InOrder
        public string CreateInOrder(InOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                WH_InOrder temp = new WH_InOrder();
                temp.Guid = new Guid();
                temp.Date = inorder.Date;
                temp.OrderNum = inorder.OrderNum;
                temp.Position = inorder.Position;
                temp.Supplier = inorder.Supplier;
                db.WH_InOrder.AddObject(temp);

                if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                {
                    foreach (var r in inorder.RecordList)
                    {
                        WH_InOrderDetails detail = new WH_InOrderDetails();
                        detail.OrderID = r.OrderID;
                        detail.Counts = r.Counts;
                        detail.InPrice = r.InPrice;
                        detail.OrderGuid = temp.Guid;
                        detail.ProductId = r.ProductId;
                        detail.ProductName = r.ProductName;
                        detail.TotalPrice = r.TotalPrice;
                        detail.Used = r.Used;
                        db.WH_InOrderDetails.AddObject(detail);
                    }
                }
                db.SaveChanges();
                inorder.ID = temp.AutoID;
                result = "0";
            }
            return result;
        }

        //向现有入库单中添加新商品信息记录
        //适用于添加新商品信息到已有入库单
        public string CreateDetailByGuid(Guid guid, InOrderRecordModel record)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var order = (from a in db.WH_InOrder
                             where a.Guid == guid
                             select a).SingleOrDefault();
                if (order != null)
                {
                    WH_InOrderDetails temp = new WH_InOrderDetails();
                    temp.Counts = record.Counts;
                    temp.InPrice = record.InPrice;
                    temp.OrderGuid = guid;
                    temp.ProductName = record.ProductName;
                    temp.TotalPrice = record.TotalPrice;
                    temp.Used = record.Used;
                    db.WH_InOrderDetails.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        //读取所有入库单及所属的商品信息
        public List<InOrderModel> ReadInOrder()
        {
            List<InOrderModel> result = new List<InOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.WH_InOrder
                           select a;
                foreach (var t in temp)
                {
                    InOrderModel item = new InOrderModel();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    item.Position = t.Position;
                    item.Supplier = t.Supplier;
                    item.OrderGuid = t.Guid;
                    //读取进货单明细
                    var details = from b in db.WH_InOrderDetails
                                  where b.OrderID == item.ID
                                  select b;
                    foreach (var detail in details)
                    {
                        InOrderRecordModel record = new InOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = detail.ProductName;
                        record.TotalPrice = detail.TotalPrice;
                        record.Used = detail.Used;
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
                }
            }
            return result;
        }
        private string getSupplier(int supplier)
        {
            string result = "";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var order = (from a in db.WH_Supplier
                             where a.AutoID == supplier
                             select a).SingleOrDefault();
                if (order != null)
                {

                    result = order.SupplierName;
                }
            }
            return result;
        }
        private string getPosition(int position)
        {
            string result = "";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var order = (from a in db.WH_Position
                             where a.AutoID == position
                             select a).SingleOrDefault();
                if (order != null)
                {

                    result = order.LocName;
                }
            }
            return result;
        }
        //读取所有入库单及所属的商品信息
        public List<InOrderModel> ReadInOrder(int page, int rows)
        {
            List<InOrderModel> result = new List<InOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                  int ordernum;
                  var temp2 = (from a in db.WH_InOrder
                            orderby a.AutoID descending
                               where a.OrderNum != null
                           select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp2)
                {
                    double TotalPriceAll = 0;
                    InOrderModel item = new InOrderModel();
                    item.RecordList =new List<InOrderRecordModel>();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    ordernum = Convert.ToInt32(t.OrderNum);
                    item.Position = getPosition(Convert.ToInt32(t.Position)); 
                    item.Supplier = getSupplier(Convert.ToInt32(t.Supplier));
                    item.OrderGuid = t.Guid;
                    //读取进货单明细
                    var details = from b in db.WH_InOrderDetails
                                  join g in db.WH_Goods on b.ProductId equals g.GoodsNo
                                  where b.OrderID == item.ID
                                  select new
                                  {
                                      b,
                                      g
                                  };
                    foreach (var detail1 in details)
                    {
                        var detail = detail1.b;
                        var good = detail1.g;
                        InOrderRecordModel record = new InOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = good.GoodsName;
                        record.GoodsStyle = good.GoodsStyle;
                        record.Unit = good.Unit;
                        record.TotalPrice = detail.TotalPrice;
                        TotalPriceAll = TotalPriceAll + Convert.ToDouble(detail.TotalPrice);
                        item.TotalPriceAll = TotalPriceAll;
                        record.Used = detail.Used;
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
                }

                //var temp = (from a in db.WH_InOrder
                //            join s in db.WH_Supplier on  a.Supplier equals  s.AutoID
                //            join p in db.WH_Position on  a.Position equals  p.AutoID
                //            orderby a.AutoID ascending
                //            where a.OrderNum != null
                //            select new
                //            {
                //                a,
                //                s,
                //                p
                //            }).Skip((page - 1) * rows).Take(rows);
                //foreach (var t1 in temp)
                //{
                //    var t = t1.a;
                //    var supplier = t1.s;
                //    var position = t1.p;
                //    InOrderModel item = new InOrderModel();
                //    item.Date = t.Date;
                //    item.ID = t.AutoID;
                //    item.OrderNum = t.OrderNum;
                //    item.Supplier = supplier.SupplierName;
                //    item.Position = position.LocName;
                //    item.OrderGuid = t.Guid;
                //    item.RecordList = new List<InOrderRecordModel>();
                //    //读取进货单明细
                //    var details = from b in db.WH_InOrderDetails
                //                  join g in db.WH_Goods on b.ProductId equals g.GoodsNo
                //                  where b.OrderID == item.ID
                //                  select new
                //                  {
                //                      b,
                //                      g
                //                  };
                //    foreach (var detail1 in details)
                //    {
                //        var detail = detail1.b;
                //        var good = detail1.g;
                //        InOrderRecordModel record = new InOrderRecordModel();
                //        record.ID = detail.AutoID;
                //        record.Counts = detail.Counts;
                //        record.InPrice = detail.InPrice;
                //        record.OrderID = detail.OrderID;
                //        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                //        record.ProductName = good.GoodsName;
                //        record.GoodsStyle = good.GoodsStyle;
                //        record.Unit = good.Unit;
                //        record.TotalPrice = detail.TotalPrice;
                //        record.Used = detail.Used;
                //        item.RecordList.Add(record);
                //    }
                //    result.Add(item);
                //}
            }
            return result;
        }

        //读取入库单信息,仅入库单部分,不包含商品记录信息
        public List<InOrderModel> ReadOnlyInOrder()
        {
            List<InOrderModel> result = new List<InOrderModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //入库单信息
                var query = from a in db.WH_InOrder
                            select a;
                //entity to model
                foreach (var q in query)
                {
                    InOrderModel temp = new InOrderModel();
                    temp.Date = q.Date;
                    temp.ID = q.AutoID;
                    temp.OrderGuid = q.Guid;
                    temp.OrderNum = q.OrderNum;
                    temp.Position = q.Position;
                    temp.RecordList = null;
                    temp.Supplier = q.Supplier;
                    result.Add(temp);
 
                }
            }
            return result;

        }

        //读取某个入库单所有商品记录信息
        public List<InOrderRecordModel> ReadInOrderRecordByGuid(Guid guid)
        {
            List<InOrderRecordModel> result = new List<InOrderRecordModel>();
            using (var db = new HotelDBEntities())
            {
                var query = from a in db.WH_InOrderDetails
                            where a.OrderGuid == guid
                            select a;
                foreach (var q in query)
                {
                    InOrderRecordModel temp = new InOrderRecordModel();
                    temp.Counts = q.Counts;
                    temp.ID = q.AutoID;
                    temp.InPrice = q.InPrice;
                    temp.OrderID = q.OrderID;
                    temp.ProductId = q.ProductId == null ? 0 : (int)q.ProductId;
                    temp.ProductName = q.ProductName;
                    temp.TotalPrice = q.TotalPrice;
                    temp.Used = q.Used;
                    result.Add(temp);
                }
            }
            return result;
        }

        public string CreateOrderDetail(InOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_InOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.Position = inorder.Position;
                    model.Supplier = inorder.Supplier;
                    if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                    {
                        foreach (var r in inorder.RecordList)
                        {
                            WH_InOrderDetails detail = new WH_InOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            detail.Used = r.Used;
                            db.WH_InOrderDetails.AddObject(detail);
                        }
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        //更新整个入库单
        public string UpdateInOrder(InOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_InOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.Position = inorder.Position;
                    model.Supplier = inorder.Supplier;
                    var details = db.WH_InOrderDetails.Where(a => a.OrderID == inorder.ID);
                    if (details != null)
                    {
                        foreach (var r in details)
                        {
                            WH_InOrderDetails detail = new WH_InOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            detail.Used = r.Used;
                            db.WH_InOrderDetails.AddObject(detail);
                        }
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        //更新入库单中的商品信息
        public string UpdateDetailByGuid(Guid guid, int ID)
        {
            string result = "-1";
            return result;
        }

        //删除整个入库单
        public string DeleteInOrder(int inorderid)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_InOrder.Where(a => a.AutoID == inorderid).SingleOrDefault();
                if(model != null)
                {
                    var detail = db.WH_InOrderDetails.Where(a => a.OrderID == inorderid);
                    foreach (var r in detail)
                    {
                        db.WH_InOrderDetails.DeleteObject(r);
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        //删除入库单内的一条商品信息
        public string DeleteDetailByGuid(Guid guid,int ID)
        {
            string result = "-1";
            return result;
        }
 
    }
    //进货单信息
    public class InOrderModel
    {
        public int ID { get; set; }
        public string OrderNum { get; set; }
        public System.Guid? OrderGuid { get; set; }
        public string Supplier { get; set; }
        public string Position { get; set; }
        public string SupplierName { get; set; }
        public string PositionName { get; set; }
        public DateTime? Date { get; set; }
        public List<InOrderRecordModel> RecordList { get; set; }
        public double TotalPriceAll { get; set; }
    }
    //进货记录信息
    public class InOrderRecordModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string GoodsStyle { get; set; }
        public string Unit { get; set; }
        public int? Counts { get; set; }
        public decimal? InPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Used { get; set; }
    }
    //进货记录信息
    [Serializable]
    public class InOrderRecordModelJson
    {
        private int productid;
        public int ProductId
        {
            get
            {
                return productid;
            }
            set
            {
                productid = value;
            }
        }
        private int? counts;
        public int? Counts
        {
            get
            {
                return counts;
            }
            set
            {
                counts = value;
            }
        }
        private decimal? inprice;
        public decimal? InPrice
        {
            get
            {
                return inprice;
            }
            set
            {
                inprice = value;
            }
        }
        private decimal? totalprice;
        public decimal? TotalPrice
        {
            get
            {
                return totalprice;
            }
            set
            {
                totalprice = value;
            }
        }
        private string used;
        public string Used
        {
            get
            {
                return used;
            }
            set
            {
                used = value;
            }
        }
    }
}
