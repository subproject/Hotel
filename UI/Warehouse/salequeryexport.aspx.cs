using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HotelLogic.WareHouse;

public partial class Warehouse_salequeryexport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ExportOut();

        //Response.Clear();
        //Response.Buffer = true;
        //Response.Charset = "utf-8";
        //Response.ContentEncoding = System.Text.Encoding.UTF8;
        //Response.AppendHeader("content-disposition", "attachment;filename=\"" + HttpUtility.HtmlEncode(Request["txtName"] ?? DateTime.Now.ToString("yyyyMMdd")) + ".html\"");
        //Response.ContentType = "Application/ms-excel";
        //Response.Write("<html>\n<head>\n");
        //Response.Write("<style type=\"text/css\">\n.pb{font-size:13px;border-collapse:collapse;} " +
        //               "\n.pb th{font-weight:bold;text-align:center;border:0.5pt solid windowtext;padding:2px;} " +
        //               "\n.pb td{border:0.5pt solid windowtext;padding:2px;}\n</style>\n</head>\n");
        //Response.Write("<body>\n" + Request["txtContent"] + "\n</body>\n</html>");
        //Response.Flush();
        //Response.End(); 
    }

    protected void ExportOut()
    {

        Microsoft.Office.Interop.Excel.Application excel1 = new Microsoft.Office.Interop.Excel.Application();
        excel1.DisplayAlerts = false;
        Microsoft.Office.Interop.Excel.Workbook workbook1 = excel1.Workbooks.Add(Type.Missing);
        excel1.Visible = false;
        Microsoft.Office.Interop.Excel.Worksheet worksheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook1.Worksheets["sheet1"];  //表头            
        worksheet1.Cells[1, 1] = "商品名称";  //Excel里从第1行，第1列计算             
        worksheet1.Cells[1, 2] = "单位";
        worksheet1.Cells[1, 3] = "数量";
        worksheet1.Cells[1, 4] = "成本单价";
        worksheet1.Cells[1, 5] = "平均售价";
        worksheet1.Cells[1, 6] = "销售额";
        worksheet1.Cells[1, 7] = "毛利";
        //System.Data.DataTable dt = GetTestData(worksheet1);
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    for (int j = 0; j < dt.Columns.Count; j++)
        //        worksheet1.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();
        //}
        GetTestData(worksheet1);
        string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
        string filePath = Server.MapPath("~/" + fileName);
        workbook1.SaveAs(filePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        excel1.Workbooks.Close();  
        excel1.Quit();
        int generation = GC.GetGeneration(excel1);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel1); excel1 = null; GC.Collect(generation);        //打开要下载的文件，并把该文件存放在FileStream中            
        System.IO.FileStream Reader = System.IO.File.OpenRead(filePath);
        //文件传送的剩余字节数：初始值为文件的总大小             
        long Length = Reader.Length;
        HttpContext.Current.Response.Buffer = false;
        HttpContext.Current.Response.AddHeader("Connection", "Keep-Alive");
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
        HttpContext.Current.Response.AddHeader("Content-Length", Length.ToString());
        byte[] Buffer = new Byte[10000];    //存放欲发送数据的缓冲区             
        int ByteToRead;              //每次实际读取的字节数             
        while (Length > 0)
        {
            //剩余字节数不为零，继续传送                
            if (Response.IsClientConnected)
            {                //客户端浏览器还打开着，继续传送                     
                ByteToRead = Reader.Read(Buffer, 0, 10000); //往缓冲区读入数据                     
                HttpContext.Current.Response.OutputStream.Write(Buffer, 0, ByteToRead); //把缓冲区的数据写入客户端浏览器                  HttpContext.Current.Response.Flush();   //立即写入客户端                   
                Length -= ByteToRead;   //剩余字节数减少                 
            }
            else
            {                //客户端浏览器已经断开，阻止继续循环                    
                Length = -1;
            }
        }
        //关闭该文件             
        Reader.Close();
        if (System.IO.File.Exists(filePath))
            System.IO.File.Delete(filePath);
    }
    private void GetTestData(Microsoft.Office.Interop.Excel.Worksheet worksheet1)
    {
        DateTime starttime = new DateTime();
        DateTime enddate = new DateTime();
        if (!string.IsNullOrEmpty(Request["starttime"]))
        {
            starttime = Convert.ToDateTime(Request["starttime"]);//"01/01/2001 00:00"
        }
        if (!string.IsNullOrEmpty(Request["endtime"]))
        {
            enddate = Convert.ToDateTime(Request["endtime"]);//"01/01/2001 00:00"
        }

        SaleProfitQueryHelper supplierHelper = new SaleProfitQueryHelper();

        List<SaleProfitQueryViewModel> resultCardNo = supplierHelper.query(starttime, enddate, Request["goodsid"], Request["goodsposition"], Request["goodsType"]);


         
            //dt.Columns.Add("payprice");
            int excel_cur = 2;///计数器 表示从EXCEL的第二行开始写入数据
            foreach (SaleProfitQueryViewModel dr in resultCardNo)
            {
               
                worksheet1.Cells[excel_cur, 1] = dr.goodsName;
                    
                worksheet1.Cells[excel_cur,2] = dr.unit ;
                    
                worksheet1.Cells[excel_cur, 3] = dr.saleCount.ToString();
                worksheet1.Cells[excel_cur, 4] = dr.inPrice;
                worksheet1.Cells[excel_cur, 5] = dr.agvSalePrice;

                worksheet1.Cells[excel_cur, 6] = dr.saleTotal;
                worksheet1.Cells[excel_cur, 7] = dr.grossProfit;
              
                excel_cur++;
            }
          
    }

}