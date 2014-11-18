<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterView.aspx.cs" Inherits="Register_RegisterView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <title>客人信息查看</title>
</head>
<body style="padding: 5px">
    <table border="1" cellpadding="1" cellspacing="0" class="T_print" style="width: 100%;">
        <tr>
            <td style="width: 100px" align="right">
                房间号：
            </td>
            <td>
                <%=data.FangJianHao%>
            </td>
            <td style="width: 100px" align="right">
                客人姓名：
            </td>
            <td>
                <%=data.XingMing%>
            </td>
            <td style="width: 100px" align="right">
                电话号码：
            </td>
            <td>
                <%=data.XingMing%>
            </td>
        </tr>
        <tr>
            <td style="width: 100px;" align="right">
                证件类型：
            </td>
            <td>
                <%=data.ZhengjianLeixing%>
            </td>
            <td style="width: 100px" align="right">
                证件号码：
            </td>
            <td>
                <%=data.ZhengjianHaoma%>
            </td>
            <td style="width: 100px" align="right">
                证件地址：
            </td>
            <td>
                <%=data.ZhengjianDizhi%>
            </td>
        </tr>
        <tr>
            <td style="width: 100px" align="right">
                到店日期：
            </td>
            <td>
                <%=data.ArriveTime%>
            </td>
            <td style="width: 100px" align="right">
                离店日期：
            </td>
            <td>
                <%=data.LeaveTime%>
            </td>
            <td style="width: 100px" align="right">
                入住天数：
            </td>
            <td>
                <%=data.FangJianHao%>
            </td>
        </tr>
        <tr>
            <td style="width: 100px" align="right">
                原房价：
            </td>
            <td>
                <%=data.YuanFangJia%>
            </td>
            <td style="width: 100px" align="right">
                折扣率：
            </td>
            <td>
                <%=data.ShijiFangjia%>
            </td>
            <td style="width: 100px" align="right">
                实际房价：
            </td>
            <td>
                <%=data.ShijiFangjia%>
            </td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 100%" align="center">
                <input type="button" value="打印" onclick="javascript:window.print();" />
                <input type="button" value="关闭" onclick="javascript:window.close();" />
            </td>
        </tr>
    </table>
</body>
</html>
