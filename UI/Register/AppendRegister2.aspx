<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppendRegister2.aspx.cs" Inherits="Register_AppendRegister2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>入住登记</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //国籍，默认中国
            $("#GuoJiCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=gj&action=read',
                valueField: 'gj',
                textField: 'gj',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#GuoJiCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#GuoJiCombo").combobox('select', data[1].gj);
                    }
                }
            });
            //国籍，默认中国
            $("#Guoji").combobox({
                url: '../Setting/BasicInfoData.aspx?module=gj&action=read',
                valueField: 'gj',
                textField: 'gj',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Guoji').combobox('getData');
                    if (data.length > 0) {
                        $("#Guoji").combobox('select', data[1].gj);
                    }
                }
            });
            //销售员
            $("#SalersCombo").combobox({
                url: '../Setting/SalersData.aspx?action=read',
                valueField: 'Name',
                textField: 'Name',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#SalersCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#SalersCombo").combobox('select', data[0].Name);
                    }
                }
            });
            //客人类别
            $("#KeLeiCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=khlb&action=read',
                valueField: 'KHLB',
                textField: 'KHLB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#KeLeiCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#KeLeiCombo").combobox('select', data[0].KHLB);
                    }
                }
            });
            //付款方式
            $("#FkfsCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=fkfs&action=read',
                valueField: 'fkfs',
                textField: 'fkfs',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#FkfsCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#FkfsCombo").combobox('select', data[0].fkfs);
                    }
                }
            });
            //证件类别
            $("#ZhengjianCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=zjlx&action=read',
                valueField: 'ZJLX',
                textField: 'ZJLX',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#ZhengjianCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#ZhengjianCombo").combobox('select', data[0].ZJLX);
                    }
                }
            });
            //证件类别
            $("#Zhengjian").combobox({
                url: '../Setting/BasicInfoData.aspx?module=zjlx&action=read',
                valueField: 'ZJLX',
                textField: 'ZJLX',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Zhengjian').combobox('getData');
                    if (data.length > 0) {
                        $("#Zhengjian").combobox('select', data[0].ZJLX);
                    }
                }
            });
            //初始化入住房间信息表格
            $('#lfdata').datagrid({
                iconCls: 'icon-edit',
                singleSelect: true,
                columns: [[
                        { field: 'action', title: '操作', align: 'center',
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
                        { field: 'IDCard', title: '证件号码', width: 160, editor: 'text' },
					    { field: 'StdPrice', title: '标准房价', width: 80, align: 'right' },
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
            //如果房号存在,则insert to datagrid
            var fh = '<%=fh %>';
            var jb = '<%=jb %>';
            var dj = '<%=dj %>';
            var index = 0;
            //每个房间1条待输记录
            if (fh != '0000') {
                $('#lfdata').datagrid('insertRow', {
                    index: index,
                    row: {
                        FH: fh,
                        FJLX: jb,
                        Name: '',
                        IDCard: '',
                        StdPrice: dj,
                        ZKL: '10.0',
                        Price: dj
                    }
                });
            }
            //初始化随客信息表格
            $('#SuikeData').datagrid({
                iconCls: 'icon-edit',
                singleSelect: true,
                columns: [[
                        { field: 'XingMing', title: '姓名', width: 110, editor: 'text' },
                        { field: 'XingBie', title: '性别', width: 110, editor: 'text' },
                        { field: 'Card', title: '证件号码', width: 110, editor: 'text' },
					    { field: 'Address', title: '地址', width: 110, align: 'text' },
					    { field: 'CarNum', title: '车牌号', width: 110, align: 'text' },
                        { field: 'Remark', title: '备注', width: 110, align: 'text' }
				        ]]
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
            $.messager.confirm('Confirm', '确认删除该条记录?', function (r) {
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

        //保存登记信息到数据库,该页唯一提交信息逻辑
        function CreateRegister() {
            //入住信息
            var rzstr = '';
            var data = $('#lfdata').datagrid('getRows');
            if (data.length > 0) {
                for (var i = 0; i < data.length - 1; i++) {
                    rzstr = rzstr + '{"FangJianHao":"' + data[i].FH + '","XingMing":"' + data[i].Name + '","ZhengjianHaoma":"' + data[i].IDCard + '","YuanFangJia":"' + data[i].StdPrice + '","ZheKouLv":"' + data[i].ZKL + '","ShijiFangjia":"' + data[i].Price + '"};';
                }
                rzstr = rzstr + '{"FangJianHao":"' + data[data.length - 1].FH + '","XingMing":"' + data[data.length - 1].Name + '","ZhengjianHaoma":"' + data[data.length - 1].IDCard + '","YuanFangJia":"' + data[i].StdPrice + '","ZheKouLv":"' + data[i].ZKL + '","ShijiFangjia":"' + data[i].Price + '"}';
            }
            //随客信息
            var skstr = '';
            var data = $('#SuikeData').datagrid('getRows');
            if (data.length > 0) {
                for (var i = 0; i < data.length - 1; i++) {
                    skstr = skstr + '{"XingMing":"' + data[i].XingMing + '","XingBie":"' + data[i].XingBie + '","Card":"' + data[i].Card + '"};';
                }
                skstr = skstr + '{"XingMing":"' + data[data.length - 1].XingMing + '","XingBie":"' + data[data.length - 1].XingBie + '","Card":"' + data[data.length - 1].Card + '"}';
            }
            $('#fm').form('submit', {
                url: 'SaveRegisterData.aspx?action=create&list=' + rzstr + '&sk=' + skstr,
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
        function ClearForm() {
            this.window.close();
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
        //Save Suike info
        function SaveSuiKe() {
            var name = this.document.getElementById('Name').value;
            var sex = this.document.getElementById('Sex').value;
            var card = this.document.getElementById('Card').value;
            var address = this.document.getElementById('Address').value;
            var carnum = this.document.getElementById('CarNum').value;
            var remark = this.document.getElementById('Remark').value;
            var index = 0;
            //每个房间1条待输记录
            $('#SuikeData').datagrid('insertRow', {
                index: index,
                row: {
                    XingMing: name,
                    XingBie: sex,
                    Card: card,
                    Address: address,
                    CarNum: carnum,
                    Remark: remark
                }
            });
        }
        $.fn.datebox.defaults.formatter = function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + m + '-' + d;
        }
    </script>
</head>
<body class="easyui-layout">
    <!--客户信息-->
    <div class="easyui-tabs" style="padding: 2px">
        <div title="主要信息" style="padding: 2px">
            <table id="lfdata" toolbar="#tbldiv" title="房间信息" style="height: 160px">
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
            <form id="fm" method="post">
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
                        <input type="checkbox" name="enablejx">叫醒服务</input>
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-datetimebox" name="JiaoxingFuwu" data-options="showSeconds:false"
                            value="<%=wakeuptime%>">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px; margin-right: 10px">
                        姓名
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" id="gettext" type="text" name="XingMing" data-options="required:true" />
                    </td>
                    <td>
                        性别
                    </td>
                    <td>
                        <select class="easyui-combobox" name="XingBie">
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
                        <input name="DaodianTime" class="easyui-datetimebox" data-options="showSeconds:false"
                            value="<%=dr%>" />
                    </td>
                    <td>
                        离店时间
                    </td>
                    <td>
                        <input name="LidianTime" class="easyui-datetimebox" data-options="showSeconds:false"
                            value="<%=lr%>" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        会员卡
                    </td>
                    <td style="width: 160px;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <input class="easyui-validatebox" type="text" name="HuaiYuanKa"></input>
                                </td>
                                <td>
                                    <input type="button" value="读卡" onclick="" />
                                </td>
                            </tr>
                        </table>
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
                        <input class="easyui-combobox" name="XieyiDanwei" data-options="valueField:'Name',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px;">
                        押金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="YaJin" disabled/>
                    </td>
                    <td>
                        付款方式
                    </td>
                    <td>
                        <input class="easyui-combobox" id="FkfsCombo" name="FukuanFangshi" />
                    </td>
                    <td>
                        特权人
                    </td>
                    <td>
                        <select class="easyui-combobox" name="TeQuanRen">
                        </select>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        折扣率
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ZheKouLv" />%
                    </td>
                    <td style="width: 80px;">
                        实际房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ShijiFangjia" />
                    </td>
                    <td>
                        手工单号
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ShougongDanhao" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        国籍
                    </td>
                    <td>
                        <input class="easyui-combobox" id="GuoJiCombo" type="text" name="Guoji" />
                    </td>
                    <td style="width: 80px;">
                        销售员
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" id="SalersCombo" type="text" name="XiaoShouYuan" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        备注
                    </td>
                    <td colspan="5">
                        <input class="easyui-validatebox" type="text" name="BeiZhu" style="width: 630px"></input>
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div title="随客信息" style="padding: 2px">
            <table id="SuikeData" title="随客信息" style="height: 200px">
            </table>
            <table style="padding: 10px">
                <tr style="height: 30px">
                    <td style="width: 55px; margin-right: 10px">
                        房号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" id="Fanghao"/>
                    </td>
                    <td style="width: 55px; margin-right: 10px">
                        姓名:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Name" />
                    </td>
                    <td style="width: 55px; margin-right: 10px">
                        性别:
                    </td>
                    <td style="width: 160px;">
                        <select class="easyui-combobox" type="text" id="Sex">
                            <option value="男">男</option>
                            <option value="女">女</option>
                        </select>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 55px; margin-right: 10px">
                        证件类型:
                    </td>
                    <td style="width: 160px;">
                        <input id="Zhengjian" class="easyui-combobox"/>
                    </td>
                    <td style="width: 50px; margin-right: 10px">
                        证件号码:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Card" />
                    </td>
                    <td style="width: 50px; margin-right: 10px">
                        车牌号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="CarNum"></input>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 50px; margin-right: 10px">
                        地址:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Address"/>
                    </td>
                    <td style="width: 55px; margin-right: 10px">
                        国籍:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" id="Guoji"/>
                    </td>
                    <td style="width: 50px; margin-right: 10px">
                        手机:
                    </td>
                    <td style="width: 160px;">
                         <input id="Tel" class="easyui-validate"/>
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 50px; margin-right: 10px">
                        备注:
                    </td>
                    <td colspan="3">
                        <input class="easyui-validatebox" type="text" id="Remark" style="width: 380px" />
                    </td>
                    <td style="width: 60px; margin-right: 10px">
                        &nbsp;
                    </td>
                    <td style="width: 160px;">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="SaveSuiKe()">保存</a>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearSuiKe()">取消</a>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <table style="padding: 5px 5px 5px 5px">
        <tr>
            <td style="width: 580px;">
                &nbsp;
            </td>
            <td style="width: 110px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="CreateRegister()">保存接洽</a>
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

