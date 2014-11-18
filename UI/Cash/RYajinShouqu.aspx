<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RYajinShouqu.aspx.cs" Inherits="Cash_RYajinShouqu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>押金收取统计表</title>
    <link href="../demo/print.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: 12px">
    <form id="form1" runat="server">
    <p style="text-align: center; width: 100%">
        <strong style="font-size: 16px">收银员押金报表</strong></p>
    <p style="width: 100%">
        <span style="position: relative; left: 10px;">收银员：<%=Session["user"].ToString()%></span>
        <span style="position: relative; left: 360px;">收银时段：<%=starttime %>&nbsp;--&nbsp;<%=endtime %></span></p>
    <table style="width: 100%">
        <tr>
            <td>
                单据号
            </td>
            <td>
                房号
            </td>
            <td>
                客人名称
            </td>
            <td>
                付款方式
            </td>
            <td>
                押金
            </td>
            <td>
                时间
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <p style="width: 100%">
        <span style="position: relative; left: 500px;">制表时间：<%=starttime %>&nbsp;&nbsp;制表人&nbsp;&nbsp;<%=Session["user"].ToString() %></span></p>
    </form>
</body>
</html>
