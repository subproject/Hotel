using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 折扣维护
    /// </summary>
    public static class ZK_weihuHelper
    {
        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Create(TqViewModel FH)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.td_tq.FirstOrDefault(a => a.isMorenZheKou == FH.isMorenZheKou == true || a.tequanren == FH.tequanren);
                    if (temp == null)
                    {
                        td_tq ctgy = new td_tq();
                        ctgy.tequanren = FH.tequanren;
                        ctgy.fjzhekou = FH.fjzhekou;
                        ctgy.isMorenZheKou = FH.isMorenZheKou;
                        ctgy.timeLimite = FH.timeLimite;
                        ctgy.validDate = FH.validDate;
                        _db.td_tq.AddObject(ctgy);
                        _db.SaveChanges();
                    }
                    else
                    {
                        return "1";
                    }


                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return "0";
            }
        }
        /// <summary>
        /// read
        /// </summary>
        /// <returns></returns>
        public static List<TqViewModel> ReadAll()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //Module for View
                List<TqViewModel> kf = new List<TqViewModel>();
                var result = from a in _db.td_tq
                             select a;
                foreach (var FH in result)
                {
                    TqViewModel ctgy = new TqViewModel();
                    ctgy.id = FH.id;
                    ctgy.tequanren = FH.tequanren;
                    ctgy.fjzhekou = FH.fjzhekou;
                    ctgy.isMorenZheKou1 = FH.isMorenZheKou == true ? "是" : "否";
                    ctgy.timeLimite1 = FH.timeLimite == true ? "是" : "否";
                    ctgy.validDate = FH.validDate;
                    //temp.IsCharge2 = r.IsCharge 
                    kf.Add(ctgy);
                }
                return kf;
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(TqViewModel update)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.td_tq.FirstOrDefault(a => a.id == update.id);
                    if (temp != null)
                    {
                        temp.id = update.id;
                        temp.tequanren = update.tequanren;
                        temp.fjzhekou = update.fjzhekou;
                        temp.isMorenZheKou = update.isMorenZheKou;
                        if (update.isMorenZheKou == true)
                        {
                            var t = _db.td_tq.FirstOrDefault(a => a.isMorenZheKou == true);
                            if (t != null)
                            {
                                t.isMorenZheKou = false;
                                _db.SaveChanges();
                            }

                        }
                        temp.timeLimite = update.timeLimite;
                        temp.validDate = update.validDate;
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
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Delete(Int32 autoid)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.td_tq.FirstOrDefault(a => a.id == autoid);
                    if (temp != null)
                    {
                        _db.td_tq.DeleteObject(temp);
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
        }
        public static int getLastAutoID()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = _db.td_tq.FirstOrDefault(a => a.id == _db.td_tq.Max(y => y.id));

                return Convert.ToInt32(result.id) + 1;


            }
        }
        public static TqViewModel gUserInfo(Int32 rowID)
        {
            //Module for View
            TqViewModel Mb = new TqViewModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.td_tq where a.id == rowID select a);

                foreach (var r in result)
                {
                    TqViewModel temp = new TqViewModel();
                    temp.id = r.id;
                    temp.tequanren = r.tequanren;
                    temp.fjzhekou = r.fjzhekou;
                    temp.isMorenZheKou = r.isMorenZheKou;
                    temp.timeLimite = r.timeLimite;
                    temp.validDate = r.validDate;

                    Mb = temp;
                }
            }
            return Mb;
        }

    }

    public class TqViewModel
    {
        public Int32 id { get; set; }
        public string tequanren { get; set; }
        public decimal? fjzhekou { get; set; }
        public bool? isMorenZheKou { get; set; }
        public bool? timeLimite { get; set; }
        public Int32? validDate { get; set; }
        public string isMorenZheKou1 { get; set; }
        public string timeLimite1 { get; set; }
    }
}
