<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewRuZhu.aspx.cs" Inherits="FrontDesk_NewRuZhu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>新增入住</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css">
    <link rel="stylesheet" type="text/css" href="../themes/icon.css">
    <link rel="stylesheet" type="text/css" href="../themes/demo.css">
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script type="text/javascript">
        function SubmitForm() {
            var str = '';
            var data = $('#lfdata').datagrid('getRows');
            for (var i = 0; i < data.length - 1; i++) {
                str = str + '{"FangJianHao":"' + data[i].FH + '","XingMing":"' + data[i].Name + '","ZhengjianHaoma":"' + data[i].IDCard + '"};';
            } 
            str = str + '{"FangJianHao":"' + data[data.length - 1].FH + '","XingMing":"' + data[data.length - 1].Name + '","ZhengjianHaoma":"' + data[data.length - 1].IDCard + '"}';
            $('#frm').form('submit', {
                url: "NewRuZhuData.aspx?action=create&list=" + str,
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
                        alert("success");
                    }
                }
            });
        }
        function ClearForm() {
            this.window.close();
        }

        function read() {
            ///////////////////////////////////////////////////////////////////////////////
            //以下为非接触式读写器函数调用例子 for javascript
            //注意个别函数（例如dc_disp_str）只有当设备满足要求（例如有数码显示）时才有效
            //更详细的帮助可参照32位动态库对应的函数使用说明
            ///////////////////////////////////////////////////////////////////////////////

            var st; //主要用于返回值
            var lSnr; //本用于取序列号，但在javascript只是当成dc_card函数的一个临时变量

            //初始化端口
            //第一个参数为0表示COM1，为1表示COM2，以此类推
            //第二个参数为通讯波特率
            st = rd.dc_init(100, 115200);
            if (st <= 0) //返回值小于等于0表示失败
            {
                alert("dc_init error!");
                return;
            }

            //寻卡，能返回在工作区域内某张卡的序列号
            //第一个参数一般设置为0，表示IDLE模式，一次只对一张卡操作
            //第二个参数在javascript只是当成dc_card函数的一个临时变量，仅在vbscript中调用后能正确返回序列号
            st = rd.dc_card(0, lSnr);
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_card error!");
                rd.dc_exit();
                return;
            }

            //将密码装入读写模块RAM中
            //第一个参数为装入密码模式
            //第二个参数为扇区号
            rd.put_bstrSBuffer_asc = "FFFFFFFFFFFF"; //在调用dc_load_key必须前先设置属性rd.put_bstrSBuffer或rd.put_bstrSBuffer_asc
            st = rd.dc_load_key(0, 0);
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_load_key error!");
                rd.dc_exit();
                return;
            }

            //核对密码函数
            //第一个参数为密码验证模式
            //第二个参数为扇区号
            st = rd.dc_authentication(0, 0);
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_authentication error!");
                rd.dc_exit();
                return;
            }


            //读取卡中数据，一次必须读一个块
            //第一个参数为块地址
            //取出的数据放在属性放在rd.put_bstrSBuffer（正常表示）和rd.put_bstrSBuffer_asc（十六进制ascll码字符串表示）中
            st = rd.dc_read(2);
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_read error!");
                rd.dc_exit();
                return;
            }

            this.HuaiYuanKa.value = rd.get_bstrRBuffer;




            //蜂鸣
            //第一个参数为蜂鸣时间，单位是10毫秒
            st = rd.dc_beep(50);
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_beep error!");
                rd.dc_exit();
                return;
            }

            //关闭端口
            st = rd.dc_exit();
            if (st != 0) //返回值小于0表示失败
            {
                alert("dc_exit error!");
                return;
            }

        }
    </script>
