using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace HotelLogic
{
    public class HotelPage:Page
    {
        protected override void OnInit(System.EventArgs e)
        {
            if (Convert.ToInt32(DateTime.Now.Year) > 2014)
            {
                Response.Write("系统已过期，无法使用，请联系QQ：1509590896");
            }
            else
            {
                base.OnInit(e);
            }
        }
    }
}
