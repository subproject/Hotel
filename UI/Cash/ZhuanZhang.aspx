<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhuanZhang.aspx.cs" Inherits="Cash_ZhuanZhang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>转帐</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function ClearForm() {
            this.window.close();
        }
        function Transfer() {
            var details = $('#runningdata').datagrid('getSelected');
            if (details) {

            }
            else {
                alert("请选择可选项目进行转账操作！");
            }
            var orders = $('#orders').datagrid('getSelected');
            if (orders) {

            }
            else {
                alert("请选择转入账单！");
            }
        }
    </script>
</head>
<body style="padding: 2px">
    <div class="easyui-layout" style="height: 500px; width: 800px">
        <div data-options="region:'north'" title="账单信息" style="height: 105px">
            <table style="padding: 10px">
                <tr>
                    <td style="width: 80px; padding: 5px">
                        账单编号
                    </td>
                    <td style="width: 160px; padding: 5px">
                        <%=AutoID%>
                    </td>
                    <td style="width: 80px; padding: 5px">
                        主账房间
                    </td>
                    <td style="width: 160px; padding: 5px">
                        <%=Fanghao%>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; padding: 5px">
                        客户名称
                    </td>
                    <td style="width: 160px; padding: 5px">
                        <%=Xingming%>
                    </td>
                    <td style="width: 80px; padding: 5px">
                        电话
                    </td>
                    <td style="width: 160px; padding: 5px">
                        <%=Dianhua%>
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'south'" style="height: 50px;">
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="Transfer()">保存</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
        </div>
        <div data-options="region:'east',split:true,title:'转入账单',collapsible:false" style="width: 400px;">
            <table class="easyui-datagrid" singleselect="true" id="orders" data-options="url:'../FrontDesk/OrderData.aspx?action=readcanselect&fh=<%=fh %>'">
                <thead>
                    <tr>
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
        </div>
        <div data-options="region:'center',title:'可选项目'">
            <table id="runningdata" class="easyui-datagrid" style="padding: 0px" url="RunningListData.aspx?action=readall&orderid=<%=OrderGuid %>">
                <thead>
                    <tr>
                        <th data-options="field:'OrderGuid',width:100" hidden>
                            账号
                        </th>
                        <th data-options="field:'CustomerName',width:100">
                            客户名称
                        </th>
                        <th data-options="field:'RoomNo',width:100">
                            房号
                        </th>
                        <th data-options="field:'KM',width:100">
                            科目
                        </th>
                        <th data-options="field:'Price',width:100">
                            消费金额
                        </th>
                        <th data-options="field:'Deposit',width:100">
                            押金金额
                        </th>
                        <th data-options="field:'Remark',width:100">
                            备注
                        </th>
                        <th data-options="field:'RunningNum',width:100">
                            单据号码
                        </th>
                        <th data-options="field:'RunningNumAuto',width:100">
                            单据号码(自动)
                        </th>
                        <th data-options="field:'RunningTime',width:100" hidden>
                            单据日期
                        </th>
                        <th data-options="field:'Payment',width:100">
                            付款方式
                        </th>
                        <th data-options="field:'Operator',width:100">
                            操作员
                        </th>
                        <th data-options="field:'Status',width:100">
                            状态
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
</body>
</html>
