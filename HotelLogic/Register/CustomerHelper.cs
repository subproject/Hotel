using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Register
{
    //在店客人管理
    public class CustomerHelper
    {
        #region 在店客人
        //读取当前在店的客人信息,status为0,每个房号一个,显示主账单信息,主账单人和主账单编号,分页部分
        public List<Customer> ReadPageCustomer(int page, int rows)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                            orderby a.FangJianHao descending
                           where a.Status == 0
                            select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;

                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;
                    }
                    result.Add(c); 
                }
            }
            return result;
        }

        //读取当前在店的客人信息,status为0,每个房号一个,显示主账单信息,主账单人和主账单编号,全部
        /// <summary>
        /// 把相对的写，你写些什么》》》》》》》》》？page是什么，rows又是什么分页写上去
        /// </summary>
        /// <param name="page">有多少页</param>  //页码还是什么
        /// <param name="rows">每页有多少条数据</param>
        /// <param name="filter">传过来的参数</param>闯过来的参数是什么int类型的数字啊
        /// <returns></returns>
        public List<Customer> ReadAllCustomer(int page, int rows, CustomerFilter filter)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuDetail
                           orderby a.FangJianHao descending
                            where a.Status == 0
                            select a;  //这里是获取全部信息吗 获取全部的在店客人信息，包括
                if (!string.IsNullOrEmpty(filter.MainName))  //名称不为空时
                {
                    //temp = from a in temp
                    //       where a.XingMing.Contains(filter.MainName) && a.ZhuCong == "主"
                    //       select a;
                    temp = temp.Where(s => s.ZhuCong == "主" && s.XingMing.Contains(filter.MainName));
                }
                if (!string.IsNullOrEmpty(filter.fangjianjibie))  //房间级别不为空时
                {
                    //temp = from a in temp
                    //       where a.JiBie.Contains(filter.fangjianjibie)
                    //       select a;
                    temp = temp.Where(s=>s.JiBie.Contains(filter.fangjianjibie));
                }

                if (!string.IsNullOrEmpty(filter.ZhengjianCard))  //证件
                {
                    temp = from a in temp
                           where a.ZhengjianHaoma.Contains(filter.ZhengjianCard)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.ZhengjianAddress))  //证件地址
                {
                    temp = from a in temp
                           where a.ZhengjianDizhi.Contains(filter.ZhengjianAddress)
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.Name))  //姓名
                {
                    temp = from a in temp
                           where a.XingMing.Contains(filter.Name)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.Fh))//房号
                {
                    temp = from a in temp
                           where a.FangJianHao.Contains(filter.Fh)
                           select a;
                }
                if (filter.Begin.Year > 2001)//来店时间
                {
                    temp = from a in temp
                           where a.ArriveTime > filter.Begin
                           select a;
                }
                if (filter.End.Year > 2001)//离店时间
                {
                    temp = from a in temp
                           where a.ArriveTime < filter.End
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.dianhuahaoma))//电话号码
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.DianHua.Contains(filter.dianhuahaoma)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.beizhu))//备注
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.BeiZhu.Contains(filter.beizhu)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.guoji))//国籍
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.GuoJi.Contains(filter.guoji)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xiaoshouyuan))//销售员
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.XiaoShouYuan.Contains(filter.xiaoshouyuan)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.MainNumber))//主房号
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.FangHao.Contains(filter.MainNumber)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.FukuanFangshi))//付款方式
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.FukuanFangshi.Contains(filter.FukuanFangshi)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.kerenleibie))//客人类型
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.KerenLeibie.Contains(filter.kerenleibie)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.huiyuanka))//会员卡
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.HuiYuanKa.Contains(filter.huiyuanka)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xieyidanwei))//协议单位
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.XieyiDanwei.Contains(filter.xieyidanwei)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.caozuoyuan))//操作员
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.CaoZuoYuan.Contains(filter.caozuoyuan)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.chepaihao))//车牌号
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuSuike on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.CarNum.Contains(filter.chepaihao)
                           select a;
                }
                temp = temp.Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;
                    if (t.Status == 0)
                    {
                        c.kerenzhuangtai = "在店";
                    }
                    else if (t.Status == 1)
                    {
                        c.kerenzhuangtai = "离店";
                    }
                    else
                    {
                        c.kerenzhuangtai = "未结离店";
                    }
                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault(); 
                    if (order != null)
                    {

                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;

                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                    }
                    result.Add(c);
                }
            }
            return result;
        }


        #endregion


        public List<Customer> ReadAll()
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuDetail
                           select a;
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;
                    if (t.Status == 0)
                    {
                        c.kerenzhuangtai = "在店";
                    }
                    else if (t.Status == 1)
                    {
                        c.kerenzhuangtai = "离店";
                    }
                    else
                    {
                        c.kerenzhuangtai = "未结离店";
                    }
                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;

                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                    }
                    result.Add(c);
                }
            }
            return result;
        }

        public List<Customer> ReadPageAll(int page, int rows, CustomerFilter filter)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                string str = " 1=1";
                var temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where a.AutoID>0
                             select new 
                                {
                                   a.ArriveTime,
                                   a.AutoID,
                                   a.FangJianHao,
                                   a.JiBie,
                                   a.LeaveTime,
                                   a.OrderGuid,
                                   a.OrderID,
                                   a.RuZhuLeiXing,
                                   a.ShijiFangjia,
                                   a.Status,
                                   a.XingBie,
                                   a.XingMing,
                                   a.YuanFangJia,
                                   a.ZheKouLv,
                                   a.ZhengjianDizhi,
                                   a.ZhengjianHaoma,
                                   a.ZhengjianLeixing,
                                   a.ZhuCong,
                                   b.BaoMi,
                                   b.BeiZhu,
                                   b.CaoZuoYuan,
                                   b.ChangBao,
                                   b.ChangTu,
                                   b.DaodianTime,
                                   b.DianHua,
                                   b.DiZhi,
                                   b.FangHao,
                                   b.FukuanFangshi,
                                   b.GuoJi,
                                   b.HuiYuanKa,
                                   b.JiaoxingFuwu,
                                   b.JiFen,
                                   b.KerenLeibie,                                  
                                   b.ShiHua,
                                   b.ShougongDanhao,
                                   b.TuanDui,
                                   b.XiaoShouYuan,
                                   b.XieyiDanwei,
                                   b.YaJin,
                                   b.ZhengJianHao,
                                   b.ZhengjianLeibie,
                                   b.ZhongDian
                                } ;
                if (!string.IsNullOrEmpty(filter.MainName))
                {
                    temp = from a in temp
                           where a.XingMing==filter.MainName //&& a.ZhuCong.Contains("主")
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.fangjianjibie))
                {
                    temp = from a in temp
                           where a.JiBie==filter.fangjianjibie
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.ZhengjianCard))
                {
                    temp = from a in temp
                           where a.ZhengjianHaoma==filter.ZhengjianCard
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.ZhengjianAddress))
                {
                    temp = from a in temp
                           where a.ZhengjianDizhi==filter.ZhengjianAddress
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    temp = from a in temp
                           where a.XingMing==filter.Name
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.Fh))
                {
                    temp = from a in temp
                           where a.FangJianHao==filter.Fh
                           select a;
                }
                if (filter.Begin.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime > filter.Begin
                           select a;
                }
                if (filter.End.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime < filter.End
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.RuZhuleixing))
                {
                    temp = from a in temp
                           orderby a.FangJianHao descending
                           where a.RuZhuLeiXing==filter.RuZhuleixing
                           select a;
                }
                
                if (!string.IsNullOrEmpty(filter.dianhuahaoma))
                {
                    temp = from a in temp
                          
                           orderby a.FangJianHao descending
                           where a.DianHua==filter.dianhuahaoma
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.beizhu))
                {
                    temp = from a in temp
                           where a.BeiZhu.Contains(filter.beizhu)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.guoji))
                {
                    temp = from a in temp
                           where a.GuoJi==filter.guoji
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xiaoshouyuan))
                {
                    temp = from a in temp
                           where a.XiaoShouYuan==filter.xiaoshouyuan
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.MainNumber))
                {
                    temp = from a in temp
                           where a.FangHao==filter.MainNumber
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.FukuanFangshi))
                {
                    temp = from a in temp
                           where a.FukuanFangshi==filter.FukuanFangshi
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.kerenleibie))
                {
                    temp = from a in temp                           
                           where a.KerenLeibie==filter.kerenleibie
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.huiyuanka))
                {
                    temp = from a in temp
                           where a.HuiYuanKa==filter.huiyuanka
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xieyidanwei))
                {
                    temp = from a in temp
                           where a.XieyiDanwei==filter.xieyidanwei
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.caozuoyuan))
                {
                    temp = from a in temp
                           where a.CaoZuoYuan==filter.caozuoyuan
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.FangType))
                {
                    int status = 0;
                    if (filter.FangType == "1")
                    {
                        status=0;
                    }
                    else if (filter.FangType == "2")
                    {
                        status = 1;
                    }
                    else
                    {
                        status = 2;
                    }
                    temp = from a in temp
                           where a.Status == status
                           select a;
                }
               
                if (!string.IsNullOrEmpty(filter.chepaihao))
                {
                    temp = from a in temp
                           join b in db.H_RuzhuSuike on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.CarNum==filter.chepaihao
                           select a;
                }


                temp = temp.Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ShenfengDiZhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;
                    c.ZhuCong=t.ZhuCong;
                    if (t.Status == 0)
                    {
                        c.kerenzhuangtai = "在店";
                    }
                    else if (t.Status == 1)
                    {
                        c.kerenzhuangtai = "离店";
                    }
                    else
                    {
                        c.kerenzhuangtai = "未结离店";//2
                    }
                    c.ruzhuleixing = t.RuZhuLeiXing;
                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.AutoID == t.OrderID
                                 select o).ToList().FirstOrDefault();
                
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;

                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                        c.beizhu = order.BeiZhu;
                        c.baomi = order.BaoMi == true ? "是" : "否";
                        c.ZhengjianDizhi = order.DiZhi;
                        c.CaoZuoYuan = order.CaoZuoYuan;
                    }
                    result.Add(c);
                }
            }
            return result;
        }

        #region 离店客人管理
        //读取退房人信息,分页部分
        public List<Customer> ReadPageTuiFang(int page, int rows)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = (from a in db.H_RuzhuDetail
                            orderby a.FangJianHao descending
                            where a.Status == 1
                            select a).Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;

                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;
                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                    }
                    result.Add(c);
                }
            }
            return result;
        }

        //读取全部退房人信息
        public List<Customer> ReadAllTuiFang()
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuDetail
                           where a.Status == 1
                           select a;
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;

                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;
                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                    }
                    result.Add(c);
                }
            }
            return result;
        }
        #endregion
        #region 查询离店客人信息 by filter
        //读取退房人信息,分页部分
        public List<Customer> ReadPageTuiFang(int page, int rows, CustomerFilter filter)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuDetail
                            orderby a.FangJianHao descending
                            where a.Status == 1
                            select a;
                if (!string.IsNullOrEmpty(filter.MainName))
                {
                    temp = from a in temp
                           where a.XingMing.Contains(filter.MainName) && a.ZhuCong == "主"
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.fangjianjibie))
                {
                    temp = from a in temp
                           where a.JiBie.Contains(filter.fangjianjibie)
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.ZhengjianCard))
                {
                    temp = from a in temp
                           where a.ZhengjianHaoma.Contains(filter.ZhengjianCard)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.ZhengjianAddress))
                {
                    temp = from a in temp
                           where a.ZhengjianDizhi.Contains(filter.ZhengjianAddress)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    temp = from a in temp
                           where a.XingMing.Contains(filter.Name)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.Fh))
                {
                    temp = from a in temp
                           where a.FangJianHao.Contains(filter.Fh)
                           select a;
                }
                if (filter.Begin.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime > filter.Begin
                           select a;
                }
                if (filter.End.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime < filter.End
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.dianhuahaoma))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.DianHua.Contains(filter.dianhuahaoma)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.beizhu))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.BeiZhu.Contains(filter.beizhu)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.guoji))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.GuoJi.Contains(filter.guoji)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xiaoshouyuan))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.XiaoShouYuan.Contains(filter.xiaoshouyuan)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.MainNumber))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.FangHao.Contains(filter.MainNumber)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.FukuanFangshi))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.FukuanFangshi.Contains(filter.FukuanFangshi)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.kerenleibie))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.KerenLeibie.Contains(filter.kerenleibie)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.huiyuanka))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.HuiYuanKa.Contains(filter.huiyuanka)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.xieyidanwei))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.XieyiDanwei.Contains(filter.xieyidanwei)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.caozuoyuan))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuOrder on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.CaoZuoYuan.Contains(filter.caozuoyuan)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.chepaihao))
                {
                    temp = from a in db.H_RuzhuDetail
                           join b in db.H_RuzhuSuike on a.OrderGuid equals b.OrderGuid
                           orderby a.FangJianHao descending
                           where b.CarNum.Contains(filter.chepaihao)
                           select a;
                }
                temp = temp.Skip((page - 1) * rows).Take(rows);
                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;
                    c.fangjianjibie = t.JiBie;
                    if (t.Status == 0)
                    {
                        c.kerenzhuangtai = "在店";
                    }
                    else if (t.Status == 1)
                    {
                        c.kerenzhuangtai = "离店";
                    }
                    else
                    {
                        c.kerenzhuangtai = "未结离店";
                    }
                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;

                        c.fukuanfangshi = order.FukuanFangshi;
                        c.YaJin = order.YaJin;
                        c.kerenleibie = order.KerenLeibie;
                        c.guoji = order.GuoJi;
                        c.xiaoshouyuan = order.XiaoShouYuan;
                        c.huiyuanka = order.HuiYuanKa;
                        c.dianhuahaoma = order.DianHua;
                        c.tuandui = order.TuanDui;
                        c.xieyidanwei = order.XieyiDanwei;
                        c.tequanren = order.TeQuanRen;
                    }
                    result.Add(c);
                }
            }
            return result;
        }

        //读取全部退房人信息
        public List<Customer> ReadAllTuiFang(CustomerFilter filter)
        {
            List<Customer> result = new List<Customer>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temp = from a in db.H_RuzhuDetail
                           where a.Status == 1
                           select a;

                if (filter.YuanFangFree != null)
                {
                    temp = from a in temp
                           where a.YuanFangJia==filter.YuanFangFree
                           select a;
                }

                if (filter.ZheKou != null)
                {
                    temp = from a in temp
                           where a.ZheKouLv==filter.ZheKou
                           select a;
                }
                if (filter.ShiJiFree != null)
                {
                    temp = from a in temp
                           where a.ShijiFangjia==filter.ShiJiFree
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.ZhengjianType.ToString()))
                {
                    temp = from a in temp
                           where a.ZhengjianLeixing.Contains(filter.ZhengjianType)
                           select a;
                }

                if (!string.IsNullOrEmpty(filter.ZhengjianCard.ToString()))
                {
                    temp = from a in temp
                           where a.ZhengjianHaoma.Contains(filter.ZhengjianCard)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.ZhengjianAddress.ToString()))
                {
                    temp = from a in temp
                           where a.ZhengjianDizhi.Contains(filter.ZhengjianAddress)
                           select a;
                }

                //if (!string.IsNullOrEmpty(filter.MainName.ToString()))
                //{
                //    temp = from a in temp
                //           where a.MainFH.Contains(filter.MainName)
                //           select a;
                //}
                //if (!string.IsNullOrEmpty(filter.MainNumber.ToString()))
                //{
                //    temp = from a in temp
                //           where a.MainOrderMan.Contains(filter.MainNumber)
                //           select a;
                //}


                if (!string.IsNullOrEmpty(filter.Name))
                {
                    temp = from a in temp
                           where a.XingMing.Contains(filter.Name)
                           select a;
                }
                if (!string.IsNullOrEmpty(filter.Fh))
                {
                    temp = from a in temp
                           where a.FangJianHao.Contains(filter.Fh)
                           select a;
                }
                if (filter.Begin.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime > filter.Begin
                           select a;
                }
                if (filter.End.Year > 2001)
                {
                    temp = from a in temp
                           where a.ArriveTime < filter.End
                           select a;
                }

                foreach (var t in temp)
                {
                    //转成viewmodel
                    Customer c = new Customer();
                    c.FangJianHao = t.FangJianHao;
                    c.ShijiFangjia = t.ShijiFangjia;
                    c.XingBie = t.XingBie;
                    c.XingMing = t.XingMing;
                    c.YuanFangJia = t.YuanFangJia;
                    c.ZheKouLv = t.ZheKouLv;
                    c.ZhengjianDizhi = t.ZhengjianDizhi;
                    c.ZhengjianHaoma = t.ZhengjianHaoma;
                    c.ZhengjianLeixing = t.ZhengjianLeixing;

                    //从账单表中获取帐单数据
                    var order = (from o in db.H_RuzhuOrder
                                 where o.OrderGuid == t.OrderGuid
                                 select o).ToList().FirstOrDefault();
                    if (order != null)
                    {
                        c.MainOrderMan = order.XingMing;
                        c.OnBoardTime = order.DaodianTime;
                        c.LeaveTime = order.LidianTime;
                        c.MainFH = order.FangHao;
                    }
                    result.Add(c);
                }
            }
            return result;
        }
        #endregion


    //    /// <summary>
    //    /// Read all the zd_ZjLx info 
    //    /// </summary>
    //    /// <returns></returns>
    //    public static List<zd_ZjLxModel> ReadCardDesignAll()
    //    {
    //        //Module for View
    //        List<zd_ZjLxModel> Mb = new List<zd_ZjLxModel>();
    //        using (HotelDBEntities _db = new HotelDBEntities())
    //        {
    //            var result = from a in _db.zd_ZjLx
    //                         select a;
    //            foreach (var r in result)
    //            {
    //                zd_ZjLxModel temp = new zd_ZjLxModel();
    //                temp.AutoID = r.AutoID;
    //                temp.zjlx = r.zjlx;
    //                Mb.Add(temp);
    //            }

    //        }
    //        return Mb;
    //    }
   }
    //public class zd_ZjLxModel
    //{
    //    public Int32 AutoID { get; set; }
    //    public string zjlx { get; set; }
    //}

    public class Customer
    {
        public string FangJianHao{get;set;}
        public string XingMing{get;set;}
        public string XingBie{get;set;}
        public string ZhengjianLeixing{get;set;}
        public string ZhengjianHaoma{get;set;}
        public string ZhengjianDizhi{get;set;}
        public decimal? YuanFangJia{get;set;}
        public double? ZheKouLv{get;set;}
        public decimal? ShijiFangjia{get;set;}

        public string kerenzhuangtai { get; set; }
        public string fangjianjibie { get; set; }

        //下面三个取自账单表
        public string MainOrderMan{get;set;}
        public DateTime OnBoardTime { get; set; }
        public DateTime LeaveTime { get; set; }
        public string MainFH { get; set; }

        public string fukuanfangshi { get; set; }
        public decimal? YaJin { get; set; }
        public string  kerenleibie{get;set;}
        public string ruzhuleixing { get; set; }
        public string guoji { get; set; }
        public string xiaoshouyuan { get; set; }
        public string huiyuanka { get; set; }
       public string dianhuahaoma { get; set; }
        public string tuandui { get; set; }
        public string xieyidanwei { get; set; }
        public string beizhu { get; set; }
        public string baomi { get; set; }
        public string tequanren { get; set; }

        public string ZhuCong { get; set; }    
        public string CaoZuoYuan { get; set; }        
        public string KerenLeibie { get; set; }
        public string XieyiDanwei { get; set; }
        public string ShenfengDiZhi { get; set; }  
        public string suoshutuandui { get; set; }
        public string YDDanHao { get; set; }
        public string YDremark { get; set; }
        public string YDDanWei { get; set; }
        public string YDKeHu { get; set; }
        public string YDTel { get; set; }
        public string YDDate { get; set; }
        public string YDCaoZuoYuan { get; set; }
        public decimal? DingJin { get; set; }
        
    }
    /// <summary>
    /// 查询入住信息历史，封装查询条件
    /// </summary>
    public class CustomerFilter
    {
        public string Name { get; set; }
        public string Fh { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }

        public string FangType { get; set; }
        public decimal? YuanFangFree { get; set; }
        public double? ZheKou { get; set; }
        public decimal? ShiJiFree { get; set; }
        public string ZhengjianType { get; set; }
        public string ZhengjianCard { get; set; }
        public string ZhengjianAddress { get; set; }
        public string MainNumber { get; set; }
        public string MainName { get; set; }

        public string kerenzhuangtai { get; set; }
        public string fangjianjibie { get; set; }

        public string FukuanFangshi { get; set; }
        public decimal yajin { get; set; }
        public string kerenleibie { get; set; }
        public string ruzhuleixing { get; set; }
        public string guoji { get; set; }
        public string xiaoshouyuan { get; set; }
        public string huiyuanka { get; set; }
        public string dianhuahaoma { get; set; }
        public string tuandui { get; set; }
        public string xieyidanwei { get; set; }
        public string beizhu { get; set; }
        public string baomi { get; set; }

        public string chepaihao { get; set; }
        public string caozuoyuan { get; set; }

        public string RuZhuleixing { get; set; }
    }
}
