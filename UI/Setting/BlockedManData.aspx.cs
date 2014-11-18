using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_BlockedManData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"] != "" ? Request["action"] : "read";
        Int32 AutoID = Convert.ToInt32(Request["AutoID"] != "" ? Request["AutoID"] : "0");
        BlockedManHelper helper = new BlockedManHelper();
        BMModel temp = new BMModel();

        string name = Request.Form["Name"] != "" ? Request.Form["Name"] : "";
        string idcardno = Request.Form["IDCardNo"] != "" ? Request.Form["IDCardNo"] : "";
        string remark = Request.Form["Remark"] != "" ? Request.Form["Remark"] : "";

        string result = string.Empty;
        switch (action)
        {
            case "create":
                temp.Name = name;
                temp.IDCardNo = idcardno;
                temp.Remark = remark;
                result = helper.CreateBM(temp);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "read":
                result = Utils.ToRecordJson(helper.ReadBM());
                break;
            case "update":

                break;
            case "delete":
                result = helper.DeleteBM(AutoID);
                if (result == "0")
                    result = "{\"success\":\"true\"}";
                break;
            default:
                break;
        }
        Response.Write(result);
    }
}