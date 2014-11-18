
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var resizeTimeoutId = null;
var currdatagrid = "";
var orderguid = "";
var addflag = false; //以判断是否新增表格行
var KeMuList = [];
var fanhao = "";
var rzid = "";
var tabpanelindex = 0;
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
        if (urlquerstr[2] != undefined && urlquerstr[2] != null) {
            rzid = urlquerstr[2].replace('id=', "");
            if (rzid == "undefined") {
                rzid = "";
            }
        }
    }
    SelKeMuaction();

    if (rzid != "" && rzid != "undefined") {
        var txts = rzid.split(';');
        rzid = txts[0];
        tabpanelindex = txts[1];
        $('.easyui-tabs').tabs('select', tabpanelindex * 1);
    }
    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });


    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "明细":
                    IntializeTableDatagrid('#table_qita');
                    Selction('#table_qita', orderguid, title)
                    currdatagrid = "table_qita";
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
                     { field: 'QF_OrderGUID', title: 'OrderGUID', hidden: true },
                     { field: 'QF_FeiYongCode', title: '费用代码', sortable: true, width: 80, align: 'center' },
                    { field: 'QF_FeiYongName', title: '费用名称', editor: { type: 'combobox',
                        options: { data: KeMuList, valueField: "value", textField: "text", required: true }
                    }, sortable: true, width: 120, align: 'center'
                    },
                    { field: 'QF_Receipt', title: '单据号码', editor: 'text', sortable: true, width: 80 },
                    { field: 'QF_Money', title: '金额', sortable: true, editor: { type: 'numberbox', options: { required: true} }, width: 60, align: 'center' },
                    { field: 'QF_Remark', title: '备注', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'QF_OccurrDateTime', title: '发生日期', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'QF_Room', title: '房间号', sortable: true, width: 60, align: 'center' },
                    { field: 'QF_Customer', title: '客户', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'CreateUser', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'QF_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存' onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt;
                            }
                            else {
                                if (row.QF_FeiYongCode == "" || row.QF_FeiYongCode == null) {
                                    if (rzid == "") {
                                        return "<input type='button' value='增加' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                    }
                                    else {
                                        return "";
                                    }
                                }
                                else {
                                    if (row.QF_Status != "结账") {
                                        var actiontxt = "<input type='button' value='编辑' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                        actiontxt = actiontxt + "<input type='button' value='删除' onclick=DeleteRow('" + divid + "'," + index + ") />"
                                        return actiontxt;
                                    }
                                    else {
                                        return "";
                                    }
                                }
                            }
                        }
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onBeforeEdit: function (rowIndex, rowData) {
            rowData.editing = true;
            rowData.QF_Room = fanhao;
            $(divid).datagrid('refreshRow', rowIndex);
            if (rowData.QF_FeiyongCode == null) {
                addflag = true;
            }
            else {
                addflag = false;
            }
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;
            rowData.QF_FeiYongCode = rowData.QF_FeiYongName;
            rowData.QF_FeiYongName = KeMuList[parseInt(rowData.QF_FeiYongName) - 1].text;
            $(divid).datagrid('refreshRow', rowIndex);
            rowData.QF_OrderGUID = orderguid;
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


//查询用品信息
function Selction(divid, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "QF_OrderGUID": orderguid, "ID": rzid
    };
    var Urlstr = "../../ActionHanlder/CashChild/QiTaHalder.ashx";
    $.messager.progress({ text: "正在查询数据，请等候……" });
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               $.messager.progress('close');
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
//查询费用科目信息
function SelKeMuaction() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelKeMuList' };
    var Urlstr = "../../ActionHanlder/CashChild/QiTaHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            KeMuList = [];
            if (data) {
                for (var i = 0; i < data.length; i++) {
                    KeMuList.push({ "value": data[i].ID, "text": data[i].KM });
                }
            }
        }
    });
}
//增加其它费用信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/QiTaHalder.ashx";
    $.messager.progress({ text: "正在处理数据，请等候……" });
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               $.messager.progress('close');
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

//删除客户用品信息
function DeleteRow(datagrid, rowindex) {
    //发送Post请求, 返回后执行回调函数.
    var rows = $(datagrid).datagrid('getRows');
    if (rows != null) {
        var data = rows[rowindex];
        data.ActionName = 'Delete';
        var paramdata = data;
        var Urlstr = "../../ActionHanlder/CashChild/QiTaHalder.ashx";
        $.messager.progress({ text: "正在处理数据，请等候……" });
        $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               $.messager.progress('close');
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
    $(".easyui-tabs").tabs('getSelected').panel('resize', { width: widthvalue, height: heightvalue });
    var currtableid = "#" + currdatagrid;
    var p = $(currtableid).datagrid('resize', { width: widthvalue, height: heightvalue * 0.93 });
}
//#endregion
