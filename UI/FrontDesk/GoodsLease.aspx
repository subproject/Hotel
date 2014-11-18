<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsLease.aspx.cs" Inherits="FrontDesk_GoodsLease" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物品租借</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link href="../css/cashaction/pageformal.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script src="../../locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../src/jquery.json-2.2.min.js" type="text/javascript"></script>
    <script src="../Hotelmgr.js" type="text/javascript"></script>
    <style type="text/css">
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
    <div region="east" split="true" title="East" class="leftpage buttons">
        <table>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="positive" onclick="Insertwp_borrow()">
                        <img src="../css/cashaction/imag/apply2.png" alt="" />
                        新 增</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="positive" onclick="updatewp_borrow();">
                        <img src="../css/cashaction/imag/apply2.png" alt="" />
                        物品归还</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="positive" onclick="deletewp_borrow();">
                        <img src="../css/cashaction/imag/apply2.png" alt="" />
                        取 消</a>
                </td>
            </tr>
            <tr>
                <td>
                    <a href="javascript:void(0)" class="positive" onclick="ClearForm();">
                        <img src="../css/cashaction/imag/apply2.png" alt="" />
                        退 出</a>
                </td>
            </tr>
        </table>
    </div>
    <div region="center" border="true" title="" style="border-left: 0px; border-right: 0px;
        overflow: hidden;">
        <div id="contextid" title="" style="width: 100%; height: 600px; padding: 0px">
            <table id="dg" title="物品租借" class="easyui-datagrid" style="padding: 0px;
                height: 400px;" url="GoodsLeaseData.aspx?action=read_wp_borrow&orderguid=<%=OrderGuid %>"
                fitcolumns="true" buttons="#dlg-buttons" singleselect="true">
                <thead>
                    <tr>
                        <th data-options="field:'id_wp_borrow',width:50">
                            序号
                        </th>
                        <th data-options="field:'wpname',width:80">
                            物品名称
                        </th>
                        <th data-options="field:'num',width:50">
                            数量
                        </th>
                        <th data-options="field:'yajin',width:50">
                            押金
                        </th>
                        <th data-options="field:'zjtime',width:120">
                            租借时间
                        </th>
                        <th data-options="field:'fanhao',width:100">
                            房间号
                        </th>
                        <th data-options="field:'djnumber',width:100">
                            单据号码
                        </th>
                        <th data-options="field:'beizhu',width:150">
                            备注
                        </th>
                        <th data-options="field:'zjtime',width:140">
                            登记时间
                        </th>
                        <th data-options="field:'caozuoyuan',width:80">
                            操作员
                        </th>
                        <th data-options="field:'state',width:50">
                            状态
                        </th>
                    </tr>
                </thead>
            </table>
            <div id="tb" style="height: auto; padding: 5px" buttons="#dlg-buttons">
                <form id="FTZDS" method="post">
                <table>
                    <tr>
                        <td style="width: 70px; margin-right: 10px">
                            物品名称
                        </td>
                        <td style="width: 200px;">
                            <input class="easyui-combobox" id="wpnamecbo" name="wpname" data-options="valueField:'name',textField:'name',url:'GoodsLeaseData.aspx?action=read'" />
                            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                                onclick="newItem()">...</a>
                        </td>
                        <td style="width: 70px;">
                            数量
                        </td>
                        <td style="width: 70px;">
                            <input class="easyui-numberspinner" style="width: 60px;" required="required" data-options="min:1,max:100,editable:false"
                                id="num" name="num">
                        </td>
                        <td>
                            押金
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="yajin" id="yajin" style="width: 60px;" />
                        </td>
                        <td>
                            房间号
                        </td>
                        <td>
                            <input class="easyui-validatebox" readonly="readonly"  type="text" value="<%=fanhao %>"
                                name="fanjianhao" id="fanjianhao" style="width: 60px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            租借时间
                        </td>
                        <td>
                            <input class="easyui-datetimebox" style="width:170px;" name="zjtime" id="zjtime" value="<%=date %>" />
                        </td>
                        <td>
                            单据号码
                        </td>
                        <td colspan="5">
                            <input class="easyui-validatebox" type="text" name="djnumber" id="djnumber" style="width: 160px;
                                margin-bottom: 0px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td colspan="7">
                            <input class="easyui-validatebox" type="text" name="beizhu" id="beizhu" style="width: 440px" />
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <script type="text/javascript">
                var resizeTimeoutId = "";
            //#region 结帐界面初始化
                $(document).ready(function () {
                    //设置布局层的事件
                    ViewIntialization();
                    //设置布局层的事件
                    $('.easyui-layout').layout('panel', 'east').panel({
                        onResize: function (width, height) {
                            window.clearTimeout(resizeTimeoutId);
                            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
                        }
                    });
                });
                function ClearForm() {
                    this.window.close();
                }
                var url;
                function newItem() {
                    $('#ZD').dialog('open').dialog('setTitle', '添加租借物品');
                    $('#fm').form('clear');
                    $('#fm').form('reset');
                }
                function Insertwp_borrow() {
                    var a = $('#wpnamecbo').datebox('getValue');
                    url = 'GoodsLeaseData.aspx?action=create_wp_borrow&wpnamecbo= ' + a + '&orderguid=<%=OrderGuid %>';
                    savewp_borrow();

                }
                function updatewp_borrow() {
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {
                        if (row.state == "已还") {
                            alert("该物品已经归还");
                        }
                        else {
                            // url = 'GoodsLeaseData.aspx?action=update_wp_borrow&id_wp_borrow=' + row.id_wp_borrow;
                            // savewp_borrow();
                            $.post('GoodsLeaseData.aspx?action=update_wp_borrow' + '&orderguid=<%=OrderGuid %>', { id_wp_borrow: row.id_wp_borrow, wpname: row.wpname, num: row.num }, function (result) {
                                if (!result.errorMsg) {
                                    $('#dg').datagrid('reload');
                                    url = 'GoodsLeaseData.aspx?action=read';
                                    saveMember();
                                } else {
                                    $.messager.show({
                                        title: 'Error',
                                        msg: result.errorMsg
                                    });
                                }
                            }, 'json');
                        }

                    }
                }

                function deletewp_borrow() {
                    var row = $('#dg').datagrid('getSelected');
                    if (row) {
                        $.messager.confirm('Confirm', '确认删除?', function (r) {
                            if (r) {
                                $.post('GoodsLeaseData.aspx?action=delete_wp_borrow', { id_wp_borrow: row.id_wp_borrow, wpname: row.wpname, num: row.num }, function (result) {
                                    if (!result.errorMsg) {
                                        $('#dg').datagrid('reload');
                                        url = 'GoodsLeaseData.aspx?action=read';
                                        saveMember();
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
                function savewp_borrow() {//提交代码
                    $('#FTZDS').form('submit', {
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
                                $('#dg').datagrid('reload');
                                url = 'GoodsLeaseData.aspx?action=read';
                                saveMember();
                            }
                        }
                    });
                }
                //界面布局调整初始化
                function ViewIntialization() {
                    //设置费用明细的长度和宽度
                    heightvalue = $(".easyui-tabs").parent().height();
                    widthvalue = $(".easyui-tabs").parent().width();
                    $('.easyui-datagrid').attr('width', widthvalue);
                    $('#contextid').attr("width", widthvalue);
                    
                }
            </script>
        </div>
        <div id="ZD" title="添加租借物品" class="easyui-dialog" style="width: 450px; height: 260px;"
            closed="true" buttons="#btn">
            <form id="FRZD" method="post">
            <table id="ZDdg" class="easyui-datagrid" style="padding: 0px;" buttons="#btn" fitcolumns="true"
                singleselect="true" url="GoodsLeaseData.aspx?action=read">
                <thead>
                    <tr>
                        <th data-options="field:'id',width:60">
                            序号
                        </th>
                        <th data-options="field:'name',width:60">
                            物品名称
                        </th>
                        <th data-options="field:'count',width:60">
                            总数
                        </th>
                        <th data-options="field:'countleave',width:60">
                            库存剩余
                        </th>
                    </tr>
                </thead>
            </table>
            </form>
        </div>
        <div id="ZDdlg" class="easyui-dialog" style="width: 260px; height: 160px;" closed="true"
            buttons="#ZDdlg-buttons">
            <div style="padding: 10px 0 10px 10px">
                <form id="ZDfm" method="post">
                <table>
                    <tr>
                        <td>
                            物品名称
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="name" id="name" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            数量
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="count" id="count" />
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
                        iconcls="icon-edit" onclick="destroyMember()">删除</a> <a href="javascript:void(0)"
                            class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#ZD').dialog('close')">
                            退出</a>
        </div>
    </div>
    <script type="text/javascript">
        var url;

        //关闭初始打开的遮盖层
        ajaxLoadEnd();

        function newMember() {
            $('#ZDdlg').dialog('open').dialog('setTitle', '添加租借物品名称');
            $('#ZDfm').form('clear');
            $('#ZDfm').form('reset');
            url = 'GoodsLeaseData.aspx?action=create';
        }

        function editMember() {
            //   alert("获取数据");
            var row = $('#ZDdg').datagrid('getSelected');
            var data = $('#dg').datagrid('getRows');
            if (row) {
                // alert(row.name);
                var wp_name = row.name
                if (data) {
                    if (data != null && data.length > 0) {
                        for (var i = 0; i < data.length; i++) {
                            if (data[i].wpname == wp_name && data[i].state == "未还") {
                                // alert(data[i].wpname + data[i].state);
                                alert("该商品尚未全部归还，不能进行修改");
                                return;
                            }
                        }
                        $('#ZDdlg').dialog('open').dialog('setTitle', '修改租借物品名称');
                        $('#ZDfm').form('load', row);
                        url = 'GoodsLeaseData.aspx?action=update&id=' + row.id;
                    }
                    $('#ZDdlg').dialog('open').dialog('setTitle', '修改租借物品名称');
                    $('#ZDfm').form('load', row);
                    url = 'GoodsLeaseData.aspx?action=update&id=' + row.id;
                }
                else {
                    $('#ZDdlg').dialog('open').dialog('setTitle', '修改租借物品名称');
                    $('#ZDfm').form('load', row);
                    url = 'GoodsLeaseData.aspx?action=update&id=' + row.id;
                }



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
                        $('#wpnamecbo').combobox('reload');

                    }
                }
            });
        }

        function destroyMember() {
            var row = $('#ZDdg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('GoodsLeaseData.aspx?action=delete', { id: row.id }, function (result) {
                            if (!result.errorMsg) {
                                $('#ZDdg').datagrid('reload');
                                $('#wpnamecbo').combobox('reload');
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
        //关闭初始打开的遮盖层
        ajaxLoadEnd();
    </script>
</body>
</html>
