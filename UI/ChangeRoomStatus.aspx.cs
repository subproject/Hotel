using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.FrontDesk;

public partial class ChangeRoomStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            RoomStatus helper = new RoomStatus();
            string fh = Request["fh"];
            string status = Request["status"];

            if (!string.IsNullOrEmpty(Request["action"]))//入住了
            {
                string action = Request["action"];
                if (action == "ruzhu")
                {
                    helper.SetRoomStatus(fh, "脏住");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fh) && !string.IsNullOrEmpty(status))
                {
                    helper.SetRoomStatus(fh, status);
                }
            }
            Response.Redirect("Default.aspx");
        }
    }
}