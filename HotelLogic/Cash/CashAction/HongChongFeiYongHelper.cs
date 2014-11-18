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
    public class HongChongFeiYongHelper
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
        private static HongChongFeiYongHelper _instance;
        public static HongChongFeiYongHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new HongChongFeiYongHelper() : _instance;
            }
        }
        private HongChongFeiYongHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 新增或更新红冲费用操作
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
                        RZ_RedMoneyRecord RMModel = new RZ_RedMoneyRecord();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(RMModel, parameters);
                        //判断入住订单是否已经结账
                        resultEntiry = CashCommHelper.IsOrderNotEnd(RMModel.RR_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_RedMoneyRecord.AddObject(RMModel);
                        if (RMModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(RMModel, System.Data.EntityState.Added);
                            RMModel.ID = System.Guid.NewGuid();
                            RMModel.CreateTime = DateTime.Now;
                            RMModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(RMModel, System.Data.EntityState.Modified);
                        }
                        RMModel.UpdateTime = DateTime.Now;
                        RMModel.UpdateUser = UserName;
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
        //删除红冲费用记录
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
                        RZ_RedMoneyRecord newRM = new RZ_RedMoneyRecord();
                        EntityHelper.FormDataToDataObject(newRM, parameters);
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newRM.RR_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;
                        _db.RZ_RedMoneyRecord.AddObject(newRM);
                        _db.ObjectStateManager.ChangeObjectState(newRM, System.Data.EntityState.Deleted);
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
        //根据OrderGuid号来查询相关红冲记录
        public List<RZ_RemoneyRunningView> GetHongChongFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZ_RemoneyRunningView> resultdatalist = new List<RZ_RemoneyRunningView>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_RedMoneyRecord newRM = new RZ_RedMoneyRecord();
                    EntityHelper.FormDataToDataObject(newRM, parameters);
                    string sqltxt = "SELECT * FROM [RZ_RemoneyRunningView] A WHERE A.[OrderGuid]=@OrderGuid";
                    var parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "OrderGuid", Value = newRM.RR_OrderGuid}
                    };
                    var tempresult = _db.ExecuteStoreQuery<RZ_RemoneyRunningView>(sqltxt, parm);
                    List<RZ_RemoneyRunningView> datalist = tempresult.ToList<RZ_RemoneyRunningView>();
                    if (newRM.ID != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        datalist = datalist.Where(a => a.ID == newRM.ID).ToList();
                    }
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RZ_RemoneyRunningView>();
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

        /// <summary>
        /// 查询单笔记录
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public RZ_RedMoneyRecord GetSigeRecord(NameValueCollection parameters)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string rzid = parameters["ID"];
                    if (string.IsNullOrEmpty(rzid))
                    {
                        Guid guid = Guid.Parse(rzid);
                        var NewET = (from a in _db.RZ_RedMoneyRecord
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
        
        //根据OrderGuid号来查询相关费用记录
        public List<RunningModel> GetFeiYongList(Guid orderguid)
        {
            RunningList rl = new RunningList();
            return rl.GetRunningByOrder(orderguid);

        }
        #endregion

        #region 相关数据模型
        public class RZ_RemoneyRunningView
        {
            public Guid? ID { get; set; }
            public string RoomNo { get; set; }
            public string CustomerName { get; set; }
            public Guid? OrderGuid { get; set; }
            public Guid? AutoID { get; set; }
            public decimal? Price { get; set; }
            public string KM { get; set; }
            public decimal? Deposit { get; set; }
            public string Remark { get; set; }
            public string Payment { get; set; }
            public string Status { get; set; }
            public string Operator { get; set; }
            public decimal? RR_RedMoney { get; set; }
            public string RR_Remark { get; set; }
            public DateTime? RunningTime { get; set; }
        }
        #endregion
    }
}
