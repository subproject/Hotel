using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.Setting;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace HotelLogic.FrontDesk
{
    /// <summary>
    /// 入住部分业务逻辑
    /// 入住单,入住详细信息,入住随客
    /// 入住单作为记账主账单
    /// </summary>
    public class OrdersHelper
    {
        #region 生成入住单 三个重载函数
        //生成一个单独的order，即入住帐单
        public string CreateOrder(OrderViewModel order)
        {
            string result = "-1";
            using (TransactionScope scope = new TransactionScope())
            {
                using (HotelDBEntities db = new HotelDBEntities())
                {
                    try
                    {
                        H_RuzhuOrder temp = new H_RuzhuOrder();
                        DateTime now = System.DateTime.Now;
                        Guid guid = Guid.NewGuid();
                        temp.AutoID = now.Year.ToString().Substring(2, 2) + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                        temp.ChangTu = order.ChangTu;
                        temp.ShiHua = order.ShiHua;
                        temp.ChangBao = order.ChangBao;
                        temp.ZhongDian = order.ZhongDian;
                        temp.DaodianTime = order.DaodianTime;
                        temp.LidianTime = order.LidianTime;
                        temp.JiaoxingFuwu = order.JiaoxingFuwu;
                        temp.FukuanFangshi = order.FukuanFangshi;
                        temp.YaJin = order.YaJin;
                        temp.XieyiDanwei = order.XieyiDanwei;
                        temp.TeQuanRen = order.TeQuanRen;
                        temp.ZheKouLv = order.ZheKouLv;
                        temp.ShijiFangjia = order.ShijiFangjia;
                        temp.BeiZhu = order.BeiZhu;
                        temp.BaoMi = order.BaoMi;
                        temp.ShougongDanhao = order.ShougongDanhao;
                        temp.KerenLeibie = order.KerenLeibie;
                        temp.TuanDui = order.TuanDui;
                        temp.ZhengjianLeibie = order.ZhengjianLeibie;
                        temp.DianHua = order.DianHua;
                        temp.FangjianLeixing = order.FangjianLeixing;
                        temp.XingBie = order.XingBie;
                        temp.HuiYuanKa = order.HuiYuanKa;
                        temp.JiFen = order.JiFen;
                        temp.XingMing = order.XingMing;
                        temp.ZhengJianHao = order.ZhengJianHao;
                        temp.DiZhi = order.DiZhi;
                        temp.BiaozhunFangjia = order.BiaozhunFangjia;
                        temp.OrderGuid = guid;
                        temp.Status = 0;
                        temp.GuoJi = order.GuoJi;
                        temp.XiaoShouYuan = order.XiaoShouYuan;
                        db.H_RuzhuOrder.AddObject(temp);
                        db.SaveChanges();
                        //改变为入住状态
                        scope.Complete();
                        result = "0";
                    }
                    catch (Exception e)
                    {
                        result = "-1";
                    }
                }
            }
            return result;
        }

        //生成一个连房的入住帐单
        //attaches means入住人与房间号的信息
        public string CreateOrder(OrderViewModel order, List<OrderAttachModel> attaches)
        {
            string result = "-1";
            using (TransactionScope scope = new TransactionScope())
            {
                using (HotelDBEntities db = new HotelDBEntities())
                {
                    try
                    {
                        //入住单信息
                        H_RuzhuOrder temp = new H_RuzhuOrder();
                        DateTime now = System.DateTime.Now;
                        Guid guid = Guid.NewGuid();
                        temp.AutoID = now.Year.ToString().Substring(2, 2) + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                        temp.ChangTu = order.ChangTu;
                        temp.ShiHua = order.ShiHua;
                        temp.ChangBao = order.ChangBao;
                        temp.ZhongDian = order.ZhongDian;
                        temp.DaodianTime = order.DaodianTime;
                        temp.LidianTime = order.LidianTime;
                        temp.JiaoxingFuwu = order.JiaoxingFuwu;
                        temp.FukuanFangshi = order.FukuanFangshi;
                        temp.YaJin = order.YaJin;
                        temp.XieyiDanwei = order.XieyiDanwei;
                        temp.TeQuanRen = order.TeQuanRen;
                        temp.ZheKouLv = order.ZheKouLv;
                        temp.ShijiFangjia = order.ShijiFangjia;
                        temp.BeiZhu = order.BeiZhu;
                        temp.BaoMi = order.BaoMi;
                        temp.ShougongDanhao = order.ShougongDanhao;
                        temp.KerenLeibie = order.KerenLeibie;
                        temp.TuanDui = order.TuanDui;
                        temp.ZhengjianLeibie = order.ZhengjianLeibie;
                        temp.DianHua = order.DianHua;
                        temp.FangjianLeixing = order.FangjianLeixing;
                        temp.XingBie = order.XingBie;
                        temp.HuiYuanKa = order.HuiYuanKa;
                        temp.JiFen = order.JiFen;
                        temp.XingMing = order.XingMing;
                        temp.ZhengJianHao = order.ZhengJianHao;
                        temp.DiZhi = order.DiZhi;
                        temp.BiaozhunFangjia = order.BiaozhunFangjia;
                        temp.OrderGuid = guid;
                        temp.FangHao = order.FangHao;
                        temp.Status = 0;
                        db.H_RuzhuOrder.AddObject(temp);

                        //attaches信息
                        foreach (var att in attaches)
                        {
                            H_RuzhuDetail attach = new H_RuzhuDetail();
                            attach.AutoID = att.AutoID;
                            attach.FangJianHao = att.FangJianHao;
                            attach.OrderGuid = guid;
                            attach.OrderID = att.OrderID;
                            attach.ShijiFangjia = att.ShijiFangjia;
                            attach.XingBie = att.XingBie;
                            attach.XingMing = att.XingMing;
                            attach.YuanFangJia = att.YuanFangJia;
                            attach.ZheKouLv = att.ZheKouLv;
                            attach.ZhengjianDizhi = att.ZhengjianDizhi;
                            attach.ZhengjianHaoma = att.ZhengjianHaoma;
                            attach.ZhengjianLeixing = att.ZhengjianLeixing;
                            attach.Status = 0;
                            attach.ArriveTime = att.ArriveTime;
                            attach.LeaveTime = att.LeaveTime;
                            db.H_RuzhuDetail.AddObject(attach);
                        }
                        db.SaveChanges();

                        //改变状态为在客
                        foreach (var att in attaches)
                        {
                            RoomStatus statushlp = new RoomStatus();
                            statushlp.SetRoomStatus(att.FangJianHao, "在客");
                        }
                        result = "0";
                    }
                    catch (Exception e)
                    {
                        result = "-1";
                        LogModel data = new LogModel();
                        data.Info = "创建入住单失败！OrderHelper.CreateOrder(p,p)";
                        data.Exception = e.ToString();
                        LoggingHelper.Instance.LogInfo(data);
                    }
                }
            }
            return result;
        }

        public static OrderViewModel GetKFDJOrder(string FH)
        {
            OrderViewModel temp = new OrderViewModel();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var order = (from a in db.H_RuzhuOrder
                             where a.FangHao == FH
                             select a).ToList().FirstOrDefault();
                if (order != null)
                {
                    temp.ChangTu = order.ChangTu;
                    temp.ShiHua = order.ShiHua;
                    temp.ChangBao = order.ChangBao;
                    temp.ZhongDian = order.ZhongDian;
                    temp.DaodianTime = order.DaodianTime;
                    temp.LidianTime = order.LidianTime;
                    temp.JiaoxingFuwu = order.JiaoxingFuwu;
                    temp.YaJin = order.YaJin;
                    temp.XieyiDanwei = order.XieyiDanwei;
                    temp.TeQuanRen = order.TeQuanRen;
                    temp.ZheKouLv = order.ZheKouLv;
                    temp.ShijiFangjia = order.ShijiFangjia;
                    temp.BeiZhu = order.BeiZhu;
                    temp.BaoMi = order.BaoMi;
                    temp.ShougongDanhao = order.ShougongDanhao;
                    temp.KerenLeibie = order.KerenLeibie;
                    temp.TuanDui = order.TuanDui;
                    temp.ZhengjianLeibie = order.ZhengjianLeibie;
                    temp.DianHua = order.DianHua;
                    temp.FangjianLeixing = order.FangjianLeixing;// order.FangjianLeixing;
                    temp.XingBie = order.XingBie;
                    temp.HuiYuanKa = order.HuiYuanKa;
                    temp.JiFen = order.JiFen;
                    temp.XingMing = order.XingMing;
                    temp.ZhengJianHao = order.ZhengJianHao;
                    temp.DiZhi = order.DiZhi;
                    temp.BiaozhunFangjia = order.BiaozhunFangjia;// order.BiaozhunFangjia;
                    temp.OrderGuid = order.OrderGuid;
                    temp.FangHao = order.FangHao;// order.FangHao;
                    //temp.Status = order.Status;
                    temp.FukuanFangshi = order.FukuanFangshi;
                    temp.GuoJi = order.GuoJi;
                    temp.XiaoShouYuan = order.XiaoShouYuan;
                    temp.CaoZuoYuan = order.CaoZuoYuan;
                }
            }
            return temp;
        }
        public static List<OrderAttachModel> GetKFDJDetail(string FH)
        {
            List<OrderAttachModel> templist = new List<OrderAttachModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var atts = (from a in db.H_RuzhuDetail
                            join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                            where b.FangHao == FH
                            orderby a.AutoID ascending
                            select a).ToList();
                foreach (var att in atts)
                {
                    OrderAttachModel attach = new OrderAttachModel();
                    attach.FangJianHao = att.FangJianHao;
                    attach.OrderGuid = att.OrderGuid;
                    attach.OrderID = att.OrderID;
                    attach.ShijiFangjia = att.ShijiFangjia;
                    attach.XingBie = att.XingBie;
                    attach.XingMing = att.XingMing;
                    attach.YuanFangJia = att.YuanFangJia;
                    //attach.ZhuCong = att.ZhuCong;
                    attach.ZheKouLv = Convert.ToSingle(att.ZheKouLv);
                    attach.ZhengjianDizhi = att.ZhengjianDizhi;
                    attach.ZhengjianHaoma = att.ZhengjianHaoma;
                    attach.ZhengjianLeixing = att.ZhengjianLeixing;
                    attach.ArriveTime = att.ArriveTime;
                    attach.LeaveTime = att.LeaveTime;
                    attach.JBName = att.JiBie;
                    // attach.Status = att.Status;
                    //attach.RuZhuLeiXing = att.RuZhuLeiXing;

                    templist.Add(attach);
                }
            }
            return templist;
        }

        public static List<SuikeModel> GetKFDJSuiKe(string FH)
        {
            List<SuikeModel> templist = new List<SuikeModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var skl = (from a in db.H_RuzhuSuike
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           where b.FangHao == FH
                           select a).ToList();
                foreach (var sk in skl)
                {
                    SuikeModel suike = new SuikeModel();
                    suike.CarNum = sk.CarNum;
                    suike.Address = sk.DiZhi;
                    suike.Card = sk.ShenfenZheng;
                    suike.XingBie = sk.XingBie;
                    suike.XingMing = sk.XingMing;
                    suike.BeiZhu = sk.Remark;
                    templist.Add(suike);
                }
            }
            return templist;
        }

        public string ModifyOrder(OrderViewModel order, List<OrderAttachModel> attaches, List<SuikeModel> skl)
        {
            string result = "-1";
            using (TransactionScope tscope = new TransactionScope())
            {
                using (HotelDBEntities db = new HotelDBEntities())
                {
                    try
                    {
                        //方法1：这里根据传进来的值修改数据库里的值 
                        //H_RuzhuOrder表里根据房号order.FangHao和状态state=0 查出OrderGuid，从入住单中查出记录，然后把order赋值到数据库
                        //里,OrderGuid再去查H_RuzhuDetail表里的记录，然后根据房号相同去修改相应的记录，根据OrderGuid去查                                 
                        //Cash_RunningDetails，查出来只有一条记录，修改下
                        //根据OrderGuid去查H_RuzhuSuike表记录，可以查出来的删了然后再插进去。

                        //方法2：查出OrderGuid后把H_RuzhuOrder，H_RuzhuDetail，Cash_RunningDetails，H_RuzhuSuike表相关记录删了再创建
                        //创建的代码下面都有了，删除原来的就行

                        //OrderViewModel temps = OrdersHelper.GetKFDJOrder(order.FangHao);
                        //order.OrderGuid = temps.OrderGuid;
                        var t = db.H_RuzhuOrder.FirstOrDefault(a => a.OrderGuid == order.OrderGuid);
                        if (t != null)
                        {
                            db.H_RuzhuOrder.DeleteObject(t);
                            db.SaveChanges();
                        }
                        var detail = db.H_RuzhuDetail.FirstOrDefault(a => a.OrderGuid == order.OrderGuid);
                        if (detail != null)
                        {
                            db.H_RuzhuDetail.DeleteObject(detail);
                            db.SaveChanges();
                        }
                        var cash = db.Cash_RunningDetails.FirstOrDefault(a => a.OrderGuid == order.OrderGuid);
                        if (cash != null)
                        {
                            db.Cash_RunningDetails.DeleteObject(cash);
                            db.SaveChanges();
                        }
                        var sui = db.H_RuzhuSuike.FirstOrDefault(a => a.OrderGuid == order.OrderGuid);
                        if (sui != null)
                        {
                            db.H_RuzhuSuike.DeleteObject(sui);
                            db.SaveChanges();
                        }
                        H_RuzhuOrder temp = new H_RuzhuOrder();
                        DateTime now = System.DateTime.Now;
                        Guid guid = Guid.NewGuid();
                        temp.AutoID = now.Year.ToString().Substring(2, 2) + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                        temp.ChangTu = order.ChangTu;
                        temp.ShiHua = order.ShiHua;
                        temp.ChangBao = order.ChangBao;
                        temp.ZhongDian = order.ZhongDian;
                        temp.DaodianTime = order.DaodianTime;
                        temp.LidianTime = order.LidianTime;
                        temp.JiaoxingFuwu = order.JiaoxingFuwu;
                        temp.YaJin = order.YaJin;
                        temp.XieyiDanwei = order.XieyiDanwei;
                        temp.TeQuanRen = order.TeQuanRen;
                        temp.ZheKouLv = order.ZheKouLv;
                        temp.ShijiFangjia = order.ShijiFangjia;
                        temp.BeiZhu = order.BeiZhu;
                        temp.BaoMi = order.BaoMi;
                        temp.ShougongDanhao = order.ShougongDanhao;
                        temp.KerenLeibie = order.KerenLeibie;
                        temp.TuanDui = order.TuanDui;
                        temp.ZhengjianLeibie = order.ZhengjianLeibie;
                        temp.DianHua = order.DianHua;
                        temp.FangjianLeixing = attaches[0].JBName;// order.FangjianLeixing;
                        temp.XingBie = order.XingBie;
                        temp.HuiYuanKa = order.HuiYuanKa;
                        temp.JiFen = order.JiFen;
                        temp.XingMing = order.XingMing;
                        temp.ZhengJianHao = order.ZhengJianHao;
                        temp.DiZhi = order.DiZhi;
                        temp.BiaozhunFangjia = attaches[0].YuanFangJia.ToString();// order.BiaozhunFangjia;
                        temp.OrderGuid = guid;
                        temp.FangHao = attaches[0].FangJianHao;// order.FangHao;
                        temp.Status = 0;
                        temp.FukuanFangshi = order.FukuanFangshi;
                        temp.GuoJi = order.GuoJi;
                        temp.XiaoShouYuan = order.XiaoShouYuan;
                        temp.CaoZuoYuan = order.CaoZuoYuan;
                        db.H_RuzhuOrder.AddObject(temp);

                        //attaches信息
                        //每个房间应该生成一条房费信息，即入住则产生房费
                        //第一个房间的实际房价，折扣率，身份证，姓名为order
                        //根据OrderGuid
                        int index = 0;
                        foreach (var att in attaches)
                        {
                            H_RuzhuDetail attach = new H_RuzhuDetail();
                            // attach.AutoID = att.AutoID;
                            attach.FangJianHao = att.FangJianHao;
                            attach.OrderGuid = guid;
                            attach.OrderID = temp.AutoID;// att.OrderID;
                            //attach.ShijiFangjia = att.ShijiFangjia;
                            if (att.ShijiFangjia == null)
                            {
                                attach.ShijiFangjia = order.ShijiFangjia;
                            }
                            else
                            {
                                attach.ShijiFangjia = att.ShijiFangjia;
                            }

                            attach.XingBie = order.XingBie;
                            if (String.IsNullOrEmpty(att.XingMing))
                            {
                                attach.XingMing = order.XingMing;
                            }
                            else
                            {
                                attach.XingMing = att.XingMing;
                            }
                            attach.YuanFangJia = att.YuanFangJia;

                            if (index == 0)//第一间房折扣
                            {
                                attach.ZhuCong = "主";
                                attach.ZheKouLv = order.ZheKouLv;
                                index = 1;
                            }
                            else
                            {
                                attach.ZhuCong = "从";
                                attach.ZheKouLv = att.ZheKouLv;
                            }

                            if (String.IsNullOrEmpty(att.ZhengjianHaoma))
                            {
                                if (!String.IsNullOrEmpty(order.ZhengJianHao))
                                {
                                    attach.ZhengjianDizhi = HotelLogic.Member.MemberHelper.getAddress(order.ZhengJianHao.Substring(0, 6));
                                }
                            }
                            else
                            {
                                attach.ZhengjianDizhi = HotelLogic.Member.MemberHelper.getAddress(att.ZhengjianHaoma.Substring(0, 6));//att.ZhengjianDizhi;
                            }
                            if (String.IsNullOrEmpty(att.ZhengjianHaoma))
                            {
                                attach.ZhengjianHaoma = order.ZhengJianHao;
                            }
                            else
                            {
                                attach.ZhengjianHaoma = att.ZhengjianHaoma;
                            }
                            //attach.ZhengjianHaoma = order.ZhengJianHao;// att.ZhengjianHaoma;
                            //attach.ZhengjianLeixing = order.ZhengjianLeibie;// att.ZhengjianLeixing;
                            if (String.IsNullOrEmpty(att.ZhengjianLeixing))
                            {
                                attach.ZhengjianLeixing = order.ZhengjianLeibie;
                            }
                            else
                            {
                                attach.ZhengjianLeixing = att.ZhengjianLeixing;
                            }
                            attach.ArriveTime = order.DaodianTime;
                            attach.LeaveTime = order.LidianTime;
                            attach.JiBie = att.JBName;
                            attach.Status = 0;
                            if (order.ChangBao == true)
                            {
                                attach.RuZhuLeiXing = "长包";
                            }
                            else if (order.ZhongDian == true)
                            {
                                attach.RuZhuLeiXing = "终点";
                            }
                            else if (att.ShijiFangjia <= 0)//免费房
                            {
                                attach.RuZhuLeiXing = "免费";
                            }
                            else
                            {
                                attach.RuZhuLeiXing = "正常";
                            }
                            db.H_RuzhuDetail.AddObject(attach);

                            //财务信息，根据fee.OrderGuid查出财务记录，然后赋值
                            Cash_RunningDetails fee = new Cash_RunningDetails();
                            fee.CustomerName = attach.XingMing;
                            fee.RoomNo = att.FangJianHao;
                            //fee.RunningTime = DateTime.Now;
                            fee.Price = attach.ShijiFangjia;
                            fee.OrderGuid = guid;
                            fee.Remark = "房费";
                            //0 means 待付款 
                            fee.Status = "未结";
                            db.Cash_RunningDetails.AddObject(fee);
                        }
                        //skl信息
                        foreach (var sk in skl)
                        {
                            H_RuzhuSuike suike = new H_RuzhuSuike();
                            suike.CarNum = sk.CarNum;
                            suike.DiZhi = sk.Address;
                            suike.OrderGuid = guid;
                            suike.ShenfenZheng = sk.Card;
                            suike.XingBie = sk.XingBie;
                            suike.XingMing = sk.XingMing;
                            suike.Remark = sk.BeiZhu;
                            // suike.FangHao = sk.BeiZhu;
                            suike.Status = 0;
                            db.H_RuzhuSuike.AddObject(suike);
                        }
                        db.SaveChanges();

                        //改变状态为在客
                        foreach (var att in attaches)
                        {
                            RoomStatus statushlp = new RoomStatus();
                            statushlp.SetRoomStatus(att.FangJianHao, "在客");
                        }
                        result = temp.AutoID;// "0";
                        tscope.Complete();
                    }
                    catch (Exception e)
                    {
                        result = e.ToString() + order.DaodianTime + " " + order.LidianTime + " " + order.JiaoxingFuwu;
                    }
                }

            }
            return result;
        }

        public string CreateYdRuzhuOrder(OrderViewModel order, List<OrderAttachModel> attaches, List<SuikeModel> skl, string ydnum)
        {
            string result = "-1";
            result = CreateOrder(order, attaches, skl);
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //将订单转为过期
                //当前房号的有效预定信息
                var Ydorder = (from a in db.Yd_Dd
                               where a.YDNum == ydnum
                               && (a.Status == 0 || a.Status == 2)
                               select a).SingleOrDefault();
                //order status change to 1, obsolete
                if (Ydorder != null)
                {
                    Ydorder.Status = 3;   //3表示已经入住                
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

            }

            return result;

        }
        //生成一个连房的入住帐单
        //attaches means入住人与房间号的信息
        //skl means 随客人信息
        public string CreateOrder(OrderViewModel order, List<OrderAttachModel> attaches, List<SuikeModel> skl)
        {
            string result = "-1";
            using (TransactionScope scope = new TransactionScope())
            {
                using (HotelDBEntities db = new HotelDBEntities())
                {

                    try
                    {
                        //入住单信息
                        H_RuzhuOrder temp = new H_RuzhuOrder();
                        DateTime now = System.DateTime.Now;
                        Guid guid = Guid.NewGuid();
                        temp.AutoID = now.Year.ToString().Substring(2, 2) + now.Month.ToString() + now.Day.ToString() + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();
                        temp.ChangTu = order.ChangTu;
                        temp.ShiHua = order.ShiHua;
                        temp.ChangBao = order.ChangBao;
                        temp.ZhongDian = order.ZhongDian;
                        temp.DaodianTime = order.DaodianTime;
                        temp.LidianTime = order.LidianTime;
                        temp.JiaoxingFuwu = order.JiaoxingFuwu;
                        temp.YaJin = order.YaJin;
                        temp.XieyiDanwei = order.XieyiDanwei;
                        temp.TeQuanRen = order.TeQuanRen;
                        temp.ZheKouLv = order.ZheKouLv;
                        temp.ShijiFangjia = order.ShijiFangjia;
                        temp.BeiZhu = order.BeiZhu;
                        temp.BaoMi = order.BaoMi;
                        temp.ShougongDanhao = order.ShougongDanhao;
                        temp.KerenLeibie = order.KerenLeibie;
                        temp.TuanDui = order.TuanDui;
                        temp.ZhengjianLeibie = order.ZhengjianLeibie;
                        temp.DianHua = order.DianHua;
                        temp.FangjianLeixing = attaches[0].JBName;// order.FangjianLeixing;
                        temp.XingBie = order.XingBie;
                        temp.HuiYuanKa = order.HuiYuanKa;
                        temp.JiFen = order.JiFen;
                        temp.XingMing = order.XingMing;
                        temp.ZhengJianHao = order.ZhengJianHao;
                        temp.DiZhi = order.DiZhi;
                        temp.BiaozhunFangjia = attaches[0].YuanFangJia.ToString();// order.BiaozhunFangjia;
                        temp.OrderGuid = guid;
                        temp.FangHao = attaches[0].FangJianHao;// order.FangHao;
                        temp.Status = 0;
                        temp.FukuanFangshi = order.FukuanFangshi;
                        temp.GuoJi = order.GuoJi;
                        temp.XiaoShouYuan = order.XiaoShouYuan;
                        temp.CaoZuoYuan = order.CaoZuoYuan;
                        db.H_RuzhuOrder.AddObject(temp);

                        //attaches信息
                        //每个房间应该生成一条房费信息，即入住则产生房费
                        //第一个房间的实际房价，折扣率，身份证，姓名为order
                        int index = 0;
                        foreach (var att in attaches)
                        {
                            H_RuzhuDetail attach = new H_RuzhuDetail();
                            // attach.AutoID = att.AutoID;
                            attach.FangJianHao = att.FangJianHao;
                            attach.OrderGuid = guid;
                            attach.OrderID = temp.AutoID;// att.OrderID;
                            //attach.ShijiFangjia = att.ShijiFangjia;
                            if (att.ShijiFangjia == null)
                            {
                                attach.ShijiFangjia = order.ShijiFangjia;
                            }
                            else
                            {
                                attach.ShijiFangjia = att.ShijiFangjia;
                            }

                            attach.XingBie = order.XingBie;
                            if (String.IsNullOrEmpty(att.XingMing))
                            {
                                attach.XingMing = order.XingMing;
                            }
                            else
                            {
                                attach.XingMing = att.XingMing;
                            }
                            attach.YuanFangJia = att.YuanFangJia;

                            if (index == 0)//第一间房折扣
                            {
                                attach.ZhuCong = "主";
                                attach.ZheKouLv = order.ZheKouLv;
                                index = 1;
                            }
                            else
                            {
                                attach.ZhuCong = "从";
                                attach.ZheKouLv = att.ZheKouLv;
                            }

                            if (String.IsNullOrEmpty(att.ZhengjianHaoma))
                            {
                                if (!String.IsNullOrEmpty(order.ZhengJianHao))
                                {
                                    attach.ZhengjianDizhi = HotelLogic.Member.MemberHelper.getAddress(order.ZhengJianHao.Substring(0, 6));
                                }
                            }
                            else
                            {
                                attach.ZhengjianDizhi = HotelLogic.Member.MemberHelper.getAddress(att.ZhengjianHaoma.Substring(0, 6));//att.ZhengjianDizhi;
                            }
                            if (String.IsNullOrEmpty(att.ZhengjianHaoma))
                            {
                                attach.ZhengjianHaoma = order.ZhengJianHao;
                            }
                            else
                            {
                                attach.ZhengjianHaoma = att.ZhengjianHaoma;
                            }
                            //attach.ZhengjianHaoma = order.ZhengJianHao;// att.ZhengjianHaoma;
                            //attach.ZhengjianLeixing = order.ZhengjianLeibie;// att.ZhengjianLeixing;
                            if (String.IsNullOrEmpty(att.ZhengjianLeixing))
                            {
                                attach.ZhengjianLeixing = order.ZhengjianLeibie;
                            }
                            else
                            {
                                attach.ZhengjianLeixing = att.ZhengjianLeixing;
                            }
                            attach.ArriveTime = order.DaodianTime;
                            attach.LeaveTime = order.LidianTime;
                            attach.JiBie = att.JBName;
                            attach.Status = 0;
                            if (order.ChangBao == true)
                            {
                                attach.RuZhuLeiXing = "长包";
                            }
                            else if (order.ZhongDian == true)
                            {
                                attach.RuZhuLeiXing = "终点";
                            }
                            else if (att.ShijiFangjia <= 0)//免费房
                            {
                                attach.RuZhuLeiXing = "免费";
                            }
                            else
                            {
                                attach.RuZhuLeiXing = "正常";
                            }
                            db.H_RuzhuDetail.AddObject(attach);

                            //财务信息，产生一套房费待付
                            Cash_RunningDetails fee = new Cash_RunningDetails();
                            fee.AutoID = Guid.NewGuid();
                            fee.CustomerName = attach.XingMing;
                            fee.RoomNo = att.FangJianHao;
                            fee.RunningTime = DateTime.Now;
                            fee.Price = attach.ShijiFangjia;
                            fee.OrderGuid = guid;
                            fee.Remark = "房费";
                            fee.KM = "房费";
                            //0 means 待付款 
                            fee.Status = "未结";
                            db.Cash_RunningDetails.AddObject(fee);
                        }
                        //skl信息
                        foreach (var sk in skl)
                        {
                            H_RuzhuSuike suike = new H_RuzhuSuike();
                            suike.CarNum = sk.CarNum;
                            suike.DiZhi = sk.Address;
                            suike.OrderGuid = guid;
                            suike.ShenfenZheng = sk.Card;
                            suike.XingBie = sk.XingBie;
                            suike.XingMing = sk.XingMing;
                            suike.Remark = sk.BeiZhu;
                            // suike.FangHao = sk.BeiZhu;
                            suike.Status = 0;
                            db.H_RuzhuSuike.AddObject(suike);
                        }
                        db.SaveChanges();

                        //改变状态为在客
                        foreach (var att in attaches)
                        {
                            RoomStatus statushlp = new RoomStatus();
                            statushlp.SetRoomStatus(att.FangJianHao, "在客");
                        }
                        result = temp.AutoID;// "0";
                        scope.Complete();
                    }
                    catch (Exception e)
                    {
                        result = e.ToString() + order.DaodianTime + " " + order.LidianTime + " " + order.JiaoxingFuwu;
                    }
                }
            }

            return result;
        }
        #endregion

        //加开房间,向已有帐号中添加attach信息
        public string AppendRoom(System.Guid guid, List<OrderAttachModel> attaches)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //获取帐单
                var query = (from a in db.H_RuzhuOrder
                             where a.OrderGuid == guid
                             select a).SingleOrDefault();
                if (query != null)
                {
                    //根据帐单id添加attach
                    foreach (var att in attaches)
                    {
                        H_RuzhuDetail attach = new H_RuzhuDetail();
                        attach.AutoID = att.AutoID;
                        attach.FangJianHao = att.FangJianHao;
                        attach.OrderGuid = guid;
                        attach.OrderID = att.OrderID;
                        attach.ShijiFangjia = att.ShijiFangjia;
                        attach.XingBie = att.XingBie;
                        attach.XingMing = att.XingMing;
                        attach.YuanFangJia = att.YuanFangJia;
                        attach.ZheKouLv = att.ZheKouLv;
                        attach.ZhengjianDizhi = att.ZhengjianDizhi;
                        attach.ZhengjianHaoma = att.ZhengjianHaoma;
                        attach.ZhengjianLeixing = att.ZhengjianLeixing;
                        attach.ArriveTime = query.DaodianTime;
                        attach.LeaveTime = query.LidianTime;
                        attach.Status = 0;
                        db.H_RuzhuDetail.AddObject(attach);
                    }
                    db.SaveChanges();
                    //改变状态为在客
                    foreach (var att in attaches)
                    {
                        RoomStatus statushlp = new RoomStatus();
                        statushlp.SetRoomStatus(att.FangJianHao, "在客");
                    }
                    result = "0";
                }
            }
            return result;
        }

        //添加随客信息到对应帐号
        public string AppendSuike(System.Guid guid, List<SuikeModel> suikes)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //获取帐单
                var query = (from a in db.H_RuzhuOrder
                             where a.OrderGuid == guid
                             select a).SingleOrDefault();
                if (query != null)
                {
                    //根据帐单id添加attach
                    foreach (var suike in suikes)
                    {
                        H_RuzhuSuike temp = new H_RuzhuSuike();
                        temp.AutoID = suike.AutoID;
                        temp.DiZhi = suike.Address;
                        temp.OrderGuid = guid;
                        temp.ShenfenZheng = suike.Card;
                        temp.XingBie = suike.XingBie;
                        temp.XingMing = suike.XingMing;
                        db.H_RuzhuSuike.AddObject(temp);
                    }
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }

        #region 获取费用信息
        //读取所有客房费用
        public List<Cash_RunningDetails> GetCashDetails(string fanhao)
        {
            List<Cash_RunningDetails> results = new List<Cash_RunningDetails>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Cash_RunningDetails
                            where a.RoomNo == fanhao
                            select a;
                results = query.ToList<Cash_RunningDetails>();
            }
            return results;
        }
        //读取所有相关费用
        public List<RZFeiYongModel> GetRZFeiYongDetails(string orderid, int pagesize, int pageindex, out int total)
        {
            List<RZFeiYongModel> resultdatalist = new List<RZFeiYongModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT [OrderGuid]
                                          ,[FeiYongType]
                                          ,[FeiYongDetai]
                                          ,[SumMoney]
                                      FROM [RZ_FeiYongView]
                                      WHERE [OrderGuid]=@OrderGuid";
                    var parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "OrderGuid", Value =orderid}
                    };
                    var tempresult = _db.ExecuteStoreQuery<RZFeiYongModel>(sqltxt, parm);
                    List<RZFeiYongModel> datalist = tempresult.ToList<RZFeiYongModel>();
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (pageindex - 1) * pagesize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RZFeiYongModel>();
                    if (resultdatalist.Count > pagesize)
                    {
                        resultdatalist.RemoveRange(pagesize, resultdatalist.Count - pagesize);
                    }
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    total = 0;
                }

            }
            return resultdatalist;
        }
        #endregion

        #region 读取账单
        //读取所有帐单信息
        public List<OrderViewModel> ReadOrder()
        {
            List<OrderViewModel> results = new List<OrderViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.H_RuzhuOrder
                            select a;
                foreach (var q in query)
                {
                    OrderViewModel temp = new OrderViewModel();
                    temp.AutoID = q.AutoID;
                    temp.ChangTu = q.ChangTu;
                    temp.ShiHua = q.ShiHua;
                    temp.ChangBao = q.ChangBao;
                    temp.ZhongDian = q.ZhongDian;
                    temp.DaodianTime = q.DaodianTime;
                    temp.LidianTime = q.LidianTime;
                    temp.JiaoxingFuwu = q.JiaoxingFuwu;
                    temp.FukuanFangshi = q.FukuanFangshi;
                    temp.YaJin = q.YaJin;
                    temp.XieyiDanwei = q.XieyiDanwei;
                    temp.TeQuanRen = q.TeQuanRen;
                    temp.ZheKouLv = q.ZheKouLv;
                    temp.ShijiFangjia = q.ShijiFangjia;
                    temp.BeiZhu = q.BeiZhu;
                    temp.BaoMi = q.BaoMi;
                    temp.ShougongDanhao = q.ShougongDanhao;
                    temp.KerenLeibie = q.KerenLeibie;
                    temp.TuanDui = q.TuanDui;
                    temp.ZhengjianLeibie = q.ZhengjianLeibie;
                    temp.DianHua = q.DianHua;
                    temp.FangjianLeixing = q.FangjianLeixing;
                    temp.XingBie = q.XingBie;
                    temp.HuiYuanKa = q.HuiYuanKa;
                    temp.JiFen = q.JiFen;
                    temp.XingMing = q.XingMing;
                    temp.ZhengJianHao = q.ZhengJianHao;
                    temp.DiZhi = q.DiZhi;
                    temp.BiaozhunFangjia = q.BiaozhunFangjia;
                    temp.FangHao = q.FangHao;
                    results.Add(temp);
                }
            }
            return results;
        }
        //合并账单时 读取所有可选信息 即非合并目标帐单,参数fh
        public List<OrderViewModel> ReadCanSelect(string fh)
        {
            List<OrderViewModel> results = new List<OrderViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var detail = (from a in db.H_RuzhuDetail
                              where a.FangJianHao == fh
                              && a.Status == 0
                              select a).ToList().FirstOrDefault();
                //编号，姓名，主账房间号，电话 四个值显示到页面表示具体主账单
                if (detail != null)
                {
                    var ordertemp = from a in db.H_RuzhuOrder
                                    where a.OrderGuid != detail.OrderGuid
                                    && a.Status == 0
                                    select a;
                    foreach (var q in ordertemp)
                    {
                        OrderViewModel temp = new OrderViewModel();
                        temp.AutoID = q.AutoID;
                        temp.ChangTu = q.ChangTu;
                        temp.ShiHua = q.ShiHua;
                        temp.ChangBao = q.ChangBao;
                        temp.ZhongDian = q.ZhongDian;
                        temp.DaodianTime = q.DaodianTime;
                        temp.LidianTime = q.LidianTime;
                        temp.JiaoxingFuwu = q.JiaoxingFuwu;
                        temp.FukuanFangshi = q.FukuanFangshi;
                        temp.YaJin = q.YaJin;
                        temp.XieyiDanwei = q.XieyiDanwei;
                        temp.TeQuanRen = q.TeQuanRen;
                        temp.ZheKouLv = q.ZheKouLv;
                        temp.ShijiFangjia = q.ShijiFangjia;
                        temp.BeiZhu = q.BeiZhu;
                        temp.BaoMi = q.BaoMi;
                        temp.ShougongDanhao = q.ShougongDanhao;
                        temp.KerenLeibie = q.KerenLeibie;
                        temp.TuanDui = q.TuanDui;
                        temp.ZhengjianLeibie = q.ZhengjianLeibie;
                        temp.DianHua = q.DianHua;
                        temp.FangjianLeixing = q.FangjianLeixing;
                        temp.XingBie = q.XingBie;
                        temp.HuiYuanKa = q.HuiYuanKa;
                        temp.JiFen = q.JiFen;
                        temp.XingMing = q.XingMing;
                        temp.ZhengJianHao = q.ZhengJianHao;
                        temp.DiZhi = q.DiZhi;
                        temp.BiaozhunFangjia = q.BiaozhunFangjia;
                        temp.FangHao = q.FangHao;
                        temp.OrderGuid = q.OrderGuid;
                        results.Add(temp);
                    }
                }
            }
            return results;
        }
        //分页读取帐单信息
        public List<OrderViewModel> ReadPartOrder(int page, int rows)
        {
            List<OrderViewModel> results = new List<OrderViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = (from a in db.H_RuzhuOrder
                             orderby a.AutoID descending
                             select a).Skip((page - 1) * rows).Take(rows);

                foreach (var q in query)
                {
                    OrderViewModel temp = new OrderViewModel();
                    temp.AutoID = q.AutoID;
                    temp.ChangTu = q.ChangTu;
                    temp.ShiHua = q.ShiHua;
                    temp.ChangBao = q.ChangBao;
                    temp.ZhongDian = q.ZhongDian;
                    temp.DaodianTime = q.DaodianTime;
                    temp.LidianTime = q.LidianTime;
                    temp.JiaoxingFuwu = q.JiaoxingFuwu;
                    temp.FukuanFangshi = q.FukuanFangshi;
                    temp.YaJin = q.YaJin;
                    temp.XieyiDanwei = q.XieyiDanwei;
                    temp.TeQuanRen = q.TeQuanRen;
                    temp.ZheKouLv = q.ZheKouLv;
                    temp.ShijiFangjia = q.ShijiFangjia;
                    temp.BeiZhu = q.BeiZhu;
                    temp.BaoMi = q.BaoMi;
                    temp.ShougongDanhao = q.ShougongDanhao;
                    temp.KerenLeibie = q.KerenLeibie;
                    temp.TuanDui = q.TuanDui;
                    temp.ZhengjianLeibie = q.ZhengjianLeibie;
                    temp.DianHua = q.DianHua;
                    temp.FangjianLeixing = q.FangjianLeixing;
                    temp.XingBie = q.XingBie;
                    temp.HuiYuanKa = q.HuiYuanKa;
                    temp.JiFen = q.JiFen;
                    temp.XingMing = q.XingMing;
                    temp.ZhengJianHao = q.ZhengJianHao;
                    temp.DiZhi = q.DiZhi;
                    temp.BiaozhunFangjia = q.BiaozhunFangjia;
                    temp.FangHao = q.FangHao;
                    results.Add(temp);
                }
            }
            return results;
        }
        //读取一个账单信息by房号
        public H_RuzhuOrder ReadOrderByFH(string fh)
        {
            H_RuzhuOrder result = new H_RuzhuOrder();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                            where a.FangJianHao == fh & a.Status != 1
                            select a).ToList().FirstOrDefault();
                //编号，姓名，主账房间号，电话 四个值显示到页面表示具体主账单
                if (temp != null)
                {
                    var ordertemp = (from a in db.H_RuzhuOrder
                                     where a.OrderGuid == temp.OrderGuid
                                     select a).SingleOrDefault();
                    if (ordertemp != null)
                    {
                        result = ordertemp;
                    }
                }
            }
            return result;
        }
        //读取一个账单信息by房号
        public H_RuzhuOrder ReadOrderByOrderGuid(string orderguid)
        {
            H_RuzhuOrder result = new H_RuzhuOrder();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                Guid guid = Guid.Parse(orderguid);
                //编号，姓名，主账房间号，电话 四个值显示到页面表示具体主账单

                var ordertemp = (from a in db.H_RuzhuOrder
                                 where a.OrderGuid == guid
                                 select a).SingleOrDefault();
                if (ordertemp != null)
                {
                    result = ordertemp;
                }

            }
            return result;
        }
        //读取一个账单信息by房号
        public OrderViewModel ReadOrderviewmodelByFH(string fh)
        {
            OrderViewModel result = new OrderViewModel();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                            where a.FangJianHao == fh
                            && a.Status == 0
                            select a).ToList().FirstOrDefault();
                //编号，姓名，主账房间号，电话 四个值显示到页面表示具体主账单
                if (temp != null)
                {
                    var ordertemp = (from a in db.H_RuzhuOrder
                                     where a.OrderGuid == temp.OrderGuid
                                     select a).SingleOrDefault();
                    if (ordertemp != null)
                    {
                        result.FangHao = ordertemp.FangHao;
                        result.DianHua = ordertemp.DianHua;
                        result.AutoID = ordertemp.AutoID;
                        result.XingMing = ordertemp.XingMing;
                        result.OrderGuid = ordertemp.OrderGuid;
                    }
                }
            }
            return result;
        }
        //通过筛选条件 读取所有帐单信息
        public List<OrderViewModel> ReadOrder(OrderFilter filter)
        {
            List<OrderViewModel> results = new List<OrderViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.H_RuzhuOrder
                            select a;
                //客户姓名
                if (!string.IsNullOrEmpty(filter.XingMing))
                {
                    query = from a in query
                            where a.XingMing.Contains(filter.XingMing)
                            select a;
                }
                //编号
                if (!string.IsNullOrEmpty(filter.AutoID))
                {
                    query = from a in query
                            where a.AutoID.Contains(filter.AutoID)
                            select a;
                }
                //房号
                if (!string.IsNullOrEmpty(filter.FangHao))
                {
                    query = from a in query
                            where a.FangHao.Contains(filter.FangHao)
                            select a;
                }
                //客人类别
                if (!string.IsNullOrEmpty(filter.KerenLeibie))
                {
                    query = from a in query
                            where a.KerenLeibie.Contains(filter.KerenLeibie)
                            select a;
                }
                //付款方式
                if (!string.IsNullOrEmpty(filter.FukuanFangshi))
                {
                    query = from a in query
                            where a.FukuanFangshi.Contains(filter.FukuanFangshi)
                            select a;
                }
                if (filter.Begin.Year > 2001)
                {
                    query = from a in query
                            where a.DaodianTime > filter.Begin
                            select a;
                }
                if (filter.End.Year > 2001)
                {
                    query = from a in query
                            where a.DaodianTime < filter.End
                            select a;
                }

                foreach (var q in query)
                {
                    OrderViewModel temp = new OrderViewModel();
                    temp.AutoID = q.AutoID;
                    temp.ChangTu = q.ChangTu;
                    temp.ShiHua = q.ShiHua;
                    temp.ChangBao = q.ChangBao;
                    temp.ZhongDian = q.ZhongDian;
                    temp.DaodianTime = q.DaodianTime;
                    temp.LidianTime = q.LidianTime;
                    temp.JiaoxingFuwu = q.JiaoxingFuwu;
                    temp.FukuanFangshi = q.FukuanFangshi;
                    temp.YaJin = q.YaJin;
                    temp.XieyiDanwei = q.XieyiDanwei;
                    temp.TeQuanRen = q.TeQuanRen;
                    temp.ZheKouLv = q.ZheKouLv;
                    temp.ShijiFangjia = q.ShijiFangjia;
                    temp.BeiZhu = q.BeiZhu;
                    temp.BaoMi = q.BaoMi;
                    temp.ShougongDanhao = q.ShougongDanhao;
                    temp.KerenLeibie = q.KerenLeibie;
                    temp.TuanDui = q.TuanDui;
                    temp.ZhengjianLeibie = q.ZhengjianLeibie;
                    temp.DianHua = q.DianHua;
                    temp.FangjianLeixing = q.FangjianLeixing;
                    temp.XingBie = q.XingBie;
                    temp.HuiYuanKa = q.HuiYuanKa;
                    temp.JiFen = q.JiFen;
                    temp.XingMing = q.XingMing;
                    temp.ZhengJianHao = q.ZhengJianHao;
                    temp.DiZhi = q.DiZhi;
                    temp.BiaozhunFangjia = q.BiaozhunFangjia;
                    temp.FangHao = q.FangHao;
                    results.Add(temp);
                }
            }
            return results;
        }
        //通过筛选条件 分页读取帐单信息
        public List<OrderViewModel> ReadPartOrder(int page, int rows, OrderFilter filter)
        {
            List<OrderViewModel> results = new List<OrderViewModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.H_RuzhuOrder
                            select a;
                //客户姓名
                if (!string.IsNullOrEmpty(filter.XingMing))
                {
                    query = from a in query
                            where a.XingMing.Contains(filter.XingMing)
                            select a;
                }
                //编号
                if (!string.IsNullOrEmpty(filter.AutoID))
                {
                    query = from a in query
                            where a.AutoID.Contains(filter.AutoID)
                            select a;
                }
                //房号
                if (!string.IsNullOrEmpty(filter.FangHao))
                {
                    query = from a in query
                            where a.FangHao.Contains(filter.FangHao)
                            select a;
                }
                //客人类别
                if (!string.IsNullOrEmpty(filter.KerenLeibie))
                {
                    query = from a in query
                            where a.KerenLeibie.Contains(filter.KerenLeibie)
                            select a;
                }
                //付款方式
                if (!string.IsNullOrEmpty(filter.FukuanFangshi))
                {
                    query = from a in query
                            where a.FukuanFangshi.Contains(filter.FukuanFangshi)
                            select a;
                }
                //付款方式
                if (!string.IsNullOrEmpty(filter.ZhanghaoZhuangtai))
                {
                    switch (filter.ZhanghaoZhuangtai)
                    {
                        case "未结离店":
                            query = from a in query
                                    where a.Status == 0 && (a.LidianTime <= DateTime.Now)
                                    select a;
                            break;
                        case "在住":
                            query = from a in query
                                    where a.Status == 0 && (a.LidianTime > DateTime.Now)
                                    select a;
                            break;
                        case "已结离店":
                            query = from a in query
                                    where a.Status == 1
                                    select a;
                            break;
                    }
                }
                if (filter.Begin.Year > 2001)
                {
                    query = from a in query
                            where a.DaodianTime > filter.Begin
                            select a;
                }
                if (filter.End.Year > 2001)
                {
                    query = from a in query
                            where a.DaodianTime < filter.End
                            select a;
                }
                query = (from a in query
                         orderby a.DaodianTime descending
                         select a).Skip((page - 1) * rows).Take(rows);

                foreach (var q in query)
                {
                    OrderViewModel temp = new OrderViewModel();
                    temp.AutoID = q.AutoID;
                    temp.OrderGuid = q.OrderGuid;
                    temp.ChangTu = q.ChangTu;
                    temp.ShiHua = q.ShiHua;
                    temp.ChangBao = q.ChangBao;
                    temp.ZhongDian = q.ZhongDian;
                    temp.DaodianTime = q.DaodianTime;
                    temp.LidianTime = q.LidianTime;
                    temp.JiaoxingFuwu = q.JiaoxingFuwu;
                    temp.FukuanFangshi = q.FukuanFangshi;
                    temp.YaJin = q.YaJin;
                    temp.XieyiDanwei = q.XieyiDanwei;
                    temp.TeQuanRen = q.TeQuanRen;
                    temp.ZheKouLv = q.ZheKouLv;
                    temp.ShijiFangjia = q.ShijiFangjia;
                    temp.BeiZhu = q.BeiZhu;
                    temp.BaoMi = q.BaoMi;
                    temp.ShougongDanhao = q.ShougongDanhao;
                    temp.KerenLeibie = q.KerenLeibie;
                    temp.TuanDui = q.TuanDui;
                    temp.ZhengjianLeibie = q.ZhengjianLeibie;
                    temp.DianHua = q.DianHua;
                    temp.FangjianLeixing = q.FangjianLeixing;
                    temp.XingBie = q.XingBie;
                    temp.HuiYuanKa = q.HuiYuanKa;
                    temp.JiFen = q.JiFen;
                    temp.XingMing = q.XingMing;
                    temp.ZhengJianHao = q.ZhengJianHao;
                    temp.DiZhi = q.DiZhi;
                    temp.BiaozhunFangjia = q.BiaozhunFangjia;
                    temp.FangHao = q.FangHao;
                    temp.Status = (q.Status == 1 ? "结账" : q.LidianTime<=DateTime.Now?"未结离店":"未结");
                    results.Add(temp);
                }
            }
            return results;
        }
        #endregion

        #region 读取入住信息
        public OrderAttachModel ReadAttachByFH(string fh)
        {
            OrderAttachModel result = new OrderAttachModel();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                            where a.Status == 0
                            && a.FangJianHao == fh
                            select a).SingleOrDefault();
                if (temp != null)
                {
                    result.ArriveTime = temp.ArriveTime;
                    result.AutoID = temp.AutoID;
                    result.FangJianHao = temp.FangJianHao;
                    result.LeaveTime = temp.LeaveTime;
                    result.OrderGuid = temp.OrderGuid;
                    result.OrderID = temp.OrderID;
                    result.ShijiFangjia = temp.ShijiFangjia;
                    result.XingBie = temp.XingBie;
                    result.XingMing = temp.XingMing;
                    result.YuanFangJia = temp.YuanFangJia;
                    result.ZhengjianDizhi = temp.ZhengjianDizhi;
                    result.ZhengjianHaoma = temp.ZhengjianHaoma;
                    result.ZhengjianLeixing = temp.ZhengjianLeixing;
                }

            }
            return result;
        }

        //根据房号读取所在账单的所有房间信息
        public List<OrderAttachModel> ReadAllAttachByFH(string fh)
        {
            List<OrderAttachModel> result = new List<OrderAttachModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var fj = (from a in db.H_RuzhuDetail
                          where a.Status == 0
                          && a.FangJianHao == fh
                          select a).SingleOrDefault();
                if (fj != null)
                {
                    var alls = from a in db.H_RuzhuDetail
                               where a.Status == 0
                               && a.OrderGuid == fj.OrderGuid
                               select a;
                    foreach (var all in alls)
                    {
                        OrderAttachModel temp = new OrderAttachModel();
                        temp.ArriveTime = all.ArriveTime;
                        temp.AutoID = all.AutoID;
                        temp.FangJianHao = all.FangJianHao;
                        temp.LeaveTime = all.LeaveTime;
                        temp.OrderGuid = all.OrderGuid;
                        temp.OrderID = all.OrderID;
                        temp.ShijiFangjia = all.ShijiFangjia;
                        temp.XingBie = all.XingBie;
                        temp.XingMing = all.XingMing;
                        temp.YuanFangJia = all.YuanFangJia;
                        temp.ZhengjianDizhi = all.ZhengjianDizhi;
                        temp.ZhengjianHaoma = all.ZhengjianHaoma;
                        temp.ZhengjianLeixing = all.ZhengjianLeixing;
                        result.Add(temp);
                    }
                }
            }
            return result;
        }
        #endregion
    }
    /// <summary>
    ///  入住model
    /// </summary>
    public class OrderViewModel
    {
        public string AutoID { get; set; }
        public bool? ChangTu { get; set; }
        public bool? ShiHua { get; set; }
        public bool? ChangBao { get; set; }
        public bool? ZhongDian { get; set; }
        public DateTime DaodianTime { get; set; }
        public DateTime LidianTime { get; set; }
        public bool? JiaoxingFuwu { get; set; }
        public string FukuanFangshi { get; set; }
        public decimal? YaJin { get; set; }
        public string XieyiDanwei { get; set; }
        public string TeQuanRen { get; set; }
        public double? ZheKouLv { get; set; }
        public decimal? ShijiFangjia { get; set; }
        public string BeiZhu { get; set; }
        public bool? BaoMi { get; set; }
        public string ShougongDanhao { get; set; }
        public string KerenLeibie { get; set; }
        public string TuanDui { get; set; }
        public string ZhengjianLeibie { get; set; }
        public string DianHua { get; set; }
        public string FangjianLeixing { get; set; }
        public string XingBie { get; set; }
        public string HuiYuanKa { get; set; }
        public Int32? JiFen { get; set; }
        public string XingMing { get; set; }
        public string ZhengJianHao { get; set; }
        public string DiZhi { get; set; }
        public string BiaozhunFangjia { get; set; }
        public Guid OrderGuid { get; set; }
        public string FangHao { get; set; }
        public string GuoJi { get; set; }
        public string XiaoShouYuan { get; set; }
        public string CaoZuoYuan { get; set; }
        public string Status { get; set; }
    }
    /// <summary>
    /// 每个房间的入住信息model
    /// </summary>
    public class OrderAttachModel
    {
        public Int32 AutoID { get; set; }
        public string OrderID { get; set; }
        public string FangJianHao { get; set; }
        public string XingMing { get; set; }
        public string XingBie { get; set; }
        public string ZhengjianLeixing { get; set; }
        public string ZhengjianHaoma { get; set; }
        public string ZhengjianDizhi { get; set; }
        public decimal? YuanFangJia { get; set; }
        public float? ZheKouLv { get; set; }
        public decimal? ShijiFangjia { get; set; }
        public DateTime ArriveTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public Guid OrderGuid { get; set; }
        public string JBName { get; set; }
    }
    /// <summary>
    /// 随客信息的model
    /// </summary>
    public class SuikeModel
    {
        public Int32 AutoID { get; set; }
        public string XingMing { get; set; }
        public string XingBie { get; set; }
        public string Card { get; set; }
        public string Address { get; set; }
        public string CarNum { get; set; }
        public string ChePai { get; set; }
        public string BeiZhu { get; set; }
        public Guid OrderGuid { get; set; }
    }
    /// <summary>
    /// 账单查询封装类
    /// </summary>
    public class OrderFilter
    {
        public string XingMing { get; set; }
        public string AutoID { get; set; }
        public string FangHao { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string KerenLeibie { get; set; }
        public string FukuanFangshi { get; set; }
        public string ZhanghaoZhuangtai { get; set; }

    }
    /// <summary>
    /// 入住相关费用对象
    /// </summary>
    public class OrderCashDetailsViewModel
    {
        /// <summary>
        /// 行ID
        /// </summary>
        public int AutoID { get; set; }
        /// <summary>
        /// 入住单ID
        /// </summyary>
        public Guid OrderGuid { get; set; }
        /// <summary>
        /// 房号
        /// </summary>
        public string Fanhao { get; set; }
        /// <summary>
        /// 费用科目
        /// </summary>
        public string KM { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 押金
        /// </summary>
        public decimal Deposit { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 单据号码(手动)
        /// </summary>
        public string RunningNum { get; set; }
        /// <summary>
        /// 单据号码(自动)
        /// </summary>
        public string RunningNumAuto { get; set; }
        /// <summary>
        /// 单据时间
        /// </summary>
        public DateTime RunningTime { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string Payment { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
    }
    /// <summary>
    /// 相关费用
    /// </summary>
    public class RZFeiYongModel
    {
        public Guid OrderGuid { get; set; }
        public string FeiYongType { get; set; }
        public string FeiYongDetai { get; set; }
        public decimal SumMoney { get; set; }
    }
}