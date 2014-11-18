using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FrontDesk_GoodsLease : System.Web.UI.Page
{
    public string OrderGuid = "";
    public string fanhao = "";
    public string date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        OrderGuid = Request.QueryString["OrderGuid"];
        fanhao = Request.QueryString["fh"];
        date=string.Format("{0:G}", DateTime.Now);
    }
}