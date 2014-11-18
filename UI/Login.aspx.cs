using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;
using HotelEntities;
using HotelLogic.Cash;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LoginBtn_Click(object sender, EventArgs e)
    {
        string LoginName = this.UserName.Value;
        string password = this.Password.Value;
        if (LoginName != "")
        {
            LoginUserHepler helper = new LoginUserHepler();
            LoginUserModel user = new LoginUserModel();
            //helper.Shoukuanview("");
            user.LoginName = LoginName;
            user.Password = password;
            if (helper.Login(user) != null)
            {
                string hostname = System.Net.Dns.GetHostName();
                //主机
                //System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(hostname);
                ////网卡IP地址集合 
                //string IP = ipEntry.AddressList[0].ToString();//
                user.ComputerName = hostname;
                helper.Update(user);

                Session["user"] = this.UserName.Value.ToString();
                Response.Redirect("Default.aspx");

            }
            else
            {
                Response.Write("<script>alert('用户名或密码输入有误');</script>");
            }
        }
    }
}