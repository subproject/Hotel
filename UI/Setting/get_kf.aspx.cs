using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;

public partial class Setting_get_kf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //读取全部
        if (Request["readall"] == "true")
        {
            List<KFViewModule> resultall = KFHelper.ReadAll();
            Response.Write(Utils.ToRecordJson(resultall));
        }
        //读取空房信息
        if (Request["readE"] == "true")
        {
            List<KFViewModule> result = KFHelper.ReadEmpty();
            Response.Write(Utils.ToRecordJson(result));
        }
        //特定时间内的可预订房间
        if (Request["readcanyd"] == "true")
        {
            DateTime from = Convert.ToDateTime(!string.IsNullOrEmpty(Request["begin"]) ? Request["begin"] : System.DateTime.Now.ToShortDateString());
            DateTime to = Convert.ToDateTime(!string.IsNullOrEmpty(Request["end"]) ? Request["end"] : System.DateTime.Now.AddDays(1).ToShortDateString());
            int lcid =Convert.ToInt32( Request["lcid"] != "" ? Request["lcid"] : "0");
            int ldid=Convert.ToInt32(Request["ldid"] != "" ? Request["ldid"] : "0");
            //如果id为0，则是读取全部房间
            List<KFViewModule> resultall = KFHelper.ReadCanYD(Request["fjtype"], Request["cgyid"], from, to, lcid, ldid);
            Response.Write(Utils.ToRecordJson(resultall));
           
        }

        //按照类别读取
        if (!string.IsNullOrEmpty(Request["cgyid"]) && string.IsNullOrEmpty(Request["readcanyd"]))
        {
            //如果id为0，则是读取全部房间
            if (Request["cgyid"] == "0")
            {
                List<KFViewModule> resultall = KFHelper.ReadAll();
                Response.Write(Utils.ToRecordJson(resultall));
            }
            else
            {
                List<KFViewModule> resultall = KFHelper.ReadByKFCgy(Request["cgyid"]);
                Response.Write(Utils.DataContractJsonSerialize(resultall));// Utils.ToRecordJson
            }    
        }
        //分页数据读取,暂时非全部或者按照分类,即为分页读取
        if (string.IsNullOrEmpty(Request["cgyid"]) && string.IsNullOrEmpty(Request["readcanyd"]) && string.IsNullOrEmpty(Request["readall"]) && string.IsNullOrEmpty(Request["readE"]))
        {
            Int32 page = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["page"] )? Request.Form["page"] : "1");
            Int32 rows = Convert.ToInt32(!String.IsNullOrEmpty(Request.Form["rows"]) ? Request.Form["rows"] : "15");
            if (!string.IsNullOrEmpty(Request["type"]))
            {
                List<KFViewModule> resultall = KFHelper.ReadAll();
               // List<KFViewModule> resultcurrent = KFHelper.ReadPart(page, 1000000);
                Response.Write(Utils.ToRecordJson(resultall));
            }
            else
            {
                List<KFViewModule> resultall = KFHelper.ReadAll();
                List<KFViewModule> resultcurrent = KFHelper.ReadPart(page, rows);
                Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
            }
        }
    }
}