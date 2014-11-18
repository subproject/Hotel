using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 客房分类处理逻辑 CRUD
    /// </summary>
    public static class KFCgyHelper
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
        public static int Create(KFCgyViewModule _kfcgy)
        {
            try
            {
               using (HotelDBEntities _db = new HotelDBEntities())
               {
                zd_jb ctgy = new zd_jb();
                ctgy.kfjb = _kfcgy.KFJB;
                ctgy.lcf = _kfcgy.LCF;
                ctgy.f_cw = _kfcgy.CW;
                ctgy.f_dj = _kfcgy.DJ;
                ctgy.zdfj = _kfcgy.DDF;
                ctgy.zd_beginTime = _kfcgy.zdbeginTime;
                ctgy.zd_endTime = _kfcgy.zdendTime;
                ctgy.zd_jzPrice = _kfcgy.jzPrice;
                ctgy.zd_maxHours = _kfcgy.maxHours;
                ctgy.zd_minHours = _kfcgy.minHours;
                _db.zd_jb.AddObject(ctgy);
                _db.SaveChanges();
               }
                return 0;
            }
            catch {
                return 1;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<KFCgyViewModule> ReadAll()
        {
            //Module for View
            List<KFCgyViewModule> kf = new List<KFCgyViewModule>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.zd_jb
                             select a;
                foreach (var r in result)
                {
                    KFCgyViewModule temp = new KFCgyViewModule();
                    temp.ID = r.AutoID;
                    temp.KFJB = r.kfjb;
                    temp.CW = r.f_cw;
                    temp.DDF = r.zdfj;
                    temp.DJ = r.f_dj;
                    temp.LCF = r.lcf;
                    temp.minHours = r.zd_minHours;
                    temp.maxHours = r.zd_maxHours;
                    temp.jzPrice = r.zd_jzPrice;
                    temp.zdbeginTime = r.zd_beginTime;
                    temp.zdendTime = r.zd_endTime;
                    kf.Add(temp);
                }
            }
            return kf;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<KFCgyViewModule> ReadPart(int page, int rows)
        {
            //Module for View
            List<KFCgyViewModule> kf = new List<KFCgyViewModule>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.zd_jb
                              orderby a.AutoID descending
                              select a).Skip((page - 1) * rows).Take(rows);
                foreach (var r in result)
                {
                    KFCgyViewModule temp = new KFCgyViewModule();
                    temp.ID = r.AutoID;
                    temp.KFJB = r.kfjb;
                    temp.CW = r.f_cw;
                    temp.DDF = r.zdfj;
                    temp.DJ = r.f_dj;
                    temp.LCF = r.lcf;
                    temp.minHours = r.zd_minHours;
                    temp.maxHours = r.zd_maxHours;
                    temp.jzPrice = r.zd_jzPrice;
                    temp.zdbeginTime = r.zd_beginTime;
                    temp.zdendTime = r.zd_endTime;
                    kf.Add(temp);
                }
            }
            return kf;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(KFCgyViewModule update)
        {
            try
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    var temp = _db.zd_jb.FirstOrDefault(a => a.AutoID == update.ID);
                    if (temp != null)
                    {
                        temp.kfjb = update.KFJB;
                        temp.f_cw = update.CW;
                        temp.zdfj = update.DDF;
                        temp.f_dj = update.DJ;
                        temp.lcf = update.LCF;
                        temp.zd_beginTime = update.zdbeginTime;
                        temp.zd_endTime = update.zdendTime;
                        temp.zd_jzPrice = update.jzPrice;
                        temp.zd_maxHours = update.maxHours;
                        temp.zd_minHours = update.minHours;
                        _db.SaveChanges();
                        return "0";
                    }
                }
                return "1";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
        public static zdModule GetzdInfo(string fjtype )
        {
             zdModule  kf = new  zdModule();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.zd_jb
                              where a.kfjb == fjtype
                             select a);
                try
                {
                    if (result != null)
                    {
                        foreach (var r in result)
                        {
                            kf.zdf_stime = r.zdf_stime;
                            kf.zdf_etime = r.zdf_etime;
                            return kf;
                        }
                    }
                   
                }
                catch (Exception e)
                {
                    
                }
                return kf;
            }
        }
        public static zdModule GetzdInfo()
        {
            zdModule kf = new zdModule();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.zd_jb                             
                              select a);
                try
                {
                    if (result != null)
                    {
                        foreach (var r in result)
                        {
                            kf.zdf_stime = r.zdf_stime;
                            kf.zdf_etime = r.zdf_etime;
                            return kf;
                        }
                    }

                }
                catch (Exception e)
                {

                }
                return kf;
            }
        } 
        public static string UpdateTime(KFCgyViewModule   update)
        {
            List<KFCgyViewModule> kf = new List<KFCgyViewModule>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.zd_jb
                             select a;
                try
                {
                    foreach (var r in result)
                    {
                        r.zdf_stime = update.zdf_stime;
                        r.zdf_etime = update.zdf_etime;
                    }
                    _db.SaveChanges();
                    return "0";
                }
                catch (Exception e)
                {
                    return "1";
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
            try
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    var temp = _db.zd_jb.FirstOrDefault(a => a.AutoID == autoid);
                    if (temp != null)
                    {
                        _db.zd_jb.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                }
                return "1";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
    }

    //[DataContract]
    public class KFCgyViewModule
    {
        public Int32 ID { get; set; }
        //[DataMember]
        public string KFJB { get; set; }
        //[DataMember]
        public byte? CW { get; set; }
        //[DataMember]
        public Decimal? DJ { get; set; }
        //[DataMember]
        public Decimal? DDF { get; set; }
        //[DataMember]
        public Decimal? LCF { get; set; }

        public double? minHours { get; set; }

        public double? maxHours { get; set; }

        public decimal? jzPrice { get; set; }

        public string zdbeginTime { get; set; }

        public string zdendTime { get; set; }

        public string  zdf_stime { get; set; }
        public string  zdf_etime { get; set; }
    }
    public class zdModule
    {
        public string zdf_stime { get; set; }
        public string zdf_etime { get; set; }
    }
}
