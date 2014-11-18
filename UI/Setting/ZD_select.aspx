<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ZD_select.aspx.cs" Inherits="Setting_ZD_select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>钟点房方案</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
  <%--  ?f_jb= "+ <%=f_jb %>  url="ZD_selectData.aspx?action=read"--%>
           <%--url="ZD_selectData.aspx?action=read&f_jb="+ <%=f_jb%>  --%>
</head>
<body class="easyui-layout">
 
    <div class="easyui-panel" title="钟点房方案" >
        <table id="dg" class="easyui-datagrid" style="padding: 0px;"
     
     url="ZD_selectData.aspx?action=read"
          buttons="#btn"
            fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                 <th data-options="field:'id',width:0">
                    序号
                    </th>
                    <th data-options="field:'FangAnName',width:100">
                    钟点房方案
                    </th>
                    <th data-options="field:'StartLen',width:100">
                       起步时长（分钟）
                    </th>
                       <th data-options="field:'StartFee',width:100">
                  起步价格
                    </th>
                    <th data-options="field:'AddLen',width:100">
                      加钟时间（分钟）
                    </th>
                        <th data-options="field:'AddFee',width:100">
                 加钟价格
                    </th>
                    <th data-options="field:'MinLen',width:100">
                      最小时长（分钟）
                    </th>
                       <th data-options="field:'MinFee',width:100">
                 最小价格
                    </th>
                    <th data-options="field:'MaxLen',width:100">
                     最大时长（分钟）
                    </th>
                </tr>
            </thead>
        </table>
 
    
    <div id="dlg" class="easyui-dialog" style="width: 260px; height: 300px;" closed="true"
        buttons="#dlg-buttons">
        <div style="padding: 10px 0 10px 10px">
            <form id="fm" method="post">
            <table>
                <tr>
                    <td>
                        方案名称：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="FangAnName" />
                    </td>
                </tr>
                <tr>
                    <td>
                        起步时长：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="StartLen" />分钟
                    </td>
                </tr>
                <tr>
                    <td>
                        起步价格：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="StartFee" />元
                    </td>
                </tr>
                <tr>
                    <td>
                        加钟时长：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="AddLen"/>分钟
                    </td>
                </tr>
                <tr>
                    <td>
                        加钟价格：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="AddFee" />元
                    </td>
                </tr>
                <tr>
                    <td>
                        最小时长：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MinLen"  />分钟
                    </td>
                </tr>
                <tr>
                    <td>
                        最小价格：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MinFee" />元
                    </td>
                </tr>
                <tr>
                    <td>
                        最长时长：
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="MaxLen" />分钟
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
   
    <table style="padding: 5px 5px 5px 5px" id="btn">
        <tr>
            <td style="width: 580px;">
                &nbsp;
            </td>
            <td style="width: 120px;" >
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" onclick="newMember()">增加</a>
            </td>
              <td style="width: 120px;" >
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" onclick="editMember()">修改</a>
            </td>
              <td style="width: 110px;" >
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-edit" onclick="destroyMember()">删除</a>
            </td>
            <td style="width: 20px;" >
            </td>
            <td style="width: 80px;" align="right">
                <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">退出</a>
            </td>
            <td style="width: 60px;">
            </td>
        </tr>
    </table>

       </div>

    <script type="text/javascript">
        function ClearForm() {
            this.window.close();
        }

        var url;
        function newMember() {
            $('#dlg').dialog('open').dialog('setTitle', '添加钟点房房价方案');
            $('#fm').form('clear');
            $('#fm').form('reset');

            url = 'ZD_selectData.aspx?action=create';
//            $('#id').show();
//            $('#Content').show();

           // getmemebernolast();
        }

        function editMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改钟点房房价方案');
                $('#fm').form('load', row);

                url = 'ZD_selectData.aspx?action=update&id=' + row.id;

//                $('#id').show();
//                $('#Content').show();
            }
        }
        function saveMember() {
            $('#fm').form('submit', {
                url: url,
                onSubmit: function () {
                    return $(this).form('validate');
                },
                success: function (result) {
                    if (result.errorMsg) {
                        $.messager.show({
                            title: 'Error',
                            msg: result.errorMsg
                        });
                    } else {
                        $('#dlg').dialog('close');
                        $('#dg').datagrid('reload');
                    }
                }
            });
        }




        function destroyMember() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('ZD_selectData.aspx?action=delete', { id: row.id }, function (result) {

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

//        function getmemebernolast() {

//            $.ajax({
//                type: "get",
//                url: "ZD_selectData.aspx?action=getLastid",
//                success: function (result) {
//                    var result = eval('(' + result + ')');
//                    if (result.errorMsg) {
//                        $.messager.show({
//                            title: 'Error',
//                            msg: result.errorMsg
//                        });
//                    }
//                },
//                error: function (result) {
//                    $.messager.show({
//                        title: 'Error',
//                        msg: result.errorMsg
//                    });
//                }
//            });
      //  }

    </script>


</body>
</html>
