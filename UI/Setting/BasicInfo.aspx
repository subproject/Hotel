<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicInfo.aspx.cs" Inherits="Setting_BasicInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
</head>
<body class="easyui-layout">
	<div class="easyui-tabs" style="width:800px;height:500px;padding:2px">
		<div title="付款方式" style="padding:0px">
        <table id="fkfs" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=fkfs&action=read" toolbar="#fkfstbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'fkfs',width:100">
                        付款方式
                    </th>
                </tr>
            </thead>
        </table>
        <div id="fkfstbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newFKFS()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editFKFS()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroy()">
                        删除</a>
        </div>
        <div id="fkfsdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#fkfsbuttons">
            <form id="fkfsfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    付款方式:</label>
                <input name="fkfs" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="fkfsbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveFKFS()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#fkfsdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newFKFS() {
                $('#fkfsdlg').dialog('open').dialog('setTitle', '新增');
                $('#fkfsfm').form('clear');
                url = 'BasicInfoData.aspx?module=fkfs&action=create';
            }
            function editFKFS() {
                var row = $('#fkfs').datagrid('getSelected');
                if (row) {
                    $('#fkfsdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#fkfsfm').form('load', row);
                    url = 'BasicInfoData.aspx?module=fkfs&action=update&id=' + row.ID;
                }
            }
            function saveFKFS() {
                $('#fkfsfm').form('submit', {
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
                            $('#fkfsdlg').dialog('close');        // close the dialog
                            $('#fkfs').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroy() {
                var row = $('#fkfs').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=fkfs&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#fkfs').datagrid('reload');
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
		<div title="国籍" style="padding:0px">
        <table id="gjdg" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=gj&action=read" toolbar="#gjtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'gj',width:100">
                        国籍
                    </th>
                </tr>
            </thead>
        </table>
        <div id="gjtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newGJ()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editGJ()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyGJ()">
                        删除</a>
        </div>
        <div id="gjdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#gjdlgbuttons">
            <form id="gjfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    国籍:</label>
                <input name="gj" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="gjdlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveGJ()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#gjdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newGJ() {
                $('#gjdlg').dialog('open').dialog('setTitle', '新增');
                $('#gjfm').form('clear');
                url = 'BasicInfoData.aspx?module=gj&action=create';
            }
            function editGJ() {
                var row = $('#gjdg').datagrid('getSelected');
                if (row) {
                    $('#gjdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#gjfm').form('load', row);
                    url = 'BasicInfoData.aspx?module=gj&action=update&ID=' + row.ID;
                }
            }
            function saveGJ() {
                $('#gjfm').form('submit', {
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
                            $('#gjdlg').dialog('close');        // close the dialog
                            $('#gjdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyGJ() {
                var row = $('#gjdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=gj&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#gjdg').datagrid('reload');
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
		<div title="客户类别" style="padding:0px">
        <table id="khlbdg" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=khlb&action=read" toolbar="#khlbtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'KHLB',width:100">
                        客户类别
                    </th>
                </tr>
            </thead>
        </table>
        <div id="khlbtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newKHLB()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editKHLB()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyKHLB()">
                        删除</a>
        </div>
        <div id="khlbdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#khlbdlgbuttons">
            <form id="khlbfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    客户类别:</label>
                <input name="kl" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="khlbdlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveKHLB()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#khlbdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newKHLB() {
                $('#khlbdlg').dialog('open').dialog('setTitle', '新增');
                $('#khlbfm').form('clear');
                url = 'BasicInfoData.aspx?module=khlb&action=create';
            }
            function editKHLB() {
                var row = $('#khlbdg').datagrid('getSelected');
                if (row) {
                    $('#khlbdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#khlbfm').form('load', row);
                    url = 'BasicInfoData.aspx?module=khlb&action=update&ID=' + row.ID;
                }
            }
            function saveKHLB() {
                $('#khlbfm').form('submit', {
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
                            $('#khlbdlg').dialog('close');        // close the dialog
                            $('#khlbdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyKHLB() {
                var row = $('#khlbdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=khlb&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#khlbdg').datagrid('reload');
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
		<div title="证件类型" style="padding:0px">
        <table id="zjdg" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=zjlx&action=read" toolbar="#zjtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'ZJLX',width:100">
                        证件类别
                    </th>
                </tr>
            </thead>
        </table>
        <div id="zjtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newZJLX()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editZJLX()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyZJLX()">
                        删除</a>
        </div>
        <div id="zjdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#zjdlgbuttons">
            <form id="zjfm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    证件类别:</label>
                <input name="zjlx" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="zjdlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveZJLX()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#zjdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newZJLX() {
                $('#zjdlg').dialog('open').dialog('setTitle', '新增');
                $('#zjfm').form('clear');
                url = 'BasicInfoData.aspx?module=zjlx&action=create';
            }
            function editZJLX() {
                var row = $('#zjdg').datagrid('getSelected');
                if (row) {
                    $('#zjdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#zjfm').form('load', row);
                    url = 'BasicInfoData.aspx?module=zjlx&action=update&ID=' + row.ID;
                }
            }
            function saveZJLX() {
                $('#zjfm').form('submit', {
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
                            $('#zjdlg').dialog('close');        // close the dialog
                            $('#zjdg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyZJLX() {
                var row = $('#zjdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=zjlx&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#zjdg').datagrid('reload');
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
		<div title="预订方式" style="padding:0px">
        <table id="yddg" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=ydway&action=read" toolbar="#ydtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'AutoID',width:100">
                        ID
                    </th>
                    <th data-options="field:'Way',width:100">
                        预订方式
                    </th>
                </tr>
            </thead>
        </table>
        <div id="ydtbr">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newYDWay()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editYDWay()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyYDWay()">
                        删除</a>
        </div>
        <div id="yddlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#yddlgbuttons">
            <form id="ydfrm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    预订方式:</label>
                <input name="ydway" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="yddlgbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveYDWay()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#yddlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newYDWay() {
                $('#yddlg').dialog('open').dialog('setTitle', '新增');
                $('#ydfrm').form('clear');
                url = 'BasicInfoData.aspx?module=ydway&action=create';
            }
            function editYDWay() {
                var row = $('#yddg').datagrid('getSelected');
                if (row) {
                    $('#yddlg').dialog('open').dialog('setTitle', '编辑');
                    $('#ydfrm').form('load', row);
                    url = 'BasicInfoData.aspx?module=ydway&action=update&ID=' + row.AutoID;
                }
            }
            function saveYDWay() {
                $('#ydfrm').form('submit', {
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
                            $('#yddlg').dialog('close');        // close the dialog
                            $('#yddg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyYDWay() {
                var row = $('#yddg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=ydway&action=delete', { id: row.AutoID }, function (result) {
                                if (result.success) {
                                    $('#yddg').datagrid('reload');
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

        <div title="付现方式" style="padding:0px">
        <table id="fuxiandg" class="easyui-datagrid" style="padding:0px"
            url="BasicInfoData.aspx?module=hxfs&action=read" toolbar="#fuxiantoolbar" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        ID
                    </th>
                    <th data-options="field:'hxfsName',width:100">
                        付现方式
                    </th>
                </tr>
            </thead>
        </table>
        <div id="fuxiantoolbar">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newHXFS()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editHXFS()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyHXFS()">
                        删除</a>
        </div>
        <div id="hxfsdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#hxfsbuttons">
            <form id="hxfsfrm" method="post" novalidate>
            <div class="fitem">
            <p>&nbsp;</p>
                <label>
                    付现方式:</label>
                <input name="hxfs" class="easyui-validatebox" required="true">
            <p>&nbsp;</p>
            </div>
            </form>
        </div>
        <div id="hxfsbuttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveHXFS()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#hxfsdlg').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            var url;
            function newHXFS() {
                $('#hxfsdlg').dialog('open').dialog('setTitle', '新增');
                $('#hxfsfrm').form('clear');
                url = 'BasicInfoData.aspx?module=hxfs&action=create';
            }
            function editHXFS() {
                var row = $('#fuxiandg').datagrid('getSelected');
                if (row) {
                    $('#hxfsdlg').dialog('open').dialog('setTitle', '编辑');
                    $('#hxfsfrm').form('load', row);
                    url = 'BasicInfoData.aspx?module=hxfs&action=update&ID=' + row.ID;
                }
            }
            function saveHXFS() {
                $('#hxfsfrm').form('submit', {
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
                            $('#hxfsdlg').dialog('close');        // close the dialog
                            $('#fuxiandg').datagrid('reload');    // reload the user data
                        }
                    }
                });
            }
            function destroyHXFS() {
                var row = $('#fuxiandg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('BasicInfoData.aspx?module=hxfs&action=delete', { id: row.ID }, function (result) {
                                if (result.success) {
                                    $('#fuxiandg').datagrid('reload');
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
