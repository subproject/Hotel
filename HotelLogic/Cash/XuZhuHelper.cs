using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelLogic.FrontDesk;
using HotelEntities;

namespace HotelLogic.Cash
{
    public class XuZhuHelper
    {
        #region 单例模式
        private static XuZhuHelper _instance;
        public static XuZhuHelper Instance
        {
            get { return _instance != null ? _instance=new XuZhuHelper() : new XuZhuHelper(); }
        }
        private XuZhuHelper() { }
        #endregion
        //续住
        //添加一条续住记录
        //将当前房间离日推迟
        //添加一条押金信息到帐单下
        public string XuZhu(XuZhuModel data)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                
                H_RuzhuXZ temp = new H_RuzhuXZ();
                temp.AppendDeposit = data.AppendDeposit;
                temp.DayCount = data.DayCount;
                temp.Deposit = data.Deposit;
                temp.LeaveTime = data.LeaveTime;
                temp.Name = data.Name;
                temp.NewLeaveTime = data.NewLeaveTime;
                temp.Payway = data.PayWay;
                temp.Price = data.Price;
                temp.RoomID = data.RoomID;
                temp.RoomLevel = data.RoomLevel;
                db.H_RuzhuXZ.AddObject(temp);
                db.SaveChanges();
                result = "0";
            }
            return result;
        }

        //根据房号得到续住前房间入住信息
        public XuZhuModel GetInitByFH(string fh)
        {
            XuZhuModel result = new XuZhuModel();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                           where a.Status == 0
                           && a.FangJianHao == fh
                           select a).SingleOrDefault();
                if (temp != null)
                {
                    result.RoomLevel = temp.JiBie;
                    result.RoomID = temp.FangJianHao;
                    result.Name = temp.XingMing;
                    result.Price = (decimal)(temp.ShijiFangjia == null ? 0 : temp.ShijiFangjia);
                    result.LeaveTime = temp.LeaveTime;

                    //入住单信息 
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == temp.OrderGuid
                                 select o).SingleOrDefault();
                    if (order != null)
                    {
                        result.Deposit = order.YaJin == null ? 0 : (decimal)order.YaJin;
                    }
                    
                }
            }
            return result;
        }
    }
    public class XuZhuModel
    {
        public string RoomLevel { get; set; }
        public string RoomID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime LeaveTime { get; set; }
        public DateTime NewLeaveTime { get; set; }
        //order table data
        public decimal Deposit { get; set; }
        public string PayWay { get; set; }
        public int DayCount { get; set; }
        public int AutoID { get; set; }
        public decimal AppendDeposit { get; set; }
    }
}
