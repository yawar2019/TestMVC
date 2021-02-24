using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using TestMVC.Reports;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home

        TestMVC.Reports.MyDataset ds = new TestMVC.Reports.MyDataset();
        HotelManagementEntities db = new HotelManagementEntities();
        public ActionResult ReportEmployee()
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Reports"), "Report1.rdlc");
            lr.ReportPath = p;

            List<CheckedInOut> chkin = new List<Reports.CheckedInOut>();
            chkin = db.CheckedInOuts.ToList() ;

            //var goods_in_data = (from cinout in db.CheckedInOuts
            //                     join cust in db.Customers on cinout.CustomerId equals cust.CustomerId
                         
            //                     select new { cinout, cust }).ToList();



            ReportDataSource rd = new ReportDataSource("MyDataSet", chkin);
            lr.DataSources.Add(rd);
           //ReportDataSource rd1 = new ReportDataSource("DataSet1", goods_in_data);
           // lr.DataSources.Add(rd1);
            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w); 
            return File(b,mt);
        }
        public ActionResult ReportEmployee2()
        {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(1200);
            reportViewer.Height = Unit.Percentage(1200);

            var connectionString = ConfigurationManager.ConnectionStrings["HotelManagementConnectionString"].ConnectionString;


            SqlConnection conx = new SqlConnection(connectionString); SqlDataAdapter adp = new SqlDataAdapter("SELECT * FROM [HotelManagement].[dbo].[CheckedInOut]", conx);

            adp.Fill(ds, ds.CheckedInOut.TableName);

            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Report1.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("MyDataSet", ds.Tables[0]));


            ViewBag.ReportViewer = reportViewer;

            return View();
        }
    }
}
