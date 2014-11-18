<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeFangXinXi.aspx.cs" Inherits="Setting_KeFangXinXi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客房信息</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>    

    <link rel="stylesheet" type="text/css" href="../css/jquery.bigcolorpicker.css" />
    <script type="text/javascript" src="../js/jquery.bigcolorpicker.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#Color").bigColorpicker("Color");
            //$("#btn").bigColorpicker("c2");
            $("#img").bigColorpicker(function (el, color) {
                $(el).css("background-color", color);
            });
            $(".ku").bigColorpicker(function (el, color) {
                $(el).css("background-color", color);
            });
            $("#c5").bigColorpicker("c5", "L", 3);
        });
</script>
    <style type="text/css">
        .demo{width:400px; margin:20px auto}
.demo p{padding-bottom:10px}
#img{width:25px;height:25px;display:block; border:1px solid #d3d3d3;}
.ku{width:25px;height:25px;display:block; border:1px solid #d3d3d3; float:left; margin-right:10px}
.clear{clear:both}

        #fm
        {
            margin: 0;
            padding: 10px 30px;
        }
        #kffm
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
</head>
<body class="easyui-layout">
    <!--客房-->
    <div class="easyui-tabs" style="width: 1200px; height:670px; padding: 2px">
        <div title="房间类别" style="padding: 0px">
            <form id="FTZDS" method="post">
            <table id="dg" title="房间分类" class="easyui-datagrid" style="padding: 0px" url="get_kfcgy.aspx"
                toolbar="#toolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'KFJB',width:100">
                            房间分类
                        </th>
                        <th data-options="field:'CW',width:100">
                            床位
                        </th>
                        <th data-options="field:'DJ',width:100">
                            单价
                        </th>
                        <th data-options="field:'DDF',width:100">
                            钟点房
                        </th>
                        <th data-options="field:'LCF',width:100">
                            凌晨房
                        </th>
                    </tr>
                </thead>
            </table>
            </form>
            <div id="toolbar">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                    onclick="newItem()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        iconcls="icon-edit" plain="true" onclick="editItem()">编辑</a> <a href="javascript:void(0)"
                            class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyItem()">
                            删除</a>
            </div>
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                      钟点房时间段： <input class="easyui-TimeSpinner
" name="zdf_stime" id="zdf_stime" data-options="showSeconds:true"  value="00:00:00" />- <input class="easyui-TimeSpinner
" name="zdf_etime" id="zdf_etime" data-options="showSeconds:true" value="23:59:59" />
                         
                           <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ZDTime()"
                            id="InsertZdTime">设置钟点房时间</a>
                    </td>
                    <td style="width: 130px;" align="right">
                  <%--  <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-search" onclick="ZDChaxun()"
                            id="A1">钟点房设置</a>--%>
                    </td>
                    <td style="width: 80px;" align="right">
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
            <div id="dlg" class="easyui-dialog" style="width: 400px; height: 280px; padding: 10px 20px"
                closed="true" buttons="#dlg-buttons">
                <div class="ftitle">
                    房间类型</div>
                <form id="fm" method="post" novalidate>
                <div class="fitem">
                    <label>
                        类型名称:</label>
                    <input name="KFJB" class="easyui-validatebox" required="true" id="KFJB">
                </div>
                <div class="fitem">
                    <label>
                        单价:</label>
                    <input name="DJ" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        床位:</label>
                    <input name="CW" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        钟点房价:</label>
                    <input name="DDF" class="easyui-validatebox" readonly="true" id="DDF">
                          <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                        onclick="newZd()">...</a>
                </div>
                <div class="fitem">
                    <label>
                        凌晨房价:</label>
                    <input name="LCF" class="easyui-validatebox">
                </div>
                </form>
            </div>
            <div id="dlg-buttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlg').dialog('close')">取消</a>
            </div>
            <script type="text/javascript">
                function newZd() {
                    var t = $("#KFJB").val();
                    if (t) {
                        $('#ZD').dialog('open').dialog('setTitle', t + '钟点房房价方案');
                        $.get('ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                            $("#ZDdg").datagrid("loadData", result);
                        }, 'json');

                    }
                }
                function ZDTime() {
                    //var stime = $("#zdf_stime").datebox('getValue');
                    var stime = $('#zdf_stime').timespinner('getValue');

                    var etime = $("#zdf_etime").timespinner('getValue');

                    var stime2 = "2000-01-01 " + $('#zdf_stime').timespinner('getValue');
                    var etime2 = "2000-01-01 " + $('#zdf_etime').timespinner('getValue');
                    var sDt = new Date(Date.parse(stime2.replace(/-/g, "/")));
                    var eDt = new Date(Date.parse(etime2.replace(/-/g, "/")));
                    if (sDt == 'Invalid Date' || eDt == 'Invalid Date' || sDt > eDt) {

                        alert('时间设置错误，请重新设置时间');
                        return;
                    }

                    //判断是否是正确的时间
                    var stimeH = stime.trim().split(':')[0] - 0;

                    var stimeM = stime.trim().split(':')[1] - 0;
                    var stimeS = stime.trim().split(':')[2] - 0;

                    var etimeH = etime.trim().split(':')[0] - 0;
                    var etimeM = etime.trim().split(':')[1] - 0;
                    var etimeS = etime.trim().split(':')[2] - 0;

                    if ((stimeH < 0 || stimeH > 24) || (etimeH < 0 || etimeH > 24)) {
                        alert('时间设置错误，请重新设置时间');
                        return;
                    }
                    if ((stimeM < 0 || stimeM > 59) || (etimeM < 0 || etimeM > 59)) {
                        alert('时间设置错误，请重新设置时间');
                        return;
                    }
                    if ((stimeS < 0 || stimeS > 59) || (etimeS < 0 || etimeS > 59)) {
                        alert('时间设置错误，请重新设置时间');
                        return;
                    }


                    if (sDt > eDt) {
                        alert('时间设置错误，请重新设置时间');
                        return;
                    }
                    $.messager.confirm('Confirm', '确认设置钟点房时间吗?', function (r) {
                        if (r) {
                            $.post('update_kfcgyTime.aspx', { zdf_stime: stime, zdf_etime: etime }, function (result) {
                                if (result.Success) {
                                    $('#dg').datagrid('reload');
                                } else {
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.errorMsg
                                    });
                                }
                            }, 'json');
                        }
                    });

                }
                //                function ZDChaxun() {
                //                    var data = $('#dg').datagrid('getRows');
                //                    var row = $('#dg').datagrid('getSelected');
                //                    if (row) {
                //                        $('#ZD').dialog('open').dialog('setTitle', row.KFJB + '钟点房房价方案');
                //                        $.get('ZD_selectData.aspx', { action: 'read', id: row.KFJB }, function (result) {
                //                            $("#ZDdg").datagrid("loadData", result);
                //                        }, 'json');
                //                    }
                //                }

                var urll;
                function newItem() {
                    $('#dlg').dialog('open').dialog('setTitle', '新增客房分类');
                    $('#fm').form('clear');
                    urll = 'save_kfcgy.aspx';
                }
                function editItem() {
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {
                        $('#dlg').dialog('open').dialog('setTitle', '编辑客房分类');
                        $('#fm').form('load', row);
                        urll = 'update_kfcgy.aspx?id=' + row.ID;
                    }
                }
                function saveItem() {
                    $('#fm').form('submit', {
                        url: urll,
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
                                $('#dlg').dialog('close');        // close the dialog
                                $('#dg').datagrid('reload');    // reload the user data
                            }
                        }
                    });
                }
                function destroyItem() {
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {
                        $.messager.confirm('Confirm', '确认删除?', function (r) {
                            if (r) {
                                $.post('destroy_kfcgy.aspx', { id: row.ID }, function (result) {
                                    if (result.success) {
                                        $('#dg').datagrid('reload');
                                        // reload the user data
                                    } else {
                                        $.messager.show({    // show error message
                                            title: 'Error',
                                            msg: result.errorMsg
                                        });
                                    }
                                }, 'json');
                            }
                        });
                    }
                }
            </script>
            <%--      </form>--%>
        </div>
        
        <div title="添加房号" style="padding: 0px">
            <table id="kfdg" title="客房信息" class="easyui-datagrid" style="padding: 0px" url="get_kf.aspx"
                toolbar="#kftoolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'FH',width:100">
                            房号
                        </th>
                        <!--<th data-options="field:'StatusCode',width:100">
                            状态代码
                        </th>-->
                        <th data-options="field:'StatusName',width:100">
                            状态名称
                        </th>
                        <!--<th data-options="field:'JBCode',width:100">
                            级别代码
                        </th>-->
                        <th data-options="field:'JBName',width:100">
                            房间级别
                        </th>
                        <th data-options="field:'CW',width:100">
                            床位
                        </th>
                        <th data-options="field:'DJ',width:100">
                            全天房价
                        </th>
                        <th data-options="field:'BJ',width:100">
                            半天房价
                        </th>
                        <th data-options="field:'Ld',width:100">
                            楼栋
                        </th>
                        <th data-options="field:'Lc',width:100">
                            楼层
                        </th>
                        <th data-options="field:'Detail',width:100">
                            说明
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="kftoolbar">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                    onclick="newKF()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                        plain="true" onclick="editKF()">编辑</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                            iconcls="icon-remove" plain="true" onclick="destroyKF()">删除</a>
            </div>
            <div id="kfdlg" class="easyui-dialog" style="width: 400px; height: 380px; padding: 10px 20px"
                closed="true" buttons="#kfdlg-buttons">
                <div class="ftitle">
                    房间类型</div>
                <form id="kffm" method="post" novalidate>
                <div class="fitem" " style=" width:380px">
                    <label>
                        楼栋：</label>
                    <input class="easyui-combobox" name="Ld" id="Ld" required="true" />
                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                        onclick="newLd()">...</a>
                        <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delLd()">
                        删除</a>
                </div>
                <div class="fitem" style=" width:380px">
                    <label>
                        楼层：</label>
                    <input class="easyui-combobox" name="Lc" id="Lc" required="true" />
                    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                        onclick="newLc()">...</a>
                         <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="delLc()">
                        删除</a>
                </div>
                <div class="fitem" "  >
                    <label>
                        房号:</label>
                    <input name="FH" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        状态名:</label>
                    <label name="StatusName" id="StatusName">
                        空房</label>     </div>
                <div class="fitem">
                    <label>
                        级别名称:</label>
                    <input class="easyui-combobox" name="JBCode" id="JBCode" data-options="valueField:'ID',textField:'KFJB',url:'../Common/getkfcgy.aspx'">
                </div>
                <div class="fitem">
                    <label>
                        全天房价:</label>
                    <input name="DJ" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        半天房价:</label>
                    <input name="BJ" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        床位:</label>
                    <input name="CW" class="easyui-validatebox">
                </div>
                <div class="fitem">
                    <label>
                        说明:</label>
                    <input name="Detail" class="easyui-validatebox">
                </div>
                </form>
            </div>
            <div id="kfdlg-buttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveKF()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#kfdlg').dialog('close')">取消</a>
            </div>
            <div id="LdInsert" class="easyui-dialog" style="width: 400px; height: 150px; padding: 10px 20px"
                closed="true" buttons="#ld-buttons">
                <form id="Ldfm" method="post" novalidate>
                <div class="fitem">
                    <label>
                        楼栋:</label>
                    <input name="Ld" id="Ld" class="easyui-validatebox">
                </div>
                </form>
            </div>
            <div id="ld-buttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveld()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#LdInsert').dialog('close')">取消</a>
            </div>
            <div id="LcInsert" class="easyui-dialog" style="width: 400px; height: 200px; padding: 10px 20px"
                closed="true" buttons="#lc-buttons">
                <form id="Lcfm" method="post" novalidate>
                <div class="fitem">
                    <label>
                        楼栋:</label>
                    <%--  <label id="ldname2"></label>--%>
                    <input name="ldname2" class="easyui-validatebox" id="ldname2" readonly="true">
                </div>
                <div class="fitem">
                    <label>
                        楼层:</label>
                    <input name="lc" class="easyui-validatebox" id="lc">
                </div>
                </form>
            </div>
            <div id="lc-buttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savelc()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#LcInsert').dialog('close')">取消</a>
            </div>
            <script type="text/javascript">
                var url;

                function delLc() {
                    var ldname1 = $('#Lc').datebox('getValue');
                    if (ldname1 == "") {
                        alert('请选择楼层!');
                    }
                    else {
                        $.messager.confirm('确认', '确定删除该楼层?', function (r) {
                            if (r) {
                                $.ajax({
                                    type: "post",
                                    url: "BasicInfoData.aspx?module=Lc&action=delete&ID=" + ldname1,
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
                                            alert('删除楼层成功!');
                                            $("#Lc").val('');
                                            $("#Lc").combobox('reload');
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
                        });

                    }
                }

                function delLd() {
                    var ldname1 = $('#Ld').datebox('getValue');
                    if (ldname1 == "") {
                        alert('请选择楼栋!');
                    }
                    else {
                        $.messager.confirm('确认', '确定删除该楼栋?', function (r) {
                            if (r) {
                                $.ajax({
                                    type: "post",
                                    url: "BasicInfoData.aspx?module=Ld&action=delete&ID=" + ldname1,
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
                                            alert('删除楼栋成功!');
                                            $("#Ld").combobox('reload');
                                            $("#Lc").combobox('reload');
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
                        });

                    }
                }

                function newLd() {
                    var ldname1 = $('#Ld').datebox('getText');
                    if (ldname1 == "") {
                        $('#LdInsert').dialog('open').dialog('setTitle', '新增楼栋');
                        $('#Ldfm').form('clear');
                        url = "BasicInfoData.aspx?module=Ld&action=create";
                    }
                    else {

                        $('#LdInsert').dialog('open').dialog('setTitle', '编辑楼栋');
                        $("input[id$='Ld']").val(ldname1);
                        url = "BasicInfoData.aspx?module=Ld&action=update&ID=" + $('#Ld').datebox('getValue');

                    }
                }
                function saveld() {
                    $('#Ldfm').form('submit', {
                        url: url,
                        onSubmit: function () {
                            return $(this).form('validate');
                        },
                        success: function (result) {
                            if (result.errorMsg) {
                                $.messager.show({
                                    title: 'Error',
                                    msg: result.errorMsg
                                });
                            } else {
                                $('#LdInsert').dialog('close');
                                $("#Ld").combobox('reload');
                            }
                        }
                    });
                }
                function newLc() {
                    var ldname1 = $('#Ld').datebox('getText');
                    var lcname1 = $('#Lc').datebox('getText');
                    if (lcname1 == "") {
                        $('#LcInsert').dialog('open').dialog('setTitle', '新增楼层');
                        $('#Lcfm').form('clear');
                        $("input[id$='ldname2']").val(ldname1);
                        url = "BasicInfoData.aspx?module=Lc&action=create&ID=" + $('#Ld').datebox('getValue');
                    }
                    else {
                        $('#LcInsert').dialog('open').dialog('setTitle', '编辑楼层');
                        $("input[id$='ldname2']").val(ldname1);
                        $("input[id$='lc']").val(lcname1);
                        url = "BasicInfoData.aspx?module=Lc&action=update&ID=" + $('#Lc').datebox('getValue') + "&ldID=" + $('#Ld').datebox('getValue');
                    }
                }
                function savelc() {

                    $('#Lcfm').form('submit', {
                        url: url,
                        onSubmit: function () {
                            return $(this).form('validate');
                        },
                        success: function (result) {
                            if (result.errorMsg) {
                                $.messager.show({
                                    title: 'Error',
                                    msg: result.errorMsg
                                });
                            } else {
                                $('#LcInsert').dialog('close');
                                $("#Lc").combobox('reload');
                            }
                        }
                    });
                }
                function newKF() {
                    $('#kfdlg').dialog('open').dialog('setTitle', '新增客房');
                    $('#kffm').form('clear');
                    url = 'save_kf.aspx?id=1';
                }
                function editKF() {
                    var row = $('#kfdg').datagrid('getSelected');
                    if (row) {
                        $('#kfdlg').dialog('open').dialog('setTitle', '编辑客房');
                        $('#kffm').form('load', row);
                        url = 'update_kf.aspx?id=' + row.ID;
                    }
                }
                function saveKF() {
                     
                    url = url+ "&JBName=" + $('#JBCode').datebox('getText');
                  
                    $('#kffm').form('submit', {
                        url: url,
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
                                $('#kfdlg').dialog('close');        // close the dialog
                                $('#kfdg').datagrid('reload');    // reload the user data
                            }
                        }
                    });
                }
                function destroyKF() {
                    var row = $('#kfdg').datagrid('getSelected');
                    if (row) {
                        $.messager.confirm('Confirm', '确认删除?', function (r) {
                            if (r) {
                                $.post('destroy_kf.aspx', { id: row.ID }, function (result) {
                                    if (result.success) {
                                        $('#kfdg').datagrid('reload');
                                        // reload the user data
                                    } else {
                                        $.messager.show({    // show error message
                                            title: 'Error',
                                            msg: result.errorMsg
                                        });
                                    }
                                }, 'json');
                            }
                        });
                    }
                }
            </script>
        </div>
        <div title="房态颜色" style="padding: 0px">
            <table id="ztdg" title="房间状态" class="easyui-datagrid" style="padding: 0px" url="KFZTData.aspx?action=read"
                toolbar="#zttoolbar" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'AutoID',width:100" hidden>
                            编号
                        </th>
                        <th data-options="field:'FjZt',width:100">
                            房间状态
                        </th>
                        <th data-options="field:'Color',width:100">
                            颜色
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="zttoolbar">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                    onclick="newZT()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                        plain="true" onclick="editZT()">编辑</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                            iconcls="icon-remove" plain="true" onclick="destroyZT()">删除</a>
            </div>
            <div id="ztdlg" class="easyui-dialog" style="width: 400px; height: 280px; padding: 10px 20px"
                closed="true" buttons="#ztdlg-buttons">
                <div class="ftitle">
                    房间状态</div>
                <form id="ztfm" method="post" novalidate>
                <input name="AutoID" class="easyui-validatebox" type="hidden">
                <div class="fitem">
                    <label>
                        状态:</label>
                    <input name="FjZt" class="easyui-validatebox" required="true">
                </div>
                <div class="fitem">
                    <label>
                        颜色:</label>
                        <input type="text" id="Color"  name="Color"/>
                  <%--  <input name="Color" class="easyui-validatebox" required="true">--%>
                </div>
                </form>
            </div>
            <div id="ztdlg-buttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveZT()">
                    保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#ztdlg').dialog('close')">取消</a>
            </div>
            <script type="text/javascript">
                var url;
                function newZT() {
                    $('#ztdlg').dialog('open').dialog('setTitle', '新增');
                    $('#ztfm').form('clear');
                    url = 'KFZTData.aspx?action=create';
                }
                function editZT() {
                    var row = $('#ztdg').datagrid('getSelected');
                    if (row) {
                        $('#ztdlg').dialog('open').dialog('setTitle', '编辑');
                        $('#ztfm').form('load', row);
                        url = 'KFZTData.aspx?action=update&AutoID=' + row.AutoID;
                    }
                }
                function saveZT() {
                    $('#ztfm').form('submit', {
                        url: url,
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
                                $('#ztdlg').dialog('close');        // close the dialog
                                $('#ztdg').datagrid('reload');    // reload the user data
                            }
                        }
                    });
                }
                function destroyZT() {
                    var row = $('#ztdg').datagrid('getSelected');
                    if (row) {
                        $.messager.confirm('Confirm', '确认删除?', function (r) {
                            if (r) {
                                $.post('KFZTData.aspx?action=delete', { AutoID: row.AutoID }, function (result) {
                                    if (result.success) {
                                        $('#ztdg').datagrid('reload');
                                        // reload the user data
                                    } else {
                                        $.messager.show({    // show error message
                                            title: 'Error',
                                            msg: result.errorMsg
                                        });
                                    }
                                }, 'json');
                            }
                        });
                    }
                }
            </script>
        </div>
    </div>
    <div title="钟点房方案">
        <div id="ZD" title="钟点房方案" class="easyui-dialog" style="width: 580px; height: 350px;"
            closed="true" buttons="#btn"  >
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
        <div id="ZDdlg" class="easyui-dialog" style="width: 300px; height: 350px;" closed="true"
            buttons="#ZDdlg-buttons">
            <div style="padding: 10px 0 10px 10px">
                <form id="ZDfm" method="post">
                <table>
                    <tr>
                        <td>
                            房间级别：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="f_jb" id="f_jb" readonly="true"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            方案名称：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="FangAnName" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            起步时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="StartLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            起步价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="StartFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加钟时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="AddLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            加钟价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="AddFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最小时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MinLen" />分钟
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最小价格：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MinFee" />元
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最长时长：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="MaxLen" />分钟
                        </td>
                    </tr>
                </table>
                <script type="text/javascript">
                </script>
                </form>
            </div>
        </div>
        <div id="ZDdlg-buttons">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMember()">
                保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                    onclick="javascript:$('#ZDdlg').dialog('close')">取消</a>
        </div>
        <div id="btn">
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" onclick="newMember()">
                增加</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                    onclick="editMember()">修改</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        iconcls="icon-edit" onclick="destroyMember()">删除</a>
        </div>
        <script type="text/javascript">
            var url;
            function newMember() {
                $('#ZDdlg').dialog('open').dialog('setTitle', '添加钟点房房价方案');
                $('#ZDfm').form('clear');
                $('#ZDfm').form('reset');
                url = 'ZD_selectData.aspx?action=create';
                var row = $('#dg').datagrid('getSelected');
                var t = $("#KFJB").val();
                $.get('ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                    $("input[id$='f_jb']").val(t);
                    $("#ZDdg").datagrid("loadData", result);
                }, 'json');

            }

            function editMember() {
                var row = $('#ZDdg').datagrid('getSelected');
                if (row) {
                    $('#ZDdlg').dialog('open').dialog('setTitle', '修改钟点房房价方案');
                    $('#ZDfm').form('load', row);
                    url = 'ZD_selectData.aspx?action=update&id=' + row.id;

                }
            }

            function saveMember() {
                $('#ZDfm').form('submit', {
                    url: url,
                    onSubmit: function () {
                        return $(this).form('validate');
                    },
                    success: function (result) {
                        if (result.errorMsg) {
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        } else {
                            $('#ZDdlg').dialog('close');
                            $('#ZDdg').datagrid('reload');
                            var row = $('#dg').datagrid('getSelected');
                            var t = $("#KFJB").val(); //row.KFJB
                            $.get('ZD_selectData.aspx', { action: 'read', id: t }, function (result) {
                                $("#ZDdg").datagrid("loadData", result);
                                var d = $("#ZDdg").datagrid('getData');
                                var fh = d.rows[0].StartFee;
                                $("#DDF").val(fh != "" ? fh : 0);
                                url = "";

                            }, 'json');
                        }
                    }
                });
            }

            function destroyMember() {
                var row = $('#ZDdg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm('Confirm', '确认删除?', function (r) {
                        if (r) {
                            $.post('ZD_selectData.aspx?action=delete', { id: row.id }, function (result) {
                                if (!result.errorMsg) {
                                    $('#ZDdg').datagrid('reload');
                                    //var data = $('#dg').datagrid('getRows');
                                    var row = $('#dg').datagrid('getSelected');
                                    var t = $("#KFJB").val();
                                    //if (data.length > 0) {

                                        $.get('ZD_selectData.aspx', { action: 'read', id:t }, function (result) {
                                            $("#ZDdg").datagrid("loadData", result);
                                            var d = $("#ZDdg").datagrid('getData');
                                            var fh = d.rows[0].StartFee;
                                            $("#DDF").val(fh != "" ? fh : 0);
                                        }, 'json');
                                  //  }

                                } else {
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.errorMsg
                                    });
                                }
                            }, 'json');
                        }
                    });
                }
            }

            function getmemebernolast() {
                $.ajax({
                    type: "get",
                    url: "ZD_selectData.aspx?action=getLastid",
                    success: function (result) {
                        var result = eval('(' + result + ')');
                        if (result.errorMsg) {
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
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
                $.ajax({
                    type: "post",
                    url: "update_kfcgyTime.aspx?action=readall",
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
                                $("#zdf_stime").val(result.zdf_stime);
                                $("#zdf_etime").val(result.zdf_etime);
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

                // 
                //楼栋
                $("#Ld").combobox({
                    url: 'BasicInfoData.aspx?module=Ld&action=read&type=notall',
                    valueField: 'ID',
                    textField: 'Ld',
                    editable: false,
                    onLoadSuccess: function () {
                        var data = $('#Ld').combobox('getData');
                        if (data.length > 0) {
                            $("#Ld").combobox('select', data[0].ld);
                        }
                    }
                });

                //楼层
                $('#Ld').combobox({
                    onSelect: function () {
                        var ldname = $('#Ld').datebox('getValue');
                        $("#Lc").combobox({
                            url: 'BasicInfoData.aspx?module=Lc&action=read&type=notall&ldID=' + ldname,
                            valueField: 'ID',
                            textField: 'Lc'
                        }).combobox('clear');
                    }
                });


            });
        </script>
    </div>
</body>
</html>
