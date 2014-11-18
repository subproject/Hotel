<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberRoomDesign.aspx.cs" Inherits="MemberRoomDesign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员类型折扣设置</title>
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
<body class="easyui-layout">
   <div data-options="region:'center'" style="padding: 1px">
        <div id="divtitle" name="divtitle" title="会员类型折扣设置" class="easyui-panel">
        </div>
        <table id="dg" class="easyui-datagrid" style="padding: 0px" 
            toolbar="#dgtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'id',width:100,hidden:true">
                        ID
                    </th>
                     <th data-options="field:'CardTypeId',width:100,hidden:true">
                        CardTypeId
                    </th>
                    <th data-options="field:'f_jb',width:100">
                        房间类型
                    </th>
                
                    <th data-options="field:'f_dj',width:100">
                        标准房价
                    </th>
                    
                     <th data-options="field:'z_dj',width:100">
                        实际房价
                    </th>
                
                    <th data-options="field:'ZdFj',width:100">
                        钟点房
                    </th>
                     <th data-options="field:'Lcf',width:100">
                        凌晨房
                    </th>
                  <th data-options="field:'Memo',width:100">
                        备注
                    </th>

                </tr>
            </thead>
        </table>
        <div id="dgtbr">

            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newRoomDesign()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editRoomDesign()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyRoomDesign()">
                        删除</a>
        </div>
        
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 10px 20px"
            closed="true" buttons="#dlgbuttons">
            <form id="fm" method="post" novalidate>
            <div class="fitem">
                <label>
                   房间类型:</label>
                    <input type="hidden" name="CardTypeId2" id="CardTypeId2"/>
                <input name="f_jb" id="f_jb" class="easyui-combobox" 
                   required="true">
            </div>
            
            <div class="fitem">
                <label>
                   标准房价:</label>
                <input name="f_dj" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                   实际房价:</label>
                <input name="z_dj" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                   钟点房:</label>
                <input name="ZdFj" class="easyui-validatebox" id="ZdFj" readonly="true">

                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                        onclick="newZd()">...</a>

            </div>
            <div class="fitem">
                <label>
                   凌晨房:</label>
                <input name="Lcf" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                   备注:</label>
                <input name="Memo" class="easyui-validatebox">
            </div>
           

 
      
        </form>
    </div>
               <div id="dlgbuttons">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveRoomDesign()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

        
    
      

    <script type="text/javascript">
        var url;

        function newZd() {
            var t = $('#f_jb').datebox('getValue');
            if (t) {
                $('#ZD').dialog('open').dialog('setTitle', '【会员卡】-【'+t +'】钟点房房价方案');
                $.get('../Setting/ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                    $("#ZDdg").datagrid("loadData", result);
                }, 'json');

            }
        }

        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
        $(document).ready(function () {
            $('#CardTypeId2').val(decodeURI(getUrlVars()["CardType"]));
            $('#dg').datagrid({ url: 'MemberCardData.aspx?action=GetRoomDesign&CardType=' + "" + getUrlVars()["CardType"] + "" });
            
            $("#f_jb").combobox({
                url: 'MemberCardData.aspx?action=fjlx&CardType=' + "" + getUrlVars()["CardType"] + "",
                valueField: 'f_jb',
                textField: 'f_jb',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#f_jb').combobox('getData');
                    if (data.length > 0) {
                        $("#f_jb").combobox('select', data[0].f_jb);
                    }
                }
            });
             
        });
        function newRoomDesign() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#f_jb').val('');
            $('#f_dj').val('');
            $('#z_dj').val('');
            $('#ZdFj').val('');
            $('#Lcf').val('');
            $('#Memo').val('');
             
            url = 'MemberCardData.aspx?action=RoomDesignCreate';

            $("#f_jb").combobox({
                url: 'MemberCardData.aspx?action=fjlx&CardType=' + "" + getUrlVars()["CardType"] + "",
                valueField: 'f_jb',
                textField: 'f_jb',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#f_jb').combobox('getData');
                    if (data.length > 0) {
                        $("#f_jb").combobox('select', data[0].f_jb);
                    }
                }
            });
        }
        function editRoomDesign() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
