using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Setting
{
    public class BasicPaymentHelper
    {
        #region 初始化
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        private static BasicPaymentHelper _instance;
       
        public static BasicPaymentHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new BasicPaymentHelper() : _instance;
            }
        }
        private BasicPaymentHelper() { }
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 新增或更新客户费用记录操作
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
                         Basic_Payment PMModel = new Basic_Payment();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(PMModel, parameters);
                        _db.Basic_Payment.AddObject(PMModel); 
                        if (PMModel.ID ==0)
                        {
                            _db.ObjectStateManager.ChangeObjectState(PMModel, System.Data.EntityState.Added);                            
                            PMModel.CreateTime = DateTime.Now;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(PMModel, System.Data.EntityState.Modified);
                        }                       
                        PMModel.UpdateTime = DateTime.Now;
                        
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
                        Basic_Payment newPM = new Basic_Payment();
                        EntityHelper.FormDataToDataObject(newPM, parameters);
                        _db.Basic_Payment.AddObject(newPM);
                        _db.ObjectStateManager.ChangeObjectState(newPM, System.Data.EntityState.Deleted);
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
        public List<Basic_Payment> GetKaFanFeiYongList(NameValueCollection parameters,out int total)
        {
            List<Basic_Payment> resultdatalist = new List<Basic_Payment>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    Basic_Payment newQX = new Basic_Payment();
                    EntityHelper.FormDataToDataObject(newQX, parameters);
                    var datalist = from a in _db.Basic_Payment
                                   orderby a.BPM_SeqNumber
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
                    resultdatalist.Add(new Basic_Payment());
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
