using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InfoSoftGlobal;
using HotelLogic;

public partial class _22ChartsDemo : HotelPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Generate chart in Literal Control
        FCLiteral1.Text = CreateChart(1);
        FCLiteral2.Text = CreateChart(2);
        FCLiteral3.Text = CreateChart(3);
        FCLiteral4.Text = CreateChart(4);
        FCLiteral5.Text = CreateChart(5);
        FCLiteral6.Text = CreateChart(6);
        FCLiteral7.Text = CreateChart(7);
        FCLiteral8.Text = CreateChart(8);
        FCLiteral9.Text = CreateChart(9);
        FCLiteral10.Text = CreateChart(10);
        FCLiteral11.Text = CreateChart(11);
        FCLiteral12.Text = CreateChart(12);
        FCLiteral13.Text = CreateChart(13);
        FCLiteral14.Text = CreateChart(14);
        FCLiteral15.Text = CreateChart(15);
        FCLiteral16.Text = CreateChart(16);
        FCLiteral17.Text = CreateChart(17);
        FCLiteral18.Text = CreateChart(18);
        FCLiteral19.Text = CreateChart(19);
        FCLiteral20.Text = CreateChart(20);
        FCLiteral21.Text = CreateChart(21);
        FCLiteral22.Text = CreateChart(22);
    }

    public string CreateChart(int i)
    {
        //We first request the data from the form (Default.asp)
        string intSoups, intSalads, intSandwiches, intBeverages, intDesserts;

        intSoups = "10";
        intSalads = "20";
        intSandwiches ="12";
        intBeverages = "14";
        intDesserts ="16";

        //In this example, we're directly showing this data back on chart.
        //In your apps, you can do the required processing and then show the 
        //relevant data only.

        //Now that we've the data in variables, we need to convert this into XML.
        //The simplest method to convert data into XML is using string concatenation.	
        string strXML;
        //Initialize <graph> element
        strXML = "<graph caption='Sales by Product Category' subCaption='For this week' showPercentageInLabel='1' pieSliceDepth='25'  decimalPrecision='0' showNames='1'>";
        //Add all data
        strXML += "<set name='Soups' value='" + intSoups + "' />";
        strXML += "<set name='Salads' value='" + intSalads + "' />";
        strXML += "<set name='Sandwiches' value='" + intSandwiches + "' />";
        strXML += "<set name='Beverages' value='" + intBeverages + "' />";
        strXML += "<set name='Desserts' value='" + intDesserts + "' />";
        //Close <graph> element
        strXML += "</graph>";

        //Create the chart - Pie 3D Chart with data from strXML
        if(i==1)
            return FusionCharts.RenderChart("FusionCharts/FCF_Area2D.swf", "", strXML, "Sales1", "600", "350", false, false);
        if (i == 2)
            return FusionCharts.RenderChart("FusionCharts/FCF_Bar2D.swf", "", strXML, "Sales2", "600", "350", false, false);
        if (i == 3)
            return FusionCharts.RenderChart("FusionCharts/FCF_Candlestick.swf", "", strXML, "Sales3", "600", "350", false, false);
        if (i == 4)
            return FusionCharts.RenderChart("FusionCharts/FCF_Column2D.swf", "", strXML, "Sales4", "600", "350", false, false);
        if (i == 5)
            return FusionCharts.RenderChart("FusionCharts/FCF_Column3D.swf", "", strXML, "Sales5", "600", "350", false, false);
        if (i == 6)
            return FusionCharts.RenderChart("FusionCharts/FCF_Doughnut2D.swf", "", strXML, "Sales6", "600", "350", false, false);
        if (i == 7)
            return FusionCharts.RenderChart("FusionCharts/FCF_Funnel.swf", "", strXML, "Sales7", "600", "350", false, false);
        if (i == 8)
            return FusionCharts.RenderChart("FusionCharts/FCF_Gantt.swf", "", strXML, "Sales8", "600", "350", false, false);
        if (i == 9)
            return FusionCharts.RenderChart("FusionCharts/FCF_Line.swf", "", strXML, "Sales9", "600", "350", false, false);
        if (i == 10)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSArea2D.swf", "", strXML, "Sales10", "600", "350", false, false);
        if (i == 11)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSBar2D.swf", "", strXML, "Sales11", "600", "350", false, false);
        if (i == 12)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSColumn2D.swf", "", strXML, "Sales12", "600", "350", false, false);
        if (i == 13)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSColumn2DLineDY.swf", "", strXML, "Sales13", "600", "350", false, false);
        if (i == 14)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSColumn3D.swf", "", strXML, "Sales14", "600", "350", false, false);
        if (i == 15)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSColumn3DLineDY.swf", "", strXML, "Sales15", "600", "350", false, false);
        if (i == 16)
            return FusionCharts.RenderChart("FusionCharts/FCF_MSLine.swf", "", strXML, "Sales16", "600", "350", false, false);
        if (i == 17)
            return FusionCharts.RenderChart("FusionCharts/FCF_Pie2D.swf", "", strXML, "Sales17", "600", "350", false, false);
        if (i == 18)
            return FusionCharts.RenderChart("FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales18", "600", "350", false, false);
        if (i == 19)
            return FusionCharts.RenderChart("FusionCharts/FCF_StackedArea2D.swf", "", strXML, "Sales19", "600", "350", false, false);
        if (i == 20)
            return FusionCharts.RenderChart("FusionCharts/FCF_StackedBar2D.swf", "", strXML, "Sales20", "600", "350", false, false);
        if (i == 21)
            return FusionCharts.RenderChart("FusionCharts/FCF_StackedColumn2D.swf", "", strXML, "Sales21", "600", "350", false, false);
        if (i == 22)
            return FusionCharts.RenderChart("FusionCharts/FCF_StackedColumn3D.swf", "", strXML, "Sales22", "600", "350", false, false);
        else
            return FusionCharts.RenderChart("FusionCharts/FCF_Pie3D.swf", "", strXML, "Sales23", "600", "350", false, false);
    }
}