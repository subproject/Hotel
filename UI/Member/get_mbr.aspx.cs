using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class Setting_get_mbr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //读取全部
        if (Request["readall"] == "true")
        {
            List<MemberViewModel> resultall = MemberHelper.ReadAll();
            Response.Write(Utils.ToRecordJson(resultall));
        }
        //按照类别读取
        if (!string.IsNullOrEmpty(Request["Mbr_No"]))
        {
            //List<MemberViewModel> resultall = MemberHelper.ReadPart(Request["Mbr_No"]);
            MemberViewModel result = new MemberViewModel();
            result.ID = Convert.ToInt32(Request["Mbr_No"]);
            Response.Write(Utils.ToRecordJson(result));
        }
        //分页数据读取,暂时非全部或者按照分类,即为分页读取
        if (string.IsNullOrEmpty(Request["Mbr_No"]) && string.IsNullOrEmpty(Request["readall"]))
        {
            Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
            Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
            List<MemberViewModel> resultall = MemberHelper.ReadAll();
            List<MemberViewModel> resultcurrent = MemberHelper.ReadPart(page, rows);
            Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
        }
    }
}