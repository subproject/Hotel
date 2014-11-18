<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCardManager.aspx.cs" Inherits="MemberCardManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员卡设置</title>
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache">
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
     <script type="text/javascript" src="../dateChange.js"></script>
      <script type="text/javascript" src="../Hotelmgr.js"></script>
       <style type="text/css">
        #xyfm
        {
            margin: 0;
            padding: 10px 30px;
        }
        .ftitle
        {
            font-size: 14px;
            font-weight: bold;
            padding: 5px 0;
            margin-bottom: 10px;
            border-bottom: 1px solid #ccc;
        }
        .fitem
        {
            margin-bottom: 5px;
        }
        .fitem label
        {
            display: inline-block;
            width: 80px;
        }
    </style>
    <script type="text/javascript">
        function myformatterDa(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        function myformatter(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        function myparser(s) {
            if (!s) return new Date();
            var ss = (s.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                return new Date(y, m - 1, d);
            } else {
                return new Date();
            }
        }
 
        function myformatter(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        function myparser(s) {
            if (!s) return new Date();
            var ss = (s.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                return new Date(y, m - 1, d);
            } else {
                return new Date();
            }
        } 
           
 
        
    </script>
</head>
<body class="easyui-layout">
   <div data-options="region:'center'" style="padding: 1px">
        <div title="会员卡设置" class="easyui-panel">
        </div>
        <table id="dg" class="easyui-datagrid" style="padding: 0px" url="MemberData.aspx?action=CardDesign"
            toolbar="#dgtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'id',width:100,hidden:true">
                        ID
                    </th>
                    <th data-options="field:'CardType',width:100">
                        卡类型
                    </th>
                 <%--   <th data-options="field:'RequestScore',width:100">
                        启用折扣所需积分
                    </th>--%>
                 <%--   <th data-options="field:'DiscountType',width:100">
                        折扣类型
                    </th>
                    <th data-options="field:'Discount',width:100">
                        折扣(%)
                    </th>editor:editor:{type:'checkbox',options:{on:'ture',off:''}} {type:'checkbox',options:{on:'',off:'true'}}--%>
                    <th data-options="field:'CheckOutDelay',width:100">
                        退房延时(分)
                    </th>
                    <th data-options="field:'IsCharge2',width:100">
                     
                        是否储值
                    </th>
                    <th data-options="field:'ChargePercent',width:100">
                        储值比例
                    </th>
                    <th data-options="field:'YePrompt',width:100">
                        提醒，当余额不足
                    </th>
                    <th data-options="field:'IsAutoUpLevel2',width:100">
                        自动升级
                    </th>
                    <th data-options="field:'IsAutoDownLevel2',width:100">
                        自动降级
                    </th>
                     <th data-options="field:'LowPoint',width:100">
                        积分区间(低)
                    </th>
                     <th data-options="field:'HighPoint',width:100">
                        积分区间(高)
                    </th>

                </tr>
            </thead>
        </table>
        <div id="dgtbr">

            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newPartner()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editPartner()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyPartner()">
                        删除</a>
        </div>
        <div id="Div1">
        <input name="yxdr" id="yxdr" type="checkbox"   >允许多人使用会员卡</input>
        <input name="yxwk" id="yxwk" type="checkbox" >允许无卡消费</input>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                onclick="jfsz()">积分设置</a>&nbsp;&nbsp;&nbsp; <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="pointToGift()">积分换礼设置</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="RoomDesign()">
                        折扣设置</a>
        </div>
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 10px 20px"
            closed="true" buttons="#dlgbuttons">
            <form id="fm" method="post" novalidate>
            <div class="fitem">
                <label>
                   卡类型:</label>
                    <input name="id" id="id" type="hidden"  class="easyui-validatebox" >
                <input name="CardType" id="CardType"  class="easyui-validatebox" required="true">
            </div>
             
           <%-- <div class="fitem">
                <label>
                    启用折扣所需积分:</label>
                <input name="Contact" class="easyui-validatebox">
            </div>--%>
            <div class="fitem">
                <label>
                    退房延时(分):</label>
                <input name="CheckOutDelay" id="CheckOutDelay" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                      是否储值:</label>
                <input name="IsCharge"  id="IsCharge" class="easyui-validatebox"  type="checkbox" >
            </div>
            <div class="fitem">
                <label>
                    储值比列:</label>
                <input name="ChargePercent"  id="ChargePercent" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                    提醒，当余额不足:</label>
                <input name="YePrompt" id="YePrompt" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                    自动升级:</label>
                <input name="IsAutoUpLevel" id="IsAutoUpLevel" type="checkbox" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                    自动降级:</label>
                <input name="IsAutoDownLevel"  id="IsAutoDownLevel" type="checkbox" class="easyui-validatebox" >
            </div>
            <div class="fitem">
                <label>
                    积分区间(低):</label>
                <input name="LowPoint" id="LowPoint" class="easyui-validatebox">
            </div>
            <div class="fitem">
                <label>
                      积分区间(高):</label>
                <input name="HighPoint" id="HighPoint" class="easyui-validatebox">
            </div>
 
      
        </form>
    </div>
               <div id="dlgbuttons">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePartner()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

        <div id="dlgbuttons2">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePointDesign()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#Div2').dialog('close')">取消</a>
    </div>
    
      
 <%--积分设置--%>
        <div id="Div2" class="easyui-dialog" style="width: 400px; height: 350px; padding: 10px 20px"
            closed="true" buttons="#dlgbuttons2">
            
            <form id="Form1" method="post" >
            <div class="fitem">
               
                <input name="chkMoneyToPoint" id="chkMoneyToPoint" type="checkbox" class="easyui-validatebox" required="true"> 
                <label>
                   按消费金额积分,每消费</label>
                   <input name="xfjf" id="xfjf" class="easyui-validatebox">
                   <label>
                   元积1分</label>

            </div>
           <div class="fitem">
               
                <input name="chkDayToPoint" id="chkDayToPoint" type="checkbox" class="easyui-validatebox" required="true"> 
                <label>
                   按入住天数积分,每住一天积</label>
                   <input name="rzts" id="rzts" class="easyui-validatebox">
                   <label>
                   分</label>

            </div>
            <div class="fitem">
               
                <input name="chkPointToMoney" id="chkPointToMoney" type="checkbox" class="easyui-validatebox" required="true"> 
                <label>
                   积分抵扣消费金额，每一元抵扣</label>
                   <input name="jfdkje"  id="jfdkje" class="easyui-validatebox">
                   <label>
                   积分</label>

            </div>
            <div class="fitem">
               
                <input name="chkPromptFreeRoom" id="chkPromptFreeRoom" type="checkbox" class="easyui-validatebox" required="true"> 
                <label>
                   入住登记时自动提示积分免费房</label>
                 <%--  
                   <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
                onclick="newPoint()">设置</a>--%>

            </div>
            
        </form>
    </div>
   

  

    

    <script type="text/javascript">

        $(function () {
            $.post('MemberChargeData.aspx?action=getAllowDesign', null, function (result) {

                if (!result.errorMsg) {

                    if (result.allowMultiUseCard) {
                        $("#yxdr").attr("checked", 'checked');
                    } else {

                        $("#yxdr").attr("checked", false);
                    }

                    if (result.allowNoCardConsume) {
                        $("#yxwk").attr("checked", 'checked');
                    } else {
                        $("#yxwk").attr("checked", false);
                    }

                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');

            if ($.browser.msie) {
                $('yxdr').click(function () {
                    savedesign();
                });
                $('yxwk').click(function () {
                    savedesign();
                });
            };
            $("#yxdr").change(function () {
                savedesign();
            });
            $("#yxwk").change(function () {
                savedesign();
            });
        });

        function savedesign() {

            $.post('MemberChargeData.aspx?action=saveAllowDesign', { yxdr: $("#yxdr").is(':checked'), yxwk: $("#yxwk").is(':checked') }, function (result) {

                if (!result.errorMsg) {
//                    if (result.allowMultiUseCard) {
//                        $("#yxdr").attr("checked", 'checked');
//                    } else {

//                        $("#yxdr").attr("checked", false);
//                    }

//                    if (result.allowNoCardConsume) {
//                        $("#yxwk").attr("checked", 'checked');
//                    } else {
//                        $("#yxwk").attr("checked", false);
//                    }

                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
        }

        var url;
        function RoomDesign() {
        var row = $('#dg').datagrid('getSelected');
        if (row){

            openwin(encodeURI('MemberRoomDesign.aspx?CardType=' + row.CardType ), '500', '800');
        }

    }

        function pointToGift() {

            openwin('MemberPointToGift.aspx', '500', '800');
        }

        function jfsz() {               
            url = 'MemberCardData.aspx?action=newPoint';
            $.ajax({
                type: "post",
                url: "MemberCardData.aspx?action=queryPointDesign",

                success: function (r) {
                    var r = eval('(' + r + ')');
                    var chkMoneyToPoint = $("#chkMoneyToPoint");
                    var chkPromptFreeRoom = $("#chkPromptFreeRoom");                  
                    var chkDayToPoint = $("#chkDayToPoint");
                    var chkPointToMoney = $("#chkPointToMoney");
                    var jfdkje = $("#jfdkje");
                    var rzts = $("#rzts");
                    var xfjf = $("#xfjf");
                    if (r.IsAlertPointToRoom) {
                        
                        chkPromptFreeRoom.attr("checked", 'checked');
                    } else {
 
                        chkPromptFreeRoom.attr("checked", false);
                    }
                    if (r.IsConsumePoint) {
                        chkMoneyToPoint.attr("checked", 'checked');
                    } else {
                        chkMoneyToPoint.attr("checked", false);
                    }
                    if (r.IsLiveDays) {
                        chkDayToPoint.attr("checked", 'checked');
                    } else {
                        chkDayToPoint.attr("checked", false);
                    }
                    if (r.IsPointToMoney) {
                        chkPointToMoney.attr("checked", 'checked');
                    } else {
                        chkPointToMoney.attr("checked", false);
                    }
                    xfjf.val(r.ByConsumePoint);
                    rzts.val(r.ByLiveDays);
                    jfdkje.val(r.PointToMoney);
                    $('#Div2').dialog('open').dialog('setTitle', '积分设置');
                },
                error: function (r) {
                    $.messager.show({// show error message
                        title: 'Error',
                        msg: r.errorMsg
                    });
                }
            });
 
        }
        
        function newPartner() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#fm').form('clear');
            url = 'MemberData.aspx?action=CardCreate';
        }
        function editPartner() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                $('#fm').form('load', row);
                if (row.IsCharge) {

                    $('#IsCharge').attr("checked", 'checked');
                } else {

                    $('#IsCharge').attr("checked", false);
                }

                if (row.IsAutoUpLevel) {

                    $('#IsAutoUpLevel').attr("checked", 'checked');
                } else {

                    $('#IsAutoUpLevel').attr("checked", false);
                }

                if (row.IsAutoDownLevel) {

                    $('#IsAutoDownLevel').attr("checked", 'checked');
                } else {

                    $('#IsAutoDownLevel').attr("checked", false);
                }
                url = 'MemberData.aspx?action=CardUpdate&id=' + row.id;
            }
        }
        function savePointDesign() {
            $('#Form1').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    var result = eval('(' + result + ')');
                    
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        
                        $('#Div2').dialog('close');        // close the dialog
                        
                        $.messager.show({// show error message
                            title: 'Sucess',
                            msg: "积分设置保存成功"
                        });
                    }
                }
            });
        }
        function savePartner() {
            $('#fm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#dlg').dialog('close');        // close the dialog
                        $('#dg').datagrid('reload');    // reload the user data
                    }
                }
            });
        }
        function destroyPartner() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('MemberData.aspx?action=CardDelete', { id: row.id }, function (result) {
                            if (result.success) {
                                $('#dg').datagrid('reload');
                                // reload the user data
                            } else {
                                $.messager.show({    // show error message
                                    title: 'Error',
                                    msg: result.errorMsg
                                });
                            }
                        }, 'json');
                    }
                });
            }
        }
    </script>
    </div>
</body>
</html>
