using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Register;
using System.Text.RegularExpressions;

public partial class FrontDesk_TuiFangChaXunData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        CustomerHelper helper = new CustomerHelper();
        //封装filter data
        CustomerFilter filter = new CustomerFilter();
        filter.Name = Request.Form["Name"];
        filter.Fh=Request.Form["Fh"];
        filter.Begin = Convert.ToDateTime(Request.Form["Begin"] != "" ? Request.Form["Begin"] : "01/01/2001 00:00");
        filter.End = Convert.ToDateTime(Request.Form["End"] != "" ? Request.Form["End"] : "01/01/2001 00:00");

        filter.FangType = Request.Form["FangType"];
        filter.dianhuahaoma = Request.Form["dianhuahaoma"];
        filter.beizhu = Request.Form["beizhu"];
        filter.ZhengjianCard = Request.Form["ZhengjianCard"];
        filter.kerenleibie = Request.Form["KeLeiCombo"];
        filter.fangjianjibie = Request.Form["fangjianjibie"];
        filter.ZhengjianAddress = Request.Form["ZhengjianAddress"];
        filter.dianhuahaoma = Request.Form["dianhuahaoma"];
        filter.guoji = Request.Form["GuoJiCombo"];

        filter.xiaoshouyuan = Request.Form["xiaoshouyuan"];
        filter.MainNumber = Request.Form["MainNumber"];
        filter.FukuanFangshi = Request.Form["FkfsCombo"];
        filter.huiyuanka = Request.Form["huiyuanka"];
        filter.xieyidanwei = Request.Form["xieyidanwei"];

        filter.caozuoyuan = Request.Form["caozuoyuan"];
        filter.chepaihao = Request.Form["chepaihao"];
        filter.MainName = Request.Form["MainName"];
        filter.RuZhuleixing = Request.Form["RuZhuleixing"];
        //filter.YuanFangFree = Convert.ToDecimal(Request.Form["YuanFangFree"]!=""?Request.Form["YuanFangFree"] :"0");
        //filter.ZheKou = Convert.ToDouble(Request.Form["ZheKou"] != "" ? Request.Form["ZheKou"] : "0");
        //filter.ShiJiFree = Convert.ToDecimal(Request.Form["ShiJiFree"] != "" ? Request.Form["ShiJiFree"] : "0");
        

        //分页数据读取
        Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
        Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "15");
        List<Customer> resultall = helper.ReadAll();

        List<Customer> resultcurrent = null;
        //if (filter.FangType== "1")
        //{
        //    resultcurrent = helper.ReadAllCustomer(page, rows, filter);
        //}
        //else if (filter.FangType == "2")
        //{
        //    resultcurrent = helper.ReadPageTuiFang(page, rows, filter);
        //}
        //else if (filter.FangType == "3")
        //{
        //    //未结离店
        //}
        //else
        //{
            resultcurrent = helper.ReadPageAll(page, rows, filter);
       // }
        string str = Regex.Replace(Utils.ToRecordJson(resultcurrent, resultall.Count), @"\\/Date\((\d+)\)\\/", match =>
        {
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
            dt = dt.ToLocalTime();
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        });

       
        Response.Write(str);
     
    }
}