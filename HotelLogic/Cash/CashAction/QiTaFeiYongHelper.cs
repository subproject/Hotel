using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;
using HotelLogic.Setting;

namespace HotelLogic.Cash.CashAction
{
    public class QiTaFeiYongHelper
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
        private static QiTaFeiYongHelper _instance;

        public static QiTaFeiYongHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new QiTaFeiYongHelper() : _instance;
            }
        }
        private QiTaFeiYongHelper() { }
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
                        RZ_QiTaFeiYong QFModel = new RZ_QiTaFeiYong();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(QFModel, parameters);
                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(QFModel.QF_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;


                        _db.RZ_QiTaFeiYong.AddObject(QFModel);
                        if (QFModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(QFModel, System.Data.EntityState.Added);
                            QFModel.ID = System.Guid.NewGuid();
                            QFModel.CreateTime = DateTime.Now;
                            QFModel.CreateUser = UserName;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(QFModel, System.Data.EntityState.Modified);
                        }
                        QFModel.UpdateTime = DateTime.Now;
                        QFModel.UpdateUser = UserName;

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
                        RZ_QiTaFeiYong newQF = new RZ_QiTaFeiYong();
                        EntityHelper.FormDataToDataObject(newQF, parameters);

                        //判断入住订单是否已经结账退房
                        resultEntiry = CashCommHelper.IsOrderNotEnd(newQF.QF_OrderGUID);
                        if (!resultEntiry.Flag) return resultEntiry;


                        _db.RZ_QiTaFeiYong.AddObject(newQF);
                        _db.ObjectStateManager.ChangeObjectState(newQF, System.Data.EntityState.Deleted);
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
        public List<RZ_QiTaFeiYong> GetKaFanFeiYongList(NameValueCollection parameters, out int total)
        {
            List<RZ_QiTaFeiYong> resultdatalist = new List<RZ_QiTaFeiYong>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_QiTaFeiYong newQF = new RZ_QiTaFeiYong();
                    EntityHelper.FormDataToDataObject(newQF, parameters);
                    resultdatalist = (from a in _db.RZ_QiTaFeiYong
                                      where a.QF_OrderGUID == newQF.QF_OrderGUID
                                      orderby a.QF_Room
                                      select a).ToList();
                    if (newQF.ID != System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        resultdatalist = resultdatalist.Where(a => a.ID == newQF.ID).ToList();
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

                    if (CashCommHelper.IsOrderNotEnd(newQF.QF_OrderGUID).Flag)
                    {
                        resultdatalist.Add(new RZ_QiTaFeiYong());
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
        //查询相关科目
        public List<KMViewModel> GetKeMuList()
        {
            KMHelper km = new KMHelper();
            return km.ReadAll();
        }

        #endregion
    }
}
