<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KeRenJieZhang.aspx.cs" Inherits="Cash_KeRenJieZhang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>客人账务信息</title>
    <link rel="stylesheet" type="text/css" href="../themes/default/easyui.css" />
    <link rel="stylesheet" type="text/css" href="../themes/icon.css" />
    <link rel="stylesheet" type="text/css" href="../css/cashaction/pageformal.css" />
    <script type="text/javascript" src="../jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="../jquery.easyui.min.js"></script>
    <script src="../src/jquery.json-2.2.min.js" type="text/javascript"></script>
    <script src="../locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../src/datagrid-scrollview.js" type="text/javascript"></script>
    <script src="../Hotelmgr.js" type="text/javascript"></script>
    <script src="../js/jiezhangaction/jiezhangaction.js" type="text/javascript"></script>
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
        .inputbutton
        {
            background-repeat: repeat-x;
        }
        .tdtype
        {
            border: 1px solid #e2eefe;
            width: 94px;
            text-align: right;
            line-height:30px;
        }
        tr
        {
             line-height:30px;
            }
        .tddatatype
        {
            border: 1px solid #e2eefe;
            width: 94px;
            text-align: center;
        }
        p
        {
           width;100%;
           height:25px;
            overflow:hidden;
            }
    </style>
    <script type="text/javascript">
        function openactionview() {
            opennewdivform('收款操作', '#windowIframe', '#WindailogDiv', 'CashAction/Shoukuan.htm', 'open');
        }
        function openactionviewV() {
            opennewdivform('收款操作', '#windowIframe', '#WindailogDiv', 'ZhanghuChaxun.aspx', 'open');
        }
        
    </script>
