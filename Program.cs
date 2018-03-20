using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using ExporPdfFromSSRS.ReportExecution2005;
using System.Web.Services.Protocols;
using System.Net;

namespace ExporPdfFromSSRS
{
    class Program
    {
        public static void Main(string[] args)
        {
            ExportPDF.ExportReport("report6","PDF","20180228 - 20180228");

        }
    }
}
