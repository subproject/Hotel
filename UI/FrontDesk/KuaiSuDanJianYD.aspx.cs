using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using HotelLogic.Setting;

public partial class FrontDesk_KuaiSuDanJianYD : System.Web.UI.Page
{
    public string fh = string.Empty;
    public string YDTime = string.Empty;
    public string YLTime = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        fh = Request.QueryString["fh"] != null ? Request.QueryString["fh"] : "0000";
        if (fh == "0000")
        {
            Response.Write("没有得到正确的房号!");
        }
        YDTime = DateTime.Now.AddHours(1).GetDateTimeFormats('g')[0].ToString();
        YLTime = DateTime.Today.AddDays(1).ToShortDateString() + " "+ SettingHelper.Instance.ReadValue("TEndTime");
    }
}