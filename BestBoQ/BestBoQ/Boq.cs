using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestBoQ
{
    public class Boq
    {
        public System.Web.UI.Page page;

        public Boq(System.Web.UI.Page page)
        {
            this.page = page;
        }

        //test git

        public string CreatePDF(string projectID, string userID)
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
            converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Portrait;
            converter.Options.MarginLeft = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginTop = 20;
            converter.Options.MarginBottom = 20;

            string url = "http://localhost:<port>/boqdemo/boq.aspx?projectID=<projectid>&userID=<userID>".Replace("<port>", HttpContext.Current.Request.Url.Port.ToString()).Replace("<projectid>", projectID).Replace("<userID>", userID);
            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);
            string fileName = "boq_<projectid>.pdf".Replace("<projectid>", projectID);
            string pdfPath = page.Server.MapPath(@"~\PDFs\") + fileName;
            doc.Save(pdfPath);
            doc.Close();

            return fileName;
        }
    }
}