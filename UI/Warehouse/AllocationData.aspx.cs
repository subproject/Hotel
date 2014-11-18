using HotelLogic.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Warehouse_AllocationData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        string action = Request["action"] != "" ? Request["action"] : "";
        string ID = Request["ID"] != "" ? Request["ID"] : "0";
        string rlt = string.Empty;
        switch (action)
        {
            //
            case "getDiaoBoOrders":
                Int32 page2 = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows2 = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);
                AllocationOrderHelper allocationOrderHelper3 = new AllocationOrderHelper();


                List<AllocationOrderModel> inordereModelLst2 = allocationOrderHelper3.ReadDiaoBoOrder(page2, rows2);
                result = Utils.DataContractJsonSerialize(inordereModelLst2);
                break;
            case "getOrders":
                Int32 page = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["page"]) ? "1" : Request.Form["page"]);
                Int32 rows = Convert.ToInt32(string.IsNullOrEmpty(Request.Form["rows"]) ? "10" : Request.Form["rows"]);
                AllocationOrderHelper allocationOrderHelper2 = new AllocationOrderHelper();


                List<AllocationOrderModel> inordereModelLst = allocationOrderHelper2.ReadOrder(page, rows);
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
            case "getAllocationType":
                AllocationOrderHelper allocationOrderHelper = new AllocationOrderHelper();
                List<AllocationTypeModel> modelLst = allocationOrderHelper.ReadAllocationType();
                kvLstPosition = new List<TextValue>();
                foreach (var i in modelLst)
                {
                    TextValue t = new TextValue
                    {
                        Value = i.ID.ToString(),
                        Text = i.TypeName
                    };
                    kvLstPosition.Add(t);
                }
                result = Utils.DataContractJsonSerialize(kvLstPosition);
                break;
            case "getOrderId":
                string orderTypeStr = Request.QueryString["ordertype"];
                int orderType = int.Parse(orderTypeStr);
                allocationOrderHelper = new AllocationOrderHelper();
                AllocationOrderModel outOrderModel = new AllocationOrderModel();
                outOrderModel.OrderType = orderType;
                allocationOrderHelper.CreateOrder(outOrderModel);
                result = "{id:" + outOrderModel.ID + ",ordernum:\"" + (outOrderModel.ID + 1).ToString("00000000") + "\"}";
                break;
            case "saveOrder":
                orderTypeStr = Request.Form["ordertype"];
                string orderIdStr = Request.Form["id"];
                string orderNum = Request.Form["ordernum"];
                string departmentStr = Request.Form["department"];
                string allocationTypeStr = Request.Form["allocationtype"];
                string date = Request.Form["date"];
                string goodsositionStr = Request.Form["goodsposition"];
                string targetPositionStr = Request.Form["targetgoodsposition"];
                string inserted = Request.Form["inserted"];
                string delete = Request.Form["delete"];
                string update = Request.Form["update"];
                orderType = int.Parse(orderTypeStr);
                int orderId = int.Parse(orderIdStr);
                int departmentId = orderType == 1 ? int.Parse(departmentStr) : 0;
                int allocationTypeId = orderType == 1 ? int.Parse(allocationTypeStr) : 0;
                int positionId = int.Parse(goodsositionStr);
                int targetPositionId = orderType != 1 ? int.Parse(targetPositionStr) : 0;
                List<CommonOrderRecordJsonModel> orderRecordJsonModelLst = Utils.DataContractJsonDeserialize<List<CommonOrderRecordJsonModel>>(inserted);
                AllocationOrderModel model = new AllocationOrderModel();
                model.ID = orderId;
                model.OrderNum = orderNum;
                model.DepartmentId = departmentId;
                model.AllocationTypeId = allocationTypeId;
                model.PositionId = positionId;
                model.TargetPositionId = targetPositionId;
                model.Date = DateTime.Parse(date);
                model.OrderType = orderType;
                List<AllocationOrderRecordModel> orderRecordModelLst = new List<AllocationOrderRecordModel>();
                foreach (var item in orderRecordJsonModelLst)
                {
                    AllocationOrderRecordModel detail = new AllocationOrderRecordModel();
                    detail.OrderID = orderId;
                    detail.ProductId = item.ProductId;
                    detail.Counts = item.Counts;
                    detail.InPrice = item.InPrice;
                    detail.TotalPrice = item.TotalPrice;
                    detail.Remark = item.Remark;
                    
                    orderRecordModelLst.Add(detail);
                }
                model.RecordList = orderRecordModelLst;
                allocationOrderHelper = new AllocationOrderHelper();
                rlt = allocationOrderHelper.CreateOrderDetail(model);
                if (rlt == "0")
                {
                    //处理商品库存
                    List<int> sectGoodsIds = new List<int>();
                    List<int> sectCounts = new List<int>();
                    foreach (var goodsItem in orderRecordModelLst)
                    {
                        sectGoodsIds.Add(goodsItem.ProductId);
                        sectCounts.Add(goodsItem.Counts == null ? 0 : (int)goodsItem.Counts);
                    }
                    List<GoodsStockViewModel> goodsStockViewModelLst = null;
                    if(orderType == 1)
                       goodsStockViewModelLst = new GoodsStockHelper().AllocationGoodsByPositionId(positionId, sectGoodsIds, sectCounts);
                    else{
                        goodsStockViewModelLst = new GoodsStockHelper().AllocationGoodsByPositionId(positionId, targetPositionId, sectGoodsIds, sectCounts);
                    }
                    rlt = Utils.DataContractJsonSerialize(goodsStockViewModelLst);
                    result = "{\"success\":\"true\", \"deletedLst\":" + rlt + "}";
                }
                else
                    result = rlt;
                break;
            case "getProduct":
                string supplierIdStr = Request.Form["supplier"];
                string positionIdStr = Request.Form["position"];
                //supplierId = int.Parse(supplierIdStr);
                positionId = int.Parse(positionIdStr);
                allocationOrderHelper = new AllocationOrderHelper();
                List<AllocationOrderGoodsModel> results = allocationOrderHelper.ReadGoodsByPositioin(positionId);
                result = Utils.DataContractJsonSerialize(results);
                break;
            case "deleteOrder":
                orderIdStr = Request.Form["id"];
                orderId = int.Parse(orderIdStr);
                 string deletedData = Request.Form["deleteddata"];
                List<GoodsStockViewModel> goodsStockLst = Utils.DataContractJsonDeserialize < List<GoodsStockViewModel>>(deletedData);
                allocationOrderHelper = new AllocationOrderHelper();
                rlt = allocationOrderHelper.DeleteOrder(orderId);
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