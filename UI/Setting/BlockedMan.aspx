<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlockedMan.aspx.cs" Inherits="Setting_BlockedMan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>黑名单</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        #fm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #kffm
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
<body class="easyui-layout" style="padding: 2px">
    <div title="黑名单管理" class="easyui-panel">
    </div>
    <table id="dg" class="easyui-datagrid" url="BlockedManData.aspx?action=read" toolbar="#toolbar"
        pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
        <thead>
            <tr>
                <th data-options="field:'AutoID',width:100" hidden>
                    编号
                </th>
                <th data-options="field:'Name',width:100">
                    姓名
                </th>
                <th data-options="field:'IDCardNo',width:100">
                    身份证
                </th>
                <th data-options="field:'Remark',width:100">
                    单位/住址
                </th>
            </tr>
        </thead>
    </table>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
            onclick="newItem()">新增</a><a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyItem()">
                    删除</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 280px; padding: 10px 20px"
        closed="true" buttons="#dlg-buttons">
        <div class="ftitle">
            黑名单</div>
        <form id="fm" method="post" novalidate>
        <input name="AutoID" class="easyui-validatebox" type="hidden">
        <div class="fitem">
            <label>
                姓名:</label>
            <input name="Name" class="easyui-validatebox" required="true">
        </div>
        <div class="fitem">
            <label>
                身份证:</label>
            <input name="IDCardNo" class="easyui-validatebox" required="true">
        </div>
        <div class="fitem">
            <label>
                单位/住址:</label>
            <input name="Remark" class="easyui-validatebox">
        </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>
    <script type="text/javascript">
        var url;
        function newItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#fm').form('clear');
            url = 'BlockedManData.aspx?action=create';
        }
        function saveItem() {
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
        function destroyItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('BlockedManData.aspx?action=delete', { AutoID: row.AutoID }, function (result) {
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
