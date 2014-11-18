using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;
using System.Web.Script.Serialization;

public partial class Register_SaveRegisterPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        string guid = Request["guid"] != "" ? Request["guid"] : "00000000-0000-0000-0000-000000000000";

        OrdersHelper helper = new OrdersHelper();

        #region 客户前端传来的值 生成一个ordermodel
        OrderViewModel model = new OrderViewModel();
        if (guid != null)
        {
            model.OrderGuid = new Guid(guid);
        }
        model.AutoID = Request.Form["AutoID"] != "" ? Request.Form["AutoID"] : "";
        model.BaoMi = Request.Form["BaoMi"] != "on" ? false : true; 
        //Request.Form["BaoMi"] != "" ? Request.Form["BaoMi"] : "";
        model.BeiZhu = Request.Form["BeiZhu"] != "" ? Request.Form["BeiZhu"] : "";
        model.BiaozhunFangjia = Request.Form["BiaozhunFangjia"] != "" ? Request.Form["BiaozhunFangjia"] : "";
        model.ChangBao = Request.Form["ChangBao"] != "on" ? false : true; 
        //Request.Form["ChangBao"] != "" ? Request.Form["ChangBao"] : "";
        model.ChangTu = Request.Form["ChangTu"] != "on" ? false : true;
        model.DaodianTime = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["DaodianTime"]) ? Request.Form["DaodianTime"] : "01/01/2001 00:00");
        model.DianHua = Request.Form["DianHua"] != "" ? Request.Form["DianHua"] : "";
        model.DiZhi = Request.Form["DiZhi"] != "" ? Request.Form["DiZhi"] : "";
        model.FangjianLeixing = Request.Form["FangjianLeixing"] != "" ? Request.Form["FangjianLeixing"] : "";
        model.FukuanFangshi = Request.Form["FukuanFangshi"] != "" ? Request.Form["FukuanFangshi"] : "";
        model.HuiYuanKa = Request.Form["HuaiYuanKa"] != "" ? Request.Form["HuaiYuanKa"] : "";
        model.JiaoxingFuwu = Request.Form["JiaoxingFuwu"] != "on" ? false : true;
        model.JiFen = Convert.ToInt32(Request.Form["JiFen"] != "" ? Request.Form["JiFen"] : "0");
        model.KerenLeibie = Request.Form["KerenLeibie"] != "" ? Request.Form["KerenLeibie"] : "";
        model.LidianTime = Convert.ToDateTime(!string.IsNullOrEmpty(Request.Form["LidianTime"]) ? Request.Form["LidianTime"] : "01/01/2001 00:00");
        model.ShiHua = Request.Form["ShiHua"] != "on" ? false : true;
        model.ShijiFangjia = Convert.ToDecimal(Request.Form["ShijiFangjia"] != "" ? Request.Form["ShijiFangjia"] : "0");
        model.ShougongDanhao = Request.Form["ShougongDanhao"] != "" ? Request.Form["ShougongDanhao"] : "";
        model.TeQuanRen = Request.Form["TeQuanRen"] != "" ? Request.Form["TeQuanRen"] : "";
        model.TuanDui = Request.Form["TuanDui"] != "" ? Request.Form["TuanDui"] : "";
        model.XieyiDanwei = Request.Form["XieyiDanwei"] != "" ? Request.Form["XieyiDanwei"] : "";
        model.XingBie = Request.Form["XingBie"] != "" ? Request.Form["XingBie"] : "";
        model.XingMing = Request.Form["XingMing"] != "" ? Request.Form["XingMing"] : "";
        model.YaJin = Convert.ToDecimal(Request.Form["YaJin"] != "" ? Request.Form["YaJin"] : "0");
        model.ZheKouLv = Convert.ToDouble(Request.Form["ZheKouLv"] != "" ? Request.Form["ZheKouLv"] : "0");
        model.ZhengJianHao = Request.Form["ZhengJianHao"] != "" ? Request.Form["ZhengJianHao"] : "";
        model.ZhengjianLeibie = Request.Form["ZhengjianLeibie"] != "" ? Request.Form["ZhengjianLeibie"] : "";
        model.ZhongDian = Request.Form["ZhongDian"] != "on" ? false : true; 
        //Request.Form["ZhongDian"] != "" ? Request.Form["ZhongDian"] : "";
        model.GuoJi = Request.Form["GuoJi"] != "" ? Request.Form["GuoJi"] : "";
        model.XiaoShouYuan = Request.Form["XiaoShouYuan"] != "" ? Request.Form["XiaoShouYuan"] : "";
        #endregion

        #region 生成一个入住详细list
        List<OrderAttachModel> fjl = new List<OrderAttachModel>();
        if (Request["list"] != "")
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string temp = Request["list"];
            if (!string.IsNullOrEmpty(temp))
            {
                temp = temp.Trim();
                string[] array = temp.Split(';');
                foreach (var a in array)
                {
                    fjl.Add(jss.Deserialize<OrderAttachModel>(a));
                }
            }
        }
        #endregion

        #region 生成一个随客详细list
        List<SuikeModel> skl = new List<SuikeModel>();
        if (Request["sk"] != "")
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();

            string temp = Request["sk"];
            if (!string.IsNullOrEmpty(temp))
            {
                temp = temp.Trim();
                string[] array = temp.Split(';');
                foreach (var a in array)
                {
                    skl.Add(jss.Deserialize<SuikeModel>(a));
                }
            }
        }
        if (Session["user"]!=null)
        {
            model.CaoZuoYuan= Session["user"].ToString();
        }
        #endregion
        
        switch (action)
        {
            case "createYdRuzhu":
                result = helper.CreateYdRuzhuOrder(model, fjl, skl, Request["ydnum"]);
                break;
            case "create":

                result = helper.CreateOrder(model, fjl,skl);

                //if (result == "0")
                //    result = "{\"Success\":\"true\"}";
                break;
            case "append":
                result = helper.AppendRoom(new System.Guid(guid), fjl);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "modify":
                result = helper.ModifyOrder(model,fjl,skl);
                break;
            case "delete":

            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}