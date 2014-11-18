using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Register;

public partial class FrontDesk_SuiKeXinXiData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SuiKeHelper helper = new SuiKeHelper();
        //分页数据读取
        Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "15");
        List<SuiKe> resultall = helper.ReadAllSuiKe();
        List<SuiKe> resultcurrent = helper.ReadPageSuiKe(page, rows);
        Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
    }
}