//                $("#f_jb").combobox({
//                    url: 'MemberCardData.aspx?action=fjlx&CardType=' + "" + getUrlVars()["CardType"] + "",
//                    valueField: 'f_jb',
//                    textField: 'f_jb',
//                    editable: false,
//                    onLoadSuccess: function () {
//                        var data = $('#f_jb').combobox('getData');
//                        if (data.length > 0) {
//                            $("#f_jb").combobox('select', data[0].f_jb);
//                        }
//                    }
//                });
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                $('#fm').form('load', row);
                url = 'MemberCardData.aspx?action=RoomDesignUpdate&id=' + row.id;
            }
        }

        function saveRoomDesign() {
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
        function destroyRoomDesign() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('MemberCardData.aspx?action=RoomDesignDelete', { id: row.id }, function (result) {
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
    </div>

        <div title="钟点房方案">
        <div id="ZD" title="钟点房方案" class="easyui-dialog" style="width: 580px; height: 350px;"
            closed="true" buttons="#btn"  >
            <form id="FRZD" method="post">
            <table id="ZDdg" class="easyui-datagrid" style="padding: 0px;" buttons="#btn" fitcolumns="true"
                singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'id',width:30">
                            序号
                        </th>
                        <th data-options="field:'f_jb',width:55">
                            房间级别
                        </th>
                        <th data-options="field:'FangAnName',width:75">
                            钟点房方案
                        </th>
                        <th data-options="field:'StartLen',width:55">
                            起步时长
                        </th>
                        <th data-options="field:'StartFee',width:55">
                            起步价格
                        </th>
                        <th data-options="field:'AddLen',width:55">
                            加钟时间
                        </th>
                        <th data-options="field:'AddFee',width:55">
                            加钟价格
                        </th>
                        <th data-options="field:'MinLen',width:55">
                            最小时长
                        </th>
                        <th data-options="field:'MinFee',width:55">
                            最小价格
                        </th>
                        <th data-options="field:'MaxLen',width:50">
                            最大时长
                        </th>
                    </tr>
                </thead>
            </table>
            </form>
        </div>
        <div id="ZDdlg" class="easyui-dialog" style="width: 300px; height: 350px;" closed="true"
            buttons="#ZDdlg-buttons">
            <div style="padding: 10px 0 10px 10px">
                <form id="ZDfm" method="post">
                <table>
                    <tr>
                        <td>
                            房间级别：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="f_jb" id="f_jb" readonly="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            方案名称：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="FangAnName" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            起步时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="StartLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            起步价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="StartFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加钟时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="AddLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加钟价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="AddFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最小时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MinLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最小价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MinFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最长时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MaxLen" />分钟
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                </script>
                </form>
            </div>
        </div>
        <div id="ZDdlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMember()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#ZDdlg').dialog('close')">取消</a>
        </div>
        <div id="btn">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" onclick="newMember()">
                增加</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                    onclick="editMember()">修改</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        iconcls="icon-edit" onclick="destroyMember()">删除</a>
        </div>
        <script type="text/javascript">
            var urll;
            function newMember() {
                $('#ZDdlg').dialog('open').dialog('setTitle', '添加钟点房房价方案');
                $('#ZDfm').form('clear');
                $('#ZDfm').form('reset');
                urll = '../Setting/ZD_selectData.aspx?action=create';
                var row = $('#dg').datagrid('getSelected');
                var t = $('#f_jb').datebox('getValue');
               $("input[id$='f_jb']").val(t);
//                $.get('../Setting/ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
//                    $("input[id$='f_jb']").val(t);
//                    $("#ZDdg").datagrid("loadData", result);
//                }, 'json');
            }

            function editMember() {
                var row = $('#ZDdg').datagrid('getSelected');
                if (row) {
                    $('#ZDdlg').dialog('open').dialog('setTitle', '修改钟点房房价方案');
                    $('#ZDfm').form('load', row);
                    urll = '../Setting/ZD_selectData.aspx?action=update&id=' + row.id;

                }
            }

            function saveMember() {
                $('#ZDfm').form('submit', {
                    url: urll,
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
                            $('#ZDdlg').dialog('close');
                            $('#ZDdg').datagrid('reload');
                            var row = $('#dg').datagrid('getSelected');
                            var t = $('#f_jb').datebox('getValue');
                            $.get('../Setting/ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                                $("#ZDdg").datagrid("loadData", result);
                                var d = $("#ZDdg").datagrid('getData');
                                var fh = d.rows[0].StartFee;
                                $("#ZdFj").val(fh != "" ? fh : 0);

                               // url = "";

                            }, 'json');
                        }
                    }
                });
            }

            function destroyMember() {
                var row = $('#ZDdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('../Setting/ZD_selectData.aspx?action=delete', { id: row.id }, function (result) {
                                if (!result.errorMsg) {
                                    $('#ZDdg').datagrid('reload');
                                    var row = $('#dg').datagrid('getSelected');
                                    var t = $('#f_jb').datebox('getValue');
                                    $.get('ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                                        $("#ZDdg").datagrid("loadData", result);
                                        var d = $("#ZDdg").datagrid('getData');
                                        var fh = d.rows[0].StartFee;
                                        $("#ZdFj").val(fh != "" ? fh : 0);
                                    }, 'json');
                                } else {
                                    $.messager.show({
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
</body>
</html>
