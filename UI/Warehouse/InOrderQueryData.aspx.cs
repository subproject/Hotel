using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class InOrderQueryData : System.Web.UI.Page
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
                SupplierQueryHelper supplierHelper = new SupplierQueryHelper();
                List<SupplierQueryViewModel> resultCardNo = supplierHelper.ReadSupplier(starttime, enddate);

                Response.Write(Utils.ToRecordJson(resultCardNo, resultCardNo.Count));
                break;

            case "querydetails":
                if (Request["id"] != "")
                {
                    SupplierQueryHelper supplierHelper2 = new SupplierQueryHelper();
                    List<SupplierQueryDetailViewModel> resultCardNo2 = supplierHelper2.ReadSupplierDetails(Request["id"]);

                    Response.Write(Utils.ToRecordJson(resultCardNo2, resultCardNo2.Count));
                }
                    
                break;
            case "queryMemberConut":
               
                

              // Response.Write("{\"membercount\":" + MemberHelper.GetMemberCount() + "}");

                
                break;
            case "queryMemberAll":

 
               // Response.Write(Utils.ToRecordJson( MemberHelper.ReadAll()));


                break;
              
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}