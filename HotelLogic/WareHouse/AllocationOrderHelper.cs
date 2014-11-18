using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.WareHouse
{
    public class AllocationOrderHelper
    {
        //生成整个入库单,一个入库单对应多个商品信息
        //RecordList为空则只生成入库单信息,对应表 Order
        public string CreateOrder(AllocationOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                WH_AllocationOrder temp = new WH_AllocationOrder();
                temp.OrderType = inorder.OrderType;
                temp.Guid = new Guid();
                temp.Date = inorder.Date;
                temp.OrderNum = inorder.OrderNum;
                temp.PositionId = inorder.PositionId;
                temp.SupplierId = inorder.SupplierId;
                temp.AllocationTypeId = inorder.AllocationTypeId;
                temp.DepartmentId = inorder.AllocationTypeId;
                db.WH_AllocationOrder.AddObject(temp);

                if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                {
                    foreach (var r in inorder.RecordList)
                    {
                        WH_AllocationOrderDetails detail = new WH_AllocationOrderDetails();
                        detail.OrderID = r.OrderID;
                        detail.Counts = r.Counts;
                        detail.InPrice = r.InPrice;
                        detail.OrderGuid = temp.Guid;
                        detail.ProductId = r.ProductId;
                        detail.ProductName = r.ProductName;
                        detail.TotalPrice = r.TotalPrice;
                        detail.Remark = r.Remark;
                        db.WH_AllocationOrderDetails.AddObject(detail);
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
                var order = (from a in db.WH_AllocationOrder
                             where a.Guid == guid
                             select a).SingleOrDefault();
                if (order != null)
                {
                    WH_AllocationOrderDetails temp = new WH_AllocationOrderDetails();
                    temp.Counts = record.Counts;
                    temp.InPrice = record.InPrice;
                    temp.OrderGuid = guid;
                    temp.ProductName = record.ProductName;
                    temp.TotalPrice = record.TotalPrice;
                    temp.Remark = record.Remark;
                    db.WH_AllocationOrderDetails.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        public string CreateOrderDetail(AllocationOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_AllocationOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.PositionId = inorder.PositionId;
                    model.TargetPositionId = inorder.TargetPositionId;
                    model.SupplierId = inorder.SupplierId;
                    model.AllocationTypeId = inorder.AllocationTypeId;
                    model.DepartmentId = inorder.AllocationTypeId;
                    if (inorder.RecordList != null && inorder.RecordList.Count > 0)
                    {
                        foreach (var r in inorder.RecordList)
                        {
                            WH_AllocationOrderDetails detail = new WH_AllocationOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            detail.Remark = r.Remark;
                            db.WH_AllocationOrderDetails.AddObject(detail);
                        }
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        //读取所有入库单及所属的商品信息 
        public List<AllocationOrderModel> ReadOrder(int page, int rows)
        {
            List<AllocationOrderModel> result = new List<AllocationOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.WH_AllocationOrder
                            join g in db.WH_Position  on a.PositionId equals g.AutoID
                            where a.OrderNum != null
                           orderby a.AutoID descending
                            select  new
                             {
                                 a.AutoID,
                                 a.Date,
                                 a.OrderNum,
                                 a.PositionId,
                                 a.SupplierId,
                                 a.AllocationTypeId,                                
                                 a.Guid,
                                 g.LocName ,
                             }).Skip((page - 1) * rows).Take(rows);

                int ordernum;
                try
                {
               
                if (temp== null||temp.Count()<=0)
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
                foreach (var t in temp)
                {
                    AllocationOrderModel item = new AllocationOrderModel();
                    item.RecordList = new List<AllocationOrderRecordModel>();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    ordernum = Convert.ToInt32(t.AutoID);//OrderNum
                    item.PositionId = t.PositionId;
                    item.SupplierId = t.SupplierId;
                    item.AllocationTypeId = t.AllocationTypeId;
                    item.DepartmentId = t.AllocationTypeId;
                    item.OrderGuid = t.Guid;
                    item.Position = t.LocName;
                    //读取进货单明细
                    var details = from b in db.WH_AllocationOrderDetails
                                  join f in db.WH_Goods on b.ProductId equals f.GoodsNo 
                                  where b.OrderID == ordernum
                                  select new
                                  {
                                      b.AutoID,
                                      b.Counts,
                                      b.InPrice,
                                      b.OrderID,
                                      b.ProductId,
                                     // b.ProductName,
                                      b.TotalPrice,
                                      b.Remark,
                                      f.GoodsName,
                                  };
                    foreach (var detail in details)
                    {
                        AllocationOrderRecordModel record = new AllocationOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = detail.GoodsName;
                        record.TotalPrice = detail.TotalPrice;
                        record.Remark = detail.Remark;
                        item.TotalPriceAll += Convert.ToDouble(record.TotalPrice);
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
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
        public List<AllocationOrderModel> ReadDiaoBoOrder(int page, int rows)
        {
            List<AllocationOrderModel> result = new List<AllocationOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.WH_AllocationOrder
                            join g in db.WH_Position on a.PositionId equals g.AutoID
                            where a.OrderNum != null && a.TargetPositionId != 0 && a.TargetPositionId != null
                            orderby a.AutoID descending
                            select new
                            {
                                a.AutoID,
                                a.Date,
                                a.OrderNum,
                                a.PositionId,
                                a.SupplierId,
                                a.AllocationTypeId,
                                a.Guid,
                                a.TargetPositionId,
                                g.LocName,
                            }).Skip((page - 1) * rows).Take(rows);

                int ordernum;
                try
                {

                    if (temp == null || temp.Count() <= 0)
                        return null;
                    //TargetPosition

                }
                catch (Exception ex)
                {
                    return null;
                }
                foreach (var t in temp)
                {
                 
                    AllocationOrderModel item = new AllocationOrderModel();
                    item.RecordList = new List<AllocationOrderRecordModel>();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    ordernum = Convert.ToInt32(t.AutoID);
                    item.PositionId = t.PositionId;
                    item.SupplierId = t.SupplierId;
                    item.AllocationTypeId = t.AllocationTypeId;
                    item.DepartmentId = t.AllocationTypeId;
                    item.OrderGuid = t.Guid;
                    item.Position = t.LocName;
                   item.TargetPosition = getPosition(Convert.ToInt32(t.TargetPositionId));
                    //读取进货单明细
                    var details = from b in db.WH_AllocationOrderDetails
                                  join f in db.WH_Goods on b.ProductId equals f.GoodsNo
                                  where b.OrderID == ordernum
                                  select new
                                  {
                                      b.AutoID,
                                      b.Counts,
                                      b.InPrice,
                                      b.OrderID,
                                      b.ProductId,
                                      // b.ProductName,
                                      b.TotalPrice,
                                      b.Remark,
                                      f.GoodsName,
                                  };
                    foreach (var detail in details)
                    {
                        AllocationOrderRecordModel record = new AllocationOrderRecordModel();
                        record.ID = detail.AutoID;
                        record.Counts = detail.Counts;
                        record.InPrice = detail.InPrice;
                        record.OrderID = detail.OrderID;
                        record.ProductId = detail.ProductId == null ? 0 : (int)detail.ProductId;
                        record.ProductName = detail.GoodsName;
                        record.TotalPrice = detail.TotalPrice;
                        record.Remark = detail.Remark;
                        item.TotalPriceAll += Convert.ToDouble(record.TotalPrice);
                        item.RecordList.Add(record);
                    }
                    result.Add(item);
                }
            }
            return result;
        }

        //读取所有入库单及所属的商品信息
        public List<AllocationOrderModel> ReadOrder()
        {
            List<AllocationOrderModel> result = new List<AllocationOrderModel>();
            //读取进货单信息
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.WH_AllocationOrder
                           select a;
                foreach (var t in temp)
                {
                    AllocationOrderModel item = new AllocationOrderModel();
                    item.Date = t.Date;
                    item.ID = t.AutoID;
                    item.OrderNum = t.OrderNum;
                    item.PositionId = t.PositionId;
                    item.SupplierId = t.SupplierId;
                    item.AllocationTypeId = t.AllocationTypeId;
                    item.DepartmentId = t.AllocationTypeId;
                    item.OrderGuid = t.Guid;
                    //读取进货单明细
                    var details = from b in db.WH_AllocationOrderDetails
                                  where b.OrderGuid == item.OrderGuid
                                  select b;
                    foreach (var detail in details)
                    {
                        AllocationOrderRecordModel record = new AllocationOrderRecordModel();
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
        public List<AllocationOrderModel> ReadOnlyOrder()
        {
            List<AllocationOrderModel> result = new List<AllocationOrderModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //入库单信息
                var query = from a in db.WH_AllocationOrder
                            select a;
                //entity to model
                foreach (var q in query)
                {
                    AllocationOrderModel temp = new AllocationOrderModel();
                    temp.Date = q.Date;
                    temp.ID = q.AutoID;
                    temp.OrderGuid = q.Guid;
                    temp.OrderNum = q.OrderNum;
                    temp.PositionId = q.PositionId;
                    temp.SupplierId = q.SupplierId;
                    temp.AllocationTypeId = q.AllocationTypeId;
                    temp.DepartmentId = q.AllocationTypeId;
                    temp.RecordList = null;
                    result.Add(temp);
 
                }
            }
            return result;

        }

        //读取某个入库单所有商品记录信息
        public List<AllocationOrderRecordModel> ReadOrderRecordByGuid(Guid guid)
        {
            List<AllocationOrderRecordModel> result = new List<AllocationOrderRecordModel>();
            using (var db = new HotelDBEntities())
            {
                var query = from a in db.WH_AllocationOrderDetails
                            where a.OrderGuid == guid
                            select a;
                foreach (var q in query)
                {
                    AllocationOrderRecordModel temp = new AllocationOrderRecordModel();
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
        public List<AllocationOrderGoodsModel> ReadGoodsByPositioin(int positionId)
        {
            List<AllocationOrderGoodsModel> result = new List<AllocationOrderGoodsModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodsStock
                             join g in _db.WH_Goods on a.GoodsID equals g.GoodsNo
                             where a.PositionID == positionId &&
                                    !a.IsUsed && !a.ISDELETE
                             select new
                             {
                                 a.ID,
                                 a.GoodsID,
                                 a.OrderID,
                                 a.ISDELETE,
                                 a.PositionID,
                                 g.GoodsName,
                                 g.GoodsStyle,
                                 g.Unit,
                                 g.SalePrice,
                             });
                var groupQuery = from a in query
                                 group a by new { a.GoodsID, a.ISDELETE, a.PositionID, a.GoodsName, a.GoodsStyle, a.Unit, a.SalePrice } into c
                             select c;
                foreach (var g in groupQuery)
                {
                    AllocationOrderGoodsModel temp = new AllocationOrderGoodsModel();
                    var s = g.Key;
                    //temp.ID = s.ID;
                    temp.GoodsID = s.GoodsID;
                    //temp.OrderID = s.OrderID;
                    temp.ISDELETE = s.ISDELETE;
                    temp.PositionID = s.PositionID;
                    temp.GoodsName = s.GoodsName;
                    temp.InPrice = s.SalePrice;
                    temp.GoodsStyle = s.GoodsStyle;
                    temp.Unit = s.Unit;
                    temp.Counts = g.Count();
                    result.Add(temp);
                }
            }
            return result;
        }

        public List<AllocationTypeModel> ReadAllocationType()
        {
            List<AllocationTypeModel> result = new List<AllocationTypeModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_AllocationType.AsQueryable();
                foreach (var r in model)
                {
                    AllocationTypeModel temp = new AllocationTypeModel();
                    temp.ID = r.ID;
                    temp.TypeName = r.TypeName;
                    result.Add(temp);
                }
            }
            return result;

        }
        
        //更新整个入库单
        public string UpdateOrder(AllocationOrderModel inorder)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_AllocationOrder.Where(a => a.AutoID == inorder.ID).SingleOrDefault();
                if (model != null)
                {
                    model.Guid = new Guid();
                    model.Date = inorder.Date;
                    model.OrderNum = inorder.OrderNum;
                    model.PositionId = inorder.PositionId;
                    model.SupplierId = inorder.SupplierId;
                    model.AllocationTypeId = inorder.AllocationTypeId;
                    model.DepartmentId = inorder.AllocationTypeId;
                    var details = db.WH_OutOrderDetails.Where(a => a.OrderID == inorder.ID);
                    if (details != null)
                    {
                        foreach (var r in details)
                        {
                            WH_AllocationOrderDetails detail = new WH_AllocationOrderDetails();
                            detail.OrderID = r.OrderID;
                            detail.Counts = r.Counts;
                            detail.InPrice = r.InPrice;
                            detail.OrderGuid = model.Guid;
                            detail.ProductId = r.ProductId;
                            detail.ProductName = r.ProductName;
                            detail.TotalPrice = r.TotalPrice;
                            detail.Remark = r.Remark;
                            db.WH_AllocationOrderDetails.AddObject(detail);
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
        public string DeleteOrder(int inorderid)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var model = db.WH_AllocationOrder.Where(a => a.AutoID == inorderid).SingleOrDefault();
                if (model != null)
                {
                    var detail = db.WH_AllocationOrderDetails.Where(a => a.OrderID == inorderid);
                    foreach (var r in detail)
                    {
                        db.WH_AllocationOrderDetails.DeleteObject(r);
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
    public class AllocationOrderModel
    {
        public int ID { get; set; }
        public int OrderType { get; set; }
        public string OrderNum { get; set; }
        public System.Guid? OrderGuid { get; set; }
        public int? SupplierId { get; set; }
        public int? DepartmentId { get; set; }
        public int? AllocationTypeId { get; set; }
        public int PositionId { get; set; }
        public int? TargetPositionId { get; set; }
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public List<AllocationOrderRecordModel> RecordList { get; set; }

        private DateTime date = DateTime.Now;
        public double TotalPriceAll { get; set; }
        public string Position { get; set; }
        public string TargetPosition { get; set; }
    }
    //退货记录信息
    public class AllocationOrderRecordModel
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
    public class AllocationOrderGoodsModel
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

    public class AllocationTypeModel{
        public int ID{get;set;}
        public string TypeName {get;set;}
    }
}
