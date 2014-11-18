using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;


namespace HotelLogic.Setting
{
    public class KFHelper
    {
        private static KFHelper _instance;
        public static KFHelper Instance
        {
            get { return _instance == null ? _instance = new KFHelper() : _instance; }
        }
        private KFHelper() { }

        /// <summary>
        /// 客房信息的CRUD
        /// </summary>
        private static readonly HotelDBEntities _db = new HotelDBEntities(); 
        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="kf"></param>
        /// <returns></returns>
        public static string Create(KFViewModule _kf)
        {
            //临时处理，因为前台只传过来级别id，级别名称需要从字典表读取后付给房间信息
            int id=Convert.ToInt32(_kf.JBCode);
            var jbname = (from a in _db.zd_jb
                          where a.AutoID == id
                          select a).SingleOrDefault();
            if (jbname != null)
                _kf.JBName = jbname.kfjb;
            try
            {
                Zd_Fj fj = new Zd_Fj();
                fj.f_fh = _kf.FH;
                fj.f_zt = _kf.StatusCode;
                fj.f_ztmc = _kf.StatusName;
                fj.f_dm = _kf.JBCode;
                fj.f_jb = _kf.JBName;
                fj.f_dj = _kf.DJ;
                fj.HalfDj = _kf.BJ;
                fj.Memo = _kf.Detail;
                fj.f_cw = _kf.CW;
                fj.JbID = _kf.JBID;
                fj.lcID = _kf.LcID;
                fj.ldID = _kf.LdID;
                _db.Zd_Fj.AddObject(fj);
                _db.SaveChanges();
                
            }
            catch(Exception e)
            {
                return e.ToString();
            }
            return "0";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<KFViewModule> ReadAll()
        {
            //Module for View
            List<KFViewModule> kf = new List<KFViewModule>();
            var result = from a in _db.Zd_Fj
                         select a;
            foreach (var r in result)
            {
                KFViewModule temp = new KFViewModule();
                temp.ID = r.AutoID;
                temp.FH = r.f_fh;
                temp.StatusCode = r.f_zt;
                temp.StatusName = r.f_ztmc;
                temp.JBCode = r.f_dm;
                temp.JBName = r.f_jb;
                temp.DJ = r.f_dj;
                temp.BJ = r.HalfDj;
                temp.Detail = r.Memo;
                temp.CW = r.f_cw;
                temp.JBID = r.JbID;

                //temp.LdID = r.ldID;
                //temp.LcID = r.lcID;
                var lc = (from o in _db.zd_lc
                          where o.id == r.lcID
                          select o).ToList().FirstOrDefault();
                if (lc != null)
                {
                    temp.Lc = lc.lcName;
                }
                var ld = (from o in _db.zd_ld
                          where o.id == r.ldID
                          select o).ToList().FirstOrDefault();
                if (ld != null)
                {
                    temp.Ld = ld.ldName;
                }
                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// 读取空房信息
        /// </summary>
        /// <returns></returns>
        public static List<KFViewModule> ReadEmpty()
        {            
            //Module for View
            List<KFViewModule> kf = new List<KFViewModule>();
            var result = from a in _db.Zd_Fj
                         where a.f_ztmc=="空房"
                         select a;
            foreach (var r in result)
            {
                KFViewModule temp = new KFViewModule();
                temp.ID = r.AutoID;
                temp.FH = r.f_fh;
                temp.StatusCode = r.f_zt;
                temp.StatusName = r.f_ztmc;
                temp.JBCode = r.f_dm;
                temp.JBName = r.f_jb;
                temp.DJ = r.f_dj;
                temp.BJ = r.HalfDj;
                temp.Detail = r.Memo;
                temp.CW = r.f_cw;
                temp.JBID = r.JbID;
                var lc = (from o in _db.zd_lc
                          where o.id == r.lcID
                          select o).ToList().FirstOrDefault();
                if (lc != null)
                {
                    temp.Lc = lc.lcName;
                }
                var ld = (from o in _db.zd_ld
                          where o.id == r.ldID
                          select o).ToList().FirstOrDefault();
                if (ld != null)
                {
                    temp.Ld = ld.ldName;
                }
                kf.Add(temp);
            }
            return kf;

        }
        public static List<KFViewModule> ReadByKFCgy(string cgyid)
        {
            //Module for View
            List<KFViewModule> kf = new List<KFViewModule>();
            var result = from a in _db.Zd_Fj
                         where a.f_dm == cgyid
                         select a;
            foreach (var r in result)
            {
                KFViewModule temp = new KFViewModule();
                temp.ID = r.AutoID;
                temp.FH = r.f_fh;
                temp.StatusCode = r.f_zt;
                temp.StatusName = r.f_ztmc;
                temp.JBCode = r.f_dm;
                temp.JBName = r.f_jb;
                temp.DJ = r.f_dj;
                temp.BJ = r.HalfDj;
                temp.Detail = r.Memo;
                temp.CW = r.f_cw;
                temp.JBID = r.JbID;
                var lc = (from o in _db.zd_lc
                          where o.id == r.lcID
                          select o).ToList().FirstOrDefault();
                if (lc != null)
                {
                    temp.Lc = lc.lcName;
                }
                var ld = (from o in _db.zd_ld
                          where o.id == r.ldID
                          select o).ToList().FirstOrDefault();
                if (ld != null)
                {
                    temp.Ld = ld.ldName;
                }
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
        public static List<KFViewModule> ReadPart(int page, int rows)
        {
            //Module for View
            List<KFViewModule> kf = new List<KFViewModule>();
            var result = (from a in _db.Zd_Fj
                          orderby a.f_fh descending
                          select a).Skip((page - 1) * rows).Take(rows);
            foreach (var r in result)
            {
                KFViewModule temp = new KFViewModule();
                temp.ID = r.AutoID;
                temp.FH = r.f_fh;
                temp.StatusCode = r.f_zt;
                temp.StatusName = r.f_ztmc;
                temp.JBCode = r.f_dm;
                temp.JBName = r.f_jb;
                temp.DJ = r.f_dj;
                temp.BJ = r.HalfDj;
                temp.Detail = r.Memo;
                temp.CW = r.f_cw;
                temp.JBID = r.JbID;
                var lc = (from o in _db.zd_lc
                          where o.id == r.lcID
                          select o).ToList().FirstOrDefault();
                if (lc != null)
                {
                    temp.Lc = lc.lcName;
                }
                var ld = (from o in _db.zd_ld
                          where o.id == r.ldID
                          select o).ToList().FirstOrDefault();
                if (ld != null)
                {
                    temp.Ld = ld.ldName;
                }
                kf.Add(temp);
            }
            return kf;
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="ctgy"></param>
        /// <returns></returns>
        public static string Update(KFViewModule update)
        {
            try
            {
                var temp = _db.Zd_Fj.FirstOrDefault(a => a.AutoID == update.ID);
                if (temp != null)
                {
                    temp.f_zt = update.StatusCode;
                    temp.f_ztmc = update.StatusName;
                    temp.f_dm = update.JBCode;
                    temp.f_jb = update.JBName;
                    temp.f_dj = update.DJ;
                    temp.HalfDj = update.BJ;
                    temp.Memo = update.Detail;
                    temp.f_cw = update.CW;
                    temp.JbID = update.JBID;
                    temp.lcID = update.LcID;
                    temp.ldID = update.LdID;
                    //temp.LdID = update.ldID;
                    //temp.LcID = update.lcID;
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
                var temp = _db.Zd_Fj.FirstOrDefault(a => a.AutoID == autoid);
                if (temp != null)
                {
                    _db.Zd_Fj.DeleteObject(temp);
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
        /// get JB string From FH
        /// </summary>
        /// <param name="FH"></param>
        /// <returns></returns>
        public static string GetJBFromFH(string FH)
        {
            string JB = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = (from a in db.Zd_Fj
                            where a.f_fh == FH
                            select a).SingleOrDefault();
                if (query!= null)
                {
                    JB = query.f_jb;
                }
            }
            return JB;
        }
        /// <summary>
        /// 通过房号得到客房信息
        /// </summary>
        /// <param name="FH"></param>
        /// <returns></returns>
        public static KFViewModule GetKFByFH(string FH)
        {
            KFViewModule temp = new KFViewModule();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var kf = (from a in db.Zd_Fj
                           where a.f_fh == FH
                           select a).ToList().FirstOrDefault();
                if (kf != null)
                {
                    temp.FH = kf.f_fh;
                    temp.JBName = kf.f_jb;
                    temp.DJ = kf.f_dj;
                }
            }
            return temp;
        }

       
        /// <summary>
        /// 要排除的房间， 被预订的和在客的
        /// yd orderdetail
        /// </summary>
        /// <param name="cgyid"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<KFViewModule> ReadCanYD(string fjtype,string cgyid,DateTime begin,DateTime end,int lcid,int ldid)
        {         

            //Module for View
            List<KFViewModule> kf = new List<KFViewModule>();
            List<Zd_Fj> result = new List<Zd_Fj>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                List<string> fh = new List<string>();
                //先得到时间冲突的订单
                List<Yd_Dd> dd = (from d in db.Yd_Dd
                                  where ((d.dr >= begin
                                  && d.dr<=end)
                                  || (d.lr >= begin
                                  && d.lr <= end)
                                   ||(d.dr<=begin
                                   &&d.lr>=end))
                                  && d.Status == 0
                                  select d).ToList();


                List<Yd_Pf> pf = (from b in _db.Yd_Pf
                                  where b.Status == 0
                                  select b).ToList();

                foreach (var p in pf)
                {
                    foreach (var d in dd)
                    {
                        if (d.YDNum == p.YDNum)
                        {
                            //将冲突的房号暂存
                            fh.Add(p.f_fh);
                        }
                    }
                }

                string[] fh1 = fh.ToArray();
                string[] fh2 = (from x in _db.H_RuzhuDetail
                                where x.Status == 0
                                && ((x.ArriveTime <= begin && begin <= x.LeaveTime)
                                || (x.ArriveTime <= end && end <= x.LeaveTime))
                                select x.FangJianHao).ToArray();
               
                if (cgyid == "0")
                {
                    bool IsFjtypeEmpty = string.IsNullOrEmpty(fjtype);
                    result = (from a in _db.Zd_Fj
                              where !fh1.Contains(a.f_fh)
                                  && !fh2.Contains(a.f_fh) && (IsFjtypeEmpty ||a.f_jb == fjtype)
                              select a).ToList();
                    //根据楼栋查询
                    if (ldid != 0)
                    {
                         result=(from a in result where a.ldID == ldid select a).ToList();
                    }
                     //根据楼栋楼层查询
                    if (lcid != 0)
                    {
                        result = (from a in result where a.lcID == lcid select a).ToList();
                    }
                    //楼栋选择全部时  或者楼栋为空时
                    //if (ldid == 0 && lcid == 0)
                    //{

                    //}
                    //根据楼层查询  楼层  根据楼层的id  获得该楼层的名称   再根据名称 获得所有的楼层名称相同的id   再根据id查询
                    if (ldid == 0 && lcid != 0)
                    {
                        var lcname = (from a in _db.zd_lc where a.id == lcid select a.lcName).ToList().FirstOrDefault();
                        var ldid1 = from a in _db.zd_lc where a.lcName == lcname select a.id;
                        foreach (var j in ldid1)
                        {
                            result.AddRange((from a in result where a.lcID == j select a).ToList());
                        }
                    }
                }
                else
                {
                    result = (from a in _db.Zd_Fj
                              where !fh1.Contains(a.f_fh)
                                  && !fh2.Contains(a.f_fh)
                                 && a.f_dm == cgyid
                              select a).ToList();

                    //根据楼栋查询
                    if (ldid != 0)
                    {
                        result = (from a in result where a.ldID == ldid select a).ToList();
                    }
                    //根据楼栋楼层查询
                    if (lcid != 0)
                    {
                        result = (from a in result where a.lcID == lcid select a).ToList();
                    }
                    //楼栋选择全部时  或者楼栋为空时
                    //if (ldid == 0 && lcid == 0)
                    //{

                    //}
                    //根据楼层查询  楼层  根据楼层的id  获得该楼层的名称   再根据名称 获得所有的楼层名称相同的id   再根据id查询
                    if (ldid == 0 && lcid != 0)
                    {
                        var lcname = (from a in _db.zd_lc where a.id == lcid select a.lcName).ToList().FirstOrDefault();
                        var ldid1 = from a in _db.zd_lc where a.lcName == lcname select a.id;
                        foreach (var j in ldid1)
                        {
                            result.AddRange((from a in result where a.lcID == j select a).ToList());
                        }
                    }

                }
                foreach (var r in result)
                {
                    KFViewModule temp = new KFViewModule();
                    temp.ID = r.AutoID;
                    temp.FH =  r.f_fh;//增加显示内容
                    temp.Location = "楼栋" + r.ldID + "_楼层" + r.lcID + "_房号_" + r.f_fh;//增加显示内容
                    temp.StatusCode = r.f_zt;
                    temp.StatusName = r.f_ztmc;
                    temp.JBCode = r.f_dm;
                    temp.JBName = r.f_jb;
                    temp.DJ = r.f_dj;
                    temp.BJ = r.HalfDj;
                    temp.Detail = r.Memo;
                    temp.CW = r.f_cw;
                    temp.JBID = r.JbID;
                    var lc = (from o in _db.zd_lc
                              where o.id == r.lcID
                              select o).ToList().FirstOrDefault();
                    if (lc != null)
                    {
                        temp.Lc = lc.lcName;
                    }
                    var ld = (from o in _db.zd_ld
                              where o.id == r.ldID
                              select o).ToList().FirstOrDefault();
                    if (ld != null)
                    {
                        temp.Ld = ld.ldName;
                    }
                    kf.Add(temp);
                }
                return kf;
            }
        }
    }

    public class KFViewModule
    {
        public Int32 ID { get; set; }
        //房号
        public string FH { get; set; }
        //状态码
        public string StatusCode { get; set; }
        //状态名称
        public string StatusName { get; set; }
        //级别码
        public string JBCode { get; set; }
        //级别名称
        public string JBName { get; set; }
        //床位
        public Byte? CW { get; set; }
        //单价
        public Decimal? DJ { get; set; }
        //半天房间
        public Decimal? BJ { get; set; }
        //说明
        public string Detail { get; set; }
        //级别ID
        public Int32? JBID { get; set; }
        //楼栋ID
        public Int32? LdID { get; set; }
        //楼层ID
        public Int32? LcID { get; set; }
        //房间总的位置信息
        public string Location { get; set; }
        public string Lc { get; set; }
        public string Ld { get; set; }
    }
}