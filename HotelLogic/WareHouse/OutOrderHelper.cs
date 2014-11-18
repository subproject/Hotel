using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.WareHouse
{
    public class OutOrderHelper
    {

        //生成整个入库单,一个入库单对应多个商品信息
        //RecordList为空则只生成入库单信息,对应表 OutOrder
        public string CreateOutOrder(OutOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                WH_OutOrder temp = new WH_OutOrder();
                temp.Guid = new Guid();
                temp.Date = inorder.Date;
                temp.OrderNum = inorder.OrderNum;
                temp.Position = inorder.Position;
                temp.Supplier = inorder.Supplier;
                db.WH_OutOrder.AddObject(temp);

                if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                {
                    foreach (var r in inorder.RecordList)
                    {
                        WH_OutOrderDetails detail = new WH_OutOrderDetails();
                        detail.OrderID = r.OrderID;
                        detail.Counts = r.Counts;
                        detail.InPrice = r.InPrice;
                        detail.OrderGuid = temp.Guid;
                        detail.ProductId = r.ProductId;
                        detail.ProductName = r.ProductName;
                        detail.TotalPrice = r.TotalPrice;
                        detail.Remark = r.Remark;
                        db.WH_OutOrderDetails.AddObject(detail);
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
        public string CreateDetailByGuid(Guid guid, OutOrderRecordModel record)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var order = (from a in db.WH_OutOrder
                             where a.Guid == guid
                             select a).SingleOrDefault();
                if (order != null)
                {
                    WH_OutOrderDetails temp = new WH_OutOrderDetails();
                    temp.Counts = record.Counts;
                    temp.InPrice = record.InPrice;
                    temp.OrderGuid = guid;
                    temp.ProductName = record.ProductName;
                    temp.TotalPrice = record.TotalPrice;
                    temp.Remark = record.Remark;
                    db.WH_OutOrderDetails.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        public string CreateOrderDetail(OutOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_OutOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.Position = inorder.Position;
                    model.Supplier = inorder.Supplier;
                    decimal? totalmoney = 0;
                    if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                    {
                        foreach (var r in inorder.RecordList)
                        {
                            WH_OutOrderDetails detail = new WH_OutOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            totalmoney += r.TotalPrice;
                            detail.Remark = r.Remark;
                            db.WH_OutOrderDetails.AddObject(detail);
                        }
                    }
                    //增加供应商退货次数
                    SupplierViewModel svm = new SupplierViewModel();
                    new SupplierHelper().UpdateSupplierTuihui(Convert.ToInt32(inorder.Supplier), totalmoney);

                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        //读取所有退库单及所属的商品信息
        public List<OutOrderModel> ReadOutOrder(int page, int rows)
        {
            List<OutOrderModel> result = new List<OutOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                int ordernum;
                var temp = (from a in db.WH_OutOrder
                            orderby a.AutoID descending
                            where a.OrderNum != null
                           select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    OutOrderModel item = new OutOrderModel();
                    item.RecordList = new List<OutOrderRecordModel>();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    ordernum = Convert.ToInt32(t.AutoID);//OrderNum
                    item.Position = t.Position;
                    item.Supplier = t.Supplier;
                    item.OrderGuid = t.Guid;
                    //读取进货单明细
                    var details = from b in db.WH_OutOrderDetails
                                  where b.OrderID == ordernum
                                  select b;
                    foreach (var detail in details)
                    {
                        OutOrderRecordModel record = new OutOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = detail.ProductName;
                        record.TotalPrice = detail.TotalPrice;
                        record.Remark = detail.Remark;
                        double ss=Convert.ToDouble(record.TotalPrice);
                        item.TotalPriceAll = item.TotalPriceAll + ss;
                        item.RecordList.Add(record);
                    }

                    result.Add(item);
                }
            }
            return result;
        }
        //读取所有入库单及所属的商品信息
        public List<OutOrderModel> ReadOutOrder()
        {
            List<OutOrderModel> result = new List<OutOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.WH_OutOrder
                           select a;
                foreach (var t in temp)
                {
                    OutOrderModel item = new OutOrderModel();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    item.Position = t.Position;
                    item.Supplier = t.Supplier;
                    item.OrderGuid = t.Guid;
                    //读取进货单明细
                    var details = from b in db.WH_OutOrderDetails
                                  where b.OrderGuid == item.OrderGuid
                                  select b;
                    foreach (var detail in details)
                    {
                        OutOrderRecordModel record = new OutOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = detail.ProductName;
                        record.TotalPrice = detail.TotalPrice;
                        record.Remark = detail.Remark;
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
                }
            }
            return result;
        }

        //读取入库单信息,仅入库单部分,不包含商品记录信息
        public List<OutOrderModel> ReadOnlyOutOrder()
        {
            List<OutOrderModel> result = new List<OutOrderModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //入库单信息
                var query = from a in db.WH_OutOrder
                            select a;
                //entity to model
                foreach (var q in query)
                {
                    OutOrderModel temp = new OutOrderModel();
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
        public List<OutOrderRecordModel> ReadOutOrderRecordByGuid(Guid guid)
        {
            List<OutOrderRecordModel> result = new List<OutOrderRecordModel>();
            using (var db = new HotelDBEntities())
            {
                var query = from a in db.WH_OutOrderDetails
                            where a.OrderGuid == guid
                            select a;
                foreach (var q in query)
                {
                    OutOrderRecordModel temp = new OutOrderRecordModel();
                    temp.Counts = q.Counts;
                    temp.ID = q.AutoID;
                    temp.InPrice = q.InPrice;
                    temp.OrderID = q.OrderID;
                    temp.ProductId = q.ProductId == null ? 0 : (int)q.ProductId;
                    temp.ProductName = q.ProductName;
                    temp.TotalPrice = q.TotalPrice;
                    temp.Remark = q.Remark;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read
        public List<OutOrderGoodsModel> ReadGoodsBySupplierAndPositioin(int supplierId, int positionId)
        {
            List<OutOrderGoodsModel> result = new List<OutOrderGoodsModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodsStock
                             join g in _db.WH_Goods on a.GoodsID equals g.GoodsNo
                             join i in _db.WH_InOrderDetails on a.OrderID equals i.OrderID
                             where a.SupplierID == supplierId &&
                                    a.PositionID == positionId &&
                                    !a.IsUsed && !a.ISDELETE
                             select new
                             {
                                 a.ID,
                                 a.GoodsID,
                                 a.OrderID,
                                 a.ISDELETE,
                                 a.SupplierID,
                                 a.PositionID,
                                 g.GoodsName,
                                 g.GoodsStyle,
                                 g.Unit,
                                 i.InPrice,
                             });
                //var groupQuery = from a in query
                //                 group a by new { a.GoodsID, a.ISDELETE, a.SupplierID, a.PositionID, a.GoodsName, a.GoodsStyle, a.Unit } into c
                //                 select c;
                var groupQuery = (from a in query
                                  group a by new { a.GoodsID, a.ISDELETE, a.SupplierID, a.PositionID, a.GoodsName, a.GoodsStyle, a.Unit} into c
                                  select new
                                  {
                                      c.Key.GoodsID,
                                      c.Key.ISDELETE,
                                      c.Key.SupplierID,
                                      c.Key.PositionID,
                                      c.Key.GoodsName,
                                      c.Key.GoodsStyle,
                                      c.Key.Unit,
                                      Counts = c.Count(),
                                      Avg = c.Average(d => d.InPrice)
                                  }).ToArray();
                foreach (var g in groupQuery)
                {
                    OutOrderGoodsModel temp = new OutOrderGoodsModel();
                    var s = g;
                    //temp.ID = s.ID;
                    temp.GoodsID = s.GoodsID;
                    //temp.OrderID = s.OrderID;
                    temp.ISDELETE = s.ISDELETE;
                    temp.SupplierID = s.SupplierID;
                    temp.PositionID = s.PositionID;
                    temp.GoodsName = s.GoodsName;
                    temp.InPrice = s.Avg;
                    temp.GoodsStyle = s.GoodsStyle;
                    temp.Unit = s.Unit;
                    temp.Counts = s.Counts;
                    result.Add(temp);
                }
            }
            return result;
        }

        public List<OutOrderGoodsModel> ReadGoodsByPositioin( int positionId)
        {
            List<OutOrderGoodsModel> result = new List<OutOrderGoodsModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodsStock
                             join g in _db.WH_Goods on a.GoodsID equals g.GoodsNo
                             join i in _db.WH_InOrderDetails on a.OrderID equals i.OrderID
                             where 
                                    a.PositionID == positionId &&
                                    !a.IsUsed && !a.ISDELETE
                             select new
                             {
                                 a.ID,
                                 a.GoodsID,
                                 a.OrderID,
                                 a.ISDELETE,
                                 a.SupplierID,
                                 a.PositionID,
                                 g.GoodsName,
                                 g.GoodsStyle,
                                 g.Unit,
                                 i.InPrice,
                             }).Distinct();
                //var groupQuery = from a in query
                //                 group a by new { a.GoodsID, a.ISDELETE, a.SupplierID, a.PositionID, a.GoodsName, a.GoodsStyle, a.Unit } into c
                //                 select c;
                var groupQuery = (from a in query
                                  group a by new { a.GoodsID, a.ISDELETE, a.SupplierID, a.PositionID, a.GoodsName, a.GoodsStyle, a.Unit } into c
                                  select new
                                  {
                                      c.Key.GoodsID,
                                      c.Key.ISDELETE,
                                      c.Key.SupplierID,
                                      c.Key.PositionID,
                                      c.Key.GoodsName,
                                      c.Key.GoodsStyle,
                                      c.Key.Unit,
                                      Counts = c.Count(),
                                      Avg = c.Average(d => d.InPrice)
                                  }).ToArray();
                foreach (var g in groupQuery)
                {
                    OutOrderGoodsModel temp = new OutOrderGoodsModel();
                    var s = g;
                    //temp.ID = s.ID;
                    temp.GoodsID = s.GoodsID;
                    //temp.OrderID = s.OrderID;
                    temp.ISDELETE = s.ISDELETE;
                    temp.SupplierID = s.SupplierID;
                    temp.PositionID = s.PositionID;
                    temp.GoodsName = s.GoodsName;
                    temp.InPrice = s.Avg;
                    temp.GoodsStyle = s.GoodsStyle;
                    temp.Unit = s.Unit;
                    temp.Counts = s.Counts;
                    result.Add(temp);
                }
            }
            return result;
        }

        //更新整个入库单
        public string UpdateOutOrder(OutOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_OutOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.Position = inorder.Position;
                    model.Supplier = inorder.Supplier;
                    var details = db.WH_OutOrderDetails.Where(a => a.OrderID == inorder.ID);
                    if (details != null)
                    {
                        foreach (var r in details)
                        {
                            WH_OutOrderDetails detail = new WH_OutOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            detail.Remark = r.Remark;
                            db.WH_OutOrderDetails.AddObject(detail);
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
        public string DeleteOutOrder(int inorderid)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_OutOrder.Where(a => a.AutoID == inorderid).SingleOrDefault();
                if (model != null)
                {
                    var detail = db.WH_OutOrderDetails.Where(a => a.OrderID == inorderid);
                    foreach (var r in detail)
                    {
                        db.WH_OutOrderDetails.DeleteObject(r);
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

        //读取所有退库单及所属的商品信息
        public List<InOrderModel> ReadTuihuoOrder(int page, int rows)
        {
            List<InOrderModel> result = new List<InOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.WH_InOrder
                            join s in db.WH_Supplier on System.Data.Objects.SqlClient.SqlFunctions.IsNumeric(a.Supplier) equals s.AutoID
                            join p in db.WH_Position on System.Data.Objects.SqlClient.SqlFunctions.IsNumeric(a.Position) equals p.AutoID
                            orderby a.AutoID ascending
                            where a.OrderNum != null
                            select new
                            {
                                a,
                                s,
                                p
                            }).Skip((page - 1) * rows).Take(rows);
                foreach (var t1 in temp)
                {
                    var t = t1.a;
                    var supplier = t1.s;
                    var position = t1.p;
                    InOrderModel item = new InOrderModel();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    item.Supplier = supplier.SupplierName;
                    item.Position = position.LocName;
                    item.OrderGuid = t.Guid;
                    item.RecordList = new List<InOrderRecordModel>();
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
                        record.Used = detail.Used;
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
                }
            }
            return result;
        }
    }
    //进货单信息
    public class OutOrderModel
    {
        public int ID { get; set; }
        public string OrderNum { get; set; }
        public System.Guid? OrderGuid { get; set; }
        public string Supplier { get; set; }
        public string Position { get; set; }
        public DateTime? Date { get; set; }
        public List<OutOrderRecordModel> RecordList { get; set; }
        public double TotalPriceAll { get; set; } //退货单总价
    }
    //退货记录信息
    public class OutOrderRecordModel
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? Counts { get; set; }
        public decimal? InPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Remark { get; set; }
    }

    //退货商品信息
    public class OutOrderGoodsModel
    {
        public int ID { get; set; }
        public string GoodsName{ get; set; }
        public decimal? InPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public string Used { get; set; }
        public int GoodsID { get; set; }
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public int PositionID { get; set; }
        public bool IsUsed { get; set; }
        public bool ISDELETE { get; set; }
        public string GoodsStyle { get; set; }
        public string Unit { get; set; }
        public int Counts { get; set; }
    }
    //通用货记录信息
    [Serializable]
    public class CommonOrderRecordJsonModel
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
        private string remark;
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
            }
        }
    }
}
