using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_update_kfcgy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        KFCgyViewModule _kfcgy = new KFCgyViewModule();
        _kfcgy.ID = Convert.ToInt32(Request["ID"] != "" ? Request["ID"] : "0");
        _kfcgy.KFJB = Request.Form["KFJB"] != "" ? Request.Form["KFJB"] : "";
        _kfcgy.DJ = Convert.ToDecimal(Request.Form["DJ"] != "" ? Request.Form["DJ"] : "0");
        _kfcgy.CW = Convert.ToByte(Request.Form["CW"] != "" ? Request.Form["CW"] : "0");
        _kfcgy.DDF = Convert.ToDecimal(Request.Form["DDF"] != "" ? Request.Form["DDF"] : "0");
        _kfcgy.LCF = Convert.ToDecimal(Request.Form["LCF"] != "" ? Request.Form["LCF"] : "0");
        string  result=KFCgyHelper.Update(_kfcgy);
        if (result == "0")
        {
            Response.Clear();
            Response.Write("{\"Success\":\"true\"}");
        }
        else
        {
            Response.Write(result);
        }

    }
}