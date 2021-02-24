using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TestReport2.Reports;
namespace TestReport2.Controllers
{
    public class NewController : Controller
    {
        // GET: New
        HotelManagementEntities db = new HotelManagementEntities();
        public ActionResult Index()
        {
            LocalReport lr = new LocalReport();
            string p = Path.Combine(Server.MapPath("~/Reports"), "Report1.rdlc");
            lr.ReportPath = p;

            List<CustInOut> chkin = new List<Reports.CustInOut>();
            // chkin = db.CheckedInOuts.ToList();

            chkin = (from cinout in db.CheckedInOuts
                                 join cust in db.Customers on cinout.CustomerId equals cust.CustomerId

                                 select new CustInOut
                                 {
                                     CustomerId = cinout.CustomerId,
                                     CustomerName = cust.CustomerName,
                                     Staying = cinout.Staying,
                                     CheckedInDate = (cinout.CheckedInDate),
                                     CheckOutDate =  (cinout.CheckOutDate),
                                     Total = cinout.Total,
                                     Remain = cinout.Remain,
                                     Paid = cinout.Paid,
                                     PassportNumber = cust.PassportNumber,
                                     CardNumber=cust.CardNumber
                                      }).ToList();



            ReportDataSource rd = new ReportDataSource("DataSet1", chkin);
            lr.DataSources.Add(rd);

            string mt, enc, f;
            string[] s;
            Warning[] w;
            byte[] b = lr.Render("PDF", null, out mt, out enc, out f, out s, out w);
            return File(b, mt);
        }
        public ActionResult getView()
        {
            List<CustInOut> chkin = new List<Reports.CustInOut>();
            // chkin = db.CheckedInOuts.ToList();

            chkin = (from cinout in db.CheckedInOuts
                     join cust in db.Customers on cinout.CustomerId equals cust.CustomerId

                     select new CustInOut
                     {
                         CustomerId = cinout.CustomerId,
                         CustomerName = cust.CustomerName,
                         Staying = cinout.Staying,
                         CheckedInDate = (cinout.CheckedInDate),
                         CheckOutDate = (cinout.CheckOutDate),
                         Total = cinout.Total,
                         Remain = cinout.Remain,
                         Paid = cinout.Paid,
                         PassportNumber = cust.PassportNumber,
                         CardNumber = cust.CardNumber
                     }).ToList();
            return View(chkin);
        }
    }
}