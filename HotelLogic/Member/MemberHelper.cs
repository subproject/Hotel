using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

using System.Data;

namespace HotelLogic.Member
{
    /// <summary>
    /// 会员信息
    /// </summary>
    public static class MemberHelper
    {
        private static readonly HotelDBEntities _db = new HotelDBEntities();
        private static object SyncRoot = new object();//同步

        public static string ReadLastMemberNo()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.Member  orderby a.MemberNo descending
                             select a ).SingleOrDefault();
               
                return result.MemberNo;
                
            }
           
        }
        #region carddesign

        public static string delMemberDes(Int32 id)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.MemberCard.FirstOrDefault(a => a.id == id);
                    if (temp != null)
                    {
                        _db.MemberCard.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }

        public static string updateMemberCard(MemberCardModel r)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = (from a in _db.MemberCard
                                where a.id == r.id
                                select a).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.id = r.id;
                        temp.CardType = r.CardType;
                        temp.ScorePercent = r.ScorePercent;
                        temp.RequestScore = r.RequestScore;
                        temp.Discount = r.Discount;
                        temp.CheckOutDelay = r.CheckOutDelay;
                        temp.IsCharge = r.IsCharge;
                        temp.ChargePercent = r.ChargePercent;
                        temp.YePrompt = r.YePrompt;
                        temp.DiscountType = r.DiscountType;
                        temp.IsAutoUpLevel = r.IsAutoUpLevel;
                        temp.IsAutoDownLevel = r.IsAutoDownLevel;
                        temp.LowPoint = r.LowPoint;
                        temp.HighPoint = r.HighPoint;
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1,没有找到要更新的纪录";
                }
                catch (Exception e)
                {
                    return "1" + e.ToString();
                }
            }
        }
      
        /// <summary>
        /// Read all the Member info   
        /// </summary>
        /// <returns></returns>
        public static string AddCardDesign(MemberCardModel r)
        {


            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    MemberCard temp = new MemberCard();
                        
                    temp.CardType = r.CardType;
                    temp.ScorePercent = r.ScorePercent;
                    temp.RequestScore = r.RequestScore;
                    temp.Discount = r.Discount;
                    temp.CheckOutDelay = r.CheckOutDelay;
                    temp.IsCharge = r.IsCharge;
                    temp.ChargePercent = r.ChargePercent;
                    temp.YePrompt = r.YePrompt;
                    temp.DiscountType = r.DiscountType;
                    temp.IsAutoUpLevel = r.IsAutoUpLevel;
                    temp.IsAutoDownLevel = r.IsAutoDownLevel;
                    temp.LowPoint = r.LowPoint;
                    temp.HighPoint = r.HighPoint;
                    
                    _db.MemberCard.AddObject(temp);
                    _db.SaveChanges();
                    return "0";
                }
                catch (Exception e)
                {
                    return "1" + e.ToString();
                }
                
            }
             
        }

        public static string CreateRoomDesign(MemberRoomDesignView _mb,string cardType)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = (from a in _db.MemberCard
                                where a.CardType == cardType
                                select a).SingleOrDefault();
                    if (temp == null)
                        return null;
                    //
                    HotelEntities.MemberDiscountOption Mb = new HotelEntities.MemberDiscountOption();
                    Mb.f_dj = _mb.f_dj;
                    Mb.f_jb = _mb.f_jb;
                    Mb.Lcf = _mb.Lcf;
                    Mb.Memo = _mb.Memo;
                    Mb.z_dj = _mb.z_dj;
                    Mb.ZdFj = _mb.ZdFj;
                    Mb.CardTypeId = temp.id;

                    _db.MemberDiscountOption.AddObject(Mb);
                    _db.SaveChanges();
                   
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return "0";
            }

        }
        public static string CreatePointToGift(PointToGiftView _mb)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {                   
                    HotelEntities.MemberGiftChangeDesign Mb = new HotelEntities.MemberGiftChangeDesign();
                    Mb.liwu = _mb.liwu;
                    Mb.MustPoint = _mb.MustPoint;
                    _db.MemberGiftChangeDesign.AddObject(Mb);
                    _db.SaveChanges();
                   
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return "0";
            }

        }

        public static List<MemberFJLXView> Getfjlx(string CardType)
        {

            List<MemberFJLXView> Mb = new List<MemberFJLXView>();

            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var temp = (from a in _db.zd_jb
                               
                                select a);
                if (temp == null)
                    return null;
                List<string> list=new List<string>();
                foreach (var r in temp)
                {
                  
                    list.Add(r.kfjb);
                }
                var temp2 = (from a in _db.MemberCard
                            where a.CardType == CardType
                            select a).SingleOrDefault();
                if (temp2 == null)
                    return null;
                var result = from a in _db.MemberDiscountOption
                             where a.CardTypeId == temp2.id
                             select a;
                foreach (var r in result)
                {
                    if (list.Contains(r.f_jb))
                    {
                        list.Remove(r.f_jb);
                    }
                }
                foreach (string r in list)
                {
                    MemberFJLXView tempView = new MemberFJLXView();
                    tempView.f_jb = r;
                    Mb.Add(tempView);
                }
            }
            return Mb;
        }
        public static List<MemberRoomDesignView> GetRoomDesign(string CardType)
        {

            List<MemberRoomDesignView> Mb = new List<MemberRoomDesignView>();
            using (HotelDBEntities _db = new HotelDBEntities())
            { 
                var temp = (from a in _db.MemberCard
                               where a.CardType ==CardType
                                select a).SingleOrDefault();
                if (temp == null)
                    return null;
                var result = from a in _db.MemberDiscountOption
                             where a.CardTypeId == temp.id
                             select a;
                foreach (var r in result)
                {
                    MemberRoomDesignView tempView = new MemberRoomDesignView();
                    tempView.id = r.id;
                    tempView.CardTypeId = r.CardTypeId;
                    tempView.f_jb = r.f_jb;
                    tempView.f_dj = r.f_dj;
                    tempView.z_dj = r.z_dj;
                    tempView.Memo = r.Memo;
                    tempView.ZdFj = r.ZdFj;
                    tempView.Lcf = r.Lcf;
                    Mb.Add(tempView);
                }

            }
            return Mb;
        }
        public static List<PointToGiftView> GetPointToGift()
        {

            List<PointToGiftView> Mb = new List<PointToGiftView>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.MemberGiftChangeDesign
                             select a;
                foreach (var r in result)
                {
                    PointToGiftView temp = new PointToGiftView();
                    temp.id = r.id;
                    temp.liwu = r.liwu;
                    temp.MustPoint = r.MustPoint;
                    
                    

                    Mb.Add(temp);
                }

            }
            return Mb;
        }

        /// <summary>
        /// Read all the Member info 
        /// </summary>
        /// <returns></returns>
        public static List<MemberCardModel> ReadCardDesignAll()
        {
            //Module for View
            List<MemberCardModel> Mb = new List<MemberCardModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.MemberCard
                             select a;
                foreach (var r in result)
                {
                    MemberCardModel temp = new MemberCardModel();
                    temp.id = r.id;
                    temp.CardType = r.CardType;
                    temp.ScorePercent = r.ScorePercent;
                    temp.RequestScore = r.RequestScore;
                    temp.Discount = r.Discount;
                    temp.CheckOutDelay = r.CheckOutDelay;
                    temp.IsCharge = r.IsCharge;
                    temp.IsCharge2 = r.IsCharge == true ? "是" : "否";
                    temp.ChargePercent = r.ChargePercent;
                    temp.YePrompt = r.YePrompt;
                    temp.DiscountType = r.DiscountType;
                    temp.IsAutoUpLevel = r.IsAutoUpLevel;
                    temp.IsAutoUpLevel2 = r.IsAutoUpLevel==true?"是":"否";
                    temp.IsAutoDownLevel = r.IsAutoDownLevel;
                    temp.IsAutoDownLevel2 = r.IsAutoDownLevel == true ? "是" : "否";
                    temp.LowPoint = r.LowPoint;
                    temp.HighPoint = r.HighPoint;
                    

                    Mb.Add(temp);
                }

            }
            return Mb;
        }
        #endregion


        public static string SaveAllowDesign(MemberAllowModel Mb)
        {

            using (HotelDBEntities _db = new HotelDBEntities())
            {
                    try
                    {                    
                        var r = (from a in _db.MemberAllowDesign
                                     select a).SingleOrDefault();
                        if (r != null)
                        {
                           // r.id = Mb.id;
                            r.allowMultiUseCard = Mb.allowMultiUseCard;
                            r.allowNoCardConsume = Mb.allowNoCardConsume;
                            _db.SaveChanges();
                        }
                        else
                        {
                            HotelEntities.MemberAllowDesign mad = new HotelEntities.MemberAllowDesign();
                            mad.allowMultiUseCard = Mb.allowMultiUseCard;
                            mad.allowNoCardConsume = Mb.allowNoCardConsume;
                            _db.MemberAllowDesign.AddObject(mad);
                            _db.SaveChanges();
                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                }
            
            return "0";
        }

        public static MemberAllowModel GetAllowDesign()
        {
          
            MemberAllowModel Mb = new MemberAllowModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {

                lock (SyncRoot)
                {
                    var r = (from a in _db.MemberAllowDesign
                                 select a).SingleOrDefault();
                   if(r!=null)
                    {

                        Mb.id = r.id;
                        Mb.allowMultiUseCard = r.allowMultiUseCard;
                        Mb.allowNoCardConsume = r.allowNoCardConsume;
                         
                    }

                }
            }
            return Mb;
        }

        /// <summary>
        /// Read all the Member info   
        /// </summary>
        /// <returns></returns>
        public static List<MemberViewModel> ReadAll()
        {
            //Module for View
            List<MemberViewModel> Mb = new List<MemberViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
               
                lock (SyncRoot)
                {
                    var result = from a in _db.Member
                                 select a;
                    foreach (var r in result)
                    {
                        MemberViewModel temp = new MemberViewModel();
                        temp.Address = r.Address == null ? "" : r.Address;
                        temp.BirthDay = r.BirthDay;
                        temp.CardType = r.CardType;
                        temp.Charge = r.Charge;
                        temp.Discount = r.Discount;
                        temp.HomeTelphone = r.HomeTelphone;
                        temp.ID = r.id;
                        temp.IdCard = r.IdCard;
                        temp.IsValidate = r.IsValidate;
                        temp.MemberCardNo = r.MemberCardNo;
                        temp.MemberName = r.MemberName;
                        if (!string.IsNullOrEmpty(r.MemberNo))
                        {
                            temp.MemberNo = r.MemberNo.PadLeft(10, '0');
                        }
                        temp.Mobile = r.Mobile;
                        temp.Password = r.Password;
                        temp.RegTime = r.RegTime;
                        temp.RegTimeStr = r.RegTime.ToString();
                        temp.RestCharge = r.RestCharge;
                        temp.RestScore = r.RestScore;
                        temp.Score = r.Score;
                        temp.ScorePercent = r.ScorePercent;

                        temp.Sex = r.Sex.Trim() == "f" ? "女" : "男";
                        temp.Status = r.Status;
                        temp.Times = r.Times;
                        temp.UserName = r.UserName;
                        temp.UseScore = r.UseScore;
                        temp.Validate = r.Validate;

                        Mb.Add(temp);
                    }

                }
            }
            return Mb;
        }

        public static List<MemberViewModel> ReadPartMember(int page, int rows, MemberFilter filter)
        {
             
            List<MemberViewModel> Mb = new List<MemberViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {

                var result = (from a in _db.Member                              
                              select a);
                if (!string.IsNullOrEmpty(filter.name))
                {
                    result = from a in result
                             where a.MemberName.Contains(filter.name)
                            select a;
                }
               
                if (!string.IsNullOrEmpty(filter.cardNo))
                {
                    result = from a in result
                             where a.MemberCardNo.Contains(filter.cardNo)
                            select a;
                }
                if (!string.IsNullOrEmpty(filter.cardType))
                {
                    result = from a in result
                             where a.CardType.Contains(filter.cardType)
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.identifycard))
                {
                    result = from a in result
                             where a.IdCard.Contains(filter.identifycard)
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.phone))
                {
                    result = from a in result
                             where a.HomeTelphone.Contains(filter.phone)
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.mobile))
                {
                    result = from a in result
                             where a.Mobile.Contains(filter.mobile)
                             select a;
                }
                if (filter.status!=null)
                {
                    result = from a in result
                             where a.Status==filter.status
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.address))
                {
                    result = from a in result
                             where a.Address.Equals(filter.address)
                             select a;
                }
                if (!string.IsNullOrEmpty(filter.birthday))
                {
                    result = from a in result
                             where a.BirthDay.Equals(filter.birthday)
                             select a;
                }
                if (filter.limitday.Year > 2001)
                {
                    result = from a in result
                             where filter.limitday.CompareTo(a.Validate) > 0
                             select a;
                }
                if (filter.regditdate.Year > 2001)
                {
                    result = from a in result
                             where a.RegTime.Value.Year.Equals(filter.regditdate.Year)
                               && a.RegTime.Value.Month.Equals(filter.regditdate.Month)
                                && a.RegTime.Value.Day.Equals(filter.regditdate.Day) 
                             select a;
                } 
       
                
                
                 result = (from a in result
                              orderby a.MemberNo descending
                              select a).Skip((page - 1) * rows).Take(rows);
               
                
                    foreach (var r in result)
                    {
                        MemberViewModel temp = new MemberViewModel();
                        temp.Address = r.Address;
                        temp.BirthDay = r.BirthDay;
                        temp.CardType = r.CardType;
                        temp.Charge = r.Charge;
                        temp.Discount = r.Discount;
                        temp.HomeTelphone = r.HomeTelphone;
                        temp.ID = r.id;
                        temp.IdCard = r.IdCard;
                        temp.IsValidate = r.IsValidate;
                        temp.MemberCardNo = r.MemberCardNo;
                        temp.MemberName = r.MemberName;
                        temp.MemberNo = r.MemberNo;
                        temp.Mobile = r.Mobile;
                        temp.Password = r.Password;
                        temp.RegTime = r.RegTime;
                        temp.RestCharge = r.RestCharge;
                        temp.RestScore = r.RestScore;
                        temp.Score = r.Score;
                        temp.ScorePercent = r.ScorePercent;
                        temp.Sex = r.Sex=="m"?"男":"女";
                        temp.Status = r.Status;
                        temp.Times = r.Times;
                        temp.UserName = r.UserName;
                        temp.UseScore = r.UseScore;
                        temp.Validate = r.Validate;

                        Mb.Add(temp);
                    }
           
            }
            return Mb;
        }
        /// <summary>
        /// Read partly Member info
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static List<MemberViewModel> ReadPart(int page, int rows)
        {
            //Module for View
            List<MemberViewModel> Mb = new List<MemberViewModel>();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.Member
                              orderby a.MemberNo descending
                              select a).Skip((page - 1) * rows).Take(rows);
                foreach (var r in result)
                {
                    MemberViewModel temp = new MemberViewModel();
                    temp.Address = r.Address;
                    temp.BirthDay = r.BirthDay;
                    temp.CardType = r.CardType;
                    temp.Charge = r.Charge;
                    temp.Discount = r.Discount;
                    temp.HomeTelphone = r.HomeTelphone;
                    temp.ID = r.id;
                    temp.IdCard = r.IdCard;
                    temp.IsValidate = r.IsValidate;
                    temp.MemberCardNo = r.MemberCardNo;
                    temp.MemberName = r.MemberName;
                    temp.MemberNo = r.MemberNo;
                    temp.Mobile = r.Mobile;
                    temp.Password = r.Password;
                    temp.RegTime = r.RegTime;
                    temp.RestCharge = r.RestCharge;
                    temp.RestScore = r.RestScore;
                    temp.Score = r.Score;
                    temp.ScorePercent = r.ScorePercent;
                    temp.Sex = r.Sex;
                    temp.Status = r.Status;
                    temp.Times = r.Times;
                    temp.UserName = r.UserName;
                    temp.UseScore = r.UseScore;
                    temp.Validate = r.Validate;

                    Mb.Add(temp);
                }
            }
            return Mb;
        }
        
        public static int GetCardType()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = _db.Member.FirstOrDefault(a => a.MemberNo == _db.Member.Max(y=>y.MemberNo));
               
                  return Convert.ToInt32(result.MemberNo) + 1;

                //MemberCardModel
                
            }
        }
        public static int getLastMemberNo()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = _db.Member.FirstOrDefault(a => a.id == _db.Member.Max(y=>y.id));
               
                  return Convert.ToInt32(result.id) + 1;
                
                //Mb = (HotelEntities.Member)new EntityKey("HotelEntities.Member", "id", rowID);
                
            }
        }
        public static string getAddress(string cardid)
        {
            //Module for View
            MemberViewModel Mb = new MemberViewModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {

                var r = (from a in _db.City where a.Code == cardid select a).SingleOrDefault();
               
                if (r != null)
                {
                    return r.City1;
                }                   
 
            }
            return "";
        }

        /// <summary>
        /// get one  Member's info from no   
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static MemberViewModel gUserInfoByNo(string cardno)
        {

            //Module for View
            MemberViewModel Mb = new MemberViewModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                string no =Convert.ToInt32(cardno).ToString();
               
                var r = (from a in _db.Member where a.MemberCardNo ==no  select a).SingleOrDefault();
                //Mb = (HotelEntities.Member)new EntityKey("HotelEntities.Member", "id", rowID);
               if(r!=null)
                {
                    MemberViewModel temp = new MemberViewModel();
                    temp.Address = r.Address;
                    temp.BirthDay = r.BirthDay;
                    temp.CardType = r.CardType;
                    temp.Charge = r.Charge;
                    temp.Discount = r.Discount;
                    temp.HomeTelphone = r.HomeTelphone;
                    temp.ID = r.id;
                    temp.IdCard = r.IdCard;
                    temp.IsValidate = r.IsValidate;
                    temp.MemberCardNo = r.MemberCardNo;
                    temp.MemberName = r.MemberName;
                    temp.MemberNo = r.MemberNo;
                    temp.Mobile = r.Mobile;
                    temp.Password = r.Password;
                    temp.RegTime = r.RegTime;
                    temp.RestCharge = r.RestCharge;
                    temp.RestScore = r.RestScore;
                    temp.Score = r.Score;
                    temp.ScorePercent = r.ScorePercent;
                    temp.Sex = r.Sex;
                    temp.Status = r.Status;
                    temp.Times = r.Times;
                    temp.UserName = r.UserName;
                    temp.UseScore = r.UseScore;
                    temp.Validate = r.Validate;

                    Mb = temp;
                }
            }
            return Mb;
        }

        /// <summary>
        /// get one  Member's info from RowID  
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static  MemberViewModel gUserInfo(Int32 rowID)
        {
            //Module for View
            MemberViewModel Mb = new MemberViewModel();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.Member where a.id == rowID select a);
                //Mb = (HotelEntities.Member)new EntityKey("HotelEntities.Member", "id", rowID);
                foreach (var r in result)
                {
                    MemberViewModel temp = new MemberViewModel();
                    temp.Address = r.Address;
                    temp.BirthDay = r.BirthDay;
                    temp.CardType = r.CardType;
                    temp.Charge = r.Charge;
                    temp.Discount = r.Discount;
                    temp.HomeTelphone = r.HomeTelphone;
                    temp.ID = r.id;
                    temp.IdCard = r.IdCard;
                    temp.IsValidate = r.IsValidate;
                    temp.MemberCardNo = r.MemberCardNo;
                    temp.MemberName = r.MemberName;
                    temp.MemberNo = r.MemberNo;
                    temp.Mobile = r.Mobile;
                    temp.Password = r.Password;
                    temp.RegTime = r.RegTime;
                    temp.RestCharge = r.RestCharge;
                    temp.RestScore = r.RestScore;
                    temp.Score = r.Score;
                    temp.ScorePercent = r.ScorePercent;
                    temp.Sex = r.Sex;
                    temp.Status = r.Status;
                    temp.Times = r.Times;
                    temp.UserName = r.UserName;
                    temp.UseScore = r.UseScore;
                    temp.Validate = r.Validate;

                    Mb = temp;
                }
            }
            return Mb;
        }
        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        public static string Create(MemberViewModel _mb)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    //查找储值比例
                   var temp = (from a in _db.MemberCard
                               where a.CardType == _mb.CardType
                                select a).SingleOrDefault();
                   if (temp != null)
                   {
                       _mb.ScorePercent =Convert.ToDouble(temp.ChargePercent);
                   }
                    HotelEntities.Member Mb = new HotelEntities.Member();

                    Mb.Address = _mb.Address;
                    Mb.BirthDay = _mb.BirthDay;
                    Mb.CardType = _mb.CardType;
                     Mb.Charge = 0;
                     Mb.Discount = _mb.Discount;
                    Mb.HomeTelphone = _mb.HomeTelphone;
                    //Mb.ID = _mb.id;
                    Mb.IdCard = _mb.IdCard;
                    Mb.IsValidate = _mb.IsValidate;
                    Mb.MemberCardNo = _mb.MemberCardNo;
                    Mb.MemberName = _mb.MemberName;
                    Mb.MemberNo = _mb.MemberNo;
                    Mb.Mobile = _mb.Mobile;
                    Mb.Password = _mb.Password;
                    Mb.RegTime = _mb.RegTime;
                    Mb.RestCharge = _mb.Score;// _mb.RestScore;
                    Mb.RestScore = _mb.Score;// _mb.RestScore;
                    Mb.Score = _mb.Score;
                    Mb.Times = 0;
                    Mb.ScorePercent = _mb.ScorePercent;
                    Mb.Sex = _mb.Sex;
                    Mb.Status = _mb.Status;
                     
                    Mb.UserName = _mb.UserName;
                    Mb.UseScore = _mb.UseScore;
                    Mb.Validate = _mb.Validate;

                    _db.Member.AddObject(Mb);
                    _db.SaveChanges();
                    //查找max id
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
                return "0";
            }
        }

        public static string UpdateRoomDesign(MemberRoomDesignView _mb)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.MemberDiscountOption.FirstOrDefault(a => a.id == _mb.id);
                    if (temp != null)
                    {
                        temp.f_dj = _mb.f_dj;
                        temp.f_jb = _mb.f_jb;
                        temp.ZdFj = _mb.ZdFj;
                        temp.Lcf = _mb.Lcf;
                        temp.Memo = _mb.Memo;
                        temp.z_dj = _mb.z_dj;
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
        public static string UpdatePointToGiftView(PointToGiftView _mb)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.MemberGiftChangeDesign.FirstOrDefault(a => a.id == _mb.id);
                    if (temp != null)
                    {
                        temp.liwu = _mb.liwu;
                        temp.MustPoint = _mb.MustPoint;                      
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="mb"></param>
        /// <returns></returns>
        public static string Update(MemberViewModel _mb)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.Member.FirstOrDefault(a => a.id == _mb.ID);
                    if (temp != null)
                    {
                        temp.Address = _mb.Address;
                        temp.BirthDay = _mb.BirthDay;
                        temp.CardType = _mb.CardType;
                        temp.Charge = _mb.Charge;
                        temp.Discount = _mb.Discount;
                        temp.HomeTelphone = _mb.HomeTelphone;
                        temp.IdCard = _mb.IdCard;
                        temp.IsValidate = _mb.IsValidate;
                        temp.MemberCardNo = _mb.MemberCardNo;
                        temp.MemberName = _mb.MemberName;
                        temp.MemberNo = _mb.MemberNo;
                        temp.Mobile = _mb.Mobile;
                        temp.Password = _mb.Password;
                        temp.RegTime = _mb.RegTime;
                        temp.RestCharge = _mb.RestCharge;
                        temp.RestScore = _mb.RestScore;
                        temp.Score = _mb.Score;
                        temp.ScorePercent = _mb.ScorePercent;
                        temp.Sex = _mb.Sex;
                        temp.Status = _mb.Status;
                        temp.Times = _mb.Times;
                        temp.UserName = _mb.UserName;
                        temp.UseScore = _mb.UseScore;
                        temp.Validate = _mb.Validate;
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }


        public static string DeleteRoomDesign(Int32 id)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.MemberDiscountOption.FirstOrDefault(a => a.id == id);
                    if (temp != null)
                    {
                        _db.MemberDiscountOption.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
            public static string DeletePointToGift(Int32 id)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.MemberGiftChangeDesign.FirstOrDefault(a => a.id == id);
                    if (temp != null)
                    {
                        _db.MemberGiftChangeDesign.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Delete(Int32 id)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = _db.Member.FirstOrDefault(a => a.id == id);
                    if (temp != null)
                    {
                        _db.Member.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1";
                }
                catch (Exception e)
                {
                    return e.ToString();
                }
            }
        }

        public static List<MemberChangeMoney> ReadMemberChangeMoneyAll(DateTime starttime, DateTime endtime)
        {
            List<MemberChangeMoney> Mb = new List<MemberChangeMoney>();
          try
            {

               using(HotelDBEntities _db=new HotelDBEntities())
            {
                    var result = (from a in _db.MemberCharge
                                  where System.Data.Objects.EntityFunctions.DiffMinutes(starttime,a.CurTime) >= 0
                                  && System.Data.Objects.EntityFunctions.DiffMinutes(endtime, a.CurTime) <= 0
                                  select a);
                    foreach (var r in result)
                    {
                        MemberChangeMoney temp = new MemberChangeMoney();
                        temp.id = r.id;
                        temp.name = r.MemberName;
                        temp.memberno = r.CardNo;
                        temp.paytype = r.FkFs;
                        temp.changemoney = r.ChargeMoney.ToString();
                        temp.money = r.ActualCharge.ToString();
                        temp.time = r.CurTime.ToString();
                        temp.bz = r.Memo;
                        Mb.Add(temp);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return Mb;

        }
        public static int GetMemberChangeMoneyCount(DateTime starttime, DateTime endtime,
           ref float changemoney, ref float money)
        {
            List<MemberChangeMoney> Mb = new List<MemberChangeMoney>();
            try
            {
               using(HotelDBEntities _db=new HotelDBEntities())
               {
                    var result = (from a in _db.MemberCharge
                                  //where starttime.CompareTo(a.CurTime) <= 0 && endtime.CompareTo(a.CurTime) >= 0
                                  select a);
                    foreach (var r in result)
                    {
                        changemoney +=Convert.ToSingle( r.ChargeMoney);
                        money += Convert.ToSingle(r.ActualCharge);
                      
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 0;
        }

        public static int GetMemberCount()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = from a in _db.Member
                             select a;

                return result.Count();
            }
            
        }
        public static MemberPointDesign ReadPointDesign()
        {

            MemberPointDesign result = new MemberPointDesign();
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var r = (from a in _db.MemberPointDesign
                         select a).SingleOrDefault();
                if (r != null)
                {
                    result.ByConsumePoint = r.ByConsumePoint;
                    result.ByLiveDays = r.ByLiveDays;
                    result.PointToMoney = r.PointToMoney;
                    result.IsAlertPointToRoom = r.IsAlertPointToRoom;
                    result.IsConsumePoint = r.IsConsumePoint;
                    result.IsLiveDays = r.IsLiveDays;
                    result.IsPointToMoney = r.IsPointToMoney;
                }
            }
                return result;
        }
        public static bool SavePointDesign(MemberPointDesignView r)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                var result = (from a in _db.MemberPointDesign
                             select a).SingleOrDefault();
                if (result != null)//更新
                {
                    result.ByConsumePoint = r.ByConsumePoint;
                    result.ByLiveDays = r.ByLiveDays;
                    result.PointToMoney = r.PointToMoney;
                    result.IsAlertPointToRoom = r.IsAlertPointToRoom;
                    result.IsConsumePoint = r.IsConsumePoint;
                    result.IsLiveDays = r.IsLiveDays;
                    result.IsPointToMoney = r.IsPointToMoney;
                    _db.SaveChanges();
                }
                else//添加
                {
                    HotelEntities.MemberPointDesign Mb = new HotelEntities.MemberPointDesign();

                    Mb.ByConsumePoint = r.ByConsumePoint;
                    Mb.ByLiveDays = r.ByLiveDays;
                    Mb.PointToMoney = r.PointToMoney;
                    Mb.IsAlertPointToRoom = r.IsAlertPointToRoom;
                    Mb.IsConsumePoint = r.IsConsumePoint;
                    Mb.IsLiveDays = r.IsLiveDays;
                    Mb.IsPointToMoney = r.IsPointToMoney;

                    _db.MemberPointDesign.AddObject(Mb);
                    _db.SaveChanges();
                }

               
                
                return true;
            }
            
        }
        
    }

   
     
    public class MemberViewModel
    {

        public Int32 ID { get; set; }
        //列标识
        public string MemberNo { get; set; }
        //用户ID
        public string   MemberName { get; set; }
        //用户姓名
        public string MemberCardNo { get; set; }
        //用户卡号
        public string   CardType { get; set; }
        //用户卡类型
        public string IdCard { get; set; }
        //身份证
        public string   Sex { get; set; }
        //性别
        public string   BirthDay { get; set; }
        //生日 （出生年月）
        public string   HomeTelphone { get; set; }
        //家庭电话
        public string   Mobile { get; set; }
        //移动电话
        public string   Address { get; set; }
        //通信地址
        public Byte? IsValidate { get; set; }
        //是否有限制  0 无限制  1 有限制
        public string Validate { get; set; }
        //有效期
        public string   Password { get; set; }
        //用户密码
        public Byte?  Status { get; set; }
        //用户状态
        public DateTime? RegTime { get; set; }
        public string RegTimeStr { get; set; }
        //注册时间
        public decimal? Charge { get; set; }
        //用户卡充值金额
        public decimal? RestCharge { get; set; }
        //用户剩余金额
        public int? Score { get; set; }
        //用户积分
        public int? Times { get; set; }
        //入住次数
        public double? ScorePercent { get; set; }
        //积分率
        public int? UseScore { get; set; }
        //用户已使用积分
        public int? RestScore { get; set; }
        //用户剩余积分
        public double? Discount { get; set; }
        //折扣
        public string  UserName { get; set; }
        // 注册该用户的操作员
    }
    /// <summary>
    /// 账单查询封装类
    /// </summary>
    public class MemberFilter
    {
        public string name { get; set; }
        public string cardNo { get; set; }
        public string cardType{get;set;}
        public string identifycard{get;set;}
        public string birthday { get; set; }
        public DateTime limitday { get; set; }
        public string phone{get;set;}
        public string mobile{get;set;}
        public byte? status{get;set;}
        public string address{get;set;}
        public DateTime regditdate { get; set; }
    }


    public class MemberViewModelTemp
    {

        public MemberViewModelTemp(MemberViewModel model)
        {
            ID = model.ID;
            MemberNo = model.MemberNo;
            MemberName = model.MemberName;
            MemberCardNo = model.MemberCardNo;
            CardType = model.CardType;
            IdCard = model.IdCard;
            Sex = model.Sex;
            BirthDay = model.BirthDay;
            HomeTelphone = model.HomeTelphone;
            Mobile = model.Mobile;
            Address = model.Address;
            IsValidate = model.IsValidate;
            Validate = model.Validate;
            Password = model.Password;
            if(model.Status==0)
            {
                  Status = "注销";
            }
            else if (model.Status == 1)
            {
                Status = "正常";
            }
            else if (model.Status == 0)
            {
                Status = "挂失";
            }
          
            RegTime = model.RegTime;
            Charge = model.Charge;
            RestCharge = model.RestCharge;
            Score = model.Score;
            Times = model.Times;
            ScorePercent = model.ScorePercent;
            UseScore = model.UseScore;
            RestScore = model.RestScore;
            Discount = model.Discount;
            UserName = model.UserName;
        }
        public Int32 ID { get; set; }
        //列标识
        public string MemberNo { get; set; }
        //用户ID
        public string MemberName { get; set; }
        //用户姓名
        public string MemberCardNo { get; set; }
        //用户卡号
        public string CardType { get; set; }
        //用户卡类型
        public string IdCard { get; set; }
        //身份证
        public string Sex { get; set; }
        //性别
        public string BirthDay { get; set; }
        //生日 （出生年月）
        public string HomeTelphone { get; set; }
        //家庭电话
        public string Mobile { get; set; }
        //移动电话
        public string Address { get; set; }
        //通信地址
        public Byte? IsValidate { get; set; }
        //是否有限制  0 无限制  1 有限制
        public string Validate { get; set; }
        //有效期
        public string Password { get; set; }
        //用户密码
        public string Status { get; set; }
        //用户状态
        public DateTime? RegTime { get; set; }
        //注册时间
        public decimal? Charge { get; set; }
        //用户卡充值金额
        public decimal? RestCharge { get; set; }
        //用户剩余金额
        public int? Score { get; set; }
        //用户积分
        public int? Times { get; set; }
        //入住次数
        public double? ScorePercent { get; set; }
        //积分率
        public int? UseScore { get; set; }
        //用户已使用积分
        public int? RestScore { get; set; }
        //用户剩余积分
        public double? Discount { get; set; }
        //折扣
        public string UserName { get; set; }
        // 注册该用户的操作员
    }


    public class MemberCardModel
    {

        public Int32 id { get; set; }
        //列标识
        public string CardType { get; set; }
        // 
        public double? ScorePercent { get; set; }
        // 
        public double? RequestScore { get; set; }
        // 
        public double? Discount { get; set; }
        // 
        public int? CheckOutDelay { get; set; }
        // 
        public bool? IsCharge { get; set; }
        public String IsCharge2 { get; set; }  
        public decimal? ChargePercent { get; set; }
        // 
        public decimal?  YePrompt { get; set; }
        // 
        public string DiscountType { get; set; }
        // 
        public bool? IsAutoUpLevel { get; set; }
        public string IsAutoUpLevel2 { get; set; }
       
        public bool?  IsAutoDownLevel { get; set; }
        public string IsAutoDownLevel2 { get; set; } 
        public int? LowPoint { get; set; }
        // 
        public int? HighPoint { get; set; }
        
    }
       public class MemberChangeMoney
    {

        public Int32 id { get; set; }
        //列标识
        public string name { get; set; }
        public string memberno { get; set; }
        
        // 
        public string changemoney { get; set; }
        // 
        public string money { get; set; }
        // 
        public string paytype { get; set; }
        // 
        public string time { get; set; }

        public string bz { get; set; }
    }

       public class MemberPointDesignView
       {
           public int id { get; set; }
           public int ByConsumePoint { get; set; }
           public int ByLiveDays { get; set; }
           public int PointToMoney { get; set; }
           public bool? IsAlertPointToRoom { get; set; }
           public bool? IsConsumePoint { get; set; }
           public bool? IsLiveDays { get; set; }
           public bool? IsPointToMoney { get; set; }
            
           
       }

       public class PointToGiftView
       {
           public int id { get; set; }
           public string liwu { get; set; }
           public Int64? MustPoint { get; set; }

       }

       public class MemberRoomDesignView
       {
           public int id { get; set; }
           public int? CardTypeId { get; set; }
           public string f_jb { get; set; }
           public decimal? f_dj { get; set; }
           public decimal? z_dj { get; set; }
           public string Memo { get; set; }
           public decimal? ZdFj { get; set; }
           public decimal? Lcf { get; set; }
           
       }
       public class MemberFJLXView
       {
           public string f_jb { get; set; }
       }
       public class MemberAllowModel
       {
           public int id { get; set; }
           public bool? allowMultiUseCard { get; set; }
           public bool? allowNoCardConsume { get; set; }
       }
    
}
