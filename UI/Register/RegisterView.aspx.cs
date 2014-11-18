using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class Register_RegisterView : System.Web.UI.Page
{
    public string FH = string.Empty;
    public OrderAttachModel data = new OrderAttachModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        FH = Request["fh"];
        if (!string.IsNullOrEmpty(FH))
        {
            OrdersHelper helper = new OrdersHelper();
            data = helper.ReadAttachByFH(FH);
        }
    }
}