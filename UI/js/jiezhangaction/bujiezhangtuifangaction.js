
//#region 共用变更

var _kapage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var recordID = "";
var orderguid = "";
var currdata = {};
var addflag = false; //以判断是否新增表格行
var paytype = [];
var fanhao = "";
var resizeTimeoutId = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    //获取入住记录ID
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
        recordID = urlquerstr[1];
        if (urlquerstr.length <= 2) {
            fanhao = urlquerstr[1];
            fanhao = fanhao.replace('fh=', "");
        }
        else {
            fanhao = urlquerstr[2];
            fanhao = fanhao.replace('fh=', "");
        }
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
                case "房间明细":
                    IntializeTableDatagrid('#table_kehuyongbing');
                    Selction('#table_kehuyongbing', orderguid);
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
    ViewIntialization();
});
//#endregion

//#region 操作列表
//初始化表格形式
function IntializeTableDatagrid(tableid) {
    $(tableid).datagrid({
        rownumbers: true,
        animate: true,
        collapsible: true,
        fitColumns: true,
        pagination: true,
        showFooter: true,
        columns: [[
                    { field: 'AutoID', checkbox: true, title: '', width: 150, align: 'center' },
                    { field: 'FangJianHao', title: '房间号', width: 80, align: 'center' },
                    { field: 'XingMing', title: '姓名', width: 80, align: 'center' },
                    { field: 'XingBie', title: '性别', width: 60, align: 'center' },
                    { field: 'ZhengjianLeixing', title: '证件类型', width: 60, align: 'center' },
                    { field: 'ZhengjianHaoma', title: '证件号', width: 60, align: 'center' },
                    { field: 'JiBie', title: '房间类型', width: 60, align: 'center' },
                    { field: 'Status', title: '状态', width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value == "1") {
                                return "退房";
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'ZhuCong', title: '订房类型', width: 60, align: 'center' },
                    { field: 'ArriveTime', title: '到店时间', width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        }
                    },
                    { field: 'LeaveTime', title: '离店时间', width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        }
                    }
                ]],
        rowStyler: function (index, row) {
            if (row.Status == "1") {
                return 'background-color:red;';
            }
        },
        onCheck: function (rowIndex, rowData) {
            if (rowData.Status == "1") {
                $(tableid).datagrid('uncheckRow', rowIndex);

            }
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
}
//不结账退房
function BuJieZhangTuiKuan() {
    $.messager.confirm('Confirm', '确定要进行不结账退房操作?', function (r) {
        if (r) {
            var roomdata = $("#table_kehuyongbing").datagrid("getChecked");
            if (roomdata.length > 0) {
                var rooms = "";
                for (var i = 0; i < roomdata.length; i++) {
                    rooms += roomdata[i].FangJianHao + ",";
                }

                //发送Post请求, 返回后执行回调函数.
                var paramdata = { "ActionName": 'BuJieZhangTuiFan', "OrderGUID": orderguid,
                    "rooms": rooms
                };
                $.messager.progress({ text: "正在处理，请等候……" });
                var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
                $.post(
                       Urlstr,
                       paramdata,
                       function (data, textStatus) {
                           try {
                               if (data.Flag) {
                                   alert("执行成功!");
                               }
                               else {
                                   alert(data.Message);
                               }
                           }
                           catch (e) {
                               alert(e);
                           }
                           $.messager.progress('close');
                           RefreshDataGrid();
                       },
                "json");
            }
            else {
                alert("请选择相关房间信息");
            }

        }
    });
}
//查询入住相关房间信息
function Selction(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelRoomList', "PageInfo": $.toJSON(_kapage),
        "OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
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
function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
}

//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-tabs").parent().parent().parent().height();
    widthvalue = $(".easyui-tabs").parent().parent().parent().width();
    $("#t1").parent().parent().css("width", widthvalue);
    $("#t1").parent().css("width", widthvalue);
    $('#table_kehuyongbing').datagrid("resize", { height: heightvalue * 0.89, width: widthvalue });
}
//#endregion
