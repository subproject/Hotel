<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FangJianYuDing.aspx.cs" Inherits="FrontDesk_FangJianYuDing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="UTF-8">
    <title>房间预定</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="../themes/demo.css"/>
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        var hyZhekou = '0';
        function myformatter(date) {
            var v = date.substring("/Date(".length, date.indexOf("+"));
            var d = new Date();
            d.setTime(v);
            var y = d.getFullYear();
            var m = d.getMonth() + 1;
            var d = d.getDate();
            return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        function myparser(s) {
            if (!s) return new Date();
            var ss = (s.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                return new Date(y, m - 1, d);
            } else {
                return new Date();
            }
        }
        function myparser2(s) {
            if (!s) return new Date();
            var ss = (s.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
         return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
            
        }
        function getKfInfo(starttime, endtime) {
            $.ajax({
                type: "post",
                url: "FangJianYuDingData.aspx?action=GetYdFjInfo&starttime=" + starttime + "&etime=" + endtime,
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
                        $('#fjtable').datagrid('reload');
                       
                        $("#fjtable").datagrid("loadData", result);
                    }
                },
                error: function (result) {
                    $.messager.show({// show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            });
        }

        function loadYdData() {
            $.ajax({
                type: "post",
                url: "FangJianYuDingData.aspx?action=getOrders&ydid=" + ydid,
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

                        $("#gdroder").datagrid("loadData", result);
                        editIndex = undefined;

                    }
                },
                error: function (result) {
                    $.messager.show({// show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                },
                beforeSend: function () {
                    //这里是开始执行方法，显示效果，效果自己写
                    $("#gdroder").datagrid("loading");
                },
                complete: function () {
                    //方法执行完毕，效果自己可以关闭，或者隐藏效果
                    $("#gdroder").datagrid("loaded");
                }
            });
        }
        function initYfdingjin() {
            $.ajax({
                type: "post",
                url: "FangJianYuDingData.aspx?action=ydshoukuanread&ydid=" + ydid,
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
                        $("#yufujinjilu").datagrid("loadData", result);
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

        $(document).ready(function () {

            initYfdingjin();
            //楼栋
            $("#Ld").combobox({
                url: '../Setting/BasicInfoData.aspx?module=Ld&action=read',
                valueField: 'ID',
                textField: 'Ld',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Ld').combobox('getData');

                    var len = data.length;
                    if (data.length > 0) {
                        $("#Ld").combobox('select', data[len].Ld);
                    }
                }
            });
            //加载所有楼层
            $("#Lc").combobox({
                url: '../Setting/BasicInfoData.aspx?module=Lc&action=readPart',
                valueField: 'ID',
                textField: 'Lc',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#Lc').combobox('getData');
                    var len = data.length;
                    if (data.length > 0) {
                        $("#Lc").combobox('select', data[len].Lc);
                    }
                }
            });
            //根据楼栋获得楼层
            $('#Ld').combobox({
                onSelect: function () {
                    var ldname = $('#Ld').datebox('getValue');
                    if (ldname == 0) {
                        $("#Lc").combobox({
                            url: '../Setting/BasicInfoData.aspx?module=Lc&action=readPart',
                            valueField: 'ID',
                            textField: 'Lc'
                        }).combobox('clear');
                    }
                    else {
                        $("#Lc").combobox({
                            url: '../Setting/BasicInfoData.aspx?module=Lc&action=read&ldID=' + ldname,
                            valueField: 'ID',
                            textField: 'Lc'
                        }).combobox('clear');

                    }
                    var row1 = $('#dg').datagrid('getSelected');
                    $.post("../Setting/get_kf.aspx?readcanyd=true&fjtype=" + row1.RoomType + "&begin=" + $('#sttime1').datebox('getValue') + "&end=" + $('#edtime1').datebox('getValue') + "&lcid=" + $('#Lc').datebox('getValue') + "&ldid=" + $('#Ld').datebox('getValue'), { cgyid: '0' }, function (result) {
                        if (!result.errorMsg) {
                            var data = $('#dg').datagrid('getRows');
                            for (var i = 0; i < data.length; i++) {
                                $('#dg').datagrid('selectRow', i).datagrid('beginEdit', i);
                                var ed = $('#dg').datagrid('getEditor', { index: i, field: 'JB' });
                                var rows = [];
                                for (var j = 0; j < result.length; j++) {
                                    if (result[j].JBName == data[i].RoomType) {
                                        rows.push(result[j]);
                                    }
                                }
                                $("#fhtable").datagrid("loadData", rows); //将房间数据绑定

                            }
                        }
                    }, 'json');
                }
            });
            //根据楼层去查房间
            $("#Lc").combobox({
                onSelect: function () {
                    var row1 = $('#dg').datagrid('getSelected');
                    $.post("../Setting/get_kf.aspx?readcanyd=true&fjtype=" + row1.RoomType + "&begin=" + $('#sttime1').datebox('getValue') + "&end=" + $('#edtime1').datebox('getValue') + "&lcid=" + $('#Lc').datebox('getValue') + "&ldid=" + $('#Ld').datebox('getValue'), { cgyid: '0' }, function (result) {
                        if (!result.errorMsg) {
                            var data = $('#dg').datagrid('getRows');
                            for (var i = 0; i < data.length; i++) {
                                $('#dg').datagrid('selectRow', i).datagrid('beginEdit', i);
                                var ed = $('#dg').datagrid('getEditor', { index: i, field: 'JB' });
                                var rows = [];
                                for (var j = 0; j < result.length; j++) {
                                    if (result[j].JBName == data[i].RoomType) {
                                        rows.push(result[j]);
                                    }
                                }

                                $("#fhtable").datagrid("loadData", rows); //将房间数据绑定
                            }
                        }
                    }, 'json');


                }
            });

            flag = false;
            $('#zkl').change(function () {
                var zhekou = $('#zkl').val() - 0;
                if (Number(hyZhekou) > 0) {//存在会员折扣
                    if (zhekou > Number(hyZhekou)) {
                        $('#zkl').val(hyZhekou);
                    }
                }
            });


            $("#MemberCardNo").change(function () {

                $.post('../Member/MemberChargeData.aspx?action=getJiFen', { MemberCardNo: $("#MemberCardNo").val() }, function (result) {

                    if (!result.errorMsg) {

                        hyZhekou = result.ZheKou
                        $('#zkl').attr("readonly", false);
                    } else {
                        hyZhekou = '0';
                        // $("#JiFen").val('');
                        $.messager.show({
                            title: 'Error',
                            msg: '没有该会员卡'
                        });

                    }
                }, 'json');
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
            //国籍，默认中国
            $("#gj").combobox({
                url: '../Setting/BasicInfoData.aspx?module=gj&action=read',
                valueField: 'gj',
                textField: 'gj',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#gj').combobox('getData');
                    if (data.length > 0) {
                        $("#gj").combobox('select', data[1].gj);
                    }
                }
            });
            //客户类型，默认散客
            $("#LXCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=khlb&action=read',
                valueField: 'KHLB',
                textField: 'KHLB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#LXCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#LXCombo").combobox('select', data[0].KHLB);
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
            //预订方式
            $("#YdfsCombo").combobox({
                url: '../Setting/BasicInfoData.aspx?module=ydway&action=read',
                valueField: 'Way',
                textField: 'Way',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#YdfsCombo').combobox('getData');
                    if (data.length > 0) {
                        $("#YdfsCombo").combobox('select', data[0].Way);
                    }
                }
            });
        });
        $.fn.datebox.defaults.formatter = function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + m + '-' + d;
        }

        function SaveForm() {
            var ids = [];
            var rows = $('#tt').datagrid('getRows');
            if (rows.length == 0) {
                alert("预订房间不能为空！");
                return false;
            }
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].KFJB + '-' + rows[i].SL);
            }

            $('#frm').form('submit', {
                url: 'FangJianYuDingData.aspx?action=CreateOrders&lists=' + ids.join(':'),
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
                        window.opener.location.reload();
                        window.close();
                    }
                }
            });
        }
        function ClearForm() { this.close(); }

        function SaveAllItem() {
            var row = $('#gdroder').datagrid('getSelected');
            if (!row) {
                alert("请先选择预定日期");
                return;
            }
             
            accept2();
            
            var rzstr = '';

            var data = $('#dg').datagrid('getRows'); //预定日期

          
            var checkflag = 0;
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    //                   
//                    var ed = $('#dg').datagrid('getEditor', { index: i, field: 'ZhuCong' });
                   if(data[i].ZhuCong=='是'){
                    checkflag++;
                                    }
                
                    if ((data[i].ShiJiFangJia + 0) > 0) {

                        rzstr = rzstr + '{"Customer":"' + data[i].CustomerName + '","f_dm":"' + data[i].JB + '","ShiJiFangJia":"' + data[i].ShiJiFangJia + '","ZhuCong":"' + data[i].ZhuCong + '","AutoID":"' + data[i].AutoID + '","JB":"' + data[i].RoomType + '"};';
                    }
                }
                rzstr = rzstr.substring(0, rzstr.length - 1); //去掉最后的;
                if (rzstr == '') {
                    return;
                }
                if(checkflag>1){
                    alert('只能选择一间主房间');
                    return;
                }
                $.post("FangJianYuDingData.aspx?action=SaveAllItem&ydid=" + ydid + "&ydnum=" + ydnum + "&ydOnBoardTime=" + myformatter(data[0].OnBoardTime), { fjlist: rzstr }, function (result) {

                    if (!result.errorMsg) {
                        alert("房间信息保存成功")
                        window.opener.location.reload();
                    } else {

                        $.messager.show({
                            title: 'Error',
                            msg: '没有该会员卡'
                        });

                    }
                }, 'json');
            }
        }
        var ydnum = "";
        var ydid = "";
        var flag = false;
        function AddFjOrder() {
            var row = $('#gdroder').datagrid('getSelected');
            if (row) {
                $('#dlgfj').dialog('open').dialog('setTitle', '新增');
                $('#frmfj').form('clear');
                url = 'FangJianYuDingData.aspx?action=FjCreate';
                $('#edtime').datebox('setValue', myformatter(row.LeaveTime));
                $('#sttime').datebox('setValue', myformatter(row.OnBoardTime));
                ydnum = row.YDNum;
                getKfInfo(myformatter(row.OnBoardTime), myformatter(row.LeaveTime));
            } else {
                alert("请先选择预定日期");
            }
        }
        //预订单收款
        var shoukuantype = 1; //1表示收款，-1表示退款
        function yufushoukuan() {
            shoukuantype = 1;
            $('#yudingjinguanli').dialog('open').dialog('setTitle', '预订单新增');
        }
          function yufutuikuan() {
              shoukuantype = -1;
              $('#yudingjinguanli').dialog('open').dialog('setTitle', '预订单新增');
        }
        function saveydzhangkuan() {
            if (ydid == '') {
                alert("请先保存预定客户信息");
            }

            $('#ydskfm').form('submit', {
                url: 'FangJianYuDingData.aspx?action=ydshoukuan&ydid=' + ydid + "&shoukuantype=" + shoukuantype,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {

                        alert("保存收款单信息成功");
                        //保存成功打印
                        $('#yudingjinguanli').dialog('close');
                        initYfdingjin();

                    }
                }
            });
        }

        function AddOrder() {
            if (flag) {
                $('#dlg').dialog('open').dialog('setTitle', '新增');
                url = 'FangJianYuDingData.aspx?action=AddOrder';
            } else {
                alert("请先保存预定客户信息");
            }
        }

        function saveKeRenOrder() {
            $('#frm').form('submit', {
                url: 'FangJianYuDingData.aspx?action=YdCustomerInfoCreate',
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        ydid = result;
                        flag = true;
                        alert("保存客户信息成功");

                    }
                }
            });

        }
        function saveOrder() {
            var begintime = myparser2($('#StartTime').datebox('getValue')); //到时
            var endtime = myparser2($('#EndTime').datebox('getValue')); //离时
            var data = $('#gdroder').datagrid('getRows');
            if (data!=null&&data.length > 0) {
                for (var i = 0; i < data.length; i++) {                     
                    if (begintime == myformatter(data[i].OnBoardTime) && endtime == myformatter(data[i].LeaveTime)) {
                        alert("已经存在相同时段的订单");
                        return;
                    }
                }
            }
            $('#fm').form('submit', {
                url: url + "&ydid=" + ydid,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#dlg').dialog('close');
                        loadYdData();

                    }
                }
            });
        }

        function saveFjOrder() {
            accept();
            if (ydnum == '' || ydid == '') {
                return;
            }
            //预定房间信息          
            var rzstr = '';
            var data = $('#fjtable').datagrid('getRows');
            if (data.length > 0) {
                for (var i = 0; i < data.length; i++) {

                    if ((data[i].UsedSL + 0) > 0) {//预订数量不为0
                        //判断是否预订数量有误 ,如果有误 则显示最大能预定的数量
                        var num = data[i].SL - data[i].OccupySL;
                        if (data[i].UsedSL > num) {
                            data[i].UsedSL = num;
                        }
                        rzstr = rzstr + '{"FJType":"' + data[i].FJType + '","SL":"' + data[i].SL + '","BiaoZhunFangJia":"' + data[i].BiaoZhunFangJia + '","OccupySL":"' + data[i].OccupySL + '","UsedSL":"' + data[i].UsedSL + '"};';
                    }
                }
                rzstr = rzstr.substring(0, rzstr.length - 1); //去掉最后的;
                if (rzstr == '') {
                    return;
                }

                $.post(url + "&ydid=" + ydid + "&ydnum=" + ydnum, { fjlist: rzstr }, function (result) {

                    if (!result.errorMsg) {
                        $('#dlgfj').dialog('close');
                        loadYdData();
                      
                        ydid = result.Success;

                    } else {

                        $.messager.show({
                            title: 'Error',
                            msg: '没有该会员卡'
                        });

                    }
                }, 'json');
            }
        }

        ////房间类型选择 
        var editIndex = undefined; //订单表格index
        var editIndex3 = undefined; //详细订单表格index
        function endEditing() {
            return true;
        }
        function endEditing2() {
            return true;
         
        }


        function onClickRow(index) {

            if (editIndex != undefined && editIndex != index) {
                $.messager.confirm('确认', '确定不保存离开?', function (r) {
                    if (r) {
                        getRowContent(index);
                        return;
                    }
                });
            } else if (editIndex == undefined) {

                getRowContent(index);
            }


        }
        function getRowContent(index) {

            editIndex = index;
            var selected = $("#gdroder").datagrid("getSelected");
            $("#dg").datagrid("loadData", selected["RecordList"]);           
            //获取所有的房间类型，房间号
            if ($('#dg').datagrid('getRows').length <= 0) {
                return;
            }
 
        }



        function onClickRow2(index) {
            $('#fjtable').datagrid('selectRow', index)
                            .datagrid('beginEdit', index);
            //            if (editIndex != index) {
            //                if (endEditing()) {
            //                    $('#fjtable').datagrid('selectRow', index)
            //                            .datagrid('beginEdit', index);
            //                    editIndex = index;
            //                } else {
            //                    $('#fjtable').datagrid('selectRow', editIndex);
            //                }
            //            }
        }
        //选择要预订的房间
        function saveFj() {

            var kfs = $('#fhtable').datagrid('getSelected');
           
            if (kfs.FH != null && kfs.FH!='') {
                var index1 = $('#dg').datagrid('getRowIndex', $('#dg').datagrid('getSelected'));
                 
                var ed = $('#dg').datagrid('getEditor', { index: index1, field: 'JB' });
               
                ed.target.val(kfs.FH);
            }
            
            $('#FHdg').dialog('close');
        }
        function removeit() {
            //if (editIndex == undefined) { return }
            $.messager.confirm('Confirm', '确定要删除?', function (r) {
                if (r) {
                    //根据订单号删除数据库里的订单记录
                    var row = $('#gdroder').datagrid('getSelected');

                    if (row) {//这里地址加一下，然后数据库里删一下，注意如果有详细的房间信息也要删掉
                        $.post("FangJianYuDingData.aspx?action=DelOrder", { ydnum: row.YDNum }, function (result) {
                            if (!result.errorMsg) {
                                alert("删除预订单成功");
                                $('#gdroder').datagrid('cancelEdit', editIndex)
                    .datagrid('deleteRow', editIndex);
                                accept2();
                                $("#dg").datagrid("clear");
                              
                            } else {
                                $.messager.show({
                                    title: 'Error',
                                    msg: '删除失败'
                                });

                            }
                        }, 'json');
                    }
                }
            });
        }

        //删除预订房间详情
        function delOrder() {
            //if (editIndex == undefined) { return }
            $.messager.confirm('Confirm', '确定要删除?', function (r) {
                if (r) {
                    //根据id删除数据库里的订单房间记录
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {

                        $.post("FangJianYuDingData.aspx?action=DelOrderFJ", { AutoID: row.AutoID }, function (result) {
                            if (!result.errorMsg) {
                                alert("删除成功");
                                $('#dg').datagrid('cancelEdit', editIndex3)
                    .datagrid('deleteRow', editIndex3);
                                //editIndex = undefined;
                                // $('#dg').datagrid('reload');
                                $('#dg').datagrid('selectRow', -1);
                                //accept2();
                            } else {
                                $.messager.show({
                                    title: 'Error',
                                    msg: '删除失败'
                                });
                            }
                        }, 'json');
                    }
                }
            });

        }

        //预定房间详细
        function onClickRow2(index) {
            $('#fjtable').datagrid('selectRow', index)
                            .datagrid('beginEdit', index);
            //            if (editIndex != index) {
            //                if (endEditing()) {
            //                    $('#fjtable').datagrid('selectRow', index)
            //                            .datagrid('beginEdit', index);
            //                    editIndex = index;
            //                } else {
            //                    $('#fjtable').datagrid('selectRow', editIndex);
            //                }
            //            }
        }

        //预定房间详细
        function onClickRow3(index) {
            // accept2();
            if (index == -1) {return;}
            //if (editIndex3 != index) {
            if (endEditing2()) {
                $('#dg').datagrid('selectRow', index)
                            .datagrid('beginEdit', index);
                editIndex3 = index;
            } else {
            alert('error');
                $('#dg').datagrid('selectRow', editIndex3);
            }
            // }
        }
        function removeit3() {
            
            $('#dg').datagrid('cancelEdit', editIndex3)
                    .datagrid('deleteRow', editIndex3);
             
        }
        function accept2() {
           // if (endEditing2()) {
                $('#dg').datagrid('acceptChanges');
            //}
        }


        function accept() {
            if (endEditing()) {
                $('#fjtable').datagrid('acceptChanges');
            }
        }

        function reject() {
            $('#gdroder').datagrid('rejectChanges');
            editIndex = undefined;
        }
        function getChanges() {
            var rows = $('#gdroder').datagrid('getChanges');
            alert(rows.length + ' rows are changed!');
        }
        function validateGrid() {
            var $dg = $("#gdroder");
            var gridRows = $dg.datagrid("getRows");
            for (var i = 0; i < gridRows.length; i++) {
                if (!$dg.datagrid("validateRow", i))
                    return false;
            }
            return true;
        }
