<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TuiFangChaXun.aspx.cs" Inherits="FrontDesk_TuiFangChaXun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>离店客人查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //国籍，默认中国
            $("#GuoJiCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=gj&action=read',
                valueField: 'gj',
                textField: 'gj',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#GuoJiCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#GuoJiCombo").combobox('select', data[1].gj);
                    }
                }
            });
            //国籍，默认中国
            $("#Guoji").combobox({
                url: '../Setting/BasicInfoData.aspx?module=gj&action=read',
                valueField: 'gj',
                textField: 'gj',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Guoji').combobox('getData');
                    if (data.length > 0) {
                        $("#Guoji").combobox('select', data[1].gj);
                    }
                }
            });

            //销售员
            $("#SalersCombo").combobox({
                url: '../Setting/SalersData.aspx?action=read',
                valueField: 'Name',
                textField: 'Name',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#SalersCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#SalersCombo").combobox('select', data[0].Name);
                    }
                }
            });
            //房间级别
            $("#jbCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=fjJB&action=read',
                valueField: 'fjJB',
                textField: 'fjJB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#jbCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#jbCombo").combobox('select', data[0].Name);
                    }
                }
            });
            //操作员
            $("#CaoCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=caozuoyuan&action=read',
                valueField: 'caozuoyuan',
                textField: 'caozuoyuan',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#CaoCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#CaoCombo").combobox('select', data[0].Name);
                    }
                }
            });

            //客人状态
            $("#KrZtCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=KrZt&action=read',
                valueField: 'ID',
                textField: 'KrZt',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#KrZtCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#KrZtCombo").combobox('select', data[0].Name);
                    }
                }
            });

            //客人类别
            $("#KeLeiCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=khlb&action=read',
                valueField: 'KHLB',
                textField: 'KHLB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#KeLeiCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#KeLeiCombo").combobox('select', data[0].KHLB);
                    }
                }
            });
            //付款方式
            $("#FkfsCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=fkfs&action=read',
                valueField: 'fkfs',
                textField: 'fkfs',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#FkfsCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#FkfsCombo").combobox('select', data[0].fkfs);
                    }
                }
            });
        });

        function doSearch() {
            $('#tt').datagrid('load', {
                Begin: $('#Begin').datebox('getValue'), //来店日期
                Name: $('#Name').val(), //客户名称
                Fh: $('#Fh').val(), //房号
                End: $('#End').datebox('getValue'), //离店日期
                FangType: $('#KrZtCombo').datebox('getValue'), //客人状态
                dianhuahaoma: $('#dianhuahaoma').val(), //电话号码
                beizhu: $('#beizhu').val(), //备注
                GuoJiCombo: $('#GuoJiCombo').datebox('getValue'), //国籍
                fangjianjibie: $('#jbCombo').datebox('getValue'), //房间级别
                Chepaihao: $('#Chepaihao').val(), //车牌号 
                FkfsCombo: $('#FkfsCombo').datebox('getValue'), //付款方式
                SalersCombo: $('#SalersCombo').datebox('getValue'), //销售员
                MainNumber: $('#MainNumber').val(), //主帐单号
                CaoCombo: $('#CaoCombo').datebox('getValue'),//操作员 
                KeLeiCombo: $('#KeLeiCombo').datebox('getValue'), //客户类型
                huiyuankahao:$('#huiyuankahao').val(), //会员卡号
                XieyiDanwei:$('#XieyiDanwei').datebox('getValue'),//协议单位
                ZhengjianCard: $('#ZhengjianCard').val(),//证件号码
                ZhengjianAddress: $('#ZhengjianAddress').val(), //证件地址
                      //    ZHName:$('#ZhengjianAddress').val()//账户名称
                MainName: $('#ZHName').val(),  //主帐名称
                RuZhuleixing: $('#RuZhuleixing').datebox('getText')
            });
        }
        function clearForm() {
            this.window.close();
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="离店客人查询">
        <table class="easyui-datagrid" border="false" toolbar="#dgtbr" url="TuiFangChaXunData.aspx"
            pagination="true" singleselect="true" id="tt">
            <thead>
                <tr>
                  <th data-options="field:'fangjianjibie',align:'center'">
                        房间级别
                    </th>
                      <th data-options="field:'ruzhuleixing',align:'center'">
                        入住类型
                    </th>
                    <th data-options="field:'FangJianHao',align:'center'">
                        房间号
                    </th>
                    <th data-options="field:'XingMing',align:'center'">
                        客户名称
                    </th>
                    <th data-options="field:'XingBie',align:'center'">
                        性别
                    </th>
                        <th data-options="field:'kerenzhuangtai',align:'center'">
                        客人状态
                    </th>
                     <th data-options="field:'OnBoardTime',align:'center'">
                        到店日期
                    </th>
                    <th data-options="field:'LeaveTime',align:'center'">
                        离店日期
                    </th>
                     <th data-options="field:'YaJin',align:'center'">
                       押金
                    </th>
                   <th data-options="field:'YuanFangJia',align:'center'">
                        房间单价
                    </th>
                     <th data-options="field:'ShijiFangjia',align:'center'">
                        实际房价
                    </th>
                     <th data-options="field:'fukuanfangshi',align:'center'">
                        付款方式
                    </th>
                     <th data-options="field:'Tequanren',align:'center'">
                        特权人
                    </th>
                    <th data-options="field:'kerenleibie',align:'center'">
                        客户类型
                    </th>
                     <th data-options="field:'ZheKouLv',align:'center'">
                        折扣率
                    </th>
                       <th data-options="field:'ZhuCong',align:'center'">
                        主从
                    </th>
                    <th data-options="field:'MainFH',align:'center'">
                        主帐房号
                    </th>
                    <th data-options="field:'MainOrderMan',align:'center'">
                        主帐名称
                    </th>
                    <th data-options="field:'ZhengjianLeixing',align:'center'">
                        证件类别
                    </th>
                    <th data-options="field:'ZhengjianHaoma',align:'center'">
                        证件号码
                    </th>
                    <th data-options="field:'dianhuahaoma',align:'center'">
                        电话号码
                    </th>
                  <%--   <th data-options="field:'KerenLeibie',align:'center'">
                      客户类型
                    </th>--%>
                    <th data-options="field:'huiyuanka',align:'center'">
                        会员卡号
                    </th>
                    <th data-options="field:'ZhengjianDizhi',align:'center'">
                        证件地址
                    </th>
                    <th data-options="field:'xieyidanwei',align:'center'">
                      协议单位
                    </th>
                    <th data-options="field:'baomi',align:'center'">
                        保密
                    </th>
                    <th data-options="field:'beizhu',align:'center'">
                        备注
                    </th>
                        <th data-options="field:'guoji',align:'center'">
                      国籍
                    </th>
                    <th data-options="field:'suoshutuandui',align:'center'">
                        所属团队
                    </th>
                    <th data-options="field:'CaoZuoYuan',align:'center'">
                        操作员
                    </th>
                        <th data-options="field:'xiaoshouyuan',align:'center'">
                      销售员
                    </th>
                    <th data-options="field:'ShenfengDiZhi',align:'center'">
                       身份证地址
                    </th>
                   
                    <th data-options="field:'YDDanHao',align:'center'">
                        预订单号
                    </th>
                        <th data-options="field:'YDremark',align:'center'">
                      预订单备注
                    </th>
                    <th data-options="field:'YDDanWei',align:'center'">
                        预订单位
                    </th>
                    <th data-options="field:'YDKeHu',align:'center'">
                        预订客户
                    </th>
                   <th data-options="field:'YDTel',align:'center'">
                        预订人电话
                    </th>
                        <th data-options="field:'YDDate',align:'center'">
                      预订日期
                    </th>
                    <th data-options="field:'YDCaoZuoYuan',align:'center'">
                        预定操作员
                    </th>
                    <th data-options="field:'DingJin',align:'center'">
                        订金
                    </th>
                </tr>
            </thead>
        </table>
        <div id="dgtbr" style="padding: 5px">
            <table>
                <tr>
                    <td>
                        <input type="radio" name="item" value="A" checked="true" />散客查询
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="MoreLM()">显示过滤</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        账户名称：
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ZHName" name="ZHName" />
                    </td>
                    <td>
                        电话号码</td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="dianhuahaoma" name="dianhuahaoma" />
                    </td>
                    <td>
                        备注</td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="beizhu" name="beizhu" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        客户名称
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Name" name="Name" />
                    </td>
                    <td style="width: 80px;">
                        客人状态
                    </td>
                    <td style="width: 160px;">
                          <input class="easyui-combobox" id="KrZtCombo" name="KrZt" />
                    </td>
                    <td style="width: 80px;">
                        客户类型
                    </td>
                    <td style="width: 160px;">
                       <input class="easyui-combobox" id="KeLeiCombo" name="KerenLeibie">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        车牌号 </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Chepaihao" name="Chepaihao" />
                    </td>
                    <td style="width: 80px;">
                        证件号码
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ZhengjianCard" name="ZhengjianCard" />
                    </td>
                    <td style="width: 80px;">
                        来店日期
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-datetimebox" name="Begin" id="Begin" style="width: 140px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        房间级别</td>
                    <td style="width: 160px;">
                         <input class="easyui-combobox" id="jbCombo" type="text" 
                            name="FangjianJB" />
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        入住类型：
                    </td>
                    <td style="width: 160px;">
                        <select class="easyui-combobox" id="RuZhuleixing" name="RuZhuleixing" style="width: 90px">
                            <option></option>
                            <option value="1">正常</option>
                            <option value="2">免费</option>
                            <option value="3">长包</option>
                            <option value="4">钟点</option>
                        </select>
                         <%--<input class="easyui-combobox" id="Text1" type="text" name="RuZhuleixing" />--%>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        国籍
                    </td>
                    <td style="width: 160px;">
                      <input class="easyui-combobox" id="GuoJiCombo" type="text" name="Guoji" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px;">
                        房号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Fh" name="Fh" />
                    </td>
                    <td style="width: 80px;">
                        销售人员
                    </td>
                    <td style="width: 160px;">
                         <input class="easyui-combobox" id="SalersCombo" type="text" name="XiaoShouYuan" />
                    </td>
                    <td style="width: 80px;">
                        离店日期
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-datetimebox" name="End" id="End" style="width: 140px" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        主帐房号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="MainNumber" name="MainNumber" />
                    </td>
                    <td style="width: 80px;">
                        操作员</td>
                    <td style="width: 160px;">
                         <input class="easyui-combobox" id="CaoCombo" type="text" 
                            name="Caozuoyuan" />
                    </td>
                    <td style="width: 80px;">
                        住址
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ZhengjianAddress" name="ZhengjianAddress" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        协议单位
                    </td>
                    <td style="width: 160px;">
           <input class="easyui-combobox" name="XieyiDanwei" data-options="valueField:'Name',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'" id="XieyiDanwei">
                    </td>
                    <td style="width: 80px;">
                        付款方式
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" id="FkfsCombo" name="FukuanFangshi" />
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        会员卡号</td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="huiyuankahao" name="huiyuankahao" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        预订单号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="yudingdanhao" name="yudingdanhao" />
                    </td>
                    <td style="width: 80px;">
                        预订客户：
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ZheKou" name="ZheKou" />
                    </td>
                    <td style="width: 80px;">
                        预订电话：
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ShiJiFree" name="ShiJiFree" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="doSearch()">检索</a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div id="dgBtn">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width:100px">
                      <a href="javascript:void(0)" class="easyui-linkbutton">换房记录  </a>
                    </td>
                     <td style="width:100px">
                         <a href="javascript:void(0)" class="easyui-linkbutton">账户明细  </a>
                     </td>
                       <td style="width:100px">
                             <a href="javascript:void(0)" class="easyui-linkbutton">操作记录  </a></td>
                         <td style="width:80px">
                                      <a href="javascript:void(0)" class="easyui-linkbutton"> 导    出 </a></td>
                           <td style="width:120px">
                                               <a href="javascript:void(0)" class="easyui-linkbutton">房价调整表</a></td>
                             <td style="width:200px">
                                                        <a href="javascript:void(0)" class="easyui-linkbutton">随客查询  </a></td>
                               <td style="width:100px">
                                                            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取    消  </a></td>
                </tr>
            </table>
         
                
        </div>
    </div>
    
    <div id="dlg" class="easyui-dialog" style="width: 450px; height: 365px;" closed="true"
        buttons="#dlg-buttons">
        <div style="padding: 10px 0 10px 10px">
            <form id="fm" method="post">
            <table>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="fangjianJB" />房间级别
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="shijizujin" />实际租金
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="xiaoshouyuan" />销售员
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="yudingriqi" />预订日期
                    </td>
                   
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="ruzhuleixing" />入住类型
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="fukuanfangshi" />付款方式
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="iddizhi" />身份证地址
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="YDcaozuoyuan" />预订操作员
                    </td>
                    
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="fangjianhao" />房间号
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="tequanren" />特权人
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="kerenleibie" />客户类型
                    </td>
                    <td>
                        <input name="item" type="checkbox" value="dingjin" />订金</td>
               
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="kehumingcheng" />客户名称
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhekoulv" />折扣率
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="huiyuankahao" />会员卡号
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="dianhuahaoma" />电话号码</td>
                   
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="xingbie" />性别
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhucongzhang" />主从帐
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="xieyidanwei" />协议单位
                    </td>
                    <td>
                
                        <input type="checkbox" name="item" value="caozuoyuan" />操作员
                                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="kerenzhuangtai" />客人状态
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhuzhangfanghao" />主帐房号
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="baomi" />保密
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="yudingdanhao" />预订单号
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="daodianriqi" />到店日期
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhuzhangmingcheng" />主帐名称
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="beizhu" />备注
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="YDbeizhu" />预订单备注
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="lidianriqi" />离店日期
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhengjianleibie" />证件类别
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="guoji" />国籍
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="YDdanwei" />预订单位
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="yajin" />押金
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="zhengjianhaoma" />证件号码
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="suoshutuandui" />所属团队
                    </td>
                    <td>
                        <input type="checkbox" name="item" value="yudingkehu" />预订客户
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="checkbox" name="item" value="fangjiandanjia" />房间单价
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        <input type="checkbox" name="item" value="YDdianhua" />预订人电话
                        
                    </td>
                </tr>
                <tr><td>
                &nbsp;
                </td></tr>
                <tr>
                <td>
                 <input type="checkbox" id="CheckAll" name="item" onclick="cli()" /> 全选
                </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <table>
            <tr>
                <td>
                    <%--<input type="checkbox" id="CheckAll" name="item" onclick="cli(item)" /> 全选--%>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveCheckAll()">
                        确定</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
                        取消</a>
                </td>
            </tr>
        </table>
    </div>
    <script type="text/javascript">
        function MoreLM() {
            $('#dlg').dialog('open').dialog('setTitle', '客人查询过滤设置');
            $('#fm').form('clear');
            $('#fm').form('reset');
            url = '';
        }
        function cli() {
            var collid = document.getElementById("CheckAll");
            var coll = document.getElementsByName("item");
            alert(coll+collid.checked);
            if (collid.checked) {
                for (var i = 0; i < coll.length; i++)
                    coll[i].checked = true;
            } else {
                for (var i = 0; i < coll.length; i++)
                    coll[i].checked = false;
            }
        }

        function saveCheckAll() {

        }
    </script>
</body>
</html>
