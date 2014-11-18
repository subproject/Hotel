using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_PositionMgrData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string ID = Request["ID"] != "" ? Request["ID"] : "";
        string action = Request["action"] != "" ? Request["action"] : "";
        PositionViewModel temp = new PositionViewModel();
        PositionHelper helper = new PositionHelper();
        string rlt=string.Empty;
        switch (action)
        {
            case "create":
                temp.LocCode = Request.Form["LocCode"] != "" ? Request.Form["LocCode"] : "";
                temp.LocName = Request.Form["LocName"] != "" ? Request.Form["LocName"] : "";
                rlt = helper.CreatePosition(temp);
                if (rlt == "0")
                    result = "{\"Success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "read":
                Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<PositionViewModel> resultall = helper.ReadPosition();
                List<PositionViewModel> resultcurrent = helper.ReadPartPosition(page, rows);
                Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
                break;
            case "update":
                temp.ID = int.Parse(ID);
                temp.LocCode = Request.Form["LocCode"] != "" ? Request.Form["LocCode"] : "";
                temp.LocName = Request.Form["LocName"] != "" ? Request.Form["LocName"] : "";
                rlt = helper.UpdatePosition(temp);
                if (rlt == "0")
                    result = "{\"Success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "delete":
                rlt = helper.DeletePosition(int.Parse(ID));
                if (rlt == "0")
                    result = "{\"Success\":\"true\"}";
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