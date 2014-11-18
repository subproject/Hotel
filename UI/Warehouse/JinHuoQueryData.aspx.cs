using HotelLogic.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Warehouse_JinHuoQueryData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        string rlt = string.Empty;
        InOrderHelper inOrderHelper;
        switch (action)
        {
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
            case "getOrders":
                Int32 page = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);
                inOrderHelper = new InOrderHelper();
                InOrderModel inOrderModel = new InOrderModel();
                List<InOrderModel> inordereModelLst = inOrderHelper.ReadInOrder(page, rows);
                result = Utils.DataContractJsonSerialize(inordereModelLst);
                break;
            case "saveOrder":
                string orderIdStr = Request.Form["id"];
                string orderNum = Request.Form["ordernum"];
                string supplier = Request.Form["supplier"];
                string date = Request.Form["date"];
                string goodsopsition = Request.Form["goodsposition"];
                string inserted = Request.Form["inserted"];
                string delete = Request.Form["delete"];
                string update = Request.Form["update"];
                int orderId = int.Parse(orderIdStr);
                List<InOrderRecordModelJson> inOrderRecordModelJsonLst = Utils.DataContractJsonDeserialize<List<InOrderRecordModelJson>>(inserted);
                InOrderModel model = new InOrderModel();
                model.ID = orderId;
                model.OrderNum = orderNum;
                model.Supplier = supplier;
                model.Position = goodsopsition;
                model.Date = DateTime.Parse(date);
                List<InOrderRecordModel> inOrderRecordModelLst = new List<InOrderRecordModel>();
                foreach (var item in inOrderRecordModelJsonLst)
                {
                    InOrderRecordModel detail = new InOrderRecordModel();
                    detail.OrderID = orderId;
                    detail.ProductId = item.ProductId;
                    detail.Counts = item.Counts;
                    detail.InPrice = item.InPrice;
                    detail.TotalPrice = item.TotalPrice;
                    detail.Used = item.Used;
                    inOrderRecordModelLst.Add(detail);
                }
                model.RecordList = inOrderRecordModelLst;
                inOrderHelper = new InOrderHelper();
                rlt = inOrderHelper.CreateOrderDetail(model);
                if (rlt == "0")
                {
                    //添加商品库存
                    List<GoodsStockViewModel> list = new List<GoodsStockViewModel>();
                    foreach (var orderItem in inOrderRecordModelLst)
                    {
                        for (int i = 0; i < orderItem.Counts; i++)
                        {
                            GoodsStockViewModel goodsStock = new GoodsStockViewModel();
                            goodsStock.OrderID = model.ID;
                            goodsStock.GoodsID = orderItem.ProductId;
                            goodsStock.SupplierID = int.Parse(supplier);
                            goodsStock.PositionID = int.Parse(goodsopsition);
                            list.Add(goodsStock);
                        }
                    }
                    rlt = new GoodsStockHelper().CreateGoods(list);
                    if (rlt == "0")
                    {
                        result = "{\"success\":\"true\", \"orderId\":\"" + model.ID + "\"}";
                    }
                    else
                    {
                        result = rlt;
                    }
                }
                else
                    result = rlt;
                break;
            case "getProduct":
                GoodsHelper helper = new GoodsHelper();
                List<GoodsViewModel> results = helper.ReadGoods();
                result = Utils.DataContractJsonSerialize(results);
                break;
            case "deleteOrder":
                orderIdStr = Request.Form["id"];
                orderId = int.Parse(orderIdStr);
                inOrderHelper = new InOrderHelper();
                rlt = inOrderHelper.DeleteInOrder(orderId);
                if (rlt == "0")
                {
                    rlt = new GoodsStockHelper().DeleteGoodsByOrderId(orderId);
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