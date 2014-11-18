using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Runtime.Serialization;
using HotelLogic.Cash.CashAction;
using System.Collections.Specialized;

namespace HotelLogic.FrontDesk
{
    /// <summary>
    /// 物品租借
    /// </summary>
    public static class wpHelper
    {
        /// <summary>
        /// Context
        /// </summary>
        private static readonly HotelDBEntities _db = new HotelDBEntities();

        /// <summary>
        /// Create wp
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Create(wpViewModel FH)
        {
            try
            {
                zd_wp ctgy = new zd_wp();
                ctgy.name = FH.name;
                ctgy.count = FH.count;
                ctgy.countleave = FH.countleave;
                _db.zd_wp.AddObject(ctgy);
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        /// <summary>
        /// read wp
        /// </summary>
        /// <returns></returns>
        public static List<wpViewModel> ReadAll()
        {
            //Module for View
            List<wpViewModel> kf = new List<wpViewModel>();
            var result = from a in _db.zd_wp
                         select a;
            foreach (var r in result)
            {
                wpViewModel temp = new wpViewModel();
                temp.id = r.id;
                temp.name = r.name;
                temp.count = r.count;
                temp.countleave = r.countleave;
                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// Update wp
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(wpViewModel update)
        {
            try
            {
                var temp = _db.zd_wp.FirstOrDefault(a => a.id == update.id);
                if (temp != null)
                {
                    temp.id = update.id;
                    temp.name = update.name;
                    temp.count = update.count;
                    temp.countleave = update.count;
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
        /// Delete wp
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Delete(Int32 autoid)
        {
            try
            {
                var temp = _db.zd_wp.FirstOrDefault(a => a.id == autoid);
                if (temp != null)
                {
                    _db.zd_wp.DeleteObject(temp);
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
        /// Create wp_borrow
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Create_wp_borrow(wp_borrowViewModel FH)
        {
            try
            {
                zd_wp_borrow ctgy = new zd_wp_borrow();
                ctgy.id = FH.id_wp_borrow;
                ctgy.OrderGuid = FH.orderguid;
                ctgy.wpname = FH.wpname.Trim();
                ctgy.num = FH.num;
                ctgy.yajin = FH.yajin;
                ctgy.zjtime = FH.zjtime;
                ctgy.djnumber = FH.djnumber;
                ctgy.beizhu = FH.beizhu;
                ctgy.caozuoyuan = FH.caozuoyuan;
                ctgy.state = 0; //FH.state;
                ctgy.fanhao = FH.fanhao;
                var t = _db.zd_wp.FirstOrDefault(a => a.name.Trim()==FH.wpname.Trim());
                if (t != null)
                {
                    t.name = FH.wpname.Trim();
                    t.countleave = t.countleave - FH.num;
                    _db.SaveChanges();
                }
                _db.zd_wp_borrow.AddObject(ctgy);
                _db.SaveChanges();

            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        /// <summary>
        /// read wp_borrow
        /// </summary>
        /// <returns></returns>
        public static List<wp_borrowViewModel> ReadAll_wp_borrow(string orderguid)
        {
            //Module for View
            List<wp_borrowViewModel> kf = new List<wp_borrowViewModel>();
            if (!string.IsNullOrEmpty(orderguid))
            {
                Guid orderGuid = Guid.Parse(orderguid);
                var result = from a in _db.zd_wp_borrow
                             where a.OrderGuid ==orderGuid
                             select a;
                foreach (var r in result)
                {
                    wp_borrowViewModel temp = new wp_borrowViewModel();
                    temp.id_wp_borrow = r.id;
                    temp.wpname = r.wpname;
                    temp.orderguid = r.OrderGuid;
                    temp.num = r.num;
                    temp.yajin = r.yajin;
                    temp.zjtime = r.zjtime;
                    temp.djnumber = r.djnumber;
                    temp.beizhu = r.beizhu;
                    temp.caozuoyuan = r.caozuoyuan;
                    temp.fanhao = r.fanhao;
                    if (r.state == 0)
                    {
                        temp.state = "未还";
                    }
                    else
                    {
                        temp.state = "已还";
                    }
                    kf.Add(temp);
                }
            }
            return kf;
        }
        /// <summary>
        /// Update wp_borrow
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update_wp_borrow(wp_borrowViewModel r)
        {
            try
            {
                var temp = _db.zd_wp_borrow.FirstOrDefault(a => a.id == r.id_wp_borrow);
                if (temp != null)
                {
                    temp.id = r.id_wp_borrow;
                    temp.state = 1;
                    var t = _db.zd_wp.FirstOrDefault(a => a.name== r.wpname);
                    if (t != null)
                    {
                        t.name = r.wpname;
                        t.countleave = t.countleave + r.num;
                        _db.SaveChanges();
                    }
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
        /// Delete wp_borrow
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Delete_wp_borrow(wp_borrowViewModel r)
        {
            try
            {
                var temp = _db.zd_wp_borrow.FirstOrDefault(a => a.id == r.id_wp_borrow);
                if (temp != null)
                {
                    var t = _db.zd_wp.FirstOrDefault(a => a.name == r.wpname);
                    if (t != null)
                    {
                        t.name = r.wpname;
                        t.countleave = t.countleave + r.num;
                        _db.SaveChanges();
                    }
                    _db.zd_wp_borrow.DeleteObject(temp);
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
        /// Update wp_borrow
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static bool IsExists_wp_borrow(Guid orderguid)
        {
            try
            {
                var temp = _db.zd_wp_borrow.Count(a => a.OrderGuid==orderguid && a.state==0);
                if (temp != null)
                {
                    if (temp > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                return  false;
            }
        }
    }


    public class wpViewModel
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public Int32? count { get; set; }
        public Int32? countleave { get; set; }
   
    }

    public class wp_borrowViewModel
    {
        public Int32 id_wp_borrow { get; set; }
        public Guid? orderguid { get; set; }
        public string wpname { get; set; }
        public Int32? num { get; set; }
        public Int32? yajin { get; set; }
        public DateTime? zjtime { get; set; }
        public string djnumber { get; set; }
        public string beizhu { get; set; }
        public string caozuoyuan { get; set; }
        public string  state { get; set; }
        public string fanhao { get; set; }
    }
}
