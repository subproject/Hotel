
//#region 共用变更

var _kbpage =     // 客户用品分页对象
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
var addflag = false; //以判断是否新增表格行
var Address = [{ "value": "1", "text": "CHINA" }, { "value": "2", "text": "USA" }, { "value": "3", "text": "Koren" }, { "value": "4", "text": "饮食"}];
var fanhao = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    //获取入住记录ID
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
        fanhao = urlquerstr[1];
        fanhao = fanhao.replace('fh=', "");
    }
    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    //初始化数据
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "明细":
                    IntializeTableDatagrid('#tab_jiezhanglist');
                    Selction('#tab_jiezhanglist', orderguid)
                    break;
            }
        }
    });

    //关闭初始打开的遮盖层
    ajaxLoadEnd();
    //alert($('.easyui-tabs').parent().height());
    $('.easyui-tabs').tabs({
        width: "auto",
        height: $('.easyui-tabs').parent().parent().height()
    });
});
//#endregion

//#region 操作列表
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid(divid) {
    //初始化表格效果
    $(divid).datagrid({
        height: $('.easyui-tabs').parent().height() * 0.90,
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'ID', title: 'ID', width: 80, hidden: true },
                    { field: 'JZ_OrderGUID', title: 'OrderGUID', hidden: true },
                    { field: 'CreateTime', title: '结算时间', sortable: true, width: 150, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'JZ_Consumption', title: '结算金额', sortable: true, width: 80, align: 'center' },
                    { field: 'JZ_Preferential', title: '优惠金额', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_Accounts', title: '应收金额', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_Money', title: '支付金额', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_DepositDeduct', title: '押金扣款', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_Surplus', title: '退款金额', editor: { type: 'text', options: { required: true} }, sortable: true, width: 80 },
                    { field: 'JZ_DepositDeduct', title: '备注', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_Receipt', title: '单据号', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'JZ_Remark', title: '备注', sortable: true, width: 60, align: 'center' },
                    { field: 'CreateUser', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'JZ_Editor', title: '操作', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            var actiontxt = "<input type='button' value='编辑' onclick=Operator_jiezhang('" + row.JZ_OrderGUID + "','" + row.ID + "') />"
                            return actiontxt;
                        }
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onDblClickRow: function (rowIndex, rowData) {
            onclick = Operator_jiezhang("'" + row.JZ_OrderGUID + "', '" + row.ID + "'");
        },
        onSelect: function (rowData) {

        }
    }).datagrid('getPager').pagination({
        pageSize: _kbpage.pageSize,
        pageNumber: _kbpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kbpage.pageNumber = pageNumber;
            _kbpage.pageSize = pageSize;
            RefreshDataGrid();
        }
    });

}

//查询结账明细信息
function Selction(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByJieZhangList', "PageInfo": $.toJSON(_kbpage),
        "JZ_OrderGUID": orderguid
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
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

function Operator_jiezhang(xorderguid,id) {
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

        },
        onClose: function () {
            RefreshDataGrid();
        }
    });
    var urltxt= 'jiezhang.htm?OrderGuid=' + xorderguid + '&ID='+id
    $('#windowIframe')[0].src = urltxt;
}

function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
}

//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().height();
    widthvalue = $(".easyui-tabs").parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $('#tab_jiezhanglist').datagrid('resize', { height: heightvalue * 0.9, width: widthvalue });

}
//全部撤消结账
function CanceRowMainEntity() {
    var data = $('#tab_jiezhanglist').datagrid("getRows");
    if (data.length == 0) {
        alert("非常抱歉,没有结账信息,不能进行取消结账动作！");
        return;
    }
    $.messager.confirm('Confirm', '确定要进行取消所有结账信息?', function (r) {
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
