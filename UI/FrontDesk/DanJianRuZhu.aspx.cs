using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontDesk_DanJianRuZhu : System.Web.UI.Page
{
    public string fh = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        fh = Request.QueryString["fh"] != null ? Request.QueryString["fh"] : "0000";
        if (fh == "0000")
        {
            Response.Write("没有得到正确的房号!");
        }

    }
}