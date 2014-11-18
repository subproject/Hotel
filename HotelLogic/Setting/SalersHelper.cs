using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    /// <summary>
    /// helper类
    /// </summary>
    public class SalersHelper
    {
        #region 单例模式
        private static SalersHelper _instance;
        public static SalersHelper Instance
        {
            get { return _instance == null ? _instance = new SalersHelper() : _instance; }
        }
        private SalersHelper(){}

        #endregion
        /// <summary>
        /// salers
        /// </summary>
        /// <param name="saler"></param>
        /// <returns></returns>
        public string CreateSalers(SalersModel saler)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Base_Salers temp = new Base_Salers();
                temp.Name = saler.Name;
                db.Base_Salers.AddObject(temp);
                db.SaveChanges();
                result = "0";
            }
            return result;
        }
        /// <summary>
        /// update salers
        /// </summary>
        /// <param name="saler"></param>
        /// <returns></returns>
        public string UpdateSalers(SalersModel saler)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.Base_Salers
                            where a.AutoID == saler.AutoID
                            select a).FirstOrDefault();
                if (temp != null)
                {
                    temp.Name = saler.Name;
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        /// <summary>
        /// delete salers
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteSalers(Int32 id)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.Base_Salers
                            where a.AutoID == id
                            select a).FirstOrDefault();
                if (temp != null)
                {
                    db.Base_Salers.DeleteObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        /// <summary>
        /// read all salers
        /// </summary>
        /// <returns></returns>
        public List<SalersModel> ReadSalers()
        {
            List<SalersModel> result = new List<SalersModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Base_Salers
                            select a;
                foreach (var q in query)
                {
                    SalersModel temp = new SalersModel();
                    temp.AutoID = q.AutoID;
                    temp.Name = q.Name;
                    result.Add(temp);
                }
            }
            return result;
        }
    }

    public class SalersModel
    {
        public Int32 AutoID { get; set; }
        public string Name { get; set; }
    }
}
