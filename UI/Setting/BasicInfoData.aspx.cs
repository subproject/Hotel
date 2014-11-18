using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.Common;
using HotelEntities;
using HotelLogic.FrontDesk;

public partial class Setting_BasicInfoData : System.Web.UI.Page
{
    /// <summary>
    /// fkfs和gj的read update delete create
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = string.Empty;
        //module: gj 和fkfs khlb zjlx
        string module = Request["module"] != "" ? Request["module"] : "";
        //action: create read update delete
        string action = Request["action"] != "" ? Request["action"] : "";

        #region 客户前端传来的值 也可以用一个value解决 
        string fs = Request.Form["fkfs"]!="" ? Request.Form["fkfs"] : "";
        string gj = Request.Form["gj"] != "" ? Request.Form["gj"] : "";
        string kl = Request.Form["kl"] != "" ? Request.Form["kl"] : "";
        string zjlx = Request.Form["zjlx"] != "" ? Request.Form["zjlx"] : "";
        string ydway = Request.Form["ydway"] != "" ? Request.Form["ydway"] : "";
        string caozuoyuan = Request.Form["caozuoyuan"] != "" ? Request.Form["caozuoyuan"] : "";
        string fjJB = Request.Form["fjJB"] != "" ? Request.Form["fjJB"] : "";
        string KrZt = Request.Form["KrZt"] != "" ? Request.Form["KrZt"] : "";
        string Ld = Request.Form["Ld"] != "" ? Request.Form["Ld"] : "";
        string Lc = Request.Form["Lc"] != "" ? Request.Form["Lc"] : "";
        string Hxfs = Request.Form["Hxfs"] != "" ? Request.Form["Hxfs"] : "";
        string TeQuanRen = Request.Form["TeQuanRen"] != "" ? Request.Form["TeQuanRen"] : "";
        Int32 ID = Convert.ToInt32(Request["ID"] != "" ? Request["ID"] : "0");
        #endregion
        switch (module)
        {
            #region fkfs
            case "fkfs":
                if (action == "create")
                {
                    zd_fsviewmodel temp = new zd_fsviewmodel();
                    temp.fkfs = fs;
                    string rlt = CommonHelper.createFKFS(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                    
                }
                if (action == "read")
                {
                    var rlt=CommonHelper.getFKFS();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_fsviewmodel temp = new zd_fsviewmodel();
                    temp.ID = ID;
                    temp.fkfs = fs;
                    string rlt = CommonHelper.updateFKFS(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteFKFS(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = "{\"Error\":\""+rlt+"\"}";
                }          
                break;
            #endregion

            #region caozuoyuan
            case "caozuoyuan":
                if (action == "create")
                {
                    zd_caozuoyuanviewmodel temp = new zd_caozuoyuanviewmodel();
                    temp.caozuoyuan= caozuoyuan;
                    string rlt = CommonHelper.createCaozuoyuan(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getCaozuoyuan();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_caozuoyuanviewmodel temp = new zd_caozuoyuanviewmodel();
                    temp.ID = ID;
                    temp.caozuoyuan= caozuoyuan;
                    string rlt = CommonHelper.updateCaozuoyuan(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteCaozuoyuan(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region fjJB
            case "fjJB":
                if (action == "create")
                {
                    zd_fJBviewmodel temp = new zd_fJBviewmodel();
                    temp.fjJB = fjJB;
                    string rlt = CommonHelper.createfjJB(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getfjJB();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_fJBviewmodel temp = new zd_fJBviewmodel();
                    temp.ID = ID;
                    temp.fjJB = fjJB;
                    string rlt = CommonHelper.updatefjJB(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deletefjJB(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region KrZt
            case "KrZt":
                if (action == "create")
                {
                    zd_KrZtviewmodel temp = new zd_KrZtviewmodel();
                    temp.KrZt = KrZt;
                    string rlt = CommonHelper.createKrZt(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getKrZt();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_KrZtviewmodel temp = new zd_KrZtviewmodel();
                    temp.ID = ID;
                    temp.KrZt = KrZt;
                    string rlt = CommonHelper.updateKrZt(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteKrZt(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region gj
            case "gj":
                if (action == "create")
                {
                    zd_gjviewmodel temp = new zd_gjviewmodel();
                    temp.gj = gj;
                    string rlt = CommonHelper.createGJ(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getGJ();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_gjviewmodel temp = new zd_gjviewmodel();
                    temp.ID = ID;
                    temp.gj = gj;
                    string rlt = CommonHelper.updateGJ(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteGJ(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region TeQuanRen
            case "TeQuanRen":
                if (action == "create")
                {
                    zd_Tequanrenviewmodel temp = new zd_Tequanrenviewmodel();
                    temp.TeQuanRen = TeQuanRen;
                    string rlt = CommonHelper.createTQR(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getTQR();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "getZhekou")//特权人折扣率
                {
                    string tqr = Request["TeQuanRen"];
                    Response.Write(Utils.ToRecordJson(CommonHelper.getTQRZhekou(tqr)));
                }
                if (action == "update")
                {
                    zd_Tequanrenviewmodel temp = new zd_Tequanrenviewmodel();
                    temp.ID = ID;
                    temp.TeQuanRen = TeQuanRen;
                    string rlt = CommonHelper.updateTQR(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteTQR(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region khlb
            case "khlb":
                if (action == "create")
                {
                    zd_klviewmodel temp = new zd_klviewmodel();
                    temp.KHLB = kl;
                    string rlt = CommonHelper.createKHLB(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getKHLB();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_klviewmodel temp = new zd_klviewmodel();
                    temp.ID = ID;
                    temp.KHLB = kl;
                    string rlt = CommonHelper.updateKHLB(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteKHLB(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region zjlx
            case "zjlx":
                if (action == "create")
                {
                    zd_zjlxviewmodel temp = new zd_zjlxviewmodel();
                    temp.ZJLX = zjlx;
                    string rlt = CommonHelper.createZJLX(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    var rlt = CommonHelper.getZJLX();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_zjlxviewmodel temp = new zd_zjlxviewmodel();
                    temp.ID = ID;
                    temp.ZJLX = kl;
                    string rlt = CommonHelper.updateZJLX(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteZJLX(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            #region ydway
            case "ydway":
                if (action == "create")
                {
                    YDWayModel temp = new YDWayModel();
                    temp.Way = ydway;
                    string rlt = YDWayHelper.Instance.CreateYDWay(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "read")
                {
                    var rlt = YDWayHelper.Instance.ReadYDWay();
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    YDWayModel temp = new YDWayModel();
                    temp.AutoID = ID;
                    temp.Way = ydway;
                    string rlt = YDWayHelper.Instance.UpdateYDWay(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = YDWayHelper.Instance.DeleteYDWay(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region Ld
            case "Ld":
                if (action == "create")
                {
                    zd_Ldviewmodel temp = new zd_Ldviewmodel();
                    temp.Ld = Ld;
                    string rlt = CommonHelper.createLd(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    List<zd_Ldviewmodel> rlt = CommonHelper.getLd(Request["type"]);
                   
                    
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    zd_Ldviewmodel temp = new zd_Ldviewmodel();
                    temp.ID = ID;
                    temp.Ld = Ld;
                    string rlt = CommonHelper.updateLd(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteLd(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region Lc
            case "Lc":
                if (action == "create")
                {
                    zd_Lcviewmodel temp = new zd_Lcviewmodel();
                    temp.Lc = Lc;
                    temp.LdID = ID;
                    string rlt = CommonHelper.createLc(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    Int32 id = Convert.ToInt32(Request["ldID"]);
                    var rlt = CommonHelper.getLc(id,Request["type"]);
                  
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if(action=="readPart")
                {
                    var rlt = CommonHelper.getPartLc();
                    zd_Lcviewmodel all = new zd_Lcviewmodel();
                    all.ID = 0;
                    all.Lc = "全部";
                    rlt.Add(all);
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    Int32 id = Convert.ToInt32(Request["ldID"]);
                    zd_Lcviewmodel temp = new zd_Lcviewmodel();
                    temp.ID = ID;
                    temp.Lc = Lc;
                    temp.LdID = id;
                    string rlt = CommonHelper.updateLc(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteLc(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion

            #region Hxfs
            case "hxfs":
                if (action == "create")
                {
                    zd_Hxfswmodel temp = new zd_Hxfswmodel();
                    temp.hxfsName = Hxfs;
                    string rlt = CommonHelper.createHxfs(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;

                }
                if (action == "read")
                {
                    Int32 id = Convert.ToInt32(Request["ldID"]);
                    var rlt = CommonHelper.getPartHxfs();//(id, Request["type"]);

                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "readPart")
                {
                    var rlt = CommonHelper.getPartHxfs();
                    zd_Hxfswmodel all = new zd_Hxfswmodel();
                    all.ID = 0;
                    all.hxfsName = "全部";
                    rlt.Add(all);
                    Response.Write(Utils.ToRecordJson(rlt));
                }
                if (action == "update")
                {
                    Int32 id = Convert.ToInt32(Request["ldID"]);
                    zd_Hxfswmodel temp = new zd_Hxfswmodel();
                    temp.ID = ID;
                    temp.hxfsName = Hxfs;
                    string rlt = CommonHelper.updateHxfs(temp);
                    if (rlt == "0")
                        result = "{\"Success\":\"true\"}";
                    else
                        result = rlt;
                }
                if (action == "delete")
                {
                    string rlt = CommonHelper.deleteHxfs(ID);
                    if (rlt == "0")
                        result = "{\"success\":\"true\"}";
                    else
                        result = rlt;
                }
                break;
            #endregion
            default:
                break;
        }
        //output json result
        Response.Write(result);
    }
}