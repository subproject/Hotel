using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelEntities;

namespace HotelLogic.Member
{
    public class IDCardHelper
    {
        private readonly string CardNo;
        private const string Dict = "10X98765432";//{"1","0","X","9","8","7","6","5","4","3","2"};             

        public IDCardHelper(string No)
        {
            this.CardNo = No.Trim();
        }

        public CheckedResult CheckIdCard()
        {
            CheckedResult result = new CheckedResult();
            result.validate=false;
            string ID = this.CardNo;
            int[] IDNumber = new int[20];
            int[] weight = new int[20];
            int sum = 0;
            if (ID.Length != 18)
            {
                result.ErrorMessage = "输入的身份证位数有误,请检查";
                return result;
            }

            int[] item = new int[20];
            for (int i = 0; i < 0x11; i++)
            {
                IDNumber[i] = int.Parse(ID[i].ToString());
                weight[i] = ((1 << (17 - i))) % 11;
                sum += IDNumber[i] * weight[i];
                //item[i] = IDNumber[i] * weight[i];
            }
            int temp = sum % 11;

            if (Dict[temp].Equals(ID[17]))
            {
                if (IDNumber[16] % 2 == 0)
                {
                    result.sex = "female";
                }
                else
                {
                    result.sex = "male";
                }
                result.ErrorMessage = "===";
                result.validate = true;

                try
                {
                    using (HotelDBEntities context = new HotelDBEntities())
                    {
                        int IDBM = int.Parse(ID[0].ToString() + ID[1].ToString() + ID[2].ToString() + ID[3].ToString() + ID[4].ToString() + ID[5].ToString());
                        var AddressInfo = from c in context.IdCard where c.BM == IDBM select c.DQ;
                        if (AddressInfo != null)
                        {
                            foreach (var a in AddressInfo)
                            {
                                result.Address = a.ToString();
                            }
                        }
                    }
                }
                catch (Exception e)
                {                    
                    result.ErrorMessage = e.Message;
                }
            }
            else
            {
                result.validate = false;
                result.ErrorMessage = "输入的身份证信息有误,请检查";
            }
            return result;
        }
    }

    public class CheckedResult
    {
        public string Address{ get; set; }
        public string  ErrorMessage{ get; set; }
        public bool ? validate { get; set; }
        public string sex{ get; set; }
    }
}
