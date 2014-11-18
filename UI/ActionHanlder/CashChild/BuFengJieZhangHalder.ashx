<%@ WebHandler Language="C#" Class="BuFengJieZhangHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class BuFengJieZhangHalder : IHttpHandler, IReadOnlySessionState 
{
    BuFengJieZhangHelper ZFHelper = BuFengJieZhangHelper.Instance;
    public void ProcessRequest(HttpContext context) {       
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        ZFHelper.UserName =(string) context.Session["user"];
        switch (actionvalue)
        {
            case "AddORUpdate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "SelByRZFeiYongList":
                actionresult = GetDataListByMainGuid(context.Request.Form);
                break;
            case "SelByJieZhangList":
                actionresult = GetJieZhangList(context.Request.Form);
                break;
           
                
        }
        context.Response.Write(actionresult);
        context.Response.End();
    }
    public string AddOrUpdate(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(ZFHelper.AddOrUpdate(parameters));        
    }
    public string GetDataListByMainGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetRiZhuFeiYongList(parameters,out total),total);        
        return result;
    }
    public string GetJieZhangList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetJieZhangList(parameters, out total), total);
        return result;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}