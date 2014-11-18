using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;

public partial class Cash_RunningListData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"] != null ? Request["action"] : "read";
        string orderid = Request["orderid"];
        if (string.IsNullOrEmpty(orderid))
        {
            orderid = Guid.NewGuid().ToString();
        }
        RunningList helper = new RunningList();
        switch (action)
        {
            case "create":
                RunningModel temp = new RunningModel();
                temp.CustomerName = Request.Form["CustomerName"];
                temp.Deposit = Convert.ToDecimal(Request.Form["Deposit"]!=""?Request.Form["Deposit"]:"0");
                temp.KM = Request.Form["KM"];
                temp.Operator = Request.Form["Operator"];
                temp.Payment = Request.Form["Payment"];
                temp.Price = Convert.ToDecimal(Request.Form["Price"] != "" ? Request.Form["Price"] : "0");
                temp.Remark = Request.Form["Remark"];
                temp.RoomNo = Request.Form["RoomNo"];
                temp.Status=Request.Form["Status"];
                temp.RunningNum = Request.Form["RunningNum"];
                temp.RunningNumAuto=Request.Form["RunningNumAuto"];
                temp.RunningTime = DateTime.Now;
                if (string.IsNullOrEmpty(orderid))
                    temp.OrderGuid = System.Guid.Empty;
                else
                    temp.OrderGuid = new Guid(orderid);
                if (helper.CreateRunning(temp) == "0")
                    Response.Write("{\"Success\":\"true\"}");
                break;
            case "readffee":
                
                Response.Write(Utils.ToRecordJson(helper.GetFFeeByOrder(new Guid(orderid))));
                break;
            case "readotrfee":
                Response.Write(Utils.ToRecordJson(helper.GetOtherFeeByOrder(new Guid(orderid))));
                break;
            case "readall":
                Response.Write(Utils.ToRecordJson(helper.GetRunningByOrder(new Guid(orderid))));
                break;
            case "update":
                break;
            case "delete":
                break;
            default:
                break;
        }
    }
}