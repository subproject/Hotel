using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Register
{
    public class RegisterHelper
    {
        //Merge Register
        //参数,两个账单,一个被Merge,一个待Merge
        //得到orderguid,将被merge的detail信息的orderguid,update成待Merge的orderguid
        public string MergeOrder(List<Guid> bemerge)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //将bemerge的detail信息改成merge的guid
                var beDetails = from a in db.H_RuzhuDetail
                                where bemerge.Contains(a.OrderGuid)
                                select a;
                foreach (var be in beDetails)
                {
                    be.OrderGuid = bemerge.First();
                }
                
                //将合并后的帐号status置为1
                var orders = from a in db.H_RuzhuOrder
                             where bemerge.Contains(a.OrderGuid)
                             //&& a.OrderGuid != bemerge.First()
                             select a;
                foreach (var order in orders)
                {
                    if(order.OrderGuid!=bemerge.First())
                        order.Status = 1;
                }

                db.SaveChanges();
                result = "0";
            }
            return result;
        }

        //参数,将被拆分的帐单guid
        //属于该账单的将被拆分的房间号列表
        //复制一个帐单,将被拆分的房间的orderguid,update成新帐单的guid
        public string SplitOrder(Guid orderguid, List<string> fhs)
        {
            string result = "-1";
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var oldorder = (from a in db.H_RuzhuOrder
                                where a.OrderGuid == orderguid
                                select a).SingleOrDefault();
                if (oldorder != null)
                {
                    //复制一个新order
                    H_RuzhuOrder neworder = new H_RuzhuOrder();
                    neworder.BaoMi = oldorder.BaoMi;
                    neworder.BeiZhu = oldorder.BeiZhu;
                    neworder.BiaozhunFangjia = oldorder.BiaozhunFangjia;
                    neworder.ChangBao = oldorder.ChangBao;
                    neworder.ChangTu = oldorder.ChangTu;
                    neworder.DaodianTime = oldorder.DaodianTime;
                    neworder.DianHua = oldorder.DianHua;
                    neworder.DiZhi = oldorder.DiZhi;
                    neworder.FangHao = oldorder.FangHao;
                    neworder.FangjianLeixing = oldorder.FangjianLeixing;
                    neworder.FukuanFangshi = oldorder.FukuanFangshi;
                    neworder.HuiYuanKa = oldorder.HuiYuanKa;
                    neworder.IsMain = oldorder.IsMain;
                    neworder.JiaoxingFuwu = oldorder.JiaoxingFuwu;
                    neworder.JiFen = oldorder.JiFen;
                    neworder.KerenLeibie = oldorder.KerenLeibie;
                    neworder.LidianTime = oldorder.LidianTime;
                    neworder.MainGuid = oldorder.MainGuid;
                    neworder.OrderGuid = System.Guid.NewGuid();
                    neworder.ShiHua = oldorder.ShiHua;
                    neworder.ShijiFangjia = oldorder.ShijiFangjia;
                    neworder.ShougongDanhao = oldorder.ShougongDanhao;
                    neworder.Status = oldorder.Status;
                    neworder.TeQuanRen = oldorder.TeQuanRen;
                    neworder.TuanDui = oldorder.TuanDui;
                    neworder.XieyiDanwei = oldorder.XieyiDanwei;
                    neworder.XingBie = oldorder.XingBie;
                    neworder.XingMing = oldorder.XingMing;
                    neworder.YaJin = oldorder.YaJin;
                    neworder.ZheKouLv = oldorder.ZheKouLv;
                    neworder.ZhengJianHao = oldorder.ZhengJianHao;
                    neworder.ZhengjianLeibie = oldorder.ZhengjianLeibie;
                    neworder.ZhongDian = oldorder.ZhongDian;

                    db.H_RuzhuOrder.AddObject(neworder);

                    //更新每个房间的orderguid
                    foreach (var fh in fhs)
                    {
                        var detail = (from d in db.H_RuzhuDetail
                                     where d.FangJianHao==fh
                                     && d.OrderGuid==orderguid
                                     select d).ToList().FirstOrDefault();
                        if (detail != null)
                        {
                            detail.OrderGuid = neworder.OrderGuid;
                        }
                    }

                    //update db
                    db.SaveChanges();
                    result = "0";
                }
            }
            return result;
        }
    }
}
