using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.FrontDesk
{
    public class YDWayHelper
    {
        #region 单例模式
        private static YDWayHelper _instance;
        public static YDWayHelper Instance
        {
            get { return _instance == null ? _instance = new YDWayHelper() : _instance; }
        }
        private YDWayHelper() { }

        #endregion

        public string CreateYDWay(YDWayModel way)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                YD_Way temp = new YD_Way();
                temp.Way = way.Way;
                db.YD_Way.AddObject(temp);
                db.SaveChanges();
                result = "0";
            }
            return result;
        }

        public string UpdateYDWay(YDWayModel way)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.YD_Way
                            where a.AutoID == way.AutoID
                            select a).FirstOrDefault();
                if (temp != null)
                {
                    temp.Way = way.Way;
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        public string DeleteYDWay(Int32 id)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.YD_Way
                            where a.AutoID == id
                            select a).FirstOrDefault();
                if (temp != null)
                {
                    db.YD_Way.DeleteObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        public List<YDWayModel> ReadYDWay()
        {
            List<YDWayModel> result = new List<YDWayModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.YD_Way
                            select a;
                foreach (var q in query)
                {
                    YDWayModel temp = new YDWayModel();
                    temp.AutoID = q.AutoID;
                    temp.Way = q.Way;
                    result.Add(temp);
                }
            }
            return result;
        }
    }

    public class YDWayModel
    {
        public Int32 AutoID { get; set; }
        public string Way { get; set; }
    }
}
