<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZhanghuChaxun.aspx.cs" Inherits="Cash_ZhanghuChaxun" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>帐户查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script src="../src/jquery.json-2.2.min.js" type="text/javascript"></script>
    <script src="../locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../src/datagrid-scrollview.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Hotelmgr.js"></script>
    <script type="text/javascript">
        var resizeTimeoutId = 0;
        var _kbpage = {
            sortField: 'ID',
            sortDirection: 0,
            pageNumber: 1,
            pageSize: 30
        };
        $(document).ready(function () {
            //设置布局层的事件
            $('.easyui-layout').layout('panel', 'east').panel({
                onResize: function (width, height) {
                    window.clearTimeout(resizeTimeoutId);
                    resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
                }
            });
            //客人类别
            $("#KerenLeibie").combobox({
                url: '../Setting/BasicInfoData.aspx?module=khlb&action=read',
                valueField: 'KHLB',
                textField: 'KHLB',
                editable: false,
                onLoadSuccess: function () {
                    var data = $('#KerenLeibie').combobox('getData');
                    if (data.length > 0) {
                        //                        $("#KerenLeibie").combobox('select', data[0].KHLB);
                    }
                }
            });
            //查询付款方式
            SelctPayType();
            IntializeTableDatagrid("#tt");
            ViewIntialization();
            doSearch();

        });
        function doSearch() {
            Selction("#tt");
        }
        //界面布局调整初始化
        function ViewIntialization() {
            //设置费用明细的长度和宽度
            try{
            var heightvalue = $(window.document.body).height();
            var widthvalue = $("#centerdiv").width();
            $('#tt').datagrid('resize', { height: heightvalue, width: widthvalue });
            //$('#centerdiv').attr("width", widthvalue * 0.2);
            }
            catch(e)
            {
              
            }
        }
        //查询单笔记录信息
        function SelctPayType() {
            //发送Post请求, 返回后执行回调函数.
            var paramdata = { "ActionName": 'SelPayType' };
            var Urlstr = '../ActionHanlder/CashChild/ShouKuanHalder.ashx';
            $.ajax({
                type: "POST",
                data: paramdata,
                dataType: 'json',
                url: Urlstr,
                async: false, //设为false就是同步请求
                cache: false,
                success: function (data) {
                    try {
                        $("#FukuanFangshi").combobox({
                            valueField: 'id',
                            textField: 'text'
                        }).combobox('loadData', data);
                    }
                    catch (e) {
                        alert(e);
                    }
                }
            });
        }

        //初始化每个选项卡相对应的表格
        function IntializeTableDatagrid(divid) {
            //初始化表格效果
            $(divid).datagrid({
                fitColumns: true,
                singleSelect: true,
                remoteSort: false,      // 后台排序
                pagination: true,
                animate: true,
                sortable: true,
                showFooter: true,
                toolbar: '#tb',
                columns: [[
                    { field: 'AutoID', title: 'ID', width: 80, hidden: true },
                    { field: 'OrderGuid', title: 'ID', width: 80, hidden: true },
                     { field: 'XingMing', title: '客户名称', sortable: true, width: 80, align: 'center' },
                    { field: 'FangHao', title: '主房间号', sortable: true, width: 120, align: 'center' },
                    { field: 'KerenLeibie', title: '入住类型', sortable: true, width: 60, align: 'center' },
                    { field: 'KerenLeibie', title: '客人类别', sortable: true, width: 60, align: 'center' },
                    { field: 'FukuanFangshi', title: '付款方式', sortable: true, width: 60, align: 'center' },
                    { field: 'YaJin', title: '开户金额', sortable: true, width: 80, align: 'center' },
                    { field: 'YaJin', title: '余额', sortable: true, width: 80, align: 'center' },
                    { field: 'ShijiFangjia', title: '房价', sortable: true, width: 60, align: 'center' },
                    { field: 'BeiZhu', title: '备注', sortable: true, width: 60, align: 'center' },
                    { field: 'DaodianTime', title: '入店日期', sortable: true, width: 120, align: 'center' },
                    { field: 'LidianTime', title: '离店日期', sortable: true, width: 120, align: 'center' },
                    { field: 'BeiZhu', title: '帐号状态', sortable: true, width: 60, align: 'center' },
                    { field: 'Status', title: '订单状态', sortable: true, width: 60, align: 'center' },
                    { field: 'CreateUser', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'QF_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.Status == "未结") {
                                var actiontxt = "<input type='button' value='结账' onclick=OpenJieZhangWindow('" + row.FangHao + "','"+row.OrderGuid+"') />";
                                return actiontxt;
                            }
                            else {
                                var actiontxt = "<input type='button' value='取消' onclick=OpenJieZhangWindow('" + row.FangHao + "','"+row.OrderGuid+"') />";
                                return actiontxt;
                            }
                        }
                    }
                ]],
                onSelect: function (rowData) {

                },
                onDblClickRow: function (rowIndex, rowData) {
                    OpenJieZhangWindow(rowData.FangHao, rowData.OrderGuid);
                }
            }).datagrid('getPager').pagination({
                pageSize: _kbpage.pageSize,
                pageNumber: _kbpage.pageNumber,
                onSelectPage: function (pageNumber, pageSize) {
                    _kbpage.pageNumber = pageNumber;
                    _kbpage.pageSize = pageSize;
                    Selction();
                }
            });

        }
        //查询发票信息
        function Selction(divid) {
            //发送Post请求, 返回后执行回调函数.
            var paramdata = {
                "page": _kbpage.pageNumber,
                "rows": _kbpage.pageSize,Begin: $('#Begin').datebox('getValue'),
                "KerenLeibie": $('#KerenLeibie').datebox('getValue'),
                "FukuanFangshi": $('#FukuanFangshi').datebox('getValue'),
                "ZhanghaoZhuangtai": $('#ZhanghaoZhuangtai').datebox('getValue'),
                "XingMing": $('#XingMing').val(),
                "FangHao": $('#FangHao').val(),
                "AutoID": $('#AutoID').val(),
                "End": $('#End').datebox('getValue')
            };
            var Urlstr = '../FrontDesk/OrderData.aspx?action=read';
            $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   $(divid).datagrid("loadData", { total: rowcount, rows: dataArray });
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
        }
        function OpenJieZhangWindow(fanhao,orderguid)
        {
         opennewwin('KeRenJieZhang.aspx?fh=' + fanhao +"&orderguid="+orderguid, '553', '800', "KeRenJieZhangwindows");
        }
    </script>
