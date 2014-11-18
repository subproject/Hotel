using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Register;
using System.Text.RegularExpressions;

public partial class FrontDesk_ZaiDianGuanLiData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //CustomerHelper helper = new CustomerHelper();
        ////分页数据读取
        //Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        //Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "15");
        //List<Customer> resultall = helper.ReadAllCustomer();
        //List<Customer> resultcurrent = helper.ReadPageCustomer(page, rows);
        //string str = Regex.Replace(Utils.ToRecordJson(resultcurrent, resultall.Count), @"\\/Date\((\d+)\)\\/", match =>
        //{
        //    DateTime dt = new DateTime(1970, 1, 1);
        //    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
        //    dt = dt.ToLocalTime();
        //    return dt.ToString("yyyy-MM-dd HH:mm:ss");
        //});
        //Response.Write(str);
    }
}