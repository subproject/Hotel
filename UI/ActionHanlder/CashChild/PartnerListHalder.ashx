<%@ WebHandler Language="C#" Class="PartnerListHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class PartnerListHalder : IHttpHandler, IReadOnlySessionState 
{
    PartnerListHelper ZFHelper = PartnerListHelper.Instance;
    public void ProcessRequest(HttpContext context)
    {
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
         ZFHelper.UserName =(string) context.Session["user"];
        switch (actionvalue)
        {
            case "CreateRelation":
                actionresult = CreateRelation(context.Request.Form);
                break;
            case "DeleteRelation":
                actionresult = DeleteRelation(context.Request.Form);
                break;
            case "SelByPartnerList":
                actionresult = GetPartnerList(context.Request.Form);
                break;
            case "SelByFeiYongList":
                actionresult = GetFeiYongList(context.Request.Form);
                break;
            case "SelHaveExitFeiYongList":
                actionresult = GetHaveExitFeiYongList(context.Request.Form);
                break;
            case "SelOrderRelationFeiYongList":
                actionresult = GetOrderRelationFeiYongList(context.Request.Form);
                break;

        }
        context.Response.Write(actionresult);
        context.Response.End();
    }

    public string GetFeiYongList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetFeiYongList(parameters, out total), total);
        return result;
    }
    public string GetPartnerList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetPartnerList(parameters, out total), total);
        return result;
    }
    public string CreateRelation(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZFHelper.CreateRelation(parameters));
        return result;
    }
    public string DeleteRelation(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZFHelper.DeleteRelation(parameters));
        return result;
    }
    public string GetHaveExitFeiYongList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetHaveExitFeiYongList(parameters, out total), total);
        return result;
    }
    public string GetOrderRelationFeiYongList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetOrderRelationFeiYongList(parameters, out total), total);
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