
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _searchpage =     // 客户用品分页对象
{
sortField: 'GoodNo',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var currdatagrid = "";
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var orderguid = "";
var fanhao = "";
var addflag = false; //以判断是否新增表格行
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

    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    //选项卡事件
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "布件":
                    IntializeTableDatagrid('#table_bujiang');
                    Selction('#table_bujiang', orderguid, title);
                    currdatagrid = "table_bujiang";
                    break;
                case "房内用品":
                    IntializeTableDatagrid('#table_fanneiyongbing');
                    Selction('#table_fanneiyongbing', orderguid, title);
                    currdatagrid = "table_fanneiyongbing";
                    break;
                case "污染烟烫":
                    IntializeTableDatagrid('#table_wuyangyongbing');
                    Selction('#table_wuyangyongbing', orderguid, title);
                    currdatagrid = "table_wuyangyongbing";
                    break;
                case "其他":
                    IntializeTableDatagrid('#table_qita');
                    Selction('#table_qita', orderguid, title);
                    currdatagrid = "table_qita";
                    break;
            }
        }
    });

    //关闭初始打开的遮盖层
    ajaxLoadEnd();
    ViewIntialization();
    //初始化自定义编辑器
    CumstomerEditor();
    if (rzid != "" && rzid != "undefined") {
        var txts = rzid.split(';');
        rzid = txts[0];
        tabpanelindex = txts[1];
        $('.easyui-tabs').tabs('select', tabpanelindex * 1);
    }
});
//#endregion

//#region 操作列表
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid(divid) {
    //初始化表格效果
    $(divid).datagrid({
        height: 500,
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'ID', title: 'ID', width: 80, hidden: true },
                    { field: 'SW_ProduceCode', title: '商品代码',
                        sortable: true,
                        width: 80,
                        align: 'center'
                    },
                    { field: 'SW_ProduceName', title: '商品名称', editor: { type: 'SerCombox', options: { required: true} }, sortable: true, width: 120, align: 'center' },
                    { field: 'SW_FeiyongType', title: '费用种类', sortable: true, width: 80, align: 'center' },
                    { field: 'SW_UnitPrice', title: '额定单价', sortable: true, width: 80 },
                    { field: 'SW_Quantity', title: '数量', editor: { type: 'numberbox',
                        options: { required: true}
                    }, sortable: true, width: 60, align: 'center'
                    },
                    { field: 'SW_Money', title: '金额', sortable: true, width: 60, align: 'center' },
                    { field: 'SW_Remark', title: '备注', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'CreateTime', title: '发生日期', sortable: true, width: 120, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'SW_RooMNum', title: '房间号', sortable: true, width: 60, align: 'center' },
                    { field: 'SW_Status', title: '状态', sortable: true, width: 60, align: 'center' },
                    { field: 'SW_Operator', title: '操作人', sortable: true, width: 60, align: 'center' },
                    { field: 'SW_Editor', title: '编辑', sortable: true, width: 80, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存' onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt
                            }
                            else {
                                if (row.SW_ProduceCode == "" || row.SW_ProduceCode == null) {
                                    if (rzid == "") {
                                        return "<input type='button' value='增加' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                    }
                                    else {
                                        return "";
                                    }
                                }
                                else {
                                    if (row.SW_Status != "结账") {
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
            rowData.SW_RooMNum = fanhao;
            $(divid).datagrid('refreshRow', rowIndex);
            if (rowData.SW_ProduceCode == null) {
                addflag = true;
            }
            else {
                addflag = false;
            }
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;
            rowData.SW_ProduceCode = rowData.SW_ProduceName.GoodsNo;
            rowData.SW_UnitPrice = rowData.SW_ProduceName.SalePrice;
            rowData.SW_Money = (parseFloat(rowData.SW_UnitPrice) * parseFloat(rowData.SW_Quantity)).toFixed(2);
            rowData.SW_ProduceName = rowData.SW_ProduceName.GoodsName;
            var r = $('.easyui-tabs').tabs('getSelected').panel('options').tab;

            rowData.SW_FeiyongType = r.text();
            $(divid).datagrid('refreshRow', rowIndex);
            rowData.SW_OrderGuid = orderguid;
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
    copydata.SW_ProduceCode = null;
    $(tableid).datagrid('appendRow', copydata);
}


//查询客户用品信息
function Selction(divid, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "SW_OrderGuid": orderguid, "SW_FeiyongType": FeiyongType, "ID": rzid
    };
    $.messager.progress({ text: "正在查询数据，请等候……" });
    var Urlstr = "../../ActionHanlder/CashChild/SunWuPeiChangHalder.ashx";
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
               $.messager.progress('close');
           },
         "json");
}

//查询商品信息
function SearchGoods(seardivid, search, issel) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelGoodsByPY', "PageInfo": $.toJSON(_searchpage), "GoodsName": search };
    var Urlstr = "../../ActionHanlder/CashChild/KeFanFeiYongHalder.ashx";
    $.messager.progress({ text: "正在查询数据，请等候……" });
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               $.messager.progress('close');
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   $(seardivid).combogrid("grid").datagrid("loadData", dataArray);
                   if (issel) {
                       $(seardivid).combogrid("grid").datagrid("selectRow", 0);
                   }
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
    var Urlstr = "../../ActionHanlder/CashChild/SunWuPeiChangHalder.ashx";
    $.messager.progress({ text: "正在处理数据，请等候……" });
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
               $.messager.progress('close');
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
        var Urlstr = "../../ActionHanlder/CashChild/SunWuPeiChangHalder.ashx";
        $.messager.progress({ text: "正在处理数据，请等候……" });
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
               $.messager.progress('close');
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
    var p = $(currtableid).datagrid('resize', { width: widthvalue, height: heightvalue * 0.79 });
}

//自定义编辑器
function CumstomerEditor() {
    var isSelFlag = true; //判断是否记录的输入
    $.extend($.fn.datagrid.defaults.editors, {
        SerCombox: {
            init: function (container, options) {
                var widthvalue = $(container).parent().parent().width();
                var input = $('<input id="goods" name="goods" stype="width:80px;"  >').appendTo(container);
                input = input.combogrid({
                    width: widthvalue,
                    panelWidth: 350,
                    idField: 'GoodsNo',
                    textField: 'GoodsName',
                    columns: [[
                     { field: 'GoodsNo', title: '商品编码', width: 60 },
                     { field: 'GoodsName', title: '商品名称', width: 100 },
                     { field: 'GoodsSimple', title: '商品简称', width: 80 },
                     { field: 'SalePrice', title: '单价', width: 80 },
                     { field: 'MakeUnit', title: '单位', width: 80 },
                     { field: 'GoodsStyle', title: '商品样式', width: 80 },
                     { field: 'GoodsType', title: '商品类别', width: 80 }
                     ]],
                    onChange: function (newValue, oldValue) {
                        if (isSelFlag) {
                            SearchGoods("#goods", newValue);
                        }
                        isSelFlag = true;
                    }
                    , onClickCell: function (rowIndex, field, value) {
                        isSelFlag = false;
                    },
                    onHidePanel: function () {

                    }
                });
                return input;
            },
            destroy: function (target) {
                $(target).remove();
            },
            getValue: function (target) {
                var va = $(target).combogrid('grid').datagrid('getSelected');
                if (va) {
                    return va;
                }
                else {
                    return "";
                }
            },
            setValue: function (target, value) {
                isSelFlag = false;
                SearchGoods("#goods", value, true);
                $(target).val(value);

            },
            resize: function (target, width) {
                $(target)._outerWidth(width);
            }
        }
    });
}
//#endregion
