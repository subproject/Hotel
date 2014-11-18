using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Member
{
    public class MemberChargeHelper
    {
        //create
        public string CreateCharge(MemberChargeViewModel data)
        {
            string result = string.Empty;
            MemberCharge temp = new MemberCharge();
            temp.id = data.ID;
            temp.ActualCharge = data.ActualCharge;
            temp.CardNo = data.CardNo;
            temp.ChargeMoney = data.ChargeMoney;
            temp.CurTime = data.CurTime;
            temp.FkFs = data.FkFs;
            temp.Memo = data.Memo;
            temp.s_cz = data.s_cz;
            temp.s_gzr = data.s_gzr;
            temp.MemberName = data.MemberName;
            try
            {
                using (HotelDBEntities _db = new HotelDBEntities())
                {
                    _db.MemberCharge.AddObject(temp);
                    _db.SaveChanges();
                    //修改会员余额，总充值金额
                    var result2 = _db.Member.FirstOrDefault(a => a.MemberCardNo== data.CardNo);
                    result2.RestCharge = result2.RestCharge + data.ChargeMoney;
                  //  result2.Score = result2.Score + Convert.ToInt32(data.ChargeMoney);
                    result2.Charge = result2.Charge + data.ChargeMoney;
                  //  result2.RestScore = result2.RestScore + Convert.ToInt32(data.ChargeMoney);
                    // result2.Times = Convert.ToInt32(result2.Times) + 1;
                    //积分
                    //result2.UseScore = Convert.ToInt32(result2.UseScore) + Convert.ToInt32(data.ChargeMoney);
                    _db.SaveChanges();
                    
                    result = "0";
                }
                
            }
            catch (Exception e)
            {
                result = "1";
            }
            return result;   
        }
        public List<MemberChargeViewModel> ReadAllById(string memberno)
        {
            //Module for View
            using (var _db = new HotelDBEntities())
            {
                List<MemberChargeViewModel> Mb = new List<MemberChargeViewModel>();
                var result = from a in _db.MemberCharge
                             where a.CardNo == memberno
                             select a;
                foreach (var data in result)
                {
                    MemberChargeViewModel temp = new MemberChargeViewModel();
                    temp.ID = data.id;
                    temp.ActualCharge = data.ActualCharge;
                    temp.CardNo = data.CardNo;
                    temp.ChargeMoney = data.ChargeMoney;
                    temp.CurTime = data.CurTime;
                    temp.FkFs = data.FkFs;  
                    temp.Memo = data.Memo;
                    temp.s_cz = data.s_cz;
                    temp.s_gzr = data.s_gzr;
                    Mb.Add(temp);
                }
                return Mb;
            }
        }
        
        public List<MemberChargeViewModel> ReadAll()
        {
            //Module for View
            using (var _db = new HotelDBEntities())
            {
                List<MemberChargeViewModel> Mb = new List<MemberChargeViewModel>();
                var result = from a in _db.MemberCharge
                             select a;
                foreach (var data in result)
                {
                    MemberChargeViewModel temp = new MemberChargeViewModel();
                    temp.ID = data.id;
                    temp.ActualCharge = data.ActualCharge;
                    temp.CardNo = data.CardNo;
                    temp.ChargeMoney = data.ChargeMoney;
                    temp.CurTime = data.CurTime;
                    temp.FkFs = data.FkFs;  
                    temp.Memo = data.Memo;
                    temp.s_cz = data.s_cz;
                    temp.s_gzr = data.s_gzr;
                    Mb.Add(temp);
                }
                return Mb;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<MemberChargeViewModel> ReadPart(int page, int rows)
        {
            //Module for View
            using (var _db = new HotelDBEntities())
            {
                List<MemberChargeViewModel> Mb = new List<MemberChargeViewModel>();
                var result = (from a in _db.MemberCharge
                              orderby a.id descending
                              select a).Skip((page - 1) * rows).Take(rows);
                foreach (var data in result)
                {
                    MemberChargeViewModel temp = new MemberChargeViewModel();
                    temp.ID = data.id;
                    temp.ActualCharge = data.ActualCharge;
                    temp.CardNo = data.CardNo;
                    temp.ChargeMoney = data.ChargeMoney;
                    temp.CurTime = data.CurTime;
                    temp.FkFs = data.FkFs;
                    temp.Memo = data.Memo;
                    temp.s_cz = data.s_cz;
                    temp.s_gzr = data.s_gzr;
                    Mb.Add(temp);
                }
                return Mb;
            }
        }

        /// <summary>
        /// Read by cardNO
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<MemberChargeViewModel> ReadPartByID(int page, int rows, string CardNo)
        {
            //Module for View
            using (var _db = new HotelDBEntities())
            {
                List<MemberChargeViewModel> Mb = new List<MemberChargeViewModel>();
                var result = (from a in _db.MemberCharge
                              orderby a.CurTime descending
                              where a.CardNo == CardNo.Trim()
                              select a).Skip((page - 1) * rows).Take(rows);
                foreach (var data in result)
                {
                    MemberChargeViewModel temp = new MemberChargeViewModel();
                    temp.ID = data.id;
                    temp.ActualCharge = data.ActualCharge;
                    temp.CardNo = data.CardNo;
                    temp.ChargeMoney = data.ChargeMoney;
                    temp.CurTime = data.CurTime;
                    temp.FkFs = data.FkFs;
                    temp.Memo = data.Memo;
                    temp.s_cz = data.s_cz;
                    temp.s_gzr = data.s_gzr;
                    temp.MemberName = data.MemberName;
                    Mb.Add(temp);
                }
                return Mb;
            }
        }
    }

    public class MemberChargeViewModel
    {
        public Int32 ID{get;set;}
        public string CardNo{get;set;}
        public DateTime? CurTime{get;set;}
        public DateTime? s_gzr{get;set;}
        public Decimal? ChargeMoney{get;set;}
        public Decimal? ActualCharge{get;set;}
        public string FkFs{get;set;}
        public string s_cz{get;set;}
        public string Memo{get;set;}
        public string MemberName { get; set; }
    }
}
