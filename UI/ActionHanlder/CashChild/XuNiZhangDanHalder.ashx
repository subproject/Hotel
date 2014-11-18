<%@ WebHandler Language="C#" Class="XuNiZhangDanHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class XuNiZhangDanHalder : IHttpHandler, IReadOnlySessionState 
{
    XuNiZhangDanHelper XNHelper = XuNiZhangDanHelper.Instance;
    public void ProcessRequest(HttpContext context) {       
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        XNHelper.UserName = (string)context.Session["user"];
        switch (actionvalue)
        { 
            case "AddORUpdate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "Delete":
                actionresult = Delete(context.Request.Form);
                break;
            case "SelByOrderGuid":
                actionresult = GetDataListByOrderGuid(context.Request.Form);
                break;
            case "SelNullEnitry":
                actionresult = GetNullEntity(context.Request.Form);
                break;
            case "SelByID":
                actionresult = GetXuNiZhangDanEntity(context.Request.Form);
                break;
                
        }
        context.Response.Write(actionresult);
        context.Response.End();
    }

    public string AddOrUpdate(NameValueCollection parameters)
    {

        return Utils.ToRecordJson(XNHelper.AddOrUpdate(parameters));        
    }
    public string Delete(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(XNHelper.Delete(parameters));
        
    }
    public string GetDataListByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(XNHelper.GetOrderSumJieZhangInfoList(parameters, out total), total);        
        return result;
    }
    public string GetNullEntity(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(XNHelper.GetNullEntityList(parameters, out total), total);
        return result;
    }
    public string GetXuNiZhangDanEntity(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(XNHelper.GetXuNiZhangDanEntity(parameters));
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