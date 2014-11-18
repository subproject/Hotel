<%@ WebHandler Language="C#" Class="OrderJieZhangHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using HotelLogic.FrontDesk;
using System.Web.SessionState;

public class OrderJieZhangHalder : IHttpHandler, IReadOnlySessionState
{
    OrdersJieZhangHelper RJZhelper = OrdersJieZhangHelper.Instance;
    public void ProcessRequest(HttpContext context) {       
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        RJZhelper.UserName = (string)context.Session["user"];
        switch (actionvalue)
        {
            case "SelOrderFeiYongList":
                actionresult = GetAllFeiYongList(context.Request.Form);
                break;
            case "SelZhuanZhangFeiYongList":
                actionresult = GetZhuanZhangFeiYongList(context.Request.Form);
                break;
            case "SelJieZhangFeiYongList":
                actionresult = GetJieZhangFeiYongList(context.Request.Form);
                break;
            case "SelShouKuanList":
                actionresult = GetShouKuanList(context.Request.Form);
                break;
            case "SelComplexOrderInfo":
                actionresult = GetComplexOrderInfo(context.Request.Form);
                break;
            case "SelSigeFeiYongInfo":
                actionresult = GetSigeFeiYongEntity(context.Request.Form);
                break;
                
                
        }
        context.Response.Write(actionresult);
        context.Response.End();
    }
    public string GetAllFeiYongList(NameValueCollection parameters)
    {
        return RJZhelper.GetAllOrderFeiYongInfo(parameters);
    }
    public string GetSigeFeiYongEntity(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(RJZhelper.GetRiZhuFeiYongEntity(parameters),1);
    }
    public string GetZhuanZhangFeiYongList(NameValueCollection parameters)
    {
        return RJZhelper.GetZhuanZhangOrderFeiYongInfo(parameters);
    }
    public string GetJieZhangFeiYongList(NameValueCollection parameters)
    {
        return RJZhelper.GetJieZhangOrderFeiYongInfo(parameters);
    }
    public string GetShouKuanList(NameValueCollection parameters)
    {
        return RJZhelper.GetShouTuKuanInfo(parameters);
    }
    public string GetComplexOrderInfo(NameValueCollection parameters)
    {
        return  Utils.ToRecordJson(RJZhelper.GetOrderComplexInfo(parameters));
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}