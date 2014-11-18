using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.HouseStatus
{
    public class LiuLiangHelper
    {
        //获取房间级别概要信息
        public List<JBSummer> GetJBSummers()
        {
            List<JBSummer> result = new List<JBSummer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var lists = from a in db.Zd_Fj
                           group a by a.f_jb into g
                           select new 
                           {
                               g.Key,
                               Count=g.Count()
                           };
                foreach (var list in lists)
                {
                    JBSummer temp = new JBSummer();
                    temp.JBName = list.Key;
                    temp.Count = list.Count;
                    result.Add(temp);
                }
            }
            return result;
        }

        public string GetStatus(JBSummer jb, DateTime date)
        {
            string result = string.Empty;
            using (HotelDBEntities db = new HotelDBEntities())
            {
                //默认为0占用
                result = jb.Count.ToString()+"  0.00%";

                //DateTime end = date.AddDays(1).AddHours(14);
                DateTime keytime = date.AddHours(23).AddMinutes(59);

                //从预定单里得到信息 
                int yd = (from a in db.Yd_Pf
                          join y in db.Yd_Dd on a.YDNum equals y.YDNum
                          where a.f_jb == jb.JBName
                          && a.Status == 0
                          && y.dr < keytime
                          && y.lr > keytime
                          select a).Count();

                //从入住表里得到信息 
                int rz = (from a in db.H_RuzhuDetail
                          where a.JiBie == jb.JBName
                          && a.Status == 0
                          && a.ArriveTime < keytime
                          && a.LeaveTime > keytime
                          select a).Count();

                //得到最终的占用信息
                result = (jb.Count - yd - rz).ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ((yd + rz) * 1.0 / jb.Count * 100).ToString("F2") + "%";  
            }
            return result;
        }
    }
    public class JBSummer
    {
        public string JBName{get;set;}
        public int Count{get;set;}
    }
}
