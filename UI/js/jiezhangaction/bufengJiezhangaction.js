
//#region 共用变更

var _jbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 10
}
var _bjbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var alldataarray = [];
var temparray = [];
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var tab_jz_id = "#tab_allfeiyong";
var tab_bj_id = "#tab_jzfeiyonglist";
var orderguid = "";
var addflag = false; //以判断是否新增表格行
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {

    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
    }

    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    //初始化所有费用
    IntializeTableDatagrid(tab_jz_id, "")
    //初始化选中结账费用
    IntializeTableDatagrid_WeiJie(tab_bj_id, "选中结账明细");
    //关闭初始打开的遮盖层
    ajaxLoadEnd();
    //显示布局
    ViewIntialization();
    //查询相关数据信息
    RefreshDataGrid();
});
//#endregion

//#region 操作列表
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid(divid, titletxt) {
    //初始化表格效果
    //初始化表格效果
    $(divid).datagrid({
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: false,
        animate: true,
        sortable: true,
        showFooter: true,
        selectOnCheck: false,
        checkOnSelect: false,
        columns: [[
                    { field: 'RZ_ID', checkbox: true, title: 'ID', width: 0 },
                    { field: 'RZ_OrderGuid', title: 'OrderID', width: 0, hidden: true },
                    { field: 'RZ_RoomNo', title: '房号', width: 60 },
                    { field: 'RZ_FYType', title: '项目名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiyongType', title: '费用名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiYongName', title: '商品名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_Money', title: '费用金额', sortable: true, width: 60, align: 'center' },
                    { field: 'RZ_Payment', title: '付款方式', align: 'center', sortable: true, width: 30 },
                    { field: 'RZ_Operator', title: '操作员', sortable: true, width: 30, align: 'center' },
                    { field: 'RZ_RunnTime', title: '时间', editor: 'text', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                               return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));                               
                            } else {
                                return value;
                            }
                        }
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onCheckAll: function (rows) {
            //var tmept = rowData;

            var currendata = { total: 0, rows: [] };
            var allmoney = 0;
            var jzmoney = 0;
            if (rows.length > 0) {
                for (var i = 0; i < rows.length; i++) {
                    temparray.push(rows[i]);
                }
                for (var y = 0; y < temparray.length; y++) {
                    jzmoney = temparray[y].RZ_Money * 1 + jzmoney;
                }
                $(tab_bj_id).datagrid('loadData', { total: temparray.length, rows: temparray });
                $(divid).datagrid('loadData', { total: currendata.rows.length, rows: currendata.rows });
                $("#WeijieMoney").val(allmoney.toFixed(2));
                $("#JieZhanShouKuan").val(jzmoney.toFixed(2));
                $("#WuEMoney").val((allmoney+ jzmoney).toFixed(2));
            }
        },
        onCheck: function (rowIndex, rowData) {
            var tmept = rowData;
            var currendata = $(divid).datagrid('getData');
            var allmoney = 0;
            var jzmoney = 0;
            temparray.push(rowData);
            for (var i = 0; i < currendata.rows.length; i++) {
                if (currendata.rows[i].RZ_ID == rowData.RZ_ID) {
                    currendata.rows.splice(i, 1);
                    i = i - 1;
                }
                else {
                    allmoney = currendata.rows[i].RZ_Money * 1 + allmoney;
                }
            }
            for (var y = 0; y < temparray.length; y++) {
                jzmoney = temparray[y].RZ_Money * 1 + jzmoney;
            }

            $(tab_bj_id).datagrid('loadData', { total: temparray.length, rows: temparray });
            $(divid).datagrid('loadData', { total: currendata.rows.length, rows: currendata.rows });
            $("#WeijieMoney").val(allmoney.toFixed(2));
            $("#JieZhanShouKuan").val(jzmoney.toFixed(2));
            $("#WuEMoney").val((allmoney + jzmoney).toFixed(2));

        },
        onUncheck: function (rowIndex, rowData) {
        }
    }).datagrid('getPager').pagination({
        pageSize: _jbpage.pageSize,
        pageNumber: _jbpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _jbpage.pageNumber = pageNumber;
            _jbpage.pageSize = pageSize;
        }
    });
}
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid_WeiJie(divid, titletxt) {
    //初始化表格效果
    $(divid).datagrid({
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: false,
        animate: true,
        sortable: true,
        showFooter: true,
        selectOnCheck: false,
        checkOnSelect: false,
        columns: [[
                    { field: 'RZ_ID', title: 'ID', checkbox: true, width: 0 },
                    { field: 'RZ_OrderGuid', title: 'OrderID', width: 0, hidden: true },
                    { field: 'RZ_RoomNo', title: '房号', width: 60 },
                    { field: 'RZ_FYType', title: '项目名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiyongType', title: '费用名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiYongName', title: '商品名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_Money', title: '费用金额', sortable: true, width: 60, align: 'center' },
                    { field: 'RZ_Payment', title: '付款方式', align: 'center', sortable: true, width: 30 },
                    { field: 'RZ_Operator', title: '操作员', sortable: true, width: 30, align: 'center' },
                    { field: 'RZ_RunnTime', title: '时间', editor: 'text', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        } 
                    },
                    { field: 'RZ_action', title: '操作', formatter: function (value, row, index) {
                        return value; //"<img src=\"../../Images/cross.png\" onclick=\"javascript:DeleteItem('" + row.RZ_ID + "')\" />";
                    }, editor: 'text', sortable: true, width: 60, align: 'center'
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onCheckAll: function (rows) {
            var currendata = $(tab_jz_id).datagrid('getData');
            var allmoney = 0;
            var jzmoney = 0;
            if (rows.length > 0) {
                temparray = [];
                for (var i = 0; i < rows.length; i++) {
                    currendata.rows.push(rows[i]);
                }
                for (var y = 0; y < currendata.rows.length; y++) {
                    allmoney = currendata.rows[y].RZ_Money * 1 + allmoney;
                }
                $(divid).datagrid('loadData', { total: temparray.length, rows: temparray });
                $(tab_jz_id).datagrid('loadData', { total: currendata.rows.length, rows: currendata.rows });
                $("#WeijieMoney").val(allmoney.toFixed(2));
                $("#JieZhanShouKuan").val(jzmoney.toFixed(2));
                $("#WuEMoney").val((allmoney+ jzmoney).toFixed(2));

            }
        },
        onCheck: function (rowIndex, rowData) {
            var tmept = rowData;
            var currendata = $(tab_jz_id).datagrid('getData');
            var allmoney = 0;
            var jzmoney = 0;
            currendata.rows.push(rowData);
            for (var i = 0; i < temparray.length; i++) {
                if (temparray[i].RZ_ID == rowData.RZ_ID) {
                    temparray.splice(i, 1);
                    i = i - 1;
                }
                else {
                    jzmoney = temparray[i].RZ_Money * 1 + jzmoney;
                }
            }
            for (var y = 0; y < currendata.rows.length; y++) {
                allmoney = currendata.rows[y].RZ_Money * 1 + allmoney;
            }
            $(divid).datagrid('loadData', { total: temparray.length, rows: temparray });
            $(tab_jz_id).datagrid('loadData', { total: currendata.rows.length, rows: currendata.rows });
            $("#WeijieMoney").val(allmoney.toFixed(2));
            $("#JieZhanShouKuan").val(jzmoney.toFixed(2));
            $("#WuEMoney").val((allmoney + jzmoney).toFixed(2));

        }
    }).datagrid('getPager').pagination({
        pageSize: _bjbpage.pageSize,
        pageNumber: _bjbpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _bjbpage.pageNumber = pageNumber;
            _bjbpage.pageSize = pageSize;
            DataPageAction(divid, temparray);
        }
    });
}

//查询入住相关费用信息
function Selction(divid, orderguid, staticvalue, _kbpage) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByRZFeiYongList', "PageInfo": $.toJSON(_kbpage),
        "RZ_OrderGuid": orderguid, "RZ_Status": staticvalue
    };
    var Urlstr = "../../ActionHanlder/CashChild/BuFengJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   $(divid).datagrid("loadData", { total: rowcount, rows: dataArray });

                   var tempvalue = 0;
                   for (var i = 0; i < dataArray.length; i++) {
                       tempvalue = dataArray[i].RZ_Money * 1 + tempvalue;
                   }
                   $("#WuEMoney").val(tempvalue.toFixed(2));
                   $("#WeijieMoney").val(tempvalue.toFixed(2));
                   $("#JieZhanShouKuan").val(0);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//增加红字冲账信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/BuFengJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   alert(data.Message);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//刷新页面数据
