using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelEntities;
using HotelLogic.Setting;

public partial class Setting_ZD_selectData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 封装filter数据类
        ZDViewMedel filter = new ZDViewMedel();
        filter.id = Convert.ToInt32(Request.Form["AutoID"]);
        filter.f_jb = Request.Form["f_jb"];
        filter.FangAnName = Request.Form["FangAnName"];
        filter.StartFee = Convert.ToDecimal(Request.Form["StartFee"]);
        filter.AddLen = Convert.ToInt32(Request.Form["AddLen"]);
        filter.StartLen = Convert.ToInt32(Request.Form["StartLen"]);
      
       
        filter.AddFee = Convert.ToDecimal(Request.Form["AddFee"]);
        filter.MinLen = Convert.ToInt32(Request.Form["MinLen"]);
        filter.MinFee = Convert.ToDecimal(Request.Form["MinFee"]);
        filter.MaxLen = Convert.ToInt32(Request.Form["MaxLen"]);
        #endregion

        switch (action)
        {
            case "getLastid":
                string rlt = ZDHelper.getLastAutoID().ToString();//.ToString("0000000000");
                result = "{\"Success\":\"true\",\"id\":" + rlt + "}";

                break;
            case "create":
                ZDViewMedel temp = new ZDViewMedel();
                temp.f_jb = Request.Form["f_jb"];
                temp.FangAnName = Request.Form["FangAnName"];
                temp.StartLen = Convert.ToInt32(Request.Form["StartLen"]);
                temp.StartFee = Convert.ToDecimal(Request.Form["StartFee"]);
                temp.AddLen = Convert.ToInt32(Request.Form["AddLen"]);
                temp.AddFee = Convert.ToDecimal(Request.Form["AddFee"]);
                temp.MinLen = Convert.ToInt32(Request.Form["MinLen"]);
                temp.MinFee = Convert.ToDecimal(Request.Form["MinFee"]);
                temp.MaxLen = Convert.ToInt32(Request.Form["MaxLen"]);
                string rlt2 = ZDHelper.Create(temp);
                result = "{\"Success\":\"true\"}";

                break;
            case "read":
                string f_jb = Request.QueryString["id"];
                if (f_jb == "")
                {
                    return;
                }
                else
                {
                    List<ZDViewMedel> resultall = ZDHelper.ReadZDByJB(f_jb);
                    result = Utils.ToRecordJson(resultall);
                }
                break;
            case "getZDByLX":
                string lx = Request.Form["FJLX"];
                if (lx == "")
                {
                    return;
                }
                else
                {
                    result = ZDHelper.GetZDByLX(lx);
                }
                break;
            case "update":
                Int32 UserID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = ZDHelper.gUserInfo(UserID);
                temp.f_jb = Request.Form["f_jb"];
                temp.FangAnName = Request.Form["FangAnName"];
                temp.StartLen = Convert.ToInt32(Request.Form["StartLen"]);
                temp.StartFee = Convert.ToDecimal(Request.Form["StartFee"]);
                temp.AddLen = Convert.ToInt32(Request.Form["AddLen"]);
                temp.AddFee = Convert.ToDecimal(Request.Form["AddFee"]);
                temp.MinLen = Convert.ToInt32(Request.Form["MinLen"]);
                temp.MinFee = Convert.ToDecimal(Request.Form["MinFee"]);
                temp.MaxLen = Convert.ToInt32(Request.Form["MaxLen"]);

                rlt = ZDHelper.Update(temp);
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
                Int32 delID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                rlt = ZDHelper.Delete(delID);
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