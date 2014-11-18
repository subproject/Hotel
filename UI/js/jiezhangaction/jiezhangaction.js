//#region 公共变更
var _kapage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _kcpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _kdpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _kepage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var orderguid = "";
var currdatagrid = "";
var fanhao = "";
//#endregion
//#region 结帐界面初始化
$(document).ready(function () {
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        fanhao = urlquerstr[0];
        fanhao = fanhao.replace('fh=', "");
    }

    orderguid = $("#orderID").val();
    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    $('#AllCostsdgrid').treegrid({
        rownumbers: true,
        animate: true,
        collapsible: true,
        fitColumns: true,
        pagination: true,
        method: 'get',
        idField: 'id',
        treeField: 'RZ_FeiYongName',
        showFooter: true,
        columns: [[
                    { field: 'RZ_FeiYongName', title: '费用名称', width: 150, align: 'center' },
                    { field: 'RZ_FeiYongCode', title: '费用代码', width: 80, align: 'center' },
                    { field: 'RZ_Money', title: '消费金额', width: 80, align: 'center' },
                    { field: 'RR_RedMoney', title: '红冲金额', width: 80, align: 'center' },
                    { field: 'RZ_Payment', title: '支付方式', hidden: true, width: 60, align: 'center' },
                    { field: 'RZ_Remark', title: '备注', width: 80, align: 'center' },
                    { field: 'RZ_ReciptNo', title: '单据号码', width: 80, align: 'center' },
                    { field: 'RZ_RoomNo', title: '房间号', width: 80, align: 'center' },
                    { field: 'RZ_RunnTime', title: '发生时间', width: 80, align: 'center' },
                    { field: 'RZ_Status', title: '结账状态', width: 60, align: 'center' }
                ]],
        onDblClickRow: function (rowData) {
            SelSigeFeiYong(rowData.id);
        }
    }).treegrid('getPager').pagination({
        pageSize: _kapage.pageSize,
        pageNumber: _kapage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kapage.pageNumber = pageNumber;
            _kapage.pageSize = pageSize;
            // RefreshDataGrid();
        }
    });
    //收退款
    $('#shoutukuandgrid').treegrid({
        rownumbers: true,
        animate: true,
        collapsible: true,
        fitColumns: true,
        pagination: true,
        method: 'get',
        idField: 'id',
        treeField: 'SK_PayType',
        showFooter: true,
        columns: [[
                    { field: 'SK_PayType', title: '支付方式', width: 150, align: 'center' },
                    { field: 'SK_Type', title: '款项类型', width: 80, align: 'center' },
                    { field: 'SK_Money', title: '金额', width: 80, align: 'center' },
                    { field: 'SK_YiShouMoney', title: '预收金额', width: 60, align: 'center' },
                    { field: 'SK_YingShouMoney', title: '应收金额', width: 80, align: 'center' },
                    { field: 'SK_LianfanHao', title: '房间号', width: 80, align: 'center' },
                    { field: 'SK_PayTime', title: '发生时间', width: 80, align: 'center' },
                    { field: 'SK_Remark', title: '备注', width: 80, align: 'center' },
                    { field: 'SK_Receipt', title: '单据号码', width: 80, align: 'center' },
                    { field: 'SK_RZCode', title: '支付代码', width: 60, align: 'center' }
                ]],
        onDblClickRow: function (rowData) {
            if (rowData.SK_Type == "收款") {
                Operator_Shoukuan(rowData.id);
            }
            else {
                Operator_tuikuan(rowData.id);
            }
        },
        onHeaderContextMenu: function (e, field) {
            e.preventDefault();
            if (!cmenu) {
                createColumnMenu();
            }
            cmenu.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    }).datagrid('getPager').pagination({
        pageSize: _kcpage.pageSize,
        pageNumber: _kcpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kcpage.pageNumber = pageNumber;
            _kcpage.pageSize = pageSize;
            RefreshDataGrid();
        }
    }); ;
    //已转账
    $('#jzhuangdgrid').treegrid({
        rownumbers: true,
        animate: true,
        collapsible: true,
        fitColumns: true,
        pagination: true,
        method: 'get',
        idField: 'id',
        treeField: 'RZ_FeiYongName',
        showFooter: true,
        columns: [[
                    { field: 'RZ_FeiYongName', title: '费用名称', width: 150, align: 'center' },
                    { field: 'RZ_FeiYongCode', title: '费用代码', width: 80, align: 'center' },
                    { field: 'RZ_Money', title: '消费金额', width: 80, align: 'center' },
                    { field: 'RZ_Payment', title: '支付方式', hidden: true, width: 60, align: 'center' },
                    { field: 'RZ_Remark', title: '备注', width: 80, align: 'center' },
                    { field: 'RZ_ReciptNo', title: '单据号码', width: 80, align: 'center' },
                    { field: 'RZ_RoomNo', title: '房间号', width: 80, align: 'center' },
                    { field: 'RZ_RunnTime', title: '发生时间', width: 80, align: 'center' },
                    { field: 'RZ_Status', title: '结账状态', width: 60, align: 'center' }
                ]],
        onDblClickRow: function (rowData) {
            SelSigeFeiYong(rowData.id);
        },
        onHeaderContextMenu: function (e, field) {
            e.preventDefault();
            if (!cmenu) {
                createColumnMenu();
            }
            cmenu.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    }).datagrid('getPager').pagination({
        pageSize: _kdpage.pageSize,
        pageNumber: _kdpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kdpage.pageNumber = pageNumber;
            _kdpage.pageSize = pageSize;
            //RefreshDataGrid();
        }
    }); ;
    //已结算
    $('#jjiedgrid').treegrid({
        rownumbers: true,
        animate: true,
        collapsible: true,
        fitColumns: true,
        pagination: true,
        method: 'get',
        idField: 'id',
        treeField: 'RZ_FeiYongName',
        showFooter: true,
        columns: [[
                    { field: 'RZ_FeiYongName', title: '费用名称', width: 150 },
                    { field: 'RZ_FeiYongCode', title: '费用代码', width: 80 },
                    { field: 'RZ_Money', title: '消费金额', width: 80, align: 'center' },
                    { field: 'RZ_Payment', title: '支付方式', hidden: true, width: 60, align: 'center' },
                    { field: 'RZ_Remark', title: '备注', width: 80, align: 'center' },
                    { field: 'RZ_ReciptNo', title: '单据号码', width: 80, align: 'center' },
                    { field: 'RZ_RoomNo', title: '房间号', width: 80, align: 'center' },
                    { field: 'RZ_RunnTime', title: '发生时间', width: 80, align: 'center' },
                    { field: 'RZ_Status', title: '结账状态', width: 60, align: 'center' }
                ]],
        onDblClickRow: function (rowData) {
            SelSigeFeiYong(rowData.id);
        },
        onHeaderContextMenu: function (e, field) {
            e.preventDefault();
            if (!cmenu) {
                createColumnMenu();
            }
            cmenu.menu('show', {
                left: e.pageX,
                top: e.pageY
            });
        }
    }).datagrid('getPager').pagination({
        pageSize: _kepage.pageSize,
        pageNumber: _kepage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kepage.pageNumber = pageNumber;
            _kepage.pageSize = pageSize;
            //RefreshDataGrid();
        }
    }); ;
    ViewIntialization();
    //设置选项卡事件
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "费用明细":
                    currdatagrid = "AllCostsdgrid";
                    SelAllFeiYongList("#AllCostsdgrid", orderguid);
                    break;
                case "收退款":
                    currdatagrid = "shoutukuandgrid";
                    SelShouTuKuanList("#shoutukuandgrid", orderguid);
                    break;
                case "已转账":
                    currdatagrid = "jzhuangdgrid";
                    SelZhuanZhangFeiYongList("#jzhuangdgrid", orderguid);
                    break;
                case "已结算":
                    currdatagrid = "jjiedgrid";
                    SelJieZhangFeiYongList("#jjiedgrid", orderguid);
                    break;
            }
            ViewIntialization();
        }
    });

});
//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().parent().height();
    widthvalue = $(".easyui-tabs").parent().parent().width();
    $(".easyui-tabs").tabs('getSelected').panel('resize', { width: widthvalue, height: heightvalue });
    var currtableid = "#" + currdatagrid;
    var p = $(currtableid).datagrid('resize', { width: widthvalue, height: heightvalue * 0.7 });
}
//#endregion

