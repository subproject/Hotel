<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberPointToGift.aspx.cs" Inherits="MemberPointToGift" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>积分换礼设置</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
     
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

</head>
<body class="easyui-layout">
   <div data-options="region:'center'" style="padding: 1px">
        <div title="积分换礼设置" class="easyui-panel">
        </div>
        <table id="dg" class="easyui-datagrid" style="padding: 0px" url="MemberCardData.aspx?action=GetPointToGift"
            toolbar="#dgtbr" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th data-options="field:'id',width:100,hidden:true">
                        ID
                    </th>
                    <th data-options="field:'liwu',width:100">
                        项目
                    </th>
                
                    <th data-options="field:'MustPoint',width:100">
                        消费积分
                    </th>
                    
                    

                </tr>
            </thead>
        </table>
        <div id="dgtbr">

            <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-add" plain="true"
                onclick="newPointToGift()">新增</a> <a href="javascript:void(0)" class="easyui-linkbutton"
                    iconcls="icon-edit" plain="true" onclick="editPointToGift()">编辑</a> <a href="javascript:void(0)"
                        class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="destroyPointToGift()">
                        删除</a>
        </div>
        
        <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 10px 20px"
            closed="true" buttons="#dlgbuttons">
            <form id="fm" method="post" novalidate>
            <div class="fitem">
                <label>
                   项目:</label>
                <input name="liwu" class="easyui-validatebox" required="true">
            </div>
            
            <div class="fitem">
                <label>
                    消费积分:</label>
                <input name="MustPoint" class="easyui-validatebox">
            </div>
           
 
      
        </form>
    </div>
               <div id="dlgbuttons">
    <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePointToGift()">
            保存</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>

        
    
      

    <script type="text/javascript">
        var url;
       

      
        
        function newPointToGift() {
            $('#dlg').dialog('open').dialog('setTitle', '新增');
            $('#fm').form('clear');
            url = 'MemberCardData.aspx?action=PointToGiftCreate';
        }
        function editPointToGift() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '编辑');
                $('#fm').form('load', row);
                url = 'MemberCardData.aspx?action=PointToGiftUpdate&id=' + row.id;
            }
        }
       
        function savePointToGift() {
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
        function destroyPointToGift() {
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $.messager.confirm('Confirm', '确认删除?', function (r) {
                    if (r) {
                        $.post('MemberCardData.aspx?action=PointToGiftDelete', { id: row.id }, function (result) {
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
