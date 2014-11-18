using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.WareHouse
{
    /// <summary>
    /// 商品最新状态表
    /// </summary>
    public class GoodsStockHelper
    {
        //read
        public List<GoodsStockViewModel> ReadGoods()
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = from a in _db.WH_GoodsStock
                            select a;
                foreach (var q in query)
                {
                    GoodsStockViewModel temp = new GoodsStockViewModel();
                    temp.ID = q.ID;
                    temp.GoodsID = q.GoodsID;
                    temp.OrderID = q.OrderID;
                    temp.ISDELETE = q.ISDELETE;
                    temp.SupplierID = q.SupplierID;
                    temp.PositionID = q.PositionID;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read part
        public List<GoodsStockViewModel> ReadPartGoods(int page, int rows)
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodsStock
                             select a).Skip((page - 1) * rows).Take(rows); ;
                foreach (var q in query)
                {
                    GoodsStockViewModel temp = new GoodsStockViewModel();
                    temp.ID = q.ID;
                    temp.GoodsID = q.GoodsID;
                    temp.OrderID = q.OrderID;
                    temp.ISDELETE = q.ISDELETE;
                    temp.SupplierID = q.SupplierID;
                    temp.PositionID = q.PositionID;
                    result.Add(temp);
                }
            }
            return result;
        }
        //read part
        public List<GoodsStockViewModel> ReadPartGoods(int orderId, int page, int rows)
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodsStock
                             where a.OrderID == orderId
                             select a).Skip((page - 1) * rows).Take(rows);
                foreach (var q in query)
                {
                    GoodsStockViewModel temp = new GoodsStockViewModel();
                    temp.ID = q.ID;
                    temp.GoodsID = q.GoodsID;
                    temp.OrderID = q.OrderID;
                    temp.ISDELETE = q.ISDELETE;
                    temp.SupplierID = q.SupplierID;
                    temp.PositionID = q.PositionID;
                    result.Add(temp);
                }
            }
            return result;
        }
        //create
        public string CreateGoods(GoodsStockViewModel goodsStock)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    WH_GoodsStock temp = new WH_GoodsStock();
                    temp.OrderID = goodsStock.OrderID;
                    temp.GoodsID = goodsStock.GoodsID;
                    temp.SupplierID = goodsStock.SupplierID;
                    temp.PositionID = goodsStock.PositionID;
                    temp.AddTime = DateTime.Now;
                    db.WH_GoodsStock.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
                return result;
            }
        }

        //create
        public string CreateGoods(List<GoodsStockViewModel> goodsStockLst)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    foreach (var goodsStock in goodsStockLst)
                    {
                        WH_GoodsStock temp = new WH_GoodsStock();
                        temp.OrderID = goodsStock.OrderID;
                        temp.GoodsID = goodsStock.GoodsID;
                        temp.SupplierID = goodsStock.SupplierID;
                        temp.PositionID = goodsStock.PositionID;
                        temp.AddTime = DateTime.Now;
                        db.WH_GoodsStock.AddObject(temp);
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
                return result;
            }
        }

        //update
        public string UpdateGoods(GoodsStockViewModel goodsStock)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_GoodsStock.Where(a => a.ID == goodsStock.ID).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.OrderID = goodsStock.OrderID;
                        temp.GoodsID = goodsStock.GoodsID;
                        temp.ISDELETE = goodsStock.ISDELETE;
                        temp.SupplierID = goodsStock.SupplierID;
                        temp.PositionID = goodsStock.PositionID;
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //update
        public string UpdateGoods(List<GoodsStockViewModel> goodsStockLst)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    foreach (var goodsStock in goodsStockLst)
                    {
                        var temp = db.WH_GoodsStock.Where(a => a.ID == goodsStock.ID).SingleOrDefault();
                        if (temp != null)
                        {
                            temp.OrderID = goodsStock.OrderID;
                            temp.GoodsID = goodsStock.GoodsID;
                            temp.IsUsed = goodsStock.IsUsed;
                            temp.ISDELETE = goodsStock.ISDELETE;
                            temp.SupplierID = goodsStock.SupplierID;
                            temp.PositionID = goodsStock.PositionID;
                            temp.UsedTime = goodsStock.UsedTime;
                        }
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //delete
        public string DeleteGoods(Int32 id)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_GoodsStock.Where(a => a.ID == id).SingleOrDefault();
                    if (temp != null)
                    {
                        db.WH_GoodsStock.DeleteObject(temp);
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //delete
        public string DeleteGoodsByOrderId(Int32 id)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var query = db.WH_GoodsStock.Where(a => a.OrderID == id);
                    foreach (var q in query)
                    {
                        q.ISDELETE = true;
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //delete
        public string DeleteGoodsBySupplierId(Int32 supplierId)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var query = db.WH_GoodsStock.Where(a => a.SupplierID == supplierId);
                    foreach (var q in query)
                    {
                        q.ISDELETE = true;
                    }
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }

        //delete
        public List<GoodsStockViewModel> AllocationGoodsByPositionId(Int32 positionId, List<int> sectGoodsIds, List<int> sectCounts)
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    for (int i = 0; i < sectGoodsIds.Count; i++)
                    {
                        var goodsId = sectGoodsIds[i];
                        var counts = sectCounts[i];
                        var query = db.WH_GoodsStock.Where(a =>
                            a.PositionID == positionId &&
                            a.GoodsID == goodsId &&
                            !a.IsUsed &&
                            !a.ISDELETE).Take(counts);
                        foreach (var q in query)
                        {
                            q.IsUsed = true;
                            GoodsStockViewModel temp = new GoodsStockViewModel();
                            temp.ID = q.ID;
                            temp.GoodsID = q.GoodsID;
                            temp.OrderID = q.OrderID;
                            temp.IsUsed = false;
                            temp.ISDELETE = q.ISDELETE;
                            temp.SupplierID = q.SupplierID;
                            temp.PositionID = q.PositionID;
                            temp.AddTime = q.AddTime;
                            result.Add(temp);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }

        //delete
        public List<GoodsStockViewModel> AllocationGoodsByPositionId(Int32 positionId, int targetPositionId, List<int> sectGoodsIds, List<int> sectCounts)
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    for (int i = 0; i < sectGoodsIds.Count; i++)
                    {
                        var goodsId = sectGoodsIds[i];
                        var counts = sectCounts[i];
                        var query = db.WH_GoodsStock.Where(a =>
                            a.PositionID == positionId &&
                            a.GoodsID == goodsId &&
                            !a.IsUsed &&
                            !a.ISDELETE).Take(counts);
                        foreach (var q in query)
                        {
                            q.PositionID = targetPositionId;
                            GoodsStockViewModel temp = new GoodsStockViewModel();
                            temp.ID = q.ID;
                            temp.GoodsID = q.GoodsID;
                            temp.OrderID = q.OrderID;
                            temp.ISDELETE = q.ISDELETE;
                            temp.SupplierID = q.SupplierID;
                            temp.PositionID = positionId;
                            temp.AddTime = q.AddTime;
                            result.Add(temp);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }

        //delete
        public List<GoodsStockViewModel> DeleteGoodsBySupplierAndPosition(int supplierId, int positionId, List<int> sectGoodsIds, List<int> sectCounts)
        {
            List<GoodsStockViewModel> result = new List<GoodsStockViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    for (int i = 0; i < sectGoodsIds.Count; i++)
                    {
                        var goodsId = sectGoodsIds[i];
                        var counts = sectCounts[i];
                        var query = db.WH_GoodsStock.Where(
                            a => a.SupplierID == supplierId &&
                            a.PositionID == positionId &&
                            a.GoodsID == goodsId &&
                            !a.ISDELETE).Take(counts);
                        foreach (var q in query)
                        {
                            q.ISDELETE = true;
                            GoodsStockViewModel temp = new GoodsStockViewModel();
                            temp.ID = q.ID;
                            temp.GoodsID = q.GoodsID;
                            temp.OrderID = q.OrderID;
                            temp.ISDELETE = false;
                            temp.SupplierID = q.SupplierID;
                            temp.PositionID = q.PositionID;
                            temp.AddTime = q.AddTime;
                            result.Add(temp);
                        }
                    }
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }
    }

    public class GoodsStockViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string GoodsName { get; set; }
        public int GoodsID { get; set; }
        public int OrderID { get; set; }
        public int SupplierID { get; set; }
        public int PositionID { get; set; }
        public bool IsUsed { get; set; }
        public bool ISDELETE { get; set; }
        public DateTime? UsedTime { get; set; }
        public DateTime AddTime { get; set; }
    }
}
