
function openwin(filename, h, w) {
    window.open(filename, "newwindow", "height=" + h + ", width=" + w + ", toolbar=no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no,z-look=yes");
}
function opennewwin(filename, h, w, newwindowname) {
    window.open(filename, newwindowname, "height=" + h + ", width=" + w + ", toolbar=no, menubar=no, scrollbars=yes, resizable=yes, location=no, status=no,z-look=yes");
}
function opennewdivform(title, iframename, divformname, targetpage, actionname,closecallbackfunc) {
    $(divformname).dialog({
        title: title,
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
            ajaxLoading(divformname);
        },
        onClose: closecallbackfunc
    })
    .dialog(actionname);
    $(iframename)[0].src = targetpage;
}
function opencustomenewdivform(title, iframename, divformname, targetpage, actionname, width, height, okcallbackfun, cancecallbackfun) {

    $(divformname).dialog({
        title: title,
        width: width,
        height: height,
        closed: false,
        cache: false,
        collapsible: true,
        minimizable: true,
        maximizable: true,
        resizable: true,
        modal: false,
        tools: [
				{
				    iconCls: 'icon-ok',
				    handler: okcallbackfun
				}, {
				    iconCls: 'icon-cancel',
				    handler: cancecallback
				}]
    }).dialog(actionname);
    $(iframename)[0].src = targetpage;
}
function gethtmlObj(id) {
    return document.getElementById(id);
}
function getiframentelement(iframeid, elementid) {
    var selobj = window.top.document.getElementById(iframeid).contentWindow;
    //通过获取到的window对象操作HTML元素，这和普通页面一样
    return selobj.document.getElementById(elementid);

}
//获取URL中的QueryString 
function getQueryString() {
    var result = location.search.match(new RegExp("[\?\&][^\?\&]+=[^\?\&]+", "g"));
    if (result == null) {
        return "";
    }
    for (var i = 0; i < result.length; i++) {
        result[i] = result[i].substring(1);
    }
    return result;

}
Date.prototype.pattern = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份           
        "d+": this.getDate(), //日           
        "h+": this.getHours() % 24 == 0 ? 24 : this.getHours() % 24, //小时           
        "H+": this.getHours(), //小时           
        "m+": this.getMinutes(), //分           
        "s+": this.getSeconds(), //秒           
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度           
        "S": this.getMilliseconds() //毫秒           
    };
    var week = {
        "0": "/u65e5",
        "1": "/u4e00",
        "2": "/u4e8c",
        "3": "/u4e09",
        "4": "/u56db",
        "5": "/u4e94",
        "6": "/u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "/u661f/u671f" : "/u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
};

function ajaxLoading(windiv) {
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%",
        height: $(windiv).height() + 30, "background-color": "#eef5ff", "filter": "Alpha(opacity=100)"
    }).appendTo($(windiv));
    $("<div class=\"datagrid-mask-msg\"></div>").html("正在加载数据，请稍等……")
    .appendTo($(windiv))
    .css({ display: "block", left: ($(windiv).outerWidth(true) - 190) / 2, top: ($(windiv).height() - 45) / 2 });
}
function ajaxLoadEnd() {
    //alert(window.parent.window.document.body.innerHTML);
    var tx = $(window.parent.window.document.body).find(".datagrid-mask");
    $(window.parent.window.document.body).find(".datagrid-mask").remove();
    $(window.parent.window.document.body).find(".datagrid-mask-msg").remove();
}
function ShopMaskLayerWipe(windiv,message, width, height, left, top) {
    if (left == null) {
        left = (width*0.70) / 2;
    }
    if (top == null) {
        top = (height*0.8) / 2;
    }
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: width,
        height: height, "background-color": "#eef5ff", "filter": "Alpha(opacity=100)"
    }).appendTo($(windiv));
    $("<div class=\"datagrid-mask-msg\"></div>").html(message)
    .appendTo($(windiv))
    .css({ display: "block", left: left, top: top });
}
function ShopMaskLayer(windiv, message) {
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: $(windiv).width() * 2,
        height: $(windiv).height() + 30, "background-color": "#eef5ff", "filter": "Alpha(opacity=80)"
    }).appendTo($(windiv));
    $("<div class=\"datagrid-mask-msg\"></div>").html(message)
    .appendTo($(windiv))
    .css({ display: "block", left: ($(windiv).outerWidth(true) - 190) / 2, top: ($(windiv).height() - 45) / 2 });
