
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var currdatarow = null;
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var orderguid = "60A7E288-5083-4B6D-841E-A5A98C77F8A8";
var addflag = false; //以判断是否新增表格行
var rdid = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
        if (urlquerstr[1] != undefined && urlquerstr[1] != null) {
            rzid = urlquerstr[1];
        }
    }

    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    //初始化所有费用
    IntializeTableDatagrid("#table_hongchongfeiyong", "红冲费用明细");
    //关闭初始打开的遮盖层
    ajaxLoadEnd();
    RefreshDataGrid();
    //重新布局界面
    ViewIntialization();
});
//#endregion

//#region 操作列表
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid(divid, titletxt) {
    //初始化表格效果
    $(divid).datagrid({
        title: titletxt,
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        toolbar: '#datagridtool',
        columns: [[
         { field: 'AutoID', title: '', hidden: true, sortable: true, width: 0, align: 'center' },
         { field: 'OrderGuid', title: '', hidden: true, sortable: true, width: 0, align: 'center' },
                    { field: 'RoomNo', title: '房间号', sortable: true, width: 30, align: 'center' },
                     { field: 'CustomerName', title: '客户名称', sortable: true, width: 60, align: 'center' },
                    { field: 'RunningTime', title: '费用日期', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        } 
                    },
                    { field: 'KM', title: '费用名称', sortable: true, width: 60, align: 'center' },
                    { field: 'Price', title: '金额', sortable: true, width: 60, align: 'center' },
                     { field: 'Status', title: '费用状态', sortable: true, width: 60, align: 'center' },
                    { field: 'Remark', title: '备注', editor: { type: 'XXX', options: { xx: 1, typex: 2} }, sortable: true, width: 120, align: 'center' },
                    { field: 'RR_RedMoney', title: '冲账金额', sortable: true, width: 60, align: 'center', editor: { type: 'numberbox', options: { precision: 2}} },
                    { field: 'RR_Remark', title: '冲账原因', editor: { type: 'text' }, sortable: true, width: 80 },
                    { field: 'QF_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存' onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt
                            }
                            else {
                                var actiontxt = "<input type='button' value='编辑' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />";

                                return actiontxt;
                            }
                        }
                    }
                ]],
        onBeforeEdit: function (rowIndex, rowData) {
            rowData.editing = true;
            $(divid).datagrid('refreshRow', rowIndex);
            currdatarow = rowData;
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;
            rowData.RR_FYID = rowData.AutoID;
            rowData.RR_OrderGuid = rowData.OrderGuid;
            $(divid).datagrid('refreshRow', rowIndex);

            AddOrUpdate(rowData);
        },
        onCancelEdit: function (rowIndex, rowData) {
            rowData.editing = false;
            $(divid).datagrid('refreshRow', rowIndex);
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
//展示编辑数据行
function EditorDataRow(tableid, rowindex, actionname, buttonid, buttontxt) {
    $(tableid).datagrid(actionname, rowindex);
    var editors = $(tableid).datagrid('getEditors', rowindex);
    var editorrow = $(tableid).datagrid('getRows');
    var moneyeditor = editors[0];
    if (moneyeditor != undefined) {
        moneyeditor.target.bind('blur', function () {
            CoputerMoney(tableid, moneyeditor, editorrow[rowindex]);
        });
    }
}
//计算冲账款金额
function CoputerMoney(divid, editor, rowdata) {
    var s = this;
    var allmoney = 0;
    var moneyeditor = editor;
    if (moneyeditor != undefined && moneyeditor != null) {
        if (rowdata.Price == null) {
            // moneyeditor.target.focus();
            // moneyeditor.editor('setValue', parseFloat(rowdata.Price));
            alert("冲账金额不能大于费用金额!");
        }
        if (parseFloat(rowdata.Price) < parseFloat(moneyeditor.target.val())) {
            //moneyeditor.oldHtml = parseFloat(rowdata.Price);
            moneyeditor.target.val(parseFloat(rowdata.Price));
            alert("冲账金额不能大于费用金额!");
        }
    }
}
//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().parent().parent().height() * 0.8;
    widthvalue = $(".easyui-tabs").parent().parent().parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $('#table_hongchongfeiyong').datagrid('resize', {
        height: heightvalue,
        width: widthvalue
    });
}
//查询红冲费用信息
function Selction(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "RR_OrderGuid": orderguid,"ID":rdid
    };
    var Urlstr = "../../ActionHanlder/CashChild/HongChongFeiYongHalder.ashx";
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
//增加红字冲账信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/HongChongFeiYongHalder.ashx";
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
function RefreshDataGrid() {
    Selction("#table_hongchongfeiyong", orderguid);
}
//#endregion
