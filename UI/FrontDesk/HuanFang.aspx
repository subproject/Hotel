<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HuanFang.aspx.cs" Inherits="FrontDesk_HuanFang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>换房</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function submitForm() {
            $('#frm').form('submit', {
                url: 'HuanFangData.aspx?action=create',
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
                        // close the dialog
                        window.opener.location.reload();
                        window.close();
                    }
                }
            });
        }
        function clearForm() {
            this.window.close();
        }

        $(document).ready(function () {
            $('#NewRoomID').combobox({
                onSelect: function () {
                    var ldname = $('#NewRoomID').datebox('getValue');
                    if (ldname != 0) {
                        $.post('../Setting/FHChargeData.aspx?action=getRoomPrice', { fangjianhao: ldname }, function (result) {
                            if (result != "") {
                                $("#NewPrice").val(result);
                            } else {
                                $.messager.show({
                                    title: 'Error',
                                    msg: '没有该房间价格'
                                });
                            }
                        });
                    }
                }
            });


            //            $("#NewRoomID").change(function () {
            //                alert("进入换房");
            //                if ($("#NewRoomID").val() != "") {
            //                    $.post('../Setting/FHChargeData.aspx?action=getRoomPrice', { fangjianhao: $("#NewRoomID").val() }, function (result) {
            //                        if (!result != "") {
            //                            $("#NewPrice").val(result);
            //                        } else {
            //                            $.messager.show({
            //                                title: 'Error',
            //                                msg: '没有该房间价格'
            //                            });
            //                        }
            //                    }, 'json');
            //                }
            //            });
            //        });
        });
    </script>
</head>
<body class="easyui-layout">
    <div style="padding: 2px">
        <form id="frm" method="post">
        <div class="easyui-panel" title="客人信息">
            <table style="margin: 15px">
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        序号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="OrderGuid" value="<%=data.OrderGuid %>" hidden/>
                        <input class="easyui-validatebox" type="text" name="Number" value="<%=HFNum %>" />
                    </td>
                    <td style="width: 80px;">
                        客人姓名
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Name" value="<%=data.Name %>" />
                    </td>
                    <td style="width: 80px;">证件号码
                    </td>
                    <td style="width: 160px;">
                    <input class="easyui-validatebox" type="text" name="ZhengjianHaoma" value="<%=data.ZhengjianHaoma %>" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        已交押金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Deposit" value="<%=data.Deposit %>"></input>
                    </td>
                    <td style="width: 80px;">
                        帐户余额
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Money" value="<%=data.Money %>"></input>
                    </td>
                    <td style="width: 80px;">
                        离店日期
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-datetimebox" style="width: 140px" name="LeaveDate" value="<%=data.LeaveDate %>" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        原房号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="RoomID" value="<%=data.RoomID %>"></input>
                    </td>
                    <td style="width: 80px;">
                        原房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Price" value="<%=data.Price %>"></input>元
                    </td>
                    <td style="width: 80px;">
                        折扣率
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Rebate" value="<%=data.Rebate %>"></input>%
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        到店时间
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ArriveTime" value="<%=data.ArriveTime %>" />
                    </td>
                    <td style="width: 80px;">
                    </td>
                    <td style="width: 160px;">
                    </td>
                    <td style="width: 80px;">
                    </td>
                    <td style="width: 160px;">
                    </td>
                </tr>
            </table>
        </div>
        <div class="easyui-panel" title="换房信息">
            <table style="margin: 15px">
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        客房级别
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" name="RoomLevel" data-options="valueField:'ID',textField:'KFJB',url:'../Common/getkfcgy.aspx'">
                    </td>
                    <td style="width: 80px;">
                        新房号
                    </td>
                    <td style="width: 160px;">
                       <%-- <input class="easyui-validatebox" type="text"  id="NewRoomID" name="NewRoomID">--%>
                        <input class="easyui-combobox"  id="NewRoomID"   name="NewRoomID" data-options="valueField:'FH',textField:'FH',url:'../Setting/get_kf.aspx?type=queryfh'" >
                    </td>
                    <td style="width: 80px;">
                        新房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="NewPrice"  id="NewPrice" />元
                    </td>
                </tr>
                <tr>
                    <td>
                        换房原因
                    </td>
                    <td  style="width: 160px;">
                         <input class="easyui-combobox" name="Remark" id="Remark" data-options="valueField:'AutoID',textField:'Content',url:'../Common/getHFYY.aspx'">
             

                    </td>
                       <td style="width: 80px;">
               <a href="javascript:void(0)" class="easyui-linkbutton"  onclick="MoreLY()" >更多</a>
                    </td>
                    <td style="width: 160px;">
                    </td>
                    <td style="width: 80px;">
                    </td>
                    <td style="width: 160px;">
                    </td>
                </tr>
            </table>
        </div>
        <table style="padding: 5px 5px 5px 5px">
            <tr>
                <td style="width: 580px;">
                    &nbsp;
                </td>
                <td style="width: 80px;" align="right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="submitForm()">保存</a>
                </td>
                <td style="width: 80px;" align="right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
                </td>
                <td style="width: 60px;">
                </td>
            </tr>
        </table>

 
        </form>
    </div>

    
    <div id="dgfh" class="easyui-dialog" style="width:350px; height: 400px;" closed="true" buttons="#toolbarfh">
        <table id="dgfhrow" class="easyui-datagrid" 
            url="HuanFangLYData.aspx?action=read" 
            fitcolumns="true" singleselect="true" >
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
    <div id="toolbarfh">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
            onclick="newMember()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                iconcls="icon-edit" plain="true" onclick="editMember()">修改</a> <a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyMember()">
                    删除</a>
    </div>
    <div id="dlgfh" class="easyui-dialog" style="width: 250px; height: 120px;" closed="true"
        buttons="#dlg-buttonsfh">
        <div style="padding: 10px 0 10px 10px">
            <form id="fmfh" method="post">
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
    <div id="dlg-buttonsfh">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMember()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>


    <script type="text/javascript">
      
        function MoreLY() {

                 $('#dgfh').dialog('open').dialog('setTitle', '换房理由');
                 // location.href = "HuanFangLY.aspx";

        }
        function newMember() {
            $('#dlgfh').dialog('open').dialog('setTitle', '新增换房理由');
            $('#fmfh').form('clear');
            $('#fmfh').form('reset');
            url = 'HuanFangLYData.aspx?action=create';
        }

        function editMember() {
            var row = $('#dgfhrow').datagrid('getSelected');
            if (row) {
                $('#dlgfh').dialog('open').dialog('setTitle', '编辑换房理由');
                $('#fmfh').form('load', row);
                url = 'HuanFangLYData.aspx?action=update&AutoID=' + row.AutoID;

            }
        }
        function saveMember() {
            $('#fmfh').form('submit', {
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
                        $('#dlgfh').dialog('close');
                        $('#dgfhrow').datagrid('reload');
                        $("#Remark").combobox('reload');
                    }
                }
            });
        }


        function destroyMember() {
            var row = $('#dgfhrow').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('HuanFangLYData.aspx?action=delete', { AutoID: row.AutoID }, function (result) {

                            if (!result.errorMsg) {
                                $('#dgfhrow').datagrid('reload');
                                $("#Remark").combobox('reload');
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
