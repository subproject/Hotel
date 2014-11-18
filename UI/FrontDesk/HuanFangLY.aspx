<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuanFangLY.aspx.cs" Inherits="FrontDesk_HuanFangLY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta http-equiv="Cache-Control" content="no-cache">
    <title>换房理由管理</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript" src="../dateChange.js"></script>
</head>
<body class="easyui-layout" style="padding: 2px">
 <div id="container" data-options="handle:'#title'"  style="height: 400px; width: 300px;margin: 0px; ">
    <div class="easyui-panel" title="换房理由">
        <table id="dg" class="easyui-datagrid" style="padding: 0px; width: 298px; height: 400px"
            url="HuanFangLYData.aspx?action=read" toolbar="#toolbar" 
            fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'AutoID',width:20">
                    序号
                    </th>
                    <th data-options="field:'Content',width:140">
                        换房理由
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
            onclick="newMember()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                iconcls="icon-edit" plain="true" onclick="editMember()">修改</a> <a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyMember()">
                    删除</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 250px; height: 120px;" closed="true"
        buttons="#dlg-buttons">
        <div style="padding: 10px 0 10px 10px">
            <form id="fm" method="post">
            <table>
                <tr>
                    <td>
                        换房理由：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="Content" data-options="required:true" />
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
       
        var url;
        function newMember() {
            $('#dlg').dialog('open').dialog('setTitle', '新增换房理由');
            $('#fm').form('clear');
            $('#fm').form('reset');

            url = 'HuanFangLYData.aspx?action=create';
            $('#AutoID').show();
            $('#Content').show();

        }

        function editMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑换房理由');
                $('#fm').form('load', row);

                url = 'HuanFangLYData.aspx?action=update&AutoID=' + row.AutoID;

                $('#AutoID').show();
                $('#Content').show();
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
                        $.post('HuanFangLYData.aspx?action=delete', {AutoID: row.AutoID }, function (result) {

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
