using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using HotelLogic.Cash.CashAction;

public partial class FrontDesk_GoodsLeaseData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 封装filter数据类
        wpViewModel filter = new wpViewModel();
        //没有找到这个数据
        filter.id = Convert.ToInt32(Request.Form["id"] != "" ? Request.Form["id"] : "0");//就这里 不知道什么原因 参数没有传过来  后面的都是有值的
        filter.name = Request.Form["name"];
        filter.countleave = Convert.ToInt32(Request.Form["countleave"] != "" ? Request.Form["countleave"] : "0");
        filter.count = Convert.ToInt32(Request.Form["count"] != "" ? Request.Form["count"] : "0");

        wp_borrowViewModel filter_wp_borrow = new wp_borrowViewModel();
        filter_wp_borrow.id_wp_borrow = Convert.ToInt32(Request.Form["id_wp_borrow"] != "" ? Request.Form["id_wp_borrow"] : "0");
        filter_wp_borrow.wpname = Request.Form["wpname"];
        filter_wp_borrow.num = Convert.ToInt32(Request.Form["num"] != "" ? Request.Form["num"] : "0");
        filter_wp_borrow.yajin = Convert.ToInt32(Request.Form["yajin"] != "" ? Request.Form["yajin"] : "0");
        filter_wp_borrow.zjtime = Convert.ToDateTime(Request.Form["zjtime"] != "" ? Request.Form["zjtime"] : "01/01/2001 00:00");
        filter_wp_borrow.djnumber = Request.Form["djnumber"];
        filter_wp_borrow.beizhu = Request.Form["beizhu"];
        filter_wp_borrow.caozuoyuan =(string) Session["user"];
        filter_wp_borrow.fanhao = Request.Form["fanjianhao"];
        if (!string.IsNullOrEmpty(Request.QueryString["orderguid"]))
        {
            filter_wp_borrow.orderguid = Guid.Parse(Request.QueryString["orderguid"]);
        }
        // filter_wp_borrow.state = Convert.ToInt32(Request.Form["state"] != "" ? Request.Form["state"] : "0");



        #endregion

        switch (action)
        {
            case "create":
                string rlt = wpHelper.Create(filter);
                result = "{\"Success\":\"true\"}";
                break;
            case "read":
                List<wpViewModel> resultall = wpHelper.ReadAll();
                Response.Write(Utils.ToRecordJson(resultall));
                break;
            case "update":
                filter.id = Convert.ToInt32(Request.QueryString["id"]);
                string rlt1 = wpHelper.Update(filter);
                if (rlt1 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt1;
                break;
            case "delete":
                string rlt2 = wpHelper.Delete(filter.id);
                if (rlt2 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt2;
                break;
            case "create_wp_borrow":
                filter_wp_borrow.wpname = Request.QueryString["wpnamecbo"];
                JieZhangTuFangHelper jzhelper = JieZhangTuFangHelper.Instance;  
                NameValueCollection nc=new NameValueCollection();
                nc.Add("OrderGuid",filter_wp_borrow.orderguid.ToString());
                if (jzhelper.IsOrderJieZhang(nc).Flag)
                {
                    string rlt3 = wpHelper.Create_wp_borrow(filter_wp_borrow);
                    result = "{\"Success\":\"true\"}";
                }
                else
                {
                    result = "{\"Success\":\"false\"}";
                }
                break;
            case "read_wp_borrow":
                List<wp_borrowViewModel> resultall_wp_borrow = wpHelper.ReadAll_wp_borrow(Request.QueryString["orderguid"]);
                // Response.Write(Utils.ToRecordJson(resultall_wp_borrow));
                string str = Regex.Replace(Utils.ToRecordJson(resultall_wp_borrow, resultall_wp_borrow.Count), @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });


                Response.Write(str);
                break;
            case "update_wp_borrow":
                //   filter_wp_borrow.id_wp_borrow = Convert.ToInt32(Request.QueryString["id_wp_borrow"]); 
                string rlt4 = wpHelper.Update_wp_borrow(filter_wp_borrow);
                if (rlt4 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt4;
                break;
            case "delete_wp_borrow":
                string rlt5 = wpHelper.Delete_wp_borrow(filter_wp_borrow);
                if (rlt5 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt5;
                break;
            default:
                break;
        }


        //output json result
        Response.Write(result);
    }
}