function RefreshDataGrid() {
    _jbpage.pageSize = 10000;
    Selction(tab_jz_id, orderguid, "", _jbpage);
    // Selction(tab_bj_id, orderguid, "结账", _bjbpage);
    _jbpage.pageSize = 10;
}
function DeleteItem(idstr) {
    var i = 0;
    for (var i = 0; i < temparray.length; i++) {
        if (temparray[i].RZ_ID == idstr) {
            temparray.splice(i, 1);
            break;
        }
    }
    var dataarr = $(tab_jz_id).datagrid("getRows");
    for (var i = 0; i < dataarr.length; i++) {
        if (dataarr[i].RZ_ID == idstr) {
            $(tab_jz_id).datagrid("uncheckRow", i);
            break;
        }
    }
    $(tab_bj_id).datagrid('loadData', temparray);
}

//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().parent().parent().height() * 0.4;
    widthvalue = $(".easyui-tabs").parent().parent().parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $(tab_bj_id).datagrid('resize', { height: heightvalue * 0.88, width: widthvalue }); ;
    $(tab_jz_id).datagrid("resize", { height: heightvalue * 0.90, width: widthvalue });
}

function Operator_jiezhang() {
    if (temparray.length <= 0) {
        alert("请先选择费用明细再进行结账操作!")
        return;
    }
    $('#WindailogDiv').dialog({
        title: '结帐',
        width: $('.easyui-layout').width() * 0.9,
        height: $('.easyui-layout').height() * 0.9,
        closed: false,
        cache: false,
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        modal: false,
        onBeforeOpen: function () {
            ajaxLoading('#windowIframe');
        },
        onBeforeClose: function () {
            RefreshDataGrid();
            temparray = [];
            $(tab_bj_id).datagrid('loadData', temparray);
        },
        onClose: function () {

        }
    });
    $('#windowIframe')[0].src = 'jiezhang.htm?OrderGuid=' + orderguid + '&savetype=bufengjie';
}
function Operator_jijiejiezhang() {
    if (temparray.length <= 0) {
        alert("请先选择费用明细再进行结账操作!")
        return;
    }
    $('#WindailogDiv').dialog({
        title: '结帐',
        width: $('.easyui-layout').width() * 0.9,
        height: $('.easyui-layout').height() * 0.9,
        closed: false,
        cache: false,
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        modal: false,
        onBeforeOpen: function () {
            ajaxLoading('#windowIframe');
        },
        onBeforeClose: function () {
            RefreshDataGrid();
            temparray = [];
            $(tab_bj_id).datagrid('loadData', temparray);
        },
        onClose: function () {

        }
    });
    $('#windowIframe')[0].src = 'jiezhang.htm?OrderGuid=' + orderguid + '&savetype=bufengjie&isYaJing=1';
}
//关闭结账窗口
function Operator_closejijiejiezhang() {
    $('#WindailogDiv').dialog('close');
}
function GetXianZhongFeiYongList() {
    var FeiYongListObject = new Object(); ;
    FeiYongListObject.Money = $("#JieZhanShouKuan").val();
    FeiYongListObject.Datas = $(tab_bj_id).datagrid("getData").rows;
    return FeiYongListObject;
}
//#endregion
