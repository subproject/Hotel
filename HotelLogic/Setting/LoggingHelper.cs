using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    public class LoggingHelper
    {
        private static LoggingHelper _instance;
        public static LoggingHelper Instance
        {
            get { return _instance == null ? _instance=new LoggingHelper() : _instance; }
        }
        private LoggingHelper() { }

        public void LogInfo(LogModel data)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Logging temp = new Logging();
                temp.Info = data.Info;
                temp.Exception = data.Exception;
                temp.Module = data.Module;
                temp.Time = System.DateTime.Now;
                db.AddToLogging(temp);
                db.SaveChanges();
            }

        }

        public void LogString(string info)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Logging temp = new Logging();
                temp.Info = info;
                temp.Time = System.DateTime.Now;
                db.AddToLogging(temp);
                db.SaveChanges();
            }
        }
    }

    public class LogModel
    {
        public string Info { get; set; }
        public DateTime Time { get; set; }
        public string Exception { get; set; }
        public string Module { get; set; }
    }
}
