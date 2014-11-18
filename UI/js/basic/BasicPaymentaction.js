
//#region 共用变更

var _kbpage =     // 客户用品分页对象
{
sortField: 'ID',
sortDirection: 0,
pageNumber: 1,
pageSize: 30
}
var currdata =null;
var addflag = false; //以判断是否新增表格行
var Address = [{ "value": "1", "text": "现金" }, { "value": "2", "text": "支票" }, { "value": "3", "text": "转账" }, { "value": "4", "text": "饮食"}];
//#endregion

//#region 结帐界面初始化
$(document).ready(function () {
    //获取入住记录ID   
    $('.easyui-tabs').tabs({
        onSelect: function (title, index) {
            switch (title) {
                case "明细":
                    IntializeTableDatagrid('#tab_paymenttypes');
                    Selction('#tab_paymenttypes', "", title)
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
                    { field: 'ID', title: 'ID', hidden: true },
                     { field: 'BPM_SeqNumber', title: '显示顺序号', width: 30,align: 'center', editor: { type: 'numberbox',
                         options: {  required: true }
                     }
                     },
                    { field: 'BPM_PayMentName', title: '付现类型',
                        editor: { type: 'validatebox', options: { required: true} },
                        sortable: true,
                        width: 80,
                        align: 'center'
                    },
                    { field: 'BPM_IsDepositPay', title: '是否用于押金付款',
                        formatter: function (value, row, index) {
                            if (value == true) {
                                return "<input type=\"checkbox\"  checked='checked' ondblclick=checkboxclick(this," + index + ",'BPM_IsDepositPay') onclick=checkboxclick(this," + index + ",'BPM_IsDepositPay')>";
                            }
                            else {
                                return "<input type=\"checkbox\" ondblclick=checkboxclick(this," + index + ",'BPM_IsDepositPay')  onclick=checkboxclick(this," + index + ",'BPM_IsDepositPay')>";
                            }
                        },
                        sortable: true, width: 80, align: 'center'
                    },
                    { field: 'BPM_IsUseCheckout', title: '是否用于结账付款',
                        formatter: function (value, row, index) {
                            if (value == true) {
                                return "<input type=\"checkbox\"  checked='checked' ondblclick=checkboxclick(this," + index + ",'BPM_IsUseCheckout') onclick=checkboxclick(this," + index + ",'BPM_IsUseCheckout')>";
                            }
                            else {
                                return "<input type=\"checkbox\" ondblclick=checkboxclick(this," + index + ",'BPM_IsUseCheckout') onclick=checkboxclick(this," + index + ",'BPM_IsUseCheckout')>";
                            }
                        }, sortable: true, width: 60, align: 'center'
                    },
                    { field: 'BPM_Remark', title: '备注', editor: 'text', sortable: true, width: 60, align: 'center' },
                    { field: 'BPM_Editor', title: '编辑', sortable: true, width: 60, align: 'center',
                        formatter: function (value, row, index) {
                            if (row.editing) {
                                var actiontxt = "<input type='button' value='保存'  onclick=EditorDataRow('" + divid + "'," + index + ",'endEdit',this,'') />"
                                actiontxt = actiontxt + "<input type='button' value='取消' onclick=EditorDataRow('" + divid + "'," + index + ",'cancelEdit',this,'') />"
                                return actiontxt
                            }
                            else {
                                if (row.BPM_PayMentName == "" || row.BPM_PayMentName == null) {
                                    return "<input type='button' value='增加' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                }
                                else {
                                    var actiontxt = "<input type='button' value='编辑' onclick=EditorDataRow('" + divid + "'," + index + ",'beginEdit',this,'') />"
                                    actiontxt = actiontxt + "<input type='button' value='删除' onclick=DeleteRow('" + divid + "'," + index + ") />"
                                    return actiontxt;
                                }
                            }
                        }
                    }
                ]],
        onHeaderContextMenu: function (e, field) {

        },
        onBeforeEdit: function (rowIndex, rowData) {
            rowData.editing = true;
            $(divid).datagrid('refreshRow', rowIndex);
            currdata = rowData;
            if (rowData.BPM_PayMentName == null) {
                addflag = true;
            }
            else {
                addflag = false;
            }
        },
        onAfterEdit: function (rowIndex, rowData, changes) {
            rowData.editing = false;

            $(divid).datagrid('refreshRow', rowIndex);
            AddOrUpdate(rowData);
            //在界面多增加一行以便录入更多数据
            if (addflag) {
                AddDataRow(divid, rowData);
                addflag = false;
            }
            currdata =null;
        },
        onCancelEdit: function (rowIndex, rowData) {
            rowData.editing = false;
            $(divid).datagrid('refreshRow', rowIndex);
            currdata = null;
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
//编辑框点击时
function checkboxclick(checkbox, rowdata, filename) {
    if (currdata != null) {
        switch (filename) {
            case "BPM_IsDepositPay":
                currdata.BPM_IsDepositPay = checkbox.checked;
                break;
            case "BPM_IsUseCheckout":
                currdata.BPM_IsUseCheckout = checkbox.checked;
                break;

        }
    }
    else {
        if (checkbox.checked) {
            checkbox.checked = false;
        }
        else {
            checkbox.checked = true;
        }
    }
}

//展示编辑数据行
function EditorDataRow(tableid, rowindex, actionname, buttonid, buttontxt) {
    $(tableid).treegrid(actionname, rowindex);
}
//展示编辑数据行
function AddDataRow(tableid, data) {
    var copydata = {};
    ArrayCopye(copydata, data);
    copydata.BPM_PayMentName = null;
    $(tableid).datagrid('appendRow', copydata);
}


//查询支付方式信息
function Selction(divid, orderguid, FeiyongType) {
    //发送Post请求, 返回后执行回调函数.
    var paramdata = { "ActionName": 'SelAll', "PageInfo": $.toJSON(_kbpage) };
    var Urlstr = "../ActionHanlder/Basic/BasicPaymentHalder.ashx";
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

//增加客户用品信息
function AddOrUpdate(data) {
    //发送Post请求, 返回后执行回调函数.
    data.ActionName = 'AddORUpdate';
    var paramdata = data;
    var Urlstr = "../ActionHanlder/Basic/BasicPaymentHalder.ashx";
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

//删除客户用品信息
function DeleteRow(datagrid, rowindex) {
    //发送Post请求, 返回后执行回调函数.
    var rows = $(datagrid).datagrid('getRows');
    if (rows != null) {
        var data = rows[rowindex];
        data.ActionName = 'Delete';
        var paramdata = data;
        var Urlstr = "../ActionHanlder/Basic/BasicPaymentHalder.ashx";
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
           },
         "json");
    }
}

function RefreshDataGrid() {
    var seletab = $('.easyui-tabs').tabs('getSelected');
    var index = $('.easyui-tabs').tabs('getTabIndex', seletab);
    $('.easyui-tabs').tabs('select', index);
}
//#endregion
