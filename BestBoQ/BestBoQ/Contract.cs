using System;
using System.Data;
using System.IO;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BestBoQ
{
    public class Contract
    {
        public System.Web.UI.Page page;

        public Contract(System.Web.UI.Page page)
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
            DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projectID + "'");
            string homename = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
            string projectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
            string customerName = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
            string cusProvince = dt.Rows[0]["cusprovince"] == null ? "" : dt.Rows[0]["cusprovince"].ToString();
            string cusAddress = dt.Rows[0]["cusaddress"] == null ? "" : dt.Rows[0]["cusaddress"].ToString();
            string projectStart = dt.Rows[0]["projectstart"] == null ? "" : Convert.ToDateTime(dt.Rows[0]["projectstart"]).ToString("dd/MM/YYYY");
            string contractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();
            string area = dt.Rows[0]["numMM"] == null ? "" : dt.Rows[0]["numMM"].ToString();
            string month = dt.Rows[0]["month"] == null ? "" : dt.Rows[0]["month"].ToString();
            string totalprice = dt.Rows[0]["totalprice"] == null ? "" : dt.Rows[0]["totalprice"].ToString();

            BaseFont bf_normal = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("fonts/THSarabunNew.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            string fileName = string.Empty;
            DateTime fileCreationDatetime = DateTime.Now;
            fileName = string.Format("contract_{0}.pdf", fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss"));
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

                        Paragraph para = new Paragraph("สัญญาจ้างเหมาก่อสร้าง" + Environment.NewLine
                            + "[homename]".Replace("[homename]", homename) + Environment.NewLine
                            + "........................................................................................" + Environment.NewLine
                            + "---   [customer]   ---".Replace("[customer]", customerName) + Environment.NewLine
                            + "(ผู้ว่าจ้าง)" + Environment.NewLine + Environment.NewLine
                            + "กับ" + Environment.NewLine + Environment.NewLine
                            + "[companyname]" + Environment.NewLine
                            + "(ผู้รับจ้าง)" + Environment.NewLine + Environment.NewLine + Environment.NewLine
                            + "สัญญาเลขที่ [contractid]" + Environment.NewLine
                            + "__________________________________" + Environment.NewLine + Environment.NewLine
                            + "สัญญาจ้างเหมาก่อสร้าง" + Environment.NewLine
                            + "[projectname]".Replace("[projectname]", projectName)
                            , new Font(bf_normal, 22));
                        para.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(para);
                        pdfDoc.NewPage();

                        para = new Paragraph(@"วันที่ [projectstart]".Replace("[projectstart]", projectStart), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_RIGHT;
                        //para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"สัญญาฉบับนี้ ทำขึ้นที่ [companyname] ตั้งอยู่[company address] ระหว่าง [companyname]โดย [name] ถือบัตรประจำตัวประชาชนเลขที่ [id]  ผู้มีอำนาจลงนาม สำนักงานตั้งอยู่[company address] ซึ่งในสัญญานี้จะเรียกว่า “ผู้รับจ้าง” ฝ่ายหนึ่ง กับ [customer] [cusAddress] ซึ่งต่อไปนี้ในสัญญาเรียกว่า “ผู้ว่าจ้าง” อีกฝ่ายหนึ่ง คู่สัญญาทั้งสองฝ่ายได้ตกลงทำสัญญากันมีข้อกำหนดและเงื่อนไขดังต่อไปนี้", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 1.ผู้ว่าจ้างตกลงว่าจ้างและผู้รับจ้างตกลงรับจ้างตกแต่ง...[homename]...พื้นที่ใช้สอยรวม...[totalMM]...ตารางเมตร จำนวน.....1 หลัง........บนโฉนดที่ดินเลขที่ - เลขที่ดิน...- หน้าสำรวจ - ตั้งอยู่ที่...[province]...(มีสำเนาโฉนดที่ดินเป็นเอกสารแนบ)".Replace("[homename]", homename).Replace("[totalMM]", area).Replace("[province]", cusProvince), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 2. ผู้ว่าจ้าง ขอรับรองว่าเป็นเจ้าของโฉนดที่ดินแปลงดังกล่าวจริง และที่ดินแปลงดังกล่าวไม่มีหนี้สินหรือ ภาระติดพันใดๆ", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 3.ผู้รับจ้าง ตกลงเป็นผู้จัดหาแรงงานรวมทั้งวัสดุและอุปกรณ์ที่ใช้ในการก่อสร้าง ไม่ว่าสำหรับงานก่อสร้างตามสัญญาหรืองานที่ขอเพิ่มเติม โดยจะดำเนินการตามมาตรฐานให้สอดคล้องกับแผนงานการก่อสร้าง", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ    4.ผู้ว่าจ้าง  ตกลงชำระค่าจ้างให้แก่ผู้รับจ้าง โดยตกลงค่าจ้างกันในราคารวมเป็น [totalprice] บาท ([priceth]) โดยปรากฏรายระเอียดการก่อสร้างตามเอกสารภาคผนวก ก.  เอกสารต่างๆ ดังต่อไปนี้ให้ถือเป็นส่วนหนึ่งของสัญญาฉบับนี้ด้วย ดังนี้ 
		                    1.1 ภาคผนวก ก.			: การชำระเงินค่าก่อสร้าง
		                    1.2 ภาคผนวก ข.			: แปลนบ้าน
		                    1.3 ภาคผนวก ค.			: รายการวัสดุมาตรฐาน
		                    1.4 ภาคผนวก ง.			: เอกสารแนบท้ายสัญญา 
                            - สำเนาบัตรประจำตัวประชาชน/สำเนาทะเบียนบ้านของผู้ว่าจ้าง
                            - สำเนาบัตรประจำตัวประชาชน/สำเนาทะเบียนบ้านของผู้รับจ้าง
                            - สำเนาโฉนดที่ดินแปลงที่จะปลูกสร้างบ้านของผู้ว่าจ้าง (ถ้ามี)".Replace("[totalprice]", totalprice).Replace("[priceth]", ThaiBaht(totalprice)), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 5. ผู้ว่าจ้างตกลงชำระค่าจ้างให้แก่ผู้รับจ้าง โดยแบ่งชำระเป็นงวดๆ ตามเอกสารภาคผนวก ก. โดยผู้ว่าจ้างหรือตัวแทนผู้ว่าจ้างจะตรวจรับมอบงานและชำระให้แก่ผู้รับจ้างภายในเจ็ด (7) วัน นับแต่วันที่ผู้รับจ้างได้ดำเนินการตามงวดงานนั้นแล้วเสร็จ", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ในกรณีที่ ผู้ว่าจ้างไม่ชำระค่างวดตามเอกสารภาคผนวก ก. ภายในระยะเวลาที่กำหนด หรือภายในเจ็ด (7) วัน  โดยไม่มีการแจ้งสาเหตุกับทางผู้รับจ้าง  ผู้รับจ้างสามารถที่จะหยุดการทำงานต่อได้ โดยผู้รับจ้างสามารถดำเนินการทางกฎหมายกับทางผู้ว่าจ้างในการบังคับใช้สิทธิดังกล่าวได้", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 6. หากผู้ว่าจ้างตกลงที่จะเป็นผู้จัดหาวัสดุและอุปกรณ์ที่ใช้ในการก่อสร้าง ไม่ว่าสำหรับงานก่อสร้างตามสัญญาหรืองานที่ขอเพิ่มเติม ผู้ว่าจ้างจะต้องดำเนินการจัดหาและส่งมอบวัสดุอุปกรณ์ดังกล่าวที่ใช้ในการก่อสร้างตามระยะเวลาในการก่อสร้างให้แก่ผู้รับจ้างภายในเวลาที่ผู้รับจ้างกำหนด เพื่อผู้รับจ้างสามารถทำงานก่อสร้างให้แล้วเสร็จตามที่กำหนดไว้ในสัญญา มิฉะนั้น ผู้รับจ้างขอสงวนสิทธิ์ที่จะใช้วัสดุและอุปกรณ์ที่ใช้ในการก่อสร้างตามที่ได้ระบุไว้ในสัญญานี้", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 7. ผู้รับจ้าง จะต้องเริ่มทำการก่อสร้างบ้านภายในสามสิบ (30) วัน นับจากวันที่เซ็นสัญญา หรือตามเงื่อนไข ข้อตกลงกันระหว่าง ผู้ว่าจ้างกับ ผู้รับจ้างอีกที  ทั้งนี้จะต้องไม่เกินกำหนดระยะเวลา [month] เดือน นับจากวันเซ็นต์สัญญา".Replace("[month]", month), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 8. ผู้รับจ้างตกลงส่งมอบเอกสารต่างๆ ให้แก่ผู้ว่าจ้าง หลังจากวันที่ชำระค่าก่อสร้างงวดสุดท้าย (เอกสารบางอย่างบริการให้เฉพาะในเขตตัวเมืองสกลนครเท่านั้น)  โดยมีรายการ ดังนี้ 
                            ใบอนุญาตก่อสร้างบ้าน (ถ้ามี)
                            เล่มทะเบียนบ้าน (ถ้ามี)
                            หนังสือรับประกันงาน
                            หนังสือรับประกันการติดตั้งเครื่องใช้ไฟฟ้าภายในบ้านทั้งหมด (ถ้ามี) จากร้านค้าเท่านั้น 
                            แบบบ้าน
                            เอกสารการเปลี่ยนแปลงต่างๆในแบบบ้าน (ถ้ามี)
                            แบบแปลนโครงสร้าง
                            แบบแปลนระบบไฟฟ้าและระบบสุขาภิบาล
                            กุญแจบ้านทั้งหมด", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 9. ผู้รับจ้างมีหน้าที่ทำการก่อสร้างบ้านพักอาศัย ตามแบบแปลนและรายการประกอบแบบแปลนที่ตกลงกันในภาคผนวก ข. หรือตามที่ได้รับอนุญาตจากเจ้าพนักงาน โดยต้องทำการก่อสร้างให้ถูกต้องตามเทคนิควิธีและมาตรฐานการก่อสร้าง และผู้รับจ้างต้องทำการให้แล้วเสร็จภายใน [month] เดือนนับจากวันเริ่มลงงาน
		                    -	8	 เดือน	สำหรับบ้านราคาไม่เกิน		3	ล้านบาท
		                    -	10	 เดือน	สำหรับบ้านราคาไม่เกิน		5	ล้านบาท
                            -	11	  เดือน	สำหรับบ้านราคาไม่เกิน		8	ล้านบาท
		                    -	12	  เดือน	สำหรับบ้านราคาไม่เกิน		10	ล้านบาท 
		                    -	14	 เดือน	สำหรับบ้านราคาไม่เกิน		15	ล้านบาท
 		                    -	18	 เดือน	สำหรับบ้านราคามากกว่า		15	ล้านบาท".Replace("[month]", month), new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 10. หากผู้รับจ้างไม่อาจดำเนินการก่อสร้างบ้านพักอาศัยให้แล้วเสร็จภายในระยะเวลาตามข้อ 9  โดยเหตุที่มิใช่ความผิดของผู้รับจ้าง ผู้รับจ้างมีสิทธิที่จะขอขยายระยะเวลาการก่อสร้างออกไปตามที่เห็นสมควร หรือ ถ้ามีการเปลี่ยนแปลงและเพิ่มเติมไปจากเดิม", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 11. หากผู้ว่าจ้างประสงค์ที่จะทำการแก้ไขเปลี่ยนแปลงบ้านพักอาศัยให้แตกต่างไปจากแบบแปลนบ้านที่ได้ตกลงทำสัญญากันไว้ ผู้ว่าจ้างต้องแจ้งให้ผู้รับจ้างทราบโดยเร็วเพื่อผู้รับจ้างจะได้ทำการแก้ไขเปลี่ยนแปลงให้เป็นไปตามความประสงค์ของผู้ว่าจ้างและขยายเวลาการก่อสร้างให้ผู้รับจ้างตามเห็นสมควรด้วย  หากมีการก่อสร้างบ้านไปตามสัญญาแล้วและไม่อาจที่จะแก้ไขเปลี่ยนแปลงตามความประสงค์ของผู้ว่าจ้างได้ ผู้รับจ้างขอสงวนสิทธิ์ที่จะดำเนินการก่อสร้างบ้านตามแบบแปลนในสัญญาเดิมจนแล้วเสร็จ โดยถือว่า ผู้ว่าจ้างไม่ประสงค์ที่จะเปลี่ยนแปลงแก้ไขแบบแปลนบ้านตามสัญญา", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 11.1 หากผู้ว่าจ้างประสงค์ที่จะให้ทำการก่อสร้างเพิ่มเติมจากแบบแปลนบ้านตามสัญญา ผู้ว่าจ้างต้องแจ้งให้ผู้รับจ้างทราบโดยเร็วและต้องมีการทำบันทึกข้อตกลงเพิ่มเติมงานก่อสร้างกันก่อนพร้อมผู้ว่าจ้างต้อง ชำระเงินในส่วนงานก่อสร้างที่เพิ่มเติมขึ้นจากสัญญาให้แก่ผู้รับจ้างก่อนลงมือทำการก่อสร้าง มิฉะนั้น ผู้รับจ้างขอสงวนสิทธิ์ที่จะดำเนินงานก่อสร้างบ้านตามแบบแปลนบ้านในสัญญาโดยถือว่า ผู้ว่าจ้างไม่ประสงค์ที่จะให้มีการก่อสร้างเพิ่มเติมบ้านให้ผิดไปจากแบบแปลนเดิม", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 11.2 หากผู้ว่าจ้างประสงค์ที่จะให้มีการลดรายการก่อสร้างบ้านตามสัญญา ผู้ว่าจ้างต้องแจ้งให้ผู้รับจ้างทราบโดยเร็วและต้องมีการทำบันทึกข้อตกลงลดรายการก่อสร้างกันก่อน สำหรับมูลค่างานลดต้องไม่เกิน 20 % ของราคาค่าก่อสร้างตามสัญญา และรายการก่อสร้างนั้นให้นำไปหักลดในงวดงานสุดท้ายตามสัญญา", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 12. ผู้ว่าจ้างมีสิทธิในการเข้าไปในพื้นที่ก่อสร้างบ้านในเวลาใดๆก็ได้ โดยผู้ว่าจ้างจะไม่ทำการรบกวนการทำงานของผู้รับจ้าง", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 13. ในกรณีที่ผู้รับจ้างทำงานไม่เสร็จภายในวันเวลาที่กำหนด ผู้ว่าจ้างสามารถบอกเลิกจ้างได้     (เว้นแต่ในเนื้องานไม่มีการเพิ่มเติมจากแบบแปลนเดิม)", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ  14. ผู้รับจ้างจะต้องรับผิดชอบต่ออุปัทวะเหตุ    หรือภยันตราย ความเสียหายใดๆ   ที่เกิดขึ้นจากการงานของผู้รับจ้างเอง และต้องรับผิดชอบในเหตุที่เสียหายอันเกิดแก่ทรัพย์สิน  ของผู้ว่าจ้างซึ่งมีอยู่ในบริเวณที่ทำการจ้างนี้โดยการกระทำ    ของคนงานช่างหรือบริวารของผู้รับจ้างด้วย", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 15. หากผู้รับจ้างผิดสัญญาข้อใดข้อหนึ่ง ผู้รับจ้างจำเป็นต้องแจ้งกับผู้ว่าจ้างทราบด้วยวาจา หรือ เป็นลายลักษณ์อักษร โดยกำหนดระยะเวลาดำเนินการในการแก้ปฏิบัติให้ถูกต้องตามสมควรเสียก่อน", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 16. หากผู้ว่าจ้างผิดสัญญาข้อใดข้อหนึ่ง หรือ ผู้ว่าจ้างไม่ชำระค่าก่อสร้างงวดใดงวดหนึ่ง ผู้ว่าจ้างจำเป็นต้องแจ้งกับผู้รับจ้างทราบด้วยวาจา หรือ เป็นลายลักษณ์อักษร โดยกำหนดระยะเวลาดำเนินการในการแก้ปฏิบัติให้ถูกต้องตามสมควรเสียก่อน", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"ข้อ 17. การรับประกันงาน
                            17.1 รับประกันคุณภาพงานทั่วไป เป็นระยะเวลา 2 ปี นับจากวันที่ส่งมอบบ้าน  
                            17.2 รับประกันคุณภาพงานโครงสร้างหลังคา เป็นระยะเวลา 5 ปี นับจากวันที่ส่งมอบบ้าน    
                            17.3 รับประกันคุณภาพงานโครงสร้างบ้าน เป็นระยะเวลา 15 ปี นับจากวันที่ส่งมอบบ้าน    
                            17.4 ผู้รับจ้าง จะไม่รับประกันคุณภาพงาน สาเหตุอันเนื่องมาจาก ภาวะสงคราม อุบัติภัย อุทกภัย หรือภัยธรรมชาติต่างๆ เช่น อัคคีภัย, วาตภัย, แผ่นดินไหว เป็นต้น", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        para = new Paragraph(@"สัญญานี้ทำขึ้นเป็นสองฉบับมีข้อความถูกต้องตรงกัน คู่สัญญาทั้งสองฝ่ายได้อ่านและเข้าใจข้อความในสัญญาโดยตลอดแล้วเห็นว่าถูกต้อง จึงได้ลงลายมือชื่อพร้อมประทับตรา (ถ้ามี) ไว้เป็นสำคัญต่อหน้าพยาน และต่างฝ่ายต่างยึดถือไว้ฝ่ายละหนึ่งฉบับ", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        PdfPTable table = new PdfPTable(2);
                        PdfPCell cell1_1 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ...................................... ผู้ว่าจ้าง" + Environment.NewLine
                            + @" ([customer])".Replace("[customer]", customerName), new Font(bf_normal, 18)));
                        PdfPCell cell1_2 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ...................................... ผู้รับจ้าง" + Environment.NewLine
                            + @"([name])".Replace("[name]", "[name]") + Environment.NewLine
                            + @"[companyname]".Replace("[companyname]", "[companyname]"), new Font(bf_normal, 18)));
                        PdfPCell cell2_1 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ......................................พยาน" + Environment.NewLine
                            + @"(                                         )", new Font(bf_normal, 18)));
                        PdfPCell cell2_2 = new PdfPCell(new Phrase(Environment.NewLine + Environment.NewLine + @"ลงชื่อ ......................................พยาน" + Environment.NewLine
                            + @"(                                         )", new Font(bf_normal, 18)));

                        cell1_1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell1_2.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2_1.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell2_2.HorizontalAlignment = Element.ALIGN_CENTER;

                        cell1_1.Border = 0;
                        cell1_2.Border = 0;
                        cell2_1.Border = 0;
                        cell2_2.Border = 0;

                        table.AddCell(cell1_1);
                        table.AddCell(cell1_2);
                        table.AddCell(cell2_1);
                        table.AddCell(cell2_2);

                        table.WidthPercentage = 100;

                        //table.TotalWidth = pdfDoc.PageSize.Width;
                        pdfDoc.Add(table);

                        para = new Paragraph(@"***ในกรณีที่ผู้ว่าจ้างดำเนินงานล่าช้าและเสร็จไม่ตรงตามกำหนดทางผู้รับจ้างยินยอมจ่ายชดเชยให้ผู้ว่าจ้างรายวันเป็นวันละ 0.05 %ของมูลค่างานที่เหลือ แต่ไม่น้อยกว่าวันละ 1,000 บาท", new Font(bf_normal, 18));
                        para.Alignment = Element.ALIGN_JUSTIFIED;
                        //para.FirstLineIndent = 50f;
                        pdfDoc.Add(para);

                        //for (int i = 0; i < 10; i++)
                        //{
                        //    para = new Paragraph("Hello world. Checking Header Footer", new Font(Font.FontFamily.HELVETICA, 22));
                        //    para.Alignment = Element.ALIGN_CENTER;
                        //    pdfDoc.Add(para);
                        //    pdfDoc.NewPage();
                        //}

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
                return fileName;
            }

        }
    }
}