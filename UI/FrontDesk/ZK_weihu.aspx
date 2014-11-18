<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZK_weihu.aspx.cs" Inherits="FrontDesk_ZK_weihu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="Cache-Control" content="no-cache">
    <title>折扣特权维护</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../dateChange.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
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
        });
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div id="container" style="height: 400px; width: 800px;
        margin: 0px;">
            <table id="dg" class="easyui-datagrid" style="padding: 0px; height: 400px" url="ZK_weihuData.aspx?action=read"
                buttons="#toolbar" fitcolumns="true" singleselect="true">
             <%--    <table id="dg" class="easyui-datagrid" style="padding: 0px; width: 800px; height: 400px"
            url="ZK_weihuData.aspx?action=read" toolbar="#toolbar" 
            fitcolumns="true" singleselect="true">--%>
                <thead>
                    <tr>
                           <th data-options="field:'tequanren',width:40">
                    折扣特权人
                    </th>
                    <th data-options="field:'fjzhekou',width:40">
                        客房折扣
                    </th>
                        <th data-options="field:'timeLimite1',width:40">
                   时间限制
                    </th>
                    <th data-options="field:'validDate',width:40">
                      有效期（小时）
                    </th>
                     <th data-options="field:'isMorenZheKou1',width:40">
                     默认
                    </th>
                    </tr>
                </thead>
            </table>
 
        <div id="toolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" onclick="newMember()">
                新增</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                    onclick="editMember()">修改</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        iconcls="icon-remove" onclick="destroyMember()">删除</a> <a href="javascript:void(0)"
                            class="easyui-linkbutton" onclick="clearForm()">返回 </a>
        </div>
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 240px;" closed="true"
            buttons="#dlg-buttons">
            <div style="padding: 10px 0 10px 10px">
                <form id="fm" method="post">
                <table>
                    <tr>
                        <td style="width: 80px">
                            特权人：
                        </td>
                        <td colspan="2">
                            <input class="easyui-combobox" id="CaoCombo" type="text" name="tequanren" />（请首先增加操作员）
                        </td>
                    </tr>
                    <tr>
                        <td>
                            客房折扣：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="fjzhekou" style="width: 60px" />%
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <input type="checkbox"  name="timeLimite" id="timeLimite">&nbsp;&nbsp;时间限制（为折扣后设定时间期限）</input>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            有效期
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="validDate" />
                        </td>
                        <td>
                            小时（折扣在设定的时间后不能再修改）
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2">
                            <input type="checkbox"  name="isMorenZheKou" id="isMorenZheKou">&nbsp;&nbsp;默认折扣（登房时默认用此折扣）</input>
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                </script>
                </form>
            </div>
        </div>
        <div id="dlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMember()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#dlg').dialog('close')">取消</a>
        </div>
    </div>
    <script type="text/javascript">
        function clearForm() {
            this.window.close();
        }
        var url;
        function newMember() {
            $('#dlg').dialog('open').dialog('setTitle', '添加特权人');
            $('#fm').form('clear');
            $('#fm').form('reset');

            url = 'ZK_weihuData.aspx?action=create';


        }

        function editMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑特权人');
                $('#fm').form('load', row);
                if (row.timeLimite1=="是") {
                    $('#timeLimite').attr("checked", 'checked');
                } else {

                    $('#timeLimite').attr("checked", false);
                }

                if (row.isMorenZheKou1 == "是") {
                    $('#isMorenZheKou').attr("checked", 'checked');
                } else {

                    $('#isMorenZheKou').attr("checked", false);
                }
                url = 'ZK_weihuData.aspx?action=update&id=' + row.id;

            }
        }
        function saveMember() {
            $('#fm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('reload');
                    }
                }
            });
        }




        function destroyMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('ZK_weihuData.aspx?action=delete', { id: row.id }, function (result) {

                            if (!result.errorMsg) {
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
