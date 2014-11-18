using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 客房状态helper类
    /// </summary>
    public class KFZTHelper
    {
        /// <summary>
        /// 创建房间状态
        /// </summary>
        /// <param name="zt"></param>
        /// <returns></returns>
        public string createKFZT(KFZTModel zt)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                zd_FjZt temp = new zd_FjZt();
                temp.Color = zt.Color;
                temp.FjZt = zt.FjZt;
                db.zd_FjZt.AddObject(temp);
                db.SaveChanges();
                result = "0";
            }
            return result;
        }
        /// <summary>
        /// 更新房间状态
        /// </summary>
        /// <param name="zt"></param>
        /// <returns></returns>
        public string UpdateKFZT(KFZTModel zt)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.zd_FjZt
                           where a.AutoID == zt.AutoID
                           select a).FirstOrDefault();
                if (temp != null)
                {
                    temp.Color = zt.Color;
                    temp.FjZt = zt.FjZt;
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        /// <summary>
        /// 删除房间状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteKFZT(Int32 id)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.zd_FjZt
                           where a.AutoID == id
                           select a).FirstOrDefault();
                if (temp != null)
                {
                    db.zd_FjZt.DeleteObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        /// <summary>
        /// 读取所有房间状态基本信息
        /// </summary>
        /// <returns></returns>
        public List<KFZTModel> ReadKFZT()
        {
            List<KFZTModel> result = new List<KFZTModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.zd_FjZt
                            select a;
                foreach (var q in query)
                {
                    KFZTModel temp = new KFZTModel();
                    temp.AutoID = q.AutoID;
                    temp.Color = q.Color;
                    temp.FjZt = q.FjZt;
                    result.Add(temp);
                }
            }
            return result;
        }
    }

    public class KFZTModel
    {
        public Int32 AutoID { get; set; }
        public string FjZt { get; set; }
        public string Color { get; set; }
    }
}
