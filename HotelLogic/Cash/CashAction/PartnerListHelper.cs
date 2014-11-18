using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;
using HotelLogic.Setting;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace HotelLogic.Cash.CashAction
{
    public class PartnerListHelper
    {
        #region 初始化
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Roles { get; set; }
        private static PartnerListHelper _instance;

        public static PartnerListHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new PartnerListHelper() : _instance;
            }
        }
        private PartnerListHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 增加单位挂账费用记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode CreateRelation(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                string RZOrderID = parameters["OrderID"];
                string UnitID = parameters["UnitID"];
                string Datarows = parameters["Rows"];
                string JZID = parameters["JZID"];

                //判断入住订单是否已经结账退房
                resultEntiry = CashCommHelper.IsOrderNotEnd(Guid.Parse(RZOrderID));
                if (!resultEntiry.Flag) return resultEntiry;

                using (TransactionScope scope = new TransactionScope())
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        try
                        {
                           
                            List<RZFeiYongModel> datarows = EntityHelper.GetEntityInfoFromJsonPage<RZFeiYongModel>(Datarows);
                            if (datarows != null)
                            {
                                foreach (RZFeiYongModel item in datarows)
                                {
                                    RZ_CreditProtocolUnit cpunit = new RZ_CreditProtocolUnit();
                                    cpunit.ID = Guid.NewGuid();
                                    cpunit.CP_OrderGUID = Guid.Parse(RZOrderID);
                                    cpunit.CP_Feiyong = item.RZ_ID;
                                    cpunit.CP_UnitID = int.Parse(UnitID);
                                    cpunit.CP_JZID = Guid.Parse(JZID);
                                    cpunit.CreateTime = DateTime.Now;
                                    cpunit.CreateUser = UserName;
                                    _db.RZ_CreditProtocolUnit.AddObject(cpunit);

                                }
                                //如果结账ID不存在即以传过来的结账ID号生成一些记录，并只保存一个记账金额
                                //保存主要结账数据
                                RZ_JieZhangTuiFang ZTModel = new RZ_JieZhangTuiFang();
                                ZTModel.ID = Guid.Parse(JZID);
                                ZTModel.JZ_OrderGUID = Guid.Parse(RZOrderID);
                                RZ_JieZhangTuiFang EntityModel = (from a in _db.RZ_JieZhangTuiFang
                                                                  where a.ID == ZTModel.ID
                                                                  select a).SingleOrDefault();
                                if (EntityModel == null)
                                {
                                    ZTModel.CreateTime = DateTime.Now;
                                    ZTModel.CreateUser = UserName;
                                    ZTModel.UpdateTime = DateTime.Now;
                                    ZTModel.UpdateUser = UserName;
                                    _db.RZ_JieZhangTuiFang.AddObject(ZTModel);
                                }

                                _db.SaveChanges();
                                scope.Complete();                               
                            }
                        }
                        catch (Exception e)
                        {
                            resultEntiry.Flag = false;
                            resultEntiry.Where = "CreateRelation";
                            resultEntiry.Message = e.Message;
                            return resultEntiry;
                        }
                    }
                }
                resultEntiry.Flag = true;
                resultEntiry.Message = "执行成功！";
                resultEntiry.value = GetRelationMoney(RZOrderID, JZID, UnitID).value;
            }
            return resultEntiry;
        }
        /// <summary>
        /// 取消单位挂账费用记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode DeleteRelation(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        string RZOrderID = parameters["OrderID"];
                        string UnitID = parameters["UnitID"];
                        string Datarows = parameters["Rows"];
                        string JZID = parameters["JZID"];
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(Guid.Parse(RZOrderID));
                        if (!resultEntiry.Flag) return resultEntiry;
                        List<RZFeiYongModel> datarows = EntityHelper.GetEntityInfoFromJsonPage<RZFeiYongModel>(Datarows);
                        if (datarows != null)
                        {
                            foreach (RZFeiYongModel item in datarows)
                            {
                                RZ_CreditProtocolUnit newitem = new RZ_CreditProtocolUnit();
                                var SelItem = (from a in _db.RZ_CreditProtocolUnit
                                           where a.CP_Feiyong == item.RZ_ID
                                           select a).SingleOrDefault();                              
                                _db.ObjectStateManager.ChangeObjectState(SelItem, System.Data.EntityState.Deleted);
                            }
                            _db.SaveChanges();
                            resultEntiry.Flag = true;
                            resultEntiry.Message = "执行成功！";
                            resultEntiry.value = GetRelationMoney(RZOrderID, JZID, UnitID).value;
                        }
                    }
                    catch (Exception e)
                    {
                        resultEntiry.Flag = false;
                        resultEntiry.Where = "DeleteRelation";
                        resultEntiry.Message = e.Message;
                    }
                }
            }
            return resultEntiry;
        }

        public ResultMode GetRelationMoney(string orderid, string jzid, string unitid)
        {
            ResultMode newRm = new ResultMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT sum([RZ_Money]) AS UnitMoney
                                      FROM [RZ_FeiYongDetailsView] FV
                                      INNER JOIN dbo.RZ_CreditProtocolUnit CPUR ON FV.RZ_ID=CPUR.CP_Feiyong
                                      WHERE CPUR.CP_OrderGUID=@OrderGUID and CPUR.CP_UnitID=@UnitID
                                          and CPUR.CP_JZID=@CP_JZID  ";//[RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { 
                        new SqlParameter { ParameterName = "OrderGUID", Value =orderid }
                        ,new SqlParameter { ParameterName = "UnitID", Value =unitid }
                        ,new SqlParameter { ParameterName = "CP_JZID", Value =jzid }
                    };

                    var tempresult = _db.ExecuteStoreQuery<UnitMoneyModel>(sqltxt, parm);
                    List<UnitMoneyModel> datalist = tempresult.ToList<UnitMoneyModel>();
                    newRm.Flag = true;
                    newRm.Message = "执行成功！";
                    if (datalist != null && datalist.Count >= 0)
                    {
                        newRm.value = datalist[0].UnitMoney;
                    }
                    else
                    {
                        newRm.value = 0;
                    }
                }
                catch (Exception e)
                {
                    newRm.Flag = false;
                    newRm.Message = "执行成功！";
                    newRm.value = 0;
                }
            }
            return newRm;
        }
        //根据OrderGuid号来查询相关记录
        public List<AgreeCompany> GetPartnerList(NameValueCollection parameters, out int total)
        {
            List<AgreeCompany> resultdatalist = new List<AgreeCompany>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    AgreeCompany newFP = new AgreeCompany();
                    EntityHelper.FormDataToDataObject(newFP, parameters);
                    var datalist = from a in _db.AgreeCompany
                                   orderby a.Company
                                   select a;
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList();
                    if (resultdatalist.Count > page.PageSize)
                    {
                        resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                    }
                   // resultdatalist.Add(new AgreeCompany());
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    total = 0;
                }

            }
            return resultdatalist;
        }
        //根据OrderGuid号来查询相关记录
        public List<RZFeiYongModel> GetFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZFeiYongModel> resultdatalist = new List<RZFeiYongModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    BuFengJieZhangHelper _bzhelper = BuFengJieZhangHelper.Instance;
                    resultdatalist = _bzhelper.GetRiZhuFeiYongList(parameters, out total);
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    total = 0;
                }

            }
            return resultdatalist;
        }
        //获取已经挂账的费用ID用于进行比较筛选
        public List<UnitFeiYongID> GetHaveExitFeiYongList(NameValueCollection parameters, out int total)
        {
            List<UnitFeiYongID> datalist = new List<UnitFeiYongID>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string RZOrderID = parameters["RZ_OrderGuid"];
                    string sqltxt = @"SELECT CP_Feiyong as FeiYongID
                                      FROM  dbo.RZ_CreditProtocolUnit CPUR 
                                      WHERE CPUR.CP_OrderGUID=@OrderGUID ";//[RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { 
                        new SqlParameter { ParameterName = "OrderGUID", Value =RZOrderID }
                    };

                    var tempresult = _db.ExecuteStoreQuery<UnitFeiYongID>(sqltxt, parm);
                    datalist = tempresult.ToList<UnitFeiYongID>();
                    total = datalist.Count;
                    return datalist;
                }
                catch (Exception e)
                {
                    total = 0;
                    return datalist;
                }
            }
        #endregion
        }
        //根据OrderGuid\JZID\UniID号来查询所有相关费用明细信息
        public List<RZFeiYongModel> GetOrderRelationFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZFeiYongModel> resultdatalist = new List<RZFeiYongModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZFeiYongModel newRM = new RZFeiYongModel();
                    string RZOrderID = parameters["RZ_OrderGuid"];
                    string UnitID = parameters["UnitID"];
                    string JZID = parameters["JZID"];
                    EntityHelper.FormDataToDataObject(newRM, parameters);
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
                                    FROM [RZ_FeiYongDetailsView] FV
                                    INNER JOIN  [RZ_CreditProtocolUnit] CU ON FV.RZ_ID=CU.CP_Feiyong
                                    WHERE CU.CP_OrderGUID=@OrderGUID AND CU.CP_UnitID=@UnitID AND CU.CP_JZID=@JZID ";//[RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { 
                        new SqlParameter { ParameterName = "OrderGUID", Value =RZOrderID },
                         new SqlParameter { ParameterName = "UnitID", Value =UnitID},
                        new SqlParameter { ParameterName = "JZID", Value = JZID }};

                    var tempresult = _db.ExecuteStoreQuery<RZFeiYongModel>(sqltxt, parm);
                    List<RZFeiYongModel> datalist = tempresult.ToList<RZFeiYongModel>();
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RZFeiYongModel>();
                    if (resultdatalist.Count > page.PageSize)
                    {
                        resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                    }
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    total = 0;
                }

            }
            return resultdatalist;
        }
    }
    #region 相关实体类
    public class UnitMoneyModel
    {
        public decimal UnitMoney { get; set; }
    }
    public class UnitFeiYongID
    {
        public Guid FeiYongID { get; set; }
    }

    #endregion

}
