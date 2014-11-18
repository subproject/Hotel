using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Transactions;

namespace HotelLogic.Cash.CashAction
{
    public class SanKeZhuanZhangHelper
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
        private static SanKeZhuanZhangHelper _instance;
        public static SanKeZhuanZhangHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new SanKeZhuanZhangHelper() : _instance;
            }
        }
        private SanKeZhuanZhangHelper() { }
        #endregion

        #region 操作实现函数列表
        //费用转出或转入调用函数
        public ResultMode FeiYongZhuanChuFunc(NameValueCollection parameters)
        {
            ResultMode resultmode = new ResultMode();
            resultmode.Where = "FeiYongZhuanChuFunc";
            //获取新OrderGuid
            string neworderguid = parameters["NewOrderGuid"];
            string currOrderGuid = parameters["OrderGuid"];

            //判断入住订单是否已经结账退房
            resultmode = CashCommHelper.IsOrderNotEnd(Guid.Parse(currOrderGuid));
            if (!resultmode.Flag) return resultmode;

            if (!string.IsNullOrEmpty(neworderguid))
            {
                try
                {
                    //保存明细数据
                    string rowsstr = parameters["DataRows"];
                    List<RZFeiYongModel> newdatalist = EntityHelper.GetEntityInfoFromJsonPage<RZFeiYongModel>(rowsstr);
                    //执行函数
                    FeiYongZhuanChu(newdatalist, Guid.Parse(neworderguid));
                    resultmode.Flag = true;
                    resultmode.Message = "执行成功！";
                }
                catch (Exception e)
                {
                    resultmode.Flag = false;
                    resultmode.Message = "执行失败！" + e.Message;
                }

            }
            else
            {
                resultmode.Flag = false;
                resultmode.Message = "执行失败！因为没有输入需要转入的入住订单ID!";
            }
            return resultmode;
        }
        //费用转出及输入
        public ResultMode FeiYongZhuanChu(List<RZFeiYongModel> newdatalist, Guid newGuid)
        {
            ResultMode resultEntiry = new ResultMode();
            using (TransactionScope scope = new TransactionScope())
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        if (newdatalist != null && newdatalist.Count > 0)
                        {
                            Guid oldOrderGuid = new Guid();
                            foreach (RZFeiYongModel item in newdatalist)
                            {
                                item.RZ_OrderGuid = newGuid;
                                switch (item.RZ_FYType)
                                {
                                    case "客房租金":
                                        Cash_RunningDetails cr = (from a in _db.Cash_RunningDetails
                                                                  where a.AutoID == item.RZ_ID
                                                                  select a).SingleOrDefault();
                                        if (cr != null)
                                        {
                                            oldOrderGuid = cr.OrderGuid;
                                            cr.OrderGuid = item.RZ_OrderGuid;
                                            _db.ObjectStateManager.ChangeObjectState(cr, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "客房用品":
                                        RZ_KeFanFeiYong kf = (from a in _db.RZ_KeFanFeiYong
                                                              where a.ID == item.RZ_ID
                                                              select a).SingleOrDefault();
                                        if (kf != null)
                                        {
                                            oldOrderGuid = (Guid)kf.KF_OrderGuid;
                                            kf.KF_OrderGuid = item.RZ_OrderGuid;
                                            kf.UpdateTime = DateTime.Now;
                                            kf.UpdateUser = UserName;
                                            _db.ObjectStateManager.ChangeObjectState(kf, System.Data.EntityState.Modified);
                                        }
                                        break;
                                    case "损物赔偿":
                                        RZ_SunWuPeiChang sw = (from a in _db.RZ_SunWuPeiChang
                                                               where a.ID == item.RZ_ID
                                                               select a).SingleOrDefault();
                                        if (sw != null)
                                        {
                                            oldOrderGuid = (Guid)sw.SW_OrderGuid;
                                            sw.SW_OrderGuid = item.RZ_OrderGuid;
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
                                            oldOrderGuid = (Guid)zc.ZC_OrderGUID;
                                            zc.ZC_OrderGUID = item.RZ_OrderGuid;
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
                                            oldOrderGuid = (Guid)qf.QF_OrderGUID;
                                            qf.QF_OrderGUID = item.RZ_OrderGuid;
                                            qf.UpdateTime = DateTime.Now;
                                            qf.UpdateUser = UserName;
                                            _db.ObjectStateManager.ChangeObjectState(qf, System.Data.EntityState.Modified);
                                        }
                                        break;
                                }
                                var zrr = (from a in _db.RZ_ZhuanZhangRelation
                                           where a.FeiYongID == item.RZ_ID
                                           select a).SingleOrDefault();
                                if (zrr != null)
                                {
                                    if (zrr.OutOrderGUID == newGuid)
                                    {
                                        _db.ObjectStateManager.ChangeObjectState(zrr, System.Data.EntityState.Deleted);
                                    }
                                    else
                                    {
                                        //如果有多次转换得不停记录变换
                                        zrr.InOrderGUID = newGuid;
                                        zrr.UpdateTime = DateTime.Now;
                                        zrr.UpdateUser = UserName;
                                    }

                                }
                                else
                                {
                                    RZ_ZhuanZhangRelation zrrentity = new RZ_ZhuanZhangRelation();
                                    zrrentity.FeiYongID = item.RZ_ID;
                                    zrrentity.InOrderGUID = newGuid;
                                    zrrentity.OutOrderGUID = oldOrderGuid;
                                    zrrentity.CreateTime = DateTime.Now;
                                    zrrentity.CreateUser = UserName;
                                    zrrentity.UpdateTime = DateTime.Now;
                                    zrrentity.UpdateUser = UserName;
                                    _db.RZ_ZhuanZhangRelation.AddObject(zrrentity);
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
                    return resultEntiry;
                }
            }
        }
        //收退款转出
        public ResultMode ShouKuanZhangChu(NameValueCollection parameters)
        {
            ResultMode resultmode = new ResultMode();
            resultmode.Where = "ShouKuanZhangChu";
            //新输入的OrderGuid
            string newguid = parameters["NewOrderGuid"];

            //判断入住订单是否已经结账退房
            string currOrderGuid = parameters["OrderGuid"];
            resultmode = CashCommHelper.IsOrderNotEnd(Guid.Parse(currOrderGuid));
            if (!resultmode.Flag) return resultmode;

            //保存明细数据
            string rowsstr = parameters["DataRows"];
            List<RZ_ShoukuanDai> newdatalist = EntityHelper.GetEntityInfoFromJsonPage<RZ_ShoukuanDai>(rowsstr);
            //执行函数
            if (newdatalist != null && !string.IsNullOrEmpty(newguid))
            {
                try
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        foreach (RZ_ShoukuanDai item in newdatalist)
                        {
                            var dataentity = (from a in _db.RZ_ShoukuanDai
                                              where item.ID == a.ID
                                              select a).SingleOrDefault();
                            if (dataentity != null)
                            {
                                dataentity.SK_OrderGUID = Guid.Parse(newguid);
                                dataentity.UpdateUser = UserName;
                                dataentity.UpdateTime = DateTime.Now;
                                _db.ObjectStateManager.ChangeObjectState(dataentity, System.Data.EntityState.Modified);
                            }
                        }
                        _db.SaveChanges();
                        resultmode.Flag = true;
                        resultmode.Message = "执行成功！";
                    }
                }
                catch (Exception e)
                {
                    resultmode.Flag = true;
                    resultmode.Message = "执行失败！" + e.Message;
                }
            }
            else
            {
                resultmode.Flag = true;
                resultmode.Message = "执行失败！因为没有新转入的订单ID!";
            }
            return resultmode;
        }
        //根据orderguid查询相关的费用
        public List<RZFeiYongModel> GetFeiYongByOrderGuid(NameValueCollection parameters, out int total)
        {
            BuFengJieZhangHelper _bzhelper = BuFengJieZhangHelper.Instance;
            return _bzhelper.GetRiZhuFeiYongList(parameters, out total);
        }
        //根据orderguid查询出相关的收退款
        public List<RZ_ShoukuanDai> GetShouKuanByOrderGuid(NameValueCollection parameters, out int total)
        {
            ShoukuanHelper _shhelper = ShoukuanHelper.Instance;
            List<RZ_ShoukuanDai> resultlist = _shhelper.GetKaFanFeiYongList(parameters, out total).Where(a=>a.ID!= System.Guid.Parse("00000000-0000-0000-0000-000000000000")).ToList();

            return resultlist;
        }
        //根据orderguid查询转入的费用
        public List<RZFeiYongModel> GetZhuangRuFeiYongByOrderGuid(NameValueCollection parameters, out int total)
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
                          FROM [RZ_FeiYongDetailsView] DV
                          INNER JOIN RZ_ZhuanZhangRelation ZR
                              ON ZR.FeiYongID=DV.RZ_ID
                          WHERE ZR.InOrderGUID=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid } };
                    if (!string.IsNullOrEmpty(newRM.RZ_Status))
                    {
                        sqltxt += " AND [RZ_Status]=@RZ_Status";
                        parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid},
                         new SqlParameter { ParameterName = "RZ_Status", Value = newRM.RZ_Status}
                        };
                    }
                    else
                    {
                        sqltxt += " AND ([RZ_Status]!='结账' OR [RZ_Status] IS NULL)";
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
        //根据orderguid查询转出的费用
        public List<RZFeiYongModel> GetZhuangChuFeiYongByOrderGuid(NameValueCollection parameters, out int total)
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
                          FROM [RZ_FeiYongDetailsView] DV
                          INNER JOIN RZ_ZhuanZhangRelation ZR
                              ON ZR.FeiYongID=DV.RZ_ID
                          WHERE ZR.OutOrderGUID=@RZ_OrderGuid ";
                    var parm = new DbParameter[] { new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid } };
                    if (!string.IsNullOrEmpty(newRM.RZ_Status))
                    {
                        sqltxt += " AND [RZ_Status]=@RZ_Status";
                        parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "RZ_OrderGuid", Value = newRM.RZ_OrderGuid},
                         new SqlParameter { ParameterName = "RZ_Status", Value = newRM.RZ_Status}
                        };
                    }
                    else
                    {
                        sqltxt += " AND ([RZ_Status]!='结账' OR [RZ_Status] IS NULL)";
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

        //根据查询在住房间列表
        public List<RoomsModel> GetOrderRoomList(NameValueCollection parameters, out int total)
        {
            List<RoomsModel> resultdatalist = new List<RoomsModel>();
            string orderguid = parameters["RZ_OrderGuid"];
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT od.[OrderGuid],  
                                           [FangJianHao]=stuff((SELECT '/'+[FangJianHao] FROM [H_RuzhuDetail]
                                             WHERE [OrderGuid]=od.[OrderGuid] for xml path('')),1,1,'')  
                                      FROM [H_RuzhuDetail]  od 
                                      INNER JOIN (SELECT  OrderGuid
                                      FROM [H_RuzhuOrder]
                                      WHERE Status=0 {0}) O  ON OD.OrderGuid=O.OrderGuid                                      
                                      GROUP BY OD.[OrderGuid]";
                    var parm = new DbParameter[] { };
                    if (!string.IsNullOrEmpty(orderguid))
                    {
                        sqltxt = string.Format(sqltxt, " AND OrderGuid!=@OrderGuid ");
                        parm = new DbParameter[] { new SqlParameter("OrderGuid", orderguid) };
                    }
                    else
                    {
                        sqltxt = string.Format(sqltxt, " ");
                    }
                    var tempresult = _db.ExecuteStoreQuery<RoomsModel>(sqltxt, parm);
                    List<RoomsModel> datalist = tempresult.ToList<RoomsModel>();
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);

                    total = datalist.Count();

                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList<RoomsModel>();
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
        //根据查询在住房间列表
        public RoomsModel GetOrderRoomList(string orderguid)
        {
            List<RoomsModel> resultdatalist = new List<RoomsModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string sqltxt = @"SELECT od.[OrderGuid],  
                                           [FangJianHao]=stuff((SELECT '/'+[FangJianHao] FROM [H_RuzhuDetail]
                                             WHERE [OrderGuid]=od.[OrderGuid] for xml path('')),1,1,'')  
                                      FROM [H_RuzhuDetail]  od 
                                      INNER JOIN (SELECT  OrderGuid
                                         FROM [H_RuzhuOrder]
                                         WHERE Status=0) O  ON OD.OrderGuid=O.OrderGuid
                                      WHERE od.[OrderGuid]=@OrderGuid                                            
                                         GROUP BY OD.[OrderGuid]
                                      ";
                    var parm = new DbParameter[] { new SqlParameter("@OrderGuid", orderguid) };
                    var tempresult = _db.ExecuteStoreQuery<RoomsModel>(sqltxt, parm);
                    List<RoomsModel> datalist = tempresult.ToList<RoomsModel>();
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
        #endregion

        #region 相关数据模型
        public class RoomsModel
        {
            public Guid OrderGuid { get; set; }
            public string FangJianHao { get; set; }
        }

        #endregion
    }
}
