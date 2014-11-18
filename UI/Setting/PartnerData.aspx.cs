using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_PartnerData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"] != "" ? Request["action"] : "read";
        PartnerHelper _helper = new PartnerHelper();
        PartnerViewModel temp = new PartnerViewModel();
        string rlt = string.Empty;
        switch (action)
        {
            case "read":
                Response.Write(Utils.ToRecordJson(_helper.getPartners()));
                break;
            case "create":
                temp.Name = Request.Form["Name"];
                temp.Contact = Request.Form["Contact"];
                temp.Tel = Request.Form["Tel"];
                temp.Saler = Request.Form["Saler"];
                //是否挂账 是否返佣
                temp.IsFany = Request.Form["IsFany"] != "1" ? false : true;
                temp.IsGuzh = Request.Form["IsGuzh"] != "1" ? false : true;
                //返佣方式
                temp.FanyWay = Request.Form["FanyWay"]!=null? Convert.ToByte(Request.Form["FanyWay"]):Convert.ToByte(0);
                temp.GuazhLimit = Request.Form["GuazhLimit"]!=null?Convert.ToDecimal(Request.Form["GuazhLimit"]):Convert.ToDecimal(0);
                temp.GuazhSum = Request.Form["GuazhSum"]!=null?Convert.ToDecimal(Request.Form["GuazhSum"]):Convert.ToDecimal(0);
                temp.Detail = Request.Form["Detail"];
                rlt=_helper.addPartner(temp);
                if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;
            case "update":
                temp.ID = Request["ID"] != "" ? Convert.ToInt32(Request["ID"]) : 0;
                temp.Name = Request.Form["Name"];
                temp.Contact = Request.Form["Contact"];
                temp.Tel = Request.Form["Tel"];
                temp.Saler = Request.Form["Saler"];
                //是否挂账 是否返佣
                temp.IsFany = Request.Form["IsFany"] != "1" ? false : true;
                temp.IsGuzh = Request.Form["IsGuzh"] != "1" ? false : true;
                //返佣方式
                temp.FanyWay = Request.Form["FanyWay"]!=null? Convert.ToByte(Request.Form["FanyWay"]):Convert.ToByte(0);
                temp.GuazhLimit = Request.Form["GuazhLimit"]!=null?Convert.ToDecimal(Request.Form["GuazhLimit"]):Convert.ToDecimal(0);
                temp.GuazhSum = Request.Form["GuazhSum"]!=null?Convert.ToDecimal(Request.Form["GuazhSum"]):Convert.ToDecimal(0);
                temp.Detail = Request.Form["Detail"];
                rlt = _helper.updatePartner(temp);
                if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;
            case "delete":
               rlt = _helper.delPartner(Convert.ToInt32(Request["ID"]));
               if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;
            default:
                break;
        }
    }
}