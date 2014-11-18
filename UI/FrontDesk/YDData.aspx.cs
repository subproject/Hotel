using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class FrontDesk_YDData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //createorder,createsummer,createall,
        string action = Request["action"] != "" ? Request["action"] : "read";

        //result, return to view page
        string result = string.Empty;

        //helper
        YDHelper helper = new YDHelper();

        #region 充分初始化，如果没有相关信息则自动为null
        //用来封装数据，进行提交
        //DD
        YDOrder order = new YDOrder();
        order.Customer = Request.Form["Customer"];
        order.CustomerTel = Request.Form["CustomerTel"];
        order.LeaveTime = Convert.ToDateTime(Request.Form["LeaveTime"] != "" ? Request.Form["LeaveTime"] : "01/01/2001 00:00");
        order.OnBoardTime = Convert.ToDateTime(Request.Form["OnBoardTime"] != "" ? Request.Form["OnBoardTime"] : "01/01/2001 00:00");
        order.Yder = Request.Form["Yder"];
        order.YdTel = Request.Form["YdTel"];
        //FJ Summer
        List<YDFJSummer> summerlist=new List<YDFJSummer>();
        //FJ Detail
        List<YDDetail> detaillist=new List<YDDetail>();
        //如果房号不为空,即有房号,则初始化detail和summer信息
        if (!string.IsNullOrEmpty(Request["fhs"]))
        {
            var FHList = Request["fhs"].Split(':');
            //房间明细
            foreach (var fh in FHList)
            {
                YDDetail detail = new YDDetail();
                detail.FH = fh.Split('-')[0];
                detail.JB = fh.Split('-')[1];
                detaillist.Add(detail);
            }
            //summer信息
            foreach (var d in detaillist)
            {
                bool exist = false;
                foreach (var t in summerlist)
                {
                    if (t.JB == d.JB)
                    {
                        exist = true;
                        t.SL += 1;
                    }
                }
                if (!exist)
                {
                    YDFJSummer temp = new YDFJSummer();
                    temp.JB = d.JB;
                    temp.SL = 1;
                    summerlist.Add(temp);
                }
            }
        }
        #endregion

        switch (action)
        {
            case "createorder":
                result = helper.CreateYDOrder(order);
                if (result != "-1")
                    result = "{\"Success\":\"true\"}";
                break;
            case "createsummer":
                result = helper.CreateYDOrderAndSummer(order, summerlist, detaillist);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "createall":
                result = helper.CreateYDAllInfo(order, summerlist, detaillist);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            default:
                break;
        }
        //output json data to view
        Response.Write(result);
    }
}