using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Cash;

public partial class FrontDesk_XuZhu : System.Web.UI.Page
{
    public XuZhuModel data = new XuZhuModel();
    protected void Page_Load(object sender, EventArgs e)
    {
        //绑定现住客信息到页面
        string fh = Request["fh"];
        if (fh != null)
        {
            data = XuZhuHelper.Instance.GetInitByFH(fh);
        }

    }

}