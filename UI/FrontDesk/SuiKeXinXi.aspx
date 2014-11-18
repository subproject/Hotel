<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SuiKeXinXi.aspx.cs" Inherits="FrontDesk_SuiKeXinXi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>随客信息管理</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="随客信息管理">
    </div>
    <table class="easyui-datagrid" style="height: 470px" pagesize="20" toolbar="#dgtbr"
        url="SuiKeXinXiData.aspx" pagination="true" rownumbers="true" fitcolumns="true"
        singleselect="true">
        <thead>
            <tr>
                <th data-options="field:'FangHao',width:80,align:'center'">
                    房间号
                </th>
                <th data-options="field:'XingMing',width:80,align:'center'">
                    姓名
                </th>
                <th data-options="field:'XingBie',width:80">
                    性别
                </th>
                <th data-options="field:'ShenfenZheng',width:80,align:'center'">
                    身份证
                </th>
                <th data-options="field:'DiZhi',width:80,align:'center'">
                    地址
                </th>
                <th data-options="field:'CarNum',width:80,align:'center'">
                    车牌号
                </th>
                <th data-options="field:'Remark',width:80,align:'center'">
                    备注
                </th>
            </tr>
        </thead>
    </table>
    <div id="dgtbr">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
            onclick="">编辑</a>
    </div>
</body>
</html>