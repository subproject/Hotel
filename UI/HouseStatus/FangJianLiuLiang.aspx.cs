using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.HouseStatus;
using System.Text;

public partial class HouseStatus_FangJianLiuLiang : System.Web.UI.Page
{
    public string tablestr = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        LiuLiangHelper helper = new LiuLiangHelper();
        StringBuilder sb = new StringBuilder();
        List<JBSummer> lists = helper.GetJBSummers();
        DateTime begin;
        if (!string.IsNullOrEmpty(Request["begin"]))
        {
            begin = Convert.ToDateTime(Request["begin"]);
            this.begin.Value = Request["begin"];
        }
        else
        {
            begin = System.DateTime.Today;
            this.begin.Value = begin.ToShortDateString();
        }
        DateTime end;
        if (!string.IsNullOrEmpty(Request["end"]))
        {
            end = Convert.ToDateTime(Request["end"]);
            this.end.Value = Request["end"];
        }
        else
        {
            end = System.DateTime.Today.AddDays(20);
            this.end.Value = end.ToShortDateString();
        }

        System.DateTime current = begin;

        sb.Append("<table id=\"tt\" class=\"easyui-datagrid\" style=\"padding:2px\">");
        sb.Append("<thead><tr>");
        sb.Append("<th field=\"Date\" width=\"95px\">日期</th>");
        foreach (var h in lists)
        {
            sb.Append("<th field=" + h.JBName + " width=\"95px\">" + h.JBName + "(" + h.Count + ")" + "</th>");
        }
        sb.Append("</tr></thead>");
        sb.Append("<tbody>");

        int days = (end - begin).Days;

        for (int i = 0; i <= days; i++)
        {
            sb.Append("<tr>");
            sb.Append("<td><span style=\"font-weight:bold\">"+current.ToShortDateString()+"</span></td>");
            foreach (var l in lists)
            {
                sb.Append("<td>"+helper.GetStatus(l,current)+"</td>");
            }
            sb.Append("</tr>");
            current = current.AddDays(1);
        }
        sb.Append("</tbody></table>");

        tablestr = sb.ToString();
    }
}

