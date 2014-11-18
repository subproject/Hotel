using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;

public partial class FrontDesk_HuanFangData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HuanFangDan data = new HuanFangDan();
        data.Deposit = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["Deposit"]) ? Request.Form["Deposit"] : "0");
        data.LeaveDate = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["LeaveTime"]) ? Request.Form["LeaveTim"] : "01/01/2001 00:00");
        data.Money = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["Money"]) ? Request.Form["Money"] : "0");
        data.Name = Request.Form["Name"];
        data.NewPrice = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["NewPrice"]) ? Request.Form["NewPrice"] : "0");
        data.NewRoomID = Request.Form["NewRoomID"];
        data.Number = Request.Form["Number"];
        data.OrderGuid = new Guid(!string.IsNullOrEmpty(Request.Form["OrderGuid"])?Request.Form["OrderGuid"]:new Guid().ToString());
        data.Price = Convert.ToDecimal(!string.IsNullOrEmpty(Request.Form["Price"]) ? Request.Form["Price"] : "0");
        data.Rebate = Convert.ToDouble(!string.IsNullOrEmpty(Request.Form["Rebate"]) ? Request.Form["Rebate"] : "0");
        data.Remark = Request.Form["Remark"];
        data.RoomID = Request.Form["RoomID"];
        data.RoomLevel = Request.Form["RoomLevel"];
        data.ZhengjianHaoma = Request.Form["ZhengjianHaoma"];
        data.ArriveTime = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["ArriveTime"]) ? Request.Form["ArriveTime"] : "01/01/2001 00:00");

        if (HuanfangHelper.Instance.HuanFang(data) == "0")
        {
            Response.Write("{\"Sucess\":\"true\"}");
        }
    }
}