using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.FrontDesk
{
    public class RoomStatus
    {
        public List<RoomStatusViewModel> getRooms(int lc, int ld)
        {
            //取消过期的预订单
            YDHelper helper = new YDHelper();
            helper.CancelOvertimeYD();

            List<RoomStatusViewModel> rooms = new List<RoomStatusViewModel>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                //房间信息
                //根据楼栋查询
                if (ld != 0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             where a.ldID == ld
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj

                             }).ToList();
                }
                //根据楼栋楼层查询
               if(lc!=0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             where a.lcID == lc 
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj
                             }).ToList();
                }
               //楼栋选择全部时  或者楼栋为空时
                if(ld==0&&lc==0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj
                             }).ToList();
                }
                //根据楼层查询  楼层  根据楼层的id  获得该楼层的名称   再根据名称 获得所有的楼层名称相同的id   再根据id查询
                if (ld == 0 && lc != 0)
                {
                    var lcname = (from a in _db.zd_lc where a.id == lc select a.lcName).ToList().FirstOrDefault();
                    var ldid = from a in _db.zd_lc where a.lcName == lcname select a.id;
                    foreach (var j in ldid)
                    {
                        rooms.AddRange((from a in _db.Zd_Fj
                                        join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                                        orderby a.f_fh descending
                                        where a.lcID == j
                                        select new RoomStatusViewModel
                                        {
                                            RoomID = a.f_fh,
                                            Status = a.f_ztmc,
                                            StatusColor = s.Color,
                                            JB = a.f_jb,
                                            DJ = a.f_dj
                                        }).ToList());
                    }

                }
                //预定信息
                //根据房间号码，得到预定单中的人名
                //一个房间不一定就只有一条预订信息，可为多个，得到最早的一个
                foreach (var r in rooms)
                {
                    var order = (from a in _db.Yd_Pf
                                 join d in _db.Yd_Dd on a.YDNum equals d.YDNum
                                 where a.f_fh == r.RoomID
                                 && d.Status==0
                                 orderby d.dr ascending
                                 select d).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        r.CustomerName = order.d_mc;
                        r.Tel = order.d_dh;
                        r.YDDR = order.dr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.dr);
                        r.YDLR = order.lr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.lr);
                        TimeSpan ts=Convert.ToDateTime(order.dr)-System.DateTime.Now;
                        r.days = ts.Days;
                    }
                }
                //入住信息,首页需要显示人名
                foreach (var r in rooms)
                {
                    var order = (from a in _db.H_RuzhuDetail
                                where a.FangJianHao == r.RoomID
                                && a.Status==0
                                select a).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        //余额=根据roomid找到H_Ruzhuorder表押金-消费表里的消费总数
                        var orderyue = (from a in _db.H_RuzhuOrder
                                        join b in _db.Cash_RunningDetails on a.OrderGuid equals b.OrderGuid
                                     where a.FangHao == r.RoomID
                                     && a.Status == 0
                                        select new 
                                        {
                                           a.YaJin,
                                           b.Price
                                        }).ToList();
                        decimal? cash=0,yajin=0;
                        foreach (var cashdetail in orderyue)
                        {
                            yajin = cashdetail.YaJin;
                            cash = cashdetail.Price;
                        }
                        r.LeftMoney =Convert.ToDecimal(yajin-cash);
                        r.days = order.LeaveTime.DayOfYear - order.ArriveTime.DayOfYear;
                        r.CustomerName = order.XingMing;
                        r.JG = order.ShijiFangjia.ToString();
                    }
                }

                //财务信息
            }
            catch (Exception e)
            {
            }
            return rooms;
        }

        //public List<RoomStatusViewModel> getRooms()
        //{
        //    //取消过期的预订单
        //    YDHelper helper = new YDHelper();
        //    helper.CancelOvertimeYD();

        //    List<RoomStatusViewModel> rooms = new List<RoomStatusViewModel>();
        //    try
        //    {
        //        HotelDBEntities _db = new HotelDBEntities();
        //        //房间信息
        //        rooms = (from a in _db.Zd_Fj
        //                 join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
        //                 orderby a.f_fh descending
        //                 select new RoomStatusViewModel
        //                 {
        //                     RoomID = a.f_fh,
        //                     Status = a.f_ztmc,
        //                     StatusColor = s.Color,
        //                     JB = a.f_jb
        //                 }).ToList();
        //        //预定信息
        //        //根据房间号码，得到预定单中的人名
        //        //一个房间不一定就只有一条预订信息，可为多个，得到最早的一个
        //        foreach (var r in rooms)
        //        {
        //            var order = (from a in _db.Yd_Pf
        //                         join d in _db.Yd_Dd on a.YDID equals d.YDID
        //                         where a.f_fh == r.RoomID
        //                         && d.Status == 0
        //                         orderby d.dr ascending
        //                         select d).ToList().FirstOrDefault();
        //            if (order != null)
        //            {
        //                r.CustomerName = order.d_mc;
        //                r.Tel = order.d_dh;
        //                r.YDDR = order.dr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.dr);
        //                r.YDLR = order.lr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.lr);
        //                TimeSpan ts = order.dr - System.DateTime.Now;
        //                r.days = ts.Days;
        //            }
        //        }
        //        //入住信息,首页需要显示人名
        //        foreach (var r in rooms)
        //        {
        //            var order = (from a in _db.H_RuzhuDetail
        //                         where a.FangJianHao == r.RoomID
        //                         && a.Status == 0
        //                         select a).ToList().FirstOrDefault();
        //            if (order != null)
        //            {
        //                //余额=根据roomid找到H_Ruzhuorder表押金-消费表里的消费总数
        //                var orderyue = (from a in _db.H_RuzhuOrder
        //                                join b in _db.Cash_RunningDetails on a.OrderGuid equals b.OrderGuid
        //                                where a.FangHao == r.RoomID
        //                                && a.Status == 0
        //                                select new
        //                                {
        //                                    a.YaJin,
        //                                    b.Price
        //                                }).ToList();
        //                decimal? cash = 0, yajin = 0;
        //                foreach (var cashdetail in orderyue)
        //                {
        //                    yajin = cashdetail.YaJin;
        //                    cash = cashdetail.Price;
        //                }
        //                r.LeftMoney = Convert.ToDecimal(yajin - cash);
        //                r.days = order.LeaveTime.DayOfYear - order.ArriveTime.DayOfYear;
        //                r.CustomerName = order.XingMing;
        //                r.JG = order.ShijiFangjia.ToString();
        //            }
        //        }

        //        //财务信息
        //    }
        //    catch (Exception e)
        //    {
        //    }
        //    return rooms;
        //}

        public List<RoomStatusViewModel> getRooms(string ctgyid, int lc, int ld)
        {
            //取消过期的预订单
            //YDHelper helper = new YDHelper();
            //helper.CancelOvertimeYD();

            List<RoomStatusViewModel> rooms = new List<RoomStatusViewModel>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                //房间信息
                 //根据楼栋查询
                if (ld != 0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             where a.f_dm == ctgyid && a.ldID == ld
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj
                             }).ToList();

                }
                //根据楼栋楼层查询 
                if (lc != 0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             where a.f_dm == ctgyid && a.lcID == lc
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj
                             }).ToList();

                }
                //楼栋选择全部时  或者楼栋为空时
                if (ld == 0&&lc==0)
                {
                    rooms = (from a in _db.Zd_Fj
                             join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                             orderby a.f_fh descending
                             where a.f_dm == ctgyid 
                             select new RoomStatusViewModel
                             {
                                 RoomID = a.f_fh,
                                 Status = a.f_ztmc,
                                 StatusColor = s.Color,
                                 JB = a.f_jb,
                                 DJ = a.f_dj
                             }).ToList();

                }
                //根据楼层查询  楼层  根据楼层的id  获得该楼层的名称   再根据名称 获得所有的楼层名称相同的id   再根据id查询
                if (ld == 0 && lc != 0)
                {
                    var lcname = (from a in _db.zd_lc where a.id == lc select a.lcName).ToList().FirstOrDefault();
                    var ldid = from a in _db.zd_lc where a.lcName == lcname select a.id;
                    foreach (var j in ldid)
                    {
                        rooms.AddRange((from a in _db.Zd_Fj
                                        join s in _db.zd_FjZt on a.f_ztmc equals s.FjZt
                                        orderby a.f_fh descending
                                        where a.f_dm == ctgyid && a.lcID == j
                                        select new RoomStatusViewModel
                                        {
                                            RoomID = a.f_fh,
                                            Status = a.f_ztmc,
                                            StatusColor = s.Color,
                                            JB = a.f_jb,
                                            DJ = a.f_dj
                                        }).ToList());

                    }

                }         
                //预定信息
                //根据房间号码，得到预定单中的人名
                foreach (var r in rooms)
                {
                    var order = (from a in _db.Yd_Pf
                                 join d in _db.Yd_Dd on a.YDNum equals d.YDNum
                                 where a.f_fh == r.RoomID
                                 && d.Status == 0
                                 select d).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        r.CustomerName = order.d_mc;
                        r.Tel = order.d_dh;
                        r.YDDR = order.dr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.dr);
                        r.YDLR = order.lr == null ? new DateTime(1900, 01, 01) : Convert.ToDateTime(order.lr);
                    }
                }
                //入住信息
                foreach (var r in rooms)
                {
                    var fj = (from a in _db.H_RuzhuDetail
                                 where a.FangJianHao == r.RoomID
                                 && a.Status==0
                                 select a).ToList().FirstOrDefault();
                    if (fj != null)
                    {
                        r.CustomerName = fj.XingMing;
                        r.JG = fj.ShijiFangjia.ToString();
                    }
                    var order = (from a in _db.H_RuzhuDetail
                                 join b in _db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                                 where a.FangJianHao == r.RoomID
                                  && a.Status == 0
                                 select b).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        r.DR = order.DaodianTime;
                        r.LR = order.LidianTime;
                    }
                }
                //财务信息
            }
            catch (Exception e)
            {
                    
            }
            return rooms;
        }
        public List<RoomCategory> getRoomCategory()
        {
            List<RoomCategory> result = new List<RoomCategory>();
            try
            {
                HotelDBEntities _db = new HotelDBEntities();
                //房间类别信息
                var roomctgrpool = from a in _db.zd_jb
                               select a;

                foreach (var ctgr in roomctgrpool)
                {
                    RoomCategory temp = new RoomCategory();
                    temp.CategoryID = ctgr.AutoID.ToString();
                    temp.CategoryName = ctgr.kfjb;
                    result.Add(temp);
                }
            }
            catch(Exception e)
            { 
            }
            return result;
        }
        public List<StatusSummer> getStatusSummer()
        {
            List<StatusSummer> result = new List<StatusSummer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.zd_FjZt
                            select a;
                foreach (var q in query)
                {
                    var sl = from a in db.Zd_Fj
                             where a.f_ztmc == q.FjZt
                             select a;
                    StatusSummer temp = new StatusSummer();
                    temp.Status = q.FjZt;
                    temp.SL = sl.ToList().Count;
                    temp.ColorStr = q.Color;
                    result.Add(temp);
                }
            }
            return result;
        }
        //更新房间状态信息
        public string SetRoomStatus(string fh,string status)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.Zd_Fj
                            where a.f_fh == fh
                            select a).SingleOrDefault();
                if (temp != null)
                {
                    temp.f_ztmc = status;
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

    }
    public class RoomStatusViewModel
    {
        public string RoomID { get; set; }
        public string CustomerName { get; set; }
        public string Tel { get; set; }
        public decimal LeftMoney { get; set; }
        public string Status { get; set; }
        public string StatusColor { get; set; }
        public string JB { get; set; }
        public DateTime YDDR { get; set; }
        public DateTime YDLR { get; set; }
        public DateTime DR { get; set; }
        public DateTime LR { get; set; }
        public string JG { get; set; }
        public int days { get; set; }
        //单价
        public Decimal? DJ { get; set; }
    }
    public class RoomCategory
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class StatusSummer
    {
        public string Status { get; set; }
        public int SL { get; set; }
        public string ColorStr { get; set; }
    }
}
