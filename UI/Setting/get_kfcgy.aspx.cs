using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_get_kfcgy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
        List<KFCgyViewModule> resultall=KFCgyHelper.ReadAll();
        List<KFCgyViewModule> resultcurrent=KFCgyHelper.ReadPart(page,rows);
        Response.Write(Utils.ToRecordJson(resultcurrent,resultall.Count));
    }
}