using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.CommonModel;
using System.Collections.Specialized;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace HotelLogic.Cash.CashAction
{
    public class BuFengJieZhangHelper
    {
        #region
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        public string Roles { get; set; }
        private static BuFengJieZhangHelper _instance;
        public static BuFengJieZhangHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new BuFengJieZhangHelper() : _instance;
            }
        }
        private BuFengJieZhangHelper() { }
        #endregion
        /// <summary>
        /// 新增或更新结账信息记录操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdate(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        try
                        {
                            RZFeiYongModel SKModel = new RZFeiYongModel();
                            //赋值到对象
                            EntityHelper.FormDataToDataObject(SKModel, parameters);
                            resultEntiry = CashCommHelper.IsOrderNotEnd(SKModel.RZ_OrderGuid);
                            if (!resultEntiry.Flag) return resultEntiry;

                            //保存明细数据
                            string rowsstr = parameters["ChildRows"];
                            List<RZFeiYongModel> newdatalist = EntityHelper.GetEntityInfoFromJsonPage<RZFeiYongModel>(rowsstr);
                            if (newdatalist != null && newdatalist.Count > 0)
                            {
                                foreach (RZFeiYongModel item in newdatalist)
                                {
                                    switch (item.RZ_FYType)
                                    {
                                        case "客房租金":
                                            Cash_RunningDetails cr = new Cash_RunningDetails();
                                            cr.AutoID = item.RZ_ID;
                                            cr.OrderGuid = item.RZ_OrderGuid;
                                            cr.Status = item.RZ_Status;
                                            var crstateEntry = _db.ObjectStateManager.GetObjectStateEntry(cr);
                                            crstateEntry.SetModifiedProperty(cr.Status);
                                            IEnumerable<KeyValuePair<string, object>> crentityKeyValues =
                                                new KeyValuePair<string, object>[] {
                                                      new KeyValuePair<string, object>("AutoID", cr.AutoID),
                                                      new KeyValuePair<string, object>("OrderGuid", cr.OrderGuid)                                                
                                                };
                                            cr.EntityKey = new System.Data.EntityKey("HotelEntities.Cash_RunningDetails", crentityKeyValues);
                                            _db.Cash_RunningDetails.AddObject(cr);
                                            _db.ObjectStateManager.ChangeObjectState(cr, System.Data.EntityState.Modified);

                                            break;
                                        case "客房用品":
                                            RZ_KeFanFeiYong kf = new RZ_KeFanFeiYong();
                                            kf.KF_Status = item.RZ_Status;
                                            kf.ID = item.RZ_ID;
                                            kf.KF_OrderGuid = item.RZ_OrderGuid;
                                            kf.KF_Status = item.RZ_Status;

                                            var kfstateEntry = _db.ObjectStateManager.GetObjectStateEntry(kf);
                                            kfstateEntry.SetModifiedProperty(kf.KF_Status);
                                            IEnumerable<KeyValuePair<string, object>> kfentityKeyValues =
                                                new KeyValuePair<string, object>[] {
                                                      new KeyValuePair<string, object>("AutoID", kf.ID),
                                                      new KeyValuePair<string, object>("OrderGuid", kf.KF_OrderGuid)                                                
                                                };
                                            kf.EntityKey = new System.Data.EntityKey("HotelEntities.RZ_KeFanFeiYong", kfentityKeyValues);

                                            _db.RZ_KeFanFeiYong.AddObject(kf);
                                            _db.ObjectStateManager.ChangeObjectState(kf, System.Data.EntityState.Modified);

                                            break;
                                        case "损物赔偿":
                                            RZ_SunWuPeiChang sw = new RZ_SunWuPeiChang();
                                            sw.SW_Status = item.RZ_Status;
                                            sw.ID = item.RZ_ID;
                                            sw.SW_OrderGuid = item.RZ_OrderGuid;
                                            sw.SW_Status = item.RZ_Status;

                                            var swstateEntry = _db.ObjectStateManager.GetObjectStateEntry(sw);
                                            swstateEntry.SetModifiedProperty(sw.SW_Status);
                                            IEnumerable<KeyValuePair<string, object>> swentityKeyValues =
                                                new KeyValuePair<string, object>[] {
                                                      new KeyValuePair<string, object>("AutoID", sw.ID),
                                                      new KeyValuePair<string, object>("OrderGuid", sw.SW_OrderGuid)                                                
                                                };
                                            sw.EntityKey = new System.Data.EntityKey("HotelEntities.RZ_SunWuPeiChang", swentityKeyValues);

                                            _db.RZ_SunWuPeiChang.AddObject(sw);
                                            _db.ObjectStateManager.ChangeObjectState(sw, System.Data.EntityState.Modified);

                                            break;
                                        case "早餐费用":
                                            RZ_ZaCanFei zc = new RZ_ZaCanFei();
                                            zc.ZC_Status = item.RZ_Status;
                                            zc.ID = item.RZ_ID;
                                            zc.ZC_OrderGUID = item.RZ_OrderGuid;
                                            zc.ZC_Status = item.RZ_Status;

                                            var zcstateEntry = _db.ObjectStateManager.GetObjectStateEntry(zc);
                                            zcstateEntry.SetModifiedProperty(zc.ZC_Status);
                                            IEnumerable<KeyValuePair<string, object>> zcentityKeyValues =
                                                new KeyValuePair<string, object>[] {
                                                      new KeyValuePair<string, object>("AutoID", zc.ID),
                                                      new KeyValuePair<string, object>("OrderGuid", zc.ZC_OrderGUID)                                                
                                                };
                                            zc.EntityKey = new System.Data.EntityKey("HotelEntities.RZ_ZaCanFei", zcentityKeyValues);

                                            _db.RZ_ZaCanFei.AddObject(zc);
                                            _db.ObjectStateManager.ChangeObjectState(zc, System.Data.EntityState.Modified);

                                            break;
                                        case "其它费用":
                                            RZ_QiTaFeiYong qf = new RZ_QiTaFeiYong();
                                            qf.QF_Status = item.RZ_Status;

                                            qf.ID = item.RZ_ID;
                                            qf.QF_OrderGUID = item.RZ_OrderGuid;
                                            qf.QF_Status = item.RZ_Status;

                                            var qfstateEntry = _db.ObjectStateManager.GetObjectStateEntry(qf);
                                            qfstateEntry.SetModifiedProperty(qf.QF_Status);
                                            IEnumerable<KeyValuePair<string, object>> qfentityKeyValues =
                                                new KeyValuePair<string, object>[] {
                                                      new KeyValuePair<string, object>("AutoID", qf.ID),
                                                      new KeyValuePair<string, object>("OrderGuid", qf.QF_OrderGUID)                                                
                                                };
                                            qf.EntityKey = new System.Data.EntityKey("HotelEntities.RZ_QiTaFeiYong", qfentityKeyValues);


                                            _db.RZ_QiTaFeiYong.AddObject(qf);
                                            _db.ObjectStateManager.ChangeObjectState(qf, System.Data.EntityState.Modified);

                                            break;
                                    }
                                }
                            }
                            _db.SaveChanges();
                            scope.Complete();
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
            }
            return resultEntiry;

        }
        /// <summary>
        /// 新增或更新结账费用记录操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdate(List<RZFeiYongModel> newdatalist, Guid jztfID, string statu)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        if (newdatalist != null && newdatalist.Count > 0)
                        {
                            foreach (RZFeiYongModel item in newdatalist)
                            {
                                switch (item.RZ_FYType)
                                {
                                    case "客房租金":
                                        Cash_RunningDetails cr = (from a in _db.Cash_RunningDetails
                                                                  where a.AutoID == item.RZ_ID
                                                                  select a).SingleOrDefault();
                                        if (cr != null)
                                        {
                                            cr.Status = statu;
                                            _db.ObjectStateManager.ChangeObjectState(cr, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "客房用品":
                                        RZ_KeFanFeiYong kf = (from a in _db.RZ_KeFanFeiYong
                                                              where a.ID == item.RZ_ID
                                                              select a).SingleOrDefault();
                                        if (kf != null)
                                        {
                                            kf.KF_Status = statu;
                                            kf.UpdateTime = DateTime.Now;
                                            _db.ObjectStateManager.ChangeObjectState(kf, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "损物赔偿":
                                        RZ_SunWuPeiChang sw = (from a in _db.RZ_SunWuPeiChang
                                                               where a.ID == item.RZ_ID
                                                               select a).SingleOrDefault();
                                        if (sw != null)
                                        {
                                            sw.SW_Status = statu;
                                            sw.UpdateTime = DateTime.Now;
                                            sw.UpdateUser = UserName;
                                            _db.ObjectStateManager.ChangeObjectState(sw, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "早餐费用":
                                        RZ_ZaCanFei zc = (from a in _db.RZ_ZaCanFei
                                                          where a.ID == item.RZ_ID
                                                          select a).SingleOrDefault();
                                        if (zc != null)
                                        {
                                            zc.ZC_Status = statu;
                                            zc.UpdateTime = DateTime.Now;
                                            zc.UpdateUser = UserName;
                                            _db.ObjectStateManager.ChangeObjectState(zc, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "其它费用":
                                        RZ_QiTaFeiYong qf = (from a in _db.RZ_QiTaFeiYong
                                                             where a.ID == item.RZ_ID
                                                             select a).SingleOrDefault();
                                        if (qf != null)
                                        {
                                            qf.QF_Status = statu;
                                            qf.UpdateTime = DateTime.Now;
                                            qf.UpdateUser = UserName;
                                            _db.ObjectStateManager.ChangeObjectState(qf, System.Data.EntityState.Modified);
                                        }
                                        break;
                                }
                                RZ_FeiYongJieZhangRelation _RFYJZR = new RZ_FeiYongJieZhangRelation();
                                _RFYJZR.ID = Guid.NewGuid();
                                _RFYJZR.JZ_ID = item.RZ_ID;
                                _RFYJZR.JZ_JZID = jztfID;
                                _db.RZ_FeiYongJieZhangRelation.AddObject(_RFYJZR);
                                _db.ObjectStateManager.ChangeObjectState(_RFYJZR, System.Data.EntityState.Added);
                            }
                        }
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
        //根据OrderGuid号来查询所有相关费用明细信息
        public List<RZFeiYongModel> GetRiZhuFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZFeiYongModel> resultdatalist = new List<RZFeiYongModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZFeiYongModel newRM = new RZFeiYongModel();
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
                                      FROM [RZ_FeiYongDetailsView]
                                      WHERE [RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid } };
                    if (newRM.RZ_Status != null && newRM.RZ_Status != "")
                    {
                        sqltxt += " AND [RZ_Status]=@RZ_Status";
                        parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid},
                         new SqlParameter { ParameterName = "RZ_Status", Value = newRM.RZ_Status}
                        };
                    }
                    else
                    {
                        sqltxt += " AND  ([RZ_Status]!='结账' OR [RZ_Status] IS NULL)";
                    }

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
        /// <summary>
        /// //根据OrderGuid号来查询所有相关费用明细信息
        /// </summary>
        /// <param name="orderid">入住单ID</param>
        /// <param name="rzrid">结账单号ID</param>
        /// <param name="status">状态值</param>
        /// <param name="pagenumber">页面数量</param>
        /// <param name="pagesize">页面大小</param>
        /// <param name="total">总共记录数</param>
        /// <returns></returns>
        public List<RZFeiYongModel> GetRiZhuFeiYongList(Guid orderid, Guid rzrid, string status, int pagenumber, int pagesize, out int total)
        {
            List<RZFeiYongModel> resultdatalist = new List<RZFeiYongModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZFeiYongModel newRM = new RZFeiYongModel();
                    newRM.RZ_OrderGuid = orderid;
                    newRM.RZ_Status = status;
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
                                       FROM [RZ_FeiYongDetailsView] RV
                                       LEFT JOIN RZ_FeiYongJieZhangRelation RR ON RV.[RZ_ID]=RR.JZ_ID
                                      WHERE [RZ_OrderGuid]=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid } };
                    if (newRM.RZ_Status != null && newRM.RZ_Status != "")
                    {
                        sqltxt += " AND [RZ_Status]=@RZ_Status";
                        parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid},
                         new SqlParameter { ParameterName = "RZ_Status", Value = newRM.RZ_Status}
                        };
                    }
                    if (rzrid != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        sqltxt += " AND RR.JZ_JZID=@JZ_JZID";
                        parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid},
                         new SqlParameter { ParameterName = "RZ_Status", Value = newRM.RZ_Status},
                          new SqlParameter { ParameterName = "JZ_JZID", Value =rzrid}
                        };
                    }

                    var tempresult = _db.ExecuteStoreQuery<RZFeiYongModel>(sqltxt, parm);
                    List<RZFeiYongModel> datalist = tempresult.ToList<RZFeiYongModel>();

                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (pagenumber - 1) * pagesize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RZFeiYongModel>();
                    if (resultdatalist.Count > pagesize)
                    {
                        resultdatalist.RemoveRange(pagesize, resultdatalist.Count - pagesize);
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
        //根据OrderGuid号来查询所有相关结账明细信息
        public List<RZ_JieZhangTuiFang> GetJieZhangList(NameValueCollection parameters, out int total)
        {
            List<RZ_JieZhangTuiFang> resultdatalist = new List<RZ_JieZhangTuiFang>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_JieZhangTuiFang newRM = new RZ_JieZhangTuiFang();
                    EntityHelper.FormDataToDataObject(newRM, parameters);
                    //首先清除相关挂账时产生的空数据
                    JieZhangTuFangHelper jzhelper = JieZhangTuFangHelper.Instance;
                    jzhelper.ClearJieZhangNullUnit();

                    var datalist = (from a in _db.RZ_JieZhangTuiFang
                                      where a.JZ_OrderGUID == newRM.JZ_OrderGUID
                                      orderby a.CreateTime descending
                                      select a);
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RZ_JieZhangTuiFang>();
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
    #region 相关Mode
    public class RZFeiYongModel
    {
        public Guid RZ_ID { get; set; }
        public string RZ_FeiYongCode { get; set; }
        public string RZ_FeiYongName { get; set; }
        public string RZ_FeiyongType { get; set; }
        public string RZ_FYType { get; set; }
        public Guid RZ_OrderGuid { get; set; }
        public decimal RZ_Money { get; set; }
        public string RZ_Remark { get; set; }
        public string RZ_ReciptNo { get; set; }
        public string RZ_RoomNo { get; set; }
        public string RZ_Customer { get; set; }
        public DateTime? RZ_RunnTime { get; set; }
        public string RZ_Operator { get; set; }
        public string RZ_Payment { get; set; }
        public string RZ_Status { get; set; }
        public decimal RR_RedMoney { get; set; }
    }

    #endregion
}
