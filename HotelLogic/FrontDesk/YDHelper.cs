using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.Setting;

namespace HotelLogic.FrontDesk
{
    /// <summary>
    /// 所有预定相关的操作
    /// YDOrder,Yd_Dd,预定单信息
    /// YDFJSummer,Yd_Fj,预定房间概要信息表
    /// YDDetail,YD_Pf,预定具体房间信息
    /// </summary>
    public class YDHelper
    {
        DateTime now = System.DateTime.Now;
        #region 生成预定单部分 预定成功后 将相应房间信息变成预订状态
        //仅生成预定单信息，对应表Yd_Dd添加一条预定信息
        public string CreateYDOrder(YDOrder yd)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //dd model to yd_dd, part information, should be append later
                Yd_Dd temp = new Yd_Dd();
                temp.YDID = Guid.NewGuid();
                yd.YDID = temp.YDID;
                temp.MainZhangID = temp.YDID.ToString();
                temp.d_mc = yd.Yder;
                temp.d_dh = yd.YdTel;
                temp.mc = yd.Customer;
                temp.jc = yd.Customer;
               // temp.dr = yd.OnBoardTime;//客人信息录入的预定时间不要
                //temp.lr = yd.LeaveTime;
                temp.MemberCardNo = yd.MemberCardNo;
                temp.d_bz = yd.D_BZ;
                temp.d_dj = yd.D_DJ;
                temp.d_dw = yd.D_DW;
                temp.d_fs = yd.D_FKFS;
                temp.d_rq = yd.D_RQ;
                temp.gj = yd.GJ;
                temp.lx = yd.LX;
                temp.rs = yd.RS;
                temp.s_cz = yd.S_CZ;
                temp.Saler = yd.Saler;
                if (yd.D_DJ > 0)
                {
                    temp.Status = 2;//付了定金为确定预定
                }
                else
                {
                    temp.Status = 0;//普通预定
                }
                temp.Tqr = yd.TQR;
                temp.YDNum = "YD" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                temp.YDWay = yd.YDWay;
                temp.Zkl = yd.ZKL;
                temp.Company = yd.Company;
                db.Yd_Dd.AddObject(temp);
                db.SaveChanges();
                result = temp.YDID.ToString();
            }
            return result;
        }
        public string CreateOrder(string starttime,string endtime,string ydid)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //查询出相同MainZhangID的记录
                var yd = (from a in db.Yd_Dd
                          where a.MainZhangID == ydid && a.dr == null
                                              && (a.Status == 0 || a.Status == 2)
                                  select a).SingleOrDefault();
                if (yd != null)
                 {
                       Yd_Dd temp = new Yd_Dd();
                        temp.YDID = Guid.NewGuid();
                        temp.MainZhangID = ydid;
                        temp.dr = Convert.ToDateTime(starttime);
                        temp.lr = Convert.ToDateTime(endtime);
                        temp.YDNum = "YD" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();

                        temp.d_mc = yd.d_mc;
                        temp.d_dh = yd.d_dh;
                        temp.mc = yd.mc;
                        temp.jc = yd.jc;                        
                        temp.MemberCardNo = yd.MemberCardNo;
                        temp.d_bz = yd.d_bz;
                        temp.d_dj = yd.d_dj;
                        if (yd.d_dj > 0)
                        {
                            temp.Status = 2;
                        }
                        else
                        {
                            temp.Status = 0;
                        }
                        temp.d_dw = yd.d_dw;
                        temp.d_fs = yd.d_fs;
                        temp.d_rq = yd.d_rq;
                        temp.gj = yd.gj;
                        temp.lx = yd.lx;
                        temp.rs = yd.rs;
                        temp.s_cz = yd.s_cz;
                        temp.Saler = yd.Saler;
                        
                        temp.Tqr = yd.Tqr;
                        temp.YDWay = yd.YDWay;
                        temp.Zkl = yd.Zkl;
                        temp.Company = yd.Company;

                        db.Yd_Dd.AddObject(temp);
                        db.SaveChanges();
                        result = "0";
                 }
               
            }
            return result;
        }
        //删除预订单信息
        public string DeleteOrder(string ydnum)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    //删除预订单信息
                    var temp = db.Yd_Dd.FirstOrDefault(a => a.YDNum == ydnum);
                    if (temp != null)
                    {//判断是否有预订房间  如果有 先删除预订房间信息
                        var t = db.Yd_Fj.FirstOrDefault(a => a.MainZhangID == temp.YDNum);
                        if (t != null)
                        {
                            db.Yd_Fj.DeleteObject(t);
                        }
                        db.Yd_Dd.DeleteObject(temp);
                        db.SaveChanges();
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
        //删除预订房间信息
        public string DeleteOrderFJ(int id)
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                        var t = db.Yd_Fj.FirstOrDefault(a => a.AutoId ==id);
                        if (t != null)
                        {
                            db.Yd_Fj.DeleteObject(t);
                            db.SaveChanges();
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


        //生成预定单信息，并且生成预定信息概要，即Yd_Dd和Yd_Fj添加记录
        public string CreateYDOrderAndSummer(YDOrder yd, List<YDFJSummer> fjs, List<YDDetail> fjl)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //dd model to yd_dd, part information, should be append later
                Yd_Dd temp = new Yd_Dd();
                temp.YDID = Guid.NewGuid();
                temp.MainZhangID = temp.YDID.ToString();
                temp.d_mc = yd.Yder;
                temp.d_dh = yd.YdTel;
                temp.mc = yd.Customer;
                temp.jc = yd.Customer;
                temp.dr = yd.OnBoardTime;
                temp.lr = yd.LeaveTime;
                temp.MemberCardNo = yd.MemberCardNo;
                temp.d_bz = yd.D_BZ;
                temp.d_dj = yd.D_DJ;                
                if (yd.D_DJ > 0)
                {
                    temp.orderType = "确认预定";
                    temp.Status = 2;
                }
                else
                {
                    temp.orderType = "普通预定";
                    temp.Status = 0;
                }
               
                temp.d_dw = yd.D_DW;
                temp.d_fs = yd.D_FKFS;
                temp.d_rq = yd.D_RQ;
                temp.gj = yd.GJ;
                temp.lx = yd.LX;
                temp.rs = yd.RS;
                temp.s_cz = yd.S_CZ;
                temp.Saler = yd.Saler;
               
                temp.Tqr = yd.TQR;
                temp.YDNum = "YD" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                temp.YDWay = yd.YDWay;
                temp.Zkl = yd.ZKL;
                temp.Company = yd.Company;
                db.Yd_Dd.AddObject(temp);

                //YD summer add
                foreach (var fj in fjs)
                {
                    Yd_Fj fjtemp = new Yd_Fj();
                    fjtemp.f_jb = fj.JB;
                    fjtemp.f_sl = fj.SL;
                    //fjtemp.YDID = temp.YDID;
                    fjtemp.MainZhangID = temp.YDNum;
                    db.Yd_Fj.AddObject(fjtemp);
                }
                //添加预定房间信息
                //YD Fangjian List add
                foreach (var f in fjl)
                {
                    Yd_Pf ftemp = new Yd_Pf();
                    ftemp.f_fh = f.FH;
                    ftemp.f_jb = f.JB;
                    ftemp.CurTime = DateTime.Now;
                    //ftemp.YDID = temp.YDID;
                    ftemp.YDNum = temp.YDNum;
                    db.Yd_Pf.AddObject(ftemp);
                }
                //保存所做的更改
                db.SaveChanges();
                result = "0";
            }
            return result;
        }
        //生成全部预定单信息，包括概要部分和具体分配的房间信息
        //fjs,fangjiansummer;fjl,fangjianlist
        public string CreateYDAllInfo(YDOrder yd, List<YDFJSummer> fjs,List<YDDetail> fjl)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //dd model to yd_dd, part information, should be append later
                Yd_Dd temp = new Yd_Dd();
                temp.YDID = Guid.NewGuid();
                temp.MainZhangID = temp.YDID.ToString();
                temp.d_mc = yd.Yder;
                temp.d_dh = yd.YdTel;
                temp.mc = yd.Customer;
                temp.jc = yd.Customer;
                temp.dr = yd.OnBoardTime;
                temp.lr = yd.LeaveTime;
                temp.MemberCardNo = yd.MemberCardNo;
                temp.d_bz = yd.D_BZ;
                temp.d_dj = yd.D_DJ;
                if (yd.D_DJ > 0)
                {
                    temp.Status = 2;
                }
                else
                {
                    temp.Status = 0;
                }
                temp.d_dw = yd.D_DW;
                temp.d_fs = yd.D_FKFS;
                temp.d_rq = yd.D_RQ;
                temp.gj = yd.GJ;
                temp.lx = yd.LX;
                temp.rs = yd.RS;
                temp.s_cz = yd.S_CZ;
                temp.Saler = yd.Saler;
              
                temp.Tqr = yd.TQR;
                temp.YDNum = "YD" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                temp.YDWay = yd.YDWay;
                temp.Zkl = yd.ZKL;
                temp.Company = yd.Company;
                db.Yd_Dd.AddObject(temp);

                //YD summer add
                foreach (var fj in fjs)
                {
                    Yd_Fj fjtemp = new Yd_Fj();
                    fjtemp.f_jb = fj.JB;
                    fjtemp.f_sl = fj.SL;
                   // fjtemp.YDID = temp.YDID;
                    fjtemp.MainZhangID = temp.YDNum;
                    fjtemp.f_dm = fj.f_dm;
                    db.Yd_Fj.AddObject(fjtemp);
                }

                //YD Fangjian List add
                foreach (var f in fjl)
                {
                    Yd_Pf ftemp = new Yd_Pf();
                    ftemp.f_fh = f.FH;
                    ftemp.f_jb = f.JB;
                    ftemp.CurTime = DateTime.Now;
                    //ftemp.YDID = temp.YDID;
                    ftemp.YDNum = temp.YDNum;
                    db.Yd_Pf.AddObject(ftemp);
                }
                //保存所做的更改
                db.SaveChanges();
                //改变房间状态,仅当order的时间是当天才显示预订状态
                if (yd.OnBoardTime != null)
                {
                    if (Convert.ToDateTime(yd.OnBoardTime).Date == System.DateTime.Today)
                    {
                        foreach (var f in fjl)
                        {
                            RoomStatus changer = new RoomStatus();
                            changer.SetRoomStatus(f.FH, "预订");
                        }
                    }
                }
                result = "0";
            }
            return result;
        }
        #endregion

        public bool ModifyYdFjInfo(string mainid, string ydnum, List<YDFJSaveInfo> fjl, string ydOnBoardTime)
        {

            using (HotelDBEntities db = new HotelDBEntities())
            {
                foreach (YDFJSaveInfo ydinfo in fjl)
                {
                    Guid gd = new Guid(mainid);
                    var fjs = from f in db.Yd_Fj
                              where f.YDID == gd && f.MainZhangID == ydnum &&f.AutoId==ydinfo.AutoID
                              select f;
                    foreach (var fj in fjs)
                    {
                        fj.ShiJiFangJia = ydinfo.ShiJiFangJia;
                        fj.CustomerName = ydinfo.Customer;
                        fj.f_dm = ydinfo.f_dm;
                        fj.ZhuCong = ydinfo.ZhuCong;

                        if (!string.IsNullOrEmpty(ydinfo.f_dm))
                        {
                            //改变房间状态,仅当order的时间是当天才显示预订状态
                            if (ydOnBoardTime != null)
                            {
                                if (Convert.ToDateTime(ydOnBoardTime).Date == System.DateTime.Today)
                                {
                                    foreach (var f in fjl)
                                    {
                                        RoomStatus changer = new RoomStatus();
                                        changer.SetRoomStatus(ydinfo.f_dm, "预订");
                                    }
                                }
                            }
                        }

                    }
                    //保存到预定房间详细表
                    Yd_Pf ftemp = new Yd_Pf();
                    ftemp.f_fh = ydinfo.f_dm;
                    ftemp.f_jb = ydinfo.JB;
                    ftemp.CurTime = DateTime.Now;
                  //  ftemp.YDID = gd;
                    ftemp.YDNum = ydnum;
                    db.Yd_Pf.AddObject(ftemp);

                    db.SaveChanges();
                }
            }
            return true;
        }

        public bool SaveYdZhuanzhang(YDShouKuan ydinfo)
        {           
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Yd_ShouKuan fjtemp = new Yd_ShouKuan();
                fjtemp.Fkfs = ydinfo.Fkfs;
                fjtemp.beizhu = ydinfo.beizhu;
                fjtemp.shoukuanType = ydinfo.shoukuanType;
                fjtemp.danjuhao = ydinfo.danjuhao;
                fjtemp.jine = ydinfo.jine;
                fjtemp.relatedname = ydinfo.relatedname;
                fjtemp.time = ydinfo.time;
                fjtemp.ydGuid =new Guid(ydinfo.ydGuid);
                fjtemp.zhangkuan = ydinfo.zhangkuan;
                db.Yd_ShouKuan.AddObject(fjtemp);
                db.SaveChanges();  
            }
            return true;
        }
        public bool CreateYdFjInfo(string mainid, string ydnum, List<YDFJInfo> fjl)
        {
           
            using (HotelDBEntities db = new HotelDBEntities())
            {
                foreach (YDFJInfo ydinfo in fjl)
                {
                    for (int i = 0; i < ydinfo.UsedSL; i++)
                    {
                        Yd_Fj fjtemp = new Yd_Fj();
                        fjtemp.f_jb = ydinfo.FJType;
                        fjtemp.f_sl = 1;// Convert.ToInt16(ydinfo.UsedSL);//预定数量
                        fjtemp.YDID = new Guid(mainid);
                        fjtemp.BiaoZhunFangJia = ydinfo.BiaoZhunFangJia;
                        fjtemp.x_sl = Convert.ToInt16(ydinfo.SL);//总数
                        fjtemp.Status = 0;//初始为0
                        fjtemp.MainZhangID = ydnum;//订单号
                        db.Yd_Fj.AddObject(fjtemp);

                      
                    }
                }

                
                db.SaveChanges();
            }
            return true;
        }
        public List<YDShouKuan> GetYfjInfo(string ydid)
        {
            List<YDShouKuan> result = new List<YDShouKuan>();

            using (HotelDBEntities db = new HotelDBEntities())
            {
                 Guid gd=new  Guid(ydid);
                var temp = (from a in db.Yd_ShouKuan
                            orderby a.id descending
                            where a.ydGuid == gd
                            select a);
                  foreach (var t in temp)
                {
                    YDShouKuan item = new YDShouKuan();
                    item.Fkfs = t.Fkfs;
                    item.jine = t.jine;
                    item.time = t.time;
                    item.danjuhao = t.danjuhao;
                    item.beizhu = t.beizhu;
                    item.relatedname = t.relatedname;
                    item.zhangkuan = t.zhangkuan;
                    item.shoukuanType = t.shoukuanType;
                    if (item.shoukuanType > 0)
                    {
                        item.type = "预付金收款";
                    }
                    else
                    {
                        item.type = "预付金退款";
                    }
                    result.Add(item);
                }
            }
            return result;
        } 
        public List<YDFJInfo> GetYdFjInfo(DateTime sttime,DateTime endtime)
        {
            List<YDFJInfo> result = new List<YDFJInfo>();

            using (HotelDBEntities db = new HotelDBEntities())
            {
                List<SpKgFjlx> temp = db.Sp_GetKgFjlxFun(sttime, endtime,  1).ToList();
                foreach (SpKgFjlx t in temp)
                {
                    YDFJInfo item = new YDFJInfo();

                    item.FJType =  t.a_jb ;
                    item.OccupySL = t.b_zjs - t.r1;
                    item.SL = t.b_zjs;
                    item.BiaoZhunFangJia = t.c_dj;
                    item.UsedSL = 0;

                 
                    result.Add(item);
                }
            }
            return result;
        }
        //读取所有退库单及所属的商品信息
        public List<YDOrder> ReadOutOrder(int page, int rows, string ydid)
        {
            List<YDOrder> result = new List<YDOrder>();
          
            
            using (HotelDBEntities db = new HotelDBEntities())
            {                
                var temp = (from a in db.Yd_Dd
                            orderby a.AutoId descending
                            where a.dr != null && a.MainZhangID == ydid
                            select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    YDOrder item = new YDOrder();
                    item.RecordList = new List<YDFJSummer>();
                    item.YDID = t.YDID;
                    item.Customer = t.mc;
                    item.Yder = t.d_mc;

                    item.YdTel = t.d_dh;
                    item.OnBoardTime = t.dr;
                    item.LeaveTime = t.lr;
                    item.MemberCardNo = t.MemberCardNo;
                    //item.AssignRoom = t.beizhu;
                    item.LX = t.lx;
                    item.GJ = t.gj;
                    item.RS = t.rs;
                    item.Company = t.Company;
                    item.ZKL = t.Zkl;
                    item.D_DW = t.d_dw;
                    item.D_FKFS = t.d_fs;
                    item.D_DJ = t.d_dj;
                    item.D_RQ = t.d_rq;
                    item.S_CZ = t.s_cz;
                    item.Saler = t.Saler;
                    item.YDWay = t.YDWay;
                    item.D_BZ = t.d_bz;
                    item.YDNum = t.YDNum;
                    item.TQR = t.Tqr;
                    item.MainZhangID = t.MainZhangID;
                    item.Status = t.Status;
                    item.SL = t.SL;                    
                    //读取明细
                    var details = from b in db.Yd_Fj
                                  where b.MainZhangID == t.YDNum
                                  select b;
                    foreach (var detail in details)
                    {
                        YDFJSummer record = new YDFJSummer();
                        record.YDOrderID = detail.YDID;
                        record.JB = detail.f_dm;
                        record.SL = detail.f_sl;
                        record.AutoID = detail.AutoId;
                        record.ShiJiFangJia = detail.ShiJiFangJia;
                        record.BiaoZhunFangJia = detail.BiaoZhunFangJia;
                        record.CustomerName = detail.CustomerName;
                        record.ZhuCong = detail.ZhuCong;
                        record.RoomType = detail.f_jb;
                        record.OnBoardTime = item.OnBoardTime;
                        record.LeaveTime = item.LeaveTime;
                        item.RecordList.Add(record);
                    }

                    result.Add(item);
                }
            }
            return result;
        }

        #region 向已存在的预定单信息中添加，完善信息的操作
        //预定单已存在，添加预定概要信息
        //public string AppendFJSummer(YDOrder yd, List<YDFJSummer> fjs)
        //{
        //    string result = "-1";
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        var query = (from a in db.Yd_Dd
        //                     where a.YDID == yd.YDID
        //                     select a).FirstOrDefault();
        //        //if exists, then append summer infomation
        //        if (query != null)
        //        {
        //            foreach (var fj in fjs)
        //            {
        //                Yd_Fj fjtemp = new Yd_Fj();
        //                fjtemp.f_jb = fj.JB;
        //                fjtemp.f_sl = fj.SL;
        //                fjtemp.YDID = query.YDID;
        //                db.Yd_Fj.AddObject(fjtemp);
        //            }
        //            db.SaveChanges();
        //            result = "0";
        //        }
        //    }
        //    return result;
        //}
        //预定单已存在，添加预定房间详细信息
        //public string AppendFJDetailList(YDOrder yd, List<YDDetail> fjl)
        //{
        //    string result = "-1";
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        var query = (from a in db.Yd_Dd
        //                     where a.YDID == yd.YDID
        //                     select a).FirstOrDefault();
        //        //if exists, then append summer infomation
        //        if (query != null)
        //        {
        //            foreach (var fj in fjl)
        //            {
        //                Yd_Pf ftemp = new Yd_Pf();
        //                ftemp.f_fh = fj.FH;
        //                ftemp.f_jb = fj.JB;
        //                ftemp.YDID = query.YDID;
        //                db.Yd_Pf.AddObject(ftemp);
        //            }
        //            db.SaveChanges();
        //            //改变房间状态
        //            foreach (var f in fjl)
        //            {
        //                RoomStatus changer = new RoomStatus();
        //                changer.SetRoomStatus(f.FH, "预订");
        //            }
        //            result = "0";
        //        }
        //    }
        //    return result;
        //}
        //预定单已存在，预定概要信息已存在，添加房间详细信息
        //public string AppendSummerAndDetail(YDOrder yd, List<YDFJSummer> fjs, List<YDDetail> fjl)
        //{
        //    string result = "-1";
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        var query = (from a in db.Yd_Dd
        //                     where a.YDID == yd.YDID
        //                     select a).FirstOrDefault();
        //        //if exists, then append summer infomation
        //        if (query != null)
        //        {
        //            foreach (var fj in fjs)
        //            {
        //                Yd_Fj fjtemp = new Yd_Fj();
        //                fjtemp.f_jb = fj.JB;
        //                fjtemp.f_sl = fj.SL;
        //                fjtemp.YDID = query.YDID;
        //                db.Yd_Fj.AddObject(fjtemp);
        //            }
        //            foreach (var f in fjl)
        //            {
        //                Yd_Pf ftemp = new Yd_Pf();
        //                ftemp.f_fh = f.FH;
        //                ftemp.f_jb = f.JB;
        //                ftemp.YDID = query.YDID;
        //                db.Yd_Pf.AddObject(ftemp);
        //            }
        //            db.SaveChanges();
        //            //改变房间状态
        //            foreach (var f in fjl)
        //            {
        //                RoomStatus changer = new RoomStatus();
        //                changer.SetRoomStatus(f.FH, "预订");
        //            }
        //            result = "0";
        //        }
        //    }
        //    return result;
        //}
        #endregion

        #region 读取预定信息
        //读取预定单
        public List<YDOrder> ReadYDOrder()
        {
            List<YDOrder> result = new List<YDOrder>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Yd_Dd
                            select a;
                foreach (var q in query)
                {
                    YDOrder temp = new YDOrder();
                    temp.YDID = q.YDID;
                    temp.Customer = q.mc;
                    temp.LeaveTime = q.lr;
                    temp.OnBoardTime = q.dr;
                    temp.Yder = q.d_mc;
                    temp.YdTel = q.d_dh;
                    temp.Company = q.Company;
                    temp.D_BZ = q.d_bz;
                    temp.D_DJ = q.d_dj;
                    temp.D_DW = q.d_dw;
                    temp.D_FKFS = q.d_fs;
                    temp.D_RQ = q.d_rq;
                    temp.GJ = q.gj;
                    temp.LX = q.lx;
                    temp.MainZhangID = q.MainZhangID;
                    temp.MemberCardNo = q.MemberCardNo;
                    temp.RS = q.rs;
                    temp.S_CZ = q.s_cz;
                    temp.Saler = q.Saler;
                    temp.Status = q.Status;
                    temp.TQR = q.Tqr;
                    temp.YDNum = q.YDNum;
                    temp.YDWay = q.YDWay;
                    temp.ZKL = q.Zkl;
                    result.Add(temp);      
                }
            }
            return result;
        }
        //读取当前有效的预定单
        public List<YDOrder> ReadActiveYDOrder()
        {
            List<YDOrder> result = new List<YDOrder>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Yd_Dd
                            where (a.Status == 0 || a.Status == 2)
                            select a;
                foreach (var q in query)
                {
                    YDOrder temp = new YDOrder();
                    //预定单本身信息
                    temp.YDID = q.YDID;
                    temp.Customer = q.mc;
                    temp.LeaveTime = q.lr;
                    temp.OnBoardTime = q.dr;
                    temp.Yder = q.d_mc;
                    temp.YdTel = q.d_dh;
                    temp.Company = q.Company;
                    temp.D_BZ = q.d_bz;
                    temp.D_DJ = q.d_dj;
                    temp.D_DW = q.d_dw;
                    temp.D_FKFS = q.d_fs;
                    temp.D_RQ = q.d_rq;
                    temp.GJ = q.gj;
                    temp.LX = q.lx;
                    temp.MainZhangID = q.MainZhangID;
                    temp.MemberCardNo = q.MemberCardNo;
                    temp.RS = q.rs;
                    temp.S_CZ = q.s_cz;
                    temp.Saler = q.Saler;
                    temp.Status = q.Status;
                    temp.TQR = q.Tqr;
                    temp.YDNum = q.YDNum;
                    temp.YDWay = q.YDWay;
                    temp.ZKL = q.Zkl;
                    temp.AssignRoom = "未配房";
                    //join 概要信息
                    int? sl1 = 0;
                    var summers = from a in db.Yd_Fj
                                  where a.MainZhangID == q.YDNum
                                  select a;
                    foreach (var s in summers)
                    {
                        sl1 = sl1 + s.f_sl;
                    }
                    //join房间信息
                    var fjs = from f in db.Yd_Pf
                              where f.YDNum == q.YDNum
                              select f;
                    if (sl1 == fjs.Count())
                    {
                        temp.AssignRoom="";
                        foreach (var fj in fjs)
                        {
                            temp.AssignRoom += fj.f_fh + ";";
                        }
                    }     
                    result.Add(temp);
                }
            }
            return result;
        }
        //读取当日即将到达的预订信息
        public List<YDOrder> ReadTodayYDOrder()
        {
            List<YDOrder> result = new List<YDOrder>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Yd_Dd
                            where (a.Status == 0 || a.Status == 2) && a.dr != null
                            
                            select a;
                foreach (var q in query)
                {   // dr为今天的YD信息  
                    if (q.dr != null)
                    {
                        if (Convert.ToDateTime(q.dr).Date == System.DateTime.Today)
                        {
                            YDOrder temp = new YDOrder();
                            //预定单本身信息
                            temp.YDID = q.YDID;
                            temp.Customer = q.mc;
                            temp.LeaveTime = q.lr;
                            temp.OnBoardTime = q.dr;
                            temp.Yder = q.d_mc;
                            temp.YdTel = q.d_dh;
                            temp.Company = q.Company;
                            temp.D_BZ = q.d_bz;
                            temp.D_DJ = q.d_dj;
                            temp.D_DW = q.d_dw;
                            temp.D_FKFS = q.d_fs;
                            temp.D_RQ = q.d_rq;
                            temp.GJ = q.gj;
                            temp.LX = q.lx;
                            temp.MainZhangID = q.MainZhangID;
                            temp.MemberCardNo = q.MemberCardNo;
                            temp.RS = q.rs;
                            temp.S_CZ = q.s_cz;
                            temp.Saler = q.Saler;
                            temp.Status = q.Status;
                            temp.TQR = q.Tqr;
                            temp.YDNum = q.YDNum;
                            temp.YDWay = q.YDWay;
                            temp.ZKL = q.Zkl;
                            temp.AssignRoom = "未配房";
                            //join 概要信息
                            int? sl1 = 0;
                            var summers = from a in db.Yd_Fj
                                          where a.MainZhangID == q.YDNum
                                          select a;
                            foreach (var s in summers)
                            {
                                sl1 = sl1 + s.f_sl;
                            }
                            //join房间信息
                            var fjs = from f in db.Yd_Pf
                                      where f.YDNum == q.YDNum
                                      select f;
                            if (sl1 == fjs.Count())
                            {
                                temp.AssignRoom = "";
                                foreach (var fj in fjs)
                                {
                                    temp.AssignRoom += fj.f_fh + ";";
                                }
                            }

                            result.Add(temp);
                        }
                    }
                }
            }
            return result;
        }
        //根据预定单Guid ID,读取预定概要信息
        //public List<YDFJSummer> ReadFJSummer(Guid guid)
        //{
        //    List<YDFJSummer> result = new List<YDFJSummer>();
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        var query = from a in db.Yd_Fj
        //                    where a.YDID == guid
        //                    select a;
        //        foreach (var q in query)
        //        {
        //            YDFJSummer temp = new YDFJSummer();
        //            temp.AutoID = q.AutoId;
        //            temp.JB = q.f_jb;
        //            temp.SL = q.f_sl;
        //            temp.YDOrderID = q.YDID;
        //            result.Add(temp);
        //        }
        //    }
        //    return result;
        //}
        //根据预定单Guid ID,读取预定房间详细信息
        //public List<YDDetail> ReadYDDetail(Guid guid)
        //{
        //    List<YDDetail> result = new List<YDDetail>();
        //    using (HotelDBEntities db = new HotelDBEntities())
        //    {
        //        var query = from a in db.Yd_Pf
        //                    where a.YDID == guid
        //                    select a;
        //        foreach (var q in query)
        //        {
        //            YDDetail temp = new YDDetail();
        //            temp.AutoID = q.AutoId;
        //            temp.FH = q.f_fh;
        //            temp.JB = q.f_jb;
        //            temp.YDOrderID = q.YDID;
        //            result.Add(temp);
        //        }
        //    }
        //    return result;
        //}
        //根据查询条件得到预订单信息
        public List<YDOrder> ReadYDOrderByFilter(YDFilter filter)
        {
            List<YDOrder> result = new List<YDOrder>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var orders = from a in db.Yd_Dd
                             where a.dr!=null&&a.lr!=null
                             select a;

                //订单类别
                if (!string.IsNullOrEmpty(filter.YDLB))
                {
                    int statue = Convert.ToInt32(filter.YDLB);
                    if (statue != 6)
                    {
                        orders = from a in orders
                                 where a.Status == statue
                                 select a;
                    }

                }
                else
                {
                    orders = from a in orders
                             where a.Status == 0 || a.Status == 2
                             select a;
                }
                //预订单号
                if (!string.IsNullOrEmpty(filter.YDNum))
                {
                    orders = from a in orders
                             where a.YDNum.Contains(filter.YDNum)
                             select a;
                }
                //客户类型
                if (!string.IsNullOrEmpty(filter.LX))
                {
                    orders = from a in orders
                             where a.lx.Contains(filter.LX)
                             select a;
                }
                //预订单位
                if (!string.IsNullOrEmpty(filter.D_DW))
                {
                    orders = from a in orders
                             where a.d_dw.Contains(filter.D_DW)
                             select a;
                }
                //房间号,无字段存储该信息暂时
                //到店日期
                if (filter.DR.Year>2001)
                {
                    orders = from a in orders
                             where a.dr >= filter.DR
                             //Convert.ToDateTime(a.dr).Year == filter.DR.Year
                             //&& Convert.ToDateTime(a.dr).Month == filter.DR.Month
                             //&& Convert.ToDateTime(a.dr).Day == filter.DR.Day
                             select a;
                }
                
                //联系电话
                if (!string.IsNullOrEmpty(filter.YdTel))
                {
                    orders = from a in orders
                             where a.d_dh.Contains(filter.YdTel)
                             select a;
                }
                //国籍
                if (!string.IsNullOrEmpty(filter.GJ))
                {
                    orders = from a in orders
                             where a.gj.Contains(filter.GJ)
                             select a;
                }
                //协议单位
                if (!string.IsNullOrEmpty(filter.Company))
                {
                    orders = from a in orders
                             where a.Company.Contains(filter.Company)
                             select a;
                }
                //预订日期
                if (filter.YDR.Year > 2001)
                {
                    orders = from a in orders
                             where Convert.ToDateTime(a.d_rq).Year == filter.YDR.Year
                             && Convert.ToDateTime(a.d_rq).Month == filter.YDR.Month
                             && Convert.ToDateTime(a.d_rq).Day == filter.YDR.Day
                             select a;
                }
                //离点日期
                if (filter.LR.Year > 2001)
                {
                    orders = from a in orders
                             where a.lr <= filter.LR
                             //Convert.ToDateTime(a.lr).Year == filter.LR.Year
                             //&& Convert.ToDateTime(a.lr).Month == filter.LR.Month
                             //&& Convert.ToDateTime(a.lr).Day == filter.LR.Day
                             select a;
                }
                //会员卡号
                if (!string.IsNullOrEmpty(filter.MemberCardNo))
                {
                    orders = from a in orders
                             where a.MemberCardNo.Contains(filter.MemberCardNo)
                             select a;
                }
                //客户名查询
                if (!string.IsNullOrEmpty(filter.Customer))
                {
                    orders = from a in orders
                             where a.mc.Contains(filter.Customer)
                             select a;
                }
                //预定人查询
                if (!string.IsNullOrEmpty(filter.Yder))
                {
                    orders = from a in orders
                             where a.d_mc.Contains(filter.Yder)
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.FH))
                {
                    
                    orders = from a in orders
                             join b in db.Yd_Fj on a.YDNum equals b.MainZhangID
                             where b.f_dm.Contains(filter.FH)
                             select a;
                }
                foreach (var q in orders)
                {
                    YDOrder temp = new YDOrder();
                    temp.YDID = q.YDID;
                    temp.Customer = q.mc;
                    temp.LeaveTime = q.lr;
                    temp.OnBoardTime = q.dr;
                    temp.Yder = q.d_mc;
                    temp.YdTel = q.d_dh;
                    temp.Company = q.Company;
                    temp.D_BZ = q.d_bz;
                    temp.D_DJ = q.d_dj;
                    temp.D_DW = q.d_dw;
                    temp.D_FKFS = q.d_fs;
                    temp.D_RQ = q.d_rq;
                    temp.GJ = q.gj;
                    temp.LX = q.lx;
                    temp.MainZhangID = q.MainZhangID;
                    temp.MemberCardNo = q.MemberCardNo;
                    temp.RS = q.rs;
                    temp.S_CZ = q.s_cz;
                    temp.Saler = q.Saler;
                    temp.Status = q.Status;
                    temp.TQR = q.Tqr;
                    temp.YDNum = q.YDNum;
                    temp.YDWay = q.YDWay;
                    temp.ZKL = q.Zkl;
                    temp.AssignRoom = "未配房";
                    //join 概要信息
                    int? sl1 = 0;
                    var summers = from a in db.Yd_Fj
                                  where a.MainZhangID == q.YDNum
                                  select a;
                    foreach (var s in summers)
                    {
                        sl1 = sl1 + s.f_sl;
                    }
                    //join房间信息
                    var fjs = from f in db.Yd_Pf
                              where f.YDNum == q.YDNum
                              select f;
                    if (sl1 == fjs.Count())
                    {
                        temp.AssignRoom = "";
                        foreach (var fj in fjs)
                        {
                            temp.AssignRoom += fj.f_fh + ";";
                        }
                    }
                    result.Add(temp);
                }
            }
            return result;
        }
        #endregion

        #region 根据房号生成一份可打印的预定单
        public PrintYD PrintYDOrder(string fh)
        {
            PrintYD result = new PrintYD();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //根据房间号码得到预定订单guid
                var Ydfj = (from a in db.Yd_Pf
                                 where a.f_fh == fh
                                 && (a.Status == 0 || a.Status == 2)
                                 select a).First();
                if (Ydfj != null)
                {
                   // Guid? orderguid = Ydfj.YDID;

                    //当前房号的有效预定信息
                    var Ydorder = (from a in db.Yd_Dd
                                   where a.YDNum == Ydfj.YDNum
                                  && (a.Status == 0 || a.Status == 2)
                                  select a).SingleOrDefault();
                    if (Ydorder != null)
                    {
                        //查询预定单信息
                        result.Customer = Ydorder.mc;
                        result.LeaveTime = Ydorder.lr;
                        result.OnBoardTime = Ydorder.dr;
                        result.Yder = Ydorder.d_mc;
                        result.YdTel = Ydorder.d_dh;
                        result.FJs = new List<YDDetail>();

                        //查询所属的该订单的房间信息
                        var fjs = from f in db.Yd_Pf
                                  where f.YDNum == Ydfj.YDNum
                                  select f;
                        foreach (var fj in fjs)
                        {
                            YDDetail temp = new YDDetail();
                            temp.AutoID = fj.AutoId;
                            temp.FH = fj.f_fh;
                            temp.JB = fj.f_jb;
                            temp.YDOrderID = fj.YDID;
                            temp.YDNum = fj.YDNum;
                            result.FJs.Add(temp);
                        }
                    }
                }
            }
            return result;
        }

        //根据预订单号生成一份可打印的预定单
        public PrintYD PrintYDOrderByNum(string ydnum)
        {
            PrintYD result = new PrintYD();
            //result.Customer = ydnum;
            result.FJs = new List<YDDetail>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //当前的有效预定信息
                var Ydorder = (from a in db.Yd_Dd
                               where a.YDNum == ydnum
                               && (a.Status == 0 || a.Status == 2)
                               select a).SingleOrDefault();
                
                if (Ydorder != null)
                {
                    //查询预定单信息
                    result.Customer = Ydorder.mc;
                    result.LeaveTime = Ydorder.lr;
                    result.OnBoardTime = Ydorder.dr;
                    result.Yder = Ydorder.d_mc;
                    result.YdTel = Ydorder.d_dh;
                    

                    //查询所属的该订单的房间信息
                    var fjs = from f in db.Yd_Pf
                              where f.YDNum == Ydorder.YDNum
                              select f;
                    foreach (var fj in fjs)
                    {
                        YDDetail temp = new YDDetail();
                        temp.AutoID = fj.AutoId;
                        temp.FH = fj.f_fh;
                        temp.JB = fj.f_jb;
                        temp.YDOrderID = fj.YDID;
                        temp.YDNum = fj.YDNum;
                        result.FJs.Add(temp);
                    }
                }
            }
            return result;
        }
        #endregion

        #region 根据条件筛选预定单信息，包括历史信息,暂未实现

        #endregion

        #region 根据房号取消预定订单,order pf fj 全部change to 1
        public string CancelYDOrder(string fh)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //根据房间号码得到预定订单guid
                var Ydfj = (from a in db.Yd_Pf
                                 where a.f_fh == fh
                                 && (a.Status == 0 || a.Status == 2)
                                 select a).FirstOrDefault();
                if (Ydfj != null)
                {
                    //Guid? orderguid = Ydfj.YDID;

                    //当前房号的有效预定信息
                    var Ydorder = (from a in db.Yd_Dd
                                   where a.YDNum == Ydfj.YDNum
                                   && (a.Status == 0 || a.Status == 2)
                                   select a).SingleOrDefault();
                    //order status change to 1, obsolete
                    if (Ydorder != null)
                    {
                        Ydorder.Status = 4;//4表示取消预定单
                        //pf, change status to 1, obsolete
                        var fjs = from f in db.Yd_Fj
                                  where f.MainZhangID == Ydfj.YDNum
                                  select f;
                        foreach (var fj in fjs)
                        {
                            fj.Status = 4;
                        }
                        //get fjs, status to 1, room change to 空房
                        var pfs = from p in db.Yd_Pf
                                  where p.YDNum == Ydfj.YDNum
                                  select p;
                        foreach (var pf in pfs)
                        {
                            pf.Status = 4;
                            RoomStatus changer = new RoomStatus();
                            changer.SetRoomStatus(pf.f_fh, "空房");
                        }
                        db.SaveChanges();
                        result = "0";
                    }
                }
            }
            return result;
        }
        #endregion

        #region 根据房号预定转入住，或者根据预定单GUID转入住
        public string YDToRuzhuByFH(string FH)
        {
            string result = "-1";
            //根据房号得到YDOrder guid，调用YDToRuzhuByGUID

            return result;
        }


        public YDOrder GetYdInfo(string ydnum)
        {
            YDOrder result = new YDOrder();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //YDOrder
                var ydorder = (from a in db.Yd_Dd
                               where a.YDNum == ydnum
                              select a).SingleOrDefault();
                if (ydorder != null)
                {
                    //new ruzhu order

                    result.YDID = ydorder.YDID;
                    result.Customer = ydorder.mc;
                    result.OnBoardTime = ydorder.dr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.dr;
                    result.LeaveTime = ydorder.lr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.lr;
                    result.Yder = ydorder.d_mc;

                    result.YdTel = ydorder.d_dh;
                    //result.CustomerTel = ydorder.dr;
                    result.MemberCardNo = ydorder.MemberCardNo;
                    //ruzhuorder.TeQuanRen = order.TeQuanRen;
                    result.LX = ydorder.lx;

                    result.GJ = ydorder.gj;


                    result.RS = ydorder.rs;
                    result.Company = ydorder.Company;
                    result.ZKL = ydorder.Zkl;


                    result.D_DW = ydorder.d_dw;
                    result.D_FKFS = ydorder.d_fs;
                    result.D_DJ = ydorder.d_dj;

                    result.D_RQ = ydorder.d_rq;
                    result.S_CZ = ydorder.s_cz;

                    result.Saler = ydorder.Saler;
                    result.YDWay = ydorder.YDWay;
                    result.D_BZ = ydorder.d_bz;

                    result.YDNum = ydorder.YDNum;
                    result.TQR = ydorder.Tqr;

                    result.MainZhangID = ydorder.MainZhangID;
                    result.Status = ydorder.Status;
                    result.SL = ydorder.SL;

                    List<YDFJSummer> RecordList = new List<YDFJSummer>();

                    var pfs = from a in db.Yd_Fj
                              where a.MainZhangID == ydnum
                              select a;
                    foreach (var pf in pfs)
                    {
                        if (pf.ZhuCong.Trim() == "是")
                        {
                            result.ShiJiFangJia = pf.ShiJiFangJia;
                            result.BiaoZhunFangJia = pf.BiaoZhunFangJia;
                            result.FangjianLeixing = pf.f_jb;
                            result.FangHao = pf.f_dm;
                            result.CustomerZhu = pf.CustomerName;
                            
                        }
                        YDFJSummer attach = new YDFJSummer();
                        attach.YDOrderID = ydorder.YDID;
                        attach.JB = pf.f_dm;
                        attach.ShiJiFangJia = pf.ShiJiFangJia;
                        attach.BiaoZhunFangJia = pf.BiaoZhunFangJia;
                        attach.CustomerName = pf.CustomerName;
                       
                        attach.ZhuCong = pf.ZhuCong;
                        attach.RoomType = pf.f_jb;
                        attach.f_dm = pf.f_dm;
                        attach.OnBoardTime = ydorder.dr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.dr;
                        attach.LeaveTime = ydorder.lr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.lr;


                        RecordList.Add(attach);
                    }
                    result.RecordList = RecordList;
                }
            }
            return result;
        }
        public string YDToRuzhuByGUID(string ydnum)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //YDOrder
                var ydorder = (from a in db.Yd_Dd
                               where a.YDNum == ydnum
                              select a).SingleOrDefault();
                if (ydorder != null)
                {
                    //new ruzhu order
                    OrderViewModel ruzhuorder = new OrderViewModel();
                    ruzhuorder.XingMing = ydorder.d_mc;
                    ruzhuorder.DianHua = ydorder.d_dh;
                    ruzhuorder.DaodianTime = ydorder.dr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.dr;
                    ruzhuorder.LidianTime = ydorder.lr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.lr;
                    ruzhuorder.OrderGuid = System.Guid.NewGuid();

                    ruzhuorder.FukuanFangshi = ydorder.d_fs;
                    ruzhuorder.YaJin = ydorder.d_dj;
                    ruzhuorder.XieyiDanwei = ydorder.Company;
                    //ruzhuorder.TeQuanRen = order.TeQuanRen;
                    ruzhuorder.ZheKouLv = ydorder.Zkl;

                    ruzhuorder.BeiZhu = ydorder.d_bz;


                    ruzhuorder.KerenLeibie = ydorder.lx;
                    //ruzhuorder.TuanDui = order.TuanDui;
                    //ruzhuorder.ZhengjianLeibie = order.ZhengjianLeibie;
                   

                    //ruzhuorder.XingBie = order.XingBie;
                    ruzhuorder.HuiYuanKa = ydorder.MemberCardNo;
                    //ruzhuorder.JiFen = ydorder.d_dj;

                    //ruzhuorder.ZhengJianHao = ydorder.ZhengJianHao;
                   // ruzhuorder.DiZhi = order.DiZhi;
                   

                    List<OrderAttachModel> attaches=new List<OrderAttachModel>();

                    var pfs = from a in db.Yd_Fj
                              where a.MainZhangID == ydnum
                              select a;
                    foreach (var pf in pfs)
                    {
                        OrderAttachModel attach = new OrderAttachModel();
                        attach.XingMing = ydorder.d_mc;
                        attach.FangJianHao=pf.f_dm;
                        attach.OrderGuid = ruzhuorder.OrderGuid;
                        attach.ArriveTime = ydorder.dr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.dr;
                        attach.LeaveTime = ydorder.lr == null ? new DateTime(2001, 1, 1) : (DateTime)ydorder.lr;
                        if (pf.ZhuCong == "主")
                        {
                            ruzhuorder.ShijiFangjia = pf.ShiJiFangJia;
                            ruzhuorder.FangjianLeixing = pf.f_jb;
                            ruzhuorder.BiaozhunFangjia = pf.BiaoZhunFangJia.ToString();
                        }

                        attaches.Add(attach);
                    }
                    OrdersHelper helper = new OrdersHelper();
                    if(helper.CreateOrder(ruzhuorder, attaches) != "0")
                    {
                        LogModel data = new LogModel();
                        data.Info = "转入住失败";
                        LoggingHelper.Instance.LogInfo(data);
                    }
                   

                    //将订单转为过期
                    //当前房号的有效预定信息
                    var Ydorder = (from a in db.Yd_Dd
                                   where a.YDNum == ydnum
                                   && (a.Status == 0 || a.Status == 2)
                                   select a).SingleOrDefault();
                    //order status change to 1, obsolete
                    if (Ydorder != null)
                    {
                        Ydorder.Status = 3;//3表示已入住
                        //pf, change status to 1, obsolete
                        var fjs = from f in db.Yd_Fj
                                  where f.MainZhangID == ydnum
                                  select f;
                        foreach (var fj in fjs)
                        {
                            fj.Status = 3;
                        }
                        //get fjs, status to 1, room change to 空房
                        var ps = from p in db.Yd_Pf
                                 where p.YDNum == ydnum
                                  select p;
                        foreach (var p in ps)
                        {
                            p.Status = 3;
                        }
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
        #endregion

        #region 取消所有过期的预订订单
        public void CancelOvertimeYD()
        {
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //取消过期预订单
                var orders = from a in db.Yd_Dd
                             where (a.Status == 0 || a.Status == 2)
                             select a;

                foreach(var order in orders)
                {
                    //dr为可空的日期，这里不得不先得出所有，再进行日期判断
                    if (Convert.ToDateTime(order.dr) < System.DateTime.Now)
                    {
                        order.Status = 1;
                        var fjs = from f in db.Yd_Fj
                                  where f.MainZhangID == order.YDNum
                                  select f;
                        foreach (var fj in fjs)
                        {
                            fj.Status = 1;
                        }
                        var pfs = from p in db.Yd_Pf
                                  where p.YDNum == order.YDNum
                                  select p;
                        foreach (var p in pfs)
                        {
                            p.Status = 1;
                            RoomStatus changer = new RoomStatus();
                            changer.SetRoomStatus(p.f_fh, "空房");
                        }
                    }
                }
                db.SaveChanges();
            }
        }
        #endregion

    }

    /// <summary>
    /// 预定单信息model，对应表Yd_Dd
    /// </summary>
    public class YDOrder
    {
        //初始化，将默认值初始化到不为空的字段中
        public YDOrder()
        {
            OnBoardTime = new DateTime(2001, 01, 01);
            LeaveTime = new DateTime(2001, 01, 01);
            YDID = new Guid();
            D_DJ = 0.0M;
            ZKL = 10.0;
            Status = 0;
            RS = 0;
            D_RQ = new DateTime(2001, 01, 01);
            //MainZhangID = new Guid();
        }
        //每有一个预订单,生成一个GUID
        public System.Guid YDID { get; set; }
        //Customer客户
        public string Customer { get; set; }
        //Yder预订人
        public string Yder { get; set; }
        //YdTel 预订人电话
        public string YdTel { get; set; }
        //CustomerTel客人电话
        public string CustomerTel { get; set; }
        //入住日期
        public System.DateTime? OnBoardTime { get; set; }
        //离店日期
        public System.DateTime? LeaveTime { get; set; }
        //会员卡
        public string MemberCardNo { get; set; }
        //是否配房
        public string AssignRoom { get; set; }
        //客户类型
        public string LX { get; set; }
        //国籍
        public string GJ { get; set; }
        //客户人数
        public short? RS { get; set; }
        //协议单位
        public string Company { get; set; }
        //折扣率
        public double? ZKL { get; set; }
        //预订单位
        public string D_DW { get; set; }
        //付款方式
        public string D_FKFS { get; set; }
        //预订金
        public decimal? D_DJ { get; set; }
        //预订日期
        public DateTime? D_RQ { get; set; }
        //操作员
        public string S_CZ { get; set; }
        //销售员
        public string Saler { get; set; }
        //预订方式
        public string YDWay { get; set; }
        //备注
        public string D_BZ { get; set; }
        //预订单编号
        public string YDNum { get; set; }
        //特权人
        public string TQR { get; set; }
        //主帐GUID
        public string MainZhangID { get; set; }
        //状态
        public int? Status { get; set; }
        //数量
        public int? SL { get; set; }
        //预定房间详细，选中一行后用这个赋给下面的详细表
        public List<YDFJSummer> RecordList { get; set; }
        //主房间信息
        public decimal? ShiJiFangJia { get; set; }
        public decimal? BiaoZhunFangJia { get; set; }
        public string FangjianLeixing { get; set; }
        public string FangHao { get; set; }
        public string CustomerZhu { get; set; }  
    }
    /// <summary>
    /// 预定房间概要信息，若干间某种类型房,对应表Yd_Fj
    /// </summary>
    public class YDFJSummer
    {
        //所属预定单ID
        public System.Guid? YDOrderID { get; set; }
        //房号
        public string JB { get; set; }
        //数量
        public short? SL { get; set; }
        //自动主键ID
        public Int32 AutoID { get; set; }

        public decimal? ShiJiFangJia { get; set; }
        public decimal? BiaoZhunFangJia { get; set; }
        public string CustomerName { get; set; }
        public string ZhuCong { get; set; }
        public string RoomType { get; set; }
        public DateTime? OnBoardTime { get; set; }

        public DateTime? LeaveTime { get; set; }
        public string f_dm { get; set; }
        
    }

    public class YDFJInfo
    {      
        //房间类型
        public string FJType { get; set; }
        //数量
        public int SL { get; set; }
        //已占用数
        public Int32 OccupySL { get; set; }
        //可用数量
        public Int32 UsedSL { get; set; }
        //房间标准价格
        public decimal BiaoZhunFangJia { get; set; }

        public int lcID { get; set; }
        public int ldID { get; set; }
    }
    /// <summary>
    /// 预定收款单,退款单
    /// </summary>
    public class YDShouKuan
    {
        public int? shoukuanType { get; set; } 
        //订单GUID
        public string ydGuid { get; set; }
        //付款方式
        public string Fkfs { get; set; }
        //金额
        public decimal? jine { get; set; }
        //发生时间
        public DateTime? time { get; set; }
        //可用数量
        public string danjuhao { get; set; }
        //备注
        public string beizhu { get; set; }
        //相关名称
        public string relatedname { get; set; }
        //
        public decimal? zhangkuan { get; set; }

       //相关名称
        public string type { get; set; }  
    }
    public class YDFJSaveInfo
    {
        //顾客
        public string Customer { get; set; }
        //房间号
        public string f_dm { get; set; }

        //房间实际价格
        public decimal ShiJiFangJia { get; set; }

         //主从
        public string ZhuCong { get; set; }
         
        public int AutoID { get; set; }
        //房间类型
        public string JB { get; set; }
        
    }
    /// <summary>
    /// 预定房间具体信息，具体到每个房间，对应表Yd_Pf
    /// </summary>
    public class YDDetail
    {
        //所属预定单ID
        public System.Guid? YDOrderID { get; set; }
        //房号
        public string FH { get; set; }
        //级别
        public string JB { get; set; }
        //自动主键ID
        public Int32 AutoID { get; set; }
        public string YDNum { get; set; }
        
    }
    /// <summary>
    /// 预定单查看,用来打印
    /// </summary>
    public class PrintYD
    {
        //Customer客户
        public string Customer { get; set; }
        //Yder预订人
        public string Yder { get; set; }
        //YdTel 预订人电话
        public string YdTel { get; set; }
        //CustomerTel客人电话
        public string CustomerTel { get; set; }
        //入住日期
        public System.DateTime? OnBoardTime { get; set; }
        //离店日期
        public System.DateTime? LeaveTime { get; set; }
        //会员卡
        public string MemberCardNo { get; set; }
        //房号与级别
        public List<YDDetail> FJs{get;set;}
    }
    /// <summary>
    /// 用来封装筛选条件
    /// </summary>
    public class YDFilter
    {
        public string YDNum { get; set; }
        public string Yder { get; set; }
        public string YdTel { get; set; }
        public string Customer { get; set; }
        public string LX { get; set; }
        public string D_DW { get; set; }
        public string FH { get; set; }
        public string GJ { get; set; }
        public string Company { get; set; }
        public string MemberCardNo { get; set; }
        public string YDLB { get; set; }
        public DateTime DR { get; set; }
        public DateTime LR { get; set; }
        public DateTime YDR { get; set; }
    }
}
