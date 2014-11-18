using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class FrontDesk_HuanFangLYData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 封装filter数据类

        HFViewModel filter = new HFViewModel();
        filter.AutoID = Convert.ToInt32(Request.Form["AutoID"]);
        filter.Content = Request.Form["Content"];
      
        #endregion

        switch (action)
        {
            case "getLastAutoID":
                string rlt = HFHelper.getLastAutoID().ToString();//.ToString("0000000000");
                result = "{\"Success\":\"true\",\"AutoID\":" + rlt + "}";

                break;
            case "create":
                HFViewModel temp = new HFViewModel();
                temp.Content = Request.Form["Content"].ToString();
               
                string rlt2 = HFHelper.Create(temp);
                result = "{\"Success\":\"true\"}";

                break;
            case "read":
                List<HFViewModel> resultall = HFHelper.ReadAll();
              Response.Write(Utils.ToRecordJson(resultall));

                break;
            case "update":
                Int32 UserID = Convert.ToInt32(Request["AutoID"] != "" ? Request["AutoID"] : "0");

                temp = HFHelper.gUserInfo(UserID);
                temp.Content = Request.Form["Content"].ToString();
                rlt = HFHelper.Update(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;

                break;
            case "delete":
                Int32 delID = Convert.ToInt32(Request["AutoID"] != "" ? Request["AutoID"] : "0");
                rlt = HFHelper.Delete(delID);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;
            default:
                break;
        }


        //output json result
        Response.Write(result);
    }
}