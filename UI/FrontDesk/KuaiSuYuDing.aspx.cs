using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using HotelEntities;
using HotelLogic.Setting;

public partial class FrontDesk_KuaiSuYuDing : System.Web.UI.Page
{
    public string YDTime = string.Empty;
    public string YLTime = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        YDTime = DateTime.Now.AddHours(1).GetDateTimeFormats('g')[0].ToString();
        YLTime = DateTime.Today.AddDays(1).ToShortDateString() + " " + SettingHelper.Instance.ReadValue("TEndTime");
    }

}