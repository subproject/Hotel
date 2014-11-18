using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic;
using HotelLogic.FrontDesk;
using System.IO;
using System.Text;
using InfoSoftGlobal;

public partial class _Default : HotelPage
{
    RoomStatus rs;
    public string SummerStr = string.Empty;
    string fjStatues = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        if (!this.IsPostBack)
        {
          
            if (!String.IsNullOrEmpty(Request["fjStatues"]))
            {
                fjStatues = Request["fjStatues"];
            }
            //helper
            rs = new RoomStatus();
            //RoomStatus.Text = CreateStatusChart();
            SummerStr = CreateSummerStr();
        }
       
    }
    //flash数据
    public string CreateSummerStr()
    {
        string str = string.Empty;
        foreach (var r in rs.getStatusSummer())
        {
            //str += "<set name='" + r.Status + "' value='" + r.SL + "' color='#" + r.ColorStr + "' />";
            str += "<tr style=\"padding:5px;height:26px\"><td style=\"padding:3px;width:30px;\" align=\"center\">" + r.Status + "</td><td  onclick=\"javascript:window.location.href='default.aspx?fjStatues=" + r.Status +"'\" style=\"background-color:#" + r.ColorStr + ";width:80px;padding:3px;\">&nbsp;</td><td style=\"padding:3px;width:20px\"  align=\"center\">" + r.SL + "</td></tr><tr style=\"height:3px;\"><td colspan=\"3\">&nbsp;</td></tr>";
        }
        return str;
        //string strXML;
        ////Initialize <graph> element
        //strXML = "<graph caption='房间即时状态' baseFontSize='12' showAlternateVGridColor='1' alternateVGridAlpha='10' alternateVGridColor='AFD8F8' numDivLines='4' decimalPrecision='0' canvasBorderThickness='1' canvasBorderColor='114B78' baseFontColor='114B78' hoverCapBorderColor='114B78' hoverCapBgColor='E7EFF6'>";
        ////Add all data
        //foreach (var r in rs.getStatusSummer())
        //{
        //    strXML += "<set name='" + r.Status + "' value='" + r.SL + "' color='#" + r.ColorStr + "' />";
        //}
        ////Close <graph> element
        //strXML += "</graph>";

        ////Create the chart
        //return FusionCharts.RenderChart("FusionCharts/FCF_Bar2D.swf", "", strXML, "Sales1", "198", "500", false, false);
    }
    public int ldid;
    public int lcid;
    public void GetRooms()
    {
        lcid = Convert.ToInt32(this.Lc.Value.ToString() != "" ? this.Lc.Value.ToString() : "0");
        ldid = Convert.ToInt32(this.Ld.Value.ToString() != "" ? this.Ld.Value.ToString() : "0");
        //画出各房间分类区块
        Response.Write("<div class=\"easyui-tabs\" border=\"false\" style=\"padding:0px\">");
        List<RoomCategory> rctgy = new List<RoomCategory>();
        rctgy = rs.getRoomCategory();
        //默认全部房间
        Response.Write("<div title=\"全部房间\" style=\"padding:1px\">");
        //全部房间,先清空过期预定房间,超过当前时间为过期
        Response.Write(DrawTable(rs.getRooms(lcid, ldid), fjStatues));
        Response.Write("</div>");

        //if (this.Ld.Value.ToString() != "" || this.Lc.Value.ToString() != "")
        //{
        //    Response.Write(DrawTable(rs.getRooms(lcid,ldid), fjStatues));
        //}

        foreach (var rc in rctgy)
        {
            Response.Write("<div title=\""+rc.CategoryName+"\" style=\"padding:1px\">");
            //各分类下的房间信息
            //Response.Write(DrawTable(rs.getRooms(rc.CategoryID), fjStatues));
           Response.Write(DrawTable(rs.getRooms(rc.CategoryID,lcid,ldid), fjStatues));
            Response.Write("</div>");
        }
        Response.Write("</div>");
    }

    private string DrawTable(List<RoomStatusViewModel> rooms,string statues="")
    {
        if (statues != "")
        {
            rooms = (from a in rooms
                     where a.Status == statues
                     select a).ToList();
        }
        int roomcount = rooms.Count;
        string result = string.Empty;
        if (roomcount > 0)
        {
            int rows = (roomcount - roomcount % 10) / 10 + 1;
            StringBuilder tblstr = new StringBuilder();
            tblstr.Append("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\">");
            //完整行,只有一行
            if (rows - 1 > 0)
            {
                for (int i = 0; i < rows - 1; i++)
                {
                    tblstr.Append("<tr height=\"85px\">");
                    for (int j = 0; j < 10; j++)
                    {
                        tblstr.Append("<td width=\"10%\">");
                        tblstr.Append(DrawRoom(rooms[i * 10 + j]));
                        tblstr.Append("</td>");
                    }
                    tblstr.Append("</tr>");
                }
            }
            //最后一行
            tblstr.Append("<tr height=\"85px\">");
            for (int j = 0; j < roomcount%10; j++)
            {
                tblstr.Append("<td width=\"10%\">");
                tblstr.Append(DrawRoom(rooms[roomcount - roomcount % 10+j]));
                tblstr.Append("</td>");
            }
            for (int j = 0; j < 10-roomcount % 10; j++)
            {
                tblstr.Append("<td width=\"10%\">&nbsp;");
                tblstr.Append("</td>");
            }
            tblstr.Append("</tr>");
            tblstr.Append("</table>");
            result = tblstr.ToString();
        }
        return result;
    }
    //画单个房间
    private string DrawRoom(RoomStatusViewModel rsvm)
    {
        string result=string.Empty;
        StringBuilder roomstr = new StringBuilder();
        //在此控制颜色，表示房间状态，控制右键菜单
        #region 锁房
        if (rsvm.Status == "锁房")
        {
            if (rsvm.days > 0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 锁房菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=脏房'\">设置脏房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            //roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">离店时间：" + rsvm.RoomID + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});</script>");
        }
        #endregion
        #region 脏房
        else if (rsvm.Status == "脏房")
        {
            if(rsvm.days>0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 脏房菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">清扫脏房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            //roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">离店时间：" + rsvm.RoomID + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});</script>");
        }
        #endregion 脏房
        #region 空房
        else if (rsvm.Status == "空房")
        {
            if (rsvm.days > 0)
            {
               //roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff;float:left\">" + rsvm.RoomID + "</div><div style=\"margin:5px;text-align:center;background:#ffcc00;float:right\">&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#000;\">" + rsvm.days.ToString() + "</font>&nbsp;&nbsp;&nbsp;&nbsp;</div><div style=\"margin:5px;clear:both\"></div></div>");
           
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 空房菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=脏房'\">设置脏房</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/RegisterPage.aspx?fh=" + rsvm.RoomID + "','553','800')\">入住登记</div>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            //roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">离店时间：" + rsvm.RoomID + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});</script>");
        }
        #endregion
        #region 预订
        else if (rsvm.Status == "预订")
        {
            //当天到，显示预订
            if (rsvm.YDDR.Date == System.DateTime.Today)
            {

                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"text-align:center;margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;text-decoration: underline;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px;text-decoration: underline;\">" + rsvm.CustomerName + "</div><div style=\"margin:5px;text-decoration: underline;\">房价：" + (rsvm.DJ.HasValue ? Math.Round(rsvm.DJ.Value, 2).ToString() : "0") + "</div><div style=\"margin:5px;text-align: right;\">" + rsvm.YDDR.Hour + ":" + rsvm.YDDR.Minute + "</div></div>");
            }
            //否则显示空房，提示几天后有预订到
            else {
                int daycount = (rsvm.YDDR - DateTime.Today).Days;
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + daycount + "</div></div>");
            }
            #region 预订菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=脏房'\">设置脏房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            //roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/YuDingDan.aspx?fh=" + rsvm.RoomID + "','400','700')\">预定信息</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='FrontDesk/CancelYD.aspx?fh=" + rsvm.RoomID + "'\">取消预定</div>");
            //roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/RegisterPage.aspx?fh=" + rsvm.RoomID + "','553','800')\">入住登记</div>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">预订人：" + rsvm.CustomerName + "</p><p style=\"color:#fff\">电话：" + rsvm.Tel + "</p><p style=\"color:#fff\">预到时间：" + rsvm.YDDR + "</p><p style=\"color:#fff\">预离时间：" + rsvm.YDLR + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
        }
        #endregion
        #region 在客
        else if (rsvm.Status == "在客")
        {
            if (rsvm.days > 0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div><div><div style=\"width:10px;height:5px;border:1px;padding-top:10px;margin:3px;\"><font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font>&nbsp;&nbsp;" + rsvm.LeftMoney.ToString() + "</div></div></div>"); 
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 在客菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?action=ruzhu&fh=" + rsvm.RoomID + "&status=脏房'\">设置脏房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            //roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/AppendRegister2.aspx?fh=" + rsvm.RoomID + "','500','800')\">加开房间</div>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/HuanFang.aspx?fh=" + rsvm.RoomID + "&action=load','500','800')\">换房</div>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/XuZhu.aspx?fh=" + rsvm.RoomID + "','500','800')\">续住</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/LuRuFeiYong.aspx?fh=" + rsvm.RoomID + "','500','800')\">录入费用</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/KeRenJieZhang.aspx?fh=" + rsvm.RoomID + "','500','800')\">客人结帐</div>");
            roomstr.Append("<hr width=\"96px\"/>");

            roomstr.Append("<div onclick=\"openwin('Register/RegisterModify.aspx?fh=" + rsvm.RoomID + "','500','800')\">修改入住登记信息</div>");
 
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/MergeRegisterOrder.aspx?fh=" + rsvm.RoomID + "','500','800')\">合并帐单</div>");
            roomstr.Append("<div onclick=\"openwin('Register/SplitRegisterOrder.aspx?fh=" + rsvm.RoomID + "','500','800')\">拆分帐单</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/ZhuanZhang.aspx?fh=" + rsvm.RoomID + "','500','800')\">转帐</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/ChaiFenMingXiZhangDan.aspx?fh=" + rsvm.RoomID + "','500','800')\">拆分明细帐单</div>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">客人姓名：" + rsvm.CustomerName + "</p><p style=\"color:#fff\">实际房价：" + rsvm.JG + "</p><p style=\"color:#fff\">到店时间：" + rsvm.DR + "</p><p style=\"color:#fff\">离店时间：" + rsvm.LR + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
        }
        #endregion
        #region 维修
        else if (rsvm.Status == "维修")
        {
            if (rsvm.days > 0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 维修菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=脏房'\">设置脏房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            //roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">离店时间：" + rsvm.RoomID + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});</script>");
        }
        #endregion

        // <img src="Images/clean.png" width="16px" height="16px" />

        #region 在客脏住
        else if (rsvm.Status == "脏住")
        {
            if (rsvm.days > 0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div><div style=\";width:10px;height:5px;margin:3px;\"><font>" + rsvm.days.ToString() + "</font>&nbsp;&nbsp;" + rsvm.LeftMoney.ToString() + "<img src=\"Images/clean.png\" width=\"16px\" height=\"16px\" /></div></div>");
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#" + rsvm.StatusColor + ";color:#fff\">" + rsvm.RoomID + "</div><div style=\"margin:5px\"></div><div style=\"margin:5px\">" + rsvm.CustomerName + "</div></div>");
            }
            #region 在客脏住菜单
            roomstr.Append("<div id=\"" + rsvm.RoomID + "\" class=\"easyui-menu\" style=\"width:120px;\">");
            roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=在客'\">清扫脏房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=空房'\">设置空房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=维修'\">设置维修房</div>");
            //roomstr.Append("<div onclick=\"javascript:window.location.href='ChangeRoomStatus.aspx?fh=" + rsvm.RoomID + "&status=锁房'\">设置锁房</div>");
            //roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/KuaiSuDanJianYD.aspx?fh=" + rsvm.RoomID + "','430','500')\">快速预定</div>");
            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/AppendRegister2.aspx?fh=" + rsvm.RoomID + "','500','800')\">加开房间</div>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/HuanFang.aspx?fh=" + rsvm.RoomID + "&action=load','500','800')\">换房</div>");
            roomstr.Append("<div onclick=\"openwin('FrontDesk/XuZhu.aspx?fh=" + rsvm.RoomID + "','500','800')\">续住</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/LuRuFeiYong.aspx?fh=" + rsvm.RoomID + "','500','800')\">录入费用</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/KeRenJieZhang.aspx?fh=" + rsvm.RoomID + "','500','800')\">客人结帐</div>");
            roomstr.Append("<hr width=\"96px\"/>");

            roomstr.Append("<div onclick=\"openwin('Register/RegisterModify.aspx?fh=" + rsvm.RoomID + "','500','800')\">修改入住登记信息</div>");

            roomstr.Append("<hr width=\"96px\"/>");
            roomstr.Append("<div onclick=\"openwin('Register/MergeRegisterOrder.aspx?fh=" + rsvm.RoomID + "','500','800')\">合并帐单</div>");
            roomstr.Append("<div onclick=\"openwin('Register/SplitRegisterOrder.aspx?fh=" + rsvm.RoomID + "','500','800')\">拆分帐单</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/ZhuanZhang.aspx?fh=" + rsvm.RoomID + "','500','800')\">转帐</div>");
            roomstr.Append("<div onclick=\"openwin('Cash/ChaiFenMingXiZhangDan.aspx?fh=" + rsvm.RoomID + "','500','800')\">拆分明细帐单</div>");
            roomstr.Append("</div>");
            #endregion

            //右击弹出邦定
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">客人姓名：" + rsvm.CustomerName + "</p><p style=\"color:#fff\">实际房价：" + rsvm.JG + "</p><p style=\"color:#fff\">到店时间：" + rsvm.DR + "</p><p style=\"color:#fff\">离店时间：" + rsvm.LR + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
        }
        #endregion
        #region 其它
        else
        {
            if (rsvm.days > 0)
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#ccc;color:#fff\">房号:" + rsvm.RoomID + "</div>&nbsp;&nbsp;&nbsp;&nbsp;<font style=\"color:#FFFF00\">" + rsvm.days.ToString() + "</font></div>");
            }
            else
            {
                roomstr.Append("<div id=\"div" + rsvm.RoomID + "\" data-options=\"handle:'#title'\" style=\"margin:1px;height:83px;background:#" + rsvm.StatusColor + ";border:1px solid #ccc;color:#fff\"><div id=\"title\" style=\"padding:5px;background:#ccc;color:#fff\">房号:" + rsvm.RoomID + "</div></div>");
            }
            //右击弹出邦定
            //roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});$('#div" + rsvm.RoomID + "').tooltip({position: 'bottom',content: '<p style=\"color:#fff\">房号：" + rsvm.RoomID + "</p><p style=\"color:#fff\">房间级别：" + rsvm.JB + "</p><p style=\"color:#fff\">离店时间：" + rsvm.RoomID + "</p>',onShow: function(){$(this).tooltip('tip').css({backgroundColor: '#666',borderColor: '#666'});}});</script>");
            roomstr.Append("<script>$(function(){$(div" + rsvm.RoomID + ").bind('contextmenu',function(e){e.preventDefault();$('#" + rsvm.RoomID + "').menu('show', {left: e.pageX,top: e.pageY});});});</script>");
        }
        #endregion
        result =roomstr.ToString();
        return result;
    }

    protected void allFT_Click(object sender, EventArgs e)
    {
        fjStatues = "";
        rs = new RoomStatus();
        SummerStr = CreateSummerStr();
        GetRooms();
       
    }
}