<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KuaiSuYuDing.aspx.cs" Inherits="FrontDesk_KuaiSuYuDing" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>快速预订</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        $.fn.datebox.defaults.formatter = function (date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + m + '-' + d;
        }
        function SaveForm() {
            var ids = [];
            var rows = $('#kfdata').datagrid('getSelections');
            for (var i = 0; i < rows.length; i++) {
                ids.push(rows[i].FH + '-' + rows[i].JBName);
            }
            
            $('#ff').form('submit', {
                url: 'YDData.aspx?action=createall&fhs=' + ids.join(':'),
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
        function ClearForm() { this.close(); }
        function GetCanUse() {
            var id = this.document.getElementById("level").value;
            var begin = document.getElementById('OnBoardTime').value;
            var end = document.getElementById('LeaveTime').value;
            //$('#kfdata').datagrid({ url: '../Setting/get_kf.aspx?readcanyd=true&cgyid=' + id + '&begin=' + begin + '&end=' + end });
            alert('../Setting/get_kf.aspx?readcanyd=true&cgyid=' + id + '&begin=' + begin + '&end=' + end);
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'center'" style="padding: 2px">
        <form id="ff" method="post">
        <table style="padding: 20px 10px 20px 10px">
            <tr>
                <td style="width: 80px; padding: 5px;" align="right">
                    客户名称
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="Yder" required></input>
                </td>
                <td style="width: 80px; padding: 5px;" align="right">
                    联系电话
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="YdTel" required></input>
                </td>
                <td style="width: 80px;" align="right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="SaveForm()">保存</a>
                </td>
                <td style="width: 80px;" align="right">
                    <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div class="easyui-panel" title="选择房间" style="padding: 0px">
            <div style="padding: 10px 10px 10px 10px">
                <table>
                    <tr>
                        <td style="width: 60px; padding: 5px;" align="right">
                            预到日期
                        </td>
                        <td>
                            <input class="easyui-datetimebox" id="OnBoardTime" name="OnBoardTime" style="width: 120px" data-options="showSeconds:false"
                                value="<%=YDTime%>">
                        </td>
                        <td style="width: 60px; padding: 5px;" align="right">
                            预离日期
                        </td>
                        <td>
                            <input class="easyui-datetimebox" id="LeaveTime" name="LeaveTime" style="width: 120px" data-options="showSeconds:false"
                                value="<%=YLTime%>">
                        </td>
                        <td style="width: 60px; padding: 5px;" align="right">
                            客房级别
                        </td>
                        <td>
                            <input class="easyui-combobox" id="level" data-options="valueField:'ID',textField:'KFJB',url:'../Common/getkfcgy.aspx',
                             onSelect: function(rec){var begin=$('#OnBoardTime').datebox('getValue');var end=$('#LeaveTime').datebox('getValue');$('#kfdata').datagrid({url:'../Setting/get_kf.aspx?readcanyd=true&cgyid='+rec.ID+'&begin='+begin+'&end='+end});}">
                        </td>
                        <!--<td>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="GetCanUse()">筛选可用房</a>
                        </td>-->
                    </tr>
                </table>
            </div>
        </div>
        </form>
        <table class="easyui-datagrid" id="kfdata" url="../Setting/get_kf.aspx?readcanyd=true&cgyid=0&begin=<%=YDTime %>&end=<%=YLTime %>"
            style="padding: 0px; width: 798px; height: 351px">
            <thead>
                <tr>
                    <th data-options="field:'ck',checkbox:true">
                    </th>
                    <th data-options="field:'FH',width:180">
                        房号
                    </th>
                    <th data-options="field:'JBName',width:180">
                        房间类型
                    </th>
                    <th data-options="field:'DJ',width:180">
                        房价
                    </th>
                    <th data-options="field:'StatusName',width:180">
                        状态
                    </th>
                </tr>
            </thead>
        </table>
    </div>
</body>
</html>
