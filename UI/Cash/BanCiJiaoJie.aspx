<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BanCiJiaoJie.aspx.cs" Inherits="Cash_BanCiJiaoJie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客人结帐退房</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        .table-a
        {
            border: 1px solid #000;
        }
        td
        {
            padding: 3px 3px 5px 5px;
        }
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
        function ShowDetail() {
            $('#Detail').dialog('open').dialog('setTitle', '班次结算汇总-按收退款差额交款(只交接备用金)');
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <table style="padding: 10px">
        <tr>
            <td style="width: 60px; padding: 5px; white-space: nowrap">
                选择班次：
            </td>
            <td style="width: 160px; padding: 5px">
                <input class="easyui-combobox" style="width: 130px" />
            </td>
            <td style="width: 60px; padding: 5px; white-space: nowrap">
                接班时间
            </td>
            <td style="width: 160px; padding: 5px">
                <input class="easyui-datetimebox" style="width: 130px" />
            </td>
            <td style="width: 60px; padding: 5px; white-space: nowrap">
                交班时间
            </td>
            <td style="width: 160px; padding: 5px">
                <input class="easyui-datetimebox" style="width: 130px" />
            </td>
            <td style="width: 100%">
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="padding: 5px;"  align="right";  colspan="7">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">交班汇总表</a> <a href="javascript:void(0)"
                    class="easyui-linkbutton" onclick="">明细打印</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        onclick="">收支明细表</a> <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ShowDetail()">
                            交班</a> <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">返回</a>
            </td>
        </tr>
    </table>
    <div class="easyui-tabs">
        <div title="现金报表">
            <table id="xj" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            科目名称
                        </th>
                        <th data-options="field:'FH',width:130">
                            单据号码
                        </th>
                        <th data-options="field:'FH',width:130">
                            房号
                        </th>
                        <th data-options="field:'FH',width:130">
                            姓名
                        </th>
                        <th data-options="field:'FH',width:130">
                            金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            付款方式
                        </th>
                        <th data-options="field:'FH',width:130">
                            时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            收银员
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="结账情况">
            <table id="jz" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            单号
                        </th>
                        <th data-options="field:'FH',width:130">
                            房号
                        </th>
                        <th data-options="field:'FH',width:130">
                            客户名称
                        </th>
                        <th data-options="field:'FH',width:130">
                            来店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            离店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            现金
                        </th>
                        <th data-options="field:'FH',width:130">
                            信用卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            借记卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            房费
                        </th>
                        <th data-options="field:'FH',width:130">
                            折扣费
                        </th>
                        <th data-options="field:'FH',width:130">
                            其它
                        </th>
                        <th data-options="field:'FH',width:130">
                            实际金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            结账退款
                        </th>
                        <th data-options="field:'FH',width:130">
                            结账收款
                        </th>
                        <th data-options="field:'FH',width:130">
                            信用卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            借记卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            代金券
                        </th>
                        <th data-options="field:'FH',width:130">
                            会员储值卡
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="其它费用">
            <table id="qt" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            姓名
                        </th>
                        <th data-options="field:'FH',width:130">
                            房号
                        </th>
                        <th data-options="field:'FH',width:130">
                            科目
                        </th>
                        <th data-options="field:'FH',width:130">
                            数量
                        </th>
                        <th data-options="field:'FH',width:130">
                            金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            日期
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="会员充值">
            <table id="cz" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            会员卡号
                        </th>
                        <th data-options="field:'FH',width:130">
                            充值金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            实收金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            付款方式
                        </th>
                        <th data-options="field:'FH',width:130">
                            时间
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="未结离店">
            <table id="wj" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            房号
                        </th>
                        <th data-options="field:'FH',width:130">
                            客户名称
                        </th>
                        <th data-options="field:'FH',width:130">
                            来店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            离店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            现金
                        </th>
                        <th data-options="field:'FH',width:130">
                            信用卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            借记卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            房费
                        </th>
                        <th data-options="field:'FH',width:130">
                            其它
                        </th>
                        <th data-options="field:'FH',width:130">
                            折扣费
                        </th>
                        <th data-options="field:'FH',width:130">
                            实际金额
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="现付佣金">
            <table id="xf" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            房间号
                        </th>
                        <th data-options="field:'FH',width:130">
                            支付人
                        </th>
                        <th data-options="field:'FH',width:130">
                            支付金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            支付类型
                        </th>
                        <th data-options="field:'FH',width:130">
                            单据号码
                        </th>
                        <th data-options="field:'FH',width:130">
                            备注
                        </th>
                        <th data-options="field:'FH',width:130">
                            时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            收银员
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
        <div title="记账回款">
            <table id="jz" class="easyui-datagrid">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:130">
                            单号
                        </th>
                        <th data-options="field:'FH',width:130">
                            房号
                        </th>
                        <th data-options="field:'FH',width:130">
                            客户名称
                        </th>
                        <th data-options="field:'FH',width:130">
                            来店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            离店时间
                        </th>
                        <th data-options="field:'FH',width:130">
                            现金
                        </th>
                        <th data-options="field:'FH',width:130">
                            信用卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            借记卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            房费
                        </th>
                        <th data-options="field:'FH',width:130">
                            其它
                        </th>
                        <th data-options="field:'FH',width:130">
                            折扣费
                        </th>
                        <th data-options="field:'FH',width:130">
                            实际金额
                        </th>
                        <th data-options="field:'FH',width:130">
                            结账退款
                        </th>
                        <th data-options="field:'FH',width:130">
                            结账收款
                        </th>
                        <th data-options="field:'FH',width:130">
                            信用卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            借记卡
                        </th>
                        <th data-options="field:'FH',width:130">
                            代金券
                        </th>
                        <th data-options="field:'FH',width:130">
                            支票收款
                        </th>
                        <th data-options="field:'FH',width:130">
                            单位记账
                        </th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <div id="Detail" class="easyui-dialog" closed="true" style="width: 750px; height: 520px;
        padding: 5px;">
        <table style="width: 100%">
            <tr>
                <td>
                    当班时间
                </td>
                <td>
                    <input class="easyui-datetimebox" data-options="showSeconds:true" />
                </td>
                <td>
                    至
                </td>
                <td>
                    <input class="easyui-datetimebox" data-options="showSeconds:true" />
                </td>
                <td>
                    当班操作员
                </td>
                <td>
                    <input class="easyui-combobox" />
                </td>
            </tr>
        </table>
        <table class="table-a" style="width: 100%">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    上班转入
                </td>
                <td>
                    预付金收款
                </td>
                <td>
                    预付金退款
                </td>
                <td>
                    会员充值
                </td>
                <td>
                    现付佣金
                </td>
                <td>
                    结账退款
                </td>
                <td>
                    结账收款
                </td>
                <td>
                    上交财务
                </td>
                <td>
                    财务下放金额
                </td>
                <td>
                    转下班金额
                </td>
            </tr>
            <tr>
                <td align="right">
                    现金
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    信用卡
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    借记卡
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    代金券
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    支票
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="right">
                    单位记账
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="11" style="height: 1px;">
                    <hr />
                </td>
            </tr>
            <tr>
                <td align="right">
                    合计
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="11" align="center" style="color: Red">
                    注：转下班金额=上班转入+预付金收款+预付金退款+本班会员充值+现付佣金+结账退款+结账收款-上交财务+财务下放金额
                </td>
            </tr>
        </table>
        <br />
        <table class="table-a" style="width: 100%">
            <tr>
                <td>
                    开房数量
                </td>
                <td>
                    0
                </td>
                <td>
                    结账退房数量
                </td>
                <td>
                    0
                </td>
                <td>
                    未离店数量结
                </td>
                <td>
                    0
                </td>
                <td>
                    结账房费
                </td>
                <td>
                    0
                </td>
            </tr>
            <tr>
                <td>
                    结账总金额
                </td>
                <td>
                    0
                </td>
                <td>
                    未结离店余额
                </td>
                <td>
                    0
                </td>
                <td>
                    结账商品费用
                </td>
                <td>
                    0
                </td>
                <td>
                    结账其它费用
                </td>
                <td>
                    0
                </td>
            </tr>
            <tr>
                <td>
                    会员储值消费
                </td>
                <td>
                    0
                </td>
                <td>
                    本班现金余额
                </td>
                <td>
                    0
                </td>
                <td>
                    本班信用卡余额
                </td>
                <td>
                    0
                </td>
                <td>
                    本班借记卡余额
                </td>
                <td>
                    0
                </td>
            </tr>
            <tr>
                <td>
                    撤销入住数量
                </td>
                <td>
                    0
                </td>
                <td>
                    开免费房数量
                </td>
                <td>
                    0
                </td>
                <td>
                    单位记账数量
                </td>
                <td>
                    0
                </td>
                <td>
                    单位记账回款
                </td>
                <td>
                    0
                </td>
            </tr>
        </table>
        <br />
        <table class="table-a" style="width: 100%">
            <tr>
                <td>
                    收银员
                </td>
                <td>
                    <input class="easyui-combobox" />
                </td>
                <td>
                    密码
                </td>
                <td>
                    <input class="easyui-validatebox" />
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">确定</a>
                </td>
                <td>
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">取消</a>
                </td>
            </tr>
        </table>
    </div>


</body>
</html>
