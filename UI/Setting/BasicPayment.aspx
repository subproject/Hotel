<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasicPayment.aspx.cs" Inherits="Setting_BasicPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>付款方式</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link href="../css/cashaction/pageformal.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script src="../src/jquery.json-2.2.min.js" type="text/javascript"></script>
    <script src="../locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../Hotelmgr.js" type="text/javascript"></script>
    <script src="../js/basic/BasicPaymentaction.js" type="text/javascript"></script>
</head>
<body class="easyui-layout">
    <div region="east" split="true" title="East" class="leftpage">
        <table>
            <tr>
                <td>
                    <input type="button" class="inputbutton"   value="增 加" />
                </td>
            </tr>
           
        </table>
    </div>
    <div region="center" border="true" title="付款方式" style="border-left: 0px; border-right: 0px;
        overflow: hidden;">
        <div class="easyui-tabs">
            <div title="明细">
                <div id="t1" class="datagridgroupby">
                </div>
                <table class="datagridformat" id="tab_paymenttypes">
                </table>
            </div>
        </div>
    </div>
</body>
</html>
