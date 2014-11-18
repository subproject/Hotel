using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.WareHouse
{
    public class PositionHelper
    {
        //read
        public List<PositionViewModel> ReadPosition()
        {
            List<PositionViewModel> result = new List<PositionViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = from a in _db.WH_Position
                            select a;
                foreach (var q in query)
                {
                    PositionViewModel temp = new PositionViewModel();
                    temp.ID = q.AutoID;
                    temp.LocCode = q.LocCode;
                    temp.LocName = q.LocName;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read part
        public List<PositionViewModel> ReadPartPosition(int page,int rows)
        {
            List<PositionViewModel> result = new List<PositionViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_Position
                             orderby a.AutoID descending
                            select a).Skip((page - 1) * rows).Take(rows);;
                foreach (var q in query)
                {
                    PositionViewModel temp = new PositionViewModel();
                    temp.ID = q.AutoID;
                    temp.LocCode = q.LocCode;
                    temp.LocName = q.LocName;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read part
        public List<SupplierGoodsViewModel> ReadPartPositionGoods(int positionId)
        {
            List<SupplierGoodsViewModel> result = new List<SupplierGoodsViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                if (positionId > 0)
                {
                    var query = from a in _db.WH_GoodsStock
                                join g in _db.WH_Goods on a.GoodsID equals g.GoodsNo
                                join i in _db.WH_InOrderDetails on a.OrderID equals i.OrderID
                                where a.IsUsed == false && a.ISDELETE == false && a.PositionID == positionId
                                group a by new
                                {
                                    g.GoodsNo,
                                    g.GoodsName,
                                    g.GoodsStyle,
                                    g.Unit,
                                    g.SalePrice
                                } into c
                                select new {
                                    c.Key.GoodsNo,
                                    c.Key.GoodsName,
                                    c.Key.GoodsStyle,
                                    c.Key.Unit,
                                    c.Key.SalePrice,
                                    Counts = c.Count()
                                };
                    var queryInorderCounts = from a in _db.WH_InOrder
                                             join i in _db.WH_InOrderDetails on a.AutoID equals i.OrderID
                                             where System.Data.Objects.SqlClient.SqlFunctions.IsNumeric(a.Position) == positionId
                                             group i by new
                                             {
                                                 i.ProductId
                                             } into c
                                             select new {
                                                c.Key.ProductId,
                                                Counts = c.Count()
                                             };
                    var queryAllocationOrderCounts = from a in _db.WH_AllocationOrder
                                                     join i in _db.WH_AllocationOrderDetails on a.AutoID equals i.OrderID
                                                     where a.PositionId == positionId
                                                     group i by new
                                                     {
                                                         i.ProductId
                                                     } into c
                                                      select new {
                                                c.Key.ProductId,
                                                Counts = c.Count()
                                             };
                    foreach (var q in query)
                    {
                        SupplierGoodsViewModel temp = new SupplierGoodsViewModel();
                        temp.GoodsName = q.GoodsName;
                        temp.GoodsStyle = q.GoodsStyle;
                        temp.Unit = q.Unit;
                        temp.Price = q.SalePrice;
                        temp.Counts = q.Counts;
                        temp.TotalInCounts = 0;
                        foreach (var item in queryInorderCounts)
                        {
                            if (q.GoodsNo == item.ProductId)
                                temp.TotalAllocationCounts = item.Counts;
                        }
                        temp.TotalAllocationCounts = 0;
                        foreach (var item in queryAllocationOrderCounts)
                        {
                            if (q.GoodsNo == item.ProductId)
                                temp.TotalAllocationCounts = item.Counts;
                        }

                        result.Add(temp);
                    }
                }
                else
                {
                    var query = from a in _db.WH_GoodsStock
                                join g in _db.WH_Goods on a.GoodsID equals g.GoodsNo
                                join i in _db.WH_InOrderDetails on a.OrderID equals i.OrderID
                                where a.IsUsed == false && a.ISDELETE == false
                                group a by new
                                {
                                    g.GoodsNo,
                                    g.GoodsName,
                                    g.GoodsStyle,
                                    g.Unit,
                                    g.SalePrice
                                } into c
                                select new
                                {
                                    c.Key.GoodsNo,
                                    c.Key.GoodsName,
                                    c.Key.GoodsStyle,
                                    c.Key.Unit,
                                    c.Key.SalePrice,
                                    Counts = c.Count()
                                };
                    var queryInorderCounts = from a in _db.WH_InOrder
                                             join i in _db.WH_InOrderDetails on a.AutoID equals i.OrderID
                                             group i by new
                                             {
                                                 i.ProductId
                                             } into c
                                             select new
                                             {
                                                 c.Key.ProductId,
                                                 Counts = c.Count()
                                             };
                    var queryAllocationOrderCounts = from a in _db.WH_AllocationOrder
                                                     join i in _db.WH_AllocationOrderDetails on a.AutoID equals i.OrderID
                                                     group i by new
                                                     {
                                                         i.ProductId
                                                     } into c
                                                     select new
                                                     {
                                                         c.Key.ProductId,
                                                         Counts = c.Count()
                                                     };
                    foreach (var q in query)
                    {
                        SupplierGoodsViewModel temp = new SupplierGoodsViewModel();
                        temp.GoodsName = q.GoodsName;
                        temp.GoodsStyle = q.GoodsStyle;
                        temp.Unit = q.Unit;
                        temp.Price = q.SalePrice;
                        temp.Counts = q.Counts;
                        temp.TotalInCounts = 0;
                        foreach (var item in queryInorderCounts)
                        {
                            if (q.GoodsNo == item.ProductId)
                                temp.TotalAllocationCounts = item.Counts;
                        }
                        temp.TotalAllocationCounts = 0;
                        foreach (var item in queryAllocationOrderCounts)
                        {
                            if (q.GoodsNo == item.ProductId)
                                temp.TotalAllocationCounts = item.Counts;
                        }
                        
                        result.Add(temp);
                    }
                }

            }
            return result;
        }

        //create
        public string CreatePosition(PositionViewModel position)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    WH_Position temp = new WH_Position();
                    temp.LocCode = position.LocCode;
                    temp.LocName = position.LocName;
                    db.WH_Position.AddObject(temp);
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
        public string UpdatePosition(PositionViewModel position)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Position.Where(a => a.AutoID == position.ID).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.LocCode = position.LocCode;
                        temp.LocName = position.LocName;
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
        public string DeletePosition(Int32 positionID)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Position.Where(a => a.AutoID == positionID).SingleOrDefault();
                    if (temp != null)
                    {
                        db.WH_Position.DeleteObject(temp);
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
    }
    public class PositionViewModel
    {
        public int ID { get; set; }
        public string LocName { get; set; }
        public string LocCode { get; set; }
    }
}
