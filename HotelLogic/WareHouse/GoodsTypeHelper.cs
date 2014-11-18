using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Transactions;

namespace HotelLogic.WareHouse
{
    public class GoodsTypeHelper
    {
        //read
        public List<GoodsTypeViewModel> ReadGoodsType()
        {
            List<GoodsTypeViewModel> result = new List<GoodsTypeViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = from a in _db.WH_GoodType
                            orderby a.AutoID
                            select a;
                foreach (var q in query)
                {
                    GoodsTypeViewModel temp = new GoodsTypeViewModel();
                    temp.ID = q.AutoID;
                    temp.PareID = q.ParentID;
                    temp.TypeName = q.TypeName;
                    result.Add(temp);
                }
            }
            return result;
        }

        //read
        public List<GoodsTypeViewModel> ReadPartGoodsType(int page, int rows)
        {
            List<GoodsTypeViewModel> result = new List<GoodsTypeViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var query = (from a in _db.WH_GoodType
                             orderby a.ParentID, a.AutoID
                             select a).Skip((page - 1) * rows).Take(rows);
                foreach (var q in query)
                {
                    GoodsTypeViewModel temp = new GoodsTypeViewModel();
                    temp.ID = q.AutoID;
                    temp.PareID = q.ParentID;
                    temp.TypeName = q.TypeName;
                    result.Add(temp);
                }
            }
            return result;
        }
        //create
        public string CreateGoodsType(GoodsTypeViewModel type)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    WH_GoodType temp = new WH_GoodType();
                    temp.TypeName = type.TypeName;
                    temp.ParentID = type.PareID == 0 ? null : type.PareID;
                    db.WH_GoodType.AddObject(temp);
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
        public string UpdateGoodsType(GoodsTypeViewModel type)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_GoodType.Where(a => a.AutoID == type.ID).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.TypeName = type.TypeName;
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
        public string DeleteGoodsType(Int32 ID)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var temp = db.WH_GoodType.Where(a => a.AutoID == ID).SingleOrDefault();                    
                    if (temp != null)
                    {
                        var childtemplist = db.WH_GoodType.Where(a => a.ParentID == ID).ToList();
                        if (childtemplist != null && childtemplist.Count > 0)
                        {
                            result = "-1";
                            return result;
                        }
                        db.WH_GoodType.DeleteObject(temp);
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
        //select
        public string SelectGoodsType(string typename)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    var selentity =( from a in db.WH_GoodType
                                    where a.TypeName == typename
                                    select a.AutoID).SingleOrDefault();

                    result = selentity.ToString();
                }
                catch (Exception e)
                {
                    result = null;
                }
                return result;
            }
        }
        public List<GoodsTypeViewModel> AllData = new List<GoodsTypeViewModel>();
        /// <summary>
        /// 将全部类型数据转成为树形结构
        /// </summary>
        /// <returns></returns>
        public string ConverToTreeJson(List<GoodsTypeViewModel> data, string dataformat)
        {
            
            StringBuilder SbRJson = new StringBuilder();
            int i = 0;
            foreach (GoodsTypeViewModel item in data)
            {
                i++;
                SbRJson.Append("{" + string.Format(dataformat, item.ID.ToString(), item.TypeName, "", "", item.PareID));
                List<GoodsTypeViewModel> childdata = AllData.Where(n => n.PareID == item.ID).ToList<GoodsTypeViewModel>();
                if (childdata != null && childdata.Count > 0)
                {
                    SbRJson.Append("'children':[");
                    SbRJson.Append(ConverToTreeJson(childdata,dataformat));
                    SbRJson.Append("]");
                }
                else
                {
                    SbRJson.Append("'children':[]");
                }
                if (i <= data.Count - 1)
                {
                    SbRJson.Append("},");
                }
                else
                {
                    SbRJson.Append("}");
                }
            }

            return SbRJson.ToString();

        }
    }
    public class GoodsTypeViewModel
    {
        public int ID { get; set; }
        public int? PareID { get; set; }
        public string TypeName { get; set; }
    }
}
