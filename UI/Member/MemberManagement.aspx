<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberManagement.aspx.cs"
    Inherits="Member_MemberManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <META HTTP-EQUIV="Cache-Control" CONTENT="no-cache">
    <title>会员管理</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../themes/demo.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
     <script type="text/javascript" src="../dateChange.js"></script>
    <script type="text/javascript">
        function myformatter(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1
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

        function dateFormat(jsondate) {
            // 将   /Date(1380124800000)/  形式的数据格式化
            //            value = value.replace(/\/Date\((-?\d+)\)\//, '$1');            
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

        function sexFormat(sex) {
            //sex = sex.trim();
            if (sex.toUpperCase() == 'M') {
                return "男";
            } else {
                return "女";
            }
        }

        function CardStatusFormat(sta) {
            if (sta == '1') {
                return "正常";
            }
            else {
                if (sta == '2') {
                    return "挂失";
                }
                else {
                    return "注销";
                }
            }
        }

        function IsValidateFormat(Val) {
            if (Val == '1') {
                return "有限制";
            }
            else {
                return "无限制";
            }
        }

        $.extend($.fn.validatebox.defaults.rules, {
            /*必须和某个字段相等*/
            equalTo: { validator: function (value, param) { return $(param[0]).val() == value; }, message: '字段不匹配' }
        });

        var aCity={11:"北京",12:"天津",13:"河北",14:"山西",15:"内蒙古",21:"辽宁",22:"吉林",23:"黑龙江",31:"上海",32:"江苏",33:"浙江",34:"安徽",35:"福建",36:"江西",37:"山东",41:"河南",42:"湖北",43:"湖南",44:"广东",45:"广西",46:"海南",50:"重庆",51:"四川",52:"贵州",53:"云南",54:"西藏",61:"陕西",62:"甘肃",63:"青海",64:"宁夏",65:"新疆",71:"台湾",81:"香港",82:"澳门",91:"国外"}

        function cidInfo(sId){
            if(sId.length==15)
            {
                    sId = sId.replace(/([\d]{6})(\d{9})/,"$119$2x");
            }
            var iSum=0
            var info=""
            if(!/^\d{17}(\d|x)$/i.test(sId))return false;
            sId=sId.replace(/x$/i,"a");
            if (aCity[parseInt(sId.substr(0, 2))] == null) return false;
            sBirthday=sId.substr(6,4)+"-"+Number(sId.substr(10,2))+"-"+Number(sId.substr(12,2));
            var d=new Date(sBirthday.replace(/-/g,"/"))
            if (sBirthday != (d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate())) return false;
            for (var i = 17; i >= 0; i--) iSum += (Math.pow(2, i) % 11) * parseInt(sId.charAt(17 - i), 11)
            if (iSum % 11 != 1) return false;
            return true;
        }
        $(function () {
            $("#IdNo").change(function () {
                if (cidInfo($("#IdNo").val())) {
                    getIdInfos();
                }
            });

        });

        function getIdInfos() {
            var IdResult;
            var xmlhttp;
            var strID = $("#IdNo").val();
            $("#BirthDay").datebox("setValue", showBirthday(strID));
            $.post('MemberChargeData.aspx?action=getAddress', { id: strID.substr(0, 6) }, function (result) {
                if (!result.errorMsg) {                    
                    $("#Address").val(result);
                } else {
                    $.messager.show({    // show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            }, 'json');
         
        }

        function showBirthday(val) {
            var birthdayValue;
            if (15 == val.length) { //15位身份证号码
                birthdayValue = val.charAt(6) + val.charAt(7);
                if (parseInt(birthdayValue) < 10) {
                    birthdayValue = '20' + birthdayValue;
                }
                else {
                    birthdayValue = '19' + birthdayValue;
                }
                birthdayValue = birthdayValue + '-' + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11);

                return birthdayValue;
            }
            if (18 == val.length) { //18位身份证号码
                birthdayValue = val.charAt(6) + val.charAt(7) + val.charAt(8) + val.charAt(9) + '-' + val.charAt(10) + val.charAt(11)

   + '-' + val.charAt(12) + val.charAt(13);


                return birthdayValue;

            }
            return "";
        }
        
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="会员管理">
        <table id="dg" class="easyui-datagrid" style="padding: 0px; width: 2500px; height: 455px"
            url="MemberData.aspx?action=read" toolbar="#toolbar" pagination="true" rownumbers="true"
            fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'ID',width:100">
                        序号
                    </th>
                    <th data-options="field:'MemberNo',width:100">
                        会员编号
                    </th>
                    <th data-options="field:'MemberName',width:100">
                        会员姓名
                    </th>
                    <th data-options="field:'MemberCardNo',width:100">
                        会员卡号
                    </th>
                    <th data-options="field:'CardType',width:100">
                        会员卡类型
                    </th>
                    <th data-options="field:'IdCard',width:100">
                        身份证
                    </th>
                    <th data-options="field:'Sex',formatter:sexFormat,width:100">
                        性别
                    </th>
                    <th data-options="field:'BirthDay',width:100">
                        出生年月
                    </th>
                    <th data-options="field:'HomeTelphone',width:100">
                        家庭电话
                    </th>
                    <th data-options="field:'Mobile',width:100">
                        移动电话
                    </th>
                    <th data-options="field:'Address',width:100">
                        通信地址
                    </th>
                    <th data-options="field:'IsValidate',formatter:IsValidateFormat,width:100">
                        是否有限制
                    </th>
                    <th data-options="field:'Validate',formatter:dateFormat,width:100">
                        有效期
                    </th>
                    <th data-options="field:'Password',width:100">
                        密码
                    </th>
                    <th data-options="field:'Status',formatter:CardStatusFormat,width:100">
                        状态
                    </th>
                    <th data-options="field:'RegTime',formatter:dateFormat,width:100">
                        注册时间
                    </th>
                    <th data-options="field:'Charge',width:100">
                        充值金额
                    </th>
                    <th data-options="field:'RestCharge',width:100">
                        剩余金额
                    </th>
                    <th data-options="field:'Score',width:100">
                        积分
                    </th>
                    <th data-options="field:'Times',width:100">
                        入住次数
                    </th>
                    <th data-options="field:'ScorePercent',width:100">
                        积分率
                    </th>
                    <th data-options="field:'UseScore',width:100">
                        已使用积分
                    </th>
                    <th data-options="field:'RestScore',width:100">
                        剩余积分
                    </th>
                    <th data-options="field:'Discount',width:100">
                        折扣
                    </th>
                    <th data-options="field:'UserName',width:100">
                        操作员
                    </th>
                </tr>
            </thead>
        </table>
    </div>
    <div id="toolbar">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
            onclick="newMember()">注册会员</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                iconcls="icon-edit" plain="true" onclick="editMember()">编辑会员信息</a> <a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="ReCharge()">充值</a><a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="guashi()">挂失</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" plain="true"
            onclick="guashiCancel()">取消挂失</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                iconcls="icon-edit" plain="true" onclick="checkin()">注销</a> <a href="javascript:void(0)"
                    class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="checkout()">
                    续卡</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit"
                        plain="true" onclick="buka()">补卡</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                            iconcls="icon-remove" plain="true" onclick="destroyMember()">删除</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-reload" plain="true"
            onclick="">礼品兑换</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 400px;" closed="true"
        buttons="#dlg-buttons">
        <div style="padding: 10px 0 10px 10px">
            <form id="fm" method="post">
            <table>
                <tr>
                    <td>
                        卡号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberCardNo" data-options="required:true" />
                    </td>
                    <td>
                        <button style="margin: 2px; width: 50px; height: 20px">
                            读卡</button>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        卡类型：
                    </td>
                    <td>
                     <input class="easyui-combobox"  id="CardType" name="CardType" data-options="valueField:'CardType',textField:'CardType',url:'MemberData.aspx?action=readCardType'">
          
                       <%-- <select class="easyui-combobox" name="CardType" style="width: 140px">
                            <option value="普通卡" selected>普通卡</option>
                        </select>--%>
                    </td>
                    <td>
                        状态:
                    </td>
                    <td>
                        <select class="easyui-combobox" name="Status" style="width: 60px" readonly="readonly">
                            <option value="1" selected>正常</option>
                            <option value="2">挂失</option>
                            <option value="0">注销</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        身份证号码：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="IdCard" data-options="required:true"
                            id="IdNo" />
                    </td>
                    <td>
                        <button style="margin: 2px; width: 80px; height: 20px"  >
                            读身份证</button>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="margin-right: 10px">
                        会员编号：
                    </td>
                    <td><div id="divParent" style="margin:0 0;">
                        <input class="easyui-validatebox" type="text" name="MemberNo"  readonly="readonly"
                          /></div>
                          <input  type="test" name="MemberNo2"  id="MemberNo2" readonly="readonly"
                          />
                          
                    </td>
                    <td style="margin-right: 10px">
                        会员姓名：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberName" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        密码：
                    </td>
                    <td>
                     <input type="password" name="Password" validtype="length[1,32]" class="easyui-validatebox"
                             id="password" value="" />
                       <%-- <input type="password" name="Password" validtype="length[1,32]" class="easyui-validatebox"
                            required="true" id="password" value="" />--%>
                        <%-- <input class="easyui-validatebox" type="text" name="Password" data-options="required:true" />--%>
                    </td>
                    <td>
                        确认密码：
                    </td>
                    <td>
                      <input type="password" name="Password02" id="repassword"   class="easyui-validatebox"
                            validtype="equalTo['#password']" invalidmessage="两次输入密码不匹配" />
                      <%--  <input type="password" name="Password02" id="repassword" required="true" class="easyui-validatebox"
                            validtype="equalTo['#password']" invalidmessage="两次输入密码不匹配" />--%>
                        <%--<input class="easyui-validatebox" type="text" name="Password02" data-options="required:true" />--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别：
                    </td>
                    <td>
                        <select class="easyui-combobox" name="Sex" id="Sex" style="width: 60px">
                            <option value="m" selected>男</option>
                            <option value="f">女</option>
                        </select>
                    </td>
                    <td>
                        生日：
                    </td>
                    <td>
                        <input class="easyui-datebox" name="BirthDay" id="BirthDay" type="text"  />
                    </td>
                </tr>
                <tr>
                    <td>
                        家庭电话：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="HomeTelphone" />
                    </td>
                    <td>
                        移动电话：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="Mobile" />
                    </td>
                </tr>
                <tr>
                    <td >
                        通信地址：
                    </td>
                    <td>
                          <input class="easyui-validatebox"  hidden  type="text" name="Address2" id="Address2" />
                   
                        <input class="easyui-validatebox" type="text" name="Address" id="Address" />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        总积分：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="Score" id="Score" />
                    </td>
                    <td id="shengyujifen">
                        剩余积分：
                    </td>
                    <td> 
                        <input class="easyui-validatebox" type="text" name="RestScore" id="RestScore">
                    </td>
                </tr>
                <tr>
                    <td>
                        是否限制：
                    </td>
                    <td>
                        <select class="easyui-combobox" name="IsValidate" style="width: 140px">
                            <option value="0" selected>无限制</option>
                            <option value="1">有限制</option>
                        </select>
                    </td>
                    <td>
                        有效时间
                    </td>
                    <td>
                        <input class="easyui-datebox" name="Validate" data-options="formatter:myformatter,parser:myparser"
                             ></input>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
</script>
            </form>
        </div>
    </div>
    <div id="xuka" class="easyui-dialog" style="width: 300px; height: 200px;" closed="true"
        buttons="#xuka-buttons">
        <div>
            <form id="xukafm" method="post">
            <table>
                <tr>
                    <td>
                        卡号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberCardNo" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td style="margin-right: 10px">
                        会员姓名：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberName" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        是否限制：
                    </td>
                    <td>
                        <select class="easyui-combobox" name="IsValidate" style="width: 140px">
                            <option value="0" selected>无限制</option>
                            <option value="1">有限制</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        有效时间
                    </td>
                    <td>
                        <input class="easyui-datebox" name="Validate" data-options="formatter:myformatter,parser:myparser"
                             ></input>
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
</script>
            </form>
        </div>
    </div>
    <div id="buka" class="easyui-dialog" style="width: 300px; height: 200px;" closed="true"
        buttons="#buka-buttons">
        <div>
            <form id="bukafm" method="post">
            <table>
                <tr>
                    <td style="margin-right: 10px">
                        会员姓名：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberName" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        原卡号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberCardNo" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        新卡号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="NewMemberCardNo"  data-options="required:true" />
                    </td>
                </tr>
            </table>
            <script type="text/javascript">
</script>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMember()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>
    <div id="xuka-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveXuka()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#xuka').dialog('close')">取消</a>
    </div>
     <div id="buka-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveBuka()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#buka').dialog('close')">取消</a>
    </div>
    <script type="text/javascript">
        var url;
        function newMember() {
            $('#dlg').dialog('open').dialog('setTitle', '新会员注册');
            $('#fm').form('clear');
            $('#fm').form('reset');

            url = 'MemberData.aspx?action=create';
            $('#RestScore').hide();
            $('#shengyujifen').hide(); 
            $('#Address').hide();
            $('#Address2').show();
            $("#divParent").css("display", "none");
            getmemebernolast();
        }
        function checkPwd() {
            $("#Password02").blur(function () {
                if ($(this).val() != $("#Password").val()) {
                    alert("你输入的密码有误!");
                    return false;
                }
            })
        }
        function editMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑会员信息');
                $('#fm').form('load', row);
                url = 'MemberData.aspx?action=update&id=' + row.ID;
                $("#divParent").css("display", "block");
                $('#MemberNo').show();
                $('#RestScore').show();
                $('#shengyujifen').show(); 
                $('#MemberNo2').hide();
                $('#Address2').hide();
                $('#Address').show();
            }
        }
        function saveMember() {
            $('#fm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
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
        function destroyMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('MemberData.aspx?action=delete', { id: row.ID }, function (result) {

                            if (!result.errorMsg) {
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

        function guashi() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.Status == 1) {
                    $.messager.confirm('Confirm', '确认挂失?', function (r) {
                        if (r) {
                            $.post('MemberData.aspx?action=guashi', { id: row.ID }, function (result) {

                                if (!result.errorMsg) {
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
                else {
                    if (row.Status == 2) {
                        alert("当前用户已挂失，不能再次挂失！")
                    }
                    if (row.Status == 0) {
                        alert("当前用户已注销，不能挂失！")
                    }

                }
            }
        }

        function guashiCancel() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.Status == 2) {
                    $.messager.confirm('Confirm', '确认取消挂失?', function (r) {
                        if (r) {
                            $.post('MemberData.aspx?action=guashiCancel', { id: row.ID }, function (result) {

                                if (!result.errorMsg) {
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
                else {
                    alert("当前用户非挂失状态，不能取消挂失！")
                }
            }
        }

        function checkin() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.Status != 0) {
                    $.messager.confirm('Confirm', '确认注销?', function (r) {
                        if (r) {
                            $.post('MemberData.aspx?action=checkin', { id: row.ID }, function (result) {

                                if (!result.errorMsg) {
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
                else {
                    alert("当前用户已注销，不能再次注销！")
                }
            }
        }

        function checkout() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#xuka').dialog('open').dialog('setTitle', '续卡');
                $('#xukafm').form('load', row);
                url = 'MemberData.aspx?action=xuka&id=' + row.ID;
            }

        }

        function saveXuka() {
            $('#xukafm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#xuka').dialog('close');        // close the dialog
                        $('#dg').datagrid('reload');    // reload the user data
                    }
                }
            });
        }

        function buka() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.Status == 2) {
                    $('#buka').dialog('open').dialog('setTitle', '补卡');
                    $('#bukafm').form('load', row);
                    url = 'MemberData.aspx?action=buka&id=' + row.ID;
                }
                else {
                    $('#bukafm').form('clear');
                    alert("只有挂失状态的用户才能补卡！");
                }
            }
        }

        function saveBuka() {
            $('#bukafm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    //var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#buka').dialog('close');        // close the dialog
                        $('#dg').datagrid('reload');    // reload the user data
                    }
                }
            });
        }

        function ReCharge() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                if (row.Status == 0) { 
                    alert("该用户已注销，不能继续充值！")
                }

                if (row.Status == 2) {
                    alert("该用户已注销，不能继续充值！")
                }
                if (row.Status == 1) {
                     
                    window.open('MemberCardRecharge.aspx?id=' + row.ID, '500', '800');
                }
                
            }
        }

        function pad(num, n) {
            var i = ('' + num).length;
            while (i++ < n) num = '0' + num;
            return num;
        }
        function getmemebernolast() {

            $.ajax({
                type: "get",
                url: "MemberData.aspx?action=getLastMemberNo",
                success: function (result) {
                    var result = eval('(' + result + ')');
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {    

                        $('#MemberNo2').show();
                        $('#MemberNo2').val(pad(result.MemberNo,10));
                        $('#MemberNo').hide();

                    }
                },
                error: function (result) {
                    $.messager.show({// show error message
                        title: 'Error',
                        msg: result.errorMsg
                    });
                }
            });
        }
    </script>
    <!--<div data-options="region:'west',split:true" style="width: 150px; padding: 10px;">
        <p style="font-weight: bolder">
            选择会员：</p>
        <input class="easyui-combobox" style="width: 120px" data-options="valueField:'id',textField:'text'">
        <p style="height: 10px">
            &nbsp;</p>
        <button style="margin: 2px; width: 120px; height: 30px">
            积分调整</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            充 值</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            会员查询</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            挂 失</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            取消挂失</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            补 卡</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            注 销</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            删 除</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            续 卡</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            积分兑换</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            设置密码</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            积分兑换</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            会员注册</button>
        <button style="margin: 2px; width: 120px; height: 30px">
            退 出</button>
    </div>-->
</body>
</html>
