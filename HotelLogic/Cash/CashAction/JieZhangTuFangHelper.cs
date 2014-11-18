using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;
using System.Transactions;
using System.Data.Common;
using System.Data.SqlClient;

namespace HotelLogic.Cash.CashAction
{
    public class JieZhangTuFangHelper
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
        private static JieZhangTuFangHelper _instance;

        public static JieZhangTuFangHelper Instance
        {
            get
            {
                return _instance == null ? _instance = new JieZhangTuFangHelper() : _instance;
            }
        }
        private JieZhangTuFangHelper() { }
        #endregion

        #region 操作实现函数列表

        /// <summary>
        /// 新增或更新结账退房支付信息操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdateMaiJieZhange(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;
            string Exustatuc = parameters["ExuStatuc"];
            lock (this)
            {                
                using (TransactionScope scope = new TransactionScope())
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        try
                        {
                            //保存主要结账数据
                            RZ_JieZhangTuiFang ZTModel = new RZ_JieZhangTuiFang();
                            //赋值到对象    
                            EntityHelper.FormDataToDataObject(ZTModel, parameters);
                            //判断是否存在未还租借物品
                            if (Exustatuc == "AllEnd")
                            {
                                var temp = _db.zd_wp_borrow.Count(a => a.OrderGuid == ZTModel.JZ_OrderGUID && a.state == 0);
                                if (temp != null)
                                {
                                    if (temp > 0)
                                    {
                                        resultEntiry.Flag = false;
                                        resultEntiry.Message = "抱歉，还有租借物品未归还，不能进行结账退房！";
                                        resultEntiry.value = temp;
                                        return resultEntiry;
                                    }
                                }
                            }
                           
                            //判断入住订单是否已经结账
                            resultEntiry = CashCommHelper.IsOrderNotEnd(ZTModel.JZ_OrderGUID);
                            if (!resultEntiry.Flag) return resultEntiry;
                            //消费金额等于0时主要是由于单位记账提前保存的
                            //通常情况是不是允许记录消费金额为零的结账单
                            RZ_JieZhangTuiFang EntityModel = (from a in _db.RZ_JieZhangTuiFang
                                                              where a.ID == ZTModel.ID
                                                                && a.JZ_Consumption > 0
                                                              select a).SingleOrDefault();
                            RZ_JieZhangTuiFang RealEntityModel = (from a in _db.RZ_JieZhangTuiFang
                                                                  where a.ID == ZTModel.ID
                                                                  select a).SingleOrDefault();
                            ZTModel.UpdateTime = DateTime.Now;
                            ZTModel.UpdateUser = UserName;

                            if (EntityModel == null)
                            {
                                //首先保存结账数据明细
                                string feiyongrows = parameters["FeiYongRows"];
                                BuFengJieZhangHelper _bf = BuFengJieZhangHelper.Instance;
                                List<RZFeiYongModel> feiyonglist = new List<RZFeiYongModel>();
                                if (!string.IsNullOrEmpty(feiyongrows))
                                {
                                    feiyonglist = EntityHelper.GetEntityInfoFromJsonPage<RZFeiYongModel>(feiyongrows);
                                }
                                else
                                {
                                    int total = 0;
                                    Guid orderguiid = Guid.Parse(ZTModel.JZ_OrderGUID.ToString());
                                    feiyonglist = _bf.GetRiZhuFeiYongList(orderguiid, ZTModel.ID,
                                        "", 1, 10000, out total);
                                }
                                _bf.AddOrUpdate(feiyonglist, ZTModel.ID, "结账");

                                //增加相应的主结账信息
                                if (RealEntityModel != null)
                                {
                                    ZTModel.CreateTime = DateTime.Now;
                                    ZTModel.UpdateTime = DateTime.Now;
                                    ZTModel.UpdateUser = UserName;
                                    ZTModel.CreateUser = UserName;
                                    EntityHelper.ConverToEntity<RZ_JieZhangTuiFang, RZ_JieZhangTuiFang>(RealEntityModel, ZTModel);
                                    _db.ObjectStateManager.ChangeObjectState(RealEntityModel, System.Data.EntityState.Modified);
                                }
                                else
                                {
                                    ZTModel.CreateTime = DateTime.Now;
                                    ZTModel.CreateUser = UserName;
                                    _db.RZ_JieZhangTuiFang.AddObject(ZTModel);
                                }
                            }
                            else
                            {

                                ZTModel.CreateTime = RealEntityModel.CreateTime;
                                ZTModel.UpdateTime = RealEntityModel.UpdateTime;
                                ZTModel.UpdateUser = RealEntityModel.UpdateUser;
                                ZTModel.CreateUser = RealEntityModel.CreateUser;
                                EntityHelper.ConverToEntity<RZ_JieZhangTuiFang, RZ_JieZhangTuiFang>(EntityModel, ZTModel);
                                _db.ObjectStateManager.ChangeObjectState(EntityModel, System.Data.EntityState.Modified);
                            }

                            //保存明细数据
                            string rowsstr = parameters["ChildRows"];
                            List<RZ_JieZhangDetail> newdatalist = EntityHelper.GetEntityInfoFromJsonPage<RZ_JieZhangDetail>(rowsstr);
                            if (newdatalist != null && newdatalist.Count > 0)
                            {
                                foreach (RZ_JieZhangDetail item in newdatalist)
                                {
                                    item.JZD_JZID = ZTModel.ID;
                                    _db.RZ_JieZhangDetail.AddObject(item);
                                    if (item.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                                    {
                                        _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Added);
                                        item.ID = System.Guid.NewGuid();
                                        item.CreateTime = DateTime.Now;
                                    }
                                    else
                                    {
                                        _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
                                    }
                                    item.UpdateTime = DateTime.Now;
                                }
                            }
                            _db.SaveChanges();

                            if (Exustatuc == "AllEnd")
                            {
                                UpdateEndJieZhanTuFan(ZTModel.JZ_OrderGUID,true);
                            }


                            resultEntiry.Flag = true;
                            resultEntiry.Message = "执行成功！";
                            resultEntiry.value = ZTModel.ID;
                            scope.Complete();
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
        /// 将入住订单进行结账，并将相关房间设置为脏房
        /// </summary>
        /// <param name="orderguid"></param>
        /// <returns></returns>
        public void UpdateEndJieZhanTuFan(Guid? orderguid,Boolean statuc)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //查询订单相关的费用明细信息
                var datalist = (from a in _db.H_RuzhuDetail
                                where a.OrderGuid == orderguid
                                select a).ToList();
                //将订单相关联房间记录设置为结账状态
                foreach (H_RuzhuDetail item in datalist)
                {
                    var room = (from a in _db.Zd_Fj
                                where a.f_fh == item.FangJianHao
                                select a).SingleOrDefault();
                    if (statuc)
                    {
                        room.f_ztmc = "脏房";
                    }
                    else
                    {
                        room.f_ztmc = "在客";
                    }
                    _db.ObjectStateManager.ChangeObjectState(room, System.Data.EntityState.Modified);
                }
                //将订单相关联房间记录设置为结账状态
                foreach (H_RuzhuDetail item in datalist)
                {
                    item.Status = (short)(statuc == true ? 1 : 0); ;
                    item.LeaveTime = DateTime.Now;
                    _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
                }

                //将主要入住订单相关联房间记录设置为结账状态
                H_RuzhuOrder HrOrder = (from a in _db.H_RuzhuOrder
                                        where a.OrderGuid == orderguid
                                        select a).SingleOrDefault();
                HrOrder.Status =(short)(statuc == true ? 1 : 0);
                HrOrder.LidianTime = DateTime.Now;
                _db.ObjectStateManager.ChangeObjectState(HrOrder, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }

        }
        /// <summary>
        /// 将入住订单进行结账，并将相关房间设置为脏房
        /// </summary>
        /// <param name="orderguid"></param>
        /// <returns></returns>
        public void CanceEndJieZhanTuFan(Guid? orderguid)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //查询订单相关的费用明细信息
                var datalist = (from a in _db.H_RuzhuDetail
                                where a.OrderGuid == orderguid
                                select a).ToList();
                
                //将订单相关联房间记录设置为结账状态
                foreach (H_RuzhuDetail item in datalist)
                {
                    item.Status = 0;                   
                    _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
                }

                //将主要入住订单相关联房间记录设置为结账状态
                H_RuzhuOrder HrOrder = (from a in _db.H_RuzhuOrder
                                        where a.OrderGuid == orderguid
                                        select a).SingleOrDefault();
                HrOrder.Status = 0;
                _db.ObjectStateManager.ChangeObjectState(HrOrder, System.Data.EntityState.Modified);
                _db.SaveChanges();
            }

        }
        /// <summary>
        /// 将入住订单进行结账，并将相关房间设置为脏房
        /// </summary>
        /// <param name="orderguid"></param>
        /// <returns></returns>
        public ResultMode BuJieZhanTuFan(Guid? orderguid, Boolean statuc,string rooms)
        {
            ResultMode rm = new ResultMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                //将主要入住订单相关联房间记录设置为结账状态
                H_RuzhuOrder HrOrder = (from a in _db.H_RuzhuOrder
                                        where a.OrderGuid == orderguid
                                        select a).SingleOrDefault();
                if (HrOrder == null)
                {
                    rm.Flag = false;
                    rm.Message = "执行失败，原因：无法查询入住订单信息";
                    rm.Where = "BuJieZhanTuFan";
                    return rm;
                }
                if (HrOrder.Status == 1)
                {
                    rm.Flag = false;
                    rm.Message = "执行失败，原因：此订单已经结账";
                    rm.Where = "BuJieZhanTuFan";
                    return rm;
                }
                //查询订单相关的费用明细信息
                var datalist = (from a in _db.H_RuzhuDetail
                                where a.OrderGuid == orderguid
                                select a).ToList();
                //将订单相关联房间记录设置为结账状态
                foreach (H_RuzhuDetail item in datalist)
                {
                    var room = (from a in _db.Zd_Fj
                                where a.f_fh == item.FangJianHao
                                select a).SingleOrDefault();
                    if (rooms.Contains(room.f_fh + ","))
                    {
                        if (statuc)
                        {
                            room.f_ztmc = "脏房";
                        }
                        else
                        {
                            room.f_ztmc = "在客";
                        }
                        _db.ObjectStateManager.ChangeObjectState(room, System.Data.EntityState.Modified);
                    }
                }
                //将订单相关联房间记录设置为离店时间
                foreach (H_RuzhuDetail item in datalist)
                {
                    if (rooms.Contains(item.FangJianHao + ","))
                    {
                        item.Status = 1;
                        item.LeaveTime = DateTime.Now;
                        _db.ObjectStateManager.ChangeObjectState(item, System.Data.EntityState.Modified);
                    }
                }
                if (rooms.Contains(HrOrder.FangHao + ","))
                {
                    HrOrder.LidianTime = DateTime.Now;
                }
                _db.SaveChanges();
                rm.Flag = true;
                rm.Message = "执行成功！";
                rm.Where = "BuJieZhanTuFan";
            }
            return rm;
        }
        /// <summary>
        /// 删除结账退房支付记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode DeleteMaiJieZhange(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode();
            lock (this)
            {
                //赋值到对象
                RZ_JieZhangTuiFang newZT = new RZ_JieZhangTuiFang();
                EntityHelper.FormDataToDataObject(newZT, parameters);
                //判断入住订单是否已经结账退房
                resultEntiry = CashCommHelper.IsOrderNotEnd(newZT.JZ_OrderGUID);
                if (!resultEntiry.Flag) return resultEntiry;

                //将相关费用记录重置为未结状态
                BuFengJieZhangHelper _bf = BuFengJieZhangHelper.Instance;
                List<RZFeiYongModel> feiyonglist = new List<RZFeiYongModel>();
                int total = 0;
                Guid orderguiid = Guid.Parse(newZT.JZ_OrderGUID.ToString());
                feiyonglist = _bf.GetRiZhuFeiYongList(orderguiid, newZT.ID,
                    "结账", 1, 10000, out total);
                using (TransactionScope scope = new TransactionScope())
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        try
                        {
                            
                            //删除单位记账记录
                            string unitdeltxt = @"DELETE FROM [RZ_CreditProtocolUnit] 
                                                  WHERE [CP_JZID]=@JZID";
                            var parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "JZID", Value = newZT.ID}};
                            var unitdelcount = _db.ExecuteStoreCommand(unitdeltxt, parm);
                            //删除费用与结账记录的关联数据删除
                            string deltxt = @"DELETE FROM [RZ_FeiYongJieZhangRelation] 
                                            WHERE [JZ_JZID]=@JZ_JZID";
                            parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "JZ_JZID", Value = newZT.ID}};
                            var delcount = _db.ExecuteStoreCommand(deltxt, parm);
                           
                            _bf.AddOrUpdate(feiyonglist, newZT.ID, null);
                            //其次删除相关支付记录
                            foreach (var itemt in
                                 (from a in _db.RZ_JieZhangDetail
                                  where a.JZD_JZID == newZT.ID
                                  orderby a.JZD_ParSeqNumber
                                  select a).ToList())
                            {
                                _db.RZ_JieZhangDetail.DeleteObject(itemt);
                            }
                            _db.RZ_JieZhangTuiFang.AddObject(newZT);
                            _db.ObjectStateManager.ChangeObjectState(newZT, System.Data.EntityState.Deleted);
                            _db.SaveChanges();
                            scope.Complete();
                            resultEntiry.Flag = true;
                            resultEntiry.Message = "执行成功！";
                        }
                        catch (Exception e)
                        {
                            resultEntiry.Flag = false;
                            resultEntiry.Where = "DeleteMaiJieZhange";
                            resultEntiry.Message = e.Message;
                        }

                    }
                }
            }
            return resultEntiry;
        }


        /// <summary>
        /// 删除结账退房支付记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode CancelMaiJieZhange(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode();
            string Exustatuc = parameters["ExuStatuc"];
            lock (this)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (HotelDBEntities _db = new HotelDBEntities())
                    {
                        try
                        {
                            //赋值到对象
                            RZ_JieZhangTuiFang newZT = new RZ_JieZhangTuiFang();
                            EntityHelper.FormDataToDataObject(newZT, parameters);

                            //删除单位记账记录
                            string unitdeltxt = @"DELETE FROM [RZ_CreditProtocolUnit] 
                                                  WHERE [CP_OrderGUID]=@OrderGUID";
                            var parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "OrderGUID", Value = newZT.JZ_OrderGUID}};
                            var unitdelcount = _db.ExecuteStoreCommand(unitdeltxt, parm);
                            //删除费用与结账记录的关联数据删除
                            string deltxt = @"DELETE FROM [RZ_FeiYongJieZhangRelation]
                                              WHERE JZ_JZID IN (  
                                                SELECT ID FROM RZ_JieZhangTuiFang
                                                WHERE JZ_OrderGUID=@OrderGUID )";
                            parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "OrderGUID", Value = newZT.JZ_OrderGUID}};
                            var delcount = _db.ExecuteStoreCommand(deltxt, parm);
                            //将相关费用记录重置为未结状态
                            BuFengJieZhangHelper _bf = BuFengJieZhangHelper.Instance;
                            List<RZFeiYongModel> feiyonglist = new List<RZFeiYongModel>();
                            int total = 0;
                            Guid orderguiid = Guid.Parse(newZT.JZ_OrderGUID.ToString());
                            feiyonglist = _bf.GetRiZhuFeiYongList(orderguiid, System.Guid.Parse("00000000-0000-0000-0000-000000000000"),
                                "结账", 1, 100000, out total);
                            _bf.AddOrUpdate(feiyonglist, newZT.ID, null);
                            //其次删除相关支付记录
                            string paydeltxt = @"DELETE  FROM [RZ_JieZhangDetail]                                              
                                                 WHERE JZD_JZID IN (  
                                                    SELECT ID FROM dbo.RZ_JieZhangTuiFang
                                                    WHERE JZ_OrderGUID=@OrderGUID
                                                  )";
                             parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "OrderGUID", Value = newZT.JZ_OrderGUID}};
                             var paydelcount = _db.ExecuteStoreCommand(paydeltxt, parm);
                             //最后删除相关结账记录
                             string jzdeltxt = @"DELETE  FROM [RZ_JieZhangTuiFang]                                              
                                                 WHERE JZ_OrderGUID =@OrderGUID ";
                             parm = new DbParameter[] {
                                 new SqlParameter { ParameterName = "OrderGUID", Value = newZT.JZ_OrderGUID}};
                             var jzdelcount = _db.ExecuteStoreCommand(jzdeltxt, parm);
                            _db.SaveChanges();
                            if (Exustatuc == "AllEnd")
                            {
                                CanceEndJieZhanTuFan(newZT.JZ_OrderGUID);
                            }
                            scope.Complete();
                            resultEntiry.Flag = true;
                            resultEntiry.Message = "执行成功！";
                        }
                        catch (Exception e)
                        {
                            resultEntiry.Flag = false;
                            resultEntiry.Where = "DeleteMaiJieZhange";
                            resultEntiry.Message = e.Message;
                        }

                    }
                }
            }
            return resultEntiry;
        }
        /// <summary>
        /// 新增或更新结账退房支付信息操作
        /// </summary>
        /// <param name="KF"></param>
        /// <returns></returns>
        public ResultMode AddOrUpdatePayDetails(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode(); ;

            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        RZ_JieZhangDetail KFModel = new RZ_JieZhangDetail();
                        //赋值到对象
                        EntityHelper.FormDataToDataObject(KFModel, parameters);
                        _db.RZ_JieZhangDetail.AddObject(KFModel);
                        if (KFModel.ID == System.Guid.Parse("00000000-0000-0000-0000-000000000000"))
                        {
                            _db.ObjectStateManager.ChangeObjectState(KFModel, System.Data.EntityState.Added);
                            KFModel.ID = System.Guid.NewGuid();
                            KFModel.CreateTime = DateTime.Now;
                        }
                        else
                        {
                            _db.ObjectStateManager.ChangeObjectState(KFModel, System.Data.EntityState.Modified);
                        }
                        KFModel.UpdateTime = DateTime.Now;

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
        /// 删除结账退房支付记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode DeletePayDetails(NameValueCollection parameters)
        {
            ResultMode resultEntiry = new ResultMode();
            lock (this)
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    try
                    {
                        //赋值到对象
                        RZ_JieZhangDetail newKF = new RZ_JieZhangDetail();
                        EntityHelper.FormDataToDataObject(newKF, parameters);
                        _db.RZ_JieZhangDetail.AddObject(newKF);
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
        /// 获取主要结账退房信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public RZ_JieZhangTuiFang GetMainEntiry(NameValueCollection parameters)
        {
            RZ_JieZhangTuiFang EntityZT = null;
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_JieZhangTuiFang newZT = new RZ_JieZhangTuiFang();
                    EntityHelper.FormDataToDataObject(newZT, parameters);
                    //首先清除相关挂账时产生的空数据
                    ClearJieZhangNullUnit();

                    EntityZT = (from a in _db.RZ_JieZhangTuiFang
                                where a.JZ_OrderGUID == newZT.JZ_OrderGUID
                                   && a.ID == newZT.ID
                                orderby a.CreateTime
                                select a).SingleOrDefault();
                    if (EntityZT == null)
                    {
                        EntityZT = new RZ_JieZhangTuiFang();
                        EntityZT.ID = Guid.NewGuid();
                        EntityZT.JZ_Money = 0;
                        EntityZT.JZ_Consumption = 0;
                        EntityZT.JZ_OrderGUID = newZT.JZ_OrderGUID;
                    }
                    OrderInfoMode ordermode = GetOrderInfo(newZT.JZ_OrderGUID.ToString());
                    EntityZT.JZ_ALLConsumption = ordermode.allCosumper;
                    EntityZT.JZ_Deposit = ordermode.YaJin-ordermode.allDepositDeduct;

                    return EntityZT;
                }
                catch (Exception e)
                {

                }

            }
            return EntityZT;
        }

        /// <summary>
        ///根据主要结账信息来查询相关支付记录
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<RZ_JieZhangDetail> GetPayDetailsList(NameValueCollection parameters, out int total)
        {
            List<RZ_JieZhangDetail> resultdatalist = new List<RZ_JieZhangDetail>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    RZ_JieZhangTuiFang newZT = new RZ_JieZhangTuiFang();
                    EntityHelper.FormDataToDataObject(newZT, parameters);
                    var datalist = from a in _db.RZ_JieZhangDetail
                                   where a.JZD_JZID == newZT.ID
                                   orderby a.JZD_ParSeqNumber
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
                    else
                    {
                        if (resultdatalist.Count <= 0)
                        {
                            //将相关结账支付明细初始化出来显示给用户录入
                            var demodatalist = from a in _db.Basic_Payment
                                               where a.BPM_IsUseCheckout == true //a.KF_OrderGuid == newKF.KF_OrderGuid
                                               orderby a.BPM_SeqNumber
                                               select a;
                            total = demodatalist.Count();
                            var demoresultdatalist = demodatalist.Skip(totalNum).ToList();
                            if (demoresultdatalist.Count > page.PageSize)
                            {
                                demoresultdatalist.RemoveRange(page.PageSize, demoresultdatalist.Count - page.PageSize);
                            }
                            foreach (var item in demoresultdatalist)
                            {
                                RZ_JieZhangDetail itemzd = new RZ_JieZhangDetail();
                                itemzd.JZD_JZID = newZT.ID;
                                itemzd.JZD_ParSeqNumber = item.BPM_SeqNumber;
                                itemzd.JZD_PayType = item.BPM_PayMentName;
                                itemzd.JZD_Money = 0;
                                resultdatalist.Add(itemzd);
                            }
                        }
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
        /// 查询入住订单有关结账信息
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public OrderInfoMode GetOrderInfo(string orderid)
        {
            OrderInfoMode orderinfo = new OrderInfoMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {

                    string sqltxt = @" SELECT [AutoID]
                                            ,[ChangTu]
                                            ,[ShiHua]
                                            ,[ChangBao]
                                            ,[ZhongDian]
                                            ,[DaodianTime]
                                            ,[LidianTime]
                                            ,[FukuanFangshi]
                                            ,[YaJin]
                                            ,[allYajingDeduct]
                                            ,[allCosumper]
                                            ,[OrderGuid]
                                            ,[XingMing]
                                            ,[FangHao]
                                            ,[KerenLeibie]
                                            ,ISNULL([allMoney],0) AS allMoney
                                            ,ISNULL([allPreferentialy],0) AS allPreferentialy
                                            ,ISNULL([allDepositDeduct],0) AS allDepositDeduct
                                            ,ISNULL([ALLPaid],0) AS ALLPaid
                                            ,ISNULL([ALLAccounts],0) AS ALLAccounts
                                            ,ISNULL([ALLSurplus],0) AS ALLSurplus
                                            ,ISNULL([ALLJZConsumption],0) AS ALLJZConsumption
                                     FROM [RZ_OrderInfoView]  A
                                     WHERE A.[OrderGuid]=@OrderGuid";
                    var parm = new DbParameter[] {
                        new SqlParameter { ParameterName = "OrderGuid", Value = orderid}
                    };
                    var tempresult = _db.ExecuteStoreQuery<OrderInfoMode>(sqltxt, parm);
                    List<OrderInfoMode> datalist = tempresult.ToList<OrderInfoMode>();
                    if (datalist != null && datalist.Count > 0)
                    {
                        orderinfo = datalist[0];
                    }
                    return orderinfo;
                }
                catch (Exception e)
                {
                    return orderinfo;
                }
            }
        #endregion
        }
        /// <summary>
        /// 获取已经结账的费用明细信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="alltotal"></param>
        /// <returns></returns>
        public List<RZFeiYongModel> GetJZFeiYongList(NameValueCollection parameters, out int alltotal)
        {
            string orderguid = parameters["RZ_OrderGuid"];
            string jzID = parameters["JZID"];
            string pageinfostr = parameters["PageInfo"];
            HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
            BuFengJieZhangHelper bfjzhelper = BuFengJieZhangHelper.Instance;
            alltotal = 0;
            List<RZFeiYongModel> resultlist = bfjzhelper.GetRiZhuFeiYongList(Guid.Parse(orderguid), Guid.Parse(jzID), "结账", page.PageNumber, page.PageSize, out alltotal);
            return resultlist;
        }

        //获取入住订单下的结账信息
        public List<RZ_JieZhangTuiFang> GetJZTuFanList(NameValueCollection parameters, out int total)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                List<RZ_JieZhangTuiFang> resutlist = new List<RZ_JieZhangTuiFang>();
                string orderguid = parameters["OrderGuid"];
                Guid OrderGuid = Guid.Parse(orderguid);
                var datalist = (from a in _db.RZ_JieZhangTuiFang
                                where a.JZ_OrderGUID == OrderGuid
                                select a).ToList();
                string pageinfostr = parameters["PageInfo"];
                HotelLogic.CommonModel.PageParams page = HotelLogic.Utils.GetPageInfoFromJsonPage(pageinfostr);
                total = datalist.Count();
                //跳过的总条数  
                int totalNum = (page.PageNumber - 1) * page.PageSize;
                resutlist = datalist.Skip(totalNum).ToList<RZ_JieZhangTuiFang>();
                if (resutlist.Count > page.PageSize)
                {
                    resutlist.RemoveRange(page.PageSize, resutlist.Count - page.PageSize);
                }
                return resutlist;
            }
        }
        /// <summary>
        ///根据主要结账信息来查询相关房间信息
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public List<H_RuzhuDetail> GetOrderRooms(NameValueCollection parameters, out int total)
        {
            List<H_RuzhuDetail> resultdatalist = new List<H_RuzhuDetail>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    H_RuzhuDetail newZT = new H_RuzhuDetail();
                    EntityHelper.FormDataToDataObject(newZT, parameters);
                    var datalist = from a in _db.H_RuzhuDetail
                                   where a.OrderGuid == newZT.OrderGuid
                                   orderby a.ArriveTime
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

        
        /// <summary>
        ///不结账退房
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode BuiJieZhangTuiFan(NameValueCollection parameters)
        {
            ResultMode rm = new ResultMode();
            string orderguid = parameters["OrderGuid"];
            string rooms = parameters["rooms"];
            if (!string.IsNullOrEmpty(orderguid))
            {
                try
                {
                    JieZhangTuFangHelper jfhelper = JieZhangTuFangHelper.Instance;
                    
                    rm= jfhelper.BuJieZhanTuFan(Guid.Parse(orderguid), true,rooms);                   
                    rm.Where = "BuiJieZhangTuiFan";
                }
                catch (Exception e)
                {
                    rm.Flag = false;
                    rm.Message = "执行失败!" + e.Message;
                    rm.Where = "BuiJieZhangTuiFan";
                }
            }
            else
            {
                rm.Flag = false;
                rm.Message = "执行失败!没有应用的Orderguid";
                rm.Where = "BuiJieZhangTuiFan";
            }
            return rm;
        }
        /// <summary>
        /// 清空因挂账时产生的临时数据
        /// </summary>
        public void ClearJieZhangNullUnit()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                string sqt = @"DELETE FROM [RZ_CreditProtocolUnit]
                              WHERE CP_JZID IN (SELECT  [ID]      
                            FROM [RZ_JieZhangTuiFang]
                            WHERE JZ_ALLConsumption IS NULL
                            );
                            DELETE FROM [RZ_JieZhangTuiFang]
                            WHERE JZ_ALLConsumption IS NULL";
                _db.ExecuteStoreCommand(sqt);
                _db.SaveChanges();
            }
        }
        /// <summary>
        /// 判断入住订单是否已经结账
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ResultMode IsOrderJieZhang(NameValueCollection parameters)
        {
            //判断入住订单是否已经结账
            string orderguid = parameters["OrderGuid"];
            ResultMode  resultEntiry = CashCommHelper.IsOrderNotEnd(Guid.Parse(orderguid));
            return resultEntiry;
        }
    }
    #region 相关查询结果实体模型
    public class OrderInfoMode
    {
        public string AutoID { get; set; }
        public bool? ChangTu { get; set; }
        public bool? ShiHua { get; set; }
        public bool? ChangBao { get; set; }
        public bool? ZhongDian { get; set; }
        public DateTime? DaodianTime { get; set; }
        public DateTime? LidianTime { get; set; }
        public string FukuanFangshi { get; set; }
        public decimal YaJin { get; set; }
        public decimal allYajingDeduct { get; set; }
        public decimal allCosumper { get; set; }
        public Guid OrderGuid { get; set; }
        public string XingMing { get; set; }
        public string FangHao { get; set; }
        public string KerenLeibie { get; set; }
        public decimal allMoney { get; set; }
        public decimal allPreferentialy { get; set; }
        public decimal allDepositDeduct { get; set; }
        public decimal ALLPaid { get; set; }
        public decimal ALLAccounts { get; set; }
        public decimal ALLSurplus { get; set; }
        public decimal ALLJZConsumption { get; set; }
    }
    #endregion
}
