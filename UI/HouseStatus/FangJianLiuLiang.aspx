<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FangJianLiuLiang.aspx.cs"
    Inherits="HouseStatus_FangJianLiuLiang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>房间即时流量查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function Refresh() {
           window.location.href='FangjianLiuliang.aspx?begin='+$('#begin').datebox('getValue')+'&end='+$('#end').datebox('getValue')+'';
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div style="padding: 5px">
        <table>
            <tr>
                <td>
                    开始时间
                </td>
                <td>
                    <input id="begin" runat="server" class="easyui-datebox"  />
                </td>
                <td>
                    结束时间
                </td>
                <td>
                    <input id="end" runat="server" class="easyui-datebox" />
                </td>
                <td><a href="javascript:void(0)" class="easyui-linkbutton" onclick="Refresh()">确定</a></td>
            </tr>
        </table>
    </div>
    <%=tablestr %>
</body>
</html>
