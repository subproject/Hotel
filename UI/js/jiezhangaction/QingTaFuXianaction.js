
//#region 共用变更

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
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var orderguid = "";
var addflag = false; //以判断是否新增表格行
var PayLiST = [];
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
    SelctPayType();
    //初始化数据
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "明细":                   
                    IntializeTableDatagrid('#tab_qiangtafuxian');
                    Selction('#tab_qiangtafuxian', orderguid, title)
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
        height: $('.easyui-tabs').parent().height() * 0.95,
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'ID', title: 'ID', width: 80, hidden: true },
                     { field: 'QX_OrderGUID', title: 'OrderGUID', hidden: true },
                     { field: 'QX_PayUser', title: '支付人', editor: { type: 'validatebox', options: { required: true} },
                         sortable: true, width: 80, align: 'center'
                     },
                    { field: 'QX_PayType', title: '支付类型',
                        editor: { type: 'combobox', options: { data: PayLiST, valueField: "text", textField: "text", required: true }
                        },
                        sortable: true,
                        width: 80,
                        align: 'center'
                    },
                    { field: 'QX_Receipt', title: '单据号码', editor: 'text', sortable: true, width: 80 },
                    { field: 'QX_PayMoney', title: '金额', sortable: true, editor: 'numberbox', width: 60, align: 'center' },
                    { field: 'QF_Remark', title: '备注', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'QX_Room', title: '房间号',  sortable: true, width: 60, align: 'center' },
                    { field: 'QX_Customer', title: '客户', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'CreateUser', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'QX_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存' onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt
                            }
                            else {
                                if (row.QX_PayType == "" || row.QX_PayType == null) {
                                    return "<input type='button' value='增加' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                }
                                else {
                                    var actiontxt = "<input type='button' value='编辑' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                    actiontxt = actiontxt + "<input type='button' value='删除' onclick=DeleteRow('" + divid + "'," + index + ") />"
                                    return actiontxt;
                                }
                            }
                        }
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onBeforeEdit: function (rowIndex, rowData) {
            rowData.editing = true;
            rowData.QX_Room = fanhao;
            //rowData.QX_PayType = PayLiST[parseInt(rowData.QX_PayType)].text;
            $(divid).datagrid('refreshRow', rowIndex);
            if (rowData.QX_PayType == null) {
                addflag = true;
            }
            else {
                addflag = false;
            }
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;
            for (var i = 0; i < PayLiST.length; i++) {
                if (PayLiST[i].text == rowData.QX_PayType) {
                    rowData.QX_PayTypeID = PayLiST[i].id;
                    break;
                }
            }
            $(divid).datagrid('refreshRow', rowIndex);
            rowData.QX_OrderGUID = orderguid;
            AddOrUpdate(rowData);
            //在界面多增加一行以便录入更多数据
            if (addflag) {
                AddDataRow(divid, rowData);
                addflag = false;
            }
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
    $(tableid).treegrid(actionname, rowindex);
}
//展示编辑数据行
function AddDataRow(tableid, data) {
    var copydata = {};
    ArrayCopye(copydata, data);
    copydata.QX_PayUser = null;
    $(tableid).datagrid('appendRow', copydata);
}


//查询客户用品信息
function Selction(divid, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "QX_OrderGUID": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/QingTaFuXianHalder.ashx";
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

//增加客户用品信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/QingTaFuXianHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   RefreshDataGrid();
                   alert(data.Message);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

//删除客户用品信息
function DeleteRow(datagrid, rowindex) {
    //发送Post请求, 返回后执行回调函数.
    var rows = $(datagrid).datagrid('getRows');
    if (rows != null) {
        var data = rows[rowindex];
        data.ActionName = 'Delete';
        var paramdata = data;
        var Urlstr = "../../ActionHanlder/CashChild/QingTaFuXianHalder.ashx";
        $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   //重新刷新数据表格
                   RefreshDataGrid();
                   alert(data.Message);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
    }
}

function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
}

//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().parent().height();
    widthvalue = $(".easyui-tabs").parent().parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $('#tab_qiangtafuxian').datagrid('resize', { height: heightvalue * 0.9, width: widthvalue });

}
//查询前台付现方式信息
function SelctPayType() {
    //发送Post请求, 返回后执行回调函数.
    _kcpage.pageNumber=1;
    _kcpage.pageSize=1000;
    var paramdata = { "ActionName": 'SelQianTaiPayTypes', "PageInfo": $.toJSON(_kcpage) };
    var Urlstr = "../../ActionHanlder/CashChild/QingTaFuXianHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            PayLiST = [];
            for (var i = 0; i < data.rows.length; i++) {
                var item = new Object();
                item.id = data.rows[i].id;
                item.text = data.rows[i].hxfsName;
                PayLiST.push(item);
            }
        }
    });
}
//#endregion
