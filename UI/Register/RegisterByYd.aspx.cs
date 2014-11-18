using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;
using HotelLogic.FrontDesk;

public partial class RegisterByYd : System.Web.UI.Page
{
    public string fh = string.Empty;
    public string jb = string.Empty;
    public string dj = string.Empty;
    public string dr = string.Empty;
    public string lr = string.Empty;
    public string wakeuptime = string.Empty;
    public string name = string.Empty;
    public string price = string.Empty;
    public string ZKL = string.Empty;
    public string fjrows =null;
    public string fjsuike = null;
    public string fjorder= null;
    public string idzhengjian = string.Empty;
    public string orderid = null;
    public string ydnumyd = string.Empty; 
    protected void Page_Load(object sender, EventArgs e)
    {
        //预定转入住
        string ydnum = Request["ydnum"];
        if (!string.IsNullOrEmpty(ydnum))
        {
            ydnumyd = ydnum;
            HotelLogic.FrontDesk.YDOrder ydorder = (new HotelLogic.FrontDesk.YDHelper()).GetYdInfo(ydnum);
            dr = Convert.ToDateTime(ydorder.OnBoardTime).GetDateTimeFormats('g')[0].ToString();
            lr = Convert.ToDateTime(ydorder.LeaveTime).ToShortDateString();
            fh = Request.QueryString["fh"] != null ? Request.QueryString["fh"] : "0000";
            //有房间信息传到该页，则在datagrid上绑定该房间信息


            jb = ydorder.FangjianLeixing;
            dj = ydorder.BiaoZhunFangJia.ToString();
            dj = dj.Replace(".0000", ".00");
            price = ydorder.ShiJiFangJia.ToString().Replace(".0000", ".00");
            ZKL = ydorder.ZKL.ToString();
            name = ydorder.CustomerZhu;
            fjorder = Utils.ToRecordJson(getInfo(ydorder));
            fjrows = Utils.ToRecordJson(getInfoRows(ydorder));
           
           
            wakeuptime = DateTime.Today.AddDays(1).ToShortDateString() + " 06:00";

         
        }       
    }

    private List<OrderAttachModel> getInfoRows(HotelLogic.FrontDesk.YDOrder order)
    {
        List<OrderAttachModel> lorm = new List<OrderAttachModel>();
        foreach (YDFJSummer att in order.RecordList)
        {
            OrderAttachModel attach = new OrderAttachModel();
            attach.FangJianHao = att.f_dm;
           
            //attach.OrderID = att.OrderID;
            attach.ShijiFangjia = att.ShiJiFangJia;
            //attach.XingBie = att.XingBie;
            attach.XingMing = att.CustomerName;
            attach.YuanFangJia = att.BiaoZhunFangJia;
            //attach.ZhuCong = att.ZhuCong;
            attach.ZheKouLv = Convert.ToSingle(order.ZKL);
            //attach.ZhengjianDizhi = att.ZhengjianDizhi;
            //attach.ZhengjianHaoma = att.ZhengjianHaoma;
            //attach.ZhengjianLeixing = att.ZhengjianLeixing;
            attach.ArriveTime = Convert.ToDateTime(att.OnBoardTime);
            attach.LeaveTime = Convert.ToDateTime(att.LeaveTime);
            attach.JBName = att.RoomType;
            // attach.Status = att.Status;
            //attach.RuZhuLeiXing = att.RuZhuLeiXing;

            lorm.Add(attach);
        }
        return lorm;
    }
    private OrderViewModel getInfo(HotelLogic.FrontDesk.YDOrder order)
    {
        OrderViewModel temp = new OrderViewModel();
       
        temp.DaodianTime = Convert.ToDateTime(order.OnBoardTime);
        temp.LidianTime = Convert.ToDateTime(order.LeaveTime);

        temp.YaJin = order.D_DJ;
        temp.XieyiDanwei = order.D_DW;
        temp.TeQuanRen = order.TQR;
        temp.ZheKouLv = order.ZKL;
        temp.ShijiFangjia = order.ShiJiFangJia;
        temp.BeiZhu = order.D_BZ;
       
      
        temp.KerenLeibie = order.LX;
        temp.TuanDui = order.D_DW;

        temp.DianHua = order.YdTel;
        temp.FangjianLeixing = order.FangjianLeixing;// order.FangjianLeixing;
        //temp.XingBie = order.XingBie;
        temp.HuiYuanKa = order.MemberCardNo;
        //temp.JiFen = order.JiFen;
        temp.XingMing = order.Customer;
        //temp.ZhengJianHao = order.ZhengJianHao;
        //temp.DiZhi = order.DiZhi;
        temp.BiaozhunFangjia = order.BiaoZhunFangJia.ToString();// order.BiaozhunFangjia;
        //temp.OrderGuid = order.OrderGuid;
        temp.FangHao = order.FangHao;// order.FangHao;
        //temp.Status = order.Status;
        temp.FukuanFangshi = order.D_FKFS;
        temp.GuoJi = order.GJ;
        temp.XiaoShouYuan = order.Saler;
        temp.CaoZuoYuan = order.S_CZ;
        return temp;
    }
}