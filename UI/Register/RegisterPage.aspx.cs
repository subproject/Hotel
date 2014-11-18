using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Register_RegisterPage : System.Web.UI.Page
{
    public string fh = string.Empty;
    public string jb = string.Empty;
    public string dj = string.Empty;
    public string dr = string.Empty;
    public string lr = string.Empty;
    public string wakeuptime = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        fh = Request.QueryString["fh"] != null ? Request.QueryString["fh"] : "0000";
        //有房间信息传到该页，则在datagrid上绑定该房间信息
        if (fh != "0000")
        {
            //根据房号得到该房信息
            KFViewModule temp = KFHelper.GetKFByFH(fh);
            fh = temp.FH;
            jb = temp.JBName;
            dj = temp.DJ.ToString();
            dj = dj.Replace(".0000",".00");
        }
        dr = DateTime.Now.GetDateTimeFormats('g')[0].ToString();
        lr = DateTime.Today.AddDays(1).ToShortDateString() + " " + SettingHelper.Instance.ReadValue("TEndTime");
        wakeuptime = DateTime.Today.AddDays(1).ToShortDateString() + " 06:00";
    }
}