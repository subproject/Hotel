<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberCardRecharge.aspx.cs"
    Inherits="Member_MemberCardRecharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员卡充值</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/jscript">
        function dateFormat(jsondate) {
            try {
                jsondate = jsondate.replace("/Date(", "").replace(")/", "");
                if (jsondate.indexOf("+") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("+"));
                } else if (jsondate.indexOf("-") > 0) {
                    jsondate = jsondate.substring(0, jsondate.indexOf("-"));
                }
                var date = new Date(parseInt(jsondate, 10));
                var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
                var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
                return date.getFullYear() + "-" + month + "-" + currentDate + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
            }
            catch(Error)  {
                return jsondate;
            }
        }
           

        function FkFsFormat( str) {
            if (str == "Cash") {
                return "现金";
            }
            else  if (str == "Debit card") {
               
                return "借记卡";
            }
            else if (str == "Free" || str == "") {
                return "免费";
            }
            
                return str;
             
        }

       
        $(function () {
            var first = $("#ActualCharge");
            var second = $("#ScorePercent");
            var third = $("#ChargeMoney");
            var four = $("#MemberCardNo");
            first.change(function () {

                var num1 = first.val(); // 取得first对象的值  
                ;
                var num2 = second.val(); // 取得second对象的值  
                
                var sum = (num1 - 0) * (num2 - 0);
                third.val(sum);
            });
            four.change(function () {
                $.post('MemberChargeData.aspx?action=getUserInfoByNo', { MemberCardNo: four.val() }, function (result) {

                    if (!result.errorMsg) {
                        $('#userfm').form('load', result.rows);

                        
                        $('#tbR').datagrid({ url: 'MemberChargeData.aspx?action=readByID&id=' + "" + result.rows.ID + "" });
                        // reload the user data
                    } else {
                        $.messager.show({   
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    }
                }, 'json');
            });
        });

    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <!--会员信息-->
    <div class="easyui-panel" title="会员信息">
        <div style="padding: 10px 10 10px 10px">
        <form id="userfm" method="post"     onkeydown="if(event.enterKey){return false}">
            <table id="userTB" >
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    卡号：
                                </td>
                                <td>
                                    <input  type="text" name="MemberCardNo" id="MemberCardNo"     size="20" />
                                </td>
                                <td>
                                    <button style="margin: 2px; width: 50px; height: 20px" >
                                        读卡 </button>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    充值金额：
                                </td>
                                <td>
                                    <input type="test" readonly="readonly" value="0" name="ChargeMoney" id="ChargeMoney" ></input>
                                </td>
                                 <td style="width: 80px;" align="left">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="saveReCharge()">充值</a>
                    </td>
                            </tr>
                            <tr>
                                <td>
                                    实收金额：
                                </td>
                                <td>
                                      
                                  <input  type="text"  value="0"  name="ActualCharge"  id="ActualCharge"
                          />
                                   <%-- <input class="easyui-numberbox" value="0"  name="ActualCharge" data-options="precision:2,groupSeparator:','">
                                --%>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    付款方式：
                                </td>
                                <td>
                                    <select class="easyui-combobox" name="FkFs" id="FkFs" style="width: 140px">
                                        <option value="Cash">现金</option>
                                        <option value="Debit card">借记卡</option>
                                        <option value="Free">免费</option>
                                    </select>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    备注：
                                </td>
                                <td>
                                    <textarea name="message" style="height: 60px; width: 140px"></textarea>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="padding: 10px">
                        <div class="easyui-panel" style="padding: 10px">
                            <table>
                                <tr>
                                    <td width="80px">
                                        姓名：
                                    </td>
                                    <td>
                                        <input class="easyui-validatebox" type="text" readonly="readonly" name="MemberName" id="MemberName" size="20" />
                                    </td>
                                    <td width="80px">
                                        身份证：
                                    </td>
                                    <td>
                                        <input class="easyui-validatebox" type="text" readonly="readonly" id="IdCard" name="IdCard" size="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        状态：
                                    </td>
                                    <td>
                                     <input class="easyui-validatebox" type="text" readonly="readonly" name="Status" size="20" />
                                       <%-- <select class="easyui-combobox" name="Status" readonly="readonly" style="width: 60px">
                                            <option value="1">正常</option>
                                            <option value="2">挂失</option>
                                            <option value="0">注销</option>
                                        </select>--%>
                                    </td>
                                    <td>
                                        类型：
                                    </td>
                                    <td>
                                     <input class="easyui-validatebox" type="text" readonly="readonly" name="CardType"  id="CardType" size="20" />
                                    
                                      <%--  <select class="easyui-combobox" name="CardType" style="width: 140px">
                                            <option value="普通卡" selected>普通卡</option>
                                        </select>--%>
                                    </td>
                                    <tr>
                                        <td>
                                            比例：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox"  readonly="readonly" type="text" name="ScorePercent"  id="ScorePercent" size="20" />
                                        </td>
                                        <td>
                                            余额：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox" type="text"  readonly="readonly" name="RestCharge" id="RestCharge" size="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            累计充值：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox"  readonly="readonly" type="text" name="Charge" id="Charge" size="20" />
                                        </td>
                                        <td>
                                            累计消费：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox"  readonly="readonly" type="text" name="Score"  id="Score" size="20" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            积分：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox" type="text" name="RestScore"  id="RestScore" size="20" />
                                        </td>
                                        <td>
                                            消费次数：
                                        </td>
                                        <td>
                                            <input class="easyui-validatebox" type="text" name="Times" size="20" />
                                        </td>
                                    </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </form>
            
        </div>
    </div>
    <!--充值记录-->
     <div id="dgtbr">

            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="printChargeOld()">打印</a>  
        </div>
    <div class="easyui-panel" title="充值记录" buttons="#blgbuttons">
        <table id="tbR" class="easyui-datagrid" style="padding: 0px;" pagination="true" rownumbers="true"
            fitcolumns="true" singleselect="true" toolbar="#dgtbr">
            <thead>
                <tr>
                    <th data-options="field:'CardNo',width:30">
                        卡号
                    </th>
                     <th data-options="field:'MemberName',width:30">
                        姓名
                    </th>
                    <th data-options="field:'ChargeMoney',width:20">
                        充值金额
                    </th>
                    <th data-options="field:'ActualCharge',width:20">
                        实收金额
                    </th>
                    <th data-options="field:'FkFs',formatter:FkFsFormat,width:40,align:'center'">
                        付款方式
                    </th>
                    <th data-options="field:'CurTime',formatter:dateFormat,width:60,align:'center'">
                        充值时间
                    </th>
                    <th data-options="field:'s_cz',width:50">
                        收银员
                    </th>
                    <th data-options="field:'Memo',width:80,align:'center'">
                        备注
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <script type="text/javascript">
        function request(paras) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            var paraObj = {}
            for (i = 0; j = paraString[i]; i++) {
                paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
            }
            var returnValue = paraObj[paras.toLowerCase()];
            if (typeof (returnValue) == "undefined") {
                return "";
            } else {
                return returnValue;
            }
        }
        var  chgmoney,acharge,fkfangs,ddtime;
        function printChargeOld() {
            var row = $("#tbR").datagrid("getSelected");
            if (row) {
                acharge = row.ActualCharge;
                chgmoney = row.ChargeMoney;
                fkfangs = FkFsFormat(row.FkFs);
                ddtime = dateFormat(row.CurTime);
                window.open("MemberCardRechargePrint.html", "print");
            }
            
        }
        
        function printCharge() {
            window.open("MemberCardRechargePrint.html", "print");  
        }
        
        var ids = request("id");
        $(document).ready(function () {
            if (ids == null || ids == "")
                return;
            $('#tbR').datagrid({ url: 'MemberChargeData.aspx?action=readByID&id=' + "" + ids + "" });

            setUseInfo(ids);

        });

        function setUseInfo(ids) {
          
            
            $.post('MemberChargeData.aspx?action=getUserInfo', { id: ids }, function (result) {
                  
                if (!result.errorMsg) {
                   $('#userfm').form('load', result.rows);
                   
                   
                    // reload the user data
                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
        }
        function setUseInfo() {
           
           
            $.post('MemberChargeData.aspx?action=getUserInfo', { id: ids }, function (result) {

                if (!result.errorMsg) {
                  
                    $('#userfm').form('load', result.rows);
                  
                    
                    
                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
        }
        function regetUserInfos() { 
           
            var cards = $('#MemberCardNo').val();

            $.post('MemberChargeData.aspx?action=regetUserInfos', { CardNos: cards }, function (result) {
               
                if (!result.errorMsg) {
                    
                    $('#userfm').form('load', result.rows);
                   
                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
        }
         function getPrintData() {


             // var rows = $("#dg").datagrid("getSelected");
          // var dtime
            return {
                data_1: $("#MemberName").val(),
                data_2: '0000001', 
                data_3: $("#CardType").val(),
                data_4: $("#MemberCardNo").val(),
                data_5: chgmoney,// $("#ChargeMoney").val(),
                data_6: acharge,//$("#ActualCharge").val(),
                data_7: $("#RestCharge").val(),
                data_8: FkFsFormat(fkfangs), //$('#FkFs').datebox('getValue'),
                data_9: 'System',
                data_10: ddtime 
                
            };
        }
        function saveReCharge() {
            var first = $("#Charge");
            var second = $("#ChargeMoney");
            var num1 = first.val(); // 取得first对象的值                         
            var num2 = second.val(); // 取得second对象的值  

            $('#userfm').form('submit', {
                url: 'MemberChargeData.aspx?action=create&id=' + "" + ids + "",
                onSubmit: function () {
                    var four = $("#MemberCardNo");
                    $.post('MemberChargeData.aspx?action=getUserInfoByNo', { MemberCardNo: four.val() }, function (result) {

                        if (!result.errorMsg) {
                            $('#userfm').form('load', result.rows);

                            $('#tbR').datagrid({ url: 'MemberChargeData.aspx?action=readByID&id=' + "" + result.rows.ID + "" });
                           
                        } else {
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        }
                    }, 'json');

                    //  return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        //$('#dlg').dialog('close');        // close the dialog
                        //$('#tbR').datagrid('reload');    //                        
                        var third = $("#RestScore");
                        var four = $("#RestCharge");

                        var sum = (num1 - 0) + (num2 - 0);
                        var sum2 = ($("#RestScore").val() - 0) + (num2 - 0); //积分                       
                        first.val(sum);
                        four.val(sum);
                        //third.val(sum2);
                      //  $('#tbR').datagrid({ url: 'MemberChargeData.aspx?action=readByID&id=' + "" + ids + "" });
                        //$('#tbR').datagrid('reload');          
                        $.messager.confirm('提醒', '是否打印充值收款单?', function (r) {
                            if (r) {
                                acharge = $("#ActualCharge").val();
                                chgmoney = $("#ChargeMoney").val();
                                fkfangs = $('#FkFs').datebox('getValue');
                                ddtime == new Date().toDateString();

                                printCharge();
                            }
                        });
                    }
                }
            });
        }
    </script>
</body>
</html>
