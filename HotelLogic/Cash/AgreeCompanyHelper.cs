using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Cash
{
    public class AgreeCompanyHelper
    {
        private static AgreeCompanyHelper _instance;
        public static AgreeCompanyHelper Instance
        {
            get { return _instance == null ? _instance = new AgreeCompanyHelper() : _instance; }
        }
        private AgreeCompanyHelper() { }

        //get all company
        public List<AgreeCompanyModel> GetAllCompany()
        {
            List<AgreeCompanyModel> result = new List<AgreeCompanyModel>();
            using (HotelDBEntities db = new HotelDBEntities())
            {
                var temps = from a in db.AgreeCompany
                            select a;
                foreach (var t in temps)
                {
                    AgreeCompanyModel temp = new AgreeCompanyModel();
                    temp.Address = t.Address;
                    temp.AutoID = t.AutoID;
                    temp.Company = t.Company;
                    temp.ContactMan = t.ContactMan;
                    temp.CreditLevel = t.CreditLevel;
                    temp.CreditMoney = t.CreditMoney;
                    temp.id = t.id;
                    temp.IsCredit = t.IsCredit;
                    temp.IsRetComm = t.IsRetComm;
                    temp.Memo = t.Memo;
                    temp.RecvMoney = t.RecvMoney;
                    temp.RetType = t.RetType;
                    temp.SaleMan = t.SaleMan;
                    temp.Status = t.Status;
                    temp.Telphone = t.Telphone;
                    result.Add(temp);
                }
            }
            return result;
        }
    }
}
