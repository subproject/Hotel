using HotelLogic.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PanDianLuRuData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        string rlt = string.Empty;
        PanDianHelper outOrderHelper;
        switch (action)
        {
            case "getOrders":
                Int32 page = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);
                outOrderHelper = new PanDianHelper();

                List<PanDianModel> inordereModelLst = outOrderHelper.ReadOutOrder(page, rows);
                result = Utils.DataContractJsonSerialize(inordereModelLst);
                break;

            case "getSupplier":
                SupplierHelper supplierHelper = new SupplierHelper();
                List<SupplierViewModel> resultall = supplierHelper.ReadSupplier();
                List<TextValue> kvLstSupplier = new List<TextValue>();
                foreach (var i in resultall)
                {
                    TextValue t = new TextValue
                    {
                        Value = i.ID.ToString(),
                        Text = i.SupplierName
                    };
                    kvLstSupplier.Add(t);
                }
                result = Utils.DataContractJsonSerialize(kvLstSupplier);
                break;
            case "getPosition":
                PositionHelper positionHelper = new PositionHelper();
                List<PositionViewModel> positionAll = positionHelper.ReadPosition();
                List<TextValue> kvLstPosition = new List<TextValue>();
                foreach (var i in positionAll)
                {
                    TextValue t = new TextValue
                    {
                        Value = i.ID.ToString(),
                        Text = i.LocName
                    };
                    kvLstPosition.Add(t);
                }
                result = Utils.DataContractJsonSerialize(kvLstPosition);
                break;
            case "getOrderId":
                outOrderHelper = new PanDianHelper();
                PanDianModel outOrderModel = new PanDianModel();
                outOrderHelper.CreateOutOrder(outOrderModel);
                result = "{id:" + outOrderModel.ID + ",ordernum:\"" + (outOrderModel.ID + 1).ToString("00000000") + "\"}";
                break;
            case "saveOrder":
                string orderIdStr = Request.Form["id"];
                string orderNum = Request.Form["ordernum"];
            
                string date = Request.Form["date"];
                string goodsopsition = Request.Form["goodsposition"];
                string inserted = Request.Form["inserted"];
                string delete = Request.Form["delete"];
                string update = Request.Form["update"];
                string goodspositionName = Request.Form["positionName"];
                int orderId = int.Parse(orderIdStr);
                //int supplierId = int.Parse(supplierStr);
                int positionId = int.Parse(goodsopsition);
                List<PanDianJsonModel> inOrderRecordModelJsonLst = Utils.DataContractJsonDeserialize<List<PanDianJsonModel>>(inserted);
                PanDianModel model = new PanDianModel();
                model.ID = orderId;
                //model.OrderNum = orderNum;
                
                model.positionid = Convert.ToInt32(goodsopsition);
                model.date = DateTime.Parse(date);
                model.luruDate = DateTime.Parse(date);
                model.positionName = goodspositionName;
                model.money = 0;
                List<PanDianItemModel> inOrderRecordModelLst = new List<PanDianItemModel>();
                foreach (var item in inOrderRecordModelJsonLst)
                {
                    PanDianItemModel detail = new PanDianItemModel();
                    detail.pandianId = orderId;
                    detail.realCount = item.RealCount;
                    detail.price = item.InPrice;
                    detail.goodid = item.ProductId;
                    detail.beizhu = item.Beizhu;
                    detail.zhangmianCount = item.Counts;
                    model.money += (item.RealCount - item.Counts) * item.InPrice;
                    inOrderRecordModelLst.Add(detail);
                }
                model.RecordList = inOrderRecordModelLst;
                outOrderHelper = new PanDianHelper();
                rlt = outOrderHelper.CreateOrderDetail(model);
                if (rlt == "0")
                {
                    ////删除商品库存
                    //List<int> sectGoodsIds = new List<int>();
                    //List<int> sectCounts = new List<int>();
                    //foreach (var goodsItem in inOrderRecordModelLst)
                    //{
                    //    sectGoodsIds.Add(goodsItem.ProductId);
                    //    sectCounts.Add(goodsItem.Counts == null ? 0 : (int)goodsItem.Counts);
                    //}
                    //List<GoodsStockViewModel> goodsStockViewModelLst = new GoodsStockHelper().DeleteGoodsBySupplierAndPosition(supplierId, positionId, sectGoodsIds, sectCounts);
                    //rlt = Utils.DataContractJsonSerialize(goodsStockViewModelLst);
                    //result = "{\"success\":\"true\", \"deletedLst\":" + rlt + "}";
                    result = "0";
                }
                else
                    result = rlt;
               
                break;
            case "getProduct":
                //string supplierIdStr = Request.Form["supplier"];
                string positionIdStr = Request.Form["position"];
                //supplierId = int.Parse(supplierIdStr);
                positionId = int.Parse(positionIdStr);
                OutOrderHelper helper = new OutOrderHelper();
                List<OutOrderGoodsModel> results = helper.ReadGoodsByPositioin( positionId);
                result = Utils.DataContractJsonSerialize(results);
                break;
            case "deleteOrder":
                orderIdStr = Request.Form["id"];
                orderId = int.Parse(orderIdStr);
                string deletedData = Request.Form["deleteddata"];
               // List<GoodsStockViewModel> goodsStockLst = Utils.DataContractJsonDeserialize < List<GoodsStockViewModel>>(deletedData);
                outOrderHelper = new PanDianHelper();
                rlt = outOrderHelper.DeleteOutOrder(orderId);
                if (rlt == "0")
                {
                    //rlt = new GoodsStockHelper().UpdateGoods(goodsStockLst);
                    //if (rlt == "0")
                    //{
                        result = "{\"success\":\"true\"}";
                    //}
                    //else
                    //{
                    //    result = rlt;
                    //}
                }
                else
                {
                    result = rlt;
                }
                break;
        }
        Response.Write(result);
    }
}