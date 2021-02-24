using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestReport2.Reports
{
    public class CustInOut
    {
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CardNumber { get; set; }
        public string PassportNumber { get; set; }
        public int? Staying { get; set; }
        public DateTime? CheckedInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal? Total { get; set; }
        public decimal? Paid { get; set; }
        public decimal? Remain { get; set; }
    }
}