using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    public class SettingHelper
    {
        #region 单例模式
        private static SettingHelper _instance;
        public static SettingHelper Instance
        {
            get {
                return _instance==null? _instance = new SettingHelper():_instance;
            }
        }
        private SettingHelper(){}
        #endregion
        //读取value
        public string ReadValue(string valuename)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var value = (from a in db.SystemSetting
                            where a.SettingName == valuename
                            select a.SettingValue).ToList().FirstOrDefault();
                return value;
            }
        }
        //重设value
        public string SetValue(string valuename,string settingvalue)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var value = (from a in db.SystemSetting
                            where a.SettingName == valuename
                            select a).ToList().FirstOrDefault();
                if (value != null)
                {
                    value.SettingValue = settingvalue;
                }
                db.SaveChanges();
                result = "0";
            }      
            return result;
        }
        //添加设置
        public string AddValue(string valuename, string settingvalue)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                SystemSetting temp = new SystemSetting();
                temp.SettingName = valuename;
                temp.SettingValue = settingvalue;
                db.SystemSetting.AddObject(temp);
                db.SaveChanges();
                result = "0";
            }
            return result;
        }
        //读取

    }
}
