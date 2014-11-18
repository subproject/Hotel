using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.CommonModel;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelLogic.Cash.CashAction
{
    public class XuNiZhangDanHelper
    {
        #region
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Roles { get; set; }
        private static XuNiZhangDanHelper _instance;
        public static XuNiZhangDanHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new XuNiZhangDanHelper() : _instance;
            }
        }
        private XuNiZhangDanHelper() { }
        #endregion
        #region 数据操作
        /// <summary>
        /// 增加或修改相关虚拟账单信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdate(NameValueCollection parameters)
        {
            ResultMode resultModel = new ResultMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_XuNiZhangDan newXZ = new RZ_XuNiZhangDan();
                    EntityHelper.FormDataToDataObject(newXZ, parameters);
                    _db.RZ_XuNiZhangDan.AddObject(newXZ);
                    newXZ.RuZhuTian =(decimal)(DateTime.Parse( newXZ.LiDianTime.ToString()) -
                        DateTime.Parse( newXZ.DaoDianTime.ToString())).TotalDays;
                    if (newXZ.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        newXZ.ID = Guid.NewGuid();
                        newXZ.CreateTime = DateTime.Now;
                        newXZ.CreateUser = UserName;
                    }
                    else
                    {
                        _db.ObjectStateManager.ChangeObjectState(newXZ, System.Data.EntityState.Modified);
                        newXZ.UpdateTime = DateTime.Now;
                        newXZ.UpdateUser = UserName;
                    }
                    _db.SaveChanges();
                    resultModel.Flag = false;
                    resultModel.Message = "执行成功!";
                    resultModel.Where = "AddOrUpdate";
                }
                catch (Exception e)
                {
                    resultModel.Flag = false;
                    resultModel.Message = "执行失败!" + e.Message;
                    resultModel.Where = "AddOrUpdate";
                }
            }
            return resultModel;
        }
        /// <summary>
        /// 删除相关虚拟账单信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode Delete(NameValueCollection parameters)
        {
            ResultMode resultModel = new ResultMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_XuNiZhangDan newXZ = new RZ_XuNiZhangDan();
                    EntityHelper.FormDataToDataObject(newXZ, parameters);
                    _db.RZ_XuNiZhangDan.AddObject(newXZ);
                    _db.ObjectStateManager.ChangeObjectState(newXZ, System.Data.EntityState.Deleted);

                    _db.SaveChanges();
                    resultModel.Flag = false;
                    resultModel.Message = "执行成功!";
                    resultModel.Where = "AddOrUpdate";
                }
                catch (Exception e)
                {
                    resultModel.Flag = false;
                    resultModel.Message = "执行失败!" + e.Message;
                    resultModel.Where = "AddOrUpdate";
                }
            }
            return resultModel;
        }
        #endregion

        #region 数据查询
        /// <summary>
        /// 查询统计入住订单结账信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<RZ_XuNiZhangDan> GetOrderSumJieZhangInfoList(NameValueCollection parameters, out int total)
        {

            List<RZ_XuNiZhangDan> resultdatalist = new List<RZ_XuNiZhangDan>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_XuNiZhangDan newXZ = new RZ_XuNiZhangDan();
                    EntityHelper.FormDataToDataObject(newXZ, parameters);
                    var datalist = (from a in _db.RZ_XuNiZhangDan
                                    where a.OrderGuid == newXZ.OrderGuid
                                    select a);
                    resultdatalist = datalist.ToList<RZ_XuNiZhangDan>();
                    if (resultdatalist != null && resultdatalist.Count > 0)
                    {
                        string pageinfostr = parameters["PageInfo"];
                        HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                        total = datalist.Count();
                        //跳过的总条数  
                        int totalNum = (page.PageNumber - 1) * page.PageSize;
                        resultdatalist = resultdatalist.Skip(totalNum).ToList<RZ_XuNiZhangDan>();
                        if (resultdatalist.Count > page.PageSize)
                        {
                            resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                        }
                    }
                    else
                    {
                        total = 0;
                        GetXuNiZhangDan(newXZ);
                        resultdatalist.Add(newXZ);
                    }
                }
                catch (Exception e)
                {
                    total = 0;
                }
            }
            return resultdatalist;
        }
        /// <summary>
        /// 获取相关订单费用值
        /// </summary>
        /// <param name="XNZDEntity"></param>
        public void GetXuNiZhangDan(RZ_XuNiZhangDan XNZDEntity)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    //订单信息
                    H_RuzhuOrder orderinfo = (from a in _db.H_RuzhuOrder
                                              where a.OrderGuid == XNZDEntity.OrderGuid
                                              select a).SingleOrDefault();
                    if (orderinfo != null)
                    {
                        XNZDEntity.Customer = orderinfo.XingMing;
                        XNZDEntity.No = orderinfo.AutoID;
                        XNZDEntity.DaoDianTime = orderinfo.DaodianTime;
                        XNZDEntity.LiDianTime = orderinfo.LidianTime;
                        XNZDEntity.YaJing = orderinfo.YaJin;
                        XNZDEntity.RuZhuTian = decimal.Parse(string.Format("{0:N2}", (orderinfo.LidianTime - orderinfo.DaodianTime).TotalDays));
                        XNZDEntity.CaoZhou = orderinfo.CaoZuoYuan;
                    }
                    //订单的房号和单价及其它信息i
                    string str = @"SELECT od.[OrderGuid],  
                                           [FangJianHao]=stuff((SELECT ','+[FangJianHao] FROM [H_RuzhuDetail]
                                             WHERE [OrderGuid]=od.[OrderGuid] for xml path('')),1,1,'')  
                                           ,[YuanFangJia]=stuff((SELECT ','+ CONVERT(nvarchar(20), [YuanFangJia]) FROM [H_RuzhuDetail]
                                             WHERE [OrderGuid]=od.[OrderGuid] for xml path('')),1,1,'') 
                                            ,[ShijiFangjia]=stuff((SELECT ','+  CONVERT(nvarchar(20),[ShijiFangjia]) FROM [H_RuzhuDetail]
                                             WHERE [OrderGuid]=od.[OrderGuid] for xml path('')),1,1,'')  
                                       FROM [H_RuzhuDetail]  od                                       
                                       WHERE OD.OrderGuid=@OrderGuid
                                       GROUP BY OD.[OrderGuid]";

                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "OrderGuid", Value = XNZDEntity.OrderGuid } };
                    List<OrderFanHaoModel> orderfanjians = _db.ExecuteStoreQuery<OrderFanHaoModel>(str, parm).ToList();
                    if (orderfanjians != null && orderfanjians.Count > 0)
                    {
                        OrderFanHaoModel ofhmEntiry = orderfanjians[0];
                        XNZDEntity.FangHaos = ofhmEntiry.FangJianHao;
                        XNZDEntity.FangJia = ofhmEntiry.ShijiFangjia;
                    }

                    //消费信息房费
                    string sqltxt = @"SELECT [OrderGuid]
                                              ,[FeiYongType]
                                              ,[SumMoney]
                                          FROM [RZ_OrderAllSumMoneyView]
                                          WHERE [OrderGuid]=@OrderGuid  ";
                    var rz_parm = new DbParameter[] { new SqlParameter { ParameterName = "OrderGuid", Value = XNZDEntity.OrderGuid } };
                    List<OrderSumFeiYongMode> feiyongModels = _db.ExecuteStoreQuery<OrderSumFeiYongMode>(sqltxt, rz_parm).ToList();
                    if (feiyongModels != null && feiyongModels.Count > 0)
                    {
                        foreach (OrderSumFeiYongMode ordersf in feiyongModels)
                        {
                            switch ( ordersf.FeiYongType.Trim())
                            {
                                case "客房用品":
                                    XNZDEntity.ShangPingFei = ordersf.SumMoney;
                                    break;
                                case "损物赔偿":
                                    XNZDEntity.PeiShun = ordersf.SumMoney;
                                    break;
                                case "房费":
                                    XNZDEntity.FanFei = ordersf.SumMoney;
                                    break;
                                case "其它费用":
                                    XNZDEntity.Qita = ordersf.SumMoney;
                                    break;
                                case "早餐费用":
                                    XNZDEntity.CanFei = ordersf.SumMoney;
                                    break;
                            }
                        }
                        XNZDEntity.XiaoFeiHeJi= XNZDEntity.ShangPingFei+XNZDEntity.PeiShun + XNZDEntity.FanFei+XNZDEntity.CanFei+XNZDEntity.Qita;
                    }

                    //结账信息
                    string jzsqltxt = @"SELECT  [JZ_OrderGUID]
                                                  ,SUM([JZ_Money]) AS [JZ_Money]
                                                  ,SUM([JZ_Consumption])AS[JZ_Consumption]
                                                  ,SUM([JZ_Deposit])AS[JZ_Deposit]
                                                  ,SUM([JZ_Surplus])AS [JZ_Surplus]
                                                  ,SUM([JZ_Preferential])AS [JZ_Preferential]
                                                  ,SUM([JZ_Accounts])AS [JZ_Accounts]
                                                  ,SUM([JZ_Paid])AS [JZ_Paid]
                                                  ,SUM([JZ_DepositDeduct])AS [JZ_DepositDeduct]
                                                  ,[LidianTime]
                                                  ,[DaodianTime]
                                                  ,[FangHao]
                                                  ,[Status]
                                                  ,[YaJin]
                                              FROM [RZ_OrerALLJieZhangInfoView]
                                              WHERE [JZ_OrderGUID]=@JZ_OrderGUID
                                              GROUP BY  [JZ_OrderGUID],[LidianTime]
                                                        ,[DaodianTime]
                                                        ,[FangHao]
                                                        ,[Status]
                                                        ,[YaJin]  ";
                    var rz_jzparm = new DbParameter[] { new SqlParameter { ParameterName = "JZ_OrderGUID", Value = XNZDEntity.OrderGuid } };
                    List<OrderSumJieZhangFeiYongMode> jiezhangModels = _db.ExecuteStoreQuery<OrderSumJieZhangFeiYongMode>(jzsqltxt, rz_jzparm).ToList();
                    if (jiezhangModels != null && jiezhangModels.Count > 0)
                    {
                        OrderSumJieZhangFeiYongMode jzsummode = jiezhangModels[0];
                        XNZDEntity.JieZhangShouKuan = jzsummode.JZ_Money;
                        XNZDEntity.TuiXianJin = jzsummode.JZ_Surplus;
                        XNZDEntity.JieZhangShouKuan = jzsummode.JZ_Money + jzsummode.JZ_DepositDeduct;
                        XNZDEntity.TuiXianJin = jzsummode.JZ_Surplus;
                    }
                    //查询与结账相关的协议单位
                    string unittxt = @"SELECT  distinct ag.Company
                                       FROM [RZ_CreditProtocolUnit] RU
                                       INNER JOIN dbo.AgreeCompany AG ON RU.[CP_UnitID]=AG.AutoID
                                       WHERE RU.CP_OrderGUID=@CP_OrderGUID";
                    var rz_unitpayparm = new DbParameter[] { new SqlParameter("CP_OrderGUID", XNZDEntity.OrderGuid) };
                    List<UnitCompanyMode> jzunitModels = _db.ExecuteStoreQuery<UnitCompanyMode>(unittxt, rz_unitpayparm).ToList();
                   
                    //结账支付信息
                    string jzspayqltxt = @"SELECT [JZ_OrderGUID]
                                                  ,[JZD_PayType]
                                                  ,SUM([JZD_Money]) as JZD_Money
                                                  ,JZD_Receipt=stuff((
                                                    SELECT ','+[JZD_Receipt] 
                                                    FROM [RZ_OrderJieZhangInfoView]
                                                    WHERE [JZ_OrderGUID]=OJZ.JZ_OrderGUID 
                                                          AND JZD_PayType=OJZ.JZD_PayType
                                                    for xml path('')),1,1,'') 
                                            FROM [RZ_OrderJieZhangInfoView] OJZ
                                            WHERE JZ_OrderGUID=@JZ_OrderGUID
                                            GROUP BY [JZ_OrderGUID],[JZD_PayType]  ";
                    var rz_jzpayparm = new DbParameter[] { new SqlParameter("JZ_OrderGUID", XNZDEntity.OrderGuid ) };
                    List<OrderFeiYongPayDetailsMode> jzpayModels = _db.ExecuteStoreQuery<OrderFeiYongPayDetailsMode>(jzspayqltxt, rz_jzpayparm).ToList();
                    if (jzpayModels != null && jzpayModels.Count > 0)
                    {
                        foreach (OrderFeiYongPayDetailsMode jzspayummode in jzpayModels)
                        {
                            if (jzspayummode.JZD_PayType.Contains("现金"))
                            {
                                XNZDEntity.XianJing = jzspayummode.JZD_Money;
                            }
                            if (jzspayummode.JZD_PayType.Contains("支票"))
                            {
                                XNZDEntity.ZhiPiaoJingE = jzspayummode.JZD_Money;
                                XNZDEntity.ZhiPiaoHaoMa = jzspayummode.JZD_Receipt;
                            }
                            if (jzspayummode.JZD_PayType.Contains("单位"))
                            {
                                XNZDEntity.ZhiZhangJingE = jzspayummode.JZD_Money;
                                if (jzunitModels != null && jzunitModels.Count>0)
                                {
                                    string units = "";
                                    foreach (UnitCompanyMode uc in jzunitModels)
                                    {
                                        units += uc.Company+",";
                                    }
                                    units.TrimEnd(',');
                                    XNZDEntity.XieYiDanWei = units;
                                }

                            }
                            if (jzspayummode.JZD_PayType.Contains("存储"))
                            {
                                XNZDEntity.ChuZhiCar = jzspayummode.JZD_Money;
                            }
                            if (jzspayummode.JZD_PayType.Contains("信用卡"))
                            {
                                XNZDEntity.XinYongCar = jzspayummode.JZD_Money;
                            }
                            if (jzspayummode.JZD_PayType.Contains("代金券"))
                            {
                                XNZDEntity.DaiJingQuang = jzspayummode.JZD_Money;
                            }
                            if (jzspayummode.JZD_PayType.Contains("借记"))
                            {
                                XNZDEntity.JieJiCar = jzspayummode.JZD_Money;
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
            }
        }
        /// <summary>
        /// 查询统计入住订单结账信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<RZ_XuNiZhangDan> GetNullEntityList(NameValueCollection parameters, out int total)
        {

            List<RZ_XuNiZhangDan> resultdatalist = new List<RZ_XuNiZhangDan>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_XuNiZhangDan newXZ = new RZ_XuNiZhangDan();                   
                    resultdatalist.Add(newXZ);
                    total = 1;
                }
                catch (Exception e)
                {
                    total = 0;
                }
            }
            return resultdatalist;
        }
        /// <summary>
        /// 查询单笔虚拟账单
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public RZ_XuNiZhangDan GetXuNiZhangDanEntity(NameValueCollection parameters)
        {

            RZ_XuNiZhangDan newXZ = new RZ_XuNiZhangDan();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_XuNiZhangDan whereXZ = new RZ_XuNiZhangDan();
                    EntityHelper.FormDataToDataObject(whereXZ,parameters);
                    newXZ = (from a in _db.RZ_XuNiZhangDan
                             where a.ID == whereXZ.ID
                             select a).SingleOrDefault();
                    if (newXZ == null)
                    {
                        newXZ = new RZ_XuNiZhangDan();
                        newXZ.OrderGuid = whereXZ.OrderGuid;
                        GetXuNiZhangDan(newXZ);
                    }
                }
                catch (Exception e)
                {
                   
                }
            }
            return newXZ;
        }

        #endregion
    }


    /// <summary>
    /// 入住订单相关房费信息
    /// </summary>
    public class OrderFanHaoModel
    {
        public Guid OrderGuid { get; set; }
        public string FangJianHao { get; set; }
        public string YuanFangJia { get; set; }
        public string ShijiFangjia { get; set; }
    }
    /// <summary>
    /// 入住订单相关费用统计信息
    /// </summary>
    public class OrderSumFeiYongMode
    {
        public Guid OrderGuid { get; set; }
        public string FeiYongType { get; set; }
        public decimal SumMoney { get; set; }
    }
    /// <summary>
    /// 入住订单相关结账统计信息
    /// </summary>
    public class OrderSumJieZhangFeiYongMode
    {
        public Guid JZ_OrderGUID { get; set; }
        public decimal JZ_Money { get; set; }
        public decimal JZ_Consumption { get; set; }
        public decimal JZ_Deposit { get; set; }
        public decimal JZ_Surplus { get; set; }
        public decimal JZ_Preferential { get; set; }
        public decimal JZ_Accounts { get; set; }
        public decimal JZ_Paid { get; set; }
        public decimal JZ_DepositDeduct { get; set; }
        public DateTime LidianTime { get; set; }
        public DateTime DaodianTime { get; set; }
        public string FangHao { get; set; }
        public Int16 Status { get; set; }
        public decimal YaJin { get; set; }
    }
    /// <summary>
    /// 入住订单相关结账支付信息
    /// </summary>
    public class OrderFeiYongPayDetailsMode
    {
        public Guid JZ_OrderGUID { get; set; }
        public string JZD_PayType { get; set; }
        public decimal JZD_Money { get; set; }
        public string JZD_Receipt { get; set; }
    }
    /// <summary>
    /// 入住订单相关协议单位信息
    /// </summary>
    public class UnitCompanyMode
    {
        public string Company { get; set; }
    }
}
