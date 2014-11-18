using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
   public  class FHChargeHelper
    {
       public static string  GetKFByFH(string FH)
       {
           //-1表示没有该房间
           //0表示空房//1表示已入住
           using (HotelDBEntities db = new HotelDBEntities())
           {
               var kf = (from a in db.Zd_Fj
                         where a.f_fh == FH
                         select a).ToList().FirstOrDefault();
               if (kf != null)
               {
                   if (kf.f_ztmc == "在客")
                   {
                       return "1";
                   }
                   if (kf.f_ztmc == "空房")
                   {
                       return "0";
                   }
               }
           }
           return "-1";
       }

       public static KFViewModule getInfoByNo(string fh)
       {
           KFViewModule Mb = new KFViewModule();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var r = (from a in _db.Zd_Fj where a.f_fh == fh select a).ToList().FirstOrDefault();
               if(r!=null)
                {
                    Mb.DJ = r.f_dj;
                }
            }
            return Mb;
   }
    }
}