</head>
<body class="easyui-layout">
    <div id="WindailogDiv">
        <iframe scrolling="auto" id='windowIframe' frameborder="0" src="" style="width: 100%;
            height: 100%;"></iframe>
    </div>
    <div region="east" split="true" title="操作列表" style="width: 250px; padding: 10px;">
        <div class="buttons">
            <table>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_kefanfeiyongdetail()">
                            <img src="../css/cashaction/imag/apply2.png" alt="" />
                            客房费用</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_shunwupeichang()">损物赔偿</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_zaocanfei()">早餐费</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_qitafeiyong()">其他费用</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_hongchongfeiyong()">
                            红冲费用</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="doSearch()">POS点费用</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_zhifengyoufei()">积分费用</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_keyenziliao()">客人资料</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_zhujiemuping()">租借物品</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_shuidianmeifeiyong()">
                            水电煤</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_bujiezhangtufan()">未结离店</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_bufengjiezhang()">部分结账</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_qiangtaifuxian()">前台现付</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_sankexunizhangdang()">
                            虚拟账单</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_fabiaomanage()">发票管理</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_cancejiezhang()">撤销结账</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_actionlog()">操作记录</a>
                    </td>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="Operator_chakandengjidian()">
                            查看登记单</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <a href="javascript:void(0)" class="positive" onclick="">返回</a>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div region="center" border="true" title="客户结帐信息" style="border-left: 0px; border-right: 0px;
        overflow: hidden;">
        <div >
            <table style="border: 1px solid #e2eefe; padding: 10px; border-collapse: collapse;
                width: 100%; ">
                <tr style=" height:5px">
                    <td class="tdtype">
                        客人姓名
                    </td>
                    <td class="tddatatype">
                        <p id="XingMing"> <%= order.XingMing %></p>
                    </td>
                    <td class="tdtype">
                        房间类型
                    </td>
                    <td class="tddatatype">
                        <p id="FangjianLeixing">  <%=order.FangjianLeixing%></p>
                    </td>
                    <td class="tdtype">
                        当前状态
                    </td>
                    <td class="tddatatype">
                         <p id="Status"><%=(order.Status==1?"已结离店":(order.LidianTime<=DateTime.Now)?"未结离店":"在住")%></p>
                    </td>
                    <td class="tdtype">
                        房价
                    </td>
                    <td class="tddatatype">
                         <p id="ShijiFangjia"><%=order.ShijiFangjia%></p>
                    </td>
                    <td class="tdtype">
                        入住类型
                    </td>
                    <td class="tddatatype" colspan="3">
                       <p id="ruzhuliexing"> <%=(order.ChangBao==true?"长包":(order.ZhongDian==true?"钟点":(order.ShijiFangjia <= 0?"免费":"正常")))%></p>
                    </td>
                </tr>
                <tr style=" height:5px">
                    <td class="tdtype">
                        入住时间
                    </td>
                    <td class="tddatatype" colspan="2" style=" width:120px; text-align:center;">
                       <p id="daodian" style=" width:100%;"> <%=order.DaodianTime%></p>
                    </td>
                    <td class="tdtype">
                       <p id="timtype" style=" width:100%;"> <%=order.Status==1?"结账时间":(order.LidianTime<=DateTime.Now)?"离店时间":"预离时间"%></p> 
                    </td>
                    <td class="tddatatype" colspan="2"  style=" width:120px;text-align:center;">
                       <p id="LidianTime" style=" width:100%;">  <%=order.LidianTime%></p>
                    </td>
                    <td class="tdtype">
                        其他服务
                    </td>
                    <td class="tddatatype">
                        <p id="TeQuanRen"> <%=order.TeQuanRen%></p>
                    </td>
                    <td class="tdtype">
                        包费用
                    </td>
                    <td class="tddatatype" colspan="3">
                        <p id="BaoMi"> <%=order.BaoMi%></p>
                    </td>
                </tr>
                <tr style=" height:5px">
                    <td class="tdtype">
                        联房房间
                    </td>
                    <td class="tddatatype" colspan="5">
                       <p id="rooms">  <%=rooms%></p>
                    </td>
                    <td class="tdtype">
                        佣金
                    </td>
                    <td class="tddatatype">
                        <p id="YongJing"> <%=""%></p>
                    </td>
                    <td class="tdtype">
                        营销人员
                    </td>
                    <td class="tddatatype">
                        <p id="XiaoShouYuan"> <%=order.XiaoShouYuan%></p>
                    </td>
                    <td class="tdtype">
                        客户类别
                    </td>
                    <td class="tddatatype">
                        <p id="KerenLeibie"> <%=order.KerenLeibie%></p>
                    </td>                    
                </tr>
                <tr style=" height:5px">
                    <td class="tdtype">
                        应收账款
                    </td>
                    <td class="tddatatype">
                       <p id="yingshou">  <%=string.Format("{0:N2}",orderinfo.allCosumper-orderinfo.ALLJZConsumption)%></p>
                    </td>
                    <td class="tdtype">
                        预收账款
                    </td>
                    <td class="tddatatype">
                       <p id="ykshou">  <%=string.Format("{0:N2}",orderinfo.YaJin-orderinfo.allDepositDeduct)%></p>
                    </td>
                    <td class="tdtype">
                        已结算
                    </td>
                    <td class="tddatatype">
                       <p id="jzshou">  <%=string.Format("{0:N2}",orderinfo.ALLJZConsumption)%></p>
                    </td>
                    <td class="tdtype">
                        总费用
                    </td>
                    <td class="tddatatype">
                       <p id="allmoney">  <%=string.Format("{0:N2}",orderinfo.allCosumper)%></p>
                    </td>
                    <td class="tdtype">
                        总收退款
                    </td>
                    <td class="tddatatype">
                        <p id="alltukuan"> <%=string.Format("{0:N2}",orderinfo.YaJin+orderinfo.allMoney)%></p>
                    </td>
                    <td class="tdtype">
                        信用卡预授权
                    </td>
                    <td class="tddatatype">
                        <p id="yishouquan"> <%=""%></p>
                    </td>
                </tr>
                <tr style=" height:5px">
                    <td class="tdtype">
                        结余
                    </td>
                    <td class="tddatatype">
                        <p id="jieyu"> <%=string.Format("{0:N2}",(orderinfo.YaJin - orderinfo.allDepositDeduct
                                           - (orderinfo.allCosumper - orderinfo.ALLJZConsumption)))%></p>
                    </td>
                    <td class="tdtype">
                        总计优惠
                    </td>
                    <td class="tddatatype" colspan="1">
                        <p id="youhui"> <%=orderinfo.allPreferentialy%></p>
                    </td>
                    <td class="tdtype">
                        备注
                    </td>
                    <td class="tddatatype" colspan="4">
                        <p id="beizhu"> <%=""%></p>
                    </td>
                    <td class="tdtype">
                        特要说明
                    </td>
                    <td class="tddatatype" colspan="2">
                        <p id="P8"> <%=""%></p>
                    </td>
                </tr>
                <tr style=" height:5px">
                    <td class="tdtype">
                        系统备注
                    </td>
                    <td class="tddatatype"  colspan="11">
                       <p id="xitongbeizhu" style=" width:200px; color:Orange;">  <%= xitongremark %></p>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <div class="buttons">
                <table>
                    <tr>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="Operator_Shoukuan()">收 款</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="Operator_tuikuan()">退 款</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="doSearch()">信用卡预授权</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="Operator_sankezhuanzhang()">转
                                帐</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="Operator_jiezhang()">结 帐</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="Operator_dayinzhangdan()">
                                打印帐单</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="">注销卡门</a>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="positive" onclick="RefreshDataGrid">刷新</a>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div region="center" border="true" title="" split="true" style="background: #E2E377;">
            <div class="easyui-tabs">
                <div title="费用明细">
                    <table id="AllCostsdgrid">
                    </table>
                </div>
                <div title="收退款">
                    <table id="shoutukuandgrid">
                    </table>
                </div>
                <div title="已转账">
                    <table id="jzhuangdgrid">
                    </table>
                </div>
                <div title="已结算">
                    <table id="jjiedgrid">
                    </table>
                </div>
            </div>
        </div>
        <div>
            <form id="frm" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="finishtask" Text="结账退房" runat="server" OnClick="Finish_Click" />
                    </td>
                    <td>
                        <asp:HiddenField runat="server" ID="orderID" Value="" />
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>
</body>
</html>
