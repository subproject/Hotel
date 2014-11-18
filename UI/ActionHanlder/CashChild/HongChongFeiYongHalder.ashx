<%@ WebHandler Language="C#" Class="HongChongFeiYongHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class HongChongFeiYongHalder : IHttpHandler, IReadOnlySessionState 
{
    HongChongFeiYongHelper HCHelper = HongChongFeiYongHelper.Instance;
    public void ProcessRequest(HttpContext context)
    {
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        HCHelper.UserName = (string)context.Session["user"];
        switch (actionvalue)
        {
            case "AddORUpdate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "SelByOrderGuid":
                actionresult = GetDataListByOrderGuid(context.Request.Form);
                break;

        }
        context.Response.Write(actionresult);
        context.Response.End();
    }

    public string AddOrUpdate(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(HCHelper.AddOrUpdate(parameters));
    }
    public string Delete(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(HCHelper.Delete(parameters));

    }
    public string GetDataListByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(HCHelper.GetHongChongFeiYongList(parameters, out total), total);
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