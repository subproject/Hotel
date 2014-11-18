using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;

public partial class FrontDesk_XuZhuData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XuZhuModel data = new XuZhuModel();
        data.AppendDeposit = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["AppendDeposit"])? Request.Form["AppendDeposit"] : "0");
        data.DayCount = Convert.ToInt32(!string.IsNullOrEmpty(Request.Form["DayCount"]) ? Request.Form["DayCount"] : "0");
        data.Deposit = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["Deposit"]) ? Request.Form["Deposit"] : "0");
        data.LeaveTime = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["LeaveTime"]) ? Request.Form["LeaveTim"] : "01/01/2001 00:00");
        data.Name = Request.Form["Name"];
        data.NewLeaveTime = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["NewLeaveTime"]) ? Request.Form["NewLeaveTime"] : "01/01/2001 00:00");
        data.PayWay = Request.Form["PayWay"];
        data.Price = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["Price"]) ? Request.Form["Price"] : "0");
        data.RoomID = Request.Form["RoomID"];
        data.RoomLevel = Request.Form["RoomLevel"];
        if (XuZhuHelper.Instance.XuZhu(data) == "0")
        {
            Response.Write("{\"Success\";\"true\"}");
        }
    }
}