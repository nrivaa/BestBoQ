using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

namespace BestBoQ
{
    public partial class GenerateContract : System.Web.UI.Page
    {
        Font h1, bold, smallBold, normal, smallNormal;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Bold
            BaseFont bf_bold = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("fonts/angsa.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            h1 = new Font(bf_bold, 18);
            bold = new Font(bf_bold, 16);
            smallBold = new Font(bf_bold, 14);

            // Normal
            BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("fonts/angsa.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            normal = new Font(bf_normal, 16);
            smallNormal = new Font(bf_normal, 14);

            try
            {
                // Create PDF document
                Document pdfDoc = new Document(PageSize.A4, 30, 30, 20, 20);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();

                pdfDoc.Add(GetHeader());
                pdfDoc.Add(GetHeaderDetail());

                LineSeparator line = new LineSeparator();

                pdfDoc.Add(line);

                pdfDoc.Add(GetBodyHeader());
                pdfDoc.Add(GetBody());
                pdfDoc.Add(GetActivities());
                pdfDoc.Add(GetBodyFooter());
                GetSignature(pdfDoc);
                pdfDoc.NewPage();
                pdfDoc.Add(GetStudentList());

                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename = Example.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                Response.End();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private Paragraph GetBodyHeader()
        {
            Phrase p = new Phrase();

            p.Add(new Chunk("เรียน ", normal));
            p.Add(new Chunk("รองอธิการบดีฝ่ายองค์กรสัมพันธ์และสารสนเทศ", normal));

            Paragraph para = new Paragraph(p);
            para.SpacingBefore = 20;
            para.SpacingAfter = 20;

            return para;
        }

        private Paragraph GetBody()
        {
            Paragraph para = new Paragraph();

            para.FirstLineIndent = 38.1f;

            para.Add(new Phrase("ด้วย", normal));
            para.Add(new Phrase("งานวิเทศสัมพันธ์", normal));
            para.Add(new Phrase("ขออนุมัติให้นักศึกษาจำนวนทั้งหมด " + "10" + " คน เดินทางไปราชการต่างประเทศระหว่างวันที่ " + "1" + " ถึงวันที่ " + "2" + " รวม " + "2" + " วัน เพื่อดำเนินกิจกรรมดังต่อไปนี้", normal));

            return para;
        }

        private PdfPTable GetActivities()
        {
            PdfPTable table = new PdfPTable(3);

            table.TotalWidth = 530f;
            table.HorizontalAlignment = 0;
            table.SpacingBefore = 20;
            table.SpacingAfter = 20;

            // ชื่อกิจกรรม
            // สถาบัน
            // ประเทศ

            float[] columnWidths = new float[3];
            columnWidths[0] = 200f;
            columnWidths[1] = 200f;
            columnWidths[2] = 130f;

            table.SetWidths(columnWidths);
            table.LockedWidth = true;

            PdfPCell cell0 = new PdfPCell(new Phrase("กิจกรรม", bold));
            cell0.HorizontalAlignment = Element.ALIGN_LEFT;
            cell0.Border = Rectangle.NO_BORDER;
            table.AddCell(cell0);

            PdfPCell cell1 = new PdfPCell(new Phrase("สภานที่", bold));
            cell1.HorizontalAlignment = Element.ALIGN_LEFT;
            cell1.Border = Rectangle.NO_BORDER;
            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase("ประเทศ", bold));
            cell2.HorizontalAlignment = Element.ALIGN_LEFT;
            cell2.Border = Rectangle.NO_BORDER;
            table.AddCell(cell2);

            //List<MasterActivity> activity = master.GetActivities();

            //foreach (MasterActivity a in activity)
            {
                //cell0 = new PdfPCell(new Phrase(a.ActivityNameThai, normal));
                cell0 = new PdfPCell(new Phrase("activity", normal));
                cell0.HorizontalAlignment = Element.ALIGN_LEFT;
                cell0.Border = Rectangle.NO_BORDER;
                table.AddCell(cell0);

                //cell1 = new PdfPCell(new Phrase(a.HostName, normal));
                cell1 = new PdfPCell(new Phrase("hostname", normal));
                cell1.HorizontalAlignment = Element.ALIGN_LEFT;
                cell1.Border = Rectangle.NO_BORDER;
                table.AddCell(cell1);

                //Institution host = Institution.GetById(a.HostId);

                //cell2 = new PdfPCell(new Phrase(host.CountryName, normal));
                cell2 = new PdfPCell(new Phrase("host countryname", normal));
                cell2.HorizontalAlignment = Element.ALIGN_LEFT;
                cell2.Border = Rectangle.NO_BORDER;
                table.AddCell(cell2);
            }

            return table;
        }

        private Paragraph GetBodyFooter()
        {
            Paragraph para = new Paragraph(new Phrase("จึงเรียนมาเพื่อโปรดพิจารณาอนุมัติด้วย จักเป็นพระคุณยิ่ง", normal));
            para.FirstLineIndent = 38.1f;
            para.SpacingAfter = 25;
            return para;
        }

        private void GetSignature(Document pdfDoc)
        {

            Paragraph para;
            Phrase p;
            Chunk dotLine = new Chunk("……………………………………………", normal);

            //if (master.LevelId.Equals(“D”))
            //{
            // p = new Phrase(dotLine);
            // p.Add(new Chunk(“หัวหน้าภาควิชา”, normal));
            // para = new Paragraph(p);
            // pdfDoc.Add(para);
            //}

            p = new Phrase(dotLine);
            p.Add(new Chunk("หัวหน้าภาควิชา", normal));
            para = new Paragraph(p);
            para.SpacingAfter = 15;
            pdfDoc.Add(para);

            p = new Phrase(dotLine);
            p.Add(new Chunk("คณบดี", normal));
            para = new Paragraph(p);
            para.SpacingAfter = 15;
            pdfDoc.Add(para);
        }



        private PdfPTable GetStudentList()
        {

            Phrase p;

            PdfPTable table = new PdfPTable(8);
            table.TotalWidth = 530f;
            table.HorizontalAlignment = 0;
            //table.SpacingAfter = 20;
            //headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            float[] colWidths = new float[8];
            colWidths[0] = 30f;
            colWidths[1] = 70f;
            colWidths[2] = 70f;
            colWidths[3] = 70f;
            colWidths[4] = 70f;
            colWidths[5] = 70f;
            colWidths[6] = 70f;
            colWidths[7] = 70f;

            table.SetWidths(colWidths);
            table.LockedWidth = true;

            PdfPCell cell;

            p = new Phrase("รายชื่อผู้เดินทางจาก " +"1" + " ถึง " +"2", normal);

            cell = new PdfPCell(p);
            cell.Colspan = 8;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.NO_BORDER;
            cell.PaddingBottom = 10;
            table.AddCell(cell);

            #region Header

            cell = new PdfPCell(new Phrase("ที่", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("รหัสนักศึกษา", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("คำนำหน้า", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("ชื่อ", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("สกุล", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("คณะ", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("วันที่เริ่ม", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            cell = new PdfPCell(new Phrase("วันที่สิ้นสุด", smallBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);

            #endregion

            #region Data

            //List<OutboundApplication> apps = master.GetApplications();

            //int i = 0;

            //foreach (OutboundApplication app in apps)
            //{
            //    cell = new PdfPCell(new Phrase((i + 1).ToString(), smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.StudentId, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.TitleName, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.Firstname, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.Lastname, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.FacultyNameThai, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.StartDate, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(app.EndDate, smallNormal));
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    table.AddCell(cell);

            //    i += 1;
            //}

            #endregion

            return table;
        }

        private PdfPTable GetHeaderDetail()
        {
            PdfPTable table = new PdfPTable(2);
            table.TotalWidth = 530f;
            table.HorizontalAlignment = 0;
            table.SpacingAfter = 10;

            float[] tableWidths = new float[2];
            tableWidths[0] = 400f;
            tableWidths[1] = 130f;

            table.SetWidths(tableWidths);
            table.LockedWidth = true;

            Chunk blank = new Chunk(" ", normal);

            Phrase p = new Phrase();

            p.Add(new Chunk("ส่วนราชการ", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk("วิเทศสัมพันธ์", normal));

            PdfPCell cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            p = new Phrase();
            p.Add(new Chunk("โทร", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk("2012", normal));

            PdfPCell cell1 = new PdfPCell(p);
            cell1.Border = Rectangle.NO_BORDER;

            table.AddCell(cell1);

            p = new Phrase();
            p.Add(new Chunk("ที่ มอ", bold));
            p.Add(new Chunk(blank));
            //p.Add(new Chunk(master.ApplicationNo, normal));
            p.Add(new Chunk("ApplicationNo", normal));

            cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;

            table.AddCell(cell0);

            p = new Phrase();
            p.Add(new Chunk("วันที่", bold));
            p.Add(new Chunk(blank));
            //p.Add(new Chunk(master.CreatedDate, normal));
            p.Add(new Chunk("CreatedDate", normal));

            cell1 = new PdfPCell(p);
            cell1.Border = Rectangle.NO_BORDER;

            table.AddCell(cell1);

            p = new Phrase();
            p.Add(new Chunk("เรื่อง", bold));
            p.Add(new Chunk(blank));
            p.Add(new Chunk("ขออนุมัติเดินทางไปต่างประเทศ", normal));

            cell0 = new PdfPCell(p);
            cell0.Border = Rectangle.NO_BORDER;
            cell0.Colspan = 2;

            table.AddCell(cell0);

            return table;
        }

        private PdfPTable GetHeader()
        {

            PdfPTable headerTable = new PdfPTable(2);
            headerTable.TotalWidth = 530f;
            headerTable.HorizontalAlignment = 0;
            headerTable.SpacingAfter = 20;
            //headerTable.DefaultCell.Border = Rectangle.NO_BORDER;

            float[] headerTableColWidth = new float[2];
            headerTableColWidth[0] = 220f;
            headerTableColWidth[1] = 310f;

            headerTable.SetWidths(headerTableColWidth);
            headerTable.LockedWidth = true;

            iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("Images/thai_gov.png"));
            png.ScaleAbsolute(40, 40);

            PdfPCell headerTableCell_0 = new PdfPCell(png);
            headerTableCell_0.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTableCell_0.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(headerTableCell_0);

            PdfPCell headerTableCell_1 = new PdfPCell(new Phrase("บันทึกข้อความ", h1));
            headerTableCell_1.HorizontalAlignment = Element.ALIGN_LEFT;
            headerTableCell_1.VerticalAlignment = Element.ALIGN_BOTTOM;
            headerTableCell_1.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(headerTableCell_1);

            return headerTable;
        }
    }
}