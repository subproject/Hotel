<%@ WebHandler Language="C#" Class="SanKenZhuanZhangHalder" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using HotelLogic;
using HotelLogic.Cash.CashAction;
using System.Web.SessionState;

public class SanKenZhuanZhangHalder : IHttpHandler, IReadOnlySessionState 
{
    SanKeZhuanZhangHelper ZZHelper = SanKeZhuanZhangHelper.Instance;
    public void ProcessRequest(HttpContext context)
    {
        string actionvalue = context.Request.Form["ActionName"];
        string actionresult = "";
        ZZHelper.UserName = (string)context.Session["user"];
        switch (actionvalue)
        {
            case "FeiYongZhuanChu":
                actionresult = FeiYongZhuangChuExcuce(context.Request.Form);
                break;
            case "ShouKuanZhuanChu":
                actionresult = ShouKuanZhuangChu(context.Request.Form);
                break;
            case "SelFeiyongList":
                actionresult = SelFeiYongList(context.Request.Form);
                break;
            case "SelShouKuanList":
                actionresult = SelShouKuanList(context.Request.Form);;
                break;
            case "SelZhuanRuFeiYongList":
                actionresult = SelZhuangRuFeiYong(context.Request.Form);
                break;
            case "SelZhuangChuFeiYongList":
                actionresult = SelZhuanChuFeiYong(context.Request.Form);
                break;
            case "SelOrderRoomList":
                actionresult = SelOrderRoomList(context.Request.Form);                
                break;

        }
        context.Response.Write(actionresult);
        context.Response.End();
    }
    /// <summary>
    /// 费用转出函数
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public string FeiYongZhuangChuExcuce(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZZHelper.FeiYongZhuanChuFunc(parameters));
        return result;
    }
    /// <summary>
    /// 收退款转出函数
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public string ShouKuanZhuangChu(NameValueCollection parameters)
    {
        string result = Utils.ToRecordJson(ZZHelper.ShouKuanZhangChu(parameters));
        return result;
    }

    public string SelFeiYongList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZZHelper.GetFeiYongByOrderGuid(parameters, out total), total);
        return result;
    }
    public string SelShouKuanList(NameValueCollection parameters)
    {
        int total = 0;
        NameValueCollection newparamets = new NameValueCollection();
        newparamets.Add("SK_OrderGUID", parameters["RZ_OrderGuid"]);
        newparamets.Add("PageInfo", parameters["PageInfo"]);
        string result = Utils.ToRecordJson(ZZHelper.GetShouKuanByOrderGuid(newparamets, out total), total);
        return result;
    }
    public string SelZhuanChuFeiYong(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZZHelper.GetZhuangChuFeiYongByOrderGuid(parameters, out total), total);
        return result;
    }
    public string SelZhuangRuFeiYong(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZZHelper.GetZhuangRuFeiYongByOrderGuid(parameters, out total), total);
        return result;
    }
    public string SelOrderRoomList(NameValueCollection parameters)
    {
        int total = 0;
        string result = Utils.ToRecordJson(ZZHelper.GetOrderRoomList(parameters, out total), total);
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