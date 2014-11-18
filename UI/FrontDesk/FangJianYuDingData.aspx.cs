using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Web.Script.Serialization;

public partial class FrontDesk_FangJianYuDingData : System.Web.UI.Page
{
    //把预定单和概要信息存进DB,待进一步自动分配房间
    protected void Page_Load(object sender, EventArgs e)
    {
      string action = Request["action"] != "" ? Request["action"] : "";
        
        switch (action)
        {
            case "ydshoukuanread":
                if (!string.IsNullOrEmpty(Request["ydid"] ))
                {
                    Response.Write(Utils.DataContractJsonSerialize((new YDHelper()).GetYfjInfo(Request["ydid"])));
                }
                break;
            case "ydshoukuan":
                YDShouKuan sk = new YDShouKuan();
                sk.Fkfs = Request.Form["ydfkfs"];
                sk.jine = Convert.ToDecimal(Request.Form["ydjine"]);
                sk.time = Request.Form["ydfstime"] != null ? Convert.ToDateTime(Request.Form["ydfstime"]) : new DateTime(2000, 1, 1);
                sk.danjuhao = Request.Form["yuddanjuhao"];
                sk.relatedname = Request.Form["ydrelatedname"];
                sk.zhangkuan =Convert.ToDecimal( Request.Form["ydzhangkuan"]);
                sk.beizhu = Request.Form["ydbeizhu"];
                sk.ydGuid = Request["ydid"];
                sk.shoukuanType = Convert.ToInt32(Request["shoukuantype"]);
                if ((new YDHelper()).SaveYdZhuanzhang(sk)) 
                {
                    Response.Write("{\"Success\":\"true\"}");
                }
                break;
            case "SaveAllItem"://获取可选择预定的房间信息
                List<YDFJSaveInfo> fjall = new List<YDFJSaveInfo>();
                if (Request["fjlist"] != "")
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    string temp = Request["fjlist"];
                    if (!string.IsNullOrEmpty(temp))
                    {
                        temp = temp.Trim();
                        string[] array = temp.Split(';');
                        foreach (var a in array)
                        {
                            fjall.Add(jss.Deserialize<YDFJSaveInfo>(a));
                        }
                    }
                }

                if ((new YDHelper()).ModifyYdFjInfo(Request["ydid"], Request["ydnum"], fjall, Request["ydOnBoardTime"]))
                {
                    Response.Write("{\"Success\":\"true\"}");
                }
                break;
            case "FjCreate":
                 // 生成一个房间list
                List<YDFJInfo> fjl = new List<YDFJInfo>();
                if (Request["fjlist"] != "")
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();

                    string temp = Request["fjlist"];
                    if (!string.IsNullOrEmpty(temp))
                    {
                        temp = temp.Trim();
                        string[] array = temp.Split(';');
                        foreach (var a in array)
                        {
                            fjl.Add(jss.Deserialize<YDFJInfo>(a));
                        }
                    }
                }

                if ((new YDHelper()).CreateYdFjInfo(Request["ydid"], Request["ydnum"], fjl))
                {
                    Response.Write("{\"Success\":\"" + Request["ydid"] + "\"}");
               
                }
                break;
            case "GetYdFjInfo":
              DateTime stime=  Request["starttime"] != null ? Convert.ToDateTime(Request["starttime"]) : new DateTime(2000,1,1);
              DateTime etime = Request["etime"] != null ? Convert.ToDateTime(Request["etime"]) : new DateTime(2000, 1, 1);

