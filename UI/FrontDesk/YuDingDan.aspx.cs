using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class FrontDesk_YuDingDan : System.Web.UI.Page
{
    public string Yder = string.Empty;
    public string YdTel = string.Empty;
    public string Customer = string.Empty;
    public string CustomerTel = string.Empty;
    public string Dr = string.Empty;
    public string Lr = string.Empty;
    public string FJs = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string fh = Request["fh"];
        string num=Request["num"];
        if (!string.IsNullOrEmpty(fh) || !string.IsNullOrEmpty(num))
        {
            YDHelper helper = new YDHelper();
            PrintYD temp = new PrintYD();
            if (!string.IsNullOrEmpty(fh))
            {
                 temp = helper.PrintYDOrder(fh);
            }
            else
            {
                if (!string.IsNullOrEmpty(num))
                {
                     temp = helper.PrintYDOrderByNum(num);
                }
            }
            Yder = temp.Yder;
            YdTel = temp.YdTel;
            Customer = temp.Customer;
            CustomerTel = temp.CustomerTel;
            Dr = temp.OnBoardTime.ToString();
            Lr = temp.LeaveTime.ToString();
            foreach (var t in temp.FJs)
            {
                if (string.IsNullOrEmpty(t.JB))
                {
                    FJs = FJs + t.FH + " ";
                }
                else
                {
                    FJs = FJs + t.FH + "(" + t.JB + ")" + " ";
                }
            }

        }
    }
}