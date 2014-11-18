
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 10
}
var _temppage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 10000
}
var oldtab = 0;
var orderguid = "";
var fanhao = "";
var neworderguid = "";
var addflag = false; //以判断是否新增表格行
var Address = [{ "value": "1", "text": "CHINA" }, { "value": "2", "text": "USA" }, { "value": "3", "text": "Koren" }, { "value": "4", "text": "饮食"}];
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
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            neworderguid = "";
            if (index != oldtab) {
                _kbpage.pageNumber = 1;
                oldtab = index;
            }
            switch (title) {
                case "按项目转帐":
                    IntializeTableDatagrid('#tab_zhuangzhangbypro');
                    Selction('#tab_zhuangzhangbypro', "SelFeiyongList", orderguid, title);
                    $("#allfeiyongroom").val("");
                    break;
                case "收退款转账":
                    IntializeTableDatagrid_ShouKuan('#tab_shoutuikuan');
                    Selction('#tab_shoutuikuan', "SelShouKuanList", orderguid, title);
                    $("").val("#shoutukuanroom");
                    break;
                case "已转入费用转出":
                    IntializeTableDatagrid('#tab_zhuangruzhuangchu');
                    Selction('#tab_zhuangruzhuangchu', "SelZhuanRuFeiYongList", orderguid, title);
                    $("").val("#zhuangchuroom");
                    break;
                case "取消转帐":
                    IntializeTableDatagrid('#tab_cancezhuangzhang');
                    Selction('#tab_cancezhuangzhang', "SelZhuangChuFeiYongList", orderguid, title);
                    $("").val("#zhuanruroom");
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
        height: $('.easyui-tabs').parent().height() * 0.68,
        fitColumns: true,
        singleSelect: false,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'RZ_ID', title: 'ID', width: 80, checkbox: true },
                     { field: 'OrderGUID', title: 'OrderGUID', hidden: true },
                     { field: 'RZ_RoomNo', title: '房间号',
                         editor: { type: 'combobox',
                             options: { data: Address, valueField: "value", textField: "text", required: true }
                         },
                         sortable: true,
                         width: 80,
                         align: 'center'
                     },
                    { field: 'RZ_FYType', title: '费用类别', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiyongType', title: '费用科目', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiYongName', title: '物件名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_Money', title: '金额', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_RunnTime', title: '发生时间', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        } 
                    },
                    { field: 'RZ_Customer', title: '客户', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'RZ_Remark', title: '客单备注', sortable: true, width: 60, align: 'center' }
                ]],
        onHeaderContextMenu: function (e, field) {

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
//初始化每个选项卡相对应的表格
function IntializeTableDatagrid_ShouKuan(divid) {
    //初始化表格效果
    $(divid).datagrid({
        height: $('.easyui-tabs').parent().height() * 0.68,
        fitColumns: true,
        singleSelect: false,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'ID', title: 'ID', width: 80, checkbox: true },
                     { field: 'SK_OrderGUID', title: 'OrderGUID', hidden: true },
                     { field: 'SK_PayType', title: '付款方式', sortable: true, width: 80, align: 'center' },
                    { field: 'SK_Type', title: '收退款类型', sortable: true, width: 120, align: 'center' },
                    { field: 'SK_Money', title: '金额', sortable: true, width: 60, align: 'center' },
                    { field: 'SK_PayTime', title: '发生时间', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        } 
                    },
                    { field: 'CreateUser', title: '操作人', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'SK_Remark', title: '客单备注', sortable: true, width: 60, align: 'center' }
                ]],
        onHeaderContextMenu: function (e, field) {

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


//查询客户用品信息
function Selction(divid, actionname, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": actionname, "PageInfo": $.toJSON(_kbpage),
        "RZ_OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/SanKenZhuanZhangHalder.ashx";
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

function openSelRoomsWindow(roomtxtid) {
    //初始化房间窗口界面
    var divwidth = $('.easyui-layout').width() * 0.5;
    var divheight = $('.easyui-layout').height() * 0.5;
    $("#fanjianarea").dialog({
        title: '房间列表',
        width: divwidth,
        height: divheight,
        closed: false,
        cache: false,
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        modal: true,
        onBeforeOpen: function () {
            ShopMaskLayerWipe("#fanjianarea", "正在加载数据……", divwidth, divheight, null, null);
            SelRoomsAction(roomtxtid);

        },
        onBeforeClose: function () {

        }
    });
}
//查询目前入住房间信息
function SelRoomsAction(roomtxtid) {

    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelOrderRoomList', "PageInfo": $.toJSON(_temppage), "RZ_OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/SanKenZhuanZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   //形成表格
                   FormatRoom(data.rows, roomtxtid);
                   CloseMaskLayer("#fanjianarea");
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
function FormatRoom(datarows, roomtxtid) {
    $("#fanjianarea").find("table").remove();
    var tablestr = "<table>";
    var markvalue = 0;
    for (var i = 1; i <= datarows.length; i++) {
        if (i % 3 == 0 && (i - markvalue) == 3) {
            tablestr = tablestr + "<td><input type=\"radio\" title='" + datarows[i - 1].FangJianHao + "' name=\"rooms\" value='"
                         + datarows[i - 1].OrderGuid + "'/>" + datarows[i - 1].FangJianHao + "</td>"
            tablestr = tablestr + "</tr>"
        }
        else {
            if ((i - 1) % 3 == 0) {
                tablestr = tablestr + "<tr>";
                markvalue = i - 1;
            }
            tablestr = tablestr + "<td><input type=\"radio\" title='" + datarows[i - 1].FangJianHao + "' name=\"rooms\" value='"
                         + datarows[i - 1].OrderGuid + "'/>" + datarows[i - 1].FangJianHao + "</td>"
        }
    }
    if (datarows.length % 3 == 0) {
        //增加按钮
        tablestr = tablestr + "<tr style='height:80px;'><td><input type='Button'   onclick=\"getSelRoom('" + roomtxtid + "');\"  value='确 定'/></td>";
        tablestr = tablestr + "<td><input type='Button'  onclick=\"CanceSelRoom('" + roomtxtid + "');\" value='取 消'/></td></tr>";
        tablestr = tablestr + "</table>"
    }
    else {
        tablestr = tablestr + "<td></td></tr>"
        tablestr = tablestr + "<tr style='height:80px;'><td><input type='Button'   onclick=\"getSelRoom('" + roomtxtid + "');\" value='确 定'/></td>";
        tablestr = tablestr + "<td><input type='Button'  onclick=\"CanceSelRoom('" + roomtxtid + "');\" value='取 消'/></td></tr></table>";
    }

    $("#fanjianarea").css("display", "block").append(tablestr);

}
function getSelRoom(roomtxtid) {
    var selradio = $('input[name="rooms"]').filter(':checked');
    neworderguid = $(selradio).val();
    var fanjianhao = $(selradio).attr('title');
    $("#fanjianarea").dialog('close');
    $(roomtxtid).val(fanjianhao);

}
function CanceSelRoom(roomtxtid) {
    neworderguid = "";
    var fanjianhao = "";
    $(roomtxtid).val("");
    $("#fanjianarea").dialog('close');
}

//按项目费用转出
function FeiYongZhuanChu() {
    if (neworderguid != "" && neworderguid != undefined) {
        $.messager.confirm('Confirm', '确定进行转出操作?', function (flag) {
            if (flag) {

                var datarows = $('#tab_zhuangzhangbypro').datagrid('getChecked');
                if (datarows.length > 0) {
                    $.messager.progress({ text: "正在处理数据，请等候……" });
                    AddOrUpdate(datarows, "FeiYongZhuanChu");
                }
                else {
                    $.messager.alert('提醒', '请勾选需要转出的费用明细', 'info');
                }
            }
        });
    }
    else {
        $.messager.alert('提醒', '请先选择转入的房间号', 'info');

    }
}
//收退款转出
function ShouTuKuanZhuanChu() {
    if (neworderguid != "" && neworderguid != undefined) {
        $.messager.confirm('Confirm', '收退款一经转出便不能再取消，请确定是否进行转出操作?', function (flag) {
            if (flag) {
                var datarows = $('#tab_shoutuikuan').datagrid('getChecked');
                if (datarows.length > 0) {
                    $.messager.progress({ text: "正在处理数据，请等候……" });
                    AddOrUpdate(datarows, "ShouKuanZhuanChu");
                }
                else {
                    $.messager.alert('提醒', '请勾选需要转出的费用明细', 'info');
                }
            }
        });
    }
    else {
        $.messager.alert('提醒', '请先选择转入的房间号', 'info');

    }
}
//转入费用转出
function ZhuangRuFeiYongZhuanChu() {
    if (neworderguid != "" && neworderguid != undefined) {
        $.messager.confirm('Confirm', '确定进行转出操作?', function (flag) {
            if (flag) {

                var datarows = $('#tab_zhuangruzhuangchu').datagrid('getChecked');
                if (datarows.length > 0) {
                    $.messager.progress({ text: "正在处理数据，请等候……" });
                    AddOrUpdate(datarows, "FeiYongZhuanChu");
                }
                else {
                    $.messager.alert('提醒', '请勾选需要转出的费用明细', 'info');
                }
            }
        });
    }
    else {
        $.messager.alert('提醒', '请先选择转入的房间号', 'info');

    }
}
//转出费用取消
function ZhuanChuFeiYongCance() {
    neworderguid = orderguid;
    if (neworderguid != "" && neworderguid != undefined) {
        $.messager.confirm('Confirm', '确定取消转账操作?', function (flag) {
            if (flag) {

                var datarows = $('#tab_cancezhuangzhang').datagrid('getChecked');
                if (datarows.length > 0) {
                    $.messager.progress({ text: "正在处理数据，请等候……" });
                    AddOrUpdate(datarows, "FeiYongZhuanChu");
                }
                else {
                    $.messager.alert('提醒', '请勾选需要转出的费用明细', 'info');
                }
            }
        });
    }
    else {
        $.messager.alert('提醒', '请先选择转入的房间号', 'info');

    }
}
//费用转出更新
function AddOrUpdate(data, actinname) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = new Object();
    paramdata.ActionName = actinname;
    paramdata.NewOrderGuid = neworderguid;
    paramdata.DataRows = $.toJSON(data);
    paramdata.OrderGuid = orderguid;
    var Urlstr = "../../ActionHanlder/CashChild/SanKenZhuanZhangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   $.messager.progress({ text: data.Message });
               }
               catch (e) {
                   alert(e);
               }
               $.messager.progress('close');
               RefreshDataGrid();
           },
         "json");
}

function allchoosed(divid) {
    $(divid).datagrid("checkAll");
}
function allcancechoosed(divid) {
    $(divid).datagrid("uncheckAll");
}

//刷新页面表格数据
function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);

}
//#endregion
