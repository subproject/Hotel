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
    public class ShoukuanHelper
    {
        #region
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Roles { get; set; }
        private static ShoukuanHelper _instance;
        public static ShoukuanHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new ShoukuanHelper() : _instance;
            }
        }
        private ShoukuanHelper() { }
        #endregion
        /// <summary>
        /// 新增或更新客户费用记录操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdate(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode();
            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        RZ_ShoukuanDai SKModel = new RZ_ShoukuanDai();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(SKModel, parameters);
                        _db.RZ_ShoukuanDai.AddObject(SKModel);
                        if (SKModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(SKModel, System.Data.EntityState.Added);
                            SKModel.ID = System.Guid.NewGuid();
                            DateTime now = DateTime.Now;
                            SKModel.SK_PayTime = now;                           
                            SKModel.SK_AutoReceipt = "SK" + string.Format("{0}{1}{2}{3}{4}{5}{6}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond);
                            SKModel.CreateTime = DateTime.Now;
                            SKModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(SKModel, System.Data.EntityState.Modified);
                        }
                        SKModel.UpdateTime = DateTime.Now;
                        SKModel.UpdateUser = UserName;
                        _db.SaveChanges();
                        resultEntiry.Flag = true;
                        resultEntiry.Message = "执行成功！";
                        resultEntiry.value = SKModel.ID.ToString();
                    }
                    catch (Exception e)
                    {
                        resultEntiry.Flag = false;
                        resultEntiry.Where = "AddorUpdate";
                        resultEntiry.Message = e.Message;
                    }

                }
            }
            return resultEntiry;

        }
        //删除客房费用记录
        public ResultMode Delete(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode();
            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        //赋值到对象
                        RZ_ShoukuanDai newSK = new RZ_ShoukuanDai();
                        EntityHelper.FormDataToDataObject(newSK, parameters);
                        _db.RZ_ShoukuanDai.AddObject(newSK);
                        _db.ObjectStateManager.ChangeObjectState(newSK, System.Data.EntityState.Deleted);
                        _db.SaveChanges();
                        resultEntiry.Flag = true;
                        resultEntiry.Message = "执行成功！";
                    }
                    catch (Exception e)
                    {
                        resultEntiry.Flag = false;
                        resultEntiry.Where = "AddorUpdate";
                        resultEntiry.Message = e.Message;
                    }

                }
            }
            return resultEntiry;
        }
        /// <summary>
        /// 根据ID来查询相关实例进行增删改
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public RZ_ShoukuanDai GetEntityForEditor(NameValueCollection parameters)
        {
            RZ_ShoukuanDai resultdata = new RZ_ShoukuanDai();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_ShoukuanDai newSK = new RZ_ShoukuanDai();
                    EntityHelper.FormDataToDataObject(newSK, parameters);
                    var entiry = from a in _db.RZ_ShoukuanDai
                                 where a.ID == newSK.ID
                                 orderby a.CreateTime descending
                                 select a;
                    if (entiry.Count() > 0)
                    {
                        resultdata = entiry.SingleOrDefault();
                    }
                    return resultdata;
                }
                catch (Exception e)
                {
                    resultdata = null;
                }

            }
            return resultdata;
        }
        /// <summary>
        /// 根据ID来查询相关实例进行增删改
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<RZ_ShoukuanDai> GetShouTuKuanList(NameValueCollection parameters, out int total)
        {
            List<RZ_ShoukuanDai> resultlist = new List<RZ_ShoukuanDai>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                total = 0;
                try
                {

                    RZ_ShoukuanDai newSK = new RZ_ShoukuanDai();
                    EntityHelper.FormDataToDataObject(newSK, parameters);
                    var datalist = from a in _db.RZ_ShoukuanDai
                                   where a.SK_OrderGUID == newSK.SK_OrderGUID
                                   orderby a.CreateTime descending
                                   select a;
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultlist = datalist.Skip(totalNum).ToList();
                    if (resultlist.Count > page.PageSize)
                    {
                        resultlist.RemoveRange(page.PageSize, resultlist.Count - page.PageSize);
                    }
                    return resultlist;
                }
                catch (Exception e)
                {
                    resultlist = null;
                }
                return resultlist;
            }
        }

        //根据OrderGuid号来查询相关记录
        public List<RZ_ShoukuanDai> GetKaFanFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZ_ShoukuanDai> resultdatalist = new List<RZ_ShoukuanDai>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_ShoukuanDai newSK = new RZ_ShoukuanDai();
                    EntityHelper.FormDataToDataObject(newSK, parameters);
                    var datalist = from a in _db.RZ_ShoukuanDai
                                   where a.SK_OrderGUID == newSK.SK_OrderGUID
                                   orderby a.CreateTime descending
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
                    resultdatalist.Add(new RZ_ShoukuanDai());
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    total = 0;
                }

            }
            return resultdatalist;
        }
        /// <summary>
        /// 查询用于收款的支付方式
        /// </summary>
        /// <returns></returns>
        public List<PayTypeModel> GetPayType()
        {
            List<PayTypeModel> resultdatalist = new List<PayTypeModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var datalist = from a in _db.Basic_Payment
                                   where a.BPM_IsDepositPay == true
                                   orderby a.BPM_SeqNumber ascending
                                   select a;
                    foreach (Basic_Payment item in datalist)
                    {
                        PayTypeModel newitem = new PayTypeModel();
                        newitem.id = item.ID;
                        newitem.text = item.BPM_PayMentName;
                        resultdatalist.Add(newitem);
                    }
                    return resultdatalist;
                }
                catch (Exception e)
                {
                    return resultdatalist;
                }
            }

        }

        //根据OrderGuid号来查询Order信息
        public List<OrderInfoModel> GetOderInfoList(NameValueCollection parameters, out int total)
        {
            List<OrderInfoModel> resultdatalist = new List<OrderInfoModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string orderguid = parameters["OrderGuid"];
                    string sqltxt = @"SELECT [AutoID]
                                          ,[ChangTu]
                                          ,[ShiHua]
                                          ,[ChangBao]
                                          ,[ZhongDian]
                                          ,[DaodianTime]
                                          ,[LidianTime]
                                          ,[FukuanFangshi]
                                          ,[YaJin]
                                          ,[allYajingDeduct]
                                          ,[allCosumper]
                                          ,[OrderGuid]
                                          ,[XingMing]
                                          ,[FangHao]
                                          ,[KerenLeibie]
                                          ,[allMoney]
                                      FROM [RZ_OrderInfoView]
                                      WHERE [OrderGuid]=@OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "OrderGuid", Value = orderguid } };

                    var tempresult = _db.ExecuteStoreQuery<OrderInfoModel>(sqltxt, parm);
                    List<OrderInfoModel> datalist = tempresult.ToList<OrderInfoModel>();
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<OrderInfoModel>();
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
    /// <summary>
    /// 收款相关信息
    /// </summary>
    public class SukuanViewMode
    {
        public string Company { get; set; }
        public string ContactMan { get; set; }
        public string id { get; set; }
    }

    public class PayTypeModel
    {
        public int id { get; set; }
        public string text { get; set; }
    }
    public class OrderInfoModel
    {
        public string AutoID { get; set; }
        public bool? ChangTu { get; set; }
        public bool? ShiHua { get; set; }
        public bool? ChangBao { get; set; }
        public bool? ZhongDian { get; set; }
        public DateTime? DaodianTime { get; set; }
        public DateTime? LidianTime { get; set; }
        public string FukuanFangshi { get; set; }
        public decimal YaJin { get; set; }
        public decimal allYajingDeduct { get; set; }
        public decimal allCosumper { get; set; }
        public Guid OrderGuid { get; set; }
        public string XingMing { get; set; }
        public string FangHao { get; set; }
        public string KerenLeibie { get; set; }
        public decimal allMoney { get; set; }
    }
}
