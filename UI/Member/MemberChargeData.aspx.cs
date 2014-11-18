using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Member;

public partial class Member_MemberChargeData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        MemberChargeHelper _helper = new MemberChargeHelper();
        switch (action)
        {
            case "create":
                MemberChargeViewModel temp = new MemberChargeViewModel();
                temp.CardNo = Request.Form["MemberCardNo"].ToString();
                temp.MemberName = Request.Form["MemberName"].ToString();
                
                temp.ActualCharge = Convert.ToDecimal(Request.Form["ActualCharge"] != "" ? Request.Form["ActualCharge"] : "0.0");
                temp.ChargeMoney = Convert.ToDecimal(Request.Form["ChargeMoney"] != "" ? Request.Form["ChargeMoney"] : "0.0");
                temp.FkFs = Request.Form["FkFs"].ToString().Trim();
                temp.CurTime = DateTime.Now;
                temp.Memo= Request.Form["message"]; 
                string rlt = _helper.CreateCharge(temp);
                if (rlt == "0")
                    result = "{\"Success\":\"true\"}";
                else
                    result = rlt;
                break;
            case "read":
                Int32 page = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rows = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<MemberChargeViewModel> resultall = _helper.ReadAll();
                List<MemberChargeViewModel> resultcurrent = _helper.ReadPart(page, rows);
                Response.Write(Utils.ToRecordJson(resultcurrent, resultall.Count));
                break;
            case "readByID":
                Int32 ID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                
                string CardNo = MemberHelper.gUserInfo(ID).MemberCardNo;
                Int32 pageNo = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                Int32 rowsNo = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                List<MemberChargeViewModel> resultCardNo = _helper.ReadAllById(CardNo);
                List<MemberChargeViewModel> resultCardNocurrent = _helper.ReadPartByID(pageNo, rowsNo, CardNo);
                Response.Write(Utils.ToRecordJson(resultCardNocurrent, resultCardNo.Count));
                break;

            case "getUserInfo":
                Int32 uID = Convert.ToInt32(Request["id"] != "" ? Request["id"] : "0");
                MemberViewModel userInfo = new MemberViewModel();
                userInfo = MemberHelper.gUserInfo(uID);
                MemberViewModelTemp tmp = new MemberViewModelTemp(userInfo);

                Response.Write(Utils.ToRecordJson(tmp, 1));
                break;
            case "getUserInfoByNo":
                //string CardNo2 = Convert.ToInt32(Request["MemberCardNo"]).ToString();
                ////Int32 pageNo2 = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                ////Int32 rowsNo2 = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                //List<MemberChargeViewModel> resultCardNo2 = _helper.ReadAllById(CardNo2);
                //List<MemberChargeViewModel> resultCardNocurrent2 = _helper.ReadPartByID(1, 10, CardNo2);
                //Response.Write(Utils.ToRecordJson(resultCardNocurrent2, resultCardNo2.Count));
                if (!string.IsNullOrEmpty(Request["MemberCardNo"]))
                {
                    MemberViewModel userInfo2 = MemberHelper.gUserInfoByNo(Request["MemberCardNo"]);
                    MemberViewModelTemp tmp2 = new MemberViewModelTemp(userInfo2);


                    Response.Write(Utils.ToRecordJson(tmp2, 1));
                }
                break;
            case "getJiFen":
                //string CardNo2 = Convert.ToInt32(Request["MemberCardNo"]).ToString();
                ////Int32 pageNo2 = Convert.ToInt32(Request.Form["page"] != "" ? Request.Form["page"] : "1");
                ////Int32 rowsNo2 = Convert.ToInt32(Request.Form["rows"] != "" ? Request.Form["rows"] : "10");
                //List<MemberChargeViewModel> resultCardNo2 = _helper.ReadAllById(CardNo2);
                //List<MemberChargeViewModel> resultCardNocurrent2 = _helper.ReadPartByID(1, 10, CardNo2);
                //Response.Write(Utils.ToRecordJson(resultCardNocurrent2, resultCardNo2.Count));
                if (!string.IsNullOrEmpty(Request["MemberCardNo"]))
                {
                    MemberViewModel userInfo2 = MemberHelper.gUserInfoByNo(Request["MemberCardNo"]);
                    result = "{\"RestScore\":" + userInfo2.RestScore + ",\"ZheKou\":" + userInfo2.Discount + "}";
                   // Response.Write(Utils.ToRecordJson(result));
                }
                break; 
            case "getAddress":
                string Address= MemberHelper.getAddress(Request["id"]);
                Response.Write(Utils.ToRecordJson(Address));
                break;
            case "regetUserInfos":
                Int32 getID=0;
                string gCardNo = Request["CardNos"];
                List<MemberViewModel> gresult =MemberHelper.ReadAll();
                userInfo = null;
                foreach(MemberViewModel r in  gresult)
                {
                    if (r.MemberCardNo == gCardNo)
                    {
                        getID = r.ID;
                        userInfo = r;
                        break;
                    }
                }
                Response.Write(Utils.ToRecordJson(userInfo, 1));
                break;

            case "getAllowDesign":
               // MemberAllowModel allowDesign = MemberHelper.GetAllowDesign();
               // Response.Write(Utils.ToRecordJson(allowDesign));
                break;
            case "saveAllowDesign":
                MemberAllowModel prdv = new MemberAllowModel();
                prdv.allowMultiUseCard = Convert.ToBoolean(Request["yxdr"]);
                prdv.allowNoCardConsume = Convert.ToBoolean(Request["yxwk"]);


                string rlt6 = MemberHelper.SaveAllowDesign(prdv);
                 if (rlt6 == "0")
                 {
                     Response.Clear();
                     Response.Write("{\"success\":true}");
                     Response.End();
                 }
                 else
                     result = rlt6;
                break;
                
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}