using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class MemberChargeQueryData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        DateTime starttime = new DateTime();
        DateTime enddate = new DateTime();
        if (!string.IsNullOrEmpty(Request["starttime"]))
        {
            starttime = Convert.ToDateTime(Request["starttime"]);//"01/01/2001 00:00"
        }
        if (!string.IsNullOrEmpty(Request["endtime"]))
        {
            enddate = Convert.ToDateTime(Request["endtime"]);//"01/01/2001 00:00"
        }
        switch (action)
        {

            case "query":
            
                
                
                //Int32 pageNo = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                //Int32 rowsNo = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10000");
                List<MemberChangeMoney> resultCardNo = MemberHelper.ReadMemberChangeMoneyAll(starttime, enddate);

                Response.Write(Utils.ToRecordJson(resultCardNo, resultCardNo.Count));
                break;

            case "queryConut":
                float changeMoney =0,Money=0;
                MemberHelper.GetMemberChangeMoneyCount(starttime, enddate,ref changeMoney,ref Money);

                Response.Write("{\"changeMoney\":" + changeMoney + ",\"Money\":" + Money + "}");

                
                break;
            case "queryMemberConut":
               
                

               Response.Write("{\"membercount\":" + MemberHelper.GetMemberCount() + "}");

                
                break;
            case "queryMemberAll":

 
                Response.Write(Utils.ToRecordJson( MemberHelper.ReadAll()));


                break;
              
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}