using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class SaleProfitQueryData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        DateTime starttime = new DateTime();
        DateTime enddate = new DateTime();
        if (!string.IsNullOrEmpty(Request["starttime"]))
        {
            starttime = Convert.ToDateTime(Request["starttime"]);//"01/01/2001 00:00"
        }
        if (!string.IsNullOrEmpty(Request["endtime"]))
        {
            enddate = Convert.ToDateTime(Request["endtime"]);//"01/01/2001 00:00"
        }
        switch (action)
        {

            case "query":

                SaleProfitQueryHelper supplierHelper = new SaleProfitQueryHelper();
                
                List<SaleProfitQueryViewModel> resultCardNo = supplierHelper.query(starttime, enddate, Request["goodsid"], Request["goodsposition"], Request["goodsType"]);

                Response.Write(Utils.ToRecordJson(resultCardNo, resultCardNo.Count));
                break;
            case "readPosition":
                SaleProfitQueryHelper Helper = new SaleProfitQueryHelper();
                List<PositionViewModel> position = Helper.ReadPosition();

                Response.Write(Utils.ToRecordJson(position));
                break;
            case "querydetails":
                if (Request["id"] != "")
                {
                    SupplierQueryHelper supplierHelper2 = new SupplierQueryHelper();
                    List<SupplierQueryDetailViewModel> resultCardNo2 = supplierHelper2.ReadSupplierDetails(Request["id"]);

                    Response.Write(Utils.ToRecordJson(resultCardNo2, resultCardNo2.Count));
                }
                    
                break;
            case "readGoodsName":
                GoodsHelper Helper2 = new GoodsHelper();
                List<GoodsViewModel> PositionView = Helper2.ReadGoods();

                Response.Write(Utils.ToRecordJson(PositionView));
                

              // Response.Write("{\"membercount\":" + MemberHelper.GetMemberCount() + "}");

                
                break;
            case "readGoodsType":

 
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
}