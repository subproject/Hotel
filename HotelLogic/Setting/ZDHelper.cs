using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.Setting
{
    public static class ZDHelper
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
        public static string Create(ZDViewMedel r)
        {
            try
            {

                Zd_ZdFn temp = new Zd_ZdFn();
                temp.id = r.id;
                temp.f_jb = r.f_jb;
                temp.FangAnName = r.FangAnName;
                temp.StartLen = r.StartLen;
                temp.StartFee = r.StartFee;
                temp.AddLen = r.AddLen;
                temp.AddFee = r.AddFee;
                temp.MinLen = r.MinLen;
                temp.MinFee = r.MinFee;
                temp.MaxLen = r.MaxLen;

                _db.Zd_ZdFn.AddObject(temp);
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<ZDViewMedel> ReadAll()
        {
            //Module for View
            List<ZDViewMedel> kf = new List<ZDViewMedel>();
            var result = from a in _db.Zd_ZdFn
                         select a;
            foreach (var r in result)
            {
                ZDViewMedel temp = new ZDViewMedel();
                temp.id = r.id;
                temp.f_jb = r.f_jb;
                temp.FangAnName = r.FangAnName;
                temp.StartLen = r.StartLen;
                temp.StartFee = r.StartFee;
                temp.AddLen = r.AddLen;
                temp.AddFee = r.AddFee;
                temp.MinLen = r.MinLen;
                temp.MinFee = r.MinFee;
                temp.MaxLen = r.MaxLen;
                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<ZDViewMedel> ReadPart(int page, int rows)
        {
            //Module for View
            List<ZDViewMedel> kf = new List<ZDViewMedel>();
            var result = (from a in _db.Zd_ZdFn
                          orderby a.id descending
                          select a).Skip((page - 1) * rows).Take(rows);
            foreach (var r in result)
            {
                ZDViewMedel temp = new ZDViewMedel();
                temp.id = r.id;
                temp.f_jb = r.f_jb;
                temp.FangAnName = r.FangAnName;
                temp.StartLen = r.StartLen;
                temp.StartFee = r.StartFee;
                temp.AddLen = r.AddLen;
                temp.AddFee = r.AddFee;
                temp.MinLen = r.MinLen;
                temp.MinFee = r.MinFee;
                temp.MaxLen = r.MaxLen;

                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(ZDViewMedel update)
        {
            try
            {
                var temp = _db.Zd_ZdFn.FirstOrDefault(a => a.id == update.id);
                if (temp != null)
                {
                    temp.id = update.id;
                    temp.f_jb = update.f_jb;
                    temp.FangAnName = update.FangAnName;
                    temp.StartLen = update.StartLen;
                    temp.StartFee = update.StartFee;
                    temp.AddLen = update.AddLen;
                    temp.AddFee = update.AddFee;
                    temp.MinLen = update.MinLen;
                    temp.MinFee = update.MinFee;
                    temp.MaxLen = update.MaxLen;
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
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Delete(Int32 autoid)
        {
            try
            {
                var temp = _db.Zd_ZdFn.FirstOrDefault(a => a.id == autoid);
                if (temp != null)
                {
                    _db.Zd_ZdFn.DeleteObject(temp);
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


        /// <summary>
        /// get one  Member's info from RowID  
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static ZDViewMedel gUserInfo(Int32 rowID)
        {
            //Module for View
            ZDViewMedel Mb = new ZDViewMedel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.Zd_ZdFn where a.id == rowID select a);

                foreach (var r in result)
                {
                    ZDViewMedel temp = new ZDViewMedel();
                    temp.id = r.id;
                    temp.f_jb = r.f_jb;
                    temp.FangAnName = r.FangAnName;
                    temp.StartLen = r.StartLen;
                    temp.StartFee = r.StartFee;
                    temp.AddLen = r.AddLen;
                    temp.AddFee = r.AddFee;
                    temp.MinLen = r.MinLen;
                    temp.MinFee = r.MinFee;
                    temp.MaxLen = r.MaxLen;

                    Mb = temp;
                }
            }
            return Mb;
        }

        public static int getLastAutoID()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = _db.Zd_ZdFn.FirstOrDefault(a => a.id == _db.Zd_ZdFn.Max(y => y.id));

                return Convert.ToInt32(result.id) + 1;


            }
        }


        /// <summary>
        /// 通过级别获得钟点房方案
        /// </summary>
        /// <param name="FH"></param>
        /// <returns></returns>
        public static List<ZDViewMedel> ReadZDByJB(string FH)
        {
            List<ZDViewMedel> kf = new List<ZDViewMedel>();

            using (HotelDBEntities db = new HotelDBEntities())
            {
                var result = (from a in db.Zd_ZdFn
                              where a.f_jb == FH
                              select a).ToList();
                foreach (var r in result)
                {
                    ZDViewMedel temp = new ZDViewMedel();
                    temp.id = r.id;
                    temp.f_jb = r.f_jb;
                    temp.FangAnName = r.FangAnName;
                    temp.StartLen = r.StartLen;
                    temp.StartFee = r.StartFee;
                    temp.AddLen = r.AddLen;
                    temp.AddFee = r.AddFee;
                    temp.MinLen = r.MinLen;
                    temp.MinFee = r.MinFee;
                    temp.MaxLen = r.MaxLen;
                    kf.Add(temp);
                }
            }
            return kf;
        }

        public static string GetZDByLX(string FH)
        {
            //-1表示没有该房间
            //0表示空房//1表示已入住
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var kf = (from a in db.Zd_ZdFn
                          where a.f_jb == FH
                          select a).ToList().FirstOrDefault();
                if (kf != null)
                {
                    return "0";
                }
            }
            return "-1";
        }

        /// <summary>
        /// Read all the Member info 
        /// </summary>
        /// <returns></returns>
        public static List<ZDViewMedel> ReadCardDesignAll()
        {
            //Module for View
            List<ZDViewMedel> Mb = new List<ZDViewMedel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.Zd_ZdFn
                             select a;
                foreach (var r in result)
                {
                    ZDViewMedel temp = new ZDViewMedel();
                    temp.id= r.id;
                    temp.f_jb = r.f_jb;
                    temp.FangAnName = r.FangAnName;
                    temp.StartLen = r.StartLen;
                    temp.StartFee = r.StartFee;
                    temp.AddLen = r.AddLen;
                    temp.AddFee = r.AddFee;
                    temp.MinLen = r.MinLen;
                    temp.MinFee = r.MinFee;
                    temp.MaxLen = r.MaxLen;
                    Mb.Add(temp);
                }

            }
            return Mb;
        }


        //public static List<ZDViewMedel> ReadPartMember(ZDViewMedel filter)
        //{

        //    List<ZDViewMedel> Mb = new List<ZDViewMedel>();
        //    using (HotelDBEntities _db = new HotelDBEntities())
        //    {

        //        var result = (from a in _db.Zd_ZdFn
        //                      select a);
        //        if (!string.IsNullOrEmpty(filter.f_jb))
        //        {
        //            result = from a in result
        //                     where a.f_jb.Contains(filter.f_jb)
        //                     select a;
        //        }


        //        foreach (var r in result)
        //        {
        //            ZDViewMedel temp = new ZDViewMedel();
        //            temp.id = r.id;
        //            temp.f_jb = r.f_jb;
        //            temp.FangAnName = r.FangAnName;
        //            temp.StartLen = r.StartLen;
        //            temp.StartFee = r.StartFee;
        //            temp.AddLen = r.AddLen;
        //            temp.AddFee = r.AddFee;
        //            temp.MinLen = r.MinLen;
        //            temp.MinFee = r.MinFee;
        //            temp.MaxLen = r.MaxLen;

        //            Mb.Add(temp);
        //        }

        //    }
        //    return Mb;
        //}
    }

    public class ZDViewMedel
    {

        public Int32 id { get; set; }
        public string f_jb { get; set; }
        public string FangAnName { get; set; }
        public Int32? StartLen { get; set; }
        public decimal? StartFee { get; set; }
        public Int32? AddLen { get; set; }
        public decimal? AddFee { get; set; }
        public Int32? MinLen { get; set; }
        public decimal? MinFee { get; set; }
        public Int32? MaxLen { get; set; }
    }
}
