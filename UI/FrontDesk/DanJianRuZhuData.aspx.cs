using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class FrontDesk_DanJianRuZhuData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        OrdersHelper helper = new OrdersHelper();

        #region 客户前端传来的值 生成一个ordermodel
        OrderViewModel model = new OrderViewModel();
        model.AutoID = Request.Form["AutoID"] != "" ? Request.Form["AutoID"] : "";
        model.BaoMi = Request.Form["BaoMi"] != "on" ? false : true; 
        //Request.Form["BaoMi"] != "" ? Request.Form["BaoMi"] : "";
        model.BeiZhu = Request.Form["BeiZhu"] != "" ? Request.Form["BeiZhu"] : "";
        model.BiaozhunFangjia = Request.Form["BiaozhunFangjia"] != "" ? Request.Form["BiaozhunFangjia"] : "";
        model.ChangBao = model.BaoMi = Request.Form["ChangBao"] != "on" ? false : true; 
        //Request.Form["ChangBao"] != "" ? Request.Form["ChangBao"] : "";
        model.ChangTu = Request.Form["ChangTu"] != "on" ? false : true;
        model.DaodianTime = Request.Form["DaodianTime"] != "" ? Convert.ToDateTime(Request.Form["DaodianTime"]) : new DateTime(1900,1,1);
        model.DianHua = Request.Form["DianHua"] != "" ? Request.Form["DianHua"] : "";
        model.DiZhi = Request.Form["DiZhi"] != "" ? Request.Form["DiZhi"] : "";
        model.FangjianLeixing = Request.Form["FangjianLeixing"] != "" ? Request.Form["FangjianLeixing"] : "";
        model.FukuanFangshi = Request.Form["FukuanFangshi"] != "" ? Request.Form["FukuanFangshi"] : "";
        model.HuiYuanKa = Request.Form["HuiYuanKa"] != "" ? Request.Form["HuiYuanKa"] : "";
        //model.JiaoxingFuwu = Request.Form["fkfs"] != "" ? Request.Form["fkfs"] : "";
        model.JiFen = Convert.ToInt32(Request.Form["JiFen"] != "" ? Request.Form["JiFen"] : "0");
        model.KerenLeibie = Request.Form["KerenLeibie"] != "" ? Request.Form["KerenLeibie"] : "";
        model.LidianTime = Request.Form["LidianTime"] != "" ? Convert.ToDateTime(Request.Form["LidianTime"]) : new DateTime(1900,1,1);
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
        model.ZhongDian = model.ChangBao = model.BaoMi = Request.Form["ZhongDian"] != "on" ? false : true; 
        //Request.Form["ZhongDian"] != "" ? Request.Form["ZhongDian"] : "";
        model.FangHao = Request.Form["FH"];
        //一个attach信息
        List<OrderAttachModel> attaches = new List<OrderAttachModel>();
        OrderAttachModel attach = new OrderAttachModel();
        attach.FangJianHao = Request.Form["FH"];
        attach.XingBie = Request.Form["XingBie"];
        attach.XingMing = Request.Form["XingMing"];
        attaches.Add(attach);
        #endregion
        switch (action)
        {
            case "create":
                result = helper.CreateOrder(model,attaches);
                if (result == "0")
                {
                    result = "{\"Success\":\"true\"}";
                }
                break;
            case "read":

                break;
            case "update":

                break;
            case "delete":

                break;
            default:
                break;
        }


        //output json result
        Response.Write(result);
    }
}