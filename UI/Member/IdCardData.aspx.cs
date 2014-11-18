using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class Member_IdCardData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;

        string IdCardNo = Request.QueryString["ID"];//"411524198511286832";// Request.Form["IdCard"].ToString();
                          //Request.Form["IdCard"].ToString();
        IDCardHelper IDChecked = new IDCardHelper(IdCardNo);
        CheckedResult res = IDChecked.CheckIdCard();
        result = "{\"address\":'" + res.Address + "',\"Exceptiion\":'" + res.ErrorMessage + "',\"validate\":'" + res.validate.ToString() + "',\"sex\":'" + res.sex + "'}";
        Response.Clear();
        Response.Write(result);
        Response.End();

    }
}