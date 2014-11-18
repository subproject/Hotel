<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoomCardSearch.aspx.cs" Inherits="HouseStatus_RoomCardSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>房卡查询</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function doSearch() {
            $('#tt').datagrid('load', {
                Begin: $('#Begin').datebox('getValue'),
                Name: $('#Name').val(),
                Fh: $('#Fh').val(),
                End: $('#End').datebox('getValue')
            });
        }
    </script>
</head>
<body class="easyui-layout" style="padding: 2px">
    <div class="easyui-panel" title="房卡查询">
        <table class="easyui-datagrid" border="false" toolbar="#dgtbr" 
            pagination="true" singleselect="true" id="tt">
            <thead>
                <tr>
                    <th data-options="field:'FangJianHao',align:'center'">
                        房间号
                    </th>
                    <th data-options="field:'XingMing',align:'center'">
                        客户名称
                    </th>
                    <th data-options="field:'XingBie'">
                        类型
                    </th>
                    <th data-options="field:'MainFH',align:'center'">
                        编号
                    </th>
                    <th data-options="field:'MainOrderMan',align:'center'">
                        状态
                    </th>
                    <th data-options="field:'OnBoardTime',align:'center'">
                        到店日期
                    </th>
                    <th data-options="field:'LeaveTime',align:'center'">
                        离店日期
                    </th>
                </tr>
            </thead>
        </table>
        <div id="dgtbr" style="padding:5px">
            <table>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        客户名称
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Name" name="Name" />
                    </td>
                    <td style="width: 80px;">
                        房号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" id="Fh" name="Fh" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        开始时间
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="Begin" id="Begin" style="width: 140px" />
                    </td>
                    <td>
                        结束时间
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="End" id="End" style="width: 140px" />
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="">检索</a>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </div>
</body>
</html>