</head>
<body class="easyui-layout">
    <!--客户信息 点击连房录入，弹出多个房间入住信息录入，主界面仅有一个入住单录入信息模块-->
    <div class="easyui-tabs" style="padding: 2px">
        <div title="主要信息" style="padding: 0px">
            <form id="frm" method="post">
            <table style="padding: 10px">
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        序号:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="AutoID" disabled></input>
                    </td>
                    <td style="width: 80px;">
                        <input type="checkbox" title="开长途" name="ChangTu">&nbsp;&nbsp;开长途</input>
                    </td>
                    <td style="width: 160px;">
                        <input type="checkbox" title="开市话" name="ShiHua">&nbsp;&nbsp;开市话</input>
                    </td>
                    <td style="width: 80px;">
                        <input type="checkbox" title="长包房" name="ChangBao">&nbsp;&nbsp;长包房</input>
                    </td>
                    <td style="width: 160px;">
                        <input type="checkbox" title="钟点房" name="ZhongDian">&nbsp;&nbsp;钟点房</input>
                    </td>
                </tr>
                <tr>
                    <td>
                        客人类别
                    </td>
                    <td>
                        <input class="easyui-combobox" id="language" name="KerenLeibie" data-options="valueField:'ID',textField:'KHLB',url:'../Setting/BasicInfoData.aspx?module=khlb&action=read'">
                    </td>
                    <td>
                        团队
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="TuanDui"></input>
                    </td>
                    <td style="width: 80px;">
                        标准房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="BiaozhunFangjia"></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        证件类别
                    </td>
                    <td>
                        <input class="easyui-combobox" id="Text3" name="ZhengjianLeibie" data-options="valueField:'ID',textField:'ZJLX',url:'../Setting/BasicInfoData.aspx?module=zjlx&action=read'">
                    </td>
                    <td>
                        电话
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DianHua"></input>
                    </td>
                    <td>
                        房间类型
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="FangjianLeixing" readonly></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别
                    </td>
                    <td>
                        <select class="easyui-combobox" name="XingBie" style="width: 140px">
                            <option value="男">男</option>
                            <option value="女">女</option>
                        </select>
                    </td>
                    <td>
                        会员卡
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="HuaiYuanKa"></input>
                        <input type="button" value="读卡" onclick="read()" />
                    </td>
                    <td>
                        积分
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="JiFen" readonly></input>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        姓名
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="XingMing" data-options="required:true"></input>
                    </td>
                    <td style="width: 80px;">
                        证件号
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ZhengJianHao"></input>
                    </td>
                    <td style="width: 80px;">
                        地址
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="DiZhi"></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        到店时间
                    </td>
                    <td>
                        <input name="DaodianTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=DateTime.Today.ToShortDateString()%>">
                    </td>
                    <td>
                        离店时间
                    </td>
                    <td>
                        <input name="LidianTime" class="easyui-datetimebox" style="width: 140px" data-options="showSeconds:false"
                            value="<%=DateTime.Today.AddDays(1).ToShortDateString()%>">
                    </td>
                    <td>
                        <input type="checkbox">叫醒服务</input>
                    </td>
                    <td>
                        <input class="easyui-datetimebox" name="JiaoxingFuwu" style="width: 140px">
                    </td>
                </tr>
                <tr>
                    <td>
                        付款方式
                    </td>
                    <td>
                        <input class="easyui-combobox" id="Text1" name="FukuanFangshi" data-options="valueField:'ID',textField:'fkfs',url:'../Setting/BasicInfoData.aspx?module=fkfs&action=read'">
                    </td>
                    <td style="width: 80px;">
                        押金
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="YaJin"></input>
                    </td>
                    <td>
                        协议单位
                    </td>
                    <td>
                        <input class="easyui-combobox" name="XieyiDanwei" data-options="valueField:'ID',textField:'Name',url:'../Setting/PartnerData.aspx?action=read'">
                    </td>
                </tr>
                <tr>
                    <td>
                        特权人
                    </td>
                    <td>
                        <select class="easyui-combobox" name="TeQuanRen" style="width: 140px">
                        </select>
                    </td>
                    <td>
                        折扣率
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ZheKouLv"></input>%
                    </td>
                    <td style="width: 80px;">
                        实际房价
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="ShijiFangjia"></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        备注
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="BeiZhu"></input>
                    </td>
                    <td>
                        手工单号
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="ShougongDanhao"></input>
                    </td>
                    <td style="width: 80px;">
                        <input type="checkbox" name="BaoMi">&nbsp;保密</input>
                    </td>
                    <td style="width: 160px;">
                        &nbsp;
                    </td>
                </tr>
            </table>
            </form>
            <table class="easyui-datagrid" toolbar="#toolbar" id="kfdata" url="../Setting/get_kf.aspx?readE=true"
                style="padding: 0px; width: 798px; height: 180px">
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
            <div id="toolbar">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-tip" plain="true"
                    onclick="LianFang()">连房录入</a>
            </div>
            <div id="dlg" class="easyui-dialog" style="width: 500px; height: 350px; padding: 1px"
                closed="true" buttons="#dlgbuttons">
                <table id="lfdata" style="padding: 0px">
                </table>
            </div>
            <div id="dlgbuttons">
                <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="HiddenDiv()">
                    确定</a> <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlg').dialog('close')">取消</a>
            </div>
            <script type="text/javascript">
                var url;
                function LianFang() {
                    //open dialog
                    $('#dlg').dialog('open').dialog('setTitle', '连房录入');
                    //clear form
                    $('#fm').form('clear');
                    //init datagrid for input
                    $('#lfdata').datagrid({
                        iconCls: 'icon-edit',
                        singleSelect: true,
                        columns: [[
                        { field: 'action', title: '操作', width: 100, align: 'center',
                            formatter: function (value, row, index) {
                                if (row.editing) {
                                    var s = '<a href="javascript:void(0)" onclick="saverow(this)">Save</a> ';
                                    var c = '<a href="javascript:void(0)" onclick="cancelrow(this)">Cancel</a>';
                                    return s + c;
                                } else {
                                    var e = '<a href="javascript:void(0)" onclick="editrow(this)">Edit</a> ';
                                    var d = '<a href="javascript:void(0)" onclick="deleterow(this)">Delete</a>';
                                    return e + d;
                                }
                            }
                        },
                        { field: 'FH', title: '房号', width: 110, editor: 'text' },
                        { field: 'Name', title: '姓名', width: 110, editor: 'text' },
                        { field: 'IDCard', title: '证件号码', width: 110, editor: 'text' },
					    { field: 'StdPrice', title: '标准房价', width: 110, align: 'right', editor: { type: 'numberbox', options: { precision: 2}} },
					    { field: 'ZKL', title: '折扣率', width: 110, align: 'right', editor: { type: 'numberbox', options: { precision: 1}} },
                        { field: 'Price', title: '实际房价', width: 110, align: 'right', editor: { type: 'numberbox', options: { precision: 2}} }
				        ]],
                        onBeforeEdit: function (index, row) {
                            row.editing = true;
                            updateActions(index);
                        },
                        onAfterEdit: function (index, row) {
                            row.editing = false;
                            updateActions(index);
                        },
                        onCancelEdit: function (index, row) {
                            row.editing = false;
                            updateActions(index);
                        }
                    });
                    //get kfdata selected info
                    var kfs = $('#kfdata').datagrid('getSelections');
                    for (var i = 0; i < kfs.length; i++) {
                        var index = 0;
                        //每个房间默认2条待输记录
                        $('#lfdata').datagrid('insertRow', {
                            index: index,
                            row: {
                                FH: kfs[i].FH,
                                Name: '',
                                IDCard: '',
                                StdPrice: kfs[i].DJ,
                                ZKL: '10.0',
                                Price: kfs[i].DJ
                            }
                        });
                        $('#lfdata').datagrid('insertRow', {
                            index: index,
                            row: {
                                FH: kfs[i].FH,
                                Name: '',
                                IDCard: '',
                                StdPrice: kfs[i].DJ,
                                ZKL: '10.0',
                                Price: kfs[i].DJ
                            }
                        });
                    }
                }
                function updateActions(index) {
                    $('#lfdata').datagrid('updateRow', {
                        index: index,
                        row: {}
                    });
                }
                function getRowIndex(target) {
                    var tr = $(target).closest('tr.datagrid-row');
                    return parseInt(tr.attr('datagrid-row-index'));
                }
                function editrow(target) {
                    $('#lfdata').datagrid('beginEdit', getRowIndex(target));
                }
                function deleterow(target) {
                    $.messager.confirm('Confirm', 'Are you sure?', function (r) {
                        if (r) {
                            $('#lfdata').datagrid('deleteRow', getRowIndex(target));
                        }
                    });
                }
                function saverow(target) {
                    $('#lfdata').datagrid('endEdit', getRowIndex(target));
                }
                function cancelrow(target) {
                    $('#lfdata').datagrid('cancelEdit', getRowIndex(target));
                }
                function insert() {
                    var row = $('#lfdata').datagrid('getSelected');
                    if (row) {
                        var index = $('#lfdata').datagrid('getRowIndex', row);
                    } else {
                        index = 0;
                    }
                    $('#lfdata').datagrid('insertRow', {
                        index: index,
                        row: {
                            status: 'P'
                        }
                    });
                    $('#lfdata').datagrid('selectRow', index);
                    $('#lfdata').datagrid('beginEdit', index);
                }

                function HiddenDiv() {
                    $('#dlg').dialog('close');
                }
            </script>    
            <table style="padding: 5px 5px 5px 5px">
                <tr>
                    <td style="width: 580px;">
                        &nbsp;
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="SubmitForm()">保存</a>
                    </td>
                    <td style="width: 80px;" align="right">
                        <a href="javascript:void(0)" class="easyui-linkbutton" onclick="ClearForm()">取消</a>
                    </td>
                    <td style="width: 60px;">
                    </td>
                </tr>
            </table>
        </div>
        <div title="随客信息" style="padding: 0px">
            <table style="padding: 10px">
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        姓名:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name" disabled></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        性别:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name" disabled></input>
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px; margin-right: 10px">
                        身份证:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name" disabled></input>
                    </td>
                    <td style="width: 80px; margin-right: 10px">
                        地址:
                    </td>
                    <td style="width: 160px;">
                        <input class="easyui-validatebox" type="text" name="name" disabled></input>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <object id="rd" classid="clsid:638B238E-EB84-4933-B3C8-854B86140668" codebase="../Bin/comRD800.dll">
    </object>
</body>
</html>
