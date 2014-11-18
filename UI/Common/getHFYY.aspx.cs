using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Common_getHFYY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<HFViewModel> resultall = HFHelper.ReadAll();

        Response.Write(Utils.ToRecordJson(resultall));



    }
}