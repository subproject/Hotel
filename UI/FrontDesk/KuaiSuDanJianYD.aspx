<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiSuDanJianYD.aspx.cs"
    Inherits="FrontDesk_KuaiSuDanJianYD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>快速预订单间</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
</head>
<body style="padding: 1px">
    <div id="container" data-options="handle:'#title'" style="height: 430px; width: 500px;
        margin: 0px; background: #fafafa; border: 1px solid #ccc">
        <div id="title" style="padding: 10px; background: #ccc; color: #fff; font-size: 14px;">
            预定房间信息</div>
        <div style="padding: 10px; font-size: 15px; color: Red">
            房号:<%=fh %>
        </div>
        <div style="padding: 30px">
            <form id="ff" method="post">
            <table>
                <tr>
                    <td style="width: 90px; padding: 10px;" align="right">
                        房号
                    </td>
                    <td>
                        <input class="easyui-validatebox" value="<%=fh %>" type="text" name="FH" readonly></input>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; padding: 10px;" align="right">
                        预到日期
                    </td>
                    <td>
                        <input name="OnBoardTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=YDTime%>">
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; padding: 10px;" align="right">
                        预离日期
                    </td>
                    <td>
                        <input name="LeaveTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=YLTime%>">
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; padding: 10px;" align="right">
                        客人姓名
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="Yder" data-options="required:true"></input>
                    </td>
                </tr>
                <tr>
                    <td style="width: 90px; padding: 10px;" align="right">
                        电话
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="YdTel" data-options="required:true"></input>
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div style="text-align: center; padding: 5px">
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="submitForm()">保存</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
        </div>
        <script type="text/javascript">
            $.fn.datebox.defaults.formatter = function (date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return y + '-' + m + '-' + d;
            }
            function submitForm() {
                $('#ff').form('submit', {
                    url: 'KuaiSuDanJianYDData.aspx',
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
                            window.opener.location.reload();
                            window.close();
                        }
                    }
                });
            }
            function clearForm() {
                $('#ff').form('clear');
                this.close();
            }
        </script>
    </div>
</body>
</html>
