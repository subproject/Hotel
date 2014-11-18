using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;

public partial class FrontDesk_HuanFang : System.Web.UI.Page
{
    public HuanFangDan data = new HuanFangDan();
    public string HFNum = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //绑定现住客信息到页面
        string fh = Request["fh"];
        if (fh != null)
        {
            data = HuanfangHelper.Instance.GetInitByFH(fh);
        }
        DateTime now=System.DateTime.Now;
        HFNum = "HF" + now.Year.ToString() + now.Month.ToString() + now.Day.ToString()
            + now.Hour.ToString() + now.Minute.ToString() + now.Second.ToString();

    }
    protected void btnSelectHF_LY_Click(object sender, EventArgs e)
    {
        Response.Redirect("HuanFangLY.aspx");
    }
}