//    .dialog({
//        closed: false,
//        cache: false,
//        modal: true
//    }).dialog("open");
   
}
function CloseMaskLayer(windiv) {
    //alert(window.parent.window.document.body.innerHTML);
    var tx = $(windiv).find(".datagrid-mask");
    $(windiv).find(".datagrid-mask").remove();
    $(windiv).find(".datagrid-mask-msg").remove();
}
//数据拷贝
function ArrayCopye(destination, source) {
    for (var p in source) {
        if (getType(source[p]) == "array" || getType(source[p]) == "object") {
            destination[p] = getType(source[p]) == "array" ? [] : {}; arguments.callee(destination[p], source[p]);
        } else {
            destination[p] = source[p];
        }
    }
}
function getType(o) {
    var _t;
    return ((_t = typeof (o)) == "object" ? o == null && "null" || Object.prototype.toString.call(o).slice(8, -1) : _t).toLowerCase();
}
//从页面将值转换为对象数组
function GetDataFromPageView(fields, action, Actiontype) {
    var fieldlist = fields.split(",");
    var fieldvalue = "";
    var subfieldvalue = "";
    var fieldval = "";
    for (var i = 0; i < fieldlist.length; i++) {
        subfieldvalue = fieldlist[i].replace("#", "");  //分解字段
        subfieldvalue = subfieldvalue.substr(0, 1).toLocaleUpperCase() + subfieldvalue.substr(1, subfieldvalue.length - 1); //去除空值
        var fieldobject = $(fieldlist[i])[0]; //获取控件
        fieldval = $(fieldlist[i]).val();    //获取控件值
        //判断控件是否为textarea
        if (fieldobject != undefined) {
            if (fieldobject.type == "textarea") {
                var encodevalue = encodeURIComponent($(fieldlist[i]).val());  //对textarea控件值进行编码
                fieldval = encodeURIComponent($(fieldlist[i]).val());
            }
            if (fieldobject.type == "checkbox") {
                fieldval = fieldobject.checked;
            }
            if (fieldobject.type == "select-one") {
                fieldval = fieldobject.value;
                if (fieldval == "") {
                    fieldval = $(fieldlist[i]).combobox('getValue');
                }
            }
        }
        fieldvalue += '"' + subfieldvalue + '":"' + fieldval + '",';
    }
    var datavalue = "{" + fieldvalue + '"param": "' + action + '"';
    if (Actiontype != "") {
        datavalue = datavalue + ',Action:"' + Actiontype + '"}';
    }
    else {
        datavalue = datavalue + '}';
    }
    var Dataobject = eval("(" + datavalue + ")");
    return Dataobject;
}
//从页面将值对象数组,用户可以直接增加参数。
function GetDataFromPageView_param(fields, action, Actiontype, parameters) {
    var fieldlist = fields.split(",");
    var fieldvalue = "";
    var subfieldvalue = "";
    var fieldval = "";
    for (var i = 0; i < fieldlist.length; i++) {
        subfieldvalue = fieldlist[i].replace("#", "");  //分解字段
        subfieldvalue = subfieldvalue.substr(0, 1).toLocaleUpperCase() + subfieldvalue.substr(1, subfieldvalue.length - 1); //去除空值
        var fieldobject = $(fieldlist[i])[0]; //获取控件
        fieldval = $(fieldlist[i]).val();    //获取控件值
        //判断控件是否为textarea
        if (fieldobject != undefined) {
            if (fieldobject.type == "textarea") {
                var encodevalue = encodeURIComponent($(fieldlist[i]).val());  //对textarea控件值进行编码
                fieldval = encodeURIComponent($(fieldlist[i]).val());
            }
            if (fieldobject.type == "checkbox") {
                fieldval = fieldobject.checked;
            }
            if (fieldobject.type == "select-one") {
                fieldval = fieldobject.value;
                if (fieldval == "") {
                    fieldval = $(fieldlist[i]).combobox('getValue');
                }
            }
        }
        fieldvalue += '"' + subfieldvalue + '":"' + fieldval + '",';
    }
    var datavalue = "{" + fieldvalue + '"param": "' + action + '"';
    if (Actiontype != "") {
        datavalue = datavalue + ',Action:"' + Actiontype + '",' + parameters + '}';
    }
    else {
        datavalue = datavalue + ',' + parameters + '}';
    }

    var Dataobject = eval("(" + datavalue + ")");
    return Dataobject;
}
//从JSON数据将值赋于页面
function DataShowToPageView(fields, dataarray) {
    if (dataarray != null) {
        var fieldlist = fields.split(",");
        for (var i = 0; i < fieldlist.length; i++) {
            var subfieldvalue = fieldlist[i].replace("#", "");
            var fiev = dataarray[subfieldvalue];

            var fieldobject = $(fieldlist[i])[0];
            if ($(fieldlist[i])[0] != undefined) {
                switch ($(fieldlist[i])[0].type) {
                    case "textarea":
                        $(fieldlist[i]).val(decodeURIComponent(fiev));   //对值进行解码
                        break;
                    case "checkbox":
                        if (fiev == "True") {
                            $(fieldlist[i])[0].checked = true;
                        }
                        else if (fiev == "False") {
                            $(fieldlist[i])[0].checked = false;
                        }
                        else {
                            $(fieldlist[i])[0].checked = false;
                        }
                        break;
                    case "select-one":
                        if (fiev != "") {
                            $(fieldlist[i]).combobox('setValue', fiev);
                        }
                        else {
                            $(fieldlist[i]).combobox('setValue', "");
                        }
                        break;
                    default:
                        $(fieldlist[i]).val(fiev);
                        break;
                }
            }
            else {
                $(fieldlist[i]).val(fiev);
            }
        }
    }
}
//将文件到上传到服务器中
function ajaxFileUpload(urltxt) {
    loading(); //动态加载小图标
    $.ajaxFileUpload({
        url: urltxt,
        secureuri: false,
        fileElementId: 'fileToUpload',
        dataType: 'json',
        success: function (data, status) {
            if (typeof (data.error) != 'undefined') {
                if (typeof (data.error) != 'undefined') {
                    if (data.error != '') {
                        alert(data.error);
                    } else {
                        alert(data.msg);
                    }
                }
            }
        },
        error: function (data, status, e) {
            alert(e);
        }
    })

    return false;
}
//下载文件
function ajaxFileDownload(urltxt) {
    ajaxFileDownloadx1(urltxt);
    ajaxFileDownloadx1(urltxt);
    ajaxFileDownloadx1(urltxt);
}
//加载上传下载图形
function loading() {
    $("#loading").ajaxStart(function () {
        $(this).show();
    }).ajaxComplete(function () {
        $(this).hide();
    });
}

