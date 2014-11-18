<%@ WebHandler Language="C#" Class="AgreeCompany" %>

using System;
using System.Web;
using HotelLogic.Cash;

public class AgreeCompany : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string action = context.Request["action"];
        switch (action)
        {
            case "read":
                context.Response.Write(Utils.ToRecordJson(AgreeCompanyHelper.Instance.GetAllCompany()));
                break;
            default:
                break;
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
}