
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _abpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var _cbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var datagridid = "#tab_shoudatadetails";
var MainguidValue = "";
var heightvalue = 0;
var widthvalue = 0;
var resizeTimeoutId = 0;
var unitrow = null;
var feilistdata = new Object();
var savetype = "";
var mainEntiry = new Object();
var orderguid = "";
var addflag = false; //以判断是否新增表格行
var isnewadd = false;
var isyajing = "";
var id = "";
var isviewid = false;
var isendjiezhangtufan = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    //获取入住记录ID
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        for (var i = 0; i < urlquerstr.length; i++) {
            if (urlquerstr[i].indexOf('OrderGuid=') != -1) {
                orderguid = urlquerstr[i];
                orderguid = orderguid.replace('OrderGuid=', "");
            }
            if (urlquerstr[i].indexOf('savetype=') != -1) {
                savetype = urlquerstr[i];
                savetype = savetype.replace('savetype=', "");
            }
            if (urlquerstr[i].indexOf('isYaJing=') != -1) {
                isyajing = urlquerstr[i];
                isyajing = isyajing.replace('isYaJing=', "");
            }
            if (urlquerstr[i].indexOf('ID=') != -1) {
                id = urlquerstr[i];
                mainEntiry.ID = id.replace('ID=', "");
                isviewid = true;

            }
            if (urlquerstr[i].indexOf('isend=') != -1) {
                isendjiezhangtufan = urlquerstr[i];
                isendjiezhangtufan = isendjiezhangtufan.replace('isend=', "");
            }
        }
    }
    //判断是否订单已经结账
    JudgeOrderIsJieZhang();
    //设置布局层的事件
    $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });

    if (savetype == "bufengjie") {
        feilistdata = window.parent.GetXianZhongFeiYongList();
        mainEntiry.ID = "";
    }
    else {
        feilistdata.Money = 0;
        feilistdata.Datas = [];
        if (!isviewid) {
            SelFeiYongList();
        }
        else {
            SelJieZhangFeiYongList();
        }
    }
    IntializeTableDatagrid(datagridid);
    SelMainEntiryaction(datagridid, orderguid);



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
    // alert($(divid).parent().parent().height());
    //初始化表格效果
    $(divid).datagrid({
        width: $(divid).parent().parent().width() * 0.4,
        height: $(divid).parent().parent().height() * 1,
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: false,
        animate: true,
        sortable: true,
        showFooter: true,
        columns: [[
                    { field: 'ID', title: 'ID', width: 0, hidden: true },
                     { field: 'JZD_JZID', title: 'JZID', hidden: true },
                     { field: 'JZD_PayType', title: '付款方式', styler: function (value, row, index) {
                         return 'font-weight:bold;';
                     }, sortable: true, width: 100, align: 'right',
                         formatter: function (value, row, index) {
                             if (value == "单位记账") {
                                 unitrow = index;
                                 return value + "(<a href='javascript:void(0)'  onclick=\"openpartnerview('" + index + "')\">选择</a>)";
                             }
                             else {
                                 return value;
                             }
                         }
                     },
                     { field: 'JZD_Money', title: '金额',
                         editor: { type: 'numberbox',
                             options: { precision: 2, required: true }
                         },
                         sortable: true,
                         width: 80,
                         align: 'center'
                     },
                    { field: 'JZD_Receipt', title: '单据号码', editor: 'text', align: 'center', sortable: true, width: 100 },
                    { field: 'JZD_Remark', title: '备注', editor: 'text', sortable: true, width: 100, align: 'center' }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onBeforeEdit: function (rowIndex, rowData) {

        },
        onAfterEdit: function (rowIndex, rowData, changes) {

        },
        onCancelEdit: function (rowIndex, rowData) {

        },
        onSelect: function (rowData) {

        },
        onClickCell: function (rowIndex, field, value) { },
        onLoadSuccess: function (data) {
            for (var i = 0; i < data.rows.length; i++) {
                if (data.rows[i].JZD_PayType != "单位记账") {
                    EditorDataRow(divid, i, 'beginEdit', '', '');
                    var editors = $(divid).datagrid('getEditors', i);
                    var moneyeditor = editors[0];
                    moneyeditor.target.bind('blur', function () {
                        CoputerMoney(divid, data.rows);
                    });
                }
            }

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
//计算相关付款金额
function CoputerMoney(divid, data) {
    var s = this;
    var allmoney = 0;
    var datarows = $(divid).datagrid('getRows');
    //专门获取单位挂账金额
    var unitmoney = parseFloat(datarows[unitrow].JZD_Money);
    allmoney = unitmoney;
    for (var i = 0; i < data.length; i++) {
        var editors = $(divid).datagrid('getEditors', i);
        var moneyeditor = editors[0];
        if (moneyeditor != undefined && moneyeditor != null) {
            allmoney += parseFloat(moneyeditor.target.val());
        }
    }

    $('#shishoumoney').html(allmoney.toFixed(2));
    SubComputeMoney();
}
//金额累加
function SubComputeMoney() {
    var ssvalue = parseFloat($("#shishoumoney").html());
    var xfvalue = parseFloat($("#xiaofeimoney").html());
    var yhvalue = parseFloat($("#youhuijinger_txt").val());
    var yajingkoukuan = parseFloat($("#DepositDeduct").val());
    var allyanjin = parseFloat($("#Deposit").html());
    if (isNaN(ssvalue)) {
        ssvalue = 0;
    }
    if (isNaN(yhvalue)) {
        yhvalue = 0;
    }
    if (isNaN(xfvalue)) {
        xfvalue = 0;
    }
    if (isNaN(yajingkoukuan)) {
        yajingkoukuan = 0;
    }
    var zlvalue = xfvalue - ssvalue - yhvalue - yajingkoukuan;

    if (zlvalue >= 0) {
        $('#zhaolimoney').html(zlvalue.toFixed(2) * -1).css('color', 'red');
    }
    else {
        $('#zhaolimoney').html(zlvalue.toFixed(2) * -1).css('color', 'black');
    }
    //本次应该收金额
    $("#beichiyingshoumoney").html((xfvalue - yhvalue).toFixed(2));
    //本次应该收金额
    $("#beichiyingshoumoney").html((xfvalue - yhvalue).toFixed(2));
}

//计算押金扣款
function YanjingChange() {
    var yajingkoukuan = parseFloat($("#DepositDeduct").val());
    var allyanjin = parseFloat($("#Deposit").html());
    var ssvalue = parseFloat($("#shishoumoney").html());
    //本次应该收金额
    var yingshou = parseFloat($("#beichiyingshoumoney").html());
    if (isNaN(ssvalue)) {
        ssvalue = 0;
    }
    if (isNaN(yingshou)) {
        yingshou = 0;
    }
    var durmoney = (yingshou - ssvalue).toFixed(2);
    //检查扣款金额是否大于押金余额!
    if ((yajingkoukuan - allyanjin) > 0) {
        alert("抱歉,抵扣押金大于押金余额，只能以押金余额作为抵扣金额!")
        //还需要进行后台验证
        $("#DepositDeduct").val(allyanjin);
    }
    else {
        if (durmoney < yajingkoukuan) {
            $.messager.confirm('Confirm', '本次押金扣款(' + yajingkoukuan + ')大于剩余本次应收金额(' + durmoney + '),是否确认为多余押金退款？',
         function (result) {
             if (result) {
                 //如果true则允许保留扣款金额
                 //检查扣款金额是否大于押金余额!
                 if ((yajingkoukuan - allyanjin) > 0) {
                     alert("抱歉,抵扣押金大于押金余额，只能以押金余额作为抵扣金额!")
                     //还需要进行后台验证
                     $("#DepositDeduct").val(allyanjin);
                 }
             }
             else {
                 yajingkoukuan = durmoney;
                 $("#DepositDeduct").val(yajingkoukuan)
             }

         });
        }
    }
    SubComputeMoney();

}
//计算相关付款金额
function SaveDataChangeValue(divid, isend) {
    $.messager.confirm('Confirm', '确定要保存当前结账信息?', function (r) {
        if (r) {
            var zhaolimoney = $("#zhaolimoney").html();

            if (parseFloat(zhaolimoney) >= 0) {
                ShopMaskLayer(window.document.body, "正在处理数据，请稍等……");
                //首先保存主要结账信息
                mainEntiry.JZ_ALLConsumption = $("#ALLConsumption").html();
                mainEntiry.JZ_Deposit = $("#Deposit").html();
                mainEntiry.JZ_Consumption = $("#xiaofeimoney").html();
                mainEntiry.JZ_Preferential = $("#youhuijinger_txt").val();
                mainEntiry.JZ_Accounts = $("#beichiyingshoumoney").html();
                mainEntiry.JZ_Money = $("#shishoumoney").html();
                mainEntiry.JZ_DepositDeduct = $("#DepositDeduct").val();
                mainEntiry.JZ_Surplus = $("#zhaolimoney").html();
                mainEntiry.JZ_Receipt = $("#ReceiptNo").val();
                mainEntiry.JZ_Remark = $("#remark").val();
                //结账退房时检查押金是否退完
                if (isend) {
                    if (mainEntiry.JZ_DepositDeduct != mainEntiry.JZ_Deposit) {
                        alert("抱歉,结账退房时必须要将多余押金金额进行退款才能继续进行！");
                        $("#DepositDeduct").val(mainEntiry.JZ_Deposit);
                        CloseMaskLayer(window.document.body);
                        SubComputeMoney();
                        return;
                    }
                }
                //检查支付现金退现
                if (((mainEntiry.JZ_Accounts - mainEntiry.JZ_DepositDeduct) <= 0 & mainEntiry.JZ_Money > 0) |
          (mainEntiry.JZ_Money > (mainEntiry.JZ_Accounts - mainEntiry.JZ_DepositDeduct) && (mainEntiry.JZ_Accounts - mainEntiry.JZ_DepositDeduct) > 0)
                 ) {
                    alert("抱歉，除押金抵扣外其它支付方式的合计支付金额不能大于应付金额");
                    CloseMaskLayer(window.document.body);
                    return;
                }
                var paydetailsdata = $(divid).datagrid('getData');
                for (var i = 0; i < paydetailsdata.rows.length; i++) {
                    $(divid).datagrid('endEdit', i);
                }
                //开始执行保存函数
                AddOrUpdateMainEntity(mainEntiry, divid, paydetailsdata.rows, isend);
            }
            else {
                alert("抱歉,本次应退金额必须大于或等于0时才能结账！")
            }
        }
    });
}
//查询主要结账信息
function SelMainEntiryaction(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelMainEntityByOrderGuid', "JZ_OrderGUID": orderguid, "ID": mainEntiry.ID
    };

    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   mainEntiry = data;
                   if (data.JZ_Consumption == 0) {
                       isnewadd = true;
                   }
                   else {
                       isnewadd = false;
                   }
                   mainEntiry.ID = data.ID;
                   mainEntiry.JZ_OrderGUID = orderguid;
                   $("#ALLConsumption").html(mainEntiry.JZ_ALLConsumption);
                   $("#Deposit").html(mainEntiry.JZ_Deposit);
                   $("#xiaofeimoney").html(mainEntiry.JZ_Consumption.toFixed(2));
                   $("#youhuijinger_txt").val(mainEntiry.JZ_Preferential);
                   $("#beichiyingshoumoney").html(mainEntiry.JZ_Accounts);
                   $("#shishoumoney").html(mainEntiry.JZ_Money);
                   $("#DepositDeduct").val(mainEntiry.JZ_DepositDeduct);
                   $("#zhaolimoney").html(mainEntiry.JZ_Surplus);
                   $("#ReceiptNo").val(mainEntiry.JZ_Receipt);
                   $("#remark").val(mainEntiry.JZ_Remark);
                   $("#jz_CreateUser").val(mainEntiry.CreateUser);
                   $("#jz_CreateTime").datetimebox('setValue', mainEntiry.CreateTime);
                   MainguidValue = mainEntiry.ID;
                   Selction(divid, mainEntiry.ID);
                   if (!isviewid) {
                       $("#xiaofeimoney").html(feilistdata.Money);
                       mainEntiry.JZ_Consumption = feilistdata.Money;
                   }
                   if (isyajing == "1") {
                       if (mainEntiry.JZ_Consumption <= mainEntiry.JZ_Deposit) {
                           $("#DepositDeduct").val(mainEntiry.JZ_Consumption);
                       }
                       else {
                           $("#DepositDeduct").val(mainEntiry.JZ_Deposit);
                       }
                       isyajing = "";
                   }
                   else {
                       $("#DepositDeduct").val(mainEntiry.JZ_DepositDeduct);
                   }
                   //当结账时自动把多余现金作为退款处理
                   if (isendjiezhangtufan == "1") {
                       $("#DepositDeduct").val(mainEntiry.JZ_Deposit);
                   }
                   SubComputeMoney();
                   if ((mainEntiry.JZ_Consumption*1)==0) {
                       $("#comfirmbtn").attr("disabled", "disabled");
                       $("#cancebtn").attr("disabled", "disabled");
                   }
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//查询支付明细信息
function Selction(divid, Mainguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByMainGuid', "PageInfo": $.toJSON(_kbpage),
        "ID": Mainguid
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
//查询入住相关费用信息
function SelFeiYongList() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByRZFeiYongList', "PageInfo": $.toJSON(_abpage),
        "RZ_OrderGuid": orderguid, "RZ_Status": ""
    };
    var Urlstr = "../../ActionHanlder/CashChild/BuFengJieZhangHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            try {
                feilistdata.Money = 0;
                if (data.rows.length > 0) {
                    for (var i = 0; i < data.rows.length; i++) {
                        feilistdata.Money = data.rows[i].RZ_Money * 1 + feilistdata.Money;
                    }
                }
                feilistdata.Datas = data.rows;
            }
            catch (e) {
                alert(e);
            }
        }
    });
}
//查询已经结账入住相关费用信息
function SelJieZhangFeiYongList() {
    //发送Post请求, 返回后执行回调函数.
    _cbpage.pageNumber = 1;
    _cbpage.pageSize = 10000;
    var paramdata = { "ActionName": 'SelJZFeiYongList', "PageInfo": $.toJSON(_cbpage),
        "RZ_OrderGuid": orderguid, "JZID": mainEntiry.ID
    };
    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            try {
                feilistdata.Money = 0;
                if (data.rows.length > 0) {
                    for (var i = 0; i < data.rows.length; i++) {
                        feilistdata.Money = data.rows[i].RZ_Money * 1 + feilistdata.Money;
                    }
                }
                feilistdata.Datas = data.rows;
            }
            catch (e) {
                alert(e);
            }
        }
    });
}
//查询特权人优惠金额信息
function SelctionPrivileged(divid, Mainguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelPrivilegedByUser', "PageInfo": $.toJSON(_kbpage),
        "ID": Mainguid
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
//打开特权人信息
function openPrivilegedWindow(windivid) {
    $(windivid).show();
    $(windivid).dialog({
        title: '特权人信息',
        width: 280,
        height: 150,
        closed: false,
        cache: false,
        modal: true,
        onClose: function () {
            $('#youhuijinger_txt').val($("#PriviMoney").val());
            SubComputeMoney();
        }
    });

}
//打开协议单位信息
function openpartnerview(rowindex) {
    $('#WindailogDiv').dialog({
        title: '单位挂账',
        width: $('.easyui-layout').width() * 0.9,
        height: $('.easyui-layout').height() * 0.9,
        closed: false,
        cache: false,
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        modal: true,
        onBeforeOpen: function () {
            ajaxLoading('#WindailogDiv');
        }
    });
    $('#windowIframe')[0].src = './PartnerList.htm?OrderGuid=' + orderguid + "&JZID=" + MainguidValue;
    unitrow = rowindex;
}
//增加支付信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
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
//保存结账信息
function AddOrUpdateMainEntity(data, divid, childrows, iaend) {
    //发送Post请求, 返回后执行回调函数.
    var chiddatarows = feilistdata.Datas;
    data.ActionName = 'AddORUpdateMainEntity';
    data.FeiYongRows = $.toJSON(chiddatarows);
    data.ChildRows = $.toJSON(childrows);
    if (iaend) {
        data.ExuStatuc = "AllEnd";
    }
    else {
        data.ExuStatuc = "";
    }
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data.Flag) {
                       
                       mainEntiry.ID = data.value;
                       SelMainEntiryaction('#tab_shoudatadetails', orderguid);
                       alert(data.Message);
                       PrintZhangDan(mainEntiry.ID, orderguid);                       
                   }
                   else {
                       alert(data.Message);
                   }
               }
               catch (e) {
                   alert(e);
               }
               CloseMaskLayer(window.document.body);
           },
         "json");
}
//删除结账主信息
function DeleteRowMainEntity() {
    //发送Post请求, 返回后执行回调函数.
    $.messager.confirm('Confirm', '确定要删除当前结账信息?', function (r) {
        if (r) {
            ShopMaskLayer(window.document.body, "正在处理数据，请稍等……");
            mainEntiry.ActionName = 'DeleteMainEntity';
            var paramdata = mainEntiry;
            var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
            $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   //重新刷新数据表格
                   SelMainEntiryaction('#tab_shoudatadetails', orderguid);
                   alert(data.Message);
                   CloseMaskLayer(window.document.body);
                   window.close();
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
        }
    });
}
//取消结账主信息
function CanceRowMainEntity() {
    //发送Post请求, 返回后执行回调函数.
    $.messager.confirm('Confirm', '确定要取消当前结账信息?', function (r) {
        if (r) {
            ShopMaskLayer(window.document.body, "正在处理数据，请稍等……");
            mainEntiry.ActionName = 'CanceMainEntity';
            mainEntiry.ExuStatuc = "AllEnd";
            var paramdata = mainEntiry;

            var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
            $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                  
                   //重新刷新数据表格
                   SelMainEntiryaction('#tab_shoudatadetails', orderguid);
                   alert(data.Message);
                   CloseMaskLayer(window.document.body);
                   window.parent.close();
                   window.close();
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
        }
    });
}
//当进行部分账时
function SaveBuFeiJieZhang() {
    var parewindow = window.parent.GetXianZhongFeiYongList();
    var dx = $(parewindow).datagrid("getRows");
}

