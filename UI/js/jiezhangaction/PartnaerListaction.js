
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _feiyongbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var feiyonglist = null;
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId; //用于窗口改变时延时触发
var orderguid = "";
var jzid = "";
var unitid = "";
var addflag = false; //以判断是否新增表格行
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {


    //获取入住记录ID
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
        if (urlquerstr[1]) {
            jzid = urlquerstr[1].replace('JZID=', "");
        }
    }
    feiyonglist = window.parent.GetFeiYongList();

    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 20);
        }
    });
    //初始化相关表格
    IntializeTableDatagrid('#tab_partnerlist');
    IntializeFeiyongDatagrid("#tab_feiyongdetails");
    //查询相关数据明细
    SelPartnerList('#tab_partnerlist', orderguid);
    if (feiyonglist != null) {
        LoadFeiYongList('#tab_feiyongdetails', feiyonglist);
    }
    else {
        SelFeiYongList('#tab_feiyongdetails', orderguid);
    }
    //Selction('#tab_partnerlist', orderguid, title)
    //显示布局
    ViewIntialization();


    //关闭初始打开的遮盖层
    ajaxLoadEnd();
});
//#endregion

//#region 操作列表

function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $("#topdiv").parent().parent().height() * 0.5;
    widthvalue = $("#topdiv").parent().parent().width();
    $("#topdiv").panel({
        width: $("#topdiv").parent().parent().width(),
        height: heightvalue
    });
    //设置协议单位的长度和宽度
    $("#downdiv").panel({
        width: $("#topdiv").parent().parent().width(),
        height: heightvalue
    });

    $('#tab_partnerlist').datagrid('resize', { height: heightvalue * 0.9 }); ;
    $('#tab_feiyongdetails').datagrid('resize', { height: heightvalue * 0.90 });

}

//初始化每个选项卡相对应的表格
function IntializeTableDatagrid(divid) {
    //初始化表格效果
    $(divid).datagrid({
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'AutoID', title: 'ID', checkbox: true, width: 20 },
                    { field: 'Company', title: '协议单位', hidden: true, align: 'center' },
                    { field: 'ContactMan', title: '联系人', sortable: true, width: 80, align: 'center' },
                    { field: 'Telphone', title: '联系电话', sortable: true, width: 120, align: 'center' },
                    { field: 'IsCredit', title: '是否记帐', sortable: true, width: 60, align: 'center' },
                    { field: 'IsRetComm', title: '是否返佣', sortable: true, width: 60, align: 'center' },
                    { field: 'RetType', title: '返佣类型', sortable: true, width: 60, align: 'center' },
                    { field: 'CreditLevel', title: '挂账限额', sortable: true, width: 80, align: 'center' },
                    { field: 'CreditMoney', title: '挂账金额', editor: 'text', sortable: true, width: 120, align: 'center' },
                    { field: 'SaleMan', title: '销售员', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'Detail', title: '备注', sortable: true, width: 60, align: 'center' }
                ]],
        onSelect: function (rowIndex, rowData) {
            unitid = rowData.AutoID;
            SelRelationFeiYongListClick();
        }
    }).datagrid('getPager').pagination({
        pageSize: _feiyongbpage.pageSize,
        pageNumber: _feiyongbpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _feiyongbpage.pageNumber = pageNumber;
            _feiyongbpage.pageSize = pageSize;
            SelPartnerList(divid, orderguid);
        }
    });
}
function IntializeFeiyongDatagrid(divid) {
    //初始化表格效果
    $(divid).datagrid({
        fitColumns: true,
        singleSelect: false,
        remoteSort: false,      // 后台排序
        pagination: true,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'RZ_ID', checkbox: true, title: 'ID', width: 0 },
                    { field: 'RZ_OrderGuid', title: 'OrderID', width: 0, hidden: true },
                    { field: 'RZ_RoomNo', title: '房号', width: 60, align: 'center' },
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
        onSelect: function (rowIndex, rowData) {
           
        }
    }).datagrid('getPager').pagination({
        pageSize: _kbpage.pageSize,
        pageNumber: _kbpage.pageNumber,
        onSelectPage: function (pageNumber, pageSize) {
            _kbpage.pageNumber = pageNumber;
            _kbpage.pageSize = pageSize;
            SelRelationFeiYongListClick();
        }
    });
}

//展示编辑数据行
function EditorDataRow(tableid, rowindex, actionname, buttonid, buttontxt) {
    $(tableid).treegrid(actionname, rowindex);
}


//查询协议用户信息
function SelPartnerList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByPartnerList', "PageInfo": $.toJSON(_kbpage),
        "FP_OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
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
//查询费用信息
function SelFeiYongList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByFeiYongList', "PageInfo": $.toJSON(_feiyongbpage),
        "RZ_OrderGuid": orderguid, "JZID": jzid
    };
    var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   SelHaveExitFeiYongList(orderguid, data.rows, div);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//根据OrderID\UnitID\JZID查询相关已经挂账的费用信息
