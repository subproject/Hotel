using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelLogic.Cash.CashAction
{
    public class KeFanFeiYongHelper
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
        private static KeFanFeiYongHelper _instance;

        public static KeFanFeiYongHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new KeFanFeiYongHelper() : _instance;
            }
        }
        private KeFanFeiYongHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 新增或更新客户费用记录操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdate(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        RZ_KeFanFeiYong KFModel = new RZ_KeFanFeiYong();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(KFModel, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(KFModel.KF_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_KeFanFeiYong.AddObject(KFModel);
                        if (KFModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(KFModel, System.Data.EntityState.Added);
                            KFModel.ID = System.Guid.NewGuid();
                            KFModel.CreateTime = DateTime.Now;
                            KFModel.KF_OccurrDateTime = DateTime.Now;
                            KFModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(KFModel, System.Data.EntityState.Modified);
                        }
                        KFModel.UpdateTime = DateTime.Now;
                        KFModel.UpdateUser = UserName;
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
                        RZ_KeFanFeiYong newKF = new RZ_KeFanFeiYong();
                        EntityHelper.FormDataToDataObject(newKF, parameters);

                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newKF.KF_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_KeFanFeiYong.AddObject(newKF);
                        _db.ObjectStateManager.ChangeObjectState(newKF, System.Data.EntityState.Deleted);
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
        /// 查询单笔记录
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public RZ_KeFanFeiYong GetSigeRecord(NameValueCollection parameters)
        {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        string rzid = parameters["ID"];
                        if (string.IsNullOrEmpty(rzid))
                        {
                            Guid guid = Guid.Parse(rzid);
                            var NewET = (from a in _db.RZ_KeFanFeiYong
                                         where a.ID == guid
                                         select a).SingleOrDefault();
                            return NewET;
                        }
                        return null;
                    }
                    catch (Exception e)
                    {
                        return null;
                    }
                }
        }

        //根据OrderGuid号来查询相关记录
        public List<RZ_KeFanFeiYong> GetKaFanFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZ_KeFanFeiYong> resultdatalist = new List<RZ_KeFanFeiYong>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_KeFanFeiYong newKF = new RZ_KeFanFeiYong();
                    EntityHelper.FormDataToDataObject(newKF, parameters);
                     resultdatalist = (from a in _db.RZ_KeFanFeiYong
                                   where a.KF_FeiyongType == newKF.KF_FeiyongType && a.KF_OrderGuid == newKF.KF_OrderGuid
                                   orderby a.KF_RooMNum
                                   select a).ToList();
                     if (newKF.ID != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                     {
                         resultdatalist = resultdatalist.Where(a => a.ID == newKF.ID).ToList();
                     }
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = resultdatalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = resultdatalist.Skip(totalNum).ToList();
                    if (resultdatalist.Count > page.PageSize)
                    {
                        resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                    }                    
                    if (CashCommHelper.IsOrderNotEnd(newKF.KF_OrderGuid).Flag)
                    {
                        resultdatalist.Add(new RZ_KeFanFeiYong());
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

        //根据条件值来查询首字母相似的商品中
        public List<GoodMode> GetGoodsByPyList(NameValueCollection parameters, out int total)
        {
            List<GoodMode> resultdatalist = new List<GoodMode>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string searchval=parameters["GoodsName"];
                    string sqltxt = @"SELECT   [GoodsNo]
                                              ,[GoodsName]
                                              ,[GoodsSimple]
                                              ,[SalePrice]
                                              ,[MakeUnit]
                                              ,[GoodsStyle]
                                              ,[GoodsType]
                                     FROM (
                                          SELECT  [GoodsNo]
                                              ,[GoodsName]
                                              ,[GoodsSimple]
                                              ,[SalePrice]
                                              ,[MakeUnit]
                                              ,[GoodsStyle]
                                              ,[GoodsType]
                                              ,dbo.fun_getPY(GoodsName) AS PY_GoodsName
                                          FROM [WH_Goods] 
                                          ) WG
                                          where WG.PY_GoodsName like  '%'+dbo.fun_getPY(@GoodsName)+'%'
                                          ORDER BY WG.PY_GoodsName";
                    var parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "GoodsName", Value =searchval}
                    };
                    var tempresult = _db.ExecuteStoreQuery<GoodMode>(sqltxt, parm);
                    List<GoodMode> datalist = tempresult.ToList<GoodMode>();
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<GoodMode>();
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

        #endregion
    }
    #region 客户记录相关信息模型

    public class KeFanFeiYongModel
    {
        public Guid ID { get; set; }
        public string KF_ProduceCode { get; set; }
        public string KF_ProduceName { get; set; }
        public string KF_FeiyongType { get; set; }
        public Guid KF_OrderGuid { get; set; }
        public decimal? KF_UnitPrice { get; set; }
        public decimal? KF_Quantity { get; set; }
        public decimal? KF_Money { get; set; }
        public string KF_Remark { get; set; }
        public DateTime KF_OccurrDateTime { get; set; }
        public string KF_Operator { get; set; }
        public string KF_ReceidNo { get; set; }
        public string KF_RooMNum { get; set; }
        public string KF_Curostmer { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }

    public class QitaFeiYongModel
    {
        public Guid ID { get; set; }
        public string QF_FeiYongCode { get; set; }
        public string QF_FeiYongName { get; set; }
        public Guid QF_OrderGUID { get; set; }
        public string QF_Receipt { get; set; }
        public string QF_Remark { get; set; }
        public decimal QF_Money { get; set; }
        public string QF_YouHuiRemark { get; set; }
        public DateTime QF_OccurrDateTime { get; set; }
        public string QF_Room { get; set; }
        public string QF_Customer { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateUser { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    /// <summary>
    /// 商品类
    /// </summary>
    public class GoodMode
    {
        public int GoodsNo { get; set; }
        public string GoodsName { get; set; }
        public string GoodsSimple { get; set; }
        public decimal SalePrice { get; set; }
        public string MakeUnit { get; set; }
        public string GoodsStyle { get; set; }
        public int GoodsType { get; set; }
    }

    #endregion
}
