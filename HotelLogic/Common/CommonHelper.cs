using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Common
{
    public static class CommonHelper
    {      
        /// <summary>
        /// Context
        /// </summary>
        //private static readonly HotelDBEntities _db = new HotelDBEntities();

#region 付款方式
        //Read
        public static List<zd_fsviewmodel> getFKFS()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_fsviewmodel> result = new List<zd_fsviewmodel>();
            var fk = from a in _db.zd_fs
                     select a;

            foreach (var f in fk)
            {
                zd_fsviewmodel temp = new zd_fsviewmodel();
                temp.ID = f.id;
                temp.fkfs = f.fkfs;
                result.Add(temp);
            }
            return result;
        }
        //Create
        public static string createFKFS(zd_fsviewmodel fs)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                zd_fs temp = new zd_fs();
                temp.fkfs = fs.fkfs;
                _db.zd_fs.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateFKFS(zd_fsviewmodel fs)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_fs.FirstOrDefault(a => a.id == fs.ID);
                if (temp != null)
                {
                    temp.id = fs.ID;
                    temp.fkfs = fs.fkfs;
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
        //Delete
        public static string deleteFKFS(Int32 autoid)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_fs.FirstOrDefault(a => a.id == autoid);
                if (temp != null)
                {
                    _db.zd_fs.DeleteObject(temp);
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
#endregion

#region 国籍
		
        //Read
        public static List<zd_gjviewmodel> getGJ()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_gjviewmodel> result = new List<zd_gjviewmodel>();
            var fk = from a in _db.zd_gj
                     select a;
            foreach (var f in fk)
            {
                zd_gjviewmodel temp = new zd_gjviewmodel();
                temp.ID = f.AutoID;
                temp.gj = f.gj;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createGJ(zd_gjviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                zd_gj temp = new zd_gj();
                temp.AutoID = gj.ID;
                temp.gj = gj.gj;
                _db.zd_gj.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateGJ(zd_gjviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_gj.FirstOrDefault(a => a.AutoID == gj.ID);
                if (temp != null)
                {
                    temp.AutoID = gj.ID;
                    temp.gj = gj.gj;
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
        //Delete
        public static string deleteGJ(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_gj.FirstOrDefault(a => a.AutoID == ID);
                if (temp != null)
                {
                    _db.zd_gj.DeleteObject(temp);
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
#endregion
        #region 操作员

        //Read
        public static List<zd_caozuoyuanviewmodel> getCaozuoyuan()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_caozuoyuanviewmodel> result = new List<zd_caozuoyuanviewmodel>();
            var fk = (from a in _db.LoginUser
                      select a).Distinct();

            foreach (var f in fk)
            {
                zd_caozuoyuanviewmodel temp = new zd_caozuoyuanviewmodel();
                temp.ID = f.id;
                temp.caozuoyuan = f.LoginName;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createCaozuoyuan(zd_caozuoyuanviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                LoginUser temp = new LoginUser();
                temp.id = gj.ID;
                temp.LoginName = gj.caozuoyuan;
                _db.LoginUser.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateCaozuoyuan(zd_caozuoyuanviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.LoginUser.FirstOrDefault(a => a.id == gj.ID);
                if (temp != null)
                {
                    temp.id = gj.ID;
                    temp.LoginName = gj.caozuoyuan;
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
        //Delete
        public static string deleteCaozuoyuan(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.LoginUser.FirstOrDefault(a => a.id == ID);
                if (temp != null)
                {
                   _db.LoginUser.DeleteObject(temp);
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

        #endregion

        #region 房间级别
        //Read
        public static List<zd_fJBviewmodel> getfjJB()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_fJBviewmodel> result = new List<zd_fJBviewmodel>();
            var kl = from a in _db.zd_jb
                     select a;
            foreach (var f in kl)
            {
                zd_fJBviewmodel temp = new zd_fJBviewmodel();
                temp.ID = f.AutoID;
                temp.fjJB = f.kfjb;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createfjJB(zd_fJBviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                zd_jb temp = new zd_jb();
                temp.AutoID = kl.ID;
                temp.kfjb = kl.fjJB;
                _db.zd_jb.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updatefjJB(zd_fJBviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_jb.FirstOrDefault(a => a.AutoID == kl.ID);
                if (temp != null)
                {
                    temp.AutoID = kl.ID;
                    temp.kfjb = kl.fjJB;
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
        //Delete
        public static string deletefjJB(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_jb.FirstOrDefault(a => a.AutoID == ID);
                if (temp != null)
                {
                    _db.zd_jb.DeleteObject(temp);
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
        #endregion

        #region 客人状态
        //Read
        public static List<zd_KrZtviewmodel> getKrZt()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_KrZtviewmodel> result = new List<zd_KrZtviewmodel>();
            var kl = from a in _db.Zd_KrZt
                     select a;
            foreach (var f in kl)
            {
                zd_KrZtviewmodel temp = new zd_KrZtviewmodel();
                temp.ID = f.id;
                temp.KrZt = f.ZtMc;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createKrZt(zd_KrZtviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                Zd_KrZt temp = new Zd_KrZt();
                temp.id = kl.ID;
                temp.ZtMc = kl.KrZt;
                _db.Zd_KrZt.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateKrZt(zd_KrZtviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.Zd_KrZt.FirstOrDefault(a => a.id == kl.ID);
                if (temp != null)
                {
                    temp.id = kl.ID;
                    temp.ZtMc = kl.KrZt;
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
        //Delete
        public static string deleteKrZt(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.Zd_KrZt.FirstOrDefault(a => a.id == ID);
                if (temp != null)
                {
                    _db.Zd_KrZt.DeleteObject(temp);
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
        #endregion

        #region 客户类型

        //Read
        public static List<zd_klviewmodel> getKHLB()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_klviewmodel> result = new List<zd_klviewmodel>();
            var kl = from a in _db.zd_kl
                     select a;
            foreach (var f in kl)
            {
                zd_klviewmodel temp = new zd_klviewmodel();
                temp.ID = f.AutoID;
                temp.KHLB = f.khlb;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createKHLB(zd_klviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                zd_kl temp = new zd_kl();
                temp.AutoID = kl.ID;
                temp.khlb = kl.KHLB;
                _db.zd_kl.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateKHLB(zd_klviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_kl.FirstOrDefault(a => a.AutoID == kl.ID);
                if (temp != null)
                {
                    temp.AutoID = kl.ID;
                    temp.khlb = kl.KHLB;
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
        //Delete
        public static string deleteKHLB(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_kl.FirstOrDefault(a => a.AutoID == ID);
                if (temp != null)
                {
                    _db.zd_kl.DeleteObject(temp);
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
        #endregion

        #region 证件类型ZJLX

        //Read
        public static List<zd_zjlxviewmodel> getZJLX()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_zjlxviewmodel> result = new List<zd_zjlxviewmodel>();
            var kl = from a in _db.zd_ZjLx
                     select a;
            foreach (var f in kl)
            {
                zd_zjlxviewmodel temp = new zd_zjlxviewmodel();
                temp.ID = f.AutoID;
                temp.ZJLX = f.zjlx;
                result.Add(temp);
            }
            return result;
        }

        //Create
        public static string createZJLX(zd_zjlxviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                zd_ZjLx temp = new zd_ZjLx();
                temp.zjlx = kl.ZJLX;
                _db.zd_ZjLx.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateZJLX(zd_zjlxviewmodel kl)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_ZjLx.FirstOrDefault(a => a.AutoID == kl.ID);
                if (temp != null)
                {
                    temp.zjlx = kl.ZJLX;
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
        //Delete
        public static string deleteZJLX(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_ZjLx.FirstOrDefault(a => a.AutoID == ID);
                if (temp != null)
                {
                    _db.zd_ZjLx.DeleteObject(temp);
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
        #endregion

        #region 特权人

        //Read
        public static List<zd_Tequanrenviewmodel> getTQR()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Tequanrenviewmodel> result = new List<zd_Tequanrenviewmodel>();
            var fk = from a in _db.td_tq
                     select a;
            foreach (var f in fk)
            {
                zd_Tequanrenviewmodel temp = new zd_Tequanrenviewmodel();
                temp.ID = f.id;
                temp.TeQuanRen = f.tequanren;
                result.Add(temp);
            }
            return result;
        }
        public static string getTQRZhekou(string tqr)
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Tequanrenviewmodel> result = new List<zd_Tequanrenviewmodel>();
            var fk = _db.td_tq.FirstOrDefault(a => a.tequanren ==tqr );                      
            if (fk != null)
            {
                return (100-fk.fjzhekou).ToString();
            }
            return "";
        }
        //Create
        public static string createTQR(zd_Tequanrenviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                td_tq temp = new td_tq();
                temp.id = gj.ID;
                temp.tequanren = gj.TeQuanRen;
                _db.td_tq.AddObject(temp);
                _db.SaveChanges();
                return "0";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateTQR(zd_Tequanrenviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.td_tq.FirstOrDefault(a => a.id == gj.ID);
                if (temp != null)
                {
                    temp.id = gj.ID;
                    temp.tequanren = gj.TeQuanRen;
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
        //Delete
        public static string deleteTQR(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.td_tq.FirstOrDefault(a => a.id == ID);
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
        #endregion

        #region 楼栋
        //Read
        public static List<zd_Ldviewmodel> getLd(string type)
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Ldviewmodel> result = new List<zd_Ldviewmodel>();
            if (type != "notall")
            {
                zd_Ldviewmodel all = new zd_Ldviewmodel();
                all.ID = 0;
                all.Ld = "全部";
                result.Add(all);
            }
            var fk = from a in _db.zd_ld
                     select a;
            foreach (var f in fk)
            {
                zd_Ldviewmodel temp = new zd_Ldviewmodel();
                temp.ID = f.id;
                temp.Ld = f.ldName;
                result.Add(temp);
            }
            return result;
        }
        //Create
        public static string createLd(zd_Ldviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var t = _db.zd_ld.FirstOrDefault(a => a.ldName == gj.Ld);
                if (t != null)
                {
                    return "1";
                }
                else
                {
                    zd_ld temp = new zd_ld();
                    temp.id = gj.ID;
                    temp.ldName = gj.Ld;
                    _db.zd_ld.AddObject(temp);
                    _db.SaveChanges();
                    return "0";
                }


            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateLd(zd_Ldviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_ld.FirstOrDefault(a => a.id == gj.ID);
                if (temp != null)
                {
                    var t = _db.zd_ld.FirstOrDefault(a => a.ldName == gj.Ld && a.id != gj.ID);
                    if (t != null)
                    {
                        return "1";
                    }
                    else
                    {
                        temp.id = gj.ID;
                        temp.ldName = gj.Ld;
                        _db.SaveChanges();
                        return "0";
                    }
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Delete
        public static string deleteLd(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_ld.FirstOrDefault(a => a.id == ID);
                if (temp != null)
                {
                    _db.zd_ld.DeleteObject(temp);
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
        #endregion

        #region 楼层
        //Read
        public static List<zd_Lcviewmodel> getLc(int id,string type)
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Lcviewmodel> result = new List<zd_Lcviewmodel>();
            if (type != "notall")
            {
                zd_Lcviewmodel all = new zd_Lcviewmodel();
                all.ID = 0;
                all.Lc = "全部";
                result.Add(all);
            }
            var fk = from a in _db.zd_lc
                     join b in _db.zd_ld on a.ldId equals b.id

                     where b.id == id&&a.ldId == id
                     select a;
            foreach (var f in fk)
            {
                zd_Lcviewmodel temp = new zd_Lcviewmodel();
                temp.ID = f.id;
                temp.Lc = f.lcName;
                temp.LdID = f.ldId;
                result.Add(temp);
            }
            return result;
        }
        public static List<zd_Lcviewmodel> getPartLc()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Lcviewmodel> result = new List<zd_Lcviewmodel>();
      

            var d=(from a in _db.zd_lc 
                   group a by a.ldId into g 
                   orderby g.Count() descending 
                   select g
            ).ToList().FirstOrDefault();
            if (d != null)
            {
                foreach (var f in d)
                {
                    zd_Lcviewmodel temp = new zd_Lcviewmodel();
                    temp.ID = f.id;
                    temp.Lc = f.lcName;
                    temp.LdID = f.ldId;
                    result.Add(temp);
                }
            }
            return result;
        }

        //Create
        public static string createLc(zd_Lcviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var t = _db.zd_lc.FirstOrDefault(a => a.lcName == gj.Lc && a.ldId == gj.LdID);
                   if (t != null)
                   {
                       return "1";
                   }
                   else
                   {
                       zd_lc temp = new zd_lc();
                       temp.id = gj.ID;
                       temp.lcName = gj.Lc;
                       temp.ldId = gj.LdID;
                       _db.zd_lc.AddObject(temp);
                       _db.SaveChanges();
                       return "0";
                   }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateLc(zd_Lcviewmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var t = _db.zd_lc.FirstOrDefault(a => a.lcName == gj.Lc && a.id != gj.ID && a.ldId == gj.LdID);
                if (t != null)
                {
                    return "1";
                }
                else
                {
                    var temp = _db.zd_lc.FirstOrDefault(a => a.id == gj.ID);
                    if (temp != null)
                    {
                        temp.id = gj.ID;
                        temp.lcName = gj.Lc;
                        temp.ldId = gj.LdID;

                        _db.SaveChanges();
                        return "0";
                    }
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Delete
        public static string deleteLc(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.zd_lc.FirstOrDefault(a => a.id == ID);
                if (temp != null)
                {
                    _db.zd_lc.DeleteObject(temp);
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
        #endregion
        #region 付现方式
        //Read
        public static List<zd_Hxfswmodel> getHxfs(int id, string type)
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Hxfswmodel> result = new List<zd_Hxfswmodel>();
            if (type != "notall")
            {
                zd_Hxfswmodel all = new zd_Hxfswmodel();
                all.ID = 0;
                all.hxfsName = "全部";
                result.Add(all);
            }
            var fk = from a in _db.Zd_Hxfs                     
                     where a.id == id
                     select a;
            foreach (var f in fk)
            {
                zd_Hxfswmodel temp = new zd_Hxfswmodel();
                temp.ID = f.id;
                temp.hxfsName = f.hxfsName;
                result.Add(temp);
            }
            return result;
        }
        public static List<zd_Hxfswmodel> getPartHxfs()
        {
            HotelDBEntities _db = new HotelDBEntities();
            List<zd_Hxfswmodel> result = new List<zd_Hxfswmodel>();
            var d = (from a in _db.Zd_Hxfs
                     orderby a.id 
                     select a
            ).ToList();
            if (d != null)
            {
                foreach (var f in d)
                {
                    zd_Hxfswmodel temp = new zd_Hxfswmodel();
                    temp.ID = f.id;
                    temp.hxfsName = f.hxfsName;
                    result.Add(temp);
                }
            }
            return result;
        }

        //Create
        public static string createHxfs(zd_Hxfswmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var t = _db.Zd_Hxfs.FirstOrDefault(a => a.hxfsName == gj.hxfsName);
                if (t != null)
                {
                    return "1";
                }
                else
                {
                    Zd_Hxfs temp = new Zd_Hxfs();
                    temp.id = gj.ID;
                    temp.hxfsName = gj.hxfsName;
                    _db.Zd_Hxfs.AddObject(temp);
                    _db.SaveChanges();
                    return "0";
                }
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Update
        public static string updateHxfs(zd_Hxfswmodel gj)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var t = _db.Zd_Hxfs.FirstOrDefault(a => a.hxfsName == gj.hxfsName && a.id != gj.ID );
                if (t != null)
                {
                    return "1";
                }
                else
                {
                    var temp = _db.Zd_Hxfs.FirstOrDefault(a => a.id == gj.ID);
                    if (temp != null)
                    {
                        temp.id = gj.ID;
                        temp.hxfsName = gj.hxfsName;

                        _db.SaveChanges();
                        return "0";
                    }
                }
                return "1";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        //Delete
        public static string deleteHxfs(Int32 ID)
        {
            HotelDBEntities _db = new HotelDBEntities();
            try
            {
                var temp = _db.Zd_Hxfs.FirstOrDefault(a => a.id == ID);
                if (temp != null)
                {
                    _db.Zd_Hxfs.DeleteObject(temp);
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
        #endregion
    }

    public class zd_fsviewmodel
    {
        public Int32 ID;
        public string fkfs;
    }
    public class zd_gjviewmodel
    {
        public Int32 ID;
        public string gj;
    }
    public class zd_klviewmodel
    {
        public Int32 ID;
        public string KHLB;
    }
    public class zd_zjlxviewmodel
    {
        public Int32 ID;
        public string ZJLX;
    }

    public class zd_caozuoyuanviewmodel
    {
        public Int32 ID;
        public string caozuoyuan;
    }

    public class zd_fJBviewmodel
    {
        public Int32 ID;
        public string fjJB;
    }
    public class zd_KrZtviewmodel
    {
        public Int32 ID;
        public string KrZt;
    }
    public class zd_Tequanrenviewmodel
    {
        public Int32 ID;
        public string TeQuanRen;
    }

    public class zd_Lcviewmodel
    {
        public Int32 ID;
        public string Lc;
        public Int32? LdID;
    }
    public class zd_Ldviewmodel
    {
        public Int32 ID;
        public string Ld;
    }
    public class zd_Hxfswmodel
    {
        public Int32 ID;
        public string hxfsName;
    }
}
