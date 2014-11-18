using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class Member_MemberData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 封装filter数据类
       
      
 
        MemberFilter filter = new MemberFilter();
        filter.name = Request.Form["MemberName"];
        filter.cardNo = Request.Form["MemberCardNo"];
        filter.cardType = Request.Form["CardType"];
        filter.identifycard = Request.Form["IdCard"];

        filter.phone = Request.Form["HomeTelphone"];
        filter.mobile = Request.Form["Mobile"];
        if (!string.IsNullOrEmpty(Request.Form["Status"]))
        {
            filter.status = Convert.ToByte(Request.Form["Status"]);
        }
        filter.address = Request.Form["Address"];
        filter.birthday = Request.Form["BirthDay"];
        if (!string.IsNullOrEmpty(Request.Form["Validate"]))
        {
            filter.limitday = Convert.ToDateTime(Request.Form["Validate"]);
        }
        if (!string.IsNullOrEmpty(Request.Form["RegTime"] ))
        {
            filter.regditdate = Convert.ToDateTime(Request.Form["RegTime"]);//"01/01/2001 00:00"
        }
        
        #endregion

        switch (action)
        {
            case "readCardType":
                Response.Write(Utils.ToRecordJson(MemberHelper.ReadCardDesignAll()));
                break;
            case "getLastMemberNo":
                string rlt = MemberHelper.getLastMemberNo().ToString();//.ToString("0000000000");
                result = "{\"Success\":\"true\",\"MemberNo\":" + rlt + "}";
                
                break;
            case "create":
                MemberViewModel temp = new MemberViewModel();
                temp.MemberCardNo = Request.Form["MemberCardNo"].ToString();
                temp.CardType = Request.Form["CardType"].ToString();
                temp.Status = Convert.ToByte(Request.Form["Status"] != "" ? Request.Form["Status"] : "0");
                temp.IdCard = Request.Form["IdCard"].ToString();
                if (!string.IsNullOrEmpty(Request.Form["MemberNo2"]))
                {
                    temp.MemberNo = Request.Form["MemberNo2"].ToString();
                }
                temp.MemberName = Request.Form["MemberName"].ToString();
                temp.Password = Request.Form["Password"].ToString();
                temp.Sex = Request.Form["Sex"].ToString();
                temp.BirthDay = Request.Form["BirthDay"].ToString();
                temp.HomeTelphone = Request.Form["HomeTelphone"].ToString();
                temp.Mobile = Request.Form["Mobile"].ToString();
                if (!string.IsNullOrEmpty(Request.Form["Address2"]))
                {
                    temp.Address = Request.Form["Address2"].ToString();
                }
              
                temp.Score = Convert.ToInt32(Request.Form["Score"].ToString() != "" ? Request.Form["Score"].ToString() : "0");
                temp.RestScore = Convert.ToInt32(Request.Form["RestScore"].ToString() != "" ? Request.Form["RestScore"].ToString() : "0");
                temp.IsValidate = Convert.ToByte(Request.Form["IsValidate"] != "" ? Request.Form["IsValidate"] : "0");
                temp.Validate = (Request.Form["Validate"].ToString());
                temp.RegTime = DateTime.Now;
                string rlt2 = MemberHelper.Create(temp);
               // string rlt = MemberHelper.Create(temp);
                 
                    //"{\"total\":"+count+",\"rows\":"+json+"}";
                    result = "{\"Success\":\"true\"}";
                   // result = "{\"Success\":\"true\",\"MemberNo\":" + rlt2 + "}";
                 
                break;
            case "read":
                Int32 page = Convert.ToInt32((Request.Form["page"] != "" || Request.Form["page"] == null) ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32((Request.Form["rows"] != "" || Request.Form["rows"] == null) ? Request.Form["rows"] : "10");
                List<MemberViewModel> resultall = MemberHelper.ReadAll();
                List<MemberViewModel> resultcurrent = MemberHelper.ReadPart(page, rows);
                 
                 Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
              
                break;
            case "update":
                Int32 UserID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");

                temp = MemberHelper.gUserInfo(UserID);
                temp.MemberCardNo = Request.Form["MemberCardNo"].ToString();
                temp.CardType = Request.Form["CardType"].ToString();
                temp.Status = Convert.ToByte(Request.Form["Status"] != "" ? Request.Form["Status"] : "0");
                temp.IdCard = Request.Form["IdCard"].ToString();
                temp.MemberNo = Request.Form["MemberNo"].ToString(); 
                temp.MemberName = Request.Form["MemberName"].ToString();
                temp.Password = Request.Form["Password"].ToString();
                temp.Sex = Request.Form["Sex"].ToString();
                temp.BirthDay = Request.Form["BirthDay"].ToString();
                temp.HomeTelphone = Request.Form["HomeTelphone"].ToString();
                temp.Mobile = Request.Form["Mobile"].ToString();
                if (!string.IsNullOrEmpty(Request.Form["Address"]))
                {
                    temp.Address = Request.Form["Address"].ToString();
                }
               
                temp.Score = Convert.ToInt32(Request.Form["Score"].ToString() != "" ? Request.Form["Score"].ToString() : "0");
                temp.RestScore = Convert.ToInt32(Request.Form["RestScore"].ToString() != "" ? Request.Form["RestScore"].ToString() : "0");
                temp.IsValidate = Convert.ToByte(Request.Form["IsValidate"] != "" ? Request.Form["IsValidate"] : "0");
                temp.Validate  = (Request.Form["Validate"].ToString());
                rlt = MemberHelper.Update(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;

                break;
            case "delete":
                Int32 delID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                rlt = MemberHelper.Delete(delID);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;
            case "guashi":
                Int32 gsID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = MemberHelper.gUserInfo(gsID);
                if (temp.Status == 1)
                {
                    temp.Status = Convert.ToByte(2);
                    rlt = MemberHelper.Update(temp);
                    if (rlt == "0")
                    {
                        Response.Clear();
                        Response.Write("{\"success\":true}");
                        Response.End();
                    }
                    else
                        result = rlt;
                }
                else
                {
                    if (temp.Status == 2)
                    {
                        rlt = "当前用户已挂失";
                    }
                    else
                    {
                        rlt = "用户已注销";
                    }
                    result = rlt;
                }

                break;
            case "guashiCancel":
                Int32 gsCID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = MemberHelper.gUserInfo(gsCID);

                if (temp.Status == 2)
                {
                    temp.Status = Convert.ToByte(1);

                    rlt = MemberHelper.Update(temp);
                    if (rlt == "0")
                    {
                        Response.Clear();
                        Response.Write("{\"success\":true}");
                        Response.End();
                    }
                    else
                        result = rlt;
                }
                else
                {
                    rlt = "当前用户非挂失状态，不能取消挂失 ";
                    result = rlt;
                }
                break;
            case "checkin":
                Int32 checkinID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = MemberHelper.gUserInfo(checkinID);

                temp.Status = Convert.ToByte(0);
                rlt = MemberHelper.Update(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;

            case "xuka":
                Int32 xukaID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = MemberHelper.gUserInfo(xukaID);

                temp.Status = Convert.ToByte(1);
                temp.IsValidate = Convert.ToByte(Request.Form["IsValidate"] != "" ? Request.Form["IsValidate"] : "0");
                temp.Validate = (Request.Form["Validate"].ToString());
                rlt = MemberHelper.Update(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;
            case "buka":
                Int32 bukaID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                temp = MemberHelper.gUserInfo(bukaID);

                temp.Status = Convert.ToByte(1);

                temp.MemberCardNo = Request.Form["NewMemberCardNo"].ToString();
                rlt = MemberHelper.Update(temp);
                if (rlt == "0")
                {
                    Response.Clear();
                    Response.Write("{\"success\":true}");
                    Response.End();
                }
                else
                    result = rlt;
                break;
            case "search":
                Int32 page2 = Convert.ToInt32((Request.Form["page"] != "" || Request.Form["page"] == null) ? Request.Form["page"] : "1");
                Int32 rows2 = Convert.ToInt32((Request.Form["rows"] != "" || Request.Form["rows"] == null) ? Request.Form["rows"] : "10");
                List<MemberViewModel> resultall2 = MemberHelper.ReadAll();            
                List<MemberViewModel> resultcurrent2 = MemberHelper.ReadPartMember(page2, rows2, filter);
                Response.Write(Utils.ToRecordJson(resultcurrent2, resultall2.Count));
                break;
            case "CalLastMemberNo"://获取最后一个MEMBERNO+1
                string LastMemberNo = MemberHelper.ReadLastMemberNo();

                Response.Write(Utils.ToRecordJson(LastMemberNo));
                break;

            case "CardDesign"://获取最后一个MEMBERNO+1
                Response.Write(Utils.ToRecordJson(MemberHelper.ReadCardDesignAll()));
                break;
            case "CardCreate":
               MemberCardModel tempCard=new MemberCardModel();

               tempCard.CardType = Request.Form["CardType"];
               tempCard.CheckOutDelay = Convert.ToInt32(Request.Form["CheckOutDelay"]);
               tempCard.IsCharge = Request.Form["IsCharge"] != "on" ? false : true;
               tempCard.ChargePercent = Convert.ToDecimal(Request.Form["ChargePercent"]);

               tempCard.YePrompt = Convert.ToDecimal(Request.Form["YePrompt"]);

               tempCard.IsAutoUpLevel = Request.Form["IsAutoUpLevel"] != "on" ? false : true;

               tempCard.IsAutoDownLevel = Request.Form["IsAutoDownLevel"] != "on" ? false : true;

               tempCard.LowPoint = Convert.ToInt32(Request.Form["LowPoint"]);
               tempCard.HighPoint = Convert.ToInt32(Request.Form["HighPoint"]);


                    rlt = MemberHelper.AddCardDesign(tempCard);
                if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;
            case "CardUpdate":
                MemberCardModel tempCard2 = new MemberCardModel();
                tempCard2.CardType = Request.Form["CardType"];
                tempCard2.CheckOutDelay = Convert.ToInt32(Request.Form["CheckOutDelay"]);
                tempCard2.IsCharge = Request.Form["IsCharge"] != "on" ? false : true;
                tempCard2.ChargePercent = Convert.ToDecimal(Request.Form["ChargePercent"]);
                if (!string.IsNullOrEmpty(Request.Form["YePrompt"]))
                {
                    tempCard2.YePrompt = Convert.ToDecimal(Request.Form["YePrompt"]);
                }
                tempCard2.id = Convert.ToInt32(Request.Form["id"]);
                tempCard2.IsAutoUpLevel = Request.Form["IsAutoUpLevel"] != "on" ? false : true;

                tempCard2.IsAutoDownLevel = Request.Form["IsAutoDownLevel"] != "on" ? false : true;

                tempCard2.LowPoint = Convert.ToInt32(Request.Form["LowPoint"]);
                tempCard2.HighPoint = Convert.ToInt32(Request.Form["HighPoint"]);
                rlt = MemberHelper.updateMemberCard(tempCard2);
                if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;
            case "CardDelete":
                rlt = MemberHelper.delMemberDes(Convert.ToInt32(Request["id"]));
                if (rlt == "0")
                    Response.Write("{\"success\":\"true\"}");
                else
                    Response.Write(rlt);
                break;  
            default:
                break;
        }


        //output json result
        Response.Write(result);
    }
}