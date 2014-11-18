using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Text.RegularExpressions;

public partial class FrontDesk_YudingChaxunData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string action = Request["action"];
        YDHelper helper = new YDHelper();

        #region  筛选条件
        YDFilter filter = new YDFilter();
        filter.YDNum=Request.Form["YDNum"];
        filter.Yder = Request.Form["Yder"];
        filter.YdTel = Request.Form["YdTel"];
        filter.Customer = Request.Form["Customer"];
        filter.LX = Request.Form["LX"];
        filter.DR = Convert.ToDateTime(Request.Form["DR"] != "" ? Request.Form["DR"] : "01/01/2001 00:00");
        filter.LR = Convert.ToDateTime(Request.Form["LR"] != "" ? Request.Form["LR"] : "01/01/2001 00:00");
        filter.YDR = Convert.ToDateTime(Request.Form["YDR"] != "" ? Request.Form["YDR"] : "01/01/2001 00:00");
        filter.FH=Request.Form["FH"];
        filter.D_DW=Request.Form["DW"];
        filter.GJ=Request.Form["GJ"];
        filter.Company=Request.Form["Company"];
        filter.MemberCardNo=Request.Form["MemberCardNo"];
        filter.YDLB=Request.Form["YDLB"];
      
        #endregion
        if (action == "read")
        {
            string str = Regex.Replace(Utils.ToRecordJson(helper.ReadYDOrderByFilter(filter)), @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });
            Response.Write(str);
        }
    }
}