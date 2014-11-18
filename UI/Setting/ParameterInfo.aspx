<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParameterInfo.aspx.cs" Inherits="Setting_ParameterInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../themes/icon.css"/>
    <link rel="stylesheet" type="text/css" href="../themes/demo.css"/>
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
     <script type="text/javascript">
         $(document).ready(function () {
             $.ajax({
                 type: "get",
                 url: 'ParameterInfoData.aspx?action=read',
                 success: function (result) {

                     var result = eval('(' + result + ')');
                     if (result.errorMsg) {
                         $.messager.show({
                             title: 'Error',
                             msg: result.errorMsg
                         });
                     }
                     else {
                         $("#CheckInMemo1").val(result.CheckInMemo1);
                         $("#CheckInMemo2").val(result.CheckInMemo2);
                         $("#CheckInMemo3").val(result.CheckInMemo3);
                     }
                 },
                 error: function (result) {
                     alert(result);
                     $.messager.show({
                         title: 'Error',
                         msg: result.errorMsg
                     });
                 }
             });
         });
    </script>
</head>
<body class="easyui-layout">
    <div class="easyui-tabs" style="width: 800px; height: 460px; padding: 2px">
    <!--参数命名:天房T,钟点房ZH,凌晨房L,换房H;是否启用Enable**-->
        <div title="计费参数" style="padding: 8px">
            <form id="frm" method="post" action="ParameterInfo.aspx?action=save">
            <p>
                入住后,<input type="text" id="CancelTime" name="CancelTime" size="1" value="0" />分钟内允许撤单 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="checkbox" id="EnableYJ" name="EnableYJ" />登记时必须输入押金，不得低于<input type="text"
                    size="2" name="MinYJ" id="MinYJ" value="0" />元</p>
            <p>
                天房超过退房时间可以再宽限<input type="text" name="TDelayTime" id="TDelayTime" size="1" value="15" />分钟，钟点房超时可以再宽限<input
                    type="text" name="ZHDelayTime" id="ZHDelayTime" size="1" value="5" />分钟</p>
            <hr />
           
            <p>
                <input type="checkbox" name="EnableLCF" value="checked" />启用凌晨房，<input class="easyui-timespinner"
                   name="LStartTime" style="width: 60px;">
                到<input name="LEndTime" class="easyui-timespinner" style="width: 60px;">开房执行凌晨房价格</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;凌晨房退房时间<input class="easyui-timespinner" name="LMaxTime" style="width: 60px;">超时
                <select name="LAddFree">
                    <option>加收半天房费</option>
                    <option>按分钟加收房费</option>
                    <option>不加收房费</option>
                </select>，直到
                <select name="LDay">
                    <option>次日</option>
                    <option>当日</option>
                </select>
                <input class="easyui-timespinner" name="LDayTime" style="width: 60px;">
                <select name="LAddDayFee">
                    <option>转为全天房价</option>
                    <option>加收全天房费</option>
                </select></p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;每隔<input type="text" size="2" value="60" />分钟加收<input type="text"
                    size="2" value="10" />元， 不足<input type="text" size="2" value="60" />分钟，超过<input type="text"
                        size="2" value="10" />分钟，加收<input type="text" size="2" value="5" />元</p>
            <hr />
            <p>
                天房退房时间<input class="easyui-timespinner" style="width: 60px;">超时
                <select>
                    <option>加收半天房费</option>
                    <option>按分钟加收房费</option>
                </select>，直到<input class="easyui-timespinner" style="width: 60px;">加收全天房费</p>
            <p>
                &nbsp;&nbsp;&nbsp;&nbsp;每隔<input type="text" size="2" value="60" />分钟加收<input type="text"
                    size="2" value="10" />元，不足<input type="text" size="2" value="60" />分钟，超过<input type="text"
                        size="2" value="10" />分钟，加收<input type="text" size="2" value="5" />元</p>
            <p>
                天房在<input id="ss" class="easyui-timespinner" style="width: 60px;">之前入住客人视同前一天入住（建议和夜审时间相同）</p>
            </form>
        </div>
        <div title="单据参数">
        <form id="fm" method="post">
         <p style="color:Blue">
                结账单</p>
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                <p>
                单据格式：                <select name="DanJuGS">
                    <option>不打印</option>
                    <option>明细账单—A4</option>
                    <option>明细账单—POS76</option>
                </select>
                </p>  <p>
                <input type="checkbox" name="" value="checked" />在打印之前预览
                                        &nbsp;&nbsp;&nbsp;&nbsp;打印份数：<input class="easyui-NumberSpinner
