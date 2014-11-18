using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Text.RegularExpressions;

public partial class FrontDesk_YuDingGuanLiData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //read current yd order
        string action = Request["action"];
        string guid = Request["guid"];
        string ydnum = Request["ydnum"];
        YDHelper helper = new YDHelper();
        if (action == "read")
        {
            
            string str = Regex.Replace(Utils.ToRecordJson(helper.ReadTodayYDOrder()), @"\\/Date\((\d+)\)\\/", match =>
                {
                    DateTime dt = new DateTime(1970, 1, 1);
                    dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                    dt = dt.ToLocalTime();
                    return dt.ToString("yyyy-MM-dd HH:mm:ss");
                });
            Response.Write(str);
        }
        else if (action == "transfer")
        {
            if (helper.YDToRuzhuByGUID(ydnum) == "0")//new Guid(guid)
                Response.Write("{\"success\":\"true\"}");      
        }
    }
}