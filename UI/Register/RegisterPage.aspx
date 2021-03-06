﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegisterPage.aspx.cs" Inherits="Register_RegisterPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>入住登记</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <style type="text/css">
        .mask
        {
            position: absolute;
            top: 0px;
            filter: alpha(opacity=60);
            background-color: #777;
            z-index: 1002;
            left: 0px;
            opacity: 0.5;
            -moz-opacity: 0.5;
        }
    </style>
    <script type="text/javascript">
       
        var hyZhekou = '', tqZhekou = '', sjfangjia = '';
        function jisuanfanghao() {
            $("#Fanghao").combobox("clear");
            var rows = $('#lfdata').datagrid('getRows');
            var data = $('#Fanghao').combobox("getData");
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    var flag = false;
                    for (var j = 0; j < data.length; j++) {

                        if (rows[i].FH == data[j].id) {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag) {
                        var o = { id: rows[i].FH, text: rows[i].FH };
                        data.push(o);

                    }
                }
                $("#Fanghao").combobox("loadData", data);
                $("#Fanghao").combobox('select', data[0].id);

                //$("#Fanghao").combobox("setValue", rows[0].FH);
            }
        }
        var zdfstime, zdfetime;
        function getzdftime() {
            var rows = $('#lfdata').datagrid('getRows'); //所有行
            if (rows != null && rows.length > 0) {
                $.ajax({
                    type: "post",
                    url: "../Setting/update_kfcgyTime.aspx?action=read&fjtype=" + rows[0].FJLX,
                    data: {
                        page: 1,
                        rows: 10000
                    },
                    success: function (result) {
                        var result = eval('(' + result + ')');

                        if (result.errorMsg) {
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        } else {
                            if (result.zdf_stime != '') {
                                zdfstime = result.zdf_stime;
                                zdfetime = result.zdf_etime;
                                if (zdfetime != '') {
                                    var ltime = $('#LidianTime').datetimebox("getValue");
                                    ltime = ltime.split(' ')[0] + " " + zdfetime;
                                    $('#LidianTime').datetimebox("setValue", ltime);

                                }
                            }
                        }
                    },
                    error: function (result) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                });
            }
        }
        $(document).ready(function () {
            //取消钟点房方案
            $("#ZhongDian").change(function () {
                if (!$("#ZhongDian").attr("checked")) {
                    var rows = $('#lfdata').datagrid('getRows'); //所有行
                    for (var i = 0; i < rows.length; i++) {
                        $('#lfdata').datagrid('updateRow', {
                            index: i,
                            row: {
                                StdPrice: dj,
                                Price: dj
                            }
                        });
                        $("#ShijiFangjia").val(dj);
                    }
                }
                else { //复选框是选中的
                    //当前时间是否在钟点房时间段内
                    var rows = $('#lfdata').datagrid('getRows'); //所有行
                    if (rows != null && rows.length > 0) {
                        $.ajax({
                            type: "get",
                            url: '../Setting/update_kfcgyTime.aspx?action=readall&id=' + rows[0].FJLX,
                            success: function (result) {
                                var result = eval('(' + result + ')');
                                if (result.errorMsg) {
                                    $('#ZhongDian').attr("checked", false);
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.errorMsg
                                    });
                                }
                                else {

                                    var stimeH = result.zdf_stime.split(':')[0] - 0;
                                    var stimeM = result.zdf_stime.split(':')[1] - 0;
                                    var stimeS = result.zdf_stime.split(':')[2] - 0;

                                    var etimeH = result.zdf_etime.split(':')[0] - 0;
                                    var etimeM = result.zdf_etime.split(':')[1] - 0;
                                    var etimeS = result.zdf_etime.split(':')[2] - 0;

                                    var dt = new Date();
                                    var hourstime = dt.getHours();
                                    var minstime = dt.getMinutes();
                                    var secstime = dt.getSeconds();
                                    if (hourstime < stimeH || hourstime > etimeH) {

                                        alert("该时段不能设置钟点房");
                                        $('#ZhongDian').attr("checked", false);
                                        return;
                                    } else if ((hourstime == stimeH && minstime < stimeM)
                                          || (hourstime == etimeH && minstime > etimeM)) {
                                        alert("该时段不能设置钟点房");
                                        $('#ZhongDian').attr("checked", false);
                                        return;
                                    } else if (((hourstime == stimeH && minstime == stimeM) && (secstime < stimeS))
                                  || ((hourstime == etimeH && minstime == etimeM) && (hourstime > etimeS))) {
                                        alert("该时段不能设置钟点房");
                                        $('#ZhongDian').attr("checked", false);
                                        return;
                                    } else {
                                        //是否有起步时长
                                        if (rows != null && rows.length > 0) {
                                            $.ajax({
                                                type: "get",
                                                url: '../Setting/ZD_selectData.aspx?action=read&id=' + rows[0].FJLX,
                                                success: function (result) {
                                                    var result = eval('(' + result + ')');
                                                    if (result.errorMsg) {
                                                        $.messager.show({
                                                            title: 'Error',
                                                            msg: result.errorMsg
                                                        });
                                                    }
                                                    else {
                                                        if (result == null || result.length <= 0) {
                                                            alert('该房间类型未设置钟点房');
                                                            $('#ZhongDian').attr("checked", false);
                                                            return;
                                                        }
                                                        var startlen = result[0].StartLen + 0;
                                                        var dt = new Date();

                                                        var hourstime = dt.getHours() + startlen / 60 + '';

                                                        var ltime = dt.getYear() + ":" + dt.getMonth() + ":" + dt.getDay() + " " + hourstime + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                                                        $('#LidianTime').datetimebox("setValue", ltime);
                                                    }
                                                },
                                                error: function (result) {
                                                    alert(result);
                                                    $('#ZhongDian').attr("checked", false);
                                                    $.messager.show({
                                                        title: 'Error',
                                                        msg: result.errorMsg
                                                    });
                                                }
                                            });
                                        }

                                    }
                                }
                            },
                            error: function (result) {
                                alert(result);
                                $('#ZhongDian').attr("checked", false);
                                $.messager.show({
                                    title: 'Error',
                                    msg: result.errorMsg
                                });
                            }
                        });
                    }

                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].FJLX != rows[0].FJLX) {
                            $('#lfdata').datagrid('deleteRow', i);
                            i = 0;
                        }
                    }
                    //getzdftime();
                }
            });



            $("#ZhengJianHao").change(function () {
                var strID = $("#ZhengJianHao").val();

                if (strID.length > 6) {
                    $.post('../Member/MemberChargeData.aspx?action=getAddress', { id: strID.substr(0, 6) }, function (result) {
                        if (!result.errorMsg) {

                            $("#DiZhi").val(result);
                        } else {
                            $.messager.show({    // show error message
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        }
                    }, 'json');
                }

            });

            //折扣率不能低于会员折扣，特权人折扣，会员折扣存在则使用会员折扣 
            $('#ZheKouLv').change(function () {
                var zhekou = $('#ZheKouLv').val() - 0;
                if (Number(hyZhekou) > 0) {//存在会员折扣  
                    if (zhekou < Number(hyZhekou)) {
                        $('#ZheKouLv').val(hyZhekou);
                    }
                }
                else if (Number(tqZhekou) > 0) {//存在特权折扣 
                    if (zhekou < Number(tqZhekou)) {
                        $('#ZheKouLv').val(tqZhekou);
                    }
                }

                $('#ShijiFangjia').val(Number(sjfangjia) * Number($('#ZheKouLv').val()) / 100);
            });

            $('#ShijiFangjia').change(function () {
                if (Number(sjfangjia) > 0) {
                    var s = 100 * Number($('#ShijiFangjia').val()) / Number(sjfangjia) + "";
                    var str = s.substring(0, s.indexOf(".") + 3);
                    $('#ZheKouLv').val(str);
                }
            });


            $("#HuaiYuanKa").change(function () {

                $.post('../Member/MemberChargeData.aspx?action=getJiFen', { MemberCardNo: $("#HuaiYuanKa").val() }, function (result) {

                    if (!result.errorMsg) {
                        $("#JiFen").val(result.RestScore);
                        hyZhekou = result.ZheKou
                        $('#ZheKouLv').attr("readonly", false);
                    } else {
                        hyZhekou = '';
                        $("#JiFen").val('');
                        $.messager.show({
                            title: 'Error',
                            msg: '没有该会员卡'
                        });

                    }
                }, 'json');
            });
            $("#ChangBao").change(function () {

                if ($("#ChangBao").attr("checked")) {
                    $('#ZhongDian').attr("checked", false);
                }
            });
            $("#ZhongDian").change(function () {
                if ($("#ZhongDian").attr("checked")) {
                    $('#ChangBao').attr("checked", false);
                }
            });


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
                editable: true,
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

            //特权人 TeQuanRen
            $("#TeQuanRen").combobox({
                url: '../Setting/BasicInfoData.aspx?module=TeQuanRen&action=read',
                valueField: 'TeQuanRen',
                textField: 'TeQuanRen',
                editable: true,
                onLoadSuccess: function () {
                    var data = $('#TeQuanRen').combobox('getData');
                    if (data.length > 0) {
                        // $("#TeQuanRen").combobox('select', data[0].TeQuanRen);


                    }
                },
                onChange: function () {
                    tqchange();
                }
            });
            //初始化入住房间信息表格
            $('#lfdata').datagrid({
                iconCls: 'icon-edit',
                singleSelect: true,
                columns: [[
                        { field: 'action', title: '操作', width: 80, align: 'center',
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
                        ZKL: '',
                        Price: ''
                    }
                });
                jisuanfanghao();
                dynFangjian(index);
                sjfangjia = dj;
                $('#ShijiFangjia').val(dj); //实际房价

            }
            //初始化随客信息表格
            $('#SuikeData').datagrid({
                iconCls: 'icon-edit',
                singleSelect: true,
                columns: [[
                        { field: 'SuikeFangHao', title: '房号', width: 110, editor: 'text' },
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
            var index = getRowIndex(target);
            if (index == 0) {//第一行不编辑
                return;
            }
            $('#lfdata').datagrid('beginEdit', getRowIndex(target));

            dynFangjian(index);
        }
        function deleterow(target) {
            $.messager.confirm('Confirm', '确认删除该条记录?', function (r) {
                if (r) {
                    $('#lfdata').datagrid('deleteRow', getRowIndex(target));
                    jisuanfanghao();
                    //如果删除的是第一行则清掉原来第二行数据
                    var index = getRowIndex(target);
                    if (index == 0) {

                    }
                }
            });
        }
        function saverow(target) {
            $('#lfdata').datagrid('endEdit', getRowIndex(target));
        }
        function cancelrow(target) {
            $('#lfdata').datagrid('cancelEdit', getRowIndex(target));
        }
        var orderid = '';
        //保存登记信息到数据库,该页唯一提交信息逻辑
        function CreateRegister() {
            //显示遮罩层，防止多次操作
            showMask();
            //入住信息
            var rzstr = '';
            var data = $('#lfdata').datagrid('getRows');
            if (data.length > 0) {
                for (var i = 0; i < data.length - 1; i++) {
                    rzstr = rzstr + '{"FangJianHao":"' + data[i].FH + '","XingMing":"' + data[i].Name + '","ZhengjianHaoma":"' + data[i].IDCard + '","YuanFangJia":"' + data[i].StdPrice + '","ZheKouLv":"' + data[i].ZKL + '","ShijiFangjia":"' + data[i].Price + '","JBName":"' + data[i].FJLX + '"};';
                }
                rzstr = rzstr + '{"FangJianHao":"' + data[data.length - 1].FH + '","XingMing":"' + data[data.length - 1].Name + '","ZhengjianHaoma":"' + data[data.length - 1].IDCard + '","YuanFangJia":"' + data[i].StdPrice + '","ZheKouLv":"' + data[i].ZKL + '","ShijiFangjia":"' + data[i].Price + '","JBName":"' + data[i].FJLX + '"}';
            }
            //随客信息
            var skstr = '';
            var data = $('#SuikeData').datagrid('getRows');
            if (data.length > 0) {
                for (var i = 0; i < data.length - 1; i++) {
                    skstr = skstr + '{"XingMing":"' + data[i].XingMing + '","XingBie":"' + data[i].XingBie + '","Address":"' + data[i].Address + '","CarNum":"' + data[i].CarNum + '","BeiZhu":"' + data[i].Remark + '","Card":"' + data[i].Card + '"};';
                }
                skstr = skstr + '{"XingMing":"' + data[data.length - 1].XingMing + '","XingBie":"' + data[data.length - 1].XingBie + '","Address":"' + data[i].Address + '","CarNum":"' + data[i].CarNum + '","BeiZhu":"' + data[i].Remark + '","Card":"' + data[data.length - 1].Card + '"}';
            }
            $('#fm').form('submit', {
                url: 'SaveRegisterData.aspx?action=create&list=' + rzstr + '&sk=' + skstr,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    // var result = eval('(' + result + ')');
                    CloseMask();
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        // close the dialog
                        orderid = result;

                        alert("入住信息保存成功");
                        //提示是否打印
                        $.messager.confirm('Confirm', '确定打印?', function (r) {
                            if (r) {

                                //window.close();
                                window.open("RegisterPagePrintA4.html", "print");
                            }
                        });
                        window.opener.location.reload();
                        var qstr = getQueryString();
                        window.location.href = 'RegisterModify.aspx?' + qstr[0];
                    }
                    //隐藏遮罩层
                   
                }
            });
        }
        function ClearForm() {
            window.open("RegisterPagePrintA4.html", "print");
            // window.open("RegisterPagePrint76.html", "print");
            //  this.window.close();
        }
        //显示选择房间对话框
        function ShowDlg() {
            $('#fjdlg').dialog('open').dialog('setTitle', '选择房间');
        }

        function SelectFJ() {
            //get kfdata selected info
            var kfs = $('#kfdata').datagrid('getSelections');
            for (var i = 0; i < kfs.length; i++) {
                var index = $('#lfdata').datagrid('getRows').length;
                //每个房间1条待输记录
                $('#lfdata').datagrid('insertRow', {
                    index: index,
                    row: {
                        FH: kfs[i].FH,
                        FJLX: kfs[i].JBName,
                        Name: '',
                        IDCard: '',
                        StdPrice: kfs[i].DJ,
                        ZKL: '100',
                        Price: kfs[i].DJ
                    }
                });

                dynFangjian(index);


            }
            $('#fjdlg').dialog('close');

            jisuanfanghao();
        }
        function dynFangjian(index) {
            if (index == 0) {//第一行不编辑
                return;
            }
            //绑定折扣改变事件

            $('#lfdata').datagrid('selectRow', index)
                        .datagrid('beginEdit', index);
            var ed = $('#lfdata').datagrid('getEditor', { index: index, field: 'ZKL' }); ;
            ed.target.bind("keyup", function () {
                //var counts = $('#lfdata').datagrid('getEditor', { index: index, field: 'ZKL' }).target.numberbox("getValue");
                var row = $('#lfdata').datagrid('getSelected');
                var counts = ed.target.val();
                var stdprice = row.StdPrice; // $('#lfdata').datagrid('getEditor', { index: index, field: 'StdPrice' }).target.numberbox("getValue");
                var valvalue = stdprice * counts / 100;
                $('#lfdata').datagrid('getEditor', { index: index, field: 'Price' }).target.numberbox("setValue", valvalue);
                //如果是第一条记录给文本框赋值

                if (index == 0) {

                    $('#ShijiFangjia').val(valvalue); //实际房价
                    $('#ZheKouLv').val(counts); //折扣率
                }
            })
            var ed2 = $('#lfdata').datagrid('getEditor', { index: index, field: 'Price' }); ;
            ed2.target.bind("keyup", function () {
                //var counts = $('#lfdata').datagrid('getEditor', { index: index, field: 'ZKL' }).target.numberbox("getValue");
                var row = $('#lfdata').datagrid('getSelected');
                var counts = ed2.target.val();
                var stdprice = row.StdPrice; // $('#lfdata').datagrid('getEditor', { index: index, field: 'StdPrice' }).target.numberbox("getValue");
                var valvalue = counts * 100 / stdprice;
                $('#lfdata').datagrid('getEditor', { index: index, field: 'ZKL' }).target.numberbox("setValue", valvalue);
                //如果是第一条记录给文本框赋值
                if (index == 0) {

                    $('#ShijiFangjia').val(counts); //实际房价
                    $('#ZheKouLv').val(valvalue); //折扣率
                }
            })
        }
        //Save Suike info
        function SaveSuiKe() {
            var Fanghao2 = $('#Fanghao').datebox("getValue");
            var name = this.document.getElementById('Name').value;
            // var sex = this.document.getElementByI d('Sex').value;
            var sex = $('#Sex').datebox("getValue");
            var card = this.document.getElementById('Card').value;
            var address = this.document.getElementById('Address').value;
            var carnum = this.document.getElementById('CarNum').value;
            var remark = this.document.getElementById('Remark').value;
            var index = 0;

            var rows = $('#SuikeData').datagrid('getRows'); //所有行
            //每个房间1条待输记录
            var row = $('#SuikeData').datagrid('getSelected'); //所选行
            if (row) {
                if (rows != null && rows.length > 0) {
                    for (var i = 0; i < rows.length; i++) {
                        if (card != row.Card) {
                            if (card == rows[i].Card) {
                                alert("身份证号码已经存在");
                                return;
                            }
                        }
                    }
                }
                var rowIndex = $('#SuikeData').datagrid('getRowIndex', $('#SuikeData').datagrid('getSelected'));
                $('#SuikeData').datagrid('updateRow', {
                    index: rowIndex,
                    row: {
                        SuikeFangHao: Fanghao2,
                        XingMing: name,
                        XingBie: sex,
                        Card: card,
                        Address: address,
                        CarNum: carnum,
                        Remark: remark
                    }
                });
            }
            else {
                if (rows != null && rows.length > 0) {
                    for (var i = 0; i < rows.length; i++) {
                        if (card == rows[i].Card) {
                            alert("身份证号码已经存在");
                            return;
                        }
                    }
                }
                $('#SuikeData').datagrid('insertRow', {
                    index: index,
                    row: {
                        SuikeFangHao: Fanghao2,
                        XingMing: name,
                        XingBie: sex,
                        Card: card,
                        Address: address,
                        CarNum: carnum,
                        Remark: remark
                    }
                });
            }
        }
        $.fn.datebox.defaults.formatter = function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + m + '-' + d;
        }
        // 打印
        var tel, CheckInMemo1, CheckInMemo12, CheckInMemo3, DaXieMoney;
        function getTel() {
            $.ajax({
                type: "get",
                url: '../Setting/ParameterInfoData.aspx?action=readTel',
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {

                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                    else {
                        tel = result.tel;
                        return tel;
                    }
                },
                error: function (result) {
                    alert(result);
                    $.messager.show({
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            });
        }
        function getPrintRemark() {
            $.ajax({
                type: "get",
                url: '../Setting/ParameterInfoData.aspx?action=read',
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                    else {
                        CheckInMemo1 = result.CheckInMemo1;
                        CheckInMemo2 = result.CheckInMemo2;
                        CheckInMemo3 = result.CheckInMemo3;
                    }
                },
                error: function (result) {
                    $.messager.show({
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            });
        }
        //押金转成大写
        function changeMoneyToChinese(money) {
            var cnNums = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"); //汉字的数字
            var cnIntRadice = new Array("", "拾", "佰", "仟"); //基本单位
            var cnIntUnits = new Array("", "万", "亿", "兆"); //对应整数部分扩展单位
            var cnDecUnits = new Array("角", "分", "毫", "厘"); //对应小数部分单位
            var cnInteger = "整"; //整数金额时后面跟的字符
            var cnIntLast = "元"; //整型完以后的单位
            var maxNum = 999999999999999.9999; //最大处理的数字
            var IntegerNum; //金额整数部分
            var DecimalNum; //金额小数部分
            var ChineseStr = ""; //输出的中文金额字符串
            var parts; //分离金额后用的数组，预定义
            if (money == "") {
                return "";
            }
            money = parseFloat(money);
            //alert(money);
            if (money >= maxNum) {
                $.alert('超出最大处理数字');
                return "";
            }
            if (money == 0) {
                ChineseStr = cnNums[0] + cnIntLast + cnInteger;
                //document.getElementById("show").value=ChineseStr;
                return ChineseStr;
            }
            money = money.toString(); //转换为字符串
            if (money.indexOf(".") == -1) {
                IntegerNum = money;
                DecimalNum = '';
            } else {
                parts = money.split(".");
                IntegerNum = parts[0];
                DecimalNum = parts[1].substr(0, 4);
            }
            if (parseInt(IntegerNum, 10) > 0) {//获取整型部分转换
                zeroCount = 0;
                IntLen = IntegerNum.length;
                for (i = 0; i < IntLen; i++) {
                    n = IntegerNum.substr(i, 1);
                    p = IntLen - i - 1;
                    q = p / 4;
                    m = p % 4;
                    if (n == "0") {
                        zeroCount++;
                    } else {
                        if (zeroCount > 0) {
                            ChineseStr += cnNums[0];
                        }
                        zeroCount = 0; //归零
                        ChineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
                    }
                    if (m == 0 && zeroCount < 4) {
                        ChineseStr += cnIntUnits[q];
                    }
                }
                ChineseStr += cnIntLast;
                //整型部分处理完毕
            }
            if (DecimalNum != '') {//小数部分
                decLen = DecimalNum.length;
                for (i = 0; i < decLen; i++) {
                    n = DecimalNum.substr(i, 1);
                    if (n != '0') {
                        ChineseStr += cnNums[Number(n)] + cnDecUnits[i];
                    }
                }
            }
            if (ChineseStr == '') {
                ChineseStr += cnNums[0] + cnIntLast + cnInteger;
            }
            else if (DecimalNum == '') {
                ChineseStr += cnInteger;
            }
            return ChineseStr;
        }

        function getPrintData() {
            $.ajaxSetup({ async: false });
            getTel();    //酒店联系电话
            getPrintRemark();     //备注
            DaXieMoney = "人民币" + changeMoneyToChinese($("#txtYaJin").val() != "" ? $("#txtYaJin").val() : "0"); //大写押金
            var name = $("#gettext").val(); //姓名
            var danhao = $("#ShougongDanhao").val(); //单号
            var arr = $('#lfdata').datagrid('getData'); //所有数据
            var fh = arr.rows[0].FH; //房号
            var toddaymoney = arr.rows[0].StdPrice;    //当日房价
            // alert(toddaymoney);
            var tongzhu = "";
            if (arr != null && arr.rows.length > 1) {
                for (var i = 1; i < arr.rows.length; i++) {
                    if (tongzhu != "") {
                        tongzhu = tongzhu + arr.rows[i].FH;
                    }
                    else {
                        tongzhu = arr.rows[i].FH;
                    }
                    if (i < arr.rows.length - 1) {
                        tongzhu = tongzhu + ",";
                    }
                }
            }
            if (tongzhu != "") {
                tongzhu = "同住" + tongzhu;
            }         //A4 房间  同住
            var cardname = $("#ZhengjianCombo").datebox('getValue'); //证件
            var begintime = $('#DaodianTime').datebox('getValue'); //到时
            var endtime = $('#LidianTime').datebox('getValue'); //离时
            var cardnum = $("#ZhengJianHao").val(); //号码
            var dizhi = $("#DiZhi").val(); //地址
            var fukuan = $('#FkfsCombo').datebox('getValue'); //付款方式
            var money = $("#txtYaJin").val() != "" ? $("#txtYaJin").val() : "0"; //押金
            //房型
            var shouyin = '<%=Session["user"].ToString() %>'; //收银员
            var huiyuan = $("#HuaiYuanKa").val(); //会员
            var xieyi = $('#XieyiDanwei').datebox('getValue');  //协议
            var rows = $("#lfdata").datagrid("getRows"); //rows
            var time = (new Date()).getTime();    //打印时间
            var kerentel = $("#DianHua").val();   //宾客电话
            var sex = $('#XingBie').datebox('getValue'); //性别
            var tePerson = $('#TeQuanRen').datebox('getValue'); //特权人

            return {
                XingMing: name,
                ShougongDanhao: danhao,
                FangJianHao: fh,
                Card: cardname,
                beginTime: begintime,
                endTime: endtime,
                haoma: cardnum,
                DiZhi: dizhi,
                Style: fukuan,
                YaJin: money,
                DaXieYaJin: DaXieMoney,
                Shouyin: shouyin,
                HuaiYuanKa: huiyuan,
                XieYiDanWei: xieyi,
                dayinTime: begintime,
                tel: kerentel,
                remark1: CheckInMemo1,
                remark2: CheckInMemo2,
                remark3: CheckInMemo3,
                JiuDianTell: tel,
                //  beizhu: CheckInMemo1+ CheckInMemo2 + CheckInMemo3,
                sex: sex,
                data_2: fh + tongzhu,
                TodayFJ: toddaymoney,
                Tequanren: tePerson,
                rows: rows
            };
        }
        function tqchange() {

            if (hyZhekou != '' || hyZhekou != null || $('#TeQuanRen').datebox('getValue') != null || $('#TeQuanRen').datebox('getValue') != '') {

                $.post('../Setting/BasicInfoData.aspx?module=TeQuanRen&action=getZhekou', { TeQuanRen: $('#TeQuanRen').datebox('getValue') }, function (result) {

                    if (!result.errorMsg) {
                        tqZhekou = result;
                        $('#ZheKouLv').attr("readonly", false);
                    } else {
                        tqZhekou = '';
                        $.messager.show({
                            title: 'Error',
                            msg: '没有该会员卡'
                        });
                    }

                }, 'json');
            }
        }

        function CreateZhongDianFang() {
            if ($("#ZhongDian").attr("checked")) {
                var data = $('#lfdata').datagrid('getRows');
                if (data.length > 0) {
                    //判断有没有设置钟点房方案
                    $.post('../Setting/ZD_selectData.aspx?action=getZDByLX', { FJLX: data[0].FJLX }, function (result) {
                        if (!result.errorMsg) {
                            if (result == "0") {
                                //显示选择钟点房方案页面
                                $('#ZD').dialog('open').dialog('setTitle', '选择' + data[0].FJLX + '钟点房房价方案');
                                $.get('../Setting/ZD_selectData.aspx', { action: 'read', id: data[0].FJLX }, function (result) {
                                    $("#ZDdg").datagrid("loadData", result);
                                }, 'json');
                            }
                            else {
                                $.messager.show({
                                    title: 'Error',
                                    msg: '所选择的房型没有设置钟点房方案'
                                });
                            }

                        } else {
                            $.messager.show({
                                title: 'Error',
                                msg: '查询失败'
                            });
                        }
                    });
                }
            }
        }



        //获取URL中的QueryString 
        function getQueryString() {

            var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));

            if (result == null) {

                return "";

            }

            for (var i = 0; i < result.length; i++) {

                result[i] = result[i].substring(1);

            }

            return result;

        }


        //兼容火狐、IE8
        //显示遮罩层
        function showMask() {
            $("#mask").css("height", $(document).height());
            $("#mask").css("width", $(document).width());
            $("#mask").show();
        }
        //关闭遮罩层
        function CloseMask() {
            $("#mask").show();
        }

    </script>
</head>
<body class="easyui-layout">
    <!--显示遮罩层-->
    <div id="mask" class="mask">
    </div>
    <!--客户信息-->
    <div class="easyui-tabs" style="padding: 2px" id="suikediv" name="suikediv">
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
                        <input type="checkbox" title="长包房" id="ChangBao" name="ChangBao">&nbsp;&nbsp;长包房</input>
                        <input type="checkbox" title="钟点房" id="ZhongDian" name="ZhongDian">&nbsp;&nbsp;钟点房</input>
                    </td>
                    <td style="width: 120px;">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="CreateZhongDianFang()">
                            钟点房设置</a>
                    </td>
                    <td style="width: 240px;">
                        <input type="checkbox" name="enablejx">叫醒服务</input>
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
                        <select class="easyui-combobox" name="XingBie" id="XingBie">
                            <option value="男">男</option>
                            <option value="女">女</option>
                        </select>
                    </td>
                    <td>
                        电话
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DianHua" id="DianHua"></input>
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
                        <input class="easyui-validatebox" type="text" id="ZhengJianHao" name="ZhengJianHao"></input>
                    </td>
                    <td style="width: 80px;">
                        地址
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DiZhi" id="DiZhi"></input>
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
                        <input name="DaodianTime" id="DaodianTime" class="easyui-datetimebox" data-options="showSeconds:false"
                            value="<%=dr%>" />
                    </td>
                    <td>
                        离店时间
                    </td>
                    <td>
                        <input name="LidianTime" id="LidianTime" class="easyui-datetimebox" data-options="showSeconds:false"
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
                                    <input class="easyui-validatebox" type="text" id="HuaiYuanKa" name="HuaiYuanKa"></input>
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
                        <input class="easyui-validatebox" type="text" name="JiFen" id="JiFen" readonly></input>
                    </td>
                    <td>
                        协议单位
                    </td>
                    <td>
                        <input class="easyui-combobox" name="XieyiDanwei" id="XieyiDanwei" data-options="valueField:'Name',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'">
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td style="width: 80px;">
                        押金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="YaJin" id="txtYaJin" />
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
                        <input class="easyui-combobox" id="TeQuanRen" name="TeQuanRen" />
                    </td>
                </tr>
                <tr style="height: 30px">
                    <td>
                        折扣率
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" id="ZheKouLv" name="ZheKouLv" value="100" />%
                    </td>
                    <td style="width: 80px;">
                        实际房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="ShijiFangjia" name="ShijiFangjia" />
                    </td>
                    <td>
                        手工单号
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ShougongDanhao" id="ShougongDanhao" />
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
                       车牌号
                    </td>
                    <td>
                         <input class="easyui-validatebox" type="text" name="Text1" id="Text1" />
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
                <tr>
                    <td colspan="6">
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
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div title="随客信息" style="padding: 2px">
            <table id="SuikeData" title="随客信息" style="height: 200px" class="easyui-datagrid">
            </table>
            <form id="fmsuike" method="post">
            <table style="padding: 10px">
                <tr style="height: 30px">
                    <td style="width: 55px; margin-right: 10px">
                        房号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" type="text" id="Fanghao" name="Fanghao" />
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
                        <%-- <select class="easyui-combobox" type="text" id="Sex" name="Sex" >--%>
                        <select class="easyui-combobox" id="Sex" name="Sex">
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
                        <input id="Zhengjian" class="easyui-combobox" />
                    </td>
                    <td style="width: 50px; margin-right: 10px">
                        证件号码:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Card" data-options="required:true" />
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
                        <input class="easyui-validatebox" type="text" id="Address" />
                    </td>
                    <td style="width: 55px; margin-right: 10px">
                        国籍:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" id="Guoji" />
                    </td>
                    <td style="width: 50px; margin-right: 10px">
                        手机:
                    </td>
                    <td style="width: 160px;">
                        <input id="Tel" class="easyui-validate" />
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
            </form>
        </div>
    </div>
    <div title="钟点房方案">
        <div id="ZD" title="钟点房方案" class="easyui-dialog" style="width: 580px; height: 350px;"
            closed="true" buttons="#ZDdlg-buttons">
            <form id="FRZD" method="post">
            <table id="ZDdg" class="easyui-datagrid" style="padding: 0px;" buttons="#btn" fitcolumns="true"
                singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'id',width:30">
                            序号
                        </th>
                        <th data-options="field:'f_jb',width:55">
                            房间级别
                        </th>
                        <th data-options="field:'FangAnName',width:75">
                            钟点房方案
                        </th>
                        <th data-options="field:'StartLen',width:55">
                            起步时长
                        </th>
                        <th data-options="field:'StartFee',width:55">
                            起步价格
                        </th>
                        <th data-options="field:'AddLen',width:55">
                            加钟时间
                        </th>
                        <th data-options="field:'AddFee',width:55">
                            加钟价格
                        </th>
                        <th data-options="field:'MinLen',width:55">
                            最小时长
                        </th>
                        <th data-options="field:'MinFee',width:55">
                            最小价格
                        </th>
                        <th data-options="field:'MaxLen',width:50">
                            最大时长
                        </th>
                    </tr>
                </thead>
            </table>
            </form>
        </div>
        <div id="ZDdlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveZD()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#ZD').dialog('close')">取消</a>
        </div>
        <script type="text/javascript">
            function saveZD() {
                var row = $('#ZDdg').datagrid('getSelected');
                if (row) {
                    $("#ShijiFangjia").val(row.StartFee);
                    var rows = $('#lfdata').datagrid('getRows'); //所有行
                    for (var i = 0; i < rows.length; i++) {
                        // alert(rows[i].FJLX);
                        if (rows[i].FJLX == rows[0].FJLX)//判断是不是同一房型（以第一条数据为准） 如果是  则修改价钱 
                        {
                            $('#lfdata').datagrid('updateRow', {
                                index: i,
                                row: {
                                    StdPrice: row.StartFee,
                                    Price: row.StartFee
                                }
                            });
                        }
                        //                        else {// 如果不是 其他房型则删除
                        //                            $('#lfdata').datagrid('deleteRow', i);
                        //                            i = 0;
                        //                        }
                    }
                    $('#ZD').dialog('close');
                }
            }

        </script>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#SuikeData').datagrid({
                onClickRow: function (index, data) {
                    var row = $('#SuikeData').datagrid('getSelected');
                    if (row) {
                        $("#Fanghao").combobox("setValue", row.SuikeFangHao);
                        $("#Name").val(row.XingMing);
                        $("#Sex").combobox("setValue", row.XingBie);
                        $("#Card").val(row.Card);
                        $("#Address").val(row.Address);
                        $("#CarNum").val(row.CarNum);
                        $("#Remark").val(row.Remark);
                        var rowIndex = $('#SuikeData').datagrid('getRowIndex', $('#SuikeData').datagrid('getSelected'));
                        $.messager.show({ title: 'information', msg: 'you select 1 row' });
                    }
                }
            })
        });
        function ClearSuiKe() {
            var row = $('#SuikeData').datagrid('getSelected');
            if (row) {
                var rowIndex = $('#SuikeData').datagrid('getRowIndex', $('#SuikeData').datagrid('getSelected'));
                $('#SuikeData').datagrid('deleteRow', rowIndex);
            }
        }
        $('#Card').change(function () {
            var rows = $('#SuikeData').datagrid('getRows');
            var card = $('#Card').val();
            for (var i = 0; i < rows.length + 1; i++) {
                if (card == rows[i].Card) {
                    alert("身份证号码已经存在");
                    break;
                }
            }
        });
    </script>
</body>
</html>
