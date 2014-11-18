<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberQuery.aspx.cs" Inherits="Member_MemberQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>会员查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
     <script type="text/javascript" src="../dateChange.js"></script>
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
        function submitForm() {

            $('#dg').datagrid('load', {
              MemberName: $('#MemberName').val(),
                MemberCardNo: $('#MemberCardNo').val(),
                CardType: $('#CardType').datebox('getValue'),
                IdCard: $('#IdCard').val(),
                BirthDay: $('#BirthDay').datebox('getValue'),
                Validate: $('#Validate').datebox('getValue'), 
                HomeTelphone: $('#HomeTelphone').val(),
                Mobile: $('#Mobile').val(),
                Status: $('#Status').datebox('getValue'),
                Address: $('#Address').val(),
                RegTime: $('#RegTime').datebox('getValue')
            });
           
//            $('#ff').form('submit', {
//                url: url, 
//                onSubmit: function () {
//                    return $(this).form('validate');
//                },
//                success: function (result) {
//                    //var result = eval('(' + result + ')');
//                    if (result.errorMsg) {
//                        $.messager.show({
//                            title: 'Error',
//                            msg: result.errorMsg
//                        });
//                    } else {
//                        $('#dlg').dialog('close');        // close the dialog
//                        $('#dg').datagrid('reload');    // reload the user data
//                    }
//                }
//            });
        }


        
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="查询用户">
    

           <div id="ff" style="padding: 10px 10px 10px 10px">
             
            <table >
                <tr>
                    <td style="margin-right: 10px">
                        会员姓名:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberName" id="MemberName"></input>
                    </td>
                    <td>
                        会员卡号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MemberCardNo" id="MemberCardNo" >
                    </td>
                    <td>
                        <button style="margin: 2px; width: 50px; height: 20px">
                            读卡</button>
                    </td>
                    <td>
                        <select class="easyui-combobox"    id="CardType" name="CardType" style="width: 76px"  data-options="valueField:'CardType',textField:'CardType',url:'MemberData.aspx?action=readCardType'">
                           <%-- <option value="普通卡">普通卡</option>--%>
                    </td>
                </tr>
                <tr>
                    <td>
                        身份证号：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="IdCard" id="IdCard"></input>
                    </td>
                    <td>
                        生日：
                    </td>
                    <td>
                        <input class="easyui-datebox" id="BirthDay" name="BirthDay"   data-options="formatter:myformatter,parser:myparser"
                            >
                    </td>
                    <td>
                        有效期：
                    </td>
                    <td>
                        </select><input class="easyui-datebox" id="Validate" name="Validate"  data-options="formatter:myformatter,parser:myparser"
                            ></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        家庭电话：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="HomeTelphone"  id="HomeTelphone"></input>
                    </td>
                    <td>
                        移动电话:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="Mobile" id="Mobile">
                    </td>
                    <td>
                        状态：
                    </td>
                    <td>
                        <select class="easyui-combobox" name="Status"  id="Status" style="width: 69px">
                            <option value=""></option>
                            <option value="0">注销</option>
                            <option value="1">正常 </option>
                            <option value="2">挂失</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        联系地址：
                    </td>
                    <td>
                        <input class="easyui-validatebox"  type="text"  name="Address" id="Address">
                    </td>
                    <td>
                        注册日期：
                    </td>
                    <td>
                        <input class="easyui-datetimebox" id="RegTime" name="RegTime"  data-options="formatter:myformatter,parser:myparser" 
                              />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>

                 <tr>
                    <td  >
                        
                    </td>
                     <td  >
                        
                    </td>
                      <td  >
                        
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton"  onclick="submitForm()">查询</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton"  onclick="window.close();">关闭</a>
                    </td>
                    
                </tr>
            </table>
            
           
          <table style="padding: 0px 0px 0px 0px">
               
            </table>
              <div >
            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-undo" plain="true"
                onclick="">消费记录</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-sum"
                    plain="true" onclick="">充值记录</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                        iconcls="icon-reload" plain="true" onclick="">兑换查询</a> <a href="javascript:void(0)"
                            class="easyui-linkbutton" iconcls="icon-tip" plain="true" onclick="">详细信息</a>
        </div>
        </div>
      
  </div>
     

         <!--查询结果-->
    <div class="easyui-panel" style=" height:0;" >
        <table id="dg" class="easyui-datagrid" style="padding: 5px; width: 2500px; height: 566px"
              pagination="true" rownumbers="true"   fitcolumns="true"           
             singleselect="true" data-options="
				toolbar: '#ff',
                url:'MemberData.aspx?action=search'      
                ">
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
                    <th data-options="field:'IdCard',width:150">
                        身份证
                    </th>
                    <th data-options="field:'Sex',width:100">
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
                    <th data-options="field:'IsValidate',width:100">
                        是否有限制
                    </th>
                    <th data-options="field:'Validate',formatter:formatDatebox,width:100">
                        有效期
                    </th>
                   <%-- <th data-options="field:'Password',width:100">
                        密码
                    </th>--%>
                    <th data-options="field:'Status',width:100">
                        状态
                    </th>
                    <th data-options="field:'RegTime',formatter:formatDatebox,width:120"  >
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
  
   
</body>
</html>