" style="width: 60px;">
                    </p>
                    <p>
                   纸张类型                <select name="PaperType">
                    <option>打印机默认</option>
                    <option>自定义</option>
                </select>  

            <input type="text" size="6" value="12" />&nbsp;X&nbsp;<input type="text" size="6" value="13.5" />
                    </p>
                 <p>
            备注1：     <input type="text" style="width:260px;height:30px;"/>
            </p>
             <p>
            备注2：     <input type="text" style="width:260px;height:30px;"/>
            </p>
                                </td>
                                <td>&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;</td>
                                <td>
                                  <p style="color:Blue">
                       单据边界（厘米）
                       </p>
                       <p>
                       左：
                       <input type="text" size="8" value="0.35" /></p>
                          <p>
                       顶：<input type="text" size="8" value="0.6" />
                       </p>
                          <p>
                       右：<input type="text" size="8" value="0.35" />
                       </p>
                          <p>
                       底：<input type="text" size="8" value="0" />
                       </p>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                    </td>
                        <td>
                        <p style="color:Blue">充值收款单</p>
                        单据格式：<br />
                        <select name="DanJuGS">
                    <option>不打印</option>
                    <option>明细账单—A4</option>
                    <option>明细账单—POS76</option>
                </select><br />
                纸张类型：<br />
                <select name="PaperType">
                    <option>打印机默认</option>
                    <option>自定义</option>
                </select><br />
                <input type="text" size="6" value="12" />&nbsp;X&nbsp;<input type="text" size="6" value="13.5" /><br />
                        <input type="checkbox" name="" value="checked" />在打印之前预览<br />
                打印份数：<input class="easyui-NumberSpinner
" style="width: 60px;">
                    </td>
                </tr>
            </table>
         <hr />
         
            <p style="color:Blue">
               入住登记单</p>
            <table border="0" cellpadding="0" cellspacing="0"  url = 'ParameterInfoData.aspx?action=read'>
                <tr>
                    <td colspan="2">
                    <p>
                单据格式：                <select name="DanJuGS">
                    <option>不打印</option>
                    <option>明细账单—A4</option>
                    <option>明细账单—POS76</option>
                </select>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="checkbox" name="" value="checked" />在打印之前预览
            </p>
               <p>
                纸张类型                <select name="PaperType">
                    <option>打印机默认</option>
                    <option>自定义</option>
                </select>  

            <input type="text" size="6" value="12" />&nbsp;X&nbsp;<input type="text" size="6" value="13.5" />&nbsp;&nbsp;&nbsp;打印份数：<input class="easyui-NumberSpinner
" style="width: 60px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="checkbox" name="" value="checked" />打印时隐藏身份证号和地址</p>
                    </td>
                </tr>
                 <tr>
                    <td >
                          
            <p>
            备注1：     <textarea id="CheckInMemo1" name="CheckInMemo1" cols="50" rows="4"></textarea>
            </p>
             <p>
            备注2：     <textarea id="CheckInMemo2"  name="CheckInMemo2" cols="50" rows="4"></textarea>
            </p>
             <p>
            备注3：   <textarea id="CheckInMemo3" name="CheckInMemo3"  cols="50" rows="4"></textarea>
            </p>
                    </td>
                       <td >
                       <p style="color:Blue">
                       单据边界（厘米）
                       </p>
                       <p>
                       左：
                       <input type="text" size="8" value="0.35" /></p>
                          <p>
                       顶：<input type="text" size="8" value="0.6" />
                       </p>
                          <p>
                       右：<input type="text" size="8" value="0.35" />
                       </p>
                          <p>
                       底：<input type="text" size="8" value="0" />
                       </p>
                    </td>
                </tr>
            </table>
            </form>
        </div>
        <div title="接口参数">
        </div>
    </div>
    <table style="padding:5px">
        <tr>
            <td style="width: 530px;">
                &nbsp;
            </td>
            <td style="width: 80px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="SaveForm()">保存</a>
            </td>
            <td style="width: 80px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        var url;
        function SaveForm() {
            url = 'ParameterInfoData.aspx?action=create';
           
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
                        window.opener.location.reload();
                        window.close();
                    }
                }
            });
        }
    </script>
</body>
</html>
