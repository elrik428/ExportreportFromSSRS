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
    class ExportPDF
    {
        public static void ExportReport(string reportName, string reportFormat,string dateFromTo)
        {
            //Define Web services 

            //New proxy for Web Service
            ReportExecutionService rsexec = new ReportExecutionService();
            rsexec = new ReportExecutionService();

            //Auhenticate to Web Service 
            rsexec.Credentials = System.Net.CredentialCache.DefaultCredentials;

            // Define URL for report server
            rsexec.Url = "https://grat1-dev-ap2t.dir.eeft.com:443/ReportServer/ReportExecution2005.asmx";
                        
            // Render arguments
            byte[] result = null;
            string reportPath = "/TestFolder/" + reportName;
            string format = "PDF";
            string historyID = null;
            string devInfo = @"<DeviceInfo><Toolbar>False</Toolbar></DeviceInfo>";

            // Prepare report parameter.
            ParameterValue[] parameters = new ParameterValue[3];
            //parameters[0] = new ParameterValue();
            //parameters[0].Name = " ";
            //parameters[0].Value = " ";

            DataSourceCredentials[] credentials = null;
            string showHideToggle = null;
            string encoding;
            string mimeType;
            string extension;
            Warning[] warnings = null;
            ParameterValue[] reportHistoryParameters = null;
            string[] streamIDs = null;

            ExecutionInfo execInfo = new ExecutionInfo();
            ExecutionHeader execHeader = new ExecutionHeader();

            rsexec.ExecutionHeaderValue = execHeader;

            try
            {
                execInfo = rsexec.LoadReport(reportPath, historyID);
            }

            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Status);
            }
            //rsexec.setexecutioncredentials(creds);

            rsexec.SetExecutionParameters(parameters, "en-us");
            string SessionId = rsexec.ExecutionHeaderValue.ExecutionID;

            Console.WriteLine("SessionID: {0}", rsexec.ExecutionHeaderValue.ExecutionID);

            try
            {
                result = rsexec.Render(format, devInfo, out extension, out encoding, out mimeType, out warnings, out streamIDs);
                execInfo = rsexec.GetExecutionInfo();
                Console.WriteLine("Execution date and time :  {0}", execInfo.ExecutionDateTime);

            }
            catch (SoapException e)
            {
                Console.WriteLine(e.Detail.OuterXml);
            }
            try
            {
                FileStream stream = File.Create("C:\\Users\\lnestoras\\Desktop\\" + reportName + "_" + dateFromTo + "." + reportFormat, result.Length);
                Console.WriteLine("File created. ");
                stream.Write(result, 0, result.Length);
                Console.WriteLine("Result written to file. ");
                stream.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
