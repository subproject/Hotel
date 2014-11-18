<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YuDingDan.aspx.cs" Inherits="FrontDesk_YuDingDan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>预定单</title>
</head>
<body>
    <table style="width: 100%; height: 50px; font-size: 18px; font-weight: bold">
        <tr>
            <td align="center">
                客房预定单
            </td>
        </tr>
    </table>
    <table style="width: 100%;">
        <tr>
            <td align="center">
                <table border="0" cellpadding="1" cellspacing="1" style="background: #000">
                    <tr>
                        <td style="background: #fff; padding: 5px;">
                            预定人
                        </td>
                        <td style="background: #fff; padding: 5px; width:120px;">
                        <%=Yder %>
                        </td>
                        <td style="background: #fff; padding: 5px;">
                            预定电话
                        </td>
                        <td style="background: #fff; padding: 5px; width:120px;">
                        <%=YdTel %>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; padding: 5px;">
                            客户
                        </td>
                        <td style="background: #fff; padding: 5px;">
                        <%=Customer %>
                        </td>
                        <td style="background: #fff; padding: 5px;">
                            客户电话
                        </td>
                        <td style="background: #fff; padding: 5px;">
                        <%=CustomerTel %>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; padding: 5px;">
                            到店日期
                        </td>
                        <td style="background: #fff; padding: 5px;">
                        <%=Dr %>
                        </td>
                        <td style="background: #fff; padding: 5px;">
                            离店日期
                        </td>
                        <td style="background: #fff; padding: 5px;">
                        <%=Lr %>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #fff; padding: 5px;">
                            预定房间信息
                        </td>
                        <td colspan="3" style="background: #fff; padding: 5px;">
                        <%=FJs %>
                        </td>

                    </tr>
                </table>
            </td>
        </tr>
    </table>


</body>
</html>
