<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <meta charset="UTF-8" />
    <title>localhost</title>
    <!--<title>争舜酒店客房管理系统</title>-->
    <script type="text/javascript" src="jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="jquery.easyui.min.js"></script>
    <link rel="stylesheet" type="text/css" href="themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="themes/demo.css" />
    <script type="text/javascript" src="Hotelmgr.js"></script>
    <script language="Javascript" type="text/javascript" src="FusionCharts/FusionCharts.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //楼栋
            $("#Ld").combobox({
                url: 'Setting/BasicInfoData.aspx?module=Ld&action=read',
                valueField: 'ID',
                textField: 'Ld',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Ld').combobox('getData');

                    var len = data.length - 1;
                    if (data.length > 0) {
                        $("#Ld").combobox('select', data[len].Ld);
                    }
                }
            });
            //加载所有楼层
            $("#Lc").combobox({
                url: 'Setting/BasicInfoData.aspx?module=Lc&action=readPart',
                valueField: 'ID',
                textField: 'Lc',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Lc').combobox('getData');
                    var len = data.length - 1;
                    if (data.length > 0) {
                        $("#Lc").combobox('select', data[len].Lc);
                    }
                }
            });
            //根据楼栋获得楼层
            $('#Ld').combobox({
                onSelect: function () {
                    var ldname = $('#Ld').datebox('getValue');
                    if (ldname == 0) {
                        $("#Lc").combobox({
                            url: 'Setting/BasicInfoData.aspx?module=Lc&action=readPart',
                            valueField: 'ID',
                            textField: 'Lc'
                        }).combobox('clear');
                    }
                    else {
                        $("#Lc").combobox({
                            url: 'Setting/BasicInfoData.aspx?module=Lc&action=read&ldID=' + ldname,
                            valueField: 'ID',
                            textField: 'Lc'
                        }).combobox('clear');
                    }
                }
            });

            $("#fangjianhao").keyup(function (e) {
                var currKey = 0, e = e || event; currKey = e.keyCode || e.which || e.charCode;
                if (currKey == 13) {
                    if ($("#fangjianhao").val() != "") {

                        $.post('Setting/FHChargeData.aspx?action=fjstatues', { fangjianhao: $("#fangjianhao").val() }, function (result) {
                            if (result != "-1") {//-1表示没有该房间
                                if (result == "0") {//0表示空房
                                    openwin('Register/RegisterPage.aspx?fh=' + $("#fangjianhao").val(), '553', '800');
                                }
                                else if (result == "1") {//1表示已入住
                                    openwin('Register/RegisterModify.aspx?fh=' + $("#fangjianhao").val(), '553', '800');
                                }
                            } else {
                                $.messager.show({
                                    title: 'Error',
                                    msg: '没有该房间'
                                });
                            }
                        });
                    }
                }
            });

            //房间号 如果没入住就进登记页面， 如果已入住  就跳到退房界面 
            $("#fangjianhao").change(function () {
                if ($("#fangjianhao").val() != "") {
                    $.post('Setting/FHChargeData.aspx?action=fjstatues', { fangjianhao: $("#fangjianhao").val() }, function (result) {
                        if (result != "-1") {//-1表示没有该房间
                            if (result == "0") {//0表示空房
                                openwin('Register/RegisterPage.aspx?fh=' + $("#fangjianhao").val(), '553', '800');
                            }
                            else if (result == "1") {//1表示已入住
                                openwin('Register/RegisterModify.aspx?fh=' + $("#fangjianhao").val(), '553', '800');
                            }
                        } else {
                            $.messager.show({
                                title: 'Error',
                                msg: '没有该房间'
                            });
                        }
                    });
                }
            });
        });
    </script>
