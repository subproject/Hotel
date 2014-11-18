<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="themes/demo.css"/>
    <script type="text/javascript" src="jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="jquery.easyui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width:100%">
        <tr>
            <td align="center" style="padding-top:100px">
                <div style="background-image: url('login.png'); width: 600px; height: 404px;">
                    <table style="top:125px;width:460px; position:relative; ">
                        <tr>
                            <td align="left" style="width:50px;height:35px;">
                                用户名
                            </td>
                            <td align="left">
                                <input class="easyui-validatebox" runat="server" id="UserName" value=""  />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="width:50px;height:35px;">
                                密码
                            </td>
                            <td align="left">
                                <input class="easyui-validatebox" type="password" id="Password" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" style="width:50px;height:35px;">
                                <asp:Button runat="server" ID="LoginBtn" Text="登录" OnClick="LoginBtn_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
