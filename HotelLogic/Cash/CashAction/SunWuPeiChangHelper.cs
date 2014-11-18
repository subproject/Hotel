using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Cash.CashAction
{
    public class SunWuPeiChangHelper
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
        private static SunWuPeiChangHelper _instance;

        public static SunWuPeiChangHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new SunWuPeiChangHelper() : _instance;
            }
        }
        private SunWuPeiChangHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 新增或更新损物赔偿费用记录操作
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
                       RZ_SunWuPeiChang  SWModel = new RZ_SunWuPeiChang();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(SWModel, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(SWModel.SW_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_SunWuPeiChang.AddObject(SWModel); 
                        if (SWModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(SWModel, System.Data.EntityState.Added);
                            SWModel.ID = System.Guid.NewGuid();
                            SWModel.CreateTime = DateTime.Now;
                            SWModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(SWModel, System.Data.EntityState.Modified);
                        }                       
                        SWModel.UpdateTime = DateTime.Now;
                        SWModel.UpdateUser = UserName;
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
        //删除损物赔偿费用记录
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
                        RZ_SunWuPeiChang newSW = new RZ_SunWuPeiChang();
                        EntityHelper.FormDataToDataObject(newSW, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newSW.SW_OrderGuid);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_SunWuPeiChang.AddObject(newSW);
                        _db.ObjectStateManager.ChangeObjectState(newSW, System.Data.EntityState.Deleted);
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
        public  List<RZ_SunWuPeiChang> GetSigeRecord(NameValueCollection parameters)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    string rzid = parameters["ID"];
                    if (string.IsNullOrEmpty(rzid))
                    {
                        Guid guid = Guid.Parse(rzid);
                        var NewET = (from a in _db.RZ_SunWuPeiChang
                                     where a.ID == guid
                                     select a).ToList();
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
        public List<RZ_SunWuPeiChang> GetKaFanFeiYongList(NameValueCollection parameters,out int total)
        {
            List<RZ_SunWuPeiChang> resultdatalist = new List<RZ_SunWuPeiChang>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_SunWuPeiChang newSW = new RZ_SunWuPeiChang();
                    EntityHelper.FormDataToDataObject(newSW, parameters);
                    resultdatalist =( from a in _db.RZ_SunWuPeiChang
                                   where a.SW_FeiyongType==newSW.SW_FeiyongType  && a.SW_OrderGuid == newSW.SW_OrderGuid
                                   orderby a.SW_RooMNum 
                                   select a).ToList();
                    if (newSW.ID != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        resultdatalist = resultdatalist.Where(a => a.ID == newSW.ID).ToList();
                    }
                    string pageinfostr = parameters["PageInfo"];
                    HotelLogic.CommonModel.PageParams page= HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                    total = resultdatalist.Count();
                    //跳过的总条数  
                    int totalNum = (page.PageNumber - 1) * page.PageSize;
                    resultdatalist = resultdatalist.Skip(totalNum).ToList();
                    if (resultdatalist.Count > page.PageSize)
                    {
                        resultdatalist.RemoveRange(page.PageSize, resultdatalist.Count - page.PageSize);
                    }
                    if (CashCommHelper.IsOrderNotEnd(newSW.SW_OrderGuid).Flag)
                    {
                        resultdatalist.Add(new RZ_SunWuPeiChang());
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
