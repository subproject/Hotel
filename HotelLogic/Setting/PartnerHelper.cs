using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Setting
{
    /// <summary>
    /// 协议单位的操作
    /// </summary>
    public class PartnerHelper
    {
        //create
        public string addPartner(PartnerViewModel q)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    AgreeCompany temp = new AgreeCompany();
                    temp.AutoID = q.ID;
                    temp.Company = q.Name;
                    temp.ContactMan = q.Contact;
                    temp.Telphone = q.Tel;
                    temp.SaleMan = q.Saler;
                    //是否挂账 是否返佣
                    temp.IsRetComm = q.IsFany;
                    temp.IsCredit = q.IsGuzh;
                    //返佣方式
                    temp.RetType = q.FanyWay;
                    temp.CreditLevel = q.GuazhLimit;
                    temp.CreditMoney = q.GuazhSum;
                    temp.Memo = q.Detail;
                    _db.AgreeCompany.AddObject(temp);
                    _db.SaveChanges();
                    return "0";
                }catch(Exception e){
                    return "1"+e.ToString();
                }
            }
            
        }
        //update
        public string updatePartner(PartnerViewModel q)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = (from a in _db.AgreeCompany
                                where a.AutoID==q.ID
                                select a).SingleOrDefault();
                    if (temp != null)
                    {
                        temp.AutoID = q.ID;
                        temp.Company = q.Name;
                        temp.ContactMan = q.Contact;
                        temp.Telphone = q.Tel;
                        temp.SaleMan = q.Saler;
                        //是否挂账 是否返佣
                        temp.IsRetComm = q.IsFany;
                        temp.IsCredit = q.IsGuzh;
                        //返佣方式
                        temp.RetType = q.FanyWay;
                        temp.CreditLevel = q.GuazhLimit;
                        temp.CreditMoney = q.GuazhSum;
                        temp.Memo = q.Detail;
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
        //delete
        public string delPartner(int ID)
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                try
                {
                    var temp = (from a in _db.AgreeCompany
                                where a.AutoID == ID
                                select a).SingleOrDefault();
                    if (temp != null)
                    {
                        _db.AgreeCompany.DeleteObject(temp);
                        _db.SaveChanges();
                        return "0";
                    }
                    return "1,没有找到要删除的纪录";
                }
                catch (Exception e)
                {
                    return "1" + e.ToString();
                }
            }
        }
        //read
        public List<PartnerViewModel> getPartners()
        {
            using (HotelDBEntities _db = new HotelDBEntities())
            {
                List<PartnerViewModel> result = new List<PartnerViewModel>();
                var query = from a in _db.AgreeCompany
                            select a;
                foreach (var q in query)
                {
                    PartnerViewModel temp = new PartnerViewModel();
                    temp.ID = q.AutoID;
                    temp.Name = q.Company;
                    temp.Contact = q.ContactMan;
                    temp.Tel = q.Telphone;
                    temp.Saler = q.SaleMan;
                    //是否挂账 是否返佣
                    temp.IsFany = q.IsRetComm;
                    temp.IsGuzh = q.IsCredit;
                    //返佣方式
                    temp.FanyWay = q.RetType;
                    temp.GuazhLimit = q.CreditLevel;
                    temp.GuazhSum = q.CreditMoney;
                    temp.Detail = q.Memo;

                    //得到该协议单位的价格信息
                    var queryprice = from a in _db.AgreeCompanyPrice
                                     where a.ComId == q.AutoID
                                     select a;
                    foreach (var qp in queryprice)
                    {
                        PartnerPrice pptemp = new PartnerPrice();
                        pptemp.ReturnFee = qp.ReturnFee;
                        pptemp.BTF = qp.BtFj;
                        pptemp.DJ = qp.FjDj;
                        pptemp.ZDF = qp.ZdFj;
                        pptemp.LCF = qp.Lcf;
                        pptemp.FJJB = qp.FjJb;
                        pptemp.ID = qp.AutoID;
                        pptemp.CompanyID = qp.ComId;
                        temp.PPs.Add(pptemp);
                    }
                    result.Add(temp);
                }
                return result;
            }
        }
    }

    public class PartnerViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Tel { get; set; }
        public bool? IsGuzh { get; set; }
        public bool? IsFany { get; set; }
        public byte? FanyWay { get; set; }
        public decimal? GuazhLimit { get; set; }
        public decimal? GuazhSum { get; set; }
        public string Saler { get; set; }
        public string Detail { get; set; }
        public List<PartnerPrice> PPs { get; set; }
    }
    public class PartnerPrice
    {
        public int? CompanyID { get; set; }
        public string FJJB { get; set; }
        public decimal? ZDF { get; set; }
        public decimal? BTF { get; set; }
        public decimal? DJ { get; set; }
        public decimal? LCF { get; set; }
        public decimal? ReturnFee { get; set; }
        public string Detail { get; set; }
        public int ID { get; set; }
    }
}
