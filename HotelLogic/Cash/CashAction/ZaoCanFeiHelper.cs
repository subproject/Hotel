using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Cash.CashAction
{
    public class ZaoCanFeiHelper
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
        private static ZaoCanFeiHelper _instance;

        public static ZaoCanFeiHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new ZaoCanFeiHelper() : _instance;
            }
        }
        private ZaoCanFeiHelper() { }
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
                        RZ_ZaCanFei ZAModel = new RZ_ZaCanFei();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(ZAModel, parameters);

                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(ZAModel.ZC_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_ZaCanFei.AddObject(ZAModel);
                        if (ZAModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(ZAModel, System.Data.EntityState.Added);
                            ZAModel.ID = System.Guid.NewGuid();
                            ZAModel.CreateTime = DateTime.Now;
                            ZAModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(ZAModel, System.Data.EntityState.Modified);
                        }
                        ZAModel.UpdateTime = DateTime.Now;
                        ZAModel.UpdateUser = UserName;
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
                        RZ_ZaCanFei newZF = new RZ_ZaCanFei();
                        EntityHelper.FormDataToDataObject(newZF, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newZF.ZC_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_ZaCanFei.AddObject(newZF);
                        _db.ObjectStateManager.ChangeObjectState(newZF, System.Data.EntityState.Deleted);
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
        public List<RZ_ZaCanFei> GetKaFanFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZ_ZaCanFei> resultdatalist = new List<RZ_ZaCanFei>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_ZaCanFei newZF = new RZ_ZaCanFei();
                    EntityHelper.FormDataToDataObject(newZF, parameters);
                    resultdatalist = (from a in _db.RZ_ZaCanFei
                                   where a.ZC_OrderGUID==newZF.ZC_OrderGUID
                                   orderby a.ZC_Room
                                   select a).ToList();
                    if (newZF.ID != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        resultdatalist = resultdatalist.Where(a => a.ID == newZF.ID).ToList();
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
                    
                    if (CashCommHelper.IsOrderNotEnd(newZF.ZC_OrderGUID).Flag)
                    {
                        resultdatalist.Add(new RZ_ZaCanFei());
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
}