</head>
<body class="easyui-layout">
    <form id="form1" runat="server">
    <!--头部menu和button区-->
    <div data-options="region:'north',border:true" style="height: 98px; padding: 0px;
        overflow: hidden">
        <div style="padding: 3px; border: 0px solid #ddd; background-color: #eeeeee">
            <a class="easyui-menubutton" data-options="menu:'#frontdesk',iconCls:'icon_frontdesk'">
                预定管理</a> <a class="easyui-menubutton" data-options="menu:'#view',iconCls:'icon_view'">
                    登记管理</a><a class="easyui-menubutton" data-options="menu:'#cash',iconCls:'icon_cash'">
                        收银管理</a> <a class="easyui-menubutton" data-options="menu:'#member',iconCls:'icon_member'">
                            营销管理</a> <a class="easyui-menubutton" data-options="menu:'#housestatus',iconCls:'icon_house'">
                                客房管理</a> <a class="easyui-menubutton" data-options="menu:'#warehouse',iconCls:'icon_warehouse'">
                                    库存管理</a> <a class="easyui-menubutton" data-options="menu:'#others',iconCls:'icon_others'">
                                        其他管理</a> <a class="easyui-menubutton" data-options="menu:'#report',iconCls:'icon_report'">
                                            报表管理</a> <a class="easyui-menubutton" data-options="menu:'#setting',iconCls:'icon_setting'">
                                                系统维护</a> <a class="easyui-menubutton" data-options="menu:'#help',iconCls:'icon-help'">
                                                    帮助</a>
        </div>
        <div id="frontdesk" style="width: 100px;">
            <div onclick="openwin('FrontDesk/KuaiSuYuDing.aspx','500','800')">
                快速预定</div>
            <div onclick="openwin('FrontDesk/FangJianYuDing.aspx','500','800')">
                房间预订</div>
            <div onclick="openwin('FrontDesk/YuDingGuanLi.htm','500','800')">
                当日抵达</div>
            <div onclick="openwin('FrontDesk/YuDingChaXun.htm','500','800')">
                预订查询</div>
        </div>
        <div id="view" style="width: 100px;">
            <div onclick="openwin('Register/RegisterPage.aspx','553','800')">
                前台登记</div>
            <!--<div onclick="openwin('FrontDesk/KeRenChaXun.htm','500','800')">客人查询</div>-->
            <%--  <div onclick="openwin('FrontDesk/ZaiDianGuanLi.aspx','500','800')">
                在店客人管理</div>
            <div onclick="openwin('FrontDesk/SuiKeXinXi.aspx','500','800')">
                随客信息管理</div>--%>
            <div onclick="openwin('FrontDesk/HuanFang.aspx','400','700')">
                换房</div>
          <%--  <div onclick="openwin('FrontDesk/HuanFangChaXun.htm','550','700')">
                打印换房单</div>--%>
            <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                续住</div>
                 <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                修改入住登记信息</div>
                 <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                合并账单</div>
                 <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                拆分账单</div>
                 <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                转账</div>
                  <div onclick="openwin('FrontDesk/XuZhu.aspx','560','700')">
                拆分明细账单</div>
            <div onclick="openwin('FrontDesk/TuiFangChaXun.aspx','500','800')">
                客人查询</div>
            <div onclick="openwin('FrontDesk/GoodsLease.aspx','500','800')">
                物品租借</div>
        </div>
        <div id="member" style="width: 100px;">
            <div onclick="openwin('Member/MemberManagement.aspx','500','800')">
                会员管理</div>
            <div onclick="openwin('Member/MemberQuery.aspx','600','1500')">
                会员查询</div>
            <div onclick="openwin('Member/MemberCardManager.aspx','500','800')">
                会员卡设置</div>
            <div onclick="openwin('Member/MemberCardRecharge.aspx','500','800')">
                会员充值</div>
            <div onclick="openwin('Member/MemberChargeQuery.htm','500','800')">
                充值记录查询</div>
            <div onclick="openwin('Member/MemberInfoQuery.htm','500','800')">
                会员信息统计</div>
            <%-- <div>
                会员消费统计</div>
            <div>
                会员消费明细表</div>--%>
            <%--  <div onclick="openwin('Member/accumulatepointsConvertQuery.aspx','500','800')">
                积分兑换查询</div>--%>
        </div>
        <div id="cash" style="width: 100px;">
            <div onclick="openwin('Cash/ZhanghuChaxun.aspx','500','800')">
                账户查询</div>
            <!--<div onclick="openwin('Cash/LuRuFeiYong.aspx','500','800')">
                录入费用</div>
            <div onclick="openwin('Cash/JiZhangShouKuan.htm','500','800')">
                记帐收款</div>
            <div onclick="openwin('Cash/KeRenJieZhang.htm','500','800')">
                客人结帐</div>-->
            <div onclick="openwin('Cash/XieYiDanWeiJieSuan.htm','500','800')">
                协议单位结算</div>
            <!--<div onclick="openwin('Cash/BuDaJieZhangDan.htm','500','800')">
                补打结帐单</div>
            <div onclick="openwin('Cash/CheXiaoJieZhang.htm','500','800')">
                撤销退房/结帐</div>-->
            <div onclick="openwin('Cash/BanCiJiaoJie.aspx','500','800')">
                班次结算</div>
            <div onclick="openwin('Cash/No.htm','500','800')">
                押金收取统计表</div>
            <div onclick="openwin('Cash/No.htm','500','800')">
                退房结算统计表</div>
            <div onclick="openwin('Cash/No.htm','500','800')">
                收银员收支汇总报表</div>
            <div onclick="openwin('Cash/No.htm','500','800')">
                收银员收支明细报表</div>
        </div>
        <div id="housestatus" style="width: 100px;">
            <div onclick="openwin('HouseStatus/FangJianLiuLiang.aspx','500','800')">
                房间流量查询</div>
            <div onclick="openwin('HouseStatus/RoomCardSearch.aspx','500','800')">
                房卡查询</div>
            <!--<div>
                即时房态</div>-->
        </div>
        <div id="warehouse" style="width: 100px;">
            <div onclick="openwin('WareHouse/GoodsTypeMgr.htm','500','800')">
                商品类别管理</div>
            <div onclick="openwin('WareHouse/GoodsMgr.htm','500','800')">
                商品资料管理</div>
            <div onclick="openwin('WareHouse/PositionMgr.htm','500','800')">
                货位管理</div>
            <div onclick="openwin('WareHouse/SupplierMgr.htm','500','800')">
                供应商管理</div>
            <div onclick="openwin('WareHouse/JinHuoGuanLi.htm','500','800')">
                进货管理</div>
            <div onclick="openwin('WareHouse/TuiHuoGuanLi.htm','500','800')">
                退货管理</div>
            <div onclick="openwin('WareHouse/Allocation.htm?orderType=1','500','800')">
                出库管理</div>
            <div onclick="openwin('WareHouse/Allocation.htm?orderType=2','500','800')">
                调拨管理</div>
            <div onclick="openwin('WareHouse/PanDianLuRu.htm','500','800')">
                库存盘点</div>
            <div onclick="openwin('WareHouse/PositionGoodsQuery.htm','500','800')">
                货位库存查询</div>
            <div onclick="openwin('WareHouse/JinHuoQuery.htm','500','800')">
                进货单查询</div>
            <div onclick="openwin('WareHouse/TuiHuoQuery.htm','500','800')">
                退货单查询</div>
            <div onclick="openwin('WareHouse/ChukuQuery.htm','500','800')">
                出库查询</div>
            <div onclick="openwin('WareHouse/DiaoBokuQuery.htm','500','800')">
                调拨单查询</div>
            <div onclick="openwin('WareHouse/PanDianQuery.htm','500','800')">
                盘点单查询</div>
            <div onclick="openwin('WareHouse/SaleProfitQuery.htm','500','800')">
                商品销售毛利</div>
            <div onclick="openwin('WareHouse/InOrderQuery.htm','500','800')">
                供应商进货报表</div>
        </div>
        <div id="others" style="width: 100px;">
            <div>
                会议管理</div>
            <div>
                客史管理</div>
            <div>
                物品寄存管理</div>
            <div>
                物品寄存查询</div>
            <div>
                遗失物品管理</div>
            <div>
                遗失物品查询</div>
            <div>
                计算器</div>
            <div>
                万年历</div>
            <div>
                世界时钟</div>
            <div>
                公共信息</div>
            <div>
                留言板</div>
            <div>
                锁定屏幕</div>
            <div>
                重新登录</div>
        </div>
        <div id="report" style="width: 100px;">
            <div>
                预订客人报表</div>
            <div>
                客房部入住日报表</div>
            <div>
                销售员销售统计表</div>
            <div>
                楼层入住情况表</div>
            <div>
                全房间房态表</div>
            <div>
                入住登记统计表</div>
            <div>
                客人续住统计表</div>
            <div>
                退房/离店统计表</div>
            <div>
                在店客人统计表</div>
            <div>
                预计离店客人报表</div>
            <div>
                记帐客人收付款报表</div>
            <div>
                在店/未结客人费用统计表</div>
            <div>
                操作员收银总报表</div>
            <div>
                其他费用统计表</div>
            <div>
                历史客人费用统计表</div>
            <div>
                未结离店客人押金统计表</div>
            <div>
                冲销账报表</div>
            <div>
                押金余额表</div>
            <div>
                调账报表</div>
            <div>
                撤销结账报表</div>
            <div>
                房间费用发生报表</div>
            <div>
                酒店营业报表</div>
            <div>
                营业日报明细表</div>
            <div>
                夜审营业综合报表</div>
            <div>
                合并账单记录</div>
            <div>
                拆分账单记录</div>
            <div>
                转账记录查询</div>
            <div>
                拆分明细账单记录</div>
            <!--<div>会议管理报表</div>
            <div>折扣统计报表</div>
            <div>住宿车辆统计表</div>
            <div>房卡数量统计表</div>
            <div>换房记录查询表</div>
            <div>电话开关查询报表</div>
            <div>会员信息统计表</div>
            <div>商品销售报表</div>
            <div>房态修改记录表</div>
            <div>房价调整情况表</div>
            <div>协议单位信息</div>
            <div>协议单位消费</div>
            <div>协议单位消费明细表</div>
            <div>协议单位佣金明细表</div>-->
        </div>
        <div id="setting" style="width: 100px;">
            <div onclick="openwin('Setting/BlockedMan.aspx','500','800')">
                黑名单管理</div>
            <div onclick="openwin('Setting/RBAC.aspx','500','800')">
                权限管理</div>
            <div onclick="openwin('Setting/KeFangXinXi.aspx','500','800')">
                客房信息</div>
            <div onclick="openwin('Setting/BasicInfo.aspx','500','800')">
                基础信息维护</div>
            <!--<div>
                会议室维护</div>-->
            <div onclick="openwin('Setting/ParameterInfo.aspx','500','800')">
                参数设置</div>
            <div onclick="openwin('Setting/KeMu.aspx','500','800')">
                科目维护</div>
            <div onclick="openwin('FrontDesk/ZK_weihu.aspx','500','800')">
                折扣维护</div>
            <div onclick="openwin('Setting/BasicPayment.aspx','500','800')">
                付款方式</div>
            <div>
                会员卡设置</div>
            <div onclick="openwin('Setting/Partner.htm','500','800')">
                协议单位设置</div>
            <div>
                客史字段维护</div>
            <div onclick="openwin('Setting/Salers.aspx','500','800')">
                销售员维护</div>
            <div>
                数据修复</div>
            <div>
                系统日志查询</div>
            <div>
                修改登陆密码</div>
            <div>
                修改折扣密码</div>
        </div>
        <div id="help" style="width: 100px;">
            <div>
                帮助</div>
            <div>
                代理商设置</div>
            <div>
                软件激活</div>
            <div>
                关于</div>
        </div>
        <div id="bigmenu">
            <a href="javascript:void(0)" onclick="openwin('FrontDesk/YuDingGuanLi.htm','500','800')"
                title="预订管理">
                <img class="icon_bigmenu" src="themes\icons\k1.png" alt="预订管理" /></a> <a href="javascript:void(0)"
                    onclick="openwin('FrontDesk/YuDingChaXun.htm','500','800')" title="预订查询">
                    <img class="icon_bigmenu" src="themes\icons\k2.png" alt="预订查询" /></a> <a href="javascript:void(0)"
                        onclick="openwin('Register/RegisterPage.aspx','553','800')" title="前台接洽">
                        <img class="icon_bigmenu" src="themes\icons\k3.png" alt="前台接洽" /></a>
            <a href="javascript:void(0)" onclick="openwin('FrontDesk/ZaiDianGuanLi.aspx','500','800')"
                title="客人查询">
                <img class="icon_bigmenu" src="themes\icons\k4.png" alt="客人查询" /></a> <a href="Index.htm"
                    title="会员管理">
                    <img class="icon_bigmenu" src="themes\icons\k5.png" alt="会员管理" /></a> <a href="Index.htm"
                        title="会员查询">
                        <img class="icon_bigmenu" src="themes\icons\k6.png" alt="会员查询" /></a>
            <a href="javascript:void(0)" onclick="openwin('Cash/BanCiJiaoJie.aspx','550','850')"
                title="班次交接">
                <img class="icon_bigmenu" src="themes\icons\k7.png" alt="班次交接" /></a> <a href="Index.htm"
                    title="夜审">
                    <img class="icon_bigmenu" src="themes\icons\k8.png" alt="夜审" /></a> <a href="Index.htm"
                        title="消费入账">
                        <img class="icon_bigmenu" src="themes\icons\k9.png" alt="消费入账" /></a>
            <a href="Index.htm" target="_blank" title="房卡查询">
                <img class="icon_bigmenu" src="themes\icons\k10.png" alt="房卡查询" /></a> <a href="javascript:void(0)"
                    onclick="openwin('Cash/ZhanghuChaxun.aspx','500','800')" title="账户查询">
                    <img class="icon_bigmenu" src="themes\icons\k11.png" alt="账户查询" /></a> <a href="Index.htm"
                        title="客人结账">
                        <img class="icon_bigmenu" src="themes\icons\k12.png" alt="客人结账" /></a>
            <a href="Index.htm" target="_blank" title="系统维护">
                <img class="icon_bigmenu" src="themes\icons\k13.png" alt="系统维护" /></a> <a href="Index.htm"
                    title="重新登录">
                    <img class="icon_bigmenu" src="themes\icons\k14.png" alt="重新登录" /></a> <a href="Index.htm"
                        title="退出系统">
                        <img class="icon_bigmenu" src="themes\icons\k15.png" alt="退出系统" /></a>
        </div>
    </div>
    <div data-options="region:'east',split:false,collapsed:false,border:true" style="width: 160px;">
        <%--  <asp:Literal ID="RoomStatus" runat="server" Visible="false"></asp:Literal>--%>
        <table border="0" cellpadding="0" cellspacing="0" style="padding: 5px">
            <tr>
                <td align="left" style="width: 55px">
                    房号
                </td>
                <td align="left">
                    <input class="easyui-validatebox" id="fangjianhao" type="text" style="width: 100px"
                        name="fangjianhao" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 55px">
                    楼栋
                </td>
                <td width="80px">
                    <input class="easyui-combobox" name="Ld" id="Ld" type="text" style="width: 100px"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 55px">
                    楼层
                </td>
                <td width="80px">
                    <input class="easyui-combobox" id="Lc" type="text" style="width: 100px" name="Lc"
                        runat="server" />
                </td>
            </tr>
        </table>
        <p align="center" style="height: 25px; padding: 4px; margin: 0px;">
            <span style="font-size: x-large; font: bolder; height: 25px;">
                <asp:Button ID="allFT" Style="width: 120px; height: 30px;" runat="server" Text="刷新"
                    OnClick="allFT_Click" />
            </span>
        </p>
        <table border="0" cellpadding="0" cellspacing="0" style="padding: 5px">
            <%=SummerStr %>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="padding: 5px">
            <tr>
                <td style="padding: 5px">
                    <img src="Images/lianfang.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;连房
                </td>
                <td style="padding: 5px">
                    <img src="Images/more.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;续住
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <img src="Images/key.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;保密
                </td>
                <td style="padding: 5px">
                    <img src="Images/file.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;协议
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <img src="Images/money.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;不足
                </td>
                <td style="padding: 5px">
                    <img src="Images/tuan.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;团队
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <img src="Images/money2.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;欠费
                </td>
                <td style="padding: 5px">
                    <img src="Images/vip.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;会员
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <img src="Images/willgo.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;预离
                </td>
                <td style="padding: 5px">
                    <img src="Images/lock.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;锁房
                </td>
            </tr>
            <tr>
                <td style="padding: 5px">
                    <img src="Images/clean.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;脏住
                </td>
                <td style="padding: 5px">
                    <img src="Images/chen.png" width="16px" height="16px" />
                </td>
                <td width="60px">
                    0&nbsp;&nbsp;凌晨
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'south',border:true" style="height: 30px; background: #dddddd;
        padding: 5px;">
        <table style="width: 100%">
            <tr>
                <td style="width: 50%;">
                    当前用户：<%=Session["user"].ToString() %>
                </td>
                <td style="width: 50%;" align="right">
                    酒店客房管理系统
                </td>
            </tr>
        </table>
    </div>
    <div data-options="region:'center',border:true">
        <%GetRooms(); %>
    </div>
    </form>
</body>
</html>
