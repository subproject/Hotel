
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
var ID = "";
var orderguid = "";
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    var urlquerstr = getQueryString();
    if (urlquerstr != "") {
            ID = urlquerstr[0];
            ID = ID.replace('ID=', "");
            orderguid = urlquerstr[1];
            orderguid = orderguid.replace('OrderGuid=', "");
    }
    Selction();
});
//#endregion

//#region 操作列表
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

//对UI赋值
function SetValues(data) {
    $("#ID").html(data.ID);
    $("#Rooms").html(data.Rooms);
    $("#Customer").html(data.Customer);
    $("#No").html(data.No);
    $("#CreateTime").html((eval('new ' + (data.DaoDianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#FangHaos").html(data.FangHaos);
    $("#FangJia").html(data.FangJia);
    $("#DaoDianTime").html((eval('new ' + (data.DaoDianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#LiDianTime").html((eval('new ' + (data.LiDianTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    $("#FanFei").html(data.FanFei);
    $("#ShangPingFei").html(data.ShangPingFei);
    $("#CanFei").html(data.CanFei);
    $("#PeiShun").html(data.PeiShun);
    $("#Qita").html(data.Qita);
    $("#RuZhuTian").html(data.RuZhuTian);
    $("#HuiYuan").html(data.HuiYuan);
    $("#ChuZiYuE").html(data.ChuZiYuE);
    $("#ZhiFeng").html(data.ZhiFeng);
    $("#CaoZhou").html(data.CaoZhou);
    $("#XiaoFeiHeJi").html(data.XiaoFeiHeJi);
    $("#XianJing").html(data.XianJing);
    $("#XinYongCar").html(data.XinYongCar);
    $("#JieJiCar").html(data.JieJiCar);
    $("#DaiJingQuang").html(data.DaiJingQuang);
    $("#ChuZhiCar").html(data.ChuZhiCar);
    $("#XieYiDanWei").html(data.XieYiDanWei);
    $("#ZhiZhangJingE").html(data.ZhiZhangJingE);
    $("#ZhiPiaoJingE").html(data.ZhiPiaoJingE);
    $("#ZhiPiaoHaoMa").html(data.ZhiPiaoHaoMa);
    $("#YaJing").html(data.YaJing);
    $("#JieZhangShouKuan").html(data.JieZhangShouKuan);
    $("#TuiXianJin").html(data.TuiXianJin);
    $("#TuXingYongCar").html(data.TuXingYongCar);
    if (data.CreateTime != null) {
        $("#CreateTime").html((eval('new ' + (data.CreateTime.replace(/\//g, ''))).pattern("yyyy-MM-dd hh:mm:ss")));
    }
    $("#OrderGuid").html(data.OrderGuid);
    $("#Address").html(data.Address);
    $("#Tel").html(data.Tel);
}

//查询红冲费用信息
function Selction() {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelByID',"ID": ID,"OrderGuid":orderguid};
    var Urlstr = "../../ActionHanlder/CashChild/XuNiZhangDanHalder.ashx";
    $.post(
           Urlstr,
           paramdata,
           function (data, textStatus) {
               try {
                   if (data != null) {
                       SetValues(data);
                   }
               }
               catch (e) {
                   alert(e);
               }
           },
         "json");
}

function RefreshDataGrid() {
    Selction("#tab_datalist", orderguid);
}
//#endregion
