<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SplitRegisterOrder.aspx.cs"
    Inherits="Register_SplitRegisterOrder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>拆分账单</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function Transfer() {
            //得到所选的行
            var orders = $('#olddata').datagrid('getSelections');
            //添加的willbemerge的datagrid
            for (var i = 0; i < orders.length; i++) {
                var index = 0;
                //每个房间1条待输记录
                $('#newdata').datagrid('insertRow', {
                    index: index,
                    row: {
                        AutoID: orders[i].AutoID,
                        FangJianHao: orders[i].FangJianHao,
                        XingMing: orders[i].XingMing,
                        ShijiFangjia: orders[i].ShijiFangjia,
                        ArriveTime: orders[i].ArriveTime,
                        LeaveTime: orders[i].LeaveTime,
                        OrderGuid: orders[i].OrderGuid
                    }
                });
            }
            //从数据源中删除
            //            for (var i = 0; i < orders.length; i++) {
            //                $('#mergedata').datagrid('deleteRow', {
            //                    index: index,
            //                    row: {
            //                        FH: kfs[i].FH,
            //                        Name: '',
            //                        IDCard: '',
            //                        StdPrice: kfs[i].DJ,
            //                        ZKL: '10.0',
            //                        Price: kfs[i].DJ
            //                    }
            //                });
            //            }
        }
    </script>
</head>
<body style="padding: 2px">
    <div class="easyui-layout" style="height: 500px; width: 800px">
        <div data-options="region:'north'" style="height: 50px">
            <p style="vertical-align: middle">
                &nbsp;&nbsp;选择右侧"账单所含房间"进入左侧"待拆分房间","待拆分房间"保存后将被生成成一个新账单</p>
        </div>
        <div data-options="region:'south'" style="height: 50px;">
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">保存</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'east',split:true,title:'待拆分房间',collapsible:false" style="width: 400px;">
            <table class="easyui-datagrid" id="newdata" style="padding: 0px;">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true">
                        </th>
                        <th data-options="field:'XingMing',width:130">
                            客人姓名
                        </th>
                        <th data-options="field:'FangJianHao',width:130">
                            房间号
                        </th>
                        <th data-options="field:'ShijiFangjia',width:130">
                            实际房价
                        </th>
                        <th data-options="field:'ArriveTime',width:130">
                            入住时间
                        </th>
                        <th data-options="field:'LeaveTime',width:130">
                            预离时间
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div data-options="region:'center',title:'账单所含房间'">
            <table class="easyui-datagrid" id="olddata" toolbar="#tb" url="SplitOrderData.aspx?fh=<%=fh %>"
                style="padding: 0px;">
                <thead>
                    <tr>
                        <th data-options="field:'ck',checkbox:true">
                        </th>
                        <th data-options="field:'XingMing',width:130">
                            客人姓名
                        </th>
                        <th data-options="field:'FangJianHao',width:130">
                            房间号
                        </th>
                        <th data-options="field:'ShijiFangjia',width:130">
                            实际房价
                        </th>
                        <th data-options="field:'ArriveTime',width:130">
                            入住时间
                        </th>
                        <th data-options="field:'LeaveTime',width:130">
                            预离时间
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="tb">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-tip" plain="true"
                    onclick="Transfer()">添加进待拆分</a>
            </div>
        </div>
    </div>
</body>
</html>
