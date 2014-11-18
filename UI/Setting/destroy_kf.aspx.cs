using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_destroy_kf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32 autoid;
        autoid = Convert.ToInt32(Request["ID"] != "" ? Request["ID"] : "0");
        string result = KFHelper.Delete(autoid);
        if (result == "0")
        {
            Response.Clear();
            Response.Write("{\"success\":true}");
            Response.End();
        }
        else
        {
            Response.Clear();
            Response.Write("{\"success\":false}");
            Response.End();
        }
    }
}