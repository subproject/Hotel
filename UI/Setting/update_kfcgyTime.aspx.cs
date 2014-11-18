using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_update_kfcgyTime : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["action"]) && Request["action"]=="read")
        {
            Response.Write(Utils.DataContractJsonSerialize(KFCgyHelper.GetzdInfo(Request["fjtype"])));
            
        }
        else if (!string.IsNullOrEmpty(Request["action"]) && Request["action"] == "readall")
        {
            Response.Write(Utils.DataContractJsonSerialize(KFCgyHelper.GetzdInfo()));
            
        }
        else
        {
            KFCgyViewModule _kfcgy = new KFCgyViewModule();
            _kfcgy.zdf_etime = Request.Form["zdf_etime"] != "" ? Request.Form["zdf_etime"] : "00:00:00";
            _kfcgy.zdf_stime = Request.Form["zdf_stime"] != "" ? Request.Form["zdf_stime"] : "00:00:00";
            string result = KFCgyHelper.UpdateTime(_kfcgy);
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
}