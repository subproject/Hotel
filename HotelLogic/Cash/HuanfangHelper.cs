using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.FrontDesk;

namespace HotelLogic.Cash
{
    public class HuanfangHelper
    {
        #region 单例模式
        private static HuanfangHelper _instance;
        public static HuanfangHelper Instance
        {
            get { return _instance != null ? _instance : _instance = new HuanfangHelper(); }
        }
        private HuanfangHelper(){ }
        #endregion

        public string HuanFang(HuanFangDan hf)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {

                    //将老房间标记为脏房
                    RoomStatus changer = new RoomStatus();
                    changer.SetRoomStatus(hf.RoomID, "脏房");
                    //计算换房费用并添加金running表

                    //添加换房记录
                    H_RuzhuHF temp = new H_RuzhuHF();
                    temp.Deposit = hf.Deposit;
                    temp.LeaveDate = hf.LeaveDate;
                    temp.Money = hf.Money;
                    temp.Name = hf.Name;
                    temp.NewPrice = hf.NewPrice;
                    temp.NewRoomID = hf.NewRoomID;
                    temp.Number = hf.Number;
                    temp.OrderGuid = hf.OrderGuid;
                    temp.Price = hf.Price;
                    temp.Rebate = hf.Rebate;
                    temp.Remark = hf.Remark;
                    temp.RoomID = hf.RoomID;
                    temp.RoomLevel = hf.RoomLevel;
                    db.H_RuzhuHF.AddObject(temp);
                    //得到原入住信息，将status标记为1
                    var old = (from a in db.H_RuzhuDetail
                              where a.Status == 0
                              && a.FangJianHao == hf.RoomID
                              select a).SingleOrDefault();
                    if (old != null)
                        old.Status = 1;
                    //添加新房房费及入住信息关联到入住单
                    H_RuzhuDetail detail = new H_RuzhuDetail();
                    detail.FangJianHao = hf.NewRoomID;
                    detail.OrderGuid = hf.OrderGuid;
                    detail.ShijiFangjia = hf.NewPrice;
                    detail.Status = 0;
                    detail.XingMing = hf.Name;
                    detail.ArriveTime = hf.ArriveTime;
                    detail.LeaveTime = hf.LeaveDate;
                    detail.YuanFangJia = hf.Price;
                    detail.ZheKouLv = hf.Rebate;
                    db.H_RuzhuDetail.AddObject(detail);
                    db.SaveChanges();
                    //将新房标记为在客
                    changer.SetRoomStatus(hf.NewRoomID,"在客");
                    result = "0";

                
            }
            return result;
        }

        public HuanFangDan GetInitByFH(string fh)
        {
            HuanFangDan result = new HuanFangDan();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //房间信息
                var detail = (from a in db.H_RuzhuDetail
                             where a.FangJianHao == fh
                             && a.Status == 0
                             select a).SingleOrDefault();
                if (detail != null)
                {
                    result.Price = detail.ShijiFangjia == null ? 0 : (decimal)detail.ShijiFangjia;
                    result.Name = detail.XingMing;
                    result.LeaveDate = detail.LeaveTime;
                    result.RoomID = fh;
                    result.Rebate = (double)(detail.ZheKouLv == null ? 0 : detail.ZheKouLv);
                    result.OrderGuid = detail.OrderGuid;
                    result.ZhengjianHaoma = detail.ZhengjianHaoma;
                    result.ArriveTime = detail.ArriveTime;

                    //入住单信息 
                    var order = (from o in db.H_RuzhuOrder
                                where o.OrderGuid == detail.OrderGuid
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

    public class HuanFangDan
    {
        public System.Int32 AutoID { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public System.Decimal Deposit { get; set; }
        public System.Decimal Money { get; set; }
        public System.DateTime LeaveDate { get; set; }
        public string RoomID { get; set; }
        public System.Decimal Price { get; set; }
        public System.Double Rebate { get; set; }
        public string RoomLevel { get; set; }
        public string NewRoomID { get; set; }
        public System.Decimal NewPrice { get; set; }
        public string Remark { get; set; }
        public System.Guid OrderGuid { get; set; }

        public string ZhengjianHaoma { get; set; }

        public System.DateTime ArriveTime { get; set; }


    }
}
