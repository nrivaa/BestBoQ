using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BestBoQ
{
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;

        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        #endregion

        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(document.PageSize.Width, document.PageSize.Height);
                footerTemplate = cb.CreateTemplate(document.PageSize.Width, document.PageSize.Height);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            //iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);
            //iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
            //Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);
            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("Images/logo.png"));
            png.ScaleAbsolute(160, 40);

            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(3);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell();
            PdfPCell pdfCell2 = new PdfPCell(png);
            PdfPCell pdfCell3 = new PdfPCell();
            //String text = @" " + Environment.NewLine + @"";//"Page " + writer.PageNumber + " of ";

            

            //Add paging to header
            {
                //cb.BeginText();
                //cb.SetFontAndSize(bf, 12);
                //cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
                //cb.ShowText(text);
                //cb.EndText();
                //float len = bf.GetWidthPoint(text, 12);
                ////Adds "12" in Page 1 of 12
                //cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
            }
            //Add paging to footer
            {
                //cb.BeginText();
                //cb.SetFontAndSize(bf, 12);
                ////cb.SetTextMatrix(document.PageSize.GetRight(50), document.PageSize.GetBottom(80));
                //cb.SetTextMatrix(0, document.PageSize.GetBottom(80));
                //cb.ShowText(text);
                //cb.EndText();
                //float len = bf.GetWidthPoint(text, 12);
                //cb.AddTemplate(footerTemplate, document.PageSize.GetRight(50) + len, document.PageSize.GetBottom(80));
            }

            PdfPTable tabHeader = new PdfPTable(1);
            PdfPCell headerline1 = new PdfPCell(new Phrase("บริษัท เนเจอร์ เอ็ซเทท จำกัด[companyname] | 373/31 ถ.สกลนคร - กาฬสินธุ์ ต.ธาตุเชิงชุม อ.เมืองสกลนคร", new Font(bf,12)));
            PdfPCell headerline2 = new PdfPCell(new Phrase("จ.สกลนคร 47000", new Font(bf, 12)));
            PdfPCell headerline3 = new PdfPCell(new Phrase("Tel: 085 - 445 - 4334", new Font(bf, 12)));

            headerline1.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerline2.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerline3.HorizontalAlignment = Element.ALIGN_RIGHT;

            headerline1.Border = 0;
            headerline2.Border = 0;
            headerline3.Border = 0;

            tabHeader.AddCell(headerline1);
            tabHeader.AddCell(headerline2);
            tabHeader.AddCell(headerline3);

            tabHeader.TotalWidth = document.PageSize.Width - 80f;
            tabHeader.WriteSelectedRows(0, -1, 40, document.PageSize.GetBottom(80), writer.DirectContent);
            
            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;

            pdfCell1.Border = 0;
            pdfCell2.Border = 0;
            pdfCell3.Border = 0;

            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell1);
            pdfTab.AddCell(pdfCell2);
            pdfTab.AddCell(pdfCell3);

            pdfTab.TotalWidth = document.PageSize.Width - 80f;
            //pdfTab.WidthPercentage = 70;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;    

            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
            //set pdfContent value

            //Move the pointer and draw line to separate header section from rest of page
            //cb.MoveTo(40, document.PageSize.Height - 100);
            //cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
            //cb.Stroke();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, document.PageSize.GetBottom(80));
            cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(80));
            cb.Stroke();
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}