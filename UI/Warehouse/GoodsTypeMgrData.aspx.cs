using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_GoodsTypeMgrData : System.Web.UI.Page
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "";
        GoodsTypeHelper helper = new GoodsTypeHelper();
        GoodsTypeViewModel temp = new GoodsTypeViewModel();
        string rlt = string.Empty;
        switch (action)
        {
            case "create":
                temp.TypeName = Request.Form["TypeName"] != "" ? Request.Form["TypeName"] : "";
                temp.PareID =( Request.Form["TypePareID"] != "" ? int.Parse(Request.Form["TypePareID"].ToString()) :0);
                rlt = helper.CreateGoodsType(temp);
                if (rlt == "0")
                {
                    string typeid= helper.SelectGoodsType(temp.TypeName);
                    result = "{\"Success\":\"true\",\"typeid\":\""+typeid+"\"}";
                }
                else
                    result = rlt;
                break;
            case "read":
                Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<GoodsTypeViewModel> resultall = helper.ReadGoodsType();
                List<GoodsTypeViewModel> resultcurrent = helper.ReadPartGoodsType(page, rows);
                string resuttxt = ToTreeDataGridJson(resultall, helper);
               // Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
                Response.Write(resuttxt); 
               break;
            case "comboxread":
               Int32 combopage = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
               Int32 comborows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
               List<GoodsTypeViewModel> comboresultall = helper.ReadGoodsType();
               List<GoodsTypeViewModel> comboresultcurrent = helper.ReadPartGoodsType(combopage, comborows);
               string comboresuttxt =  ToComboxTreeDataGridJson(comboresultall, helper);
               // Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
               Response.Write(comboresuttxt);
               break;
            case "update":
                temp.ID = int.Parse(ID);
                temp.TypeName = Request.Form["TypeName"] != "" ? Request.Form["TypeName"] : "";
                rlt = helper.UpdateGoodsType(temp);
                if (rlt == "0")
                    result = "{\"Success\":\"true\",\"typeid\":\"" + temp.ID.ToString() + "\"}";
                else
                    result = rlt;
                break;
            case "delete":
                rlt=helper.DeleteGoodsType(int.Parse(ID));
                if (rlt == "0")
                    result = "{\"Success\":\"true\"}";
                else
                    result = rlt;
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }

    /// <summary>
    /// 转成树型表格的Json格式
    /// </summary>
    /// <returns></returns>
    private string ToTreeDataGridJson(List<GoodsTypeViewModel> alldata, GoodsTypeHelper helper)
    {
        List<GoodsTypeViewModel> TopLeveData = alldata.Where(n => n.PareID == null).ToList < GoodsTypeViewModel>();
        helper.AllData = alldata;
        string dataformat = "'id':'{0}','TypeName':'{1}', 'IsCheck':'{2}','state':'{3}','parentid':'{4}',";

        string valuetxt = @"{'Rows':[{ " + string.Format(dataformat,Guid.NewGuid().ToString(),"全部物品","","","") + " 'children':[";
        valuetxt += helper.ConverToTreeJson(TopLeveData, dataformat).ToString();
        valuetxt += "]}]}";

        return valuetxt;
      
    }
    /// <summary>
    /// 转成树型表格的Json格式
    /// </summary>
    /// <returns></returns>
    private string ToComboxTreeDataGridJson(List<GoodsTypeViewModel> alldata, GoodsTypeHelper helper)
    {
        List<GoodsTypeViewModel> TopLeveData = alldata.Where(n => n.PareID == null).ToList<GoodsTypeViewModel>();
        helper.AllData = alldata;
        string dataformat = "'id':'{0}','text':'{1}', 'IsCheck':'{2}','state':'{3}','parentid':'{4}',";

        string valuetxt = @"{'Rows':[{ " + string.Format(dataformat, Guid.NewGuid().ToString(), "全部物品", "", "", "") + " 'children':[";
        string xtx = helper.ConverToTreeJson(TopLeveData, dataformat).ToString();
        valuetxt += helper.ConverToTreeJson(TopLeveData, dataformat).ToString();
        valuetxt += "]}]}";

        return valuetxt;

    }
}