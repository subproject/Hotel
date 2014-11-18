using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Common_getKFCgy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
        List<KFCgyViewModule> resultall = KFCgyHelper.ReadAll();
        //添加一个全部选项，数据库中没有前台需要用到
        KFCgyViewModule all = new KFCgyViewModule();
        all.ID = 0;
        all.KFJB = "全部";
        resultall.Add(all);

        Response.Write(Utils.ToRecordJson(resultall));
    }
}