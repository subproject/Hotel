using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    public class KMHelper
    {
        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="kf"></param>
        /// <returns></returns>
        public  string Create(KMViewModel _km)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    zd_km km = new zd_km();
                    km.a_km = _km.KM;
                    km.a_jd = _km.JD;
                    _db.zd_km.AddObject(km);
                    _db.SaveChanges();
                    return "0";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public  List<KMViewModel> ReadAll()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //Module for View
                List<KMViewModel> km = new List<KMViewModel>();
                var result =from a in _db.zd_km
                             select a;
                foreach (var r in result)
                {
                    KMViewModel temp = new KMViewModel();
                    temp.ID = r.AutoID;
                    temp.KM = r.a_km;
                    temp.JD = r.a_jd;
                    km.Add(temp);
                }
                return km;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public  List<KMViewModel> ReadPart(int page, int rows)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //Module for View
                List<KMViewModel> km = new List<KMViewModel>();
                var result = (from a in _db.zd_km
                              orderby a.AutoID descending
                              select a).Skip((page - 1) * rows).Take(rows);
                foreach (var r in result)
                {
                    KMViewModel temp = new KMViewModel();
                    temp.ID = r.AutoID;
                    temp.KM = r.a_km;
                    temp.JD = r.a_jd;
                    km.Add(temp);
                }
                return km;
            }
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public  string Update(KMViewModel update)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.zd_km.FirstOrDefault(a => a.AutoID == update.ID);
                    if (temp != null)
                    {
                        temp.a_km = update.KM;
                        temp.a_jd = update.JD;
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
        public  string Delete(Int32 autoid)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.zd_km.FirstOrDefault(a => a.AutoID == autoid);
                    if (temp != null)
                    {
                        _db.zd_km.DeleteObject(temp);
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
    }

    public class KMViewModel
    {
        public string KM;
        public string JD;
        public Int32 ID;
    }

}
