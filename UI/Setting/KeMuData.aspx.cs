using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_KeMuData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //result
        string result = string.Empty;

        KMHelper helper = new KMHelper();

        string action = Request["action"] != "" ? Request["action"] : "read";
        string km = Request.Form["km"] != "" ? Request.Form["km"]: "default km";
        string jd = Request.Form["jd"] != "" ? Request.Form["jd"] : "贷";
        Int32 ID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
        KMViewModel temp = new KMViewModel();

        switch (action)
        {
            case "read":
                var rlt = helper.ReadAll();
                result = Utils.ToRecordJson(rlt);
                break;
            case "update":
                temp.KM = km;
                temp.JD = jd;
                temp.ID = ID;
                result=helper.Update(temp);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "create":
                temp.KM = km;
                temp.JD = jd;
                result=helper.Create(temp);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "delete":
                result = helper.Delete(ID);
                if(result=="0")
                    result = "{\"success\":\"true\"}";
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}