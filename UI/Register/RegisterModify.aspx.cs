using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;
using HotelLogic.FrontDesk;

public partial class RegisterModify : System.Web.UI.Page
{
    public string fh = string.Empty;
    public string jb = string.Empty;
    public string dj = string.Empty;
    public string dr = string.Empty;
    public string lr = string.Empty;
    public string wakeuptime = string.Empty;
    public string name = string.Empty;
    public string price = string.Empty;
    public string ZKL = string.Empty;
    public string fjrows =null;
    public string fjsuike = null;
    public string fjorder= null;

    public string orderid = null;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        fh = Request.QueryString["fh"] != null ? Request.QueryString["fh"] : "0000";
        //有房间信息传到该页，则在datagrid上绑定该房间信息
        if (fh != "0000")
        {
            OrderViewModel temp = OrdersHelper.GetKFDJOrder(fh);
            // fh = temp.FH;
            jb = temp.FangjianLeixing;
            dj = temp.BiaozhunFangjia.ToString();
            dj = dj.Replace(".0000", ".00");
            price = temp.ShijiFangjia.ToString().Replace(".0000", ".00");
            ZKL = temp.ZheKouLv.ToString();
            fjorder = Utils.ToRecordJson(temp);
            fjrows = Utils.ToRecordJson(OrdersHelper.GetKFDJDetail(fh));
            fjsuike = Utils.ToRecordJson(OrdersHelper.GetKFDJSuiKe(fh));
            dr = temp.DaodianTime.GetDateTimeFormats('g')[0].ToString();
            lr = temp.LidianTime.ToShortDateString();
            wakeuptime = DateTime.Today.AddDays(1).ToShortDateString() + " 06:00";

            orderid = temp.OrderGuid.ToString();

        }
       

             
           
      
       
    }
}