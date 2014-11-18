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
    public class GoodsHelper
    {
        //read
        public List<GoodsViewModel> ReadGoods()
        {
            List<GoodsViewModel> result = new List<GoodsViewModel>();
            using(HotelDBEntities _db=new HotelDBEntities())
            {
               
                var query=from a in _db.WH_Goods
                          select a;
                foreach(var q in query)
                {
                    GoodsViewModel temp=new GoodsViewModel();
                    temp.ID = q.GoodsNo;
                    temp.GoodsName = q.GoodsName;
                    temp.GoodsType = q.GoodsType;
                    temp.GoodsStyle = q.GoodsStyle;
                    temp.Unit = q.Unit;
                    temp.SalePrice = q.SalePrice;
                    //temp.IsScore = q.IsScore;
                    temp.IsStoreManage = q.IsStoreManage;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read part
        public List<GoodsViewModel> ReadPartGoods(int page, int rows)
        {
            List<GoodsViewModel> result = new List<GoodsViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_Goods
                             orderby a.GoodsNo descending
                            select a).Skip((page - 1) * rows).Take(rows);;
                foreach (var q in query)
                {
                    GoodsViewModel temp = new GoodsViewModel();
                    temp.ID = q.GoodsNo;
                    temp.GoodsName = q.GoodsName;
                    temp.GoodsType = q.GoodsType;
                    temp.GoodsStyle = q.GoodsStyle;
                    temp.Unit = q.Unit;
                    temp.SalePrice = q.SalePrice;
                    //temp.IsScore = q.IsScore;
                    temp.IsStoreManage = q.IsStoreManage;
                    result.Add(temp);
                }
            }
            return result;
        }
        //read part
        public List<GoodsViewModel> ReadPartGoods(int typeId, int page, int rows)
        {
            List<GoodsViewModel> result = new List<GoodsViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_Goods
                             orderby a.GoodsNo descending
                             where a.GoodsType == typeId
                             select a).Skip((page - 1) * rows).Take(rows);
                var goodsType = (from b in _db.WH_GoodType 
                                where b.AutoID == typeId 
                                select b).First();
                foreach (var q in query)
                {
                    GoodsViewModel temp = new GoodsViewModel();
                    temp.ID = q.GoodsNo;
                    temp.GoodsName = q.GoodsName;
                    temp.GoodsType = q.GoodsType;
                    temp.GoodsStyle = q.GoodsStyle;
                    temp.GoodsTypeName = goodsType.TypeName;
                    temp.Unit = q.Unit;
                    temp.SalePrice = q.SalePrice;
                    //temp.IsScore = q.IsScore;
                    temp.IsStoreManage = q.IsStoreManage;
                    temp.GoodsSimple = q.GoodsSimple;
                    result.Add(temp);
                }
            }
            return result;
        }

        //create
        public string CreateGoods(GoodsViewModel goods)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    WH_Goods temp = new WH_Goods();
                    temp.GoodsName = goods.GoodsName;
                    temp.GoodsType = goods.GoodsType;
                    temp.GoodsStyle = goods.GoodsStyle;
                    temp.Unit = goods.Unit;
                    temp.SalePrice = goods.SalePrice;
                    temp.IsScore = goods.IsScore;
                    temp.IsStoreManage = goods.IsStoreManage;
                    temp.GoodsSimple = goods.GoodsSimple;
                    db.WH_Goods.AddObject(temp);
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
        public string UpdateGoods(GoodsViewModel goods)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Goods.Where(a => a.GoodsNo == goods.ID).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.GoodsName = goods.GoodsName;
                        temp.GoodsType = goods.GoodsType;
                        temp.GoodsStyle = goods.GoodsStyle;
                        temp.Unit = goods.Unit;
                        temp.SalePrice = goods.SalePrice;
                        temp.IsScore = goods.IsScore;
                        temp.IsStoreManage = goods.IsStoreManage;
                        temp.GoodsSimple = goods.GoodsSimple;
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
        public string DeleteGoods(Int32 GoodsID)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_Goods.Where(a => a.GoodsNo == GoodsID).SingleOrDefault();
                    if (temp != null)
                    {
                        db.WH_Goods.DeleteObject(temp);
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

    public class GoodsViewModel
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string GoodsName { get; set; }
        public int TypeID { get; set; }
        public int GoodsType { get; set; }
        public string GoodsStyle { get; set; }
        public string GoodsTypeName { get; set; }
        public string Unit { get; set; }
        public decimal? SalePrice { get; set; }
        public bool? IsStoreManage { get; set; }
        public bool? IsScore { get; set; }
        public string GoodsSimple { get; set; }  //简写
    }
}