function formatOper(val,row,index){  
    return '<a href="#" onclick="editUser('+index+')">修改</a>';  
}  
function editUser(index){  
   $('#dg').datagrid('selectRow',index);// 关键在这里  
   var row1 = $('#dg').datagrid('getSelected');
   if (row1) {
       $.post("../Setting/get_kf.aspx?readcanyd=true&fjtype=" + row1.RoomType + "&begin=" + $('#sttime').datebox('getValue') + "&end=" + $('#edtime').datebox('getValue') + "&lcid=" + $('#Lc').datebox('getValue') + "&ldid=" + $('#Ld').datebox('getValue'), { cgyid: '0' }, function (result) {

           if (!result.errorMsg) {
               var rows = [];
               var data = $('#dg').datagrid('getRows');
               //for (var i = 0; i < data.length; i++) {

                   $('#dg').datagrid('selectRow', index)
                                            .datagrid('beginEdit', index);
                   for (var j = 0; j < result.length; j++) {
                       if (result[j].JBName == data[index].RoomType) {
                           rows.push(result[j]);
                           //rows = rows.slice(0, j).concat(rows.slice(j + 1, rows.length));                              
                       }
                   }
              // }
               $('#fhtable').datagrid('reload');
               $("#fhtable").datagrid("loadData", rows); //将房间数据绑定                    
               //$('#dg').datagrid('selectRow', -1);
           }
       }, 'json');

        $('#FHdg').dialog('open').dialog('setTitle', '选择预订房间号');
        $('#XuanFJfm').form('clear');
        url = 'FangJianYuDingData.aspx?action=FjCreate';
        $('#edtime1').datebox('setValue', myformatter(row1.LeaveTime));
        $('#sttime1').datebox('setValue', myformatter(row1.OnBoardTime));
        $("#FjJB").val(row1.RoomType);
                                      
                                     
    }  
}
        function printYd() {
            //提示是否打印
            $.messager.confirm('Confirm', '确定打印预订单?', function (r) {
                if (r) {
                    window.open("PrintYd.html", "print");
                }
            });
        }
        function getPrintData() {
            
            var rows = $("#dg").datagrid("getRows");
            return {
                ShougongDanhao: ydnum,
                data_2: $("#Yder").val(), //姓名 
                Kehuleixing: $("#LXCombo").datebox("getValue"),
                Guoji: $("#gj").datebox("getValue"),
                Huiyuanka: $("#MemberCardNo").val(),
                Xieyidanwei: $("#company").val(),
                Yudingren: $("#Yder").val(),
                Lianxidianhua: $("#YdTel").val(),

                startTime: $("#OnBoardTime").datebox("getValue"),
                endTime: $("#LeaveTime").datebox("getValue"),
                Fukuanfangshi: $("#FkfsCombo").datebox("getValue"),
                Yudingjin: $("#dj").val(),
                Caozuoyuan: $("#cz").val(),
                Hejishuliang: rows.length,
                Yudingdanwei: $("#dw").val(),
                Beizhu: $("#bz").val(),
                rows: rows
            };
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'center'" style="padding: 1px">
        <form id="frm" method="post">
        <!--客户信息-->
        <div class="easyui-panel" title="客户信息">
            <table style="padding: 10px">
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        客户名称
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="Customer" data-options="required:true" />
                    </td>
                    <td style="width: 80px;">
                        会员卡
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="MemberCardNo" name="MemberCardNo"
                            onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')"></input>
                    </td>
                    <td style="width: 80px;">
                        客户类型
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" name="lx" id="LXCombo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        国籍
                    </td>
                    <td>
                        <input class="easyui-combobox" name="gj" id="gj" />
                    </td>
                    <td>
                        抵达时间
                    </td>
                    <td>
                        <input class="easyui-datetimebox" id="OnBoardTime" name="OnBoardTime" data-options="showSeconds:false"
                            value="<%=ARRTime%>">
                    </td>
                    <td>
                        离店时间
                    </td>
                    <td>
                        <input class="easyui-datetimebox" id="LeaveTime"  name="LeaveTime" data-options="showSeconds:false"
                            value="<%=LEATime%>">
                    </td>
                </tr>
                <tr>
                    <td>
                        客户人数
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="rs"></input>
                    </td>
                    <td>
                        协议单位
                    </td>
                    <td>
                        <input class="easyui-combobox" id="company" name="company" data-options="valueField:'Name',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'"></input>
                    </td>
                    <td>
                        折扣
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" id="zkl" name="zkl" readonly></input>%
                    </td>
                </tr>
            </table>
        </div>
        <!--预定信息-->
        <div class="easyui-panel" title="预定人信息">
            <table style="padding: 0px">
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        预定人
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Yder" name="Yder" data-options="required:true"></input>
                    </td>
                    <td style="width: 80px;">
                        联系电话
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text"  id="YdTel"  name="YdTel" data-options="required:true"></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        预定单位
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="dw" name="dw" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        付款方式
                    </td>
                    <td style="width: 160px;">
                     <%--   <input class="easyui-combobox" name="fkfs" id="FkfsCombo" />--%>
                          <select class="easyui-combobox" name="fkfs" id="FkfsCombo" style="width: 140px">
                            <option value="现金">现金</option>
                           
                        </select>
                    </td>
                    <td style="width: 80px;">
                        预定金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="dj" name="dj" />元
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        预定日期
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="rq" value="<%=YDTime%>" />
                    </td>
                    
                </tr>
              
                <tr>
                    <td style="width: 80px;">
                        操作员
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text"  id="cz"  name="cz" value="<%=caozuoyuan%>" readonly />
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        主账户名称
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="MainZhang" />
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        销售员
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" name="saler" id="SalersCombo" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        预订方式
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-combobox" name="ydway" id="YdfsCombo" />
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        备注
                    </td>
                    <td colspan="3">
                        <input class="easyui-validatebox" type="text"  id="bz" name="bz" size="60" />
                    </td>
                </tr>
            </table>
        </div>
        <!--中间Grid-->
        <div style="margin: 0px 0">
            <table style="padding: 0px 0px 0px 0px; height: 30px;">
                <tr>
                    <td style="width: 120px; height: 30px;">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="saveKeRenOrder()">客户信息保存</a>
                    </td>
                    <td style="width: 100px; height: 30px;">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="printYd()">打印预订单</a>
                    </td>
                    <td style="width: 150px; height: 30px" align="left">
                        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="yufushoukuan()">
                    预付金收款</a>
                    </td>
                    <td style="width: 150px; height: 30px" align="left">
                       <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="yufutuikuan()">
                    预付金退款</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>

                  
            </table>
        </div>
        <%--  <table id="tt" style="padding: 1px">
        </table>--%>
        </form>
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
            closed="true" buttons="#dlgbuttons">
            <form id="fm" method="post">
            <table>
                <tr>
                    <td>
                        预到日期：
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="StartTime" id="StartTime" value="<%=DateTime.Today.ToShortDateString()%>" />
                    </td>
                </tr>
                <tr>
                    <td>
                        预离日期：
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="EndTime" id="EndTime" value="<%=DateTime.Today.AddDays(1).ToShortDateString()%>" />
                    </td>
                </tr>
            </table>
            <div id="dlgbuttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveOrder()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlg').dialog('close')">取消</a>
            </div>
            </form>
        </div>
        <div id="dlgfj" class="easyui-dialog" style="width: 600px; height: 600px; padding: 10px 20px"
            closed="true" buttons="#dlgfjbtn">
            <table>
                <tr>
                    <td style="width: 60px">
                        预到日期：
                    </td>
                    <td>
                        <input class="easyui-datebox" name="sttime" id="sttime" data-options="formatter:myformatter,parser:myparser"
                            readonly />
                    </td>
                    <td style="margin-left: 20px; width: 60px">
                        预离日期：
                    </td>
                    <td>
                        <input class="easyui-datebox" name="edtime" id="edtime" data-options="formatter:myformatter,parser:myparser"
                            readonly />
                    </td>
                </tr>
            </table>
            <form id="frmfj" method="post">
            <table id="fjtable" class="easyui-datagrid" title="房间详情" rownumbers="true" style="height: 480px"
                data-options="iconCls: 'icon-ok',
                singleSelect: true,
                 onClickRow: onClickRow2,
                toolbar: '#tbfj'">
                <thead>
                    <tr>
                        <th data-options="field:'FJType',width:100,align:'center',editor:{options:{editable:false }} ">
                            房间类型
                        </th>
                        <th data-options="field:'SL',width:80,align:'center',editor:'text',editor:{options:{editable:false }} ">
                            房间总数
                        </th>
                        <th data-options="field:'BiaoZhunFangJia',width:80,align:'center',editor:'text',editor:{options:{editable:false }} ">
                            标准房价
                        </th>
                        <th data-options="field:'OccupySL',width:80,align:'center',editor:'text' ,editor:{options:{editable:false }} ">
                            已占用数
                        </th>
                        <th data-options="field:'UsedSL',width:80,align:'center',editor:{type:'numberbox',options:{precision:0,required:true,editable:true }} ">
                            预定数
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="tbfj" style="height: auto">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
                    onclick="AddFJOrder()" id="A3">添加</a>
            </div>
            </form>
            <div id="dlgfjbtn">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveFjOrder()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlgfj').dialog('close')">取消</a>
            </div>
        </div>
        <!--预订单列表-->
        <table id="gdroder" class="easyui-datagrid" title="预定单" rownumbers="true" style="height: 200px"
            data-options="iconCls: 'icon-ok',
                singleSelect: true,
                toolbar: '#tborder',
                onClickRow: onClickRow">
            <thead>
                <tr>
                    <th data-options="field:'YDNum',width:150,align:'center' ">
                        订单号
                    </th>
                    <th data-options="field:'OnBoardTime',width:120,align:'center',showSeconds:true,editor:'text',formatter:myformatter,parser:myparser ">
                        预到时间
                    </th>
                    <th data-options="field:'LeaveTime',width:120,align:'center',showSeconds:true,editor:'text',formatter:myformatter,parser:myparser ">
                        预离时间
                    </th>
                </tr>
            </thead>
        </table>
        <!--预定单详情-->
        <table id="dg" class="easyui-datagrid" title="预定单详情" rownumbers="true" style="height: 200px"
            data-options="iconCls: 'icon-ok',
                singleSelect: true,
              
                 onClickRow: onClickRow3,
                toolbar: '#tb'">
            <thead>
                <tr>
                    <th data-options="field:'AutoID',width:50,align:'center'">
                        ID
                    </th>
                    <th data-options="field:'CustomerName',width:80,align:'center',editor:{type:'validatebox',options:{required:true,editable:true }} ">
                        客人姓名
                    </th>
                    <th data-options="field:'RoomType',width:80,align:'center',editor:'text',editor:{options:{editable:false }} ">
                        房间类型
                    </th>
                    <%--   
                <th data-options="field:'JB',width:80,align:'center',editor:{type:'validatebox',options:{required:true,editable:true }} ">--%>
                    <%--   formatter:function(value,row){
                            return row.FH;
                        },--%>
                    <th data-options="field:'JB',width:150,            
                        editor:{
                            type:'text',
                            options:{
                            
                                editable: false,                             
                                width:150,
                             
                                required:true
                            }
                        },align:'center'">
                        房间号
                    </th>
                       <th data-options="width:80,align:'center',formatter:formatOper">选择房号</th>
                    <th data-options="field:'ZhuCong',width:80,align:'center',editor:{type:'checkbox',options:{on:'是',off:'否'}} ">
                        主
                    </th>
                    <th data-options="field:'BiaoZhunFangJia',width:80,align:'center' ">
                        标准房价
                    </th>
                    <th data-options="field:'ShiJiFangJia',width:80,align:'center',editor:{type:'numberbox',options:{precision:2,required:true,editable:true }} ">
                        实际房价
                    </th>
                    <th data-options="field:'OnBoardTime',showSeconds:true,width:100,editor:'text',align:'center',formatter:myformatter,parser:myparser,editor:{options:{editable:false }} ">
                        预到日期
                    </th>
                    <th data-options="field:'LeaveTime',showSeconds:true,width:100,editor:'text',align:'center' ,formatter:myformatter,parser:myparser,editor:{options:{editable:false }}  ">
                        预离日期
                    </th>
                </tr>
            </thead>
        </table>
        <div id="tb" style="height: auto">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
                onclick="AddFjOrder()" id="A1">增加</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    data-options="iconCls:'icon-remove',plain:true" onclick="delOrder()" id="A2">删除</a>
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true"
                onclick="SaveAllItem()" id="A4">保存</a>
        </div>
        <div id="tborder" style="height: auto">
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
                onclick="AddOrder()" id="pageprev">增加</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    data-options="iconCls:'icon-remove',plain:true" onclick="removeit()" id="pagenext">
                    删除</a>
        </div>
        <!--选择预订房间号-->
        <div id="FHdg" class="easyui-dialog" style="width: 650px; height: 500px; padding: 10px 20px"
            closed="true" buttons="#btnFH">
            <table>
                <tr>
                    <td style="width: 60px">
                        预到日期：
                    </td>
                    <td>
                        <input class="easyui-datebox" name="sttime" id="sttime1" data-options="formatter:myformatter,parser:myparser"
                            readonly />
                    </td>
                    <td style="margin-left: 20px; width: 60px">
                        预离日期：
                    </td>
                    <td>
                        <input class="easyui-datebox" name="edtime" id="edtime1" data-options="formatter:myformatter,parser:myparser"
                            readonly />
                    </td>
                    <td style="margin-left: 20px; width: 60px">
                        房间类型：
                    </td>
                    <td>
                        <input class="easyui-validatebox" id="FjJB" type="text" id="FjJB" style="width: 100px"
                            name="fangjianhao" readonly />
                    </td>
                </tr>
            </table>
            <hr />
            <div style="width: 160px; float: left">
                <table>
                    <tr>
                        <td align="left" style="width: 55px">
                            楼栋
                        </td>
                        <td width="80px">
                            <input class="easyui-combobox" name="Ld" id="Ld" type="text" style="width: 120px"
                                runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 55px">
                            楼层
                        </td>
                        <td width="80px">
                            <input class="easyui-combobox" id="Lc" type="text" style="width: 120px" name="Lc"
                                runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 400px; float: right;">
                <form id="XuanFJfm" method="post">
                <%--     <table id="fhtable" class="easyui-datagrid" title="选择预订房间号" rownumbers="true" style="height: 200px" checkbox="true" >--%>
                <table class="easyui-datagrid" id="fhtable" style="padding: 0px; height: 340px; width: 350px">
                    <thead>
                        <tr>
                            <th data-options="field:'ck',checkbox:true">
                            </th>
                            <th data-options="field:'FH',width:50">
                                房号
                            </th>
                            <th data-options="field:'JBName',width:100">
                                房间类型
                            </th>
                            <th data-options="field:'DJ',width:80">
                                标准房价
                            </th>
                            <th data-options="field:'StatusName',width:60">
                                状态
                            </th>
                        </tr>
                    </thead>
                </table>
                <%--  </table>--%>
                </form>
            </div>
            <div id="btnFH">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveFj()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#FHdg').dialog('close')">取消</a>
            </div>
        </div>


         <div   class="easyui-dialog" id="yudingjinguanli" style="width: 650px; height: 500px; padding: 10px 20px"
            closed="true"  >

         <div class="easyui-tabs"         >
              <div title="新增&收款" style="padding: 0px">
               <form id="ydskfm" method="post">
                 <table>
                    <thead>
                        <tr>
                            <td>
                             <label style="width:90px">
                       付款方式：</label></td><td>
                    <select class="easyui-combobox" name="ydfkfs" id="ydfkfs" style="width:130px" >
                            <option value="现金">现金</option>
                           
                        </select>
                            </td>
                            <td>
                             <label style="width:90px">
                       金额：</label></td><td>
                         <input name="ydjine" id="ydjine" class="easyui-validatebox" required="true">
                            </td>
                        </tr>

                         <tr>
                            <td>
                             <label>
                       发生时间：</label></td><td>
                    <input class="easyui-datetimebox" name="ydfstime" id="ydfstime" required="true"
                              />
                            </td>
                            <td>
                             <label>
                       单据号：</label></td><td>
                         <input name="yuddanjuhao" id="yuddanjuhao" class="easyui-validatebox" required="true"/>
                            </td>
                        </tr>

                        <tr>
                           <td >                             
                       备注： 
                        </td>
                        
                             <td >
                      
                            <input name="ydbeizhu" id="ydbeizhu" class="easyui-validatebox"   >
                           </td>
                            <td >                             
                       
                        </td>
                         <td >                             
                      
                        </td>
                        </tr>

                          <tr>
                            <td>
                             <label>
                       相关名称：</label></td><td>
                     <input name="ydrelatedname" id="ydrelatedname" class="easyui-validatebox" >
                            </td>
                            <td>
                             <label>
                       预收账款：</label></td><td>
                         <input name="ydzhangkuan" id="ydzhangkuan" class="easyui-validatebox" required="true">
                            </td>
                        </tr>
                    </thead>
                </table>
                  
             </form>
              <div id="kfdlg-buttons">
                    <a href="javascript:void(0)" class="easyui-linkbutton"
                data-options="iconCls:'icon-print',plain:true" disabled="disabled" onclick="printOrder()" id="btnprint">打印</a>
   
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveydzhangkuan()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#kfdlg').dialog('close')">取消</a>
            </div>
          </div>  
        
              <div title="全部收款纪录" style="padding: 0px">
            <form id="FTZDS" method="post">
            <table id="yufujinjilu" title="全部收款纪录" class="easyui-datagrid" style="padding: 0px" ,
                toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'Fkfs',width:100">
                            支付方式
                        </th>
                        <th data-options="field:'jine',width:100">
                            金额
                        </th>
                        <th data-options="field:'time',width:100,showSeconds:true,editor:'text',formatter:myformatter,parser:myparser">
                            发生时间
                        </th>
                        <th data-options="field:'danjuhao',width:100">
                            单据号
                        </th>
                        <th data-options="field:'beizhu',width:100">
                            备注
                        </th>
                         <th data-options="field:'type',width:100">
                            收款单类型
                        </th>
                    </tr>
                </thead>
            </table>
            </form>
            <div id="toolbar">
              <%-- <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="javascript:$('#kfdlg').dialog('close')">
                    确定</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#kfdlg').dialog('close')">取消</a>--%>
            </div>
           
           
          
        </div>  
     
       </div></div>
     
    </div>
</body>
</html>
