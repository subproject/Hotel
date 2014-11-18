using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 黑名单管理
    /// </summary>
    public class BlockedManHelper
    {
        /// <summary>
        /// 创建黑名单
        /// </summary>
        /// <param name="bm"></param>
        /// <returns></returns>
        public string CreateBM(BMModel bm)
        {
            string result="-1";
            using(HotelDBEntities db=new HotelDBEntities())
            {
                Setting_BlockedMan temp=new Setting_BlockedMan();
                temp.Name=bm.Name;
                temp.IDCardNo=bm.IDCardNo;
                temp.Remark=bm.Remark;
                db.Setting_BlockedMan.AddObject(temp);
                db.SaveChanges();
                result="0";
            }
            return result;
        }
        /// <summary>
        /// 读取所有的黑名单
        /// </summary>
        /// <returns></returns>
        public List<BMModel> ReadBM()
        {
            List<BMModel> result=new List<BMModel>();
            using(HotelDBEntities db=new HotelDBEntities())
            {
                var query=from a in db.Setting_BlockedMan
                          select a;
                foreach(var bm in query)
                {
                    BMModel temp=new BMModel();
                    temp.AutoID = bm.AutoID;
                    temp.Name=bm.Name;
                    temp.IDCardNo=bm.IDCardNo;
                    temp.Remark=bm.Remark;
                    result.Add(temp);
                }
            }
            return result;
        }
        /// <summary>
        /// 删除黑名单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteBM(Int32 id)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.Setting_BlockedMan
                           where a.AutoID == id
                           select a).FirstOrDefault();
                if (temp != null)
                {
                    db.Setting_BlockedMan.DeleteObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
    }

    public class BMModel
    {
        public Int32 AutoID{get;set;}
        public string Name{get;set;}
        public string IDCardNo{get;set;}
        public string Remark{get;set;}
    }
}
