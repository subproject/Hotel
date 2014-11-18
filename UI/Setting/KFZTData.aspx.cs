using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_KFZTData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"] != "" ? Request["action"] : "read";
        string AutoID = Request["AutoID"] != "" ? Request["AutoID"] : "0";
        KFZTHelper helper = new KFZTHelper();
        KFZTModel temp = new KFZTModel();
        string result = string.Empty;
        string rlt = string.Empty;
        switch (action)
        {
            case "create":
                temp.Color = Request.Form["Color"].Replace('#', ' ').Trim();
                temp.FjZt = Request.Form["FjZt"];
                rlt=helper.createKFZT(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "read":
                result = Utils.ToRecordJson(helper.ReadKFZT());
                break;
            case "update":
                temp.AutoID = Convert.ToInt32(AutoID);
                temp.Color = Request.Form["Color"].Replace('#',' ').Trim();
                temp.FjZt = Request.Form["FjZt"];
                rlt=helper.UpdateKFZT(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "delete":
                rlt = helper.DeleteKFZT(Convert.ToInt32(AutoID));
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            default:
                break;
        }
        Response.Write(result);
    }
}