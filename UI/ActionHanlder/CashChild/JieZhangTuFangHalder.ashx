<%@ WebHandler Language="C#" Class="JieZhangTuFangHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class JieZhangTuFangHalder : IHttpHandler, IReadOnlySessionState 
{
    JieZhangTuFangHelper ZFHelper = JieZhangTuFangHelper.Instance;
    public void ProcessRequest(HttpContext context) {       
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        ZFHelper.UserName =(string) context.Session["user"];
        switch (actionvalue)
        {
            case "AddORUpdateMainEntity":
                actionresult = AddOrUpdateMainEntity(context.Request.Form);
                break;
            case "AddORUpdate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "Upddate":
                actionresult = AddOrUpdate(context.Request.Form);
                break;
            case "Delete":
                actionresult = Delete(context.Request.Form);
                break;
            case "DeleteMainEntity":
                actionresult = DeleteMainEntity(context.Request.Form);
                break;
            case "CanceMainEntity":
                actionresult = CanceMainEntity(context.Request.Form);
                break;
            case "SelByMainGuid":
                actionresult = GetDataListByMainGuid(context.Request.Form);
                break;
            case "SelMainEntityByOrderGuid":
                actionresult = GetMainEntryByOrderGuid(context.Request.Form);
                break;
            case "SelPrivileged":
                actionresult = GetMainEntryByOrderGuid(context.Request.Form);
                break;
            case "SelJZFeiYongList":
                actionresult = GetJZFeiYongListByOrderGuid(context.Request.Form);
                break;
            case "SelJZList":
                actionresult = GetJZListByOrderGuid(context.Request.Form);
                break;
            case "SelRoomList":
                actionresult = GetRoomistByOrderGuid(context.Request.Form);
                break;
            case "BuJieZhangTuiFan":
                actionresult = BuJieZhangTuiFan(context.Request.Form);
                break;
            case "SelIsOrderGuidJieZhang":
                actionresult = IsOrderGuidJieZhang(context.Request.Form);
                break;
        }
        context.Response.Write(actionresult);
        context.Response.End();
    }
    public string AddOrUpdateMainEntity(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(ZFHelper.AddOrUpdateMaiJieZhange(parameters));
    }
    public string DeleteMainEntity(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(ZFHelper.DeleteMaiJieZhange(parameters));
    }

    public string CanceMainEntity(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(ZFHelper.CancelMaiJieZhange(parameters));
    }
    public string GetMainEntryByOrderGuid(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZFHelper.GetMainEntiry(parameters));
        return result;
    }
    public string AddOrUpdate(NameValueCollection parameters)
    {
        return Utils.ToRecordJson(ZFHelper.AddOrUpdatePayDetails(parameters));        
    }
    public string Delete(NameValueCollection parameters)
    {
        return  Utils.ToRecordJson(ZFHelper.DeletePayDetails(parameters));        
    }
    public string GetDataListByMainGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetPayDetailsList(parameters,out total),total);        
        return result;
    }

    public string GetJZFeiYongListByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetJZFeiYongList(parameters, out total), total);
        return result;
    }
    public string GetJZListByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetJZTuFanList(parameters, out total), total);
        return result;
    }
    public string GetRoomistByOrderGuid(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZFHelper.GetOrderRooms(parameters, out total), total);
        return result;
    }

    public string BuJieZhangTuiFan(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZFHelper.BuiJieZhangTuiFan(parameters));
        return result;
    }
    public string IsOrderGuidJieZhang(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZFHelper.IsOrderJieZhang(parameters));
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