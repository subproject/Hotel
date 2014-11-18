using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.FrontDesk;

namespace HotelLogic.Cash
{
    public class RunningList
    {
        //create
        public string CreateRunning(RunningModel q)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                try
                {
                    Cash_RunningDetails temp = new Cash_RunningDetails();
                    temp.OrderGuid = q.OrderGuid;
                    temp.CustomerName = q.CustomerName;
                    temp.RoomNo = q.RoomNo;
                    temp.KM = q.KM;
                    temp.Price = q.Price;
                    temp.Deposit = q.Deposit;
                    temp.Remark = q.Remark;
                    temp.RunningNum = q.RunningNum;
                    temp.RunningNumAuto = q.RunningNumAuto;
                    temp.RunningTime = q.RunningTime;
                    temp.Payment = q.Payment;
                    temp.Operator = q.Operator;
                    temp.Status = q.Status;
                    db.Cash_RunningDetails.AddObject(temp);
                    db.SaveChanges();
                    result = "0";
                }
                catch (Exception e)
                {
                    result = "-1";
                }
            }
            return result;
        }
        //read 根据帐单查找
        public List<RunningModel> GetRunningByOrder(System.Guid guid)
        {
            List<RunningModel> result = new List<RunningModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query= from a in db.Cash_RunningDetails
                           where a.OrderGuid==guid
                           select a;
                foreach (var q in query)
                {
                    RunningModel temp = new RunningModel();
                    temp.AutoID = q.AutoID;
                    temp.OrderGuid = q.OrderGuid;
                    temp.CustomerName = q.CustomerName;
                    temp.RoomNo = q.RoomNo;
                    temp.KM = q.KM;
                    temp.Price = q.Price;
                    temp.Deposit = q.Deposit;
                    temp.Remark = q.Remark;
                    temp.RunningNum = q.RunningNum;
                    temp.RunningNumAuto = q.RunningNumAuto;
                    temp.RunningTime = q.RunningTime;
                    temp.Payment = q.Payment;
                    temp.Operator = q.Operator;
                    temp.Status = q.Status;
                    result.Add(temp);
                }
            }
            return result;
        }
        //read 根据帐单查找房费
        public List<RunningModel> GetFFeeByOrder(System.Guid guid)
        {
            List<RunningModel> result = new List<RunningModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Cash_RunningDetails
                            where a.OrderGuid == guid
                            && a.Remark.Contains("房费")
                            select a;
                foreach (var q in query)
                {
                    RunningModel temp = new RunningModel();
                    temp.AutoID = q.AutoID;
                    temp.OrderGuid = q.OrderGuid;
                    temp.CustomerName = q.CustomerName;
                    temp.RoomNo = q.RoomNo;
                    temp.KM = q.KM;
                    temp.Price = q.Price;
                    temp.Deposit = q.Deposit;
                    temp.Remark = q.Remark;
                    temp.RunningNum = q.RunningNum;
                    temp.RunningNumAuto = q.RunningNumAuto;
                    temp.RunningTime = q.RunningTime;
                    temp.Payment = q.Payment;
                    temp.Operator = q.Operator;
                    temp.Status = q.Status;
                    result.Add(temp);
                }
            }
            return result;
        }
        //read 根据帐单查找消费费用
        public List<RunningModel> GetOtherFeeByOrder(System.Guid guid)
        {
            List<RunningModel> result = new List<RunningModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var query = from a in db.Cash_RunningDetails
                            where a.OrderGuid == guid
                            && !a.Remark.Contains("房费")
                            select a;
                foreach (var q in query)
                {
                    RunningModel temp = new RunningModel();
                    temp.AutoID = q.AutoID;
                    temp.OrderGuid = q.OrderGuid;
                    temp.CustomerName = q.CustomerName;
                    temp.RoomNo = q.RoomNo;
                    temp.KM = q.KM;
                    temp.Price = q.Price;
                    temp.Deposit = q.Deposit;
                    temp.Remark = q.Remark;
                    temp.RunningNum = q.RunningNum;
                    temp.RunningNumAuto = q.RunningNumAuto;
                    temp.RunningTime = q.RunningTime;
                    temp.Payment = q.Payment;
                    temp.Operator = q.Operator;
                    temp.Status = q.Status;
                    result.Add(temp);
                }
            }
            return result;
        }
        //红冲
        public string DisableRunning(RunningModel q)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var record = (from a in db.Cash_RunningDetails
                             where a.AutoID == q.AutoID
                             select a).FirstOrDefault();
                if(record!=null)
                {
                    Cash_RunningDetails temp = new Cash_RunningDetails();
                    temp.OrderGuid = record.OrderGuid;
                    temp.CustomerName = record.CustomerName;
                    temp.RoomNo = record.RoomNo;
                    temp.KM = record.KM;
                    temp.Price = -record.Price;
                    temp.Deposit = record.Deposit;
                    temp.Remark = record.Remark;
                    temp.RunningNum = record.RunningNum;
                    temp.RunningNumAuto = record.RunningNumAuto;
                    temp.RunningTime = record.RunningTime;
                    temp.Payment = record.Payment;
                    temp.Operator = record.Operator;
                    temp.Status = record.Status;
                    db.Cash_RunningDetails.AddObject(temp);
                    result = "0";
                }
            }
            return result;
        }

        //结账部分逻辑,根据orderguid得到相关信息
        //根据账单结账，根据账单得到日期天数，根据detail表分别计算房费
        //根据账单计算费用
        public  JieZhangModel GetJZDanByGuid(System.Guid guid)
        {
            JieZhangModel result = new JieZhangModel();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //得到账单信息
                var order = (from a in db.H_RuzhuOrder
                             where a.OrderGuid == guid
                             select a).SingleOrDefault();
                if (order != null)
                {
                    result.AutoID = order.AutoID;
                    result.XingMing = order.XingMing;
                    result.FangHao = order.FangHao;
                    result.DianHua = order.DianHua;
                    result.OrderGuid = order.OrderGuid;
                }
            }
            return result;
        }

        public List<FeiYongModel> GetFYListByGuid(System.Guid guid)
        {
            List<FeiYongModel> result = new List<FeiYongModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var feilist = from a in db.Cash_RunningDetails
                             where a.OrderGuid == guid
                             select a;
                foreach (var fei in feilist)
                {
                    FeiYongModel temp = new FeiYongModel();
                    temp.Fei = fei.Price;
                    temp.KM = fei.KM;
                    result.Add(temp);
                }
            }
            return result;
        }

        public List<FangFeiModel> GetFFListByGuid(System.Guid guid)
        {
            List<FangFeiModel> result = new List<FangFeiModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //得到账单信息
                var order = (from a in db.H_RuzhuOrder
                             where a.OrderGuid == guid
                             select a).SingleOrDefault();
                if (order != null)
                {
                    //天数计算
                    TimeSpan ts=Convert.ToDateTime(order.LidianTime)-Convert.ToDateTime(order.DaodianTime);
                    int days = ts.Days;
                    var fflist = from a in db.H_RuzhuDetail
                                 where a.OrderGuid == guid
                                 select a;
                    foreach (var ff in fflist)
                    {
                        FangFeiModel temp = new FangFeiModel();
                        temp.Fei = ff.ShijiFangjia * days;
                        temp.TianShu = days;
                        temp.FH = ff.FangJianHao;
                    }
                }
            }
            return result;
        }

        //结帐
        public string FinishOrder(System.Guid guid)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //将费用标记为“已结”
                var fylist = from a in db.Cash_RunningDetails
                             where a.OrderGuid == guid
                             select a;
                foreach (var fy in fylist)
                {
                    fy.Status = "已结";
                }

                //将房间标记为脏房
                var fjlist = from f in db.H_RuzhuDetail
                             where f.OrderGuid == guid
                             select f;
                foreach (var fj in fjlist)
                {
                    RoomStatus helper=new RoomStatus();
                    helper.SetRoomStatus(fj.FangJianHao,"脏房");                    
                }
                //将订单标记为已结
                var order = (from o in db.H_RuzhuOrder
                             where o.OrderGuid == guid
                             select o).SingleOrDefault();
                if (order != null)
                {
                    order.Status = 1;
                }
                //将入住详细标为过期
                var details = from d in db.H_RuzhuDetail
                             where d.OrderGuid == guid
                             select d;
                foreach (var detail in details)
                {
                    detail.Status = 1;
                }
                //将随客信息标为过期
                var suikes = from s in db.H_RuzhuSuike
                             where s.OrderGuid == guid
                             select s;
                foreach (var suike in suikes)
                {
                    suike.Status = 1;
                }
                db.SaveChanges();
                result = "0";
            }
            return result;
        }
    }

    //流水账model
    public class RunningModel
    {
        public Guid AutoID { get; set; }
        public System.Guid OrderGuid { get; set; }
        public string CustomerName { get; set; }
        public string RoomNo { get; set; }
        public string KM { get; set; }
        public decimal? Price { get; set; }
        public decimal? Deposit { get; set; }
        public string Remark { get; set; }
        public string RunningNum { get; set; }
        public string RunningNumAuto { get; set; }
        public System.DateTime? RunningTime { get; set; }
        public string Payment { get; set; }
        public string Operator { get; set; }
        public string Status { get; set; }
    }
    //结帐model
    public class JieZhangModel
    {
        public string AutoID { get; set; }
        public string XingMing { get; set; }
        public string FangHao { get; set; }
        public string DianHua { get; set; }
        public System.Guid OrderGuid { get; set; }
        public decimal YingFu { get; set; }
        public decimal YiFu { get; set; }
        public decimal DingJin { get; set; }
        public decimal WeiFu { get; set; }
        public decimal TuiKuan { get; set; }
    }
    //结帐费用明细model
    public class FeiYongModel
    {
        public string KM{get;set;}
        public decimal? Fei{get;set;}
    }
    //房费费用明细model
    public class FangFeiModel
    {
        public string FH{get;set;}
        public Int32 TianShu{get;set;}
        public decimal? Fei {get;set;}
    }
}
