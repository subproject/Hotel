using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class Setting_save_mbr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
   

        MemberViewModel _mbr = new MemberViewModel();
        _mbr.MemberName = Request.Form["Mbr_Name"] != "" ? Request.Form["Mbr_Name"] : "";
        _mbr.Status = Convert.ToByte(Request.Form["Mbr_Status"] != "" ? Request.Form["Mbr_Status"] : "1");
        _mbr.MemberCardNo = Request.Form["CardID"] != "" ? Request.Form["CardID"] : "";
        _mbr.CardType = Request.Form["CardType"] != "" ? Request.Form["CardType"] : "";
        _mbr.RestScore = Convert.ToInt32(Request.Form["JF"] != "" ? Request.Form["JF"] : "");
        _mbr.IdCard = Request.Form["Mbr_ID"] != "" ? Request.Form["Mbr_ID"] : "";
        _mbr.Sex = Request.Form["sex"] != "" ? Request.Form["sex"] : "";
        _mbr.BirthDay = Request.Form["birtaday"] != "" ? Request.Form["birtaday"] : "";
        _mbr.HomeTelphone = Request.Form["HomePhone"] != "" ? Request.Form["HomePhone"] : "";
        _mbr.Mobile = Request.Form["mobile"] != "" ? Request.Form["mobile"] : "";
        _mbr.Address = Request.Form["address"] != "" ? Request.Form["address"] : "";
        _mbr.Validate =  ( Request.Form["validateM"] != "" ? Request.Form["validateM"] : "");
        _mbr.Address = (Request.Form["pwd"] != "" && Request.Form["pwd"] == Request.Form["pwd1"]) ? Request.Form["pwd"] : "";

       
        MemberHelper.Create(_mbr);        
        Response.Clear();
        Response.Write("{\"Success\":\"true\"}");
        Response.End();
    }
}