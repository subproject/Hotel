<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RBAC.aspx.cs" Inherits="Setting_RBAC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        #mfm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #ufm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #afm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #rfm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #Rfm
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
<body class="easyui-layout">
	<div class="easyui-tabs" style="width:800px;height:500px;padding:2px">
		<div title="模块" style="padding:0px">
        <table id="mdg" class="easyui-datagrid" style="padding:0px"
            url="RBACData.aspx?module=module&action=read" toolbar="#mtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'ModuleTitle',width:100">
                        模块名称
                    </th>
                </tr>
            </thead>
        </table>
        <div id="mtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newModule()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editModule()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyModule()">
                        删除</a>
        </div>
        <div id="mdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#mbuttons">
            <form id="mfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    模块名称:</label>
                <input name="ModuleTitle" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="mbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveModule()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#mdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newModule() {
                $('#mdlg').dialog('open').dialog('setTitle', '新增');
                $('#mfm').form('clear');
                url = 'RBACData.aspx?module=module&action=add';
            }
            function editModule() {
                var row = $('#mdg').datagrid('getSelected');
                if (row) {
                    $('#mdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#mfm').form('load', row);
                    url = 'RBACData.aspx?module=module&action=update&id=' + row.ID;
                }
            }
            function saveModule() {
                $('#mfm').form('submit', {
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
                            $('#mdlg').dialog('close');        // close the dialog
                            $('#mdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyModule() {
                var row = $('#mdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RBACData.aspx?module=module&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#mdg').datagrid('reload');
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
        </div>
		<div title="操作" style="padding:0px">
        <table id="adg" class="easyui-datagrid" style="padding:0px"
            url="RBACData.aspx?module=action&action=read" toolbar="#atbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'ModuleID',width:100">
                        所属模块
                    </th>
                    <th data-options="field:'ActionTitle',width:100">
                        操作
                    </th>
                </tr>
            </thead>
        </table>
        <div id="atbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newAction()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editAction()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyAction()">
                        删除</a>
        </div>
        <div id="adlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#adlgbuttons">
            <form id="afm" method="post" novalidate>
            <div class="fitem">
                <label>
                    所属模块:</label>
                <input name="ModuleID" class="easyui-combobox" dataoption="url:'RBACData.aspx?module=module&action=read'" required="true">
                </div>
            <div  class="fitem">
                <label>
                    操作:</label>
                <input name="ActionTitle" class="easyui-validatebox" required="true">     
            </div>
            </form>
        </div>
        <div id="adlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveAction()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#adlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newAction() {
                $('#adlg').dialog('open').dialog('setTitle', '新增');
                $('#afm').form('clear');
                url = 'RBACData.aspx?module=action&action=add';
            }
            function editAction() {
                var row = $('#adg').datagrid('getSelected');
                if (row) {
                    $('#adlg').dialog('open').dialog('setTitle', '编辑');
                    $('#afm').form('load', row);
                    url = 'RBACData.aspx?module=action&action=update&ID=' + row.ID;
                }
            }
            function saveAction() {
                $('#afm').form('submit', {
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
                            $('#adlg').dialog('close');        // close the dialog
                            $('#adg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyAction() {
                var row = $('#adg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RBACData.aspx?module=action&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#adg').datagrid('reload');
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
        </div>
		<div title="角色" style="padding:0px">
        <table id="rdg" class="easyui-datagrid" style="padding:0px"
            url="RBACData.aspx?module=role&action=read" toolbar="#rtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'RoleName',width:100">
                        角色
                    </th>
                </tr>
            </thead>
        </table>
        <div id="rtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newRole()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editRole()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyRole()">
                        删除</a>
        </div>
        <div id="rdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#rdlgbuttons">
            <form id="rfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    角色:</label>
                <input name="RoleName" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="rdlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveRole()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#rdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newRole() {
                $('#rdlg').dialog('open').dialog('setTitle', '新增');
                $('#rfm').form('clear');
                url = 'RBACData.aspx?module=role&action=add';
            }
            function editRole() {
                var row = $('#rdg').datagrid('getSelected');
                if (row) {
                    $('#rdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#rfm').form('load', row);
                    url = 'RBACData.aspx?module=role&action=update&ID=' + row.ID;
                }
            }
            function saveRole() {
                $('#rfm').form('submit', {
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
                            $('#rdlg').dialog('close');        // close the dialog
                            $('#rdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyRole() {
                var row = $('#rdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RBACData.aspx?module=role&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#rdg').datagrid('reload');
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
        </div>
		<div title="用户" style="padding:0px">
        <table id="udg" class="easyui-datagrid" style="padding:0px"
            url="RBACData.aspx?module=user&action=read" toolbar="#utbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'UserName',width:100">
                        用户名
                    </th>
                    <th data-options="field:'Password',width:100">
                        密码
                    </th>
                    <th data-options="field:'RoleID',width:100">
                        角色
                    </th>
                </tr>
            </thead>
        </table>
        <div id="utbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newUser()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editUser()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyUser()">
                        删除</a>
        </div>
        <div id="udlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#udlgbuttons">
            <form id="ufm" method="post" novalidate>
            <div class="fitem">
                <label>
                    所属角色:</label>
                <input name="RoleID" class="easyui-validatebox" required="true">
            </div>
            <div class="fitem">
                <label>
                    用户名:</label>
                <input name="UserName" class="easyui-validatebox" required="true">
            </div>
            <div class="fitem">
                <label>
                    密码:</label>
                <input name="Password" class="easyui-validatebox" required="true">
            </div>
            </form>
        </div>
        <div id="udlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveUser()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#udlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newUser() {
                $('#udlg').dialog('open').dialog('setTitle', '新增');
                $('#ufm').form('clear');
                url = 'RBACData.aspx?module=user&action=add';
            }
            function editUser() {
                var row = $('#udg').datagrid('getSelected');
                if (row) {
                    $('#udlg').dialog('open').dialog('setTitle', '编辑');
                    $('#ufm').form('load', row);
                    url = 'RBACData.aspx?module=user&action=update&ID=' + row.ID;
                }
            }
            function saveUser() {
                $('#ufm').form('submit', {
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
                            $('#udlg').dialog('close');        // close the dialog
                            $('#udg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyUser() {
                var row = $('#udg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RBACData.aspx?module=user&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#udg').datagrid('reload');
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
        </div>
		<div title="权限分配" style="padding:0px">
        <table id="Rdg" class="easyui-datagrid" style="padding:0px"
            url="RBACData.aspx?module=rights&action=read" toolbar="#Rtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'ModuleID',width:100">
                        模块
                    </th>
                    <th data-options="field:'ActionID',width:100">
                        操作
                    </th>
                    <th data-options="field:'RoleID',width:100">
                        角色
                    </th>
                    <th data-options="field:'Enable',width:100">
                        启用
                    </th>
                </tr>
            </thead>
        </table>
        <div id="Rtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newRight()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editRight()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyRight()">
                        删除</a>
        </div>
        <div id="Rdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#Rdlgbuttons">
            <form id="Rfm" method="post" novalidate>
            <div class="fitem">
                <label>
                    模块:</label>
                <input name="ModuleID" class="easyui-validatebox" required="true">
            </div>
            <div class="fitem">
                <label>
                    操作:</label>
                <input name="ActionID" class="easyui-validatebox" required="true">
            </div>
            <div class="fitem">
                <label>
                    角色:</label>
                <input name="RoleID" class="easyui-validatebox" required="true">
            </div>
            <div class="fitem">
                <label>
                    启用:</label>
                <input name="Enable" class="easyui-validatebox" required="true">
            </div>
            </form>
        </div>
        <div id="Rdlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveRight()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#Rdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newRight() {
                $('#Rdlg').dialog('open').dialog('setTitle', '新增');
                $('#Rfm').form('clear');
                url = 'RBACData.aspx?module=rights&action=add';
            }
            function editRight() {
                var row = $('#Rdg').datagrid('getSelected');
                if (row) {
                    $('#Rdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#Rfm').form('load', row);
                    url = 'RBACData.aspx?module=rights&action=update&ID=' + row.ID;
                }
            }
            function saveRight() {
                $('#Rfm').form('submit', {
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
                            $('#Rdlg').dialog('close');        // close the dialog
                            $('#Rdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyRight() {
                var row = $('#Rdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('RBACData.aspx?module=rights&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#Rdg').datagrid('reload');
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
        </div>
    </div>
</body>
</html>
