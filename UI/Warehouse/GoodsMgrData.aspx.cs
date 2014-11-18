using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_GoodsMgrData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        GoodsHelper helper = new GoodsHelper();
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        int typeId = string.IsNullOrEmpty(Request["typeId"]) ? -1 : int.Parse(Request["typeId"]);
        //used for temp, init from page data, create update 
        GoodsViewModel temp = new GoodsViewModel();
        string rlt = string.Empty;
        switch (action)
        {
            case "create":
                temp.GoodsName = Request.Form["GoodsName"] != "" ? Request.Form["GoodsName"] : "未定义";
                //中文简写
                temp.GoodsSimple = GetPYString(temp.GoodsName);
                temp.GoodsType = Request.Form["GoodsTypeid"] != "" ? int.Parse(Request.Form["GoodsTypeid"]) : -1;
                temp.GoodsStyle = Request.Form["GoodsStyle"];
                temp.IsStoreManage = Convert.ToBoolean(Request.Form["IsStoreManage"] != "" ? Request.Form["IsStoreManage"] : "False");
                temp.SalePrice = Convert.ToDecimal(Request.Form["SalePrice"] != "" ? Request.Form["SalePrice"] : "0");
                temp.Unit = Request.Form["Unit"].ToString();
                rlt = helper.CreateGoods(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "read":
                Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<GoodsViewModel> resultall = helper.ReadGoods();
                List<GoodsViewModel> resultcurrent = helper.ReadPartGoods(typeId, page, rows);
                Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
                break;
            case "update":
                temp.ID = int.Parse(ID);
                temp.GoodsName = Request.Form["GoodsName"] != "" ? Request.Form["GoodsName"] : "未定义";
                temp.GoodsSimple = GetPYString(temp.GoodsName);

                temp.GoodsType = Request.Form["GoodsType"] != "" ? int.Parse(Request.Form["GoodsType"]) : -1;
                temp.GoodsStyle = Request.Form["GoodsStyle"];
                temp.IsStoreManage = Convert.ToBoolean(Request.Form["IsStoreManage"]);
                temp.SalePrice = Convert.ToDecimal(Request.Form["SalePrice"].ToString() != "" ? Request.Form["SalePrice"].ToString() : "0");
                temp.Unit = Request.Form["Unit"].ToString() != "" ? Request.Form["Unit"].ToString() : "";
                rlt = helper.UpdateGoods(temp);
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "delete":
                rlt=helper.DeleteGoods(int.Parse(ID));
                if (rlt == "0")
                    result = "{\"success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "getTree":
                GoodsTypeHelper goodTypeHelper = new GoodsTypeHelper();
                List<GoodsTypeViewModel> goodTypeLst = goodTypeHelper.ReadGoodsType();
                List<Tree> treeLst = new List<Tree>();
                foreach (var g in goodTypeLst)
                {
                    Tree t = new Tree
                    {
                        Id = g.ID,
                        Text = g.TypeName
                    };
                    treeLst.Add(t);
                }
                goodTypeLst = null;
                result = Utils.DataContractJsonSerialize(treeLst);
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }

    /// <summary>  
    /// 汉字转拼音缩写  
    /// </summary>  
    /// <param name="str">要转换的汉字字符串</param>  
    /// <returns>拼音缩写</returns>  
    public string GetPYString(string str)
    {
        str = str.Replace(" ", "");
        string tempStr = "";
        foreach (char c in str)
        {
            if ((int)c >= 33 && (int)c <= 126)
            {//字母和符号原样保留  
                tempStr += c.ToString();
            }
            else
            {//累加拼音声母  
                tempStr += GetPYChar(c.ToString());
            }
        }
        return tempStr;
    }
    /// <summary>  
    /// 取单个字符的拼音声母  
    /// Code By   
    /// 2004-11-30  
    /// </summary>  
    /// <param name="c">要转换的单个汉字</param>  
    /// <returns>拼音声母</returns>  
    public string GetPYChar(string c)
    {
        byte[] array = new byte[2];
        array = System.Text.Encoding.Default.GetBytes(c);
        int i = (short)(array[0] - '\0') * 256 + ((short)(array[1] - '\0'));
        if (i < 0xB0A1) return "*";
        if (i < 0xB0C5) return "a";
        if (i < 0xB2C1) return "b";
        if (i < 0xB4EE) return "c";
        if (i < 0xB6EA) return "d";
        if (i < 0xB7A2) return "e";
        if (i < 0xB8C1) return "f";
        if (i < 0xB9FE) return "g";
        if (i < 0xBBF7) return "h";
        if (i < 0xBFA6) return "j";
        if (i < 0xC0AC) return "k";
        if (i < 0xC2E8) return "l";
        if (i < 0xC4C3) return "m";
        if (i < 0xC5B6) return "n";
        if (i < 0xC5BE) return "o";
        if (i < 0xC6DA) return "p";
        if (i < 0xC8BB) return "q";
        if (i < 0xC8F6) return "r";
        if (i < 0xCBFA) return "s";
        if (i < 0xCDDA) return "t";
        if (i < 0xCEF4) return "w";
        if (i < 0xD1B9) return "x";
        if (i < 0xD4D1) return "y";
        if (i < 0xD7FA) return "z";
        return "*";
    }  
}