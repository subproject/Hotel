<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XuZhu.aspx.cs" Inherits="FrontDesk_XuZhu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>续住</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //付款方式
            $("#PaywayCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=fkfs&action=read',
                valueField: 'ID',
                textField: 'fkfs',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#PaywayCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#PaywayCombo").combobox('select', data[0].ID);
                    }
                }
            });
        });

        function submitForm() {
            alert("test");
            $('#frm').form('submit', {
                url: 'XuZhuData.aspx',
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
    </script>
</head>
<body class="easyui-layout">
    <div style="padding: 2px">
        <form id="frm" method="post">
        <div class="easyui-panel" title="房间信息">
        <table style="margin: 15px">
            <tr>
                <td style="width: 80px; margin-right: 10px">
                    房间级别
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="RoomLevel" value="<%=data.RoomLevel %>"
                        readonly />
                </td>
                <td style="width: 80px;">
                    房号
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="RoomID" value="<%=data.RoomID %>"
                        readonly />
                </td>
            </tr>
            <tr>
                <td style="width: 80px;">
                    姓名
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="Name" value="<%=data.Name %>"
                        readonly />
                </td>
                <td style="width: 80px;">
                    房价
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="Price" value="<%=data.Price %>"
                        readonly />
                </td>
            </tr>
            <tr>
                <td style="width: 80px;">
                    原离店日
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" name="LeaveTime" value="<%=data.LeaveTime %>" readonly />
                </td>
                <td style="width: 80px;">
                    已交押金
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="Deposit" value="<%=data.Deposit %>" readonly />
                </td>
            </tr>
        </table>
        </div>
        <div class="easyui-panel" title="续住信息">
        <table style="margin: 15px">
            <tr>
                <td style="width: 80px;">
                    续住日期
                </td>
                <td style="width: 220px;">
                    <input class="easyui-datebox" style="width: 140px" name="NewLeaveTime">
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 80px; margin-right: 10px">
                    付款方式
                </td>
                <td style="width: 220px;">
                    <input class="easyui-combobox" name="PayWay" id="PaywayCombo" style="width: 140px" />
                </td>
                <td style="width: 80px;">
                    单据号码
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="AutoID" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px;">
                    天数
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="DayCount"></input>
                </td>
                <td style="width: 80px;">
                    续交押金
                </td>
                <td style="width: 220px;">
                    <input class="easyui-validatebox" type="text" name="AppendDeposit"></input>
                </td>
            </tr>
        </table>
        </div>
        <table style="margin: 15px">
            <tr>
                <td style="color: Red">
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="submitForm()">保存</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
