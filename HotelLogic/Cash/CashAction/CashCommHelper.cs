using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using System.Collections.Specialized;
using HotelLogic.CommonModel;

namespace HotelLogic.Cash.CashAction
{
    public static class CashCommHelper
    {
        #region 初始化
        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public static string Roles { get; set; }
        
        #endregion

        #region 操作实现函数列表
        /// <summary>
        /// 判断入住订单是否已经结账
        /// </summary>
        /// <param name="ordergui"></param>
        /// <returns></returns>
        public static ResultMode IsOrderNotEnd(Guid? ordergui)
        {
            ResultMode resultEntiry = new ResultMode();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var statuc = (from a in _db.H_RuzhuOrder
                                  where a.OrderGuid == ordergui
                                  select a.Status).SingleOrDefault();

                    if (statuc == 1)
                    {
                        resultEntiry.Flag =false ;
                        resultEntiry.Message = "抱歉，入住订单已经结账，不能再进行相关更新操作!";
                    }
                    else
                    {
                        resultEntiry.Flag = true;                        
                    }
                }
                catch (Exception e)
                {
                    resultEntiry.Flag = false;
                    resultEntiry.Message = "无此订单!";
                }
            }
            return resultEntiry;

        }

        #endregion
    }
  
}
