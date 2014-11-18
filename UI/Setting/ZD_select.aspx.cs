using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelEntities;
using HotelLogic.Setting;


public partial class Setting_ZD_select : System.Web.UI.Page
{

    public string f_jb = string.Empty;
    public Int32 id = 0;

    public string FangAnName = string.Empty;
    public Int32? StartLen = 0;
    public decimal? StartFee = 0;
    public Int32? AddLen = 0;
    public decimal? AddFee = 0;
    public Int32? MinLen = 0;
    public decimal? MinFee = 0;
    public Int32? MaxLen = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
          f_jb = Request["f_jb"];
          //if (f_jb != "")
          //{
          //    List<ZDViewMedel> resultall = ZDHelper.ReadZDByJB(f_jb);
          //    Response.Write(Utils.ToRecordJson(resultall));

              //Response.Redirect("ZD_selectData.aspx?action=read&all= "+f_jb);
              //
              //foreach (var r in resultall)
              //{
              //    id = r.id;
              //    f_jb = r.f_jb;
              //    FangAnName = r.FangAnName;
              //    StartLen = r.StartLen;
              //    StartFee = r.StartFee;
              //   AddLen = r.AddLen;
              //    AddFee = r.AddFee;
              //    MinLen = r.MinLen;
              //    MinFee = r.MinFee;
              //    MaxLen = r.MaxLen;
               
              //}

          //}
    }
}