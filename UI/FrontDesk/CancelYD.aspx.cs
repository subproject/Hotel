using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class FrontDesk_CancelYD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fh = Request["fh"];
        if (!string.IsNullOrEmpty(fh))
        {
            YDHelper helper = new YDHelper();
            helper.CancelYDOrder(fh);
           
            if (string.IsNullOrEmpty(Request["ydnum"]))
            {
                Response.Write("<script>alert('取消预定成功')</script>");
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Response.Write("<script>alert('取消预定成功')</script>");
                Response.Redirect("YuDingChaXun.htm");
            }
        }
    }
}