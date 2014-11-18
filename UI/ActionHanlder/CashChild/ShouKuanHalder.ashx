<%@ WebHandler Language="C#" Class="ShouKuanHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class ShouKuanHalder : IHttpHandler, IReadOnlySessionState 
{
    ShoukuanHelper KFHelper = ShoukuanHelper.Instance;
    public void ProcessRequest(HttpContext context) {       
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        KFHelper.UserName =(string) context.Session["user"];
        switch (actionvalue)
        { 
            case "AddORUpdate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "Upddate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "Delete":
                actionresult = Delete(context.Request.Form);
                break;
            case "SelByOrderGuid":
                actionresult = GetDataListByOrderGuid(context.Request.Form);
                break;
            case "SelEntiryByID":
                actionresult = GetSigeEntiryByID(context.Request.Form);
                break;
            case "SelPayType":
                actionresult = GetPayType(context.Request.Form);
                break;
            case "SelOrderInfo":
                actionresult = SelOrderInfoList(context.Request.Form);
                break;
                
        }
        context.Response.Write(actionresult);
        context.Response.End();
    }

    public string AddOrUpdate(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(KFHelper.AddOrUpdate(parameters));        
    }
    public string Delete(NameValueCollection parameters)
    {
        return  Utils.ToRecordJson(KFHelper.Delete(parameters));        
    }
    public string GetDataListByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(KFHelper.GetKaFanFeiYongList(parameters,out total),total);        
        return result;
    }
    public string GetSigeEntiryByID(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(KFHelper.GetEntityForEditor(parameters));
        return result;
    }
    public string GetPayType(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(KFHelper.GetPayType());
        return result;
    }
    public string SelOrderInfoList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(KFHelper.GetOderInfoList(parameters, out total), total);
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