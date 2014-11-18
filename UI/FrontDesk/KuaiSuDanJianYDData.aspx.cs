using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using HotelLogic.Setting;

public partial class FrontDesk_KuaiSuDanJianYDData : System.Web.UI.Page
{
    //仅实现新增一条预定信息的功能
    protected void Page_Load(object sender, EventArgs e)
    {
        //Customer客户
        string Customer=Request.Form["Customer"];
        //Yder预订人
        string Yder = Request.Form["Yder"];
        //YdTel 预订人电话
        string YdTel = Request.Form["YdTel"];
        //CustomerTel客人电话
        string CustomerTel = Request.Form["CustomerTel"];
        //入住日期
        System.DateTime OnBoardTime = Convert.ToDateTime(Request.Form["OnBoardTime"] != "" ? Request.Form["OnBoardTime"] : "01/01/2001 00:00");
        //离店日期
        System.DateTime LeaveTime = Convert.ToDateTime(Request.Form["LeaveTime"] != "" ? Request.Form["LeaveTime"] : "01/01/2001 00:00");
        //房号
        string FH=Request.Form["FH"];
        string JB = KFHelper.GetJBFromFH(FH);

        YDOrder order = new YDOrder();
        order.OnBoardTime = OnBoardTime;
        order.LeaveTime = LeaveTime;
        order.Customer = Customer;
        order.CustomerTel = CustomerTel;
        order.Yder = Yder;
        order.YdTel = YdTel;

        List<YDFJSummer> fjs = new List<YDFJSummer>();
        YDFJSummer fj = new YDFJSummer();
        fj.SL = 1;
        fj.JB = JB;
        fj.f_dm = FH;
        fjs.Add(fj);

        List<YDDetail> fjl = new List<YDDetail>();
        YDDetail yd = new YDDetail();
        yd.FH = FH;
        yd.JB = JB;
        fjl.Add(yd);

        YDHelper helper = new YDHelper();
        if (helper.CreateYDAllInfo(order, fjs, fjl) == "0")
        {
            Response.Write("{\"Success\":\"true\"}");
        }
    }
}