using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_FHChargeData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
             string  result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        switch (action)
        {
            case "fjstatues":
                if (!string.IsNullOrEmpty(Request["fangjianhao"]))
                {
                    result = FHChargeHelper.GetKFByFH(Request["fangjianhao"]);
                   // Response.Write(Utils.ToRecordJson(result));
                }
                break;
            case "getRoomPrice":
                if (!string.IsNullOrEmpty(Request["fangjianhao"]))
                {
                    KFViewModule temp = FHChargeHelper.getInfoByNo(Request["fangjianhao"]);
                    result = temp.DJ.ToString().Replace(".0000",".00");;
                }
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}