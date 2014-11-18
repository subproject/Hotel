using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Register;

public partial class Register_MergeOrder : System.Web.UI.Page
{
    //将接收到的orders guid merge到一起
    protected void Page_Load(object sender, EventArgs e)
    {
        string guidstr = Request["guids"];
        if (!string.IsNullOrEmpty(guidstr))
        {
            guidstr = guidstr.Trim().Trim(';');
            List<string> guidliststr = guidstr.Split(';').ToList();
            List<Guid> guidlist = new List<Guid>();
            foreach (var guid in guidliststr)
            {
                guidlist.Add(new Guid(guid));
            }

            RegisterHelper helper = new RegisterHelper();
            if (helper.MergeOrder(guidlist) == "0")
            {
                Response.Write("{\"Success\":\"true\"}");
            }
        }
    }
}