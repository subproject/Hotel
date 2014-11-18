<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppendRegister.aspx.cs" Inherits="Register_AppendRegister" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>加开房间</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        #xyfm
        {
            margin: 0;
            padding: 10px 30px;
        }
        .ftitle
        {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }
        .fitem
        {
            margin-bottom: 5px;
        }
        .fitem label
        {
            display: inline-block;
            width: 80px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            //初始化入住房间信息表格
            $('#lfdata').datagrid({
                iconCls: 'icon-edit',
                singleSelect: true,
                columns: [[
                        { field: 'action', title: '操作', width: 100, align: 'center',
                            formatter: function (value, row, index) {
                                if (row.editing) {
                                    var s = '<a href="javascript:void(0)" onclick="saverow(this)">保存</a> ';
                                    var c = '<a href="javascript:void(0)" onclick="cancelrow(this)">取消</a>';
                                    return s + c;
                                } else {
                                    var e = '<a href="javascript:void(0)" onclick="editrow(this)">编辑</a> ';
                                    var d = '<a href="javascript:void(0)" onclick="deleterow(this)">删除</a>';
                                    return e + d;
                                }
                            }
                        },
                        { field: 'FH', title: '房号', width: 80 },
                        { field: 'FJLX', title: '房间类型', width: 110 },
                        { field: 'Name', title: '姓名', width: 80, editor: 'text' },
                        { field: 'IDCard', title: '证件号码', width: 130, editor: 'text' },
					    { field: 'StdPrice', title: '标准房价', width: 80, align: 'right'},
					    { field: 'ZKL', title: '折扣率', width: 80, align: 'right', editor: { type: 'numberbox', options: { precision: 1}} },
                        { field: 'Price', title: '实际房价', width: 80, align: 'right', editor: { type: 'numberbox', options: { precision: 2}} }
				        ]],
                onBeforeEdit: function (index, row) {
                    row.editing = true;
                    updateActions(index);
                },
                onAfterEdit: function (index, row) {
                    row.editing = false;
                    updateActions(index);
                },
                onCancelEdit: function (index, row) {
                    row.editing = false;
                    updateActions(index);
                }
            });
        });

        //可编辑表格处理函数
        function updateActions(index) {
            $('#lfdata').datagrid('updateRow', {
                index: index,
                row: {}
            });
        }
        function getRowIndex(target) {
            var tr = $(target).closest('tr.datagrid-row');
            return parseInt(tr.attr('datagrid-row-index'));
        }
        function editrow(target) {
            $('#lfdata').datagrid('beginEdit', getRowIndex(target));
        }
        function deleterow(target) {
            $.messager.confirm('Confirm', '确认删除改条记录?', function (r) {
                if (r) {
                    $('#lfdata').datagrid('deleteRow', getRowIndex(target));
                }
            });
        }
        function saverow(target) {
            $('#lfdata').datagrid('endEdit', getRowIndex(target));
        }
        function cancelrow(target) {
            $('#lfdata').datagrid('cancelEdit', getRowIndex(target));
        }
        //显示选择房间对话框
        function ShowDlg() {
            $('#fjdlg').dialog('open').dialog('setTitle', '选择房间');
        }

        function SelectFJ() {
            //get kfdata selected info
            var kfs = $('#kfdata').datagrid('getSelections');
            for (var i = 0; i < kfs.length; i++) {
                var index = 0;
                //每个房间1条待输记录
                $('#lfdata').datagrid('insertRow', {
                    index: index,
                    row: {
                        FH: kfs[i].FH,
                        FJLX: kfs[i].JBName,
                        Name: '',
                        IDCard: '',
                        StdPrice: kfs[i].DJ,
                        ZKL: '10.0',
                        Price: kfs[i].DJ
                    }
                });
            }
            $('#fjdlg').dialog('close');
        }
        function ClearForm() {
            this.window.close();
        }

        //保存登记信息到数据库,该页唯一提交信息逻辑
        function AppendRegister() {
            //入住信息
            var rzstr = '';
            var data = $('#lfdata').datagrid('getRows');
            for (var i = 0; i < data.length - 1; i++) {
                rzstr = rzstr + '{"FangJianHao":"' + data[i].FH + '","XingMing":"' + data[i].Name + '","ZhengjianHaoma":"' + data[i].IDCard + '"};';
            }
            rzstr = rzstr + '{"FangJianHao":"' + data[data.length - 1].FH + '","XingMing":"' + data[data.length - 1].Name + '","ZhengjianHaoma":"' + data[data.length - 1].IDCard + '"}';
            $.ajax({
                url: 'SaveRegisterData.aspx?action=append&list='+rzstr+'&guid=<%=OrderGuid %>',
                type: 'POST',
                success: function (result) {
                    // close the dialog
                    window.opener.location.reload();
                    window.close();
                 }
            });
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div title="账单信息" class="easyui-panel">
        <form id="fm" method="post">
        <table style="padding: 10px">
            <tr style="height: 30px">
                <td style="width: 80px; padding: 5px; font-weight: bold">
                    账单编号：
                </td>
                <td style="width: 160px; padding: 5px">
                    <%=AutoID%>
                </td>
                <td style="width: 80px; padding: 5px; font-weight: bold">
                    主账房间：
                </td>
                <td style="width: 160px; padding: 5px">
                    <%=Fanghao%>
                </td>
            </tr>
            <tr style="height: 30px">
                <td style="width: 80px; padding: 5px; font-weight: bold">
                    主账名称：
                </td>
                <td style="width: 160px; padding: 5px">
                    <%=Xingming%>
                </td>
                <td style="width: 80px; padding: 5px; font-weight: bold">
                    电话：
                </td>
                <td style="width: 160px; padding: 5px">
                    <%=Dianhua%>
                </td>
            </tr>

        </table>
        </form>
    </div>
    <table id="lfdata" toolbar="#tbldiv" title="加开房间" style="height: 280px">
    </table>
    <div id="tbldiv">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-tip" plain="true"
            onclick="ShowDlg()">添加房间</a>
    </div>
    <div id="fjdlg" class="easyui-dialog" closed="true" style="width: 600px; height: 400px;"
        buttons="#dlg-buttons">
        <div style="padding: 10px 10px 10px 10px">
            <table>
                <tr>
                    <td style="width: 80px; padding: 5px;" align="right">
                        客房级别
                    </td>
                    <td>
                        <input class="easyui-combobox" data-options="valueField:'ID',textField:'KFJB',url:'../Common/getkfcgy.aspx',
                             onSelect: function(rec){$('#kfdata').datagrid({url:'../Setting/get_kf.aspx?cgyid='+rec.ID});}">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <table class="easyui-datagrid" id="kfdata" url="../Setting/get_kf.aspx?readE=true"
            style="padding: 0px;">
            <thead>
                <tr>
                    <th data-options="field:'ck',checkbox:true">
                    </th>
                    <th data-options="field:'FH',width:130">
                        房号
                    </th>
                    <th data-options="field:'JBName',width:130">
                        房间类型
                    </th>
                    <th data-options="field:'DJ',width:130">
                        标准房价
                    </th>
                    <th data-options="field:'StatusName',width:130">
                        状态
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="SelectFJ()">
            确定</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#fjdlg').dialog('close')">取消</a>
    </div>
    <table style="padding: 15px 15px 15px 15px">
        <tr>
            <td style="width: 580px;">
                &nbsp;
            </td>
            <td style="width: 80px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="AppendRegister()">保存</a>
            </td>
            <td style="width: 80px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
            </td>
            <td style="width: 60px;">
            </td>
        </tr>
    </table>
</body>
</html>
