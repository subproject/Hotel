using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Data.Objects;

namespace HotelLogic.WareHouse
{
    public class SaleProfitQueryHelper
    {
        public List<SaleProfitQueryViewModel> query(DateTime sttime, DateTime endtime, string goodsId, string goodsposition, string goodsType)
        {
            List<SaleProfitQueryViewModel> temp = new List<SaleProfitQueryViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                int gid = -1,gpos=-1,gtype=-1;
                if (!string.IsNullOrEmpty(goodsId))
                {
                    gid = Convert.ToInt32(goodsId);
                }
                if (!string.IsNullOrEmpty(goodsposition))
                {
                    gpos = Convert.ToInt32(goodsposition);
                }
                if (!string.IsNullOrEmpty(goodsType))
                {
                    gtype = Convert.ToInt32(goodsType);
                }
                List<SpSaleProfitFZ> tempfz = _db.Sp_SaleProfitFun(sttime, endtime, gid, gpos, gtype).ToList();
                foreach (SpSaleProfitFZ fz in tempfz)
                {
                    SaleProfitQueryViewModel vm = new SaleProfitQueryViewModel();
                    vm.goodsName=fz.GoodsName;
                    vm.ID = fz.GoodsID;
                    vm.inPrice = fz.InPrice;
                    vm.agvSalePrice = fz.SalePrice;
                    vm.unit = fz.Unit;
                    vm.saleCount = fz.too;
                    vm.inTotal = fz.too * fz.InPrice;
                    vm.saleTotal = fz.SalePrice * fz.too;
                    vm.grossProfit = vm.saleTotal - vm.inTotal;
                    temp.Add(vm);
                }
               
            }
            return temp;
            //string sql = "1=1 "; SaleProfitQueryViewModel
            //decimal? posid=Convert.ToDecimal(goodsposition);
            //int goodid=Convert.ToInt32(goodsId);
            // int goodtype=Convert.ToInt32(goodsType);
            ////if (!string.IsNullOrEmpty(goodsName))
            ////{
            ////    sql+=""
            ////}
            //List<SaleProfitQueryViewModel> result = new List<SaleProfitQueryViewModel>();
            //using (HotelDBEntities _db = new HotelDBEntities())
            //{
                       
            //    var query = (from a in _db.WH_GoodsStock
            //                 where a.PositionID == posid&& a.GoodsID==goodid
            //                 join g in _db.WH_Goods on  a.GoodsID equals g.GoodsNo                             
            //                 where g.GoodsType == goodtype
            //                 select new
            //                 {
            //                     g.GoodsNo, 
            //                     g.SaleTimes,
            //                     g.InPrice,
            //                     g.OutPrice,
            //                     g.TotalInMoney,
            //                     g.TotalOutMoney,
            //                     g.TotalProfit,                                
            //                     g.Unit,
            //                     g.GoodsName,
            //                 });

            //    foreach (var q in query)
            //    {
            //        SaleProfitQueryViewModel temp = new SaleProfitQueryViewModel();
            //        temp.ID = q.id;
            //        temp.goodsName = q.GoodsName;
            //        temp.unit = q.Unit;
            //        temp.saleCount = q.saleCount;
            //        temp.inPrice = q.inPrice;
            //        temp.inTotal = q.inTotal;
            //        temp.agvSalePrice = q.agvSalePrice;
            //        temp.saleTotal = q.saleTotal;
            //        temp.grossProfit = q.grossProfit;
                
            //        result.Add(temp);
            //    }
         
            //}
            //return result;

            return null;
        }

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
    //public class PositionViewModel
    //{
    //    public int ID { get; set; }
    //    public string LocName { get; set; }
    //    public string LocCode { get; set; }
    //}
    public class SaleProfitQueryViewModel
    {
        public int ID { get; set; }
        public string goodsName { get; set; }
        public string unit { get; set; }

        public int? saleCount { get; set; }
        public decimal? inPrice { get; set; }
        public decimal? inTotal { get; set; }
        public decimal? agvSalePrice { get; set; }
        public decimal? saleTotal { get; set; }
        public decimal? grossProfit { get; set; }
    }
}
