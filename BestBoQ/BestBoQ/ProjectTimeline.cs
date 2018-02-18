using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BestBoQ
{
    public class ProjectTimeline
    {
        public System.Web.UI.Page page;

        public ProjectTimeline(System.Web.UI.Page page)
        {
            this.page = page;
        }

        public string CreatePDF(string projectID)
        {
            SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
            converter.Options.PdfPageSize = SelectPdf.PdfPageSize.A3;
            converter.Options.PdfPageOrientation = SelectPdf.PdfPageOrientation.Landscape;

            string url = "http://localhost:<port>/Timeline2.aspx?projectID=<projectid>".Replace("<port>", HttpContext.Current.Request.Url.Port.ToString()).Replace("<projectid>", projectID);
            SelectPdf.PdfDocument doc = converter.ConvertUrl(url);
            string fileName = "timeplan_<projectid>.pdf".Replace("<projectid>", projectID);
            string pdfPath = page.Server.MapPath(@"~\PDFs\") + fileName;
            doc.Save(pdfPath);
            doc.Close();

            return fileName;
        }

    }
}