function SelRelationFeiYongList(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelOrderRelationFeiYongList', "PageInfo": $.toJSON(_kbpage),
        "RZ_OrderGuid": orderguid, "JZID": jzid, "UnitID": unitid
    };
    var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data.rows;
                   var rowcount = data.total;
                   $(divid).datagrid({ pagination: true }).datagrid('getPager').pagination({
                       pageSize: _kbpage.pageSize,
                       pageNumber: _kbpage.pageNumber,
                       onSelectPage: function (pageNumber, pageSize) {
                           _kbpage.pageNumber = pageNumber;
                           _kbpage.pageSize = pageSize;
                           SelRelationFeiYongListClick();
                       }
                   });;
                   $(divid).datagrid("loadData", { total: data.total, rows: dataArray });

               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//查询已经挂账的费用信息ID
function SelHaveExitFeiYongList(orderguid, compdata, divid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelHaveExitFeiYongList', "PageInfo": $.toJSON(_feiyongbpage),
        "RZ_OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   dataArray = data;
                   var tempdata = [];
                   var tvalue = false;
                   for (var i = 0; i < compdata.length; i++) {
                       for (var y = 0; y < data.total; y++) {
                           if (compdata[i].RZ_ID == data.rows[y].FeiYongID) {
                               tvalue = true;
                               break;
                           }
                       }
                       if (!tvalue) {
                           tempdata.push(compdata[i]);
                       }
                       tvalue = false;
                   }
                   $(divid).datagrid({ pagination: false });
                   $(divid).datagrid("loadData", { total: tempdata.length, rows: tempdata });
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

//当从部分结账时传递过来的费用信息，加载到费用表格中
function LoadFeiYongList(divid, feiyonglist) {
    //发送Post请求, 返回后执行回调函数.
    try {
        if (feiyonglist != null) {
            dataArray = feiyonglist.Datas;
            SelHaveExitFeiYongList(orderguid, dataArray, divid);
        }
    }
    catch (e) {
        alert(e);
    }

}
//确认挂账
function CreateRelation(divid, datadivid) {
    if ($("#selrelation_check")[0].checked) {
        alert("抱歉,当前费用记录已经挂账不能再进行挂，只能进行取消挂账操作！");
        return;
    }
    var unitSelRow = $(divid).datagrid("getChecked");   
    if (unitSelRow.length > 0) {
        var selfeiyonglist = $(datadivid).datagrid("getChecked");

        var resultdata = { "unit": unitSelRow[0], "rows": selfeiyonglist };
        if (selfeiyonglist.length <= 0) {
            alert("请选中费用记录!");
            return;
        }
        //发送Post请求, 返回后执行回调函数.
        var paramdata = { "ActionName": 'CreateRelation', "OrderID": orderguid, "JZID": jzid, "UnitID": unitSelRow[0].AutoID, "Rows": $.toJSON(selfeiyonglist) };
        var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
        $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data.Flag) {
                       window.parent.SetUnitMoney(data.value);
                       alert("执行成功!");
                   }
                   else {
                       alert("执行失败!" + data.Message);
                   }
               }
               catch (e) {
                   alert(e);
               }
              
               //重新刷新费用记录，排除掉已经挂账的信息
               if (feiyonglist != null) {
                   LoadFeiYongList('#tab_feiyongdetails', feiyonglist);
               }
               else {
                   SelFeiYongList('#tab_feiyongdetails', orderguid);
               }
               CloseMaskLayer(window.document.body);
           },
         "json");
        ShopMaskLayer(window.document.body, "正在处理数量，请稍等……");
    }
    else {
        alert("请选中挂账单位！");
    }
}

function DeleteRelation(divid, datadivid) {
    if (!$("#selrelation_check")[0].checked) {
        alert("抱歉,当前费用记录还没有挂账，只能进行确认挂账操作！");
        return;
    }
    var unitSelRow = $(divid).datagrid("getChecked");
    if (unitSelRow.length > 0) {
        var feiyonglist = $(datadivid).datagrid("getChecked");

        var resultdata = { "unit": unitSelRow[0], "rows": feiyonglist };
        if (feiyonglist.length <= 0) {
            alert("请选中费用记录!");
            return;
        }
        //发送Post请求, 返回后执行回调函数.
        var paramdata = { "ActionName": 'DeleteRelation', "OrderID": orderguid, "JZID": jzid, "UnitID": unitSelRow[0].AutoID, "Rows": $.toJSON(feiyonglist) };
        var Urlstr = "../../ActionHanlder/CashChild/PartnerListHalder.ashx";
        $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data.Flag) {
                       window.parent.SetUnitMoney(data.value);
                       alert("执行成功!");
                   }
                   else {
                       alert("执行失败!" + data.Message);
                   }
               }
               catch (e) {
                   alert(e);
               }
               SelRelationFeiYongListClick();
               CloseMaskLayer(window.document.body);
           },
         "json");
        ShopMaskLayer(window.document.body, "正在处理数量，请稍等……");
    }
    else {
        alert("请选中挂账单位！");
    }
}
//根据选项进行查询相关数据
function SelRelationFeiYongListClick() {
    if ($("#selrelation_check")[0].checked) {
        SelRelationFeiYongList("#tab_feiyongdetails", orderguid);
    }
    else {
        if (feiyonglist != null) {
            LoadFeiYongList('#tab_feiyongdetails', feiyonglist);
        }
        else {
            SelFeiYongList('#tab_feiyongdetails', orderguid);
        }
    }
}
//刷新查询
function RefreshDataGrid() {
    //查询相关数据明细
    SelPartnerList('#tab_partnerlist', orderguid);
    SelFeiYongList('#tab_feiyongdetails', orderguid);
}
//#endregion
