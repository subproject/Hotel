using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Cash.CashAction
{
    public class QingTaFuXianHelper
    {
        #region 初始化
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        private static QingTaFuXianHelper _instance;
       
        public static QingTaFuXianHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new QingTaFuXianHelper() : _instance;
            }
        }
        private QingTaFuXianHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 新增或更新前台付现记录操作
        /// </summary>
        /// <param name="parameters"></param>
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
                        RZ_QingTaFuXian QXModel = new RZ_QingTaFuXian();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(QXModel, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(QXModel.QX_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_QingTaFuXian.AddObject(QXModel); 
                        if (QXModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(QXModel, System.Data.EntityState.Added);
                            QXModel.ID = System.Guid.NewGuid();
                            QXModel.CreateTime = DateTime.Now;
                            QXModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(QXModel, System.Data.EntityState.Modified);
                        }                       
                        QXModel.UpdateTime = DateTime.Now;
                        QXModel.CreateUser = UserName;
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
        //删除前台付现记录
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
                        RZ_QingTaFuXian newQX = new RZ_QingTaFuXian();
                        EntityHelper.FormDataToDataObject(newQX, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newQX.QX_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;

                        _db.RZ_QingTaFuXian.AddObject(newQX);
                        _db.ObjectStateManager.ChangeObjectState(newQX, System.Data.EntityState.Deleted);
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
        public List<RZ_QingTaFuXian> GetKaFanFeiYongList(NameValueCollection parameters,out int total)
        {
            List<RZ_QingTaFuXian> resultdatalist = new List<RZ_QingTaFuXian>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_QingTaFuXian newQX = new RZ_QingTaFuXian();
                    EntityHelper.FormDataToDataObject(newQX, parameters);
                    var datalist = from a in _db.RZ_QingTaFuXian
                                   where a.QX_OrderGUID==newQX.QX_OrderGUID
                                   orderby a.QX_Room
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
                    if (CashCommHelper.IsOrderNotEnd(newQX.QX_OrderGUID).Flag)
                    {
                        resultdatalist.Add(new RZ_QingTaFuXian());
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
        public List<Zd_Hxfs> GetQianTaiPayTypeList(NameValueCollection parameters, out int total)
        {
            List<Zd_Hxfs> resultdatalist = new List<Zd_Hxfs>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_QingTaFuXian newQX = new RZ_QingTaFuXian();
                    EntityHelper.FormDataToDataObject(newQX, parameters);
                    var datalist = from a in _db.Zd_Hxfs
                                   orderby a.id
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
