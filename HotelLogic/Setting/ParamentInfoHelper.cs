using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;
using HotelLogic.Setting;

namespace HotelLogic.Setting
{
  public  class ParamentInfoHelper
    {
      public string CreateOrder(CheckInOutMemoModel order)
      {
          string result = "-1";
          using (HotelDBEntities db = new HotelDBEntities())
          {
              try
              {
                  var res = db.CheckInOutMemo.FirstOrDefault(a => a.id > 0);
                
                      CheckInOutMemo temp = new CheckInOutMemo();
                      temp.CheckInMemo1 = order.CheckInMemo1;
                      temp.CheckInMemo2 = order.CheckInMemo2;
                      temp.CheckInMemo3 = order.CheckInMemo3;
                      if (res == null)
                      {
                          db.CheckInOutMemo.AddObject(temp);
                      }
                      else
                      {
                          res.id = res.id;
                          res.CheckInMemo1 = order.CheckInMemo1;
                          res.CheckInMemo2 = order.CheckInMemo2;
                          res.CheckInMemo3 = order.CheckInMemo3;
                      }
                      db.SaveChanges();
                      result = "0";
                
              }
              catch (Exception e)
              {
                  result = e.ToString();
              }
          }
          return result;
      }
      /// <summary>
      /// read
      /// </summary>
      /// <returns></returns>
      public  CheckInOutMemoModel ReadAll()
      {
          using (HotelDBEntities _db = new HotelDBEntities())
          {
              //Module for View
              List<CheckInOutMemoModel> kf = new List<CheckInOutMemoModel>();
              CheckInOutMemoModel ctgy = new CheckInOutMemoModel();
              var result = from a in _db.CheckInOutMemo
                           select a;
              foreach (var FH in result)
              {
                  ctgy.ID = FH.id;
                  ctgy.CheckInMemo1 = FH.CheckInMemo1;
                  ctgy.CheckInMemo2 = FH.CheckInMemo2;
                  ctgy.CheckInMemo3 = FH.CheckInMemo3;
              }
              return ctgy;
          }
      }

      public telMemoModel readTel()
      {
          using (HotelDBEntities _db = new HotelDBEntities())
          {
              //Module for View
              List<telMemoModel> kf = new List<telMemoModel>();
              telMemoModel ctgy = new telMemoModel();
              var result = from a in _db.sy_cn
                           select a;
              foreach (var FH in result)
              {
                  ctgy.tel = FH.Telphone;
              }
              return ctgy;
          }
      }
  }


    public class CheckInOutMemoModel
    {
        public Int32 ID{get;set;}
        public string CheckInMemo1{get;set;}
        public string CheckInMemo2{get;set;}
        public string CheckInMemo3{get;set;}
          public string CheckOutMemo1{get;set;}
        public string CheckOutMemo2{get;set;}
    }

    public class telMemoModel
    {
        public string tel { get; set; }
    }
}
