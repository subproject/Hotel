using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class MemberCardData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
       
        string action = Request["action"] != "" ? Request["action"] : "";
     
        switch (action)
        {

            case "newPoint":
                MemberPointDesignView filter = new MemberPointDesignView();
                filter.IsAlertPointToRoom = Request.Form["chkPromptFreeRoom"] != "on" ? false : true;
                filter.IsConsumePoint = Request.Form["chkMoneyToPoint"] != "on" ? false : true;
                filter.IsLiveDays = Request.Form["chkDayToPoint"] != "on" ? false : true;
                filter.IsPointToMoney = Request.Form["chkPointToMoney"] != "on" ? false : true;

                if (!string.IsNullOrEmpty(Request.Form["jfdkje"] ) )
                {
                    filter.PointToMoney = Convert.ToInt32(Request.Form["jfdkje"]);
                }
                if (!string.IsNullOrEmpty(Request.Form["xfjf"] ))
                {
                    filter.ByConsumePoint = Convert.ToInt32(Request.Form["xfjf"]);
                }
                if (!string.IsNullOrEmpty(Request.Form["rzts"] ))
                {
                    filter.ByLiveDays = Convert.ToInt32(Request.Form["rzts"]);
                }
                  MemberHelper.SavePointDesign(filter);
                result = "{\"Success\":\"true\"}";
                //Response.Write(result);
                break;
 
            case "queryMemberConut":
               
                

               Response.Write("{\"membercount\":" + MemberHelper.GetMemberCount() + "}");

                
                break;
            case "queryPointDesign":


                Response.Write(Utils.ToRecordJson(MemberHelper.ReadPointDesign()));


                break;
            case "GetPointToGift":
                Response.Write(Utils.ToRecordJson(MemberHelper.GetPointToGift()));
                break;
            case "PointToGiftCreate":
                PointToGiftView pgv = new PointToGiftView();
                pgv.liwu = Request.Form["liwu"];
                pgv.MustPoint = Convert.ToInt64(Request.Form["MustPoint"]);
                 string rlt2  =MemberHelper.CreatePointToGift(pgv);
                 if (rlt2 == "0")
                 {
                     Response.Clear();
                     Response.Write("{\"success\":true}");
                     Response.End();
                 }
                 else
                     result = rlt2;
                break;
                     
            case "PointToGiftUpdate":
                PointToGiftView temp = new PointToGiftView();
                Int32 UserID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");

                temp.liwu = Request.Form["liwu"];
                temp.MustPoint = Convert.ToInt64(Request.Form["MustPoint"]);
                temp.id =UserID;
                string rlt = MemberHelper.UpdatePointToGiftView(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;

            case "PointToGiftDelete":

             Int32 delID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                string rlt3  = MemberHelper.DeletePointToGift(delID);
                if (rlt3 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt3;
                
                break;
                
            case "GetRoomDesign":

                Response.Write(Utils.ToRecordJson(MemberHelper.GetRoomDesign(Request["CardType"])));
                break;
            case "RoomDesignUpdate":
                MemberRoomDesignView mrdv = new MemberRoomDesignView();
                Int32 UserID2 = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");

                mrdv.CardTypeId = Convert.ToInt32(Request.Form["CardTypeId"]);
                mrdv.f_dj = Convert.ToDecimal(Request.Form["f_dj"]);
                mrdv.f_jb = Request.Form["f_jb"];
                mrdv.Lcf = Convert.ToDecimal(Request.Form["Lcf"]);
                mrdv.Memo = Request.Form["Memo"];
                mrdv.z_dj = Convert.ToDecimal(Request.Form["z_dj"]);
                mrdv.ZdFj = Convert.ToDecimal(Request.Form["ZdFj"]);
                mrdv.id = UserID2;
                string rlt4 = MemberHelper.UpdateRoomDesign(mrdv);
                if (rlt4 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt4;
                break;
            case "RoomDesignDelete":
                Int32 delID2 = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                string rlt5 = MemberHelper.DeleteRoomDesign(delID2);
                if (rlt5 == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt5;
                break;
            case "RoomDesignCreate":
                MemberRoomDesignView prdv = new MemberRoomDesignView();

                prdv.f_dj = Convert.ToDecimal(Request.Form["f_dj"]);
                prdv.f_jb = Request.Form["f_jb"];
                if (!string.IsNullOrEmpty(Request.Form["Lcf"]))
                {
                    prdv.Lcf = Convert.ToDecimal(Request.Form["Lcf"]);
                }
             
                prdv.Memo = Request.Form["Memo"];
                if (!string.IsNullOrEmpty(Request.Form["z_dj"]))
                {
                    prdv.z_dj = Convert.ToDecimal(Request.Form["z_dj"]);
                }
                if (!string.IsNullOrEmpty(Request.Form["ZdFj"]))
                {
                    prdv.ZdFj = Convert.ToDecimal(Request.Form["ZdFj"]);
                }
               
                string CardType= Request.Form["CardTypeId2"];

                string rlt6 = MemberHelper.CreateRoomDesign(prdv, CardType);
                 if (rlt6 == "0")
                 {
                     Response.Clear();
                     Response.Write("{\"success\":true}");
                     Response.End();
                 }
                 else
                     result = rlt6;
                break;
            case "fjlx":
                Response.Write(Utils.ToRecordJson(MemberHelper.Getfjlx(Request["CardType"])));
                break;
            default:
                
                break;
        }
        //output json result
        Response.Write(result);
    }
}