
//#region 共用变更

var _kbpage =     // 客户用品分页对象
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
var fanhao = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    //获取入住记录ID
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        for (var i = 0; i < urlquerstr.length; i++) {
            if (urlquerstr[i].indexOf('OrderGuid=') > -1) {
                orderguid = urlquerstr[i];
                orderguid = orderguid.replace('OrderGuid=', "");
            }
            if (urlquerstr[i].indexOf('fh=') > -1) {
                fanhao = urlquerstr[i];
                fanhao = fanhao.replace('fh=', "");
            }
            if (urlquerstr[i].indexOf('recordid=') > -1) {
                recordID = urlquerstr[i];
                recordID = recordID.replace('recordid=', "");
            }
        }

    }
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
    //初始化支付方式
    SelctPayType();
    //关闭初始打开的遮盖层
    ajaxLoadEnd();
    //alert($('.easyui-tabs').parent().height());
    $('.easyui-tabs').tabs({
        width: "auto",
        height: $('.easyui-tabs').parent().parent().height()
    });
    //查询实体
    SelctSigeEntity(recordID);
});
//#endregion

//#region 操作列表

//展示编辑数据行
function AddDataRow() {
    getvaluefromUI(currdata);
    AddOrUpdate(currdata);
}


//查询单笔记录信息
function SelctSigeEntity(id) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelEntiryByID', "ID": id };
    var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   currdata = data;
                   setvaluetoUI(currdata);
                   SelctOrderInofList();
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

//查询单笔记录信息
function SelctOrderInofList() {
    //发送Post请求, 返回后执行回调函数.
    _kbpage.pageSize = 10000;
    var paramdata = { "ActionName": 'SelOrderInfo', "OrderGuid": orderguid, "PageInfo": $.toJSON(_kbpage) };
    var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data.rows.length > 0) {
                       var orderdata = data.rows[0];
                       $("#shunpay").val(orderdata.allCosumper);
                       $("#Prepaid").val(orderdata.YaJin);
                       $("#payrooms").val(orderdata.FangHao);
                       $("#payUnit").val(orderdata.KerenLeibie);
                       $("#payman").val(orderdata.XingMing);
                   }
                   else {

                   }
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//将值显示到界面中
function setvaluetoUI(data) {
    $("#paymoney").val(currdata.SK_Money);
    $('#paytime').datetimebox('setValue', currdata.SK_PayTime)
    $("#payreceiptcode").val(currdata.SK_Receipt);
    $("#payremark").val(currdata.SK_Remark);
    data.SK_PayType = $("#paytype").combobox('setValue', currdata.SK_PayType);
    if (data.CreateTime != null) {
        data.CreateTime = (eval('new ' + (data.CreateTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")); ;
    }
    if (data.UpdateTime != null) {
        data.UpdateTime = (eval('new ' + (data.UpdateTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")); ;
    }
}
//从UI获取界面值
function getvaluefromUI(data) {
    data.SK_OrderGUID = orderguid;

    data.SK_Money = $("#paymoney").val();
    data.SK_PayTime = $('#paytime').datetimebox('getValue');
    data.SK_Receipt = $("#payreceiptcode").val();
    data.SK_Remark = $("#payremark").val();
    data.SK_PayType = $("#paytype").combobox('getText');
    data.SK_YiShouMoney = $('#Prepaid').val();
    data.SK_YingShouMoney = $("#shunpay").val();
    data.SK_LianfanHao = $("#payrooms").val();
}
//增加客户用品信息
function AddOrUpdate(dataEntity) {
    //发送Post请求, 返回后执行回调函数.
    if (!(parseFloat(dataEntity.SK_Money) < 0)) {
        alert("请输入小于0的金额！");
        return;
    }

    $.messager.progress({ text: "正在查询数据，请等候……" });
    dataEntity.ActionName = 'AddORUpdate';
    dataEntity.SK_Type = "退款";
    var paramdata = dataEntity;
    var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   $.messager.progress('close');
                   if (data.Flag) {
                       SelctSigeEntity(data.value);
                   }
                   alert(data.Message);
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

//删除退款信息
function DeleteRow() {
    //发送Post请求, 返回后执行回调函数.
    if (currdata != null) {
        var paramdata = { "ActionName": 'Delete', "ID": currdata.ID };      
        var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
        $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   //重新刷新数据表格
                   SelctSigeEntity(currdata.ID);
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

   //查询单笔记录信息
   function DeleteRowxx() {
       //发送Post请求, 返回后执行回调函数.
       var paramdata = { "ActionName": 'Delete', "ID": "" };
       var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
       $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   currdata = data;
                   setvaluetoUI(currdata);
                   SelctOrderInofList();
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
   }
//查询单笔记录信息
function SelctPayType() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelPayType' };
    var Urlstr = "../../ActionHanlder/CashChild/ShouKuanHalder.ashx";
    $.ajax({
        type: "POST",
        data: paramdata,
        dataType: 'json',
        url: Urlstr,
        async: false, //设为false就是同步请求
        cache: false,
        success: function (data) {
            try {
                $('#paytype').combobox({
                    valueField: 'id',
                    textField: 'text'
                }).combobox('loadData', data);
            }
            catch (e) {
                alert(e);
            }
        }
    });
}
function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
}
//#endregion
