
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
                    IntializeTableDatagrid('#tab_fapiaolist');
                    Selction('#tab_fapiaolist', orderguid, title)
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
                     { field: 'FP_OrderGuid', title: 'OrderGUID', hidden: true },
                     { field: 'FP_Customer', title: '客户名称', sortable: true, width: 80, align: 'center' },
                    { field: 'FP_Account', title: '账号', sortable: true, width: 120, align: 'center' },
                    { field: 'FP_FP_ConsumMoney', title: '消费总金额', sortable: true, width: 60, align: 'center' }  ,
                    { field: 'FP_Money', title: '收款金额', sortable: true, width: 60, align: 'center'     }  ,
                    { field: 'FP_ReceiptMoney', title: '发票金额', sortable: true, width: 60, align: 'center',
                        editor: { type: 'numberbox', options: { precision: 2} }},
                     { field: 'FP_Receipt', title: '发票号码', editor: { type: 'text', options: { required: true} }, sortable: true, width: 80 },
                    { field: 'QF_Remark', title: '备注', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'FP_Room', title: '房间号',  sortable: true, width: 60, align: 'center' },
                    { field: 'CreateUser', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'QF_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存' onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt
                            }
                            else {
                                if (row.FP_Receipt == "" || row.FP_Receipt == null) {
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
            rowData.FP_Room = fanhao;
            $(divid).datagrid('refreshRow', rowIndex);
            if (rowData.FP_Receipt == null) {
                addflag = true;
            }
            else {
                addflag = false;
            }
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;
            $(divid).datagrid('refreshRow', rowIndex);
            rowData.FP_OrderGuid = orderguid;
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
    copydata.QF_FeiYongCode = null;
    $(tableid).datagrid('appendRow', copydata);
}


//查询发票信息
function Selction(divid, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "FP_OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/FaPiaoManagerHalder.ashx";
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
    var Urlstr = "../../ActionHanlder/CashChild/FaPiaoManagerHalder.ashx";
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
        var Urlstr = "../../ActionHanlder/CashChild/FaPiaoManagerHalder.ashx";
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
    heightvalue = $(".easyui-tabs").parent().height();
    widthvalue = $(".easyui-tabs").parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $('#tab_fapiaolist').datagrid('resize', { height: heightvalue * 0.9, width: widthvalue });

}
//#endregion
