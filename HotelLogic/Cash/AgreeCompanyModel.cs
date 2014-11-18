using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelLogic.Cash
{
    public class AgreeCompanyModel
    {
        public int? id { get; set; }
        public string Company { get; set; }
        public string ContactMan { get; set; }
        public string Telphone { get; set; }
        public Boolean? IsCredit { get; set; }
        public Decimal? CreditMoney { get; set; }
        public Decimal? RecvMoney { get; set; }
        public string SaleMan { get; set; }
        public string Address { get; set; }
        public Decimal? CreditLevel { get; set; }
        public Byte? Status { get; set; }
        public string Memo { get; set; }
        public Boolean? IsRetComm { get; set; }
        public Byte? RetType { get; set; }
        public Int32 AutoID { get; set; }
    }
}
