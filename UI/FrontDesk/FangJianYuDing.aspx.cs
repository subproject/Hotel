using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class FrontDesk_FangJianYuDing : System.Web.UI.Page
{
    public string FjCtgStr = string.Empty;
    public string ARRTime = string.Empty;
    public string LEATime = string.Empty;
    public string YDTime = string.Empty;
    public string caozuoyuan = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        List<KFCgyViewModule> resultall = KFCgyHelper.ReadAll();
        FjCtgStr=Utils.ToRecordJson(resultall);
        ARRTime = DateTime.Now.AddHours(1).GetDateTimeFormats('g')[0].ToString();
        YDTime = DateTime.Now.AddHours(1).GetDateTimeFormats('g')[0].ToString();
        LEATime = DateTime.Today.AddDays(1).ToShortDateString() + " " + SettingHelper.Instance.ReadValue("TEndTime");
        caozuoyuan = Session["user"].ToString();
    }
}