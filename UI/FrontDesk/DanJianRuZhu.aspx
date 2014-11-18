<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DanJianRuZhu.aspx.cs" Inherits="FrontDesk_DanJianRuZhu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新增单间入住</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //KeRenLeiBie
            $("#KeLeiCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=khlb&action=read',
                valueField: 'ID',
                textField: 'KHLB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#KeLeiCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#KeLeiCombo").combobox('select', data[0].ID);
                    }
                }
            });
            //FKFS
            $("#FkfsCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=fkfs&action=read',
                valueField: 'ID',
                textField: 'fkfs',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#FkfsCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#FkfsCombo").combobox('select', data[0].ID);
                    }
                }
            });
            //shenfenzheng
            $("#ZhengjianCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=zjlx&action=read',
                valueField: 'ID',
                textField: 'ZJLX',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#ZhengjianCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#ZhengjianCombo").combobox('select', data[0].ID);
                    }
                }
            });

        });

        function newRuZhu() {
            $('#fm').form('submit', {
                url: 'DanJianRuZhuData.aspx?action=create',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        // close the dialog
                        window.opener.location.reload();
                        window.close();
                    }
                }
            });
        }
        function clearForm() {
            this.window.close();
        }
    </script>
</head>
<body class="easyui-layout">
    <!--客户信息-->
    <div class="easyui-tabs" style="padding: 2px">
        <div title="主要信息" style="padding: 0px">
            <form id="fm" method="post">
            <div style="padding: 10px 0px 0px 10px; font-size: 15px; color: Red">
                房间信息:
            </div>
            <table style="padding: 10px">
                <tr>
                    <td style="width: 80px;">
                        房号
                    </td>
                    <td style="width: 160px;">
                        <input type="text" name="FH" value="<%=fh %>" readonly></input>
                    </td>
                    <td style="width: 80px;">
                        房间类型
                    </td>
                    <td style="width: 160px;">
                        <input type="text" name="JB" value="<%=fh %>" readonly></input>
                    </td>
                    <td style="width: 80px;">
                        标准房价
                    </td>
                    <td style="width: 160px;">
                        <input type="text" name="DJ" value="<%=fh %>" readonly></input>
                    </td>
                </tr>
            </table>
            <div style="padding: 0px 0px 0px 10px; font-size: 15px; color: Red">
                入住信息:
            </div>
            <table style="padding: 10px">
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        <input type="checkbox" title="开长途" name="ChangTu">&nbsp;&nbsp;开长途</input>
                        <input type="checkbox" title="开市话" name="ShiHua">&nbsp;&nbsp;开市话</input>
                    </td>
                    <td style="width: 80px;">
                        <input type="checkbox" name="BaoMi">&nbsp;保密</input>
                    </td>
                    <td style="width: 160px;">
                        <input type="checkbox" title="长包房" name="ChangBao">&nbsp;&nbsp;长包房</input>
                        <input type="checkbox" title="钟点房" name="ZhongDian">&nbsp;&nbsp;钟点房</input>
                    </td>
                    <td style="width: 80px;">
                        <input type="checkbox" name="JiaoxingFuwu">叫醒服务</input>
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-datetimebox" style="width: 140px">
                    </td>
                </tr>
                <tr  style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        姓名
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="XingMing" data-options="required:true"></input>
                    </td>
                    <td>
                        性别
                    </td>
                    <td>
                        <select class="easyui-combobox" name="XingBie" style="width: 140px">
                            <option value="男">男</option>
                            <option value="女">女</option>
                        </select>
                    </td>
                    <td>
                        电话
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DianHua"></input>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        证件类别
                    </td>
                    <td>
                        <input class="easyui-combobox" id="ZhengjianCombo" name="ZhengjianLeibie">
                    </td>
                    <td style="width: 80px;">
                        证件号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ZhengJianHao"></input>
                    </td>
                    <td style="width: 80px;">
                        地址
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DiZhi"></input>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        客人类别
                    </td>
                    <td>
                        <input class="easyui-combobox" id="KeLeiCombo" name="KerenLeibie">
                    </td>
                    <td>
                        到店时间
                    </td>
                    <td>
                        <input name="DaodianTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=DateTime.Today.ToShortDateString()%>">
                    </td>
                    <td>
                        离店时间
                    </td>
                    <td>
                        <input name="LidianTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=DateTime.Today.AddDays(1).ToShortDateString()%>">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        会员卡
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="HuaiYuanKa"></input>
                    </td>
                    <td>
                        积分
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="JiFen" readonly></input>
                    </td>
                    <td>
                        协议单位
                    </td>
                    <td>
                        <input class="easyui-combobox" id="Text2" name="XieyiDanwei" data-options="valueField:'ID',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px;">
                        押金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="YaJin"></input>
                    </td>
                    <td>
                        付款方式
                    </td>
                    <td>
                        <input class="easyui-combobox" id="FkfsCombo" name="FukuanFangshi">
                    </td>
                    <td>
                        特权人
                    </td>
                    <td>
                        <select class="easyui-combobox" name="TeQuanRen" style="width: 140px">
                        </select>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        折扣率
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ZheKouLv"></input>%
                    </td>
                    <td style="width: 80px;">
                        实际房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ShijiFangjia"></input>
                    </td>
                    <td>
                        手工单号
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ShougongDanhao"></input>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        备注
                    </td>
                    <td colspan="5">
                        <input class="easyui-validatebox" type="text" name="BeiZhu" style="width:630px"></input>
                    </td>
                </tr>
            </table>
            </form>
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="newRuZhu()">保存</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="clearForm()">取消</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
        </div>
        <div title="随客信息" style="padding: 0px">
            <table style="padding: 10px">
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        姓名:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        性别:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        身份证:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        地址:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        车牌号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        备注:
                    </td>
                    <td colspan="3">
                        <input class="easyui-validatebox" type="text" name="name" style="width: 380px"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">保存</a>
                    </td>
                    <td style="width: 160px;">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">取消</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