//押金转成大写
function changeMoneyToChinese(money) {
    var cnNums = new Array("零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖"); //汉字的数字
    var cnIntRadice = new Array("", "拾", "佰", "仟"); //基本单位
    var cnIntUnits = new Array("", "万", "亿", "兆"); //对应整数部分扩展单位
    var cnDecUnits = new Array("角", "分", "毫", "厘"); //对应小数部分单位
    var cnInteger = "整"; //整数金额时后面跟的字符
    var cnIntLast = "元"; //整型完以后的单位
    var maxNum = 999999999999999.9999; //最大处理的数字
    var IntegerNum; //金额整数部分
    var DecimalNum; //金额小数部分
    var ChineseStr = ""; //输出的中文金额字符串
    var parts; //分离金额后用的数组，预定义
    if (money == "") {
        return "";
    }
    money = parseFloat(money);
    //alert(money);
    if (money >= maxNum) {
        $.alert('超出最大处理数字');
        return "";
    }
    if (money == 0) {
        ChineseStr = cnNums[0] + cnIntLast + cnInteger;
        //document.getElementById("show").value=ChineseStr;
        return ChineseStr;
    }
    money = money.toString(); //转换为字符串
    if (money.indexOf(".") == -1) {
        IntegerNum = money;
        DecimalNum = '';
    } else {
        parts = money.split(".");
        IntegerNum = parts[0];
        DecimalNum = parts[1].substr(0, 4);
    }
    if (parseInt(IntegerNum, 10) > 0) {//获取整型部分转换
        zeroCount = 0;
        IntLen = IntegerNum.length;
        for (i = 0; i < IntLen; i++) {
            n = IntegerNum.substr(i, 1);
            p = IntLen - i - 1;
            q = p / 4;
            m = p % 4;
            if (n == "0") {
                zeroCount++;
            } else {
                if (zeroCount > 0) {
                    ChineseStr += cnNums[0];
                }
                zeroCount = 0; //归零
                ChineseStr += cnNums[parseInt(n)] + cnIntRadice[m];
            }
            if (m == 0 && zeroCount < 4) {
                ChineseStr += cnIntUnits[q];
            }
        }
        ChineseStr += cnIntLast;
        //整型部分处理完毕
    }
    if (DecimalNum != '') {//小数部分
        decLen = DecimalNum.length;
        for (i = 0; i < decLen; i++) {
            n = DecimalNum.substr(i, 1);
            if (n != '0') {
                ChineseStr += cnNums[Number(n)] + cnDecUnits[i];
            }
        }
    }
    if (ChineseStr == '') {
        ChineseStr += cnNums[0] + cnIntLast + cnInteger;
    }
    else if (DecimalNum == '') {
        ChineseStr += cnInteger;
    }
    return ChineseStr;
}