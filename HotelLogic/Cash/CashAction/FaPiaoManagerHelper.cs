using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Cash.CashAction
{
    public class FaPiaoManagerHelper
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
        private static FaPiaoManagerHelper _instance;
       
        public static FaPiaoManagerHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new FaPiaoManagerHelper() : _instance;
            }
        }
        private FaPiaoManagerHelper() { }
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
                        RZ_FaPiaoManage FPModel = new RZ_FaPiaoManage();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(FPModel, parameters);
                        //判断是否已经结账
                        resultEntiry = CashCommHelper.IsOrderNotEnd(FPModel.FP_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_FaPiaoManage.AddObject(FPModel); 
                        if (FPModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(FPModel, System.Data.EntityState.Added);
                            FPModel.ID = System.Guid.NewGuid();
                            FPModel.CreateTime = DateTime.Now;
                            FPModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(FPModel, System.Data.EntityState.Modified);
                        }                       
                        FPModel.UpdateTime = DateTime.Now;
                        FPModel.UpdateUser = UserName;
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
                        RZ_FaPiaoManage newFP = new RZ_FaPiaoManage();
                        EntityHelper.FormDataToDataObject(newFP, parameters);
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newFP.FP_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;
                        _db.RZ_FaPiaoManage.AddObject(newFP);
                        _db.ObjectStateManager.ChangeObjectState(newFP, System.Data.EntityState.Deleted);
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

        //根据OrderGuid号来查询相关记录
        public List<RZ_FaPiaoManage> GetKaFanFeiYongList(NameValueCollection parameters,out int total)
        {
            List<RZ_FaPiaoManage> resultdatalist = new List<RZ_FaPiaoManage>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_FaPiaoManage newFP = new RZ_FaPiaoManage();
                    EntityHelper.FormDataToDataObject(newFP, parameters);
                    var datalist = from a in _db.RZ_FaPiaoManage
                                   where a.FP_OrderGuid==newFP.FP_OrderGuid
                                   orderby a.FP_Room 
                                   select a;
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page= HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = datalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = datalist.Skip(totalNum).ToList();
                    if (resultdatalist.Count > page.PageSize)
                    {
                        resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                    }
                    RZ_FaPiaoManage newAddEntiry = new RZ_FaPiaoManage();
                    JieZhangTuFangHelper jzthelper = JieZhangTuFangHelper.Instance;
                    OrderInfoMode orderinfo = jzthelper.GetOrderInfo(newFP.FP_OrderGuid.ToString());
                     newAddEntiry.FP_FP_ConsumMoney = orderinfo.allCosumper;
                     newAddEntiry.FP_Money = orderinfo.YaJin + orderinfo.allMoney;
                     if (CashCommHelper.IsOrderNotEnd(newFP.FP_OrderGuid).Flag)
                     {
                         resultdatalist.Add(newAddEntiry);
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
        //根据OrderGuid号来查询相关记录
        public List<RZ_FaPiaoManage> GetFaPiaoList(Guid orderguid)
        {
            List<RZ_FaPiaoManage> resultdatalist = new List<RZ_FaPiaoManage>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var datalist = from a in _db.RZ_FaPiaoManage
                                   where a.FP_OrderGuid ==orderguid
                                   orderby a.FP_Room
                                   select a;
                    resultdatalist = datalist.ToList<RZ_FaPiaoManage>();

                    return resultdatalist;
                }
                catch (Exception e)
                {
                   
                }

            }
            return resultdatalist;
        }
        #endregion
    }
  
}
