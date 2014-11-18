<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MergeRegisterOrder.aspx.cs"
    Inherits="Register_MergeRegisterOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合并账单</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //初始化datagrid
            $('#mergedata').datagrid({
                columns: [[
                        { field: 'AutoID', title: '主账号', width: 130 },
                        { field: 'FangHao', title: '主房间号', width: 130 },
                        { field: 'XingMing', title: '账单人', width: 130 },
                        { field: 'YaJin', title: '押金', width: 130 },
					    { field: 'DaodianTime', title: '入住时间', width: 130 },
					    { field: 'LeaveTime', title: '预离时间', width: 130 },
                        { field: 'OrderGuid', title: 'GUID', width: 130 }
				        ]]
            });
            var index = 0;
            var fh = '<%=fh %>';
            //每个房间1条待输记录
            if (fh != '0000') {
                $('#mergedata').datagrid('insertRow', {
                    index: index,
                    row: {
                        AutoID: '<%=AutoID %>',
                        FangHao: '<%=Fanghao %>',
                        XingMing: '<%=Xingming %>',
                        YaJin: '<%=Yajin %>',
                        DaodianTime: '<%=DaodianTime %>',
                        LeaveTime: '<%=LidianTime %>',
                        OrderGuid: '<%=OrderGuid %>'
                    }
                });
            }
        });

        function Append() {
            //得到所选的行
            var orders = $('#orderdata').datagrid('getSelections');
            //添加的willbemerge的datagrid
            for (var i = 0; i < orders.length; i++) {
                var index = 0;
                //每个房间1条待输记录
                $('#mergedata').datagrid('insertRow', {
                    index: index,
                    row: {
                        AutoID: orders[i].AutoID,
                        FangHao: orders[i].FangHao,
                        XingMing: orders[i].XingMing,
                        YaJin: orders[i].YaJin,
                        DaodianTime: orders[i].DaodianTime,
                        LeaveTime: orders[i].LeaveTime,
                        OrderGuid: orders[i].OrderGuid
                    }
                });
            }
            从数据源中删除
                        for (var i = 0; i < orders.length; i++) {
                            $('#mergedata').datagrid('deleteRow', {
                                index: index,
                                row: {
                                    FH: kfs[i].FH,
                                    Name: '',
                                    IDCard: '',
                                    StdPrice: kfs[i].DJ,
                                    ZKL: '10.0',
                                    Price: kfs[i].DJ
                                }
                            });
                        }
        }

        function ClearForm() {
            this.window.close();
        }

        function Merge() {
            //生成一个guid和一个待合并guid的数组
            var guidstr = '';
            var orders = $('#mergedata').datagrid('getRows');
            for (var i = 0; i < orders.length; i++) {
                guidstr += orders[i].OrderGuid + ';';
            }
            $.post('MergeOrder.aspx?guids=' + guidstr, function (result) {
                if (result.Success) {
                    alert('合并账单成功');
                    close();
                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
        }
    </script>
</head>
<body style="padding: 2px">
    <div class="easyui-layout" style="height: 500px; width: 800px">
        <div data-options="region:'north'" style="height: 50px">
            <p style="vertical-align: middle">
                &nbsp;&nbsp;选择右侧"可选账单"进入左侧"待合并帐单","待合并账单"保存后将被合并成一个账单</p>
        </div>
        <div data-options="region:'south'" style="height: 50px;">
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="Merge()">保存</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'east',split:true,title:'可选账单',collapsible:false" style="width: 400px;">
            <table class="easyui-datagrid" id="orderdata" toolbar="#tb" rownumbers="true" data-options="
                url:'../FrontDesk/OrderData.aspx?action=readcanselect&fh=<%=fh %>'      
                ">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true">
                        </th>
                        <th data-options="field:'AutoID',width:130">
                            主账号
                        </th>
                        <th data-options="field:'FangHao',width:130">
                            主房间号
                        </th>
                        <th data-options="field:'XingMing',width:130">
                            帐单人
                        </th>
                        <th data-options="field:'YaJin',width:130">
                            押金
                        </th>
                        <th data-options="field:'DaodianTime',width:130">
                            入住时间
                        </th>
                        <th data-options="field:'LeaveTime',width:130">
                            预离时间
                        </th>
                        <th data-options="field:'OrderGuid',width:130">
                            GUID
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="tb">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-tip" plain="true"
                    onclick="Append()">添加进待合并</a>
            </div>
        </div>
        <div data-options="region:'center',title:'待合并账单'">
            <table class="easyui-datagrid" id="mergedata">
            </table>
        </div>
    </div>
</body>
</html>
