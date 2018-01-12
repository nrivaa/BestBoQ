using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BestBoQ
{
    public class AppendixA
    {
        public System.Web.UI.Page page;

        public AppendixA(System.Web.UI.Page page)
        {
            this.page = page;
        }

        private string ThaiBaht(string txt)
        {
            string bahtTxt, n, bahtTH = "";
            double amount;
            try { amount = Convert.ToDouble(txt); }
            catch { amount = 0; }
            bahtTxt = amount.ToString("####.00");
            string[] num = { "ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ" };
            string[] rank = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] temp = bahtTxt.Split('.');
            string intVal = temp[0];
            string decVal = temp[1];
            if (Convert.ToDouble(bahtTxt) == 0)
                bahtTH = "ศูนย์บาทถ้วน";
            else
            {
                for (int i = 0; i < intVal.Length; i++)
                {
                    n = intVal.Substring(i, 1);
                    if (n != "0")
                    {
                        if ((i == (intVal.Length - 1)) && (n == "1"))
                            bahtTH += "เอ็ด";
                        else if ((i == (intVal.Length - 2)) && (n == "2"))
                            bahtTH += "ยี่";
                        else if ((i == (intVal.Length - 2)) && (n == "1"))
                            bahtTH += "";
                        else
                            bahtTH += num[Convert.ToInt32(n)];
                        bahtTH += rank[(intVal.Length - i) - 1];
                    }
                }
                bahtTH += "บาท";
                if (decVal == "00")
                    bahtTH += "ถ้วน";
                else
                {
                    for (int i = 0; i < decVal.Length; i++)
                    {
                        n = decVal.Substring(i, 1);
                        if (n != "0")
                        {
                            if ((i == decVal.Length - 1) && (n == "1"))
                                bahtTH += "เอ็ด";
                            else if ((i == (decVal.Length - 2)) && (n == "2"))
                                bahtTH += "ยี่";
                            else if ((i == (decVal.Length - 2)) && (n == "1"))
                                bahtTH += "";
                            else
                                bahtTH += num[Convert.ToInt32(n)];
                            bahtTH += rank[(decVal.Length - i) - 1];
                        }
                    }
                    bahtTH += "สตางค์";
                }
            }
            return bahtTH;
        }

        public string CreatePDF(string projectID)
        {
            //get data
            DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_AppendixA '" + projectID + "'");
            string projectName = dt.Rows[0]["projectname"].ToString();
            string homeType = dt.Rows[0]["hometype"].ToString();
            string customerName = dt.Rows[0]["customername"].ToString();
            string name = dt.Rows[0]["name"].ToString();
            string company = dt.Rows[0]["company"].ToString();
            string area = dt.Rows[0]["numMM"].ToString();
            string start = Convert.ToDateTime(dt.Rows[0]["start"]).ToString();
            string stop = Convert.ToDateTime(dt.Rows[0]["stop"]).ToString();
            string month = dt.Rows[0]["Month"].ToString();
            string total = dt.Rows[0]["Total"].ToString();
            string step1 = dt.Rows[0]["step01"].ToString();
            string step2 = dt.Rows[0]["step02"].ToString();
            string step3 = dt.Rows[0]["step03"].ToString();
            string step4 = dt.Rows[0]["step04"].ToString();
            string step5 = dt.Rows[0]["step05"].ToString();
            string step6 = dt.Rows[0]["step06"].ToString();
            string step7 = dt.Rows[0]["step07"].ToString();
            string step8 = dt.Rows[0]["step08"].ToString();
            string step9 = dt.Rows[0]["step09"].ToString();
            string step10 = dt.Rows[0]["step10"].ToString();

            BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            string fileName = string.Empty;
            DateTime fileCreationDatetime = DateTime.Now;
            fileName = string.Format("appendix_a_{0}.pdf", fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss"));
            string pdfPath = page.Server.MapPath(@"~\PDFs\") + fileName;

            using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 50f, 50f, 100f, 100f))
                {
                    try
                    {
                        // step 2
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = new ITextEvents();

                        //open the stream 
                        pdfDoc.Open();

                        Paragraph para = new Paragraph("ภาคผนวก ก: การชำระเงินค่าก่อสร้าง", new Font(bf_normal, 22));
                        para.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"เงื่อนไขการชำระเงินค่าก่อสร้าง [projecttype] พื้นที่ใช้สอย [area] ตารางเมตร".Replace("[projecttype]", homeType).Replace("[area]", area), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ทั้ง ""ผู้ว่าจ้าง"" และ ""ผู้รับจ้าง"" ได้ตกลงราคาค่าก่อสร้าง รวมทั้งค่าวัสดุ อุปกรณ์ และค่าแรงทั้งหมด", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"เป็นจำนวนเงินทั้งสิ้น [amount] บาท [amount_txt]".Replace("[amount]", total).Replace("[amount_txt]", ThaiBaht(total)), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        //para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"โดย ""ผู้ว่าจ้าง"" ตกลงแบ่งชำระเงินค่าก่อสร้าง เป็นงวดๆ ดังนี้" + Environment.NewLine, new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        // collection of table row
                        Dictionary<string, string> pdfTableRows = new Dictionary<string, string>();
                        pdfTableRows.Add("งวดที่ 1 20% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term1] บาท".Replace("[term1]", step1));
                        pdfTableRows.Add("เซ็นสัญญาว่าจ้างก่อสร้าง", "([term1str])".Replace("[term1str]", ThaiBaht(step1)));
                        pdfTableRows.Add("งวดที่ 2 15% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term2] บาท".Replace("[term2]", step2));
                        pdfTableRows.Add("ฐานรากงานคานคอดินแล้วเสร็จ", "([term2str]".Replace("[term2str]", ThaiBaht(step2)));
                        pdfTableRows.Add("งวดที่ 3 10% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term3] บาท".Replace("[term3]", step3));
                        pdfTableRows.Add("งานโครงสร้างทั้งหมดแล้วเสร็จ", "([term3str])".Replace("[term3str]", ThaiBaht(step3)));
                        pdfTableRows.Add("งวดที่ 4 10% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term4] บาท".Replace("[term4]", step4));
                        pdfTableRows.Add("งานโครงหลังคา/ยิงไม้เชิงชาย/งานพื้นคสลแล้วเสร็จ", "([term4str])".Replace("[term4str]", ThaiBaht(step4)));
                        pdfTableRows.Add("งวดที่ 5 5% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term5] บาท".Replace("[term5]", step5));
                        pdfTableRows.Add("งานก่อผนัง/วางระบบไฟ/วางระบบน้ำแล้วเสร็จ", "([terem5str])".Replace("[term5str]", ThaiBaht(step5)));
                        pdfTableRows.Add("งวดที่ 6 5% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term6] บาท".Replace("[term6]", step6));
                        pdfTableRows.Add("งานฉาบผนัง/งานมุงหลังคาแล้วเสร็จ", "([term6str])".Replace("[term6str]", ThaiBaht(step6)));
                        pdfTableRows.Add("งวดที่ 7 10% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term7] บาท".Replace("[term7]", step7));
                        pdfTableRows.Add("งานสีรองพื้น/งานโครงฝ้า/ร้อยสายไฟ/วางระบบประปาแล้วเสร็จ", "([term7str])".Replace("[term7str]", ThaiBaht(step7)));
                        pdfTableRows.Add("งวดที่ 8 5% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term8] บาท".Replace("[term8]", step8));
                        pdfTableRows.Add("งานกระเบื้องแล้วเสร็จ/งานประตูหน้าต่างแล้วเสร็จ/งานฝ้าแล้วเสร็จ", "([term8str])".Replace("[term8str]", ThaiBaht(step8)));
                        pdfTableRows.Add("งวดที่ 9 15% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term9] บาท".Replace("[term9]", step9));
                        pdfTableRows.Add("งานสี/งานไฟ/งานสุขภัณฑ์/และงานอื่นๆ แล้วเสร็จ", "([term9str])".Replace("[term9str]", ThaiBaht(step9)));
                        pdfTableRows.Add("งวดที่ 10 5% ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน", "[term10] บาท".Replace("[term10]", step10));
                        pdfTableRows.Add("งานเก็บรายละเอียดส่งงาน", "([term10str])".Replace("[term10str]", ThaiBaht(step10)));

                        PdfPTable table = new PdfPTable(2);
                        foreach (var item in pdfTableRows)
                        {
                            PdfPCell cell1 = new PdfPCell(new Phrase(item.Key, new Font(bf_normal, 18)));
                            PdfPCell cell2 = new PdfPCell(new Phrase(item.Value, new Font(bf_normal, 18)));
                            //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell1.Border = 0;
                            cell2.Border = 0;
                            table.AddCell(cell1);
                            table.AddCell(cell2);
                        }

                        table.WidthPercentage = 100;

                        //table.TotalWidth = pdfDoc.PageSize.Width;
                        pdfDoc.Add(table);

                        para = new Paragraph(@"""ผู้รับจ้าง"" ตกลงเริ่มทำการก่อสร้างในวันที่ [start] และจะดำเนินการให้แล้วเสร็จสมบูรณ์ภายในวันที่ [end] (เป็นระยะเวลา 6 เดือน)", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"**บริษัทฯ ขอสงวนสิทธิ์ในการเปลี่ยนแปลงเงื่อนไขการชำระเงินได้ตามความเหมาะสม", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);


                        table = new PdfPTable(2);
                        PdfPCell cell1_1 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ...................................... ผู้ว่าจ้าง" + Environment.NewLine
                            + @" ([customer])", new Font(bf_normal, 18)));
                        PdfPCell cell1_2 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ...................................... ผู้รับจ้าง" + Environment.NewLine
                            + @"([vendorname])" + Environment.NewLine
                            + @"[companyname]", new Font(bf_normal, 18)));
                        cell1_1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell1_2.HorizontalAlignment = Element.ALIGN_CENTER;

                        cell1_1.Border = 0;
                        cell1_2.Border = 0;

                        table.AddCell(cell1_1);
                        table.AddCell(cell1_2);

                        table.WidthPercentage = 100;

                        //table.TotalWidth = pdfDoc.PageSize.Width;
                        pdfDoc.Add(table);


                        pdfDoc.Close();
                    }
                    catch (Exception ex)
                    {
                        //handle exception
                    }
                    finally
                    {
                    }
                }
            }
            return fileName;
        }
    }
}