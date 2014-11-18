using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_SupplierMgrData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        SupplierHelper helper = new SupplierHelper();
        string rlt=string.Empty;
        SupplierViewModel temp = new SupplierViewModel();
        switch (action)
        {
            case "create":
                temp.SupplierName = Request.Form["SupplierName"] != "" ? Request.Form["SupplierName"] : "";
                temp.SupplierSimple = Request.Form["SupplierSimple"] != "" ? Request.Form["SupplierSimple"] : "";
                temp.Address = Request.Form["Address"] != "" ? Request.Form["Address"] : "";
                temp.PostCode = Request.Form["PostCode"] != "" ? Request.Form["PostCode"] : "";
                temp.Tel = Request.Form["Tel"] != "" ? Request.Form["Tel"] : "";
                 rlt = helper.CreateSupplier(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "read":
                Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<SupplierViewModel> resultall = helper.ReadSupplier();
                List<SupplierViewModel> resultcurrent = helper.ReadPartSupplier(page, rows);
                result = Utils.ToRecordJson(resultcurrent, resultall.Count);
                break;
            case "update":
                
                temp.ID = int.Parse(ID);
                temp.SupplierName = Request.Form["SupplierName"] != "" ? Request.Form["SupplierName"] : "";
                temp.SupplierSimple = Request.Form["SupplierSimple"] != "" ? Request.Form["SupplierSimple"] : "";
                temp.Address = Request.Form["Address"] != "" ? Request.Form["Address"] : "";
                temp.PostCode = Request.Form["PostCode"] != "" ? Request.Form["PostCode"] : "";
                temp.Tel = Request.Form["Tel"] != "" ? Request.Form["Tel"] : "";
                rlt = helper.UpdateSupplier(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "delete":
                rlt = helper.DeleteSupplier(int.Parse(ID));
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);

    }
}