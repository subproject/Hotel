using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Register
{
    public class HuanfangHelper
    {
        //读取当前随客信息,分页部分
        public List<SuiKe> ReadPageSuiKe(int page, int rows)
        {
            List<SuiKe> result = new List<SuiKe>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuSuike
                            orderby a.FangHao descending
                            where a.Status == 0
                            select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    SuiKe s = new SuiKe();
                    s.FangHao = t.FangHao;
                    s.XingBie = t.XingBie;
                    s.XingMing = t.XingMing;
                    s.CarNum = t.CarNum;
                    s.DiZhi = t.DiZhi;
                    s.Remark = t.Remark;
                    s.ShenfenZheng = t.ShenfenZheng;
                    result.Add(s);
                }
            }
            return result;
        }

        //读取当前随客信息,全部
        public List<SuiKe> ReadAllSuiKe()
        {
            List<SuiKe> result = new List<SuiKe>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuSuike
                           orderby a.FangHao descending
                           where a.Status == 0
                           select a;
                foreach (var t in temp)
                {
                    //转成viewmodel
                    SuiKe s = new SuiKe();
                    s.FangHao = t.FangHao;
                    s.XingBie = t.XingBie;
                    s.XingMing = t.XingMing;
                    s.CarNum = t.CarNum;
                    s.DiZhi = t.DiZhi;
                    s.Remark = t.Remark;
                    s.ShenfenZheng = t.ShenfenZheng;
                    result.Add(s);
                }
            }
            return result;
        }
    }
}