//#region 结账相关操作
function Operator_kefanfeiyongdetail(idtxt) {
    opennewdivform('客房费用', '#windowIframe', '#WindailogDiv',
     'CashAction/kefanfeiyongdetail.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&id=' + idtxt, 'open', RefreshDataGrid);
}
function Operator_shunwupeichang(idtxt) {
    opennewdivform('损物赔偿', '#windowIframe', '#WindailogDiv',
    'CashAction/shunwupeichang.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&id=' + idtxt, 'open', RefreshDataGrid);
}
function Operator_Shoukuan() {
    opennewdivform('收款操作', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_tuikuan() {
    opennewdivform('退款操作', '#windowIframe', '#WindailogDiv',
    'CashAction/tuikuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_zaocanfei(idtxt) {
    opennewdivform('早餐费用', '#windowIframe', '#WindailogDiv',
     'CashAction/zaocanfei.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&id=' + idtxt, 'open', RefreshDataGrid);
}
function Operator_qitafeiyong(idtxt) {
    opennewdivform('其他费用', '#windowIframe', '#WindailogDiv',
    'CashAction/qitafeiyong.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&id=' + idtxt, 'open', RefreshDataGrid);
}
function Operator_hongchongfeiyong() {
    opennewdivform('红冲费用', '#windowIframe', '#WindailogDiv',
    'CashAction/hongchongfeiyong.htm?OrderGuid=' + orderguid + '&fh=' + fanhao, 'open', RefreshDataGrid);
}
function Operator_zhifengyoufei() {
    opennewdivform('积分费用', '#windowIframe', '#WindailogDiv',
    'CashAction/zhifengyoufei.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_keyenziliao() {
    opennewdivform('客人资料', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_zhujiemuping() {
    opennewdivform('租借物品', '#windowIframe', '#WindailogDiv',
    '../FrontDesk/GoodsLease.aspx?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_shuidianmeifeiyong() {
    opennewdivform('水电煤', '#windowIframe', '#WindailogDiv',
    'CashAction/shuidianmeifeiyong.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_bufengjiezhang() {
    opennewdivform('部分结账', '#windowIframe', '#WindailogDiv',
     'CashAction/bufengjiezhang.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_bujiezhangtufan() {
    opennewdivform('不结账退房', '#windowIframe', '#WindailogDiv',
     'CashAction/bujiezhangtufan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_qiangtaifuxian() {
    opennewdivform('前台付现', '#windowIframe', '#WindailogDiv',
     'CashAction/qiangtaifuxian.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_sankexunizhangdang() {
    opennewdivform('虚拟帐单', '#windowIframe', '#WindailogDiv',
    'CashAction/sankexunizhangdang.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_fabiaomanage() {
    opennewdivform('发票管理', '#windowIframe', '#WindailogDiv',
    'CashAction/fapiaomanage.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_cancejiezhang() {
    //CanceRowMainEntity();
    opennewdivform('撤销结账', '#windowIframe', '#WindailogDiv',
    'CashAction/Cancejiezhangmanage.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_actionlog() {
    opennewdivform('操作记录', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_chakandengjidian() {
    opennewdivform('查看登录单', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}

function Operator_Shoukuan(recordid) {
    opennewdivform('收款', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&recordid=' + recordid, 'open', RefreshDataGrid);
}

function Operator_tuikuan(recordid) {
    opennewdivform('退款', '#windowIframe', '#WindailogDiv',
    'CashAction/tuikuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&recordid=' + recordid, 'open', RefreshDataGrid);
}
function Operator_sankezhuanzhang() {
    opennewdivform('转帐', '#windowIframe', '#WindailogDiv',
    'CashAction/sankezhuanzhang.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}
function Operator_jiezhang() {
    opennewdivform('结帐', '#windowIframe', '#WindailogDiv',
    'CashAction/Endjiezhang.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '&isend=1', 'open', RefreshDataGrid);
}
function Operator_dayinzhangdan() {
    window.open('CashAction/sankejiezhangdangPrint.htm?ID=00000000-0000-0000-0000-000000000000&OrderGuid=' + orderguid, "xunizhangdanprintview",
     "width=1100,height=400,title='打印结账单',toolbar=no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no,z-look=yes");

}

function Operator_chakandengjidian() {
    opennewdivform('注销卡门', '#windowIframe', '#WindailogDiv',
    'CashAction/Shoukuan.htm?OrderGuid=' + orderguid + '&fh=' + fanhao + '', 'open', RefreshDataGrid);
}

function CloseWindow() {
    window.close();
}

//撤消结账
function CanceRowMainEntity() {
    $.messager.confirm('Confirm', '确定要进行取消当前订单的所有结账信息?', function (r) {
        if (r) {
            $.messager.progress({ text: "正在处理，请等候……" });
            //发送Post请求, 返回后执行回调函数.
            var paramdata = { "ActionName": 'CanceMainEntity', "JZ_OrderGUID": orderguid, "ExuStatuc": "AllEnd"
            };
            var Urlstr = "../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
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
               $.messager.progress('close');
               RefreshDataGrid();
           },
         "json");
        }
    });
}
//#endregion

//#region 数据查询相关操作
//查询全部入住订单费用信息
function SelAllFeiYongList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    _kapage.pageNumber = 10000;
    var paramdata = { "ActionName": 'SelOrderFeiYongList', "PageInfo": $.toJSON(_kapage),
        "RZ_OrderGuid": orderguid
    };
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = [eval("(" + data + ")")];
                   $(divid).treegrid("loadData", { rows: dataArray[0].Rows });
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "text");
}
//查询入住订单相关的已转账费用信息
function SelZhuanZhangFeiYongList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    _kdpage.pageSize = 10000;
    var paramdata = { "ActionName": 'SelZhuanZhangFeiYongList', "PageInfo": $.toJSON(_kdpage),
        "RZ_OrderGuid": orderguid
    };
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data != "") {
                       dataArray = [eval("(" + data + ")")];
                   }
                   else {
                       dataArray = [{ Rows: []}]
                   }
                   $(divid).treegrid("loadData", { rows: dataArray[0].Rows });
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "text");
}
//查询入住订单相关的已结账费用信息
function SelJieZhangFeiYongList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    _kepage.pageSize = 10000;
    var paramdata = { "ActionName": 'SelJieZhangFeiYongList', "PageInfo": $.toJSON(_kepage),
        "RZ_OrderGuid": orderguid
    };
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data != "") {
                       dataArray = [eval("(" + data + ")")];
                   }
                   else {
                       dataArray = [{ Rows: []}]
                   }
                   $(divid).treegrid("loadData", { rows: dataArray[0].Rows });
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "text");
}
//查询入住订单相关的已结账费用信息
function SelShouTuKuanList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    _kcpage.pageSize = 10000;
    var paramdata = { "ActionName": 'SelShouKuanList', "PageInfo": $.toJSON(_kcpage),
        "SK_OrderGUID": orderguid
    };
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data != "") {
                       dataArray = [eval("(" + data + ")")];
                   }
                   else {
                       dataArray = [{ Rows: []}]
                   }
                   $(divid).treegrid("loadData", { rows: dataArray[0].Rows });
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "text");
}
//查询入住订单相关的已结账费用信息
function SelSigeFeiYong(id) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelSigeFeiYongInfo', "RZ_ID": id };
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data != null) {
                       var tableindex = null;
                       switch (data.rows.RZ_FYType) {
                           case "客房用品":
                               switch (data.rows.RZ_FeiyongType) {

                                   case "客户用品":
                                       tableindex = 0;
                                       break;
                                   case "洗衣费":
                                       tableindex = 1;
                                       break;
                                   case "房间卖品":
                                       tableindex = 2;
                                       break;
                                   case "其他客房费":
                                       tableindex = 3;
                                       break;
                               }
                               Operator_kefanfeiyongdetail(data.rows.RZ_ID + ";" + tableindex);
                               break;
                           case "损物赔偿":
                               switch (data.rows.RZ_FeiyongType) {

                                   case "布件":
                                       tableindex = 0;
                                       break;
                                   case "房内用品":
                                       tableindex = 1;
                                       break;
                                   case "污染烟烫":
                                       tableindex = 2;
                                       break;
                                   case "其他":
                                       tableindex = 3;
                                       break;
                               }
                               Operator_shunwupeichang(data.rows.RZ_ID + ";" + tableindex);
                               break;
                           case "早餐费用":
                               tableindex = 0;
                               Operator_zaocanfei(data.rows.RZ_ID + ";" + tableindex);
                               break;
                           case "其它费用":
                               tableindex = 0;
                               Operator_qitafeiyong(data.rows.RZ_ID + ";" + tableindex);
                               break;
                       }
                   }
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "json");
}
//查询入住订单相关的已结账费用信息
function SelComplexOrderinfo() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelComplexOrderInfo', "fanhao": fanhao,"OrderGuid":orderguid }
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../ActionHanlder/CashChild/OrderJieZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   var dataarray = [eval("(" + data + ")")];
                   setvalue(dataarray[0]);
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
           },
         "text");
}

function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
    SelComplexOrderinfo();
}

function setvalue(data) {
    var order = data.Order;
    var orderinfo = data.orderdata;
    var rooms = data.rooms;
    var xitongbeizhu = data.xtRemark;
    $("#XingMing").html(order.XingMing);
    $("#FangjianLeixing").html(order.FangjianLeixing);
    if (order.Status == 1) {
        $("#timtype").html("结账时间");
        $("#Status").html("已结离店");
    }
    else {
        if (order.Status == 0) {
            $("#timtype").html("预离时间");
            $("#Status").html("在住");
        }
        else {
            $("#timtype").html("离店时间");
            $("#Status").html("未结离店");
        }
    }
    $("#ShijiFangjia").html(order.ShijiFangjia);
    $("#daodian").html((eval('new ' + (order.DaodianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#LidianTime").html(eval('new ' + (order.LidianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
    $("#TeQuanRen").html(order.TeQuanRen);
    $("#BaoMi").html(order.BaoMi);
    $("#rooms").html(rooms);
    $("#YongJing").html("");
    $("#XiaoShouYuan").html(order.XiaoShouYuan);
    $("#KerenLeibie").html(order.KerenLeibie);
    $("#SpeRemark").html("");
    $("#yingshou").html((orderinfo.allCosumper - orderinfo.ALLJZConsumption).toFixed(2));
    $("#ykshou").html((orderinfo.YaJin - orderinfo.allDepositDeduct).toFixed(2));
    $("#jzshou").html(orderinfo.ALLJZConsumption);
    $("#allmoney").html(orderinfo.allCosumper);
    $("#alltukuan").html((orderinfo.YaJin + orderinfo.allMoney).toFixed(2));
    $("#yishouquan").html("");
    $("#jieyu").html(((orderinfo.YaJin - orderinfo.allDepositDeduct - (orderinfo.allCosumper - orderinfo.ALLJZConsumption))).toFixed(2));
    $("#youhui").html(orderinfo.allPreferentialy);
    $("#beizhu").html("");
    $("#xitongbeizhu").html(xitongbeizhu);
    $("#xitongbeizhu").attr("color", 'red');
    if (order.ChangBao == true) {
        $("#ruzhuliexing").html("长包");
    }
    else if (order.ZhongDian == true) {
        $("#ruzhuliexing").html("钟点");
    }
    else if (order.ShijiFangjia <= 0)//免费房
    {
        $("#ruzhuliexing").html("免费");
    }
    else {
        $("#ruzhuliexing").html("正常");
    }

}
//关闭结账窗口
function Operator_closejijiejiezhang() {
    $('#WindailogDiv').dialog('close');
    window.opener.location.reload();
    window.close();

}
//#endregion

