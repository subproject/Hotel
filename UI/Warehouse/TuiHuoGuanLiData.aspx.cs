using HotelLogic.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Warehouse_TuiHuoGuanLiData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        string rlt = string.Empty;
        OutOrderHelper outOrderHelper;
        switch (action)
        {
            case "getOrders":
                Int32 page = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);
                outOrderHelper = new OutOrderHelper();

                List<OutOrderModel> inordereModelLst = outOrderHelper.ReadOutOrder( page,rows);
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
                outOrderHelper = new OutOrderHelper();
                OutOrderModel outOrderModel = new OutOrderModel();
                outOrderHelper.CreateOutOrder(outOrderModel);
                result = "{id:" + outOrderModel.ID + ",ordernum:\"" + (outOrderModel.ID + 1).ToString("00000000") + "\"}";
                break;
            case "saveOrder":
                string orderIdStr = Request.Form["id"];
                string orderNum = Request.Form["ordernum"];
                string supplierStr = Request.Form["supplier"];
                string date = Request.Form["date"];
                string goodsopsition = Request.Form["goodsposition"];
                string inserted = Request.Form["inserted"];
                string delete = Request.Form["delete"];
                string update = Request.Form["update"];
                int orderId = int.Parse(orderIdStr);
                int supplierId = int.Parse(supplierStr);
                int positionId = int.Parse(goodsopsition);
                List<CommonOrderRecordJsonModel> inOrderRecordModelJsonLst = Utils.DataContractJsonDeserialize<List<CommonOrderRecordJsonModel>>(inserted);
                OutOrderModel model = new OutOrderModel();
                model.ID = orderId;
                model.OrderNum = orderNum;
                model.Supplier = supplierStr;
                model.Position = goodsopsition;
                model.Date = DateTime.Parse(date);
                List<OutOrderRecordModel> inOrderRecordModelLst = new List<OutOrderRecordModel>();
                foreach (var item in inOrderRecordModelJsonLst)
                {
                    OutOrderRecordModel detail = new OutOrderRecordModel();
                    detail.OrderID = orderId;
                    detail.ProductId = item.ProductId;
                    detail.Counts = item.Counts;
                    detail.InPrice = item.InPrice;
                    detail.TotalPrice = item.TotalPrice;
                    detail.Remark = item.Remark;
                    inOrderRecordModelLst.Add(detail);
                }
                model.RecordList = inOrderRecordModelLst;
                outOrderHelper = new OutOrderHelper();
                rlt = outOrderHelper.CreateOrderDetail(model);
                if (rlt == "0")
                {
                    //删除商品库存
                    List<int> sectGoodsIds = new List<int>();
                    List<int> sectCounts = new List<int>();
                    foreach (var goodsItem in inOrderRecordModelLst)
                    {
                        sectGoodsIds.Add(goodsItem.ProductId);
                        sectCounts.Add(goodsItem.Counts == null ? 0 : (int)goodsItem.Counts);
                    }
                    List<GoodsStockViewModel> goodsStockViewModelLst = new GoodsStockHelper().DeleteGoodsBySupplierAndPosition(supplierId, positionId, sectGoodsIds, sectCounts);
                    rlt = Utils.DataContractJsonSerialize(goodsStockViewModelLst);
                    result = "{\"success\":\"true\", \"deletedLst\":" + rlt + "}";
                }
                else
                    result = rlt;
                break;
            case "getProduct":
                string supplierIdStr = Request.Form["supplier"];
                string positionIdStr = Request.Form["position"];
                supplierId = int.Parse(supplierIdStr);
                positionId = int.Parse(positionIdStr);
                OutOrderHelper helper = new OutOrderHelper();
                List<OutOrderGoodsModel> results = helper.ReadGoodsBySupplierAndPositioin(supplierId, positionId);
                result = Utils.DataContractJsonSerialize(results);
                break;
            case "deleteOrder":
                orderIdStr = Request.Form["id"];
                orderId = int.Parse(orderIdStr);
                string deletedData = Request.Form["deleteddata"];
                List<GoodsStockViewModel> goodsStockLst = Utils.DataContractJsonDeserialize < List<GoodsStockViewModel>>(deletedData);
                outOrderHelper = new OutOrderHelper();
                rlt = outOrderHelper.DeleteOutOrder(orderId);
                if (rlt == "0")
                {
                    rlt = new GoodsStockHelper().UpdateGoods(goodsStockLst);
                    if (rlt == "0")
                    {
                        result = "{\"success\":\"true\"}";
                    }
                    else
                    {
                        result = rlt;
                    }
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