</head>
<body class="easyui-layout">
    <div region="east" split="true" title="East" id="eastdiv" class="leftpage " style="width: 150px;">
        <form id="frm" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" Width="80" Text="账务信息" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" Width="80" Text="房间信息" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button3" Width="80" Text="导出EXCEL" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button5" Width="80" Text="发票查询" runat="server" />
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div data-options="region:'center'" id="centerdiv" style="padding: 1px; overflow: hidden;">
        <table id="tt">
        </table>
        <div id="tb" style="height: 100px; padding-bottom: 15px;">
            <table>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        客户名称
                    </td>
                    <td style="width: 170px;">
                        <input class="easyui-validatebox" type="text" id="XingMing" name="XingMing" />
                    </td>
                    <td style="width: 80px;">
                        房号
                    </td>
                    <td style="width: 170px;">
                        <input class="easyui-validatebox" type="text" name="FangHao" id="FangHao" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        开户日期从:
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="Begin" id="Begin" />
                    </td>
                    <td>
                        到：
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="End" id="End" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        编号
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="AutoID" id="AutoID" />
                    </td>
                    <td>
                        客人类别
                    </td>
                    <td>
                        <input class="easyui-combobox" name="KerenLeibie" id="KerenLeibie" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        付款方式
                    </td>
                    <td>
                        <input class="easyui-combobox" name="FukuanFangshi" id="FukuanFangshi" />
                    </td>
                    <td>
                        订单状态
                    </td>
                    <td>
                        <select id="ZhanghaoZhuangtai" class="easyui-combobox" name="ZhanghaoZhuangtai">                           
                            <option value="在住" selected="selected">在住</option>
                            <option value="未结离店">未结离店</option>
                            <option value="已结离店">已结离店</option>
                        </select>
                    </td>
                    <td style="width: 100px; padding-left: 5px" align="center">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="doSearch()">查询</a>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>
