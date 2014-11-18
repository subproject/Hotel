using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_SalersData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //result
        string result = string.Empty;

        string action = Request["action"] != "" ? Request["action"] : "read";
        string name = Request.Form["name"] != "" ? Request.Form["name"] : "销售员";
        Int32 ID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
        SalersModel temp = new SalersModel();

        switch (action)
        {
            case "read":
                var rlt = SalersHelper.Instance.ReadSalers();
                result = Utils.ToRecordJson(rlt);
                break;
            case "update":
                temp.Name = name;
                temp.AutoID = ID;
                result = SalersHelper.Instance.UpdateSalers(temp);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "create":
                temp.Name = name;
                result = SalersHelper.Instance.CreateSalers(temp);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "delete":
                result = SalersHelper.Instance.DeleteSalers(ID);
                if (result == "0")
                    result = "{\"success\":\"true\"}";
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}