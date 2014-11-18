
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
var Address = [{ "value": "1", "text": "CHINA" }, { "value": "2", "text": "USA" }, { "value": "3", "text": "Koren" }, { "value": "4", "text": "饮食"}];
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
        orderguid = urlquerstr[0];
        orderguid = orderguid.replace('OrderGuid=', "");
    }

    //设置布局层的事件
     $('.easyui-layout').layout('panel', 'east').panel({
        onResize: function (width, height) {
            window.clearTimeout(resizeTimeoutId);
            resizeTimeoutId = window.setTimeout('ViewIntialization();', 10);
        }
    });
    //初始化所有费用
    IntializeTableDatagrid("#tab_datalist", "虚拟账单明细");
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
        columns: [[
         { field: 'ID', title: '', hidden: true, sortable: true, width: 0, align: 'center' },
         { field: 'OrderGuid', title: '', hidden: true, sortable: true, width: 0, align: 'center' },
                    { field: 'Customer', title: '姓名/单位', sortable: true, width: 30, align: 'center' },
                     { field: 'CreateTime', title: '时间', sortable: true, width: 60, align: 'center' },
                    { field: 'No', title: 'No', sortable: true, width: 60, align: 'center' },
                    { field: 'FangHaos', title: '房号', sortable: true, width: 60, align: 'center' },
                    { field: 'FangJia', title: '房价', editor: { type: 'XXX', options: { xx: 1, typex: 2} }, sortable: true, width: 120, align: 'center' },
                    { field: 'DaoDianTime', title: '到店时间', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (value) {
                                return (eval('new ' + (value.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss"));
                            } else {
                                return value;
                            }
                        } 
                    },
                    { field: 'LiDianTime', title: '离店时间', editor: { type: 'text' }, sortable: true, width: 80,
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
            currdatarow = rowData;
            SetValues(currdatarow);
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
//界面布局调整初始化
function ViewIntialization() {
    //设置费用明细的长度和宽度
    heightvalue = $(".easyui-layout").parent().height() * 0.3;
    widthvalue = $(".easyui-layout").parent().width();
    $('#tab_datalist').datagrid('resize', {
        height: heightvalue,
        width: widthvalue
    });
}
//从UI控件获取相应的值
function GetValues() {
    var dataobject = new Object();
    dataobject.ID = currdatarow.ID;
    dataobject.OrderGuid = orderguid;
    dataobject.Rooms = $("#Rooms").val();
    dataobject.Customer = $("#Customer").val();
    dataobject.No = $("#No").val();
    dataobject.FangHaos = $("#FangHaos").val();
    dataobject.FangJia = $("#FangJia").val();
    dataobject.DaoDianTime = $("#DaoDianTime").datetimebox('getValue');
    dataobject.LiDianTime = $("#LiDianTime").datetimebox('getValue');
    dataobject.FanFei = $("#FanFei").val();
    dataobject.ShangPingFei = $("#ShangPingFei").val();
    dataobject.CanFei = $("#CanFei").val();
    dataobject.PeiShun = $("#PeiShun").val();
    dataobject.Qita = $("#Qita").val();
    dataobject.RuZhuTian = $("#RuZhuTian").val();
    dataobject.HuiYuan = $("#HuiYuan").val();
    dataobject.ChuZiYuE = $("#ChuZiYuE").val();
    dataobject.ZhiFeng = $("#ZhiFeng").val();
    dataobject.CaoZhou = $("#CaoZhou").val();
    dataobject.XiaoFeiHeJi = $("#XiaoFeiHeJi").val();
    dataobject.XianJing = $("#XianJing").val();
    dataobject.XinYongCar = $("#XinYongCar").val();
    dataobject.JieJiCar = $("#JieJiCar").val();
    dataobject.DaiJingQuang = $("#DaiJingQuang").val();
    dataobject.ChuZhiCar = $("#ChuZhiCar").val();
    dataobject.XieYiDanWei = $("#XieYiDanWei").val();
    dataobject.ZhiZhangJingE = $("#ZhiZhangJingE").val();
    dataobject.ZhiPiaoJingE = $("#ZhiPiaoJingE").val();
    dataobject.ZhiPiaoHaoMa = $("#ZhiPiaoHaoMa").val();
    dataobject.YaJing = $("#YaJing").val();
    dataobject.JieZhangShouKuan = $("#JieZhangShouKuan").val();
    dataobject.TuiXianJin = $("#TuiXianJin").val();
    dataobject.TuXingYongCar = $("#TuXingYongCar").val();
    dataobject.CreateTime = $("#CreateTime").val();
    dataobject.Tel = $("#Tel").val();
    dataobject.Address = $("#Address").val();
    return dataobject;
}
//对UI赋值
function SetValues(data) {
    $("#ID").val(data.ID);
    $("#Rooms").val(data.Rooms);
    $("#Customer").val(data.Customer);
    $("#No").val(data.No);
    $("#FangHaos").val(data.FangHaos);
    $("#FangJia").val(data.FangJia);
    $("#DaoDianTime").datebox('setValue', (eval('new ' + (data.DaoDianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#LiDianTime").datebox('setValue', (eval('new ' + (data.LiDianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#FanFei").val(data.FanFei);
    $("#ShangPingFei").val(data.ShangPingFei);
    $("#CanFei").val(data.CanFei);
    $("#PeiShun").val(data.PeiShun);
    $("#Qita").val(data.Qita);
    $("#RuZhuTian").val(data.RuZhuTian);
    $("#HuiYuan").val(data.HuiYuan);
    $("#ChuZiYuE").val(data.ChuZiYuE);
    $("#ZhiFeng").val(data.ZhiFeng);
    $("#CaoZhou").val(data.CaoZhou);
    $("#XiaoFeiHeJi").val(data.XiaoFeiHeJi);
    $("#XianJing").val(data.XianJing);
    $("#XinYongCar").val(data.XinYongCar);
    $("#JieJiCar").val(data.JieJiCar);
    $("#DaiJingQuang").val(data.DaiJingQuang);
    $("#ChuZhiCar").val(data.ChuZhiCar);
    $("#XieYiDanWei").val(data.XieYiDanWei);
    $("#ZhiZhangJingE").val(data.ZhiZhangJingE);
    $("#ZhiPiaoJingE").val(data.ZhiPiaoJingE);
    $("#ZhiPiaoHaoMa").val(data.ZhiPiaoHaoMa);
    $("#YaJing").val(data.YaJing);
    $("#JieZhangShouKuan").val(data.JieZhangShouKuan);
    $("#TuiXianJin").val(data.TuiXianJin);
    $("#TuXingYongCar").val(data.TuXingYongCar);
    $("#CreateTime").val(data.CreateTime);
    $("#OrderGuid").val(data.OrderGuid);
    $("#Address").val(data.Address);
    $("#Tel").val(data.Tel);
}

//重新生成
function resize() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelNullEnitry', "PageInfo": $.toJSON(_kbpage),
        "OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/XuNiZhangDanHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   currdatarow = data.rows[0];
                   SetValues(currdatarow);

               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}
//查询红冲费用信息
function Selction(divid, orderguid) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByOrderGuid', "PageInfo": $.toJSON(_kbpage),
        "OrderGuid": orderguid
    };
    var Urlstr = "../../ActionHanlder/CashChild/XuNiZhangDanHalder.ashx";
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
function AddOrUpdate() {
    //发送Post请求, 返回后执行回调函数.
    var data = GetValues();
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/XuNiZhangDanHalder.ashx";
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
//增加红字冲账信息
function Delete(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'Delete';
    var paramdata = data;
    var Urlstr = "../../ActionHanlder/CashChild/XuNiZhangDanHalder.ashx";
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
//重新刷新相关表格
function RefreshDataGrid() {
    Selction("#tab_datalist", orderguid);
}
function PrintView() {
    if (currdatarow) {
        window.open('sankexunizhangdangPrint.htm?ID=' + currdatarow.ID + '&OrderGuid=' + orderguid, "xunizhangdanprintview",
     "width=1100,height=400, toolbar=no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no,z-look=yes");
    }
    else {
        alert("请选择记录");
    }

}
//#endregion
