using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class FrontDesk_ZK_weihuData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 封装filter数据类

        TqViewModel filter = new TqViewModel();
        filter.id = Convert.ToInt32(Request.Form["id"]);
        filter.tequanren = Request.Form["tequanren"];
        filter.fjzhekou = Convert.ToDecimal(Request.Form["fjzhekou"]);
        filter.isMorenZheKou = Request.Form["isMorenZheKou"] != "on" ? false : true;
        filter.timeLimite = Request.Form["timeLimite"] != "on" ? false : true;
        filter.validDate = Convert.ToInt32(Request.Form["validDate"]!=""?Request.Form["validDate"]:"0");
        #endregion

        switch (action)
        {
            case "getLastAutoID":
                string rlt = ZK_weihuHelper.getLastAutoID().ToString();//.ToString("0000000000");
                result = "{\"Success\":\"true\",\"id\":" + rlt + "}";

                break;
            case "create":
                TqViewModel temp = new TqViewModel();
                temp.tequanren = Request.Form["tequanren"];
                temp.fjzhekou = Convert.ToDecimal(Request.Form["fjzhekou"]);
                temp.isMorenZheKou = Request.Form["isMorenZheKou"] != "on" ? false : true;
                temp.timeLimite = Request.Form["timeLimite"] != "on" ? false : true;
                temp.validDate = Convert.ToInt32(Request.Form["validDate"]);
                if (temp.fjzhekou < 80)
                {
                    Response.Write("<script>alert('折扣不能少于80%')</script>");
                }
                else if (temp.fjzhekou > 100)
                {
                    Response.Write("<script>alert('折扣不能大于100%')</script>");
                }
                else
                {
                    string rlt2 = ZK_weihuHelper.Create(temp);
                    result = "{\"Success\":\"true\"}";
                }

                break;
            case "read":
                List<TqViewModel> resultall = ZK_weihuHelper.ReadAll();
                Response.Write(Utils.ToRecordJson(resultall));

                break;
            case "update":
                Int32 UserID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");

                temp = ZK_weihuHelper.gUserInfo(UserID);
                temp.tequanren = Request.Form["tequanren"];
                temp.fjzhekou = Convert.ToDecimal(Request.Form["fjzhekou"]);
                temp.isMorenZheKou = Request.Form["isMorenZheKou"] != "on" ? false : true;
                temp.timeLimite = Request.Form["timeLimite"] != "on" ? false : true;
                temp.validDate = Convert.ToInt32(Request.Form["validDate"]);
                if (temp.fjzhekou < 80)
                {
                    Response.Write("<script>alert('折扣不能少于80%')</script>");
                }
                else if (temp.fjzhekou > 100)
                {
                    Response.Write("<script>alert('折扣不能大于100%')</script>");
                }
                else
                {
                    rlt = ZK_weihuHelper.Update(temp);
                    if (rlt == "0")
                    {
                        Response.Clear();
                        Response.Write("{\"success\":true}");
                        Response.End();
                    }
                    else
                        result = rlt;
                }

                break;
            case "delete":
                Int32 delID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                rlt = ZK_weihuHelper.Delete(delID);
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