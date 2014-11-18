using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class Register_SplitRegisterOrder : System.Web.UI.Page
{
    public string Fanghao = string.Empty;
    public string Xingming = string.Empty;
    public string AutoID = string.Empty;
    public string Dianhua = string.Empty;
    public string OrderGuid = string.Empty;
    public string fh = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //fh不为0000时进行处理
        fh = Request["fh"] != "" ? Request["fh"] : "0000";

        if (fh != "0000")
        {
            //get order information by fh
            OrdersHelper helper = new OrdersHelper();
            OrderViewModel order = helper.ReadOrderviewmodelByFH(fh);
            if (order != null)
            {
                Fanghao = order.FangHao;
                Dianhua = order.DianHua;
                AutoID = order.AutoID;
                Xingming = order.XingMing;
                OrderGuid = order.OrderGuid.ToString();
            }
            //binding order information to page, post to server with running attach
        }
    }
}