using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Text.RegularExpressions;

public partial class FrontDesk_OrderData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"];
        string ID = Request["ID"] == "" ? Request["ID"] : "0";
        string fh = Request["fh"];
        OrdersHelper helper = new OrdersHelper();
        Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "1bn5");
        string result=string.Empty;
        #region 封装filter数据类
        OrderFilter filter = new OrderFilter();
        filter.XingMing=Request.Form["XingMing"];
        filter.AutoID=Request.Form["AutoID"];
        filter.FangHao=Request.Form["FangHao"];
        filter.FukuanFangshi=Request.Form["FukuanFangshi"];
        filter.KerenLeibie=Request.Form["KerenLeibie"];
        filter.ZhanghaoZhuangtai=Request.Form["ZhanghaoZhuangtai"];
        filter.Begin = Convert.ToDateTime(Request.Form["Begin"] != "" ? Request.Form["Begin"] : "01/01/2001 00:00");
        filter.End = Convert.ToDateTime(Request.Form["End"] != "" ? Request.Form["End"] : "01/01/2001 00:00");
        #endregion
        switch (action)
        {
                //CRUD
            case "create":
                break;
            case "read":
                Int32 count = helper.ReadOrder(filter).Count;
                result= Utils.ToRecordJson(helper.ReadPartOrder(page,rows,filter),count);
                break;
            case "readall":
                result = Utils.ToRecordJson(helper.ReadOrder());
                break;
            case "readcanselect":
                result = Utils.ToRecordJson(helper.ReadCanSelect(fh));
                break;
            case "update":
                break;
            case "delete":
                break;
            default:
                break;
        }
        string str = Regex.Replace(result, @"\\/Date\((\d+)\)\\/", match =>
        {
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
            dt = dt.ToLocalTime();
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        });
        Response.Write(str);
    }
}