//获取相关费用明细数据
function GetFeiYongList() {
    return feilistdata;
}
//设置单位挂账的金额
function SetUnitMoney(value) {
    var paydetailsdata = $(datagridid).datagrid('getData');
    for (var i = 0; i < paydetailsdata.rows.length; i++) {
        $(datagridid).datagrid('endEdit', i);
    }
    paydetailsdata.rows[unitrow].JZD_Money = value;
    $(datagridid).datagrid('loadData', paydetailsdata.rows);
    for (var i = 0; i < paydetailsdata.rows.length; i++) {
        if (i != unitrow) {
            $(datagridid).datagrid('beginEdit', i);
        }
    }
    CoputerMoney(datagridid, paydetailsdata.rows);
}

//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $('#divlatyout').height() * 1;
    widthvalue = $('#divlatyout').width() * 0.4;
    $('#tab_shoudatadetails').datagrid('resize', { height: heightvalue, width: widthvalue });
}
function PrintZhangDan(id, xorderguid) {
    $.messager.confirm('Confirm', '是否要进行打印账单?', function (r) {
        if (r) {
            window.open('sankejiezhangdangPrint.htm?ID=' + id + '&OrderGuid=' + xorderguid, "xunizhangdanprintview",
     "width=" + $('#divlatyout').width() * 0.9 + ",title='结账单打印',height=400, toolbar=no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no,z-look=yes");
            if (window.parent.Operator_closejijiejiezhang != undefined) {
                window.parent.Operator_closejijiejiezhang();                
            }
        }
        else {
            if (window.parent.Operator_closejijiejiezhang != undefined) {
                window.parent.Operator_closejijiejiezhang();
            }

        }
    });
}
function ViewJieZhanFeiYongList() {
    $('#feiyongdiv').attr("display", 'block').dialog({
        title: '已结账费用明细',
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
        },
        onBeforeClose: function () {

        },
        onClose: function () {
            $('#feiyongdiv').attr("display", 'none')
        }
    });
    $("#tab_feiyonglist").datagrid({
        fitColumns: true,
        singleSelect: true,
        remoteSort: false,      // 后台排序
        pagination: false,
        animate: true,
        sortable: true,
        showFooter: true,
        selectOnCheck: false,
        checkOnSelect: false,
        columns: [[
                    { field: 'RZ_ID', checkbox: true, title: 'ID', width: 0 },
                    { field: 'RZ_OrderGuid', title: 'OrderID', width: 0, hidden: true },
                    { field: 'RZ_RoomNo', title: '房号', width: 60 },
                    { field: 'RZ_FYType', title: '项目名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiyongType', title: '费用名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_FeiYongName', title: '商品名称', sortable: true, width: 120, align: 'center' },
                    { field: 'RZ_Money', title: '费用金额', sortable: true, width: 60, align: 'center' },
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
                ]]
    });
    var rowcount = feilistdata.Datas.length;
    $('#tab_feiyonglist').datagrid("loadData", { total: rowcount, rows: feilistdata.Datas });
}
//判断订单是否已经结账
function JudgeOrderIsJieZhang() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelIsOrderGuidJieZhang', "OrderGuid": orderguid };
    var Urlstr = "../../ActionHanlder/CashChild/JieZhangTuFangHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            try {
                if (!data.Flag) {
                    $("#comfirmbtn").attr("disabled", "disabled");
                    $("#cancebtn").attr("disabled", "disabled");
                }
            }
            catch (e) {
                alert(e);
            }
        }
    });
};
//#endregion
