<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RShouzhiHuizong.aspx.cs"
    Inherits="Cash_RShouzhiHuizong" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>收银员收支汇总报表</title>
    <link href="../demo/print.css" rel="stylesheet" type="text/css" />
</head>
<body style="font-size: 12px">
    <form id="form1" runat="server">
    <p style="text-align: center; width: 100%">
        <strong style="font-size: 16px">收银员收支汇总报表</strong></p>
    <p style="width: 100%">
        <span ">时间段：<%=starttime %>&nbsp;--&nbsp;<%=endtime %></span></p>
    <table style="width: 100%">
        <tr>
            <td>
                房号
            </td>
            <td>
                现金收入
            </td>
            <td>
                现金支出
            </td>
            <td>
                现付佣金
            </td>
            <td>
                信用卡
            </td>
            <td>
                借记卡
            </td>
            <td>
                代金券
            </td>
            <td>
                支票
            </td>
            <td>
                单位记账
            </td>
            <td>
                合计
            </td>
        </tr>
        <tr colspan="10"><%Session["user"].ToString();%></tr>
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
    <p style="width:100%; text-align:center; font-size:14px; color:Red">负数表示收入，正数表示支出。信用卡的金额为确认授权的金额。</p>
    </form>
</body>
</html>
