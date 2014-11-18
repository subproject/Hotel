using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Setting;
using System.Web.Script.Serialization;

public partial class Setting_ParameterInfoData : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";
        string guid = Request["guid"] != "" ? Request["guid"] : "00000000-0000-0000-0000-000000000000";

        ParamentInfoHelper helper = new ParamentInfoHelper();

        #region 客户前端传来的值 生成一个model
        CheckInOutMemoModel model = new CheckInOutMemoModel();
        model.CheckInMemo1 = Request.Form["CheckInMemo1"];
        model.CheckInMemo2 = Request.Form["CheckInMemo2"];
        model.CheckInMemo3 = Request.Form["CheckInMemo3"];
        #endregion

        switch (action)
        {
            case "create":
                result = helper.CreateOrder(model);
                if (result == "0")
                    result = "{\"Success\":\"true\"}";
                break;
            case "read":
                CheckInOutMemoModel resultall = helper.ReadAll();
                //Response.Write(Utils.ToRecordJson(resultall));
                result = "{CheckInMemo1:\"" + resultall.CheckInMemo1 + "\",CheckInMemo2:\"" + resultall.CheckInMemo2 + "\",CheckInMemo3:\"" + resultall.CheckInMemo3 + "\"}";

                //result = "{\"CheckInMemo1\":\"" + resultall.CheckInMemo1 + "\",\"CheckInMemo2\":\"" + resultall.CheckInMemo2 + "\",\"CheckInMemo3\":\"" + resultall.CheckInMemo3 + "\"}";
                break;
            case "readTel":
                telMemoModel res = helper.readTel();
                result = "{tel:\"" + res.tel+ "\"}";
                break;
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}