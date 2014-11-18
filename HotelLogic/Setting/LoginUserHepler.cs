using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.Setting
{
    public class LoginUserHepler
    {
        /// <summary>
        /// Context
        /// </summary>
        private static readonly HotelDBEntities _db = new HotelDBEntities();

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public string Create(LoginUserModel User)
        {
            try
            {
                LoginUser ctgy = new LoginUser();
                ctgy.LoginName = User.LoginName;
                ctgy.LoginTime = DateTime.Now;
                ctgy.Password = User.Password;
                ctgy.ComputerName = User.ComputerName;

                _db.LoginUser.AddObject(ctgy);
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public  string Update(LoginUserModel update)
        {
            try
            {
                var temp = _db.LoginUser.FirstOrDefault(a => a.LoginName == update.LoginName);
                if (temp != null)
                {
                    temp.LoginTime = DateTime.Now;
                    temp.ComputerName = update.ComputerName;
                    _db.SaveChanges();
                    return "0";
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        public LoginUserModel Login(LoginUserModel User)
        {
            
           // var temp = _db.LoginUser.FirstOrDefault(a => a.LoginName == User.LoginName);
            LoginUserModel ctgy = new LoginUserModel();
            //if (temp != null)
            //{
                //if (temp.Password == null || temp.Password == "")
                //{
                //    ctgy.LoginName = temp.LoginName;
                //    ctgy.Password = "";

                //}
                //else
                //{
                  var  temp = _db.LoginUser.FirstOrDefault(a => a.LoginName == User.LoginName && a.Password == User.Password);
                    if (temp != null)
                    {
                        ctgy.LoginName = temp.LoginName;
                        ctgy.Password = temp.Password;
                    }
                    else {
                        ctgy = null;
                    }

                //}
            //}

            //else
            //{
            //    ctgy = null;
            //}
            return ctgy;
        }
        //查询收款时相关的一些显示信息
        public string Shoukuanview(string fh)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                lock (db)
                {

                    var selresult = db.ExecuteStoreQuery<LoginUserModela>(@"SELECT  LoginName, ComputerName, LoginTime FROM  LoginUser");
                    foreach (var r in selresult)
                    {
                        string t = r.LoginName;
                    }
                }

            }
            return result;
        }


    
    }  
    public class LoginUserModel
    {
        public Int32 ID { get; set; }
        public DateTime LoginTime { get; set; }
        public string LoginName { get; set; }
        public string ComputerName { get; set; }
        public string Password { get; set; }
    }

    public class LoginUserModela
    {

        public string LoginName { get; set; }
        public string ComputerName { get; set; }
        public DateTime LoginTime { get; set; }
    }
}