   Response.Write(Utils.DataContractJsonSerialize((new YDHelper()).GetYdFjInfo(stime, etime)));
                break;
            case "CreateOrders":
                createYdOrder();
                break;
            case "getOrders":
                Int32 page = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);

                List<YDOrder> inordereModelLst = (new YDHelper()).ReadOutOrder(page, rows, Request["ydid"]);
                Response.Write(Utils.DataContractJsonSerialize(inordereModelLst)); 
              
                break;
            case "AddOrder":
                YDHelper helper = new YDHelper();
                if (helper.CreateOrder(Request["StartTime"], Request["EndTime"], Request["ydid"]) == "0")
                {
                    Response.Write("{\"Success\":\"true\"}");
                }
                break;
            case"DelOrder":
                 YDHelper help = new YDHelper();
                if (help.DeleteOrder(Request["ydnum"]) == "0")
                 {
                     Response.Write("{\"Success\":\"true\"}");
                 }
                break;
            case "DelOrderFJ":
                 YDHelper help1 = new YDHelper();
                if (help1.DeleteOrderFJ (Convert.ToInt32( Request["AutoID"])) == "0")
                 {
                     Response.Write("{\"Success\":\"true\"}");
                 }
                break;
            case "OrderCreate":

                YDOrder order = new YDOrder();
                order.OnBoardTime = Convert.ToDateTime(Request.Form["OnBoardTime"] != "" ? Request.Form["OnBoardTime"] : "01/01/2001 00:00");
                order.LeaveTime = Convert.ToDateTime(Request.Form["LeaveTime"] != "" ? Request.Form["LeaveTime"] : "01/01/2001 00:00");
                order.Customer = Request.Form["Customer"];
                order.CustomerTel = Request.Form["CustomerTel"];
                order.Yder = Request.Form["Yder"];
                order.YdTel = Request.Form["YdTel"];
                order.MemberCardNo = Request.Form["MemberCardNo"];
                //客户类型
                order.LX = Request.Form["lx"];
                //国籍 
                order.GJ = Request.Form["gj"];
                //客户人数 
                order.RS = Convert.ToInt16(Request.Form["rs"] != "" ? Request.Form["rs"] : "0");
                //协议单位 
                order.Company = Request.Form["company"];
                //折扣率 
                order.ZKL = Convert.ToDouble(Request.Form["zkl"] != "" ? Request.Form["zkl"] : "0");
                //预定单位 
                order.D_DW = Request.Form["dw"];
                //付款方式
                order.D_FKFS = Request.Form["fkfs"];
                //预订金 
                order.D_DJ = Convert.ToDecimal(Request.Form["dj"] != "" ? Request.Form["dj"] : "0");
                //预订日期 
                //order.D_RQ = Convert.ToDateTime(Request.Form["rq"] != "" ? Request.Form["rq"] : "01/01/2001 00:00");
                //操作员 
                order.S_CZ = Request.Form["cz"];
                //销售员 
                order.Saler = Request.Form["saler"];
                //预订方式  
                order.YDWay = Request.Form["ydway"];
                //备注
                order.D_BZ = Request.Form["bz"];
                YDHelper helper2 = new YDHelper();
                if (helper2.CreateYDOrder(order) == "-1")//helper.CreateOrder(Request["StartTime"], Request["EndTime"]) 
                {
                    Response.Write(order.YDID.ToString()); 
                }
                break;
            case "YdCustomerInfoCreate":

                YDOrder order2 = new YDOrder();
                order2.OnBoardTime = Convert.ToDateTime(Request.Form["OnBoardTime"] != "" ? Request.Form["OnBoardTime"] : "01/01/2001 00:00");
                order2.LeaveTime = Convert.ToDateTime(Request.Form["LeaveTime"] != "" ? Request.Form["LeaveTime"] : "01/01/2001 00:00");
                order2.Customer = Request.Form["Customer"];
                order2.CustomerTel = Request.Form["CustomerTel"];
                order2.Yder = Request.Form["Yder"];
                order2.YdTel = Request.Form["YdTel"];
                order2.MemberCardNo = Request.Form["MemberCardNo"];
                //客户类型
                order2.LX = Request.Form["lx"];
                //国籍 
                order2.GJ = Request.Form["gj"];
                //客户人数 
                order2.RS = Convert.ToInt16(Request.Form["rs"] != "" ? Request.Form["rs"] : "0");
                //协议单位 
                order2.Company = Request.Form["company"];
                //折扣率 
                order2.ZKL = Convert.ToDouble(Request.Form["zkl"] != "" ? Request.Form["zkl"] : "0");
                //预定单位 
                order2.D_DW = Request.Form["dw"];
                //付款方式
                order2.D_FKFS = Request.Form["fkfs"];
                //预订金 
                order2.D_DJ = Convert.ToDecimal(Request.Form["dj"] != "" ? Request.Form["dj"] : "0");
                //预订日期 
                //order.D_RQ = Convert.ToDateTime(Request.Form["rq"] != "" ? Request.Form["rq"] : "01/01/2001 00:00");
                //操作员 
                order2.S_CZ = Request.Form["cz"];
                //销售员 
                order2.Saler = Request.Form["saler"];
                //预订方式  
                order2.YDWay = Request.Form["ydway"];
                //备注
                order2.D_BZ = Request.Form["bz"];
                YDHelper helper3 = new YDHelper();
                if (helper3.CreateYDOrder(order2) != "-1")//helper.CreateOrder(Request["StartTime"], Request["EndTime"]) 
                {
                        Response.Write(order2.YDID.ToString());
                }
                break;   
        }
    }
    private void createYdOrder()
    {
        YDOrder order = new YDOrder();
        order.OnBoardTime = Convert.ToDateTime(Request.Form["OnBoardTime"] != "" ? Request.Form["OnBoardTime"] : "01/01/2001 00:00");
        order.LeaveTime = Convert.ToDateTime(Request.Form["LeaveTime"] != "" ? Request.Form["LeaveTime"] : "01/01/2001 00:00");
        order.Customer = Request.Form["Customer"];
        order.CustomerTel = Request.Form["CustomerTel"];
        order.Yder = Request.Form["Yder"];
        order.YdTel = Request.Form["YdTel"];
        order.MemberCardNo = Request.Form["MemberCardNo"];
        //客户类型
        order.LX = Request.Form["lx"];
        //国籍 
        order.GJ = Request.Form["gj"];
        //客户人数 
        order.RS = Convert.ToInt16(Request.Form["rs"] != "" ? Request.Form["rs"] : "0");
        //协议单位 
        order.Company = Request.Form["company"];
        //折扣率 
        order.ZKL = Convert.ToDouble(Request.Form["zkl"] != "" ? Request.Form["zkl"] : "0");
        //预定单位 
        order.D_DW = Request.Form["dw"];
        //付款方式
        order.D_FKFS = Request.Form["fkfs"];
        //预订金 
        order.D_DJ = Convert.ToDecimal(Request.Form["dj"] != "" ? Request.Form["dj"] : "0");
        //预订日期 
        //order.D_RQ = Convert.ToDateTime(Request.Form["rq"] != "" ? Request.Form["rq"] : "01/01/2001 00:00");
        //操作员 
        order.S_CZ = Request.Form["cz"];
        //销售员 
        order.Saler = Request.Form["saler"];
        //预订方式  
        order.YDWay = Request.Form["ydway"];
        //备注
        order.D_BZ = Request.Form["bz"];

        //FJ Summer
        List<YDFJSummer> summerlist = new List<YDFJSummer>();

        if (!string.IsNullOrEmpty(Request["lists"]))
        {
            var lists = Request["lists"].Split(':');
            //summer
            foreach (var list in lists)
            {
                YDFJSummer summer = new YDFJSummer();
                summer.JB = list.Split('-')[0];
                summer.SL = Convert.ToInt16(list.Split('-')[1]);
                summerlist.Add(summer);
            }
        }
        //FJ Detail
        List<YDDetail> detaillist = new List<YDDetail>();
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
        }
        YDHelper helper = new YDHelper();
        if (helper.CreateYDOrderAndSummer(order, summerlist, detaillist) == "0")
        {
            Response.Write("{\"Success\":\"true\"}");
        }

    }
}