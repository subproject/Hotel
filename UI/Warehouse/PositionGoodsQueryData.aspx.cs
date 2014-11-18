using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_PositionGoodsQueryData : System.Web.UI.Page
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
            case "getPosition":
                PositionHelper positionHelper = new PositionHelper();
                List<PositionViewModel> positionAll = positionHelper.ReadPosition();
                 List<Tree> treeLst = new List<Tree>();
                 foreach (var g in positionAll)
                {
                    Tree t = new Tree
                    {
                        Id = g.ID,
                        Text = g.LocName
                    };
                    treeLst.Add(t);
                }
                result = "[{\"id\":0,\"text\": \"总库\",\"state\": \"opened\",\"children\": "+Utils.DataContractJsonSerialize(treeLst) + "}]";
                break;
            case "getPositionGoods":
                string positionIdStr = Request.QueryString["id"];
                int positionId = 0;
                int.TryParse(positionIdStr, out positionId);
                positionHelper = new PositionHelper();
                List<SupplierGoodsViewModel> supplierGoodsViewModel = positionHelper.ReadPartPositionGoods(positionId);
                result = Utils.DataContractJsonSerialize(supplierGoodsViewModel);
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}