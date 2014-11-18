<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPage.aspx.cs" Inherits="Warehouse_PrintPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        window.onload = function () {
            var printData = window.opener.getPrintData();
            var inner = document.body.innerHTML;
            for (var it in printData) {
                if (typeof (printData[it]) == "object") {
                    continue;
                } else {
                    inner = inner.replace(new RegExp("{" + it + "}", "ig"), printData[it]);
                }
            }
            document.body.innerHTML = inner;
            var temp = document.getElementById("rows");
            var tempRow = temp.children[0];
            temp.removeChild(tempRow);
            var rows = printData["rows"];
            for (var i = 0; i < rows.length; i++) {
                var itemInner = tempRow.innerHTML;
                for (var r in rows[i]) {
                    itemInner = itemInner.replace(new RegExp("{" + r + "}", "ig"), rows[i][r]);
                }
                temp.innerHTML += itemInner;
            }
        };
        window.onbeforeunload = function () {
            event.returnValue = false;
            return "请确认已经打印完成，是否继续操作?";
        };
        function printDoc() {
            window.print();
        }
        function closeWindow() {
            window.close();
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
