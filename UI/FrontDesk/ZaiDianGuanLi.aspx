<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZaiDianGuanLi.aspx.cs" Inherits="FrontDesk_ZaiDianGuanLi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>在店客人管理</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function Edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#edit').dialog('open').dialog('setTitle', '编辑信息');

            }
            else {
                alert("请先选中行！");
            }
        }
        function XZList() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#xz').dialog('open').dialog('setTitle', '续住记录');
                $('#xzlist').datagrid({
                    url: 'get_xz.aspx?fh=' + row.FangJianHao
                });
            }
            else {
                alert("请先选中行！");
            }
        }
        function HFList() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#hf').dialog('open').dialog('setTitle', '换房记录');
                $('#hflist').datagrid({
                    url: 'get_hf.aspx?fh=' + row.FangJianHao
                });
            }
            else {
                alert("请先选中行！");
            }
        }
        function ZHList() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#zh').dialog('open').dialog('setTitle', '账户明细');
                $('#zhlist').datagrid({
                    url: 'get_zh.aspx?fh=' + row.FangJianHao
                });
            }
            else {
                alert("请先选中行！");
            }
        }
        function SKList() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#sk').dialog('open').dialog('setTitle', '随客信息');
                $('#sklist').datagrid({
                    url: 'get_sk.aspx?fh=' + row.FangJianHao
                });
            }
            else {
                alert("请先选中行！");
            }
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="当前在店客人列表">
    </div>
    <table class="easyui-datagrid" id="dg" style="height: 470px" pagesize="20" toolbar="#dgtbr"
        url="ZaiDianGuanLiData.aspx" pagination="true" rownumbers="true" singleselect="true">
        <thead>
            <tr>
                <th data-options="field:'FangJianHao',width:80,align:'center'">
                    房间号
                </th>
                <th data-options="field:'XingMing',width:80,align:'center'">
                    客户名称
                </th>
                <th data-options="field:'XingBie',width:80">
                    性别
                </th>
                <th data-options="field:'MainFH',width:80,align:'center'">
                    主帐房号
                </th>
                <th data-options="field:'MainOrderMan',width:80,align:'center'">
                    主帐名称
                </th>
                <th data-options="field:'ZhengjianLeibie',width:80,align:'center'">
                    证件类别
                </th>
                <th data-options="field:'ZhengjianHaoma',width:150,align:'center'">
                    证件号码
                </th>
                <th data-options="field:'ZhengjianDizhi',width:150,align:'center'">
                    证件地址
                </th>
                <th data-options="field:'YuanFangJia',width:80,align:'center'">
                    原房价
                </th>
                <th data-options="field:'ZheKouLv',width:80,align:'center'">
                    折扣率
                </th>
                <th data-options="field:'ShijiFangjia',width:80,align:'center'">
                    实际房价
                </th>
                <th data-options="field:'OnBoardTime',width:150,align:'center'">
                    到店日期
                </th>
                <th data-options="field:'LeaveTime',width:150,align:'center'">
                    离店日期
                </th>
            </tr>
        </thead>
    </table>
    <div id="dgtbr">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
            onclick="Edit()">编辑</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add"
                plain="true" onclick="XZList()">续住记录</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon_warehouse" plain="true" onclick="HFList()">换房记录</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-money" plain="true" onclick="ZHList()">帐户明细</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon_member" plain="true"
            onclick="SKList()">随客查询</a>
    </div>
    <div id="edit" buttons="#editbtns" class="easyui-dialog" closed="true" style="width: 600px;
        height: 400px">
        <table id="Table1">
        </table>
    </div>
    <div id="editbtns">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#edit').dialog('close')">取消</a>
    </div>
    <div id="xz" buttons="#xzbtns" class="easyui-dialog" closed="true" style="width: 600px;
        height: 400px">
        <table id="xzlist">
        </table>
    </div>
    <div id="xzbtns">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#xz').dialog('close')">取消</a>
    </div>
    <div id="hf" buttons="#hfbtns" class="easyui-dialog" closed="true" style="width: 600px;
        height: 400px">
        <table id="hflist">
        </table>
    </div>
    <div id="hfbtns">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#hf').dialog('close')">取消</a>
    </div>
    <div id="zh" buttons="#zhbtns" class="easyui-dialog" closed="true" style="width: 600px;
        height: 400px">
        <table id="zhlist">
        </table>
    </div>
    <div id="zhbtns">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#zh').dialog('close')">取消</a>
    </div>
    <div id="sk" buttons="#skbtns" class="easyui-dialog" closed="true" style="width: 600px;
        height: 400px">
        <table id="sklist">
        </table>
    </div>
    <div id="skbtns">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#sk').dialog('close')">取消</a>
    </div>
</body>
</html>
