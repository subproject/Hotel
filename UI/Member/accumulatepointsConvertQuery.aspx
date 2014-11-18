<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accumulatepointsConvertQuery.aspx.cs" Inherits="Member_accumulatepointsConvertQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>积分兑换查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
	<link rel="stylesheet" type="text/css" href="../themes/icon.css">
	<link rel="stylesheet" type="text/css" href="../themes/demo.css">
	<script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
	<script type="text/javascript" src="../jquery.easyui.min.js"></script>
        <script type="text/javascript">
            function myformatterDa(date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
            }
            function myformatter(date) {
                var y = date.getFullYear();
                var m = date.getMonth() + 1;
                var d = date.getDate();
                return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
            }
            function myparser(s) {
                if (!s) return new Date();
                var ss = (s.split('-'));
                var y = parseInt(ss[0], 10);
                var m = parseInt(ss[1], 10);
                var d = parseInt(ss[2], 10);
                if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                    return new Date(y, m - 1, d);
                } else {
                    return new Date();
                }
            }
    </script>
    <style type="text/css">
        .style1
        {
            width: 168px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
        <tr> 
        <td>起始日期：<input class="easyui-datebox" data-options="formatter:myformatter,parser:myparser"  />&nbsp;&nbsp;&nbsp; 
        终止日期：<input class="easyui-datebox" data-options="formatter:myformatter,parser:myparser" /></td>
        <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" Height="21px" 
            RepeatDirection="Horizontal">
            <asp:ListItem Value="details">明细</asp:ListItem>
            <asp:ListItem Value="Sum">汇总</asp:ListItem>
        </asp:RadioButtonList></td>
        <td class="style1"><button style="margin:2px;width:50px; height:30px">查询</button>
        <button style="margin:2px;width:50px; height:30px">导出</button></td>
        <td><button style="margin:2px;width:50px; height:30px">退出</button></td>
        </tr>
        </table>
        &nbsp;  
        
        <%--积分兑换汇总--%>
        <div class="easyui-panel" title="积分兑换汇总" style="width: 790px">
            <table class="easyui-datagrid" style="width: 1520px; height: 250px">
                <thead>
                    <tr>
                        <th data-options="field:'itemid',width:80">
                            兑换项目
                        </th>
                        <th data-options="field:'productid',width:80">
                            数量
                        </th>
                        <th data-options="field:'listprice',width:80,align:'center'">
                            每项目积分
                        </th>
                        <th data-options="field:'unitcost',width:80,align:'center'">
                            已用积分
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
           
    </div>
    </form>
</body>
</html>
