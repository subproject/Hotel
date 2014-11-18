using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;
using HotelLogic.FrontDesk;
using System.Collections.Specialized;
using HotelLogic.Cash.CashAction;

public partial class Cash_KeRenJieZhang : System.Web.UI.Page
{

    public HotelEntities.H_RuzhuOrder order = null;
    public HotelLogic.Cash.CashAction.OrderInfoMode orderinfo = null;
    public string rooms = "";
    public string xitongremark = "";
    public string FFStr = string.Empty;
    public string FYStr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string t =(string) Session["user"];
        //fh不为0000时进行处理
        string fh = Request["fh"] != "" ? Request["fh"] : "0000";
        string orderguid = Request["orderguid"] != "" ? Request["orderguid"] : "";
        if (fh != "0000")
        {
            //先通过房号得到orderguid，这guid是连接一切财务信息的主键
            OrdersHelper helper = new OrdersHelper();
            if (string.IsNullOrEmpty(orderguid))
            {
                order = helper.ReadOrderByFH(fh);
            }
            else
            {
                order = helper.ReadOrderByOrderGuid(orderguid);
            }
             JieZhangTuFangHelper jzhelper=HotelLogic.Cash.CashAction.JieZhangTuFangHelper.Instance;
             orderinfo = jzhelper.GetOrderInfo(order.OrderGuid.ToString());
             SanKeZhuanZhangHelper zzhelper = SanKeZhuanZhangHelper.Instance;
             var RM= zzhelper.GetOrderRoomList(order.OrderGuid.ToString());
             FaPiaoManagerHelper fphelper = FaPiaoManagerHelper.Instance;
             decimal? fpmoney=  fphelper.GetFaPiaoList(order.OrderGuid).Sum(a=>a.FP_Money);
             if (RM != null)
             {
                rooms= RM.FangJianHao;
             }
             if (fpmoney == null)
             {
                 xitongremark = "[已开发票:0元]";
             }
             else
             {
                 xitongremark = "[已开发票:" + string.Format("{0:N2}", fpmoney) + "元]";
             }

           
            if (order != null)
            {
                orderID.Value = order.OrderGuid.ToString();
                //计算房费
                //RoomFeeHelper.Instance.FFeeMaker(fh);
                helper.GetCashDetails(fh);
                //前台绑定的字符串                   
                RunningList builder = new RunningList();
                //得到房费的json
                 FFStr = Utils.ToRecordJson(builder.GetFFListByGuid(order.OrderGuid));
                //得到费用的json
                 FYStr = Utils.ToRecordJson(builder.GetFYListByGuid(order.OrderGuid));
            }
        }
    }


    protected void Finish_Click(object sender, EventArgs e)
    {
        RunningList builder = new RunningList();
        builder.FinishOrder(order.OrderGuid);
        Response.Write("<script type=\"text/javascript\">window.opener.location.reload();window.close();</script>");
    }
}