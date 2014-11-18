using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Cash
{
    public class RoomFeeHelper
    {
        #region
        private static RoomFeeHelper _instance;
        public static RoomFeeHelper Instance
        {
            get { 
                return _instance == null ? _instance = new RoomFeeHelper() : _instance;
            }
        }
        private RoomFeeHelper() { }
        #endregion

        //根据房号计算该房房费
        //根据需要转成service
        public string FFeeMaker(string fh)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //get ruzhu info by fh
                var rzinfo = (from r in db.H_RuzhuDetail
                             where r.FangJianHao == fh
                             && r.Status == 0
                              select r).FirstOrDefault(); ;

                if (rzinfo != null)
                {
                    //delete old status is 0 fee
                    var oldfees = from a in db.Cash_RunningDetails
                                  where a.Status == "未结"
                                  && a.Remark.Contains("房费")
                                  select a;
                    foreach (var oldfee in oldfees)
                    {
                        db.Cash_RunningDetails.DeleteObject(oldfee);
                    }
                    //calculate fee according datetime.now and arrive time
                    TimeSpan ts = System.DateTime.Now - rzinfo.ArriveTime;
                    if (ts.Days > 0)
                    {
                        //入住当天的费用入住时刻生成，这里只负责非当日房费的产生
                        //超时费用
                        if (System.DateTime.Now.Hour >= 14 && System.DateTime.Now.Hour < 18)
                        {
                            Cash_RunningDetails temp = new Cash_RunningDetails();
                            temp.Price = 60;
                            temp.Remark = "超时房费";
                            temp.KM = "超时房费";
                            temp.CustomerName = rzinfo.XingMing;
                            temp.RoomNo = fh;
                            temp.RunningTime = System.DateTime.Now;
                            temp.OrderGuid = rzinfo.OrderGuid;
                            temp.Status = "未结";
                            db.Cash_RunningDetails.AddObject(temp);
                            //db.SaveChanges();

                        }
                        if (System.DateTime.Now.Hour >= 18)
                        {
                            Cash_RunningDetails temp = new Cash_RunningDetails();
                            temp.Price = 60;
                            temp.Remark = "房费(" + System.DateTime.Now.ToShortDateString() + ")";
                            temp.KM = "房费";
                            temp.CustomerName = rzinfo.XingMing;
                            temp.RoomNo = fh;
                            temp.RunningTime = System.DateTime.Now;
                            temp.OrderGuid = rzinfo.OrderGuid;
                            temp.Status = "未结";
                            db.Cash_RunningDetails.AddObject(temp);
                            //db.SaveChanges();

                        }
                        //间隔时间大于1天，即入住天数至少2晚以上
                        if (ts.Days > 1)
                        {
                            for (int i = 0; i < ts.Days; i++)
                            {
                                Cash_RunningDetails temp = new Cash_RunningDetails();
                                temp.Price = rzinfo.ShijiFangjia;
                                temp.Remark = "房费(" + rzinfo.ArriveTime.AddDays(i).Date.ToShortDateString() + ")";
                                temp.RoomNo = fh;
                                temp.KM = "房费";
                                temp.RunningTime = System.DateTime.Now;
                                temp.OrderGuid = rzinfo.OrderGuid;
                                temp.Status = "未结";
                                temp.CustomerName = rzinfo.XingMing;
                                db.Cash_RunningDetails.AddObject(temp);
                            }
                        }
                        db.SaveChanges();
                    }
                }
            }
            return result;
        }
    }
}
