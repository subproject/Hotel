﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LuRuFeiYong.aspx.cs" Inherits="Cash_LuRuFeiYong" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>录入费用</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        #xyfm
        {
            margin: 0;
            padding: 10px 30px;
        }
        .ftitle
        {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }
        .fitem
        {
            margin-bottom: 5px;
        }
        .fitem label
        {
            display: inline-block;
            width: 80px;
        }
    </style>
</head>
<body class="easyui-layout" style="padding:2px">
        <div title="账单信息" class="easyui-panel">
        </div>
        <table style="padding: 10px">
            <tr>
                <td style="width: 80px;padding:5px">
                    账单编号
                </td>
                <td style="width: 160px;padding:5px">
                    <%=AutoID%>
                </td>
                <td style="width: 80px;padding:5px">
                    主账房间
                </td>
                <td style="width: 160px;padding:5px">
                   <%=Fanghao%>
                </td>
            </tr>
            <tr>
                <td style="width: 80px;padding:5px">
                    客户名称
                </td>
                <td style="width: 160px;padding:5px">
                    <%=Xingming%>
                </td>
                <td style="width: 80px;padding:5px">
                    电话
                </td>
                <td style="width: 160px;padding:5px">
                    <%=Dianhua%>
                </td>
            </tr>
        </table>
        <div title="费用明细" class="easyui-panel">
        </div>
        <table id="dg" class="easyui-datagrid" style="padding: 0px" url="RunningListData.aspx?action=readall&orderid=<%=OrderGuid %>"
            toolbar="#dgtbr" rownumbers="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'OrderGuid',width:100" hidden>
                        账号
                    </th>
                    <th data-options="field:'CustomerName',width:100">
                        客户名称
                    </th>
                    <th data-options="field:'RoomNo',width:100">
                        房号
                    </th>
                    <th data-options="field:'KM',width:100">
                        科目
                    </th>
                    <th data-options="field:'Price',width:100">
                        消费金额
                    </th>
                    <th data-options="field:'Deposit',width:100">
                        押金金额
                    </th>
                    <th data-options="field:'Remark',width:100">
                        备注
                    </th>
                    <th data-options="field:'RunningNum',width:100">
                        单据号码
                    </th>
                    <th data-options="field:'RunningNumAuto',width:100">
                        单据号码(自动)
                    </th>
                    <th data-options="field:'RunningTime',width:100" hidden>
                        单据日期
                    </th>
                    <th data-options="field:'Payment',width:100">
                        付款方式
                    </th>
                    <th data-options="field:'Operator',width:100">
                        操作员
                    </th>
                    <th data-options="field:'Status',width:100">
                        状态
                    </th>
                </tr>
            </thead>
        </table>
        <div id="dgtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newPartner()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editPartner()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyPartner()">
                        删除</a>
        </div>
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 35px"
            closed="true" buttons="#dlgbuttons">
            <form id="fm" method="post" novalidate>
            <div class="fitem">
                <label>
                    科目:</label>
                <input class="easyui-combobox" name="KM" data-options="valueField:'ID',textField:'KM',url:'../Setting/KeMuData.aspx?action=read'">
            </div>
            <div class="fitem">
                <label>
                    金额:</label>
                <input name="Price" class="easyui-validatebox" />
            </div>
            <!--<div class="fitem">
                <label>
                    押金费用:</label>
                <input name="Deposit" class="easyui-validatebox" />
            </div>-->
            <div class="fitem">
                <label>
                    付款方式:</label>
                <input class="easyui-combobox" name="Payment" data-options="valueField:'ID',textField:'fkfs',url:'../Setting/BasicInfoData.aspx?module=fkfs&action=read'"/>
            </div>
            <div class="fitem">
                <label>
                    备注:</label>
                <input name="Remark" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    单据号码:</label>
                <input name="RunningNum" class="easyui-validatebox" />
            </div>
            <div class="fitem">
                <label>
                    操作员:</label>
                <input name="Operator" class="easyui-validatebox" />
            </div>
            </form>
        </div>
        <div id="dlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePartner()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#dlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newPartner() {
                $('#dlg').dialog('open').dialog('setTitle', '新增');
                $('#fm').form('clear');
                url = 'RunningListData.aspx?action=create&orderid='+'<%=OrderGuid %>';
            }
            function editPartner() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $('#dlg').dialog('open').dialog('setTitle', '编辑');
                    $('#fm').form('load', row);
                    url = 'RunningListData.aspx?action=update&id=' + row.ID;
                }
            }
            function savePartner() {
                $('#fm').form('submit', {
                    url: url,
                    onSubmit: function () {
                        return $(this).form('validate');
                    },
                    success: function (result) {
                        var result = eval('(' + result + ')');
                        if (result.errorMsg) {
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        } else {
                            $('#dlg').dialog('close');        // close the dialog
                            $('#dg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyPartner() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RunningListData.aspx?action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#dg').datagrid('reload');
                                    // reload the user data
                                } else {
                                    $.messager.show({    // show error message
                                        title: 'Error',
                                        msg: result.errorMsg
                                    });
                                }
                            }, 'json');
                        }
                    });
                }
            }
        </script>
</body>
</html>
