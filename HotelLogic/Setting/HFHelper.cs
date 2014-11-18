using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 换房原因
    /// </summary>
    public static class HFHelper
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
        public static string  Create(HFViewModel FH)
        {
            try
            {
                
                H_HuanFangYY ctgy = new H_HuanFangYY();
                ctgy.Content = FH.Content;
              
                _db.H_HuanFangYY.AddObject(ctgy);
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
        public static List<HFViewModel> ReadAll()
        {
            //Module for View
            List<HFViewModel> kf = new List<HFViewModel>();
            var result = from a in _db.H_HuanFangYY
                         select a;
            foreach (var r in result)
            {
                HFViewModel temp = new HFViewModel();
                temp.AutoID = r.AutoID;
                temp.Content = r.Content;
             
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
        public static List<HFViewModel> ReadPart(int page, int rows)
        {
            //Module for View
            List<HFViewModel> kf = new List<HFViewModel>();
            var result = (from a in _db.H_HuanFangYY
                          orderby a.AutoID descending
                          select a).Skip((page - 1) * rows).Take(rows);
            foreach (var r in result)
            {
                HFViewModel temp = new HFViewModel();
                temp.AutoID = r.AutoID;
                temp.Content = r.Content;
             
                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(HFViewModel update)
        {
            try
            {
                var temp = _db.H_HuanFangYY.FirstOrDefault(a => a.AutoID == update.AutoID);
                if (temp != null)
                {
                    temp.Content = update.Content;
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
                var temp = _db.H_HuanFangYY.FirstOrDefault(a => a.AutoID == autoid);
                if (temp != null)
                {
                    _db.H_HuanFangYY.DeleteObject(temp);
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
        /// get one  LY's info from RowID  
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static HFViewModel gUserInfo(Int32 rowID)
        {
            //Module for View
            HFViewModel Mb = new HFViewModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.H_HuanFangYY where a.AutoID == rowID select a);
               
                foreach (var r in result)
                {
                    HFViewModel temp = new HFViewModel();
                    temp.AutoID = r.AutoID;
                    temp.Content = r.Content;
      
                    Mb = temp;
                }
            }
            return Mb;
        }

        public static int getLastAutoID()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = _db.H_HuanFangYY.FirstOrDefault(a => a.AutoID == _db.H_HuanFangYY.Max(y => y.AutoID));

                return Convert.ToInt32(result.AutoID) + 1;


            }
        }


        /// <summary>
        /// Read all info 
        /// </summary>
        /// <returns></returns>
        public static List<HFViewModel> ReadCardDesignAll()
        {
            //Module for View
            List<HFViewModel> Mb = new List<HFViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.H_HuanFangYY
                             select a;
                foreach (var r in result)
                {
                    HFViewModel temp = new HFViewModel();
                    temp.AutoID = r.AutoID;
                    temp.Content = r.Content;

                    Mb.Add(temp);
                }

            }
            return Mb;
        }
    }


    public class HFViewModel
    {
        public Int32 AutoID { get; set; }
        public string Content { get; set; }
    }
}
