using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.Setting;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;
using System.Collections.Specialized;
using HotelLogic.Cash;
using HotelLogic.Cash.CashAction;
using HotelLogic.CommonModel;

namespace HotelLogic.FrontDesk
{
    /// <summary>
    /// 入住订单相关费用信息查询及操作
    /// </summary>
    public class OrdersJieZhangHelper
    {
        #region 初始化
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        private static OrdersJieZhangHelper _instance;

        public static OrdersJieZhangHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new OrdersJieZhangHelper() : _instance;
            }
        }
        private OrdersJieZhangHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 查询收退款明细列表
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<RZ_ShoukuanDai> GetShouTuKuanList(NameValueCollection parameters)
        {
            try
            {
                ShoukuanHelper shhelper = ShoukuanHelper.Instance;
                int total = 0;
                return shhelper.GetShouTuKuanList(parameters, out total);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// 相关相关费用明细表
        /// </summary>
        /// <param name="orderguid"></param>
        /// <returns></returns>
        public List<HotelLogic.Cash.CashAction.RZFeiYongModel> GetRiZhuFeiYongList(Guid orderguid)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT [RZ_ID]
                                          ,[RZ_FeiYongCode]
                                          ,[RZ_FeiYongName]
                                          ,[RZ_FeiyongType]
                                          ,[RZ_FYType]
                                          ,[RZ_OrderGuid]
                                          ,[RZ_Money]
                                          ,[RZ_Remark]
                                          ,[RZ_ReciptNo]
                                          ,[RZ_RoomNo]
                                          ,[RZ_Customer]
                                          ,[RZ_RunnTime]
                                          ,[RZ_Operator]
                                          ,[RZ_Payment]
                                          ,[RZ_Status]
                                          ,[RR_RedMoney]
                                      FROM [RZ_FeiYongDetailsView]
                                      WHERE [RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_OrderGuid", Value = orderguid } };

                    var tempresult = _db.ExecuteStoreQuery<HotelLogic.Cash.CashAction.RZFeiYongModel>(sqltxt, parm);
                    List<HotelLogic.Cash.CashAction.RZFeiYongModel> datalist = tempresult.ToList<HotelLogic.Cash.CashAction.RZFeiYongModel>();

                    return datalist;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 相关相关费用明细表
        /// </summary>
        /// <param name="orderguid"></param>
        /// <returns></returns>
        public HotelLogic.Cash.CashAction.RZFeiYongModel GetRiZhuFeiYongEntity(NameValueCollection parameters)
        {
            string rzid = parameters["RZ_ID"];
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT [RZ_ID]
                                          ,[RZ_FeiYongCode]
                                          ,[RZ_FeiYongName]
                                          ,[RZ_FeiyongType]
                                          ,[RZ_FYType]
                                          ,[RZ_OrderGuid]
                                          ,[RZ_Money]
                                          ,[RZ_Remark]
                                          ,[RZ_ReciptNo]
                                          ,[RZ_RoomNo]
                                          ,[RZ_Customer]
                                          ,[RZ_RunnTime]
                                          ,[RZ_Operator]
                                          ,[RZ_Payment]
                                          ,[RZ_Status]
                                          ,[RR_RedMoney]
                                      FROM [RZ_FeiYongDetailsView]
                                      WHERE [RZ_ID]=@RZ_ID ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_ID", Value = rzid } };

                    var tempresult = _db.ExecuteStoreQuery<HotelLogic.Cash.CashAction.RZFeiYongModel>(sqltxt, parm);
                    List<HotelLogic.Cash.CashAction.RZFeiYongModel> datalist = tempresult.ToList<HotelLogic.Cash.CashAction.RZFeiYongModel>();
                    if (datalist != null & datalist.Count > 0)
                    {
                        return datalist[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
        //获取OrderGUID全部费用明细
        public string GetAllOrderFeiYongInfo(NameValueCollection parameters)
        {
            string orderguid = parameters["RZ_OrderGuid"];
            if (!string.IsNullOrEmpty(orderguid))
            {
                List<HotelLogic.Cash.CashAction.RZFeiYongModel> Datarows = GetRiZhuFeiYongList(Guid.Parse(orderguid));
                return GetFeiYongTreeJsonStr(Datarows);
            }
            else
            {
                return "";
            }
        }
        //获取OrderGUID全部转账费用明细
        public string GetZhuanZhangOrderFeiYongInfo(NameValueCollection parameters)
        {
            string orderguid = parameters["RZ_OrderGuid"];
            if (!string.IsNullOrEmpty(orderguid))
            {
                SanKeZhuanZhangHelper zzhhelper = SanKeZhuanZhangHelper.Instance;
                int total = 0;
                List<HotelLogic.Cash.CashAction.RZFeiYongModel> Datarows = zzhhelper.GetZhuangChuFeiYongByOrderGuid(parameters, out total);
                return GetFeiYongTreeJsonStr(Datarows);
            }
            else
            {
                return "";
            }
        }
        //获取OrderGUID全部已结账费用明细
        public string GetJieZhangOrderFeiYongInfo(NameValueCollection parameters)
        {
            string orderguid = parameters["RZ_OrderGuid"];
            if (!string.IsNullOrEmpty(orderguid))
            {
                string pageinfostr = parameters["PageInfo"];
                HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);

                BuFengJieZhangHelper bfjhelper = BuFengJieZhangHelper.Instance;
                int total = 0;
                List<HotelLogic.Cash.CashAction.RZFeiYongModel> Datarows =
                    bfjhelper.GetRiZhuFeiYongList(Guid.Parse(orderguid), new Guid(), "结账", page.PageNumber, page.PageSize, out total);
                return GetFeiYongTreeJsonStr(Datarows);
            }
            else
            {
                return "";
            }
        }
        //获取OrderGUID相关收退款明细
        public string GetShouTuKuanInfo(NameValueCollection parameters)
        {
            string orderguid = parameters["SK_OrderGuid"];
            if (!string.IsNullOrEmpty(orderguid))
            {
                List<RZ_ShoukuanDai> Datarows = GetShouTuKuanList(parameters);
                if (Datarows != null)
                {
                    return GetShouTuKuanTreeJsonStr(Datarows);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 将费用List转化Tree格式的字符串
        /// </summary>
        /// <param name="AllFeiYongData"></param>
        /// <returns></returns>
        public string GetFeiYongTreeJsonStr(List<HotelLogic.Cash.CashAction.RZFeiYongModel> AllFeiYongData)
        {
            var TopLeveData = (from a in AllFeiYongData
                               select a.RZ_FYType).Distinct().ToList();
            var allmoney = (from a in AllFeiYongData
                            select a.RZ_Money).Sum();
            var allRedmoney = (from a in AllFeiYongData
                            select a.RR_RedMoney).Sum();
            string dataformat = @"'id':'{0}','RZ_FeiYongCode':'{1}','RZ_FeiYongName':'{2}','IsCheck':'{3}','state':'{4}','parentid':'{5}',";
            dataformat += "'RZ_Money':'{6}','RZ_Remark':'{7}','RZ_ReciptNo':'{8}','RZ_RoomNo':'{9}','RZ_Customer':'{10}',";
            dataformat += "'RZ_RunnTime':'{11}', 'RZ_Operator':'{12}', 'RZ_Payment':'{13}','RZ_Status':'{14}','RR_RedMoney':'{15}',";
            string rootid = Guid.NewGuid().ToString();
            string leve1Guid = Guid.NewGuid().ToString();
            string leve2Guid = Guid.NewGuid().ToString();
            string valuetxt = @"{'Rows':[{ " + string.Format(dataformat, rootid, "", "全部费用", "", "", "", allmoney, "", "", "", "", "", "", "", "", allRedmoney) + " 'children':[";
            StringBuilder SbRJson = new StringBuilder();
            SbRJson.Append(valuetxt);
            int leve1int = 0;
            int leve2int = 0;
            int leve3int = 0;
            foreach (var leve1 in TopLeveData)
            {
                var SecondLeveData = (from a in AllFeiYongData
                                      where a.RZ_FYType == leve1.ToString()
                                      select a.RZ_FeiyongType).Distinct().ToList();
                var Leve1money = (from a in AllFeiYongData
                                  where a.RZ_FYType == leve1.ToString()
                                  select a.RZ_Money).Sum();
                var Leve1Redmoney = (from a in AllFeiYongData
                                  where a.RZ_FYType == leve1.ToString()
                                  select a.RR_RedMoney).Sum();
                leve1int++;
                SbRJson.Append("{" + string.Format(dataformat, leve1Guid, "", leve1, "", "", rootid, Leve1money, "", "", "", "", "", "", "", "", Leve1Redmoney));
                SbRJson.Append(" 'children':[");
                leve2int = 0;
                foreach (var leve2 in SecondLeveData)
                {
                    var dataList = (from a in AllFeiYongData
                                    where a.RZ_FYType == leve1.ToString()
                                      && a.RZ_FeiyongType == leve2
                                    select a).ToList<HotelLogic.Cash.CashAction.RZFeiYongModel>();
                    var Leve2money = (from a in AllFeiYongData
                                      where a.RZ_FYType == leve1.ToString()
                                        && a.RZ_FeiyongType == leve2
                                      select a.RZ_Money).Sum();
                    var Leve2Redmoney = (from a in AllFeiYongData
                                      where a.RZ_FYType == leve1.ToString()
                                        && a.RZ_FeiyongType == leve2
                                      select a.RR_RedMoney).Sum();
                    leve2int++;
                    SbRJson.Append("{" + string.Format(dataformat, leve2Guid, "", leve2, "", "closed", leve1Guid, Leve2money, "", "", "", "", "", "", "", "", Leve2Redmoney));
                    SbRJson.Append(" 'children':[");
                    leve3int = 0;
                    foreach (HotelLogic.Cash.CashAction.RZFeiYongModel item in dataList)
                    {
                        leve3int++;
                        SbRJson.Append("{" + string.Format(dataformat, item.RZ_ID, item.RZ_FeiYongCode, item.RZ_FeiYongName, "", "", leve2Guid,
                            item.RZ_Money, item.RZ_Remark, item.RZ_ReciptNo, item.RZ_RoomNo, item.RZ_Customer, item.RZ_RunnTime,
                            item.RZ_Operator, item.RZ_Payment, item.RZ_Status,item.RR_RedMoney));
                        if (leve3int == dataList.Count)
                        {
                            SbRJson.Append(" 'children':[]}");
                        }
                        else
                        {
                            SbRJson.Append(" 'children':[]},");
                        }
                    }
                    if (leve2int == SecondLeveData.Count)
                    {
                        SbRJson.Append(" ]}");
                    }
                    else
                    {
                        SbRJson.Append(" ]},");
                    }
                    leve2Guid = Guid.NewGuid().ToString();
                }
                if (leve1int == TopLeveData.Count)
                {
                    SbRJson.Append(" ]}");
                }
                else
                {
                    SbRJson.Append(" ]},");
                }
                leve1Guid = Guid.NewGuid().ToString();
            }
            SbRJson.Append(" ]}]}");
            return SbRJson.ToString();
        }
        /// <summary>
        /// 将收退款List转化Tree格式的字符串
        /// </summary>
        /// <param name="AllFeiYongData"></param>
        /// <returns></returns>
        public string GetShouTuKuanTreeJsonStr(List<RZ_ShoukuanDai> AllFeiYongData)
        {
            var TopLeveData = (from a in AllFeiYongData
                               select a.SK_Type).Distinct().ToList();
            var allmoney = (from a in AllFeiYongData
                            select a.SK_Money).Sum();
            string dataformat = @"'id':'{0}','SK_PayType':'{1}','SK_Type':'{2}','IsCheck':'{3}','state':'{4}','parentid':'{5}',";
            dataformat += "'SK_Money':'{6}','SK_YiShouMoney':'{7}','SK_YingShouMoney':'{8}','SK_LianfanHao':'{9}','SK_PayTime':'{10}',";
            dataformat += "'SK_Remark':'{11}', 'SK_Receipt':'{12}','SK_RZCode':'{13}',";
            string rootid = Guid.NewGuid().ToString();
            string leve1Guid = Guid.NewGuid().ToString();
            string valuetxt = @"{'Rows':[{ " + string.Format(dataformat, rootid, "", "全部款项", "", "", "", allmoney, "", "", "", "", "", "","") + " 'children':[";
            StringBuilder SbRJson = new StringBuilder();
            SbRJson.Append(valuetxt);
            int leve1int = 0;
            int leve2int = 0;
            foreach (var leve1 in TopLeveData)
            {
                var dataList = (from a in AllFeiYongData
                                where a.SK_Type == leve1.ToString()
                                select a).ToList();
                var Leve1money = (from a in AllFeiYongData
                                  where a.SK_Type == leve1.ToString()
                                  select a.SK_Money).Sum();
                leve1int++;
                SbRJson.Append("{" + string.Format(dataformat, leve1Guid, leve1, "", "closed", "", rootid, Leve1money, "", "", "", "", "", "", ""));
                SbRJson.Append(" 'children':[");
                leve2int = 0;
                foreach (RZ_ShoukuanDai item in dataList)
                {
                    leve2int++;
                    SbRJson.Append("{" + string.Format(dataformat, item.ID, item.SK_PayType, item.SK_Type, "", "", leve1Guid,
                        item.SK_Money, item.SK_YiShouMoney, item.SK_YingShouMoney, item.SK_LianfanHao, item.SK_PayTime, item.SK_Remark,
                        item.SK_Receipt, item.SK_RZCode));
                    if (leve2int == dataList.Count)
                    {
                        SbRJson.Append(" 'children':[]}");
                    }
                    else
                    {
                        SbRJson.Append(" 'children':[]},");
                    }
                }
                if (leve1int == TopLeveData.Count)
                {
                    SbRJson.Append(" ]}");
                }
                else
                {
                    SbRJson.Append(" ]},");
                }
                leve1Guid = Guid.NewGuid().ToString();
            }
            SbRJson.Append(" ]}]}");
            return SbRJson.ToString();
        }

       /// <summary>
       /// 获取入住订单综合信息
       /// </summary>
       /// <param name="parameters"></param>
       /// <returns></returns>
        public Dictionary<string, object> GetOrderComplexInfo(NameValueCollection parameters)
        {
            string rooms = "";
            string xitongremark = "";
            Dictionary<string, object> resultobject = new Dictionary<string, object>();
            H_RuzhuOrder order = new H_RuzhuOrder();
            OrdersHelper helper = new OrdersHelper();
            OrderInfoMode orderdatainfo = new OrderInfoMode();
            if (string.IsNullOrEmpty(parameters["OrderGuid"]))
            {
                order = helper.ReadOrderByFH(parameters["fanhao"]);
            }
            else
            {
                order = helper.ReadOrderByOrderGuid(parameters["OrderGuid"]);
            }

            JieZhangTuFangHelper jzhelper = HotelLogic.Cash.CashAction.JieZhangTuFangHelper.Instance;
            orderdatainfo = jzhelper.GetOrderInfo(order.OrderGuid.ToString());
            SanKeZhuanZhangHelper zzhelper = SanKeZhuanZhangHelper.Instance;
            var RM = zzhelper.GetOrderRoomList(order.OrderGuid.ToString());
            
            FaPiaoManagerHelper fphelper = FaPiaoManagerHelper.Instance;
            decimal? fpmoney = fphelper.GetFaPiaoList(order.OrderGuid).Sum(a => a.FP_Money);
            if (RM != null)
            {
                rooms = RM.FangJianHao;
            }
            if (fpmoney == null)
            {
                xitongremark = "[已开发票:0元]";
            }
            else
            {
                xitongremark = "[已开发票:" + fpmoney.ToString() + "元]";
            }
            if (order.Status == 0)
            {
                if (order.LidianTime<DateTime.Now)
                {
                    order.Status = 2;
                }
            }
            resultobject.Add("Order", order);
            resultobject.Add("orderdata", orderdatainfo);
            resultobject.Add("rooms", rooms);
            resultobject.Add("xtRemark", xitongremark);
            return resultobject;
        }
       
        #endregion
    }

    #region 相关数据Model
    public class FeiYongTypeMode
    {
        public string FeiYongType { get; set; }
    }
    #endregion
}