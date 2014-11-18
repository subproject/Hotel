using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_save_kf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KFViewModule _kf = new KFViewModule();
        _kf.FH = Request.Form["FH"] != "" ? Request.Form["FH"] : "0000";
        _kf.StatusCode = Request.Form["StatusCode"] != "" ? Request.Form["StatusCode"] : "";
        _kf.StatusName = "空房"; //Request.Form["StatusName"] != "" ? Request.Form["StatusName"] : "";
        _kf.JBCode = Request.Form["JBCode"] != "" ? Request.Form["JBCode"] : "";
        _kf.JBName = Request["JBName"] != "" ? Request["JBName"] : "";
        _kf.Detail = Request.Form["Detail"] != "" ? Request.Form["Detail"] : "";
        _kf.DJ = Convert.ToDecimal(Request.Form["DJ"] != "" ? Request.Form["DJ"] : "0");
        _kf.BJ = Convert.ToDecimal(Request.Form["BJ"] != "" ? Request.Form["BJ"] : "0");
        _kf.CW = Convert.ToByte(Request.Form["CW"] != "" ? Request.Form["CW"] : "0");
        _kf.LcID = Convert.ToByte(Request.Form["Lc"] != "" ? Request.Form["Lc"] : "0");
        _kf.LdID = Convert.ToByte(Request.Form["Ld"] != "" ? Request.Form["Ld"] : "0");
        KFHelper.Create(_kf);
        Response.Clear();
        Response.Write("{\"Success\":\"true\"}");
        Response.End();
    }
}