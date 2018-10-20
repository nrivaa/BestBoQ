using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Services;
using Word = Microsoft.Office.Interop.Word;

namespace BestBoQ
{
    /// <summary>
    /// Summary description for generate_doc
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class generate_doc : System.Web.Services.WebService
    {

        object oMissing = System.Reflection.Missing.Value;

        private void RunMacro(object oApp, object[] oRunArgs)
        {
            oApp.GetType().InvokeMember("Run",
                System.Reflection.BindingFlags.Default |
                System.Reflection.BindingFlags.InvokeMethod,
                null, oApp, oRunArgs);
        }

        private void SearchReplace(Word.Application app, string search, string replace)
        {
            Word.Find findObject = app.Selection.Find;
            findObject.ClearFormatting();
            findObject.Text = search;
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Text = replace;

            object replaceAll = Word.WdReplace.wdReplaceAll;
            findObject.Execute(ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref replaceAll, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        }

        private bool Search(Word.Application app, string search)
        {
            Word.Find findObject = app.Selection.Find;
            findObject.ClearFormatting();

            object findText = search;

            return findObject.Execute(ref findText, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);
        }

        private void GetColumnAndSearchReplace(DataTable dt, string columnname, Word.Application app, string search)
        {
            string textReplace = dt.Rows[0][columnname] == null ? "" : dt.Rows[0][columnname].ToString();
            SearchReplace(app, search, textReplace);
        }

        private string GetColumn(DataRow dr, string columnname)
        {
            return dr[columnname] == null ? "" : dr[columnname].ToString();
        }

        private void ReplaceFooter(Word.Application app, Word._Document oDoc, string search, string replace)
        {
            // Loop through all sections
            foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Sections)
            {
                //Get all Footers
                Microsoft.Office.Interop.Word.HeadersFooters footers = section.Footers;
                //Section headerfooter loop for all types enum WdHeaderFooterIndex. wdHeaderFooterEvenPages/wdHeaderFooterFirstPage/wdHeaderFooterPrimary; 
                foreach (Microsoft.Office.Interop.Word.HeaderFooter footer in footers)
                {
                    Word.Fields fields = footer.Range.Fields;
                    foreach (Word.Field field in fields)
                    {
                        SearchReplace(app, search, replace);
                        //if (field.Type == WdFieldType.wdFieldDate)
                        //{
                        //    field.Select();
                        //    field.Delete();
                        //    app.Selection.TypeText("[DATE]");
                        //}
                        //else if (field.Type == WdFieldType.wdFieldFileName)
                        //{
                        //    field.Select();
                        //    field.Delete();
                        //    app.Selection.TypeText("[FILE NAME]");

                        //}
                    }
                }
            }
        }

        private void ReplaceBOQ(Word.Application app, DataRow dr, string tagType, string value)
        {
            string tagMaterial = GetColumn(dr, "Material").Trim();
            string tagTypeID = GetColumn(dr, "TypeID").Trim();
            string tag = "<" + tagMaterial + "_" + tagTypeID + "_" + tagType.Trim() + ">";
            string tag_mini = "<" + tagMaterial + "_" + tagType + ">";

            //if (Search(app, tag))
            //{

            SearchReplace(app, tag, GetColumn(dr, value));
            //}
            //}
            //else
            //{
            if (!tagTypeID.Contains("Slap"))
            {
                SearchReplace(app, tag_mini, GetColumn(dr, value));
            }
            //}
        }

        private void ReplaceBOQDetail(Word.Application app, DataRow dr, string value)
        {
            string tagMaterial = GetColumn(dr, "Material").Trim();
            string tagTypeID = GetColumn(dr, "TypeID").Trim();
            string tagCostID = GetColumn(dr, "costID").Trim();

            string tag = "<" + tagMaterial + "_" + tagTypeID + "_" + tagCostID + ">";
            string tag_mini = "<" + tagMaterial + "_" + tagCostID + ">";

            //if (Search(app, tag))
            //{
            SearchReplace(app, tag, GetColumn(dr, value));
            //}
            //else
            //{
            SearchReplace(app, tag_mini, GetColumn(dr, value));
            //}
        }

        [WebMethod]
        public string GenerateDocumentPack(string projid)
        {
            string dest = Server.MapPath(".") + @"\GeneratedDocument\";

            string docBoq = dest + GenerateBOQ(projid);
            string docContract = dest + GenerateContract(projid);
            string docPayment = dest + GeneratePayment(projid);
            string docTimeplan = dest + GenerateTimeplan(projid);
            string docSummary = dest + GenerateSummary(projid);

            // Get some file names
            List<string> files = new List<string>();
            files.Add(docBoq);
            files.Add(docContract);
            files.Add(docPayment);
            files.Add(docTimeplan);
            files.Add(docSummary);

            // Open the output document
            PdfDocument outputDocument = new PdfDocument();

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = inputDocument.PageCount;
                for (int idx = 0; idx < count; idx++)
                {
                    // Get the page from the external document...
                    PdfPage page = inputDocument.Pages[idx];
                    // ...and add it to the output document.
                    outputDocument.AddPage(page);
                }
            }

            // Save the document...
            string filename = projid + "_BestBOQ_DocPack.pdf";
            outputDocument.Save(dest + filename);
            // ...and start a viewer.
            // Process.Start(filename);

            return filename;
        }

        [WebMethod]
        public string GenerateContract(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone, space, place;

                // No data yet
                string CompanyName, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;


                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)

                string source = Server.MapPath(".") + @"\templates\BestBOQ_contract.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_contract.docm";
                File.Copy(source, dest, true);

                // Open document
                // Create an instance of Word, make it visible,
                // and open Doc1.doc.
                // Object for missing (or optional) arguments.

                Word.Application oWord = new Word.Application();
                oWord.Visible = true;
                Word.Documents oDocs = oWord.Documents;
                object oFile = dest;

                Word._Document oDoc = oDocs.Open(ref oFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projid + "'");
                HomeName = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
                ProjectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
                CustomerName = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
                CustomerProvince = dt.Rows[0]["cusprovince"] == null ? "" : dt.Rows[0]["cusprovince"].ToString();
                CustomerAddress = dt.Rows[0]["cusaddress"] == null ? "" : dt.Rows[0]["cusaddress"].ToString();
                ProjectStart = dt.Rows[0]["projectstart"] == null ? "" : Convert.ToDateTime(dt.Rows[0]["projectstart"]).ToString("dd/MM/yyyy");
                ContractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();
                Area = dt.Rows[0]["numMM"] == null ? "" : dt.Rows[0]["numMM"].ToString();
                Month = dt.Rows[0]["month"] == null ? "" : dt.Rows[0]["month"].ToString();
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : Convert.ToDecimal(dt.Rows[0]["totalprice"].ToString()).ToString("#,##0.00");
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();
                space = dt.Rows[0]["space"] == null ? "" : dt.Rows[0]["space"].ToString();
                place = dt.Rows[0]["place"] == null ? "" : dt.Rows[0]["place"].ToString();

                // Replace data in template document
                SearchReplace(oWord, "[project_name]", ProjectName);
                SearchReplace(oWord, "[contract_id]", ContractID);
                SearchReplace(oWord, "[customer_name]", CustomerName);
                SearchReplace(oWord, "[project_type]", HomeName);
                SearchReplace(oWord, "[area]", Area);
                SearchReplace(oWord, "[room_amount]", RoomAmount);
                SearchReplace(oWord, "[project_start]", ProjectStart);
                SearchReplace(oWord, "[customer_address]", CustomerAddress);
                SearchReplace(oWord, "[company_name]", CompanyName);
                SearchReplace(oWord, "[company_address]", CompanyAddress);
                SearchReplace(oWord, "[customer_national_id]", CustomerNationalID);
                SearchReplace(oWord, "[home_name]", HomeName);
                SearchReplace(oWord, "[total_price]", TotalPrice);
                SearchReplace(oWord, "[total_price_txt]", TotalPriceTxt);
                SearchReplace(oWord, "[company_sign]", CompanySign);
                SearchReplace(oWord, "[customer_province]", CustomerProvince);
                SearchReplace(oWord, "[month]", Month);
                SearchReplace(oWord, "[telephone] ", Telephone);
                SearchReplace(oWord, "[spec]", space);
                SearchReplace(oWord, "[place]", place);

                // Replace footer
                ReplaceFooter(oWord, oDoc, "[company_name]", CompanyName);
                ReplaceFooter(oWord, oDoc, "[company_address]", CompanyAddress);
                ReplaceFooter(oWord, oDoc, "[telephone] ", Telephone);

                // Save to PDF
                // Run the macros.
                RunMacro(oWord, new Object[] { "Silent_saves_to_PDF" });

                // Quit Word and clean up.
                oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oDoc = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDocs);
                oDocs = null;
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                oWord = null;

                GC.Collect();

                return Path.GetFileName(dest.Replace(".docm", ".pdf"));
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }

        [WebMethod]
        public string GeneratePayment(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone;

                // No data yet
                string CompanyName, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;
                string step01, step02, step03, step04, step05, step06, step07, step08, step09, step10, startDate, stopDate;
                string step01Txt, step02Txt, step03Txt, step04Txt, step05Txt, step06Txt, step07Txt, step08Txt, step09Txt, step10Txt;

                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)

                string source = Server.MapPath(".") + @"\templates\BestBOQ_PaymentTerm.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_payment.docm";
                File.Copy(source, dest, true);

                // Open document
                // Create an instance of Word, make it visible,
                // and open Doc1.doc.
                // Object for missing (or optional) arguments.

                Word.Application oWord = new Word.Application();
                oWord.Visible = true;
                Word.Documents oDocs = oWord.Documents;
                object oFile = dest;

                Word._Document oDoc = oDocs.Open(ref oFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projid + "'");
                HomeName = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
                ProjectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
                CustomerName = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
                CustomerProvince = dt.Rows[0]["cusprovince"] == null ? "" : dt.Rows[0]["cusprovince"].ToString();
                CustomerAddress = dt.Rows[0]["cusaddress"] == null ? "" : dt.Rows[0]["cusaddress"].ToString();
                ProjectStart = dt.Rows[0]["projectstart"] == null ? "" : Convert.ToDateTime(dt.Rows[0]["projectstart"]).ToString("dd/MM/yyyy");
                ContractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();
                Area = dt.Rows[0]["numMM"] == null ? "" : dt.Rows[0]["numMM"].ToString();
                Month = dt.Rows[0]["month"] == null ? "" : dt.Rows[0]["month"].ToString();
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : Convert.ToDecimal(dt.Rows[0]["totalprice"].ToString()).ToString("#,##0");
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString(); ;

                DataTable dt_pay = ClassConfig.GetDataSQL("exec dbo.get_AppendixA '" + projid + "'");
                step01 = dt_pay.Rows[0]["step01"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step01"].ToString()).ToString("#,##0");
                step01Txt = ClassConfig.ThaiBaht(step01);
                step02 = dt_pay.Rows[0]["step02"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step02"].ToString()).ToString("#,##0");
                step02Txt = ClassConfig.ThaiBaht(step02);
                step03 = dt_pay.Rows[0]["step03"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step03"].ToString()).ToString("#,##0");
                step03Txt = ClassConfig.ThaiBaht(step03);
                step04 = dt_pay.Rows[0]["step04"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step04"].ToString()).ToString("#,##0");
                step04Txt = ClassConfig.ThaiBaht(step04);
                step05 = dt_pay.Rows[0]["step05"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step05"].ToString()).ToString("#,##0");
                step05Txt = ClassConfig.ThaiBaht(step05);
                step06 = dt_pay.Rows[0]["step06"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step06"].ToString()).ToString("#,##0");
                step06Txt = ClassConfig.ThaiBaht(step06);
                step07 = dt_pay.Rows[0]["step07"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step07"].ToString()).ToString("#,##0");
                step07Txt = ClassConfig.ThaiBaht(step07);
                step08 = dt_pay.Rows[0]["step08"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step08"].ToString()).ToString("#,##0");
                step08Txt = ClassConfig.ThaiBaht(step08);
                step09 = dt_pay.Rows[0]["step09"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step09"].ToString()).ToString("#,##0");
                step09Txt = ClassConfig.ThaiBaht(step09);
                step10 = dt_pay.Rows[0]["step10"] == null ? "" : Convert.ToDecimal(dt_pay.Rows[0]["step10"].ToString()).ToString("#,##0");
                step10Txt = ClassConfig.ThaiBaht(step10);
                startDate = dt_pay.Rows[0]["start"] == null ? "" : Convert.ToDateTime(dt_pay.Rows[0]["start"]).ToString("dd/MM/yyyy");
                stopDate = dt_pay.Rows[0]["stop"] == null ? "" : Convert.ToDateTime(dt_pay.Rows[0]["stop"]).ToString("dd/MM/yyyy");

                // Replace data in template document
                SearchReplace(oWord, "[project_name]", ProjectName);
                SearchReplace(oWord, "[contract_id]", ContractID);
                SearchReplace(oWord, "[customer_name]", CustomerName);
                SearchReplace(oWord, "[project_type]", HomeName);
                SearchReplace(oWord, "[area]", Area);
                SearchReplace(oWord, "[room_amount]", RoomAmount);
                SearchReplace(oWord, "[project_start]", ProjectStart);
                SearchReplace(oWord, "[customer_address]", CustomerAddress);
                SearchReplace(oWord, "[company_name]", CompanyName);
                SearchReplace(oWord, "[company_address]", CompanyAddress);
                SearchReplace(oWord, "[customer_national_id]", CustomerNationalID);
                SearchReplace(oWord, "[home_name]", HomeName);
                SearchReplace(oWord, "[total_price]", TotalPrice);
                SearchReplace(oWord, "[total_price_txt]", TotalPriceTxt);
                SearchReplace(oWord, "[company_sign]", CompanySign);
                SearchReplace(oWord, "[customer_province]", CustomerProvince);
                SearchReplace(oWord, "[month]", Month);
                SearchReplace(oWord, "[telephone] ", Telephone);

                SearchReplace(oWord, "[startDate]", startDate);
                SearchReplace(oWord, "[stopDate]", stopDate);
                SearchReplace(oWord, "[Term1Amount]", step01);
                SearchReplace(oWord, "[Term2Amount]", step02);
                SearchReplace(oWord, "[Term3Amount]", step03);
                SearchReplace(oWord, "[Term4Amount]", step04);
                SearchReplace(oWord, "[Term5Amount]", step05);
                SearchReplace(oWord, "[Term6Amount]", step06);
                SearchReplace(oWord, "[Term7Amount]", step07);
                SearchReplace(oWord, "[Term8Amount]", step08);
                SearchReplace(oWord, "[Term9Amount]", step09);
                SearchReplace(oWord, "[Term10Amount]", step10);
                SearchReplace(oWord, "[Term1Text]", step01Txt);
                SearchReplace(oWord, "[Term2Text]", step02Txt);
                SearchReplace(oWord, "[Term3Text]", step03Txt);
                SearchReplace(oWord, "[Term4Text]", step04Txt);
                SearchReplace(oWord, "[Term5Text]", step05Txt);
                SearchReplace(oWord, "[Term6Text]", step06Txt);
                SearchReplace(oWord, "[Term7Text]", step07Txt);
                SearchReplace(oWord, "[Term8Text]", step08Txt);
                SearchReplace(oWord, "[Term9Text]", step09Txt);
                SearchReplace(oWord, "[Term10Text]", step10Txt);

                // Save to PDF
                // Run the macros.
                RunMacro(oWord, new Object[] { "Silent_saves_to_PDF" });

                // Quit Word and clean up.
                oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oDoc = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDocs);
                oDocs = null;
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                oWord = null;

                GC.Collect();

                return Path.GetFileName(dest.Replace(".docm", ".pdf"));
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }

        [WebMethod]
        public string GenerateBOQ(string projid)
        {
            //try
            {
                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)

                string source = Server.MapPath(".") + @"\templates\BestBOQ_BOQ.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_boq.docm";
                File.Copy(source, dest, true);

                // Open document
                // Create an instance of Word, make it visible,
                // and open Doc1.doc.
                // Object for missing (or optional) arguments.

                Word.Application oWord = new Word.Application();
                oWord.Visible = true;
                Word.Documents oDocs = oWord.Documents;
                object oFile = dest;

                Word._Document oDoc = oDocs.Open(ref oFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

                // Project Information
                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projid + "'");

                GetColumnAndSearchReplace(dt, "projectname", oWord, "[project_name]");
                GetColumnAndSearchReplace(dt, "projectname", oWord, "<project_name>");

                GetColumnAndSearchReplace(dt, "contractid", oWord, "[contract_id]");
                GetColumnAndSearchReplace(dt, "contractid", oWord, "<contract_id>");

                GetColumnAndSearchReplace(dt, "customername", oWord, "[customer_name]");
                GetColumnAndSearchReplace(dt, "customername", oWord, "<customer_name>");

                GetColumnAndSearchReplace(dt, "homename", oWord, "[project_type]");
                GetColumnAndSearchReplace(dt, "homename", oWord, "<project_type>");

                GetColumnAndSearchReplace(dt, "numMM", oWord, "[area]");
                GetColumnAndSearchReplace(dt, "numMM", oWord, "<area>");

                GetColumnAndSearchReplace(dt, "roomamount", oWord, "[room_amount]");
                GetColumnAndSearchReplace(dt, "roomamount", oWord, "<room_amount>");

                GetColumnAndSearchReplace(dt, "projectstart", oWord, "[project_start]");
                GetColumnAndSearchReplace(dt, "projectstart", oWord, "<project_start>");

                GetColumnAndSearchReplace(dt, "cusaddress", oWord, "[customer_address]");
                GetColumnAndSearchReplace(dt, "cusaddress", oWord, "<customer_address>");

                // Get Project Summary
                DataTable dt_summary = ClassConfig.GetDataSQL("exec dbo.[get_Last_Price] '" + projid + "'");
                GetColumnAndSearchReplace(dt_summary, "TotalMoney", oWord, "<project_p>");
                GetColumnAndSearchReplace(dt_summary, "FreeMoney", oWord, "<project_p_com>");
                GetColumnAndSearchReplace(dt_summary, "PromoMoney", oWord, "<project_p_discount>");
                GetColumnAndSearchReplace(dt_summary, "OtherMoney", oWord, "<project_p_other>");
                GetColumnAndSearchReplace(dt_summary, "LastMoney", oWord, "<project_p_total>");

                // Get Boq
                dt = ClassConfig.GetDataSQL("exec dbo.[get_BOQ] '" + projid + "'");

                foreach (DataRow dr in dt.Rows)
                {
                    ReplaceBOQ(oWord, dr, "type", "footingType");
                    ReplaceBOQ(oWord, dr, "amount", "numpole");
                    ReplaceBOQ(oWord, dr, "priceunit", "cost_pole");
                    ReplaceBOQ(oWord, dr, "total", "Total");
                }

                // Get Boq Detail
                for (int i = 1; i <= 14; i++)
                {
                    if (i != 11)
                    {
                        string number = i.ToString("D2");
                        dt = ClassConfig.GetDataSQL("exec [dbo].[get_BOQ_Detail_" + number + "]  '" + projid + "'");

                        foreach (DataRow dr in dt.Rows)
                        {
                            ReplaceBOQDetail(oWord, dr, "detailPrice");
                        }
                    }
                }

                //Replacr Picture
                DataTable dt_pic = ClassConfig.GetDataSQL("exec dbo.[get_BOQ_Picture] '" + projid + "'");
                List<Word.Range> ranges = new List<Microsoft.Office.Interop.Word.Range>();
                foreach (DataRow dr in dt_pic.Rows)
                {
                    foreach (Word.InlineShape s in oDoc.InlineShapes)
                    {
                        if (s.Title == dr["Title"].ToString() && s.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePicture)
                        {
                            Word.Range toreplace = s.Range;
                            s.Delete();
                            string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                            toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);
                        }
                    }
                }


                // Replace All
                Word.Find findObject = oWord.Selection.Find;
                findObject.ClearFormatting();
                findObject.MatchWildcards = true;
                findObject.Text = "[<]*[>]"; // SSN with dashes.
                findObject.Replacement.ClearFormatting();
                findObject.Replacement.Text = "-";

                object replaceAll = Word.WdReplace.wdReplaceAll;
                findObject.Execute(ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref replaceAll, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                // Save to PDF
                // Run the macros.
                RunMacro(oWord, new Object[] { "Silent_saves_to_PDF" });

                // Quit Word and clean up.
                oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oDoc = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDocs);
                oDocs = null;
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                oWord = null;

                GC.Collect();

                return Path.GetFileName(dest.Replace(".docm", ".pdf"));
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }

        [WebMethod]
        public string GenerateSummary(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice;

                // No data yet
                string CompanyName, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;
                string benefit, power, material, benefit_pct, power_pct, material_pct;

                // Create document (Copy from template)
                string source;
                string dest;
                DataTable dt_report = ClassConfig.GetDataSQL("exec dbo.get_Report '" + projid + "'");
                if (dt_report.Rows.Count > 0)
                {
                    string template = dt_report.Rows[0]["Template"] == null ? "" : dt_report.Rows[0]["Template"].ToString();

                    source = Server.MapPath(".") + @"\templates\BestBOQ_summary" + template + ".docm";
                    dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.docm";
                    File.Copy(source, dest, true);
                }
                else
                {
                    source = Server.MapPath(".") + @"\templates\BestBOQ_summary1.docm";
                    dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.docm";
                    File.Copy(source, dest, true);
                }
                benefit = dt_report.Rows[0]["bBenefit"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["bBenefit"].ToString()).ToString("#,##0.00");
                power = dt_report.Rows[0]["bPower"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["bPower"].ToString()).ToString("#,##0.00");
                material = dt_report.Rows[0]["bMaterial"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["bMaterial"].ToString()).ToString("#,##0.00");
                benefit_pct = dt_report.Rows[0]["pBenefit"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["pBenefit"].ToString()).ToString("#,##0.00");
                power_pct = dt_report.Rows[0]["pPower"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["pPower"].ToString()).ToString("#,##0.00");
                material_pct = dt_report.Rows[0]["pMaterial"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["pMaterial"].ToString()).ToString("#,##0.00");

                // Open document
                // Create an instance of Word, make it visible,
                // and open Doc1.doc.
                // Object for missing (or optional) arguments.

                Word.Application oWord = new Word.Application();
                oWord.Visible = true;
                Word.Documents oDocs = oWord.Documents;
                object oFile = dest;

                Word._Document oDoc = oDocs.Open(ref oFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projid + "'");
                HomeName = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
                ProjectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
                CustomerName = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
                CustomerProvince = dt.Rows[0]["cusprovince"] == null ? "" : dt.Rows[0]["cusprovince"].ToString();
                CustomerAddress = dt.Rows[0]["cusaddress"] == null ? "" : dt.Rows[0]["cusaddress"].ToString();
                ProjectStart = dt.Rows[0]["projectstart"] == null ? "" : Convert.ToDateTime(dt.Rows[0]["projectstart"]).ToString("dd/MM/yyyy");
                ContractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();
                Area = dt.Rows[0]["numMM"] == null ? "" : dt.Rows[0]["numMM"].ToString();
                Month = dt.Rows[0]["month"] == null ? "" : dt.Rows[0]["month"].ToString();
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : Convert.ToDecimal(dt.Rows[0]["totalprice"].ToString()).ToString("#,##0.00");
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString(); ;


                //// Replace data in template document
                SearchReplace(oWord, "[project_name]", ProjectName);
                SearchReplace(oWord, "[contract_id]", ContractID);
                SearchReplace(oWord, "[customer_name]", CustomerName);
                SearchReplace(oWord, "[project_type]", HomeName);
                SearchReplace(oWord, "[area]", Area);
                SearchReplace(oWord, "[room_amount]", RoomAmount);
                SearchReplace(oWord, "[project_start]", ProjectStart);
                SearchReplace(oWord, "[customer_address]", CustomerAddress);
                SearchReplace(oWord, "[company_name]", CompanyName);
                SearchReplace(oWord, "[company_address]", CompanyAddress);
                SearchReplace(oWord, "[customer_national_id]", CustomerNationalID);
                SearchReplace(oWord, "[home_name]", HomeName);
                SearchReplace(oWord, "[total_price]", TotalPrice);
                SearchReplace(oWord, "[total_price_txt]", TotalPriceTxt);
                SearchReplace(oWord, "[company_sign]", CompanySign);
                SearchReplace(oWord, "[customer_province]", CustomerProvince);
                SearchReplace(oWord, "[month]", Month);

                SearchReplace(oWord, "[benefit]", benefit);
                SearchReplace(oWord, "[power]", power);
                SearchReplace(oWord, "[material]", material);
                SearchReplace(oWord, "[benefit_pct]", benefit_pct);
                SearchReplace(oWord, "[power_pct]", power_pct);
                SearchReplace(oWord, "[material_pct]", material_pct);

                // Save to PDF
                // Run the macros.
                RunMacro(oWord, new Object[] { "Silent_saves_to_PDF" });

                // Quit Word and clean up.
                oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oDoc = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDocs);
                oDocs = null;
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                oWord = null;

                GC.Collect();

                return Path.GetFileName(dest.Replace(".docm", ".pdf"));
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }

        [WebMethod]
        public string GenerateTimeplan(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone;

                // No data yet
                string CompanyName, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount, TotalDuration;
                string s01, s02, s03, s04, s05, s06, s07, s08, s09, s10, s11, s12, s13, s14, s15, s16, s17, s18;
                string d01, d02, d03, d04, d05, d06, d07, d08, d09, d10, d11, d12, d13, d14, d15, d16, d17, d18;
                string p01, p02, p03, p04, p05, p06, p07, p08, p09, p10, p11, p12, p13, p14, p15, p16, p17, p18;

                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)

                string source = Server.MapPath(".") + @"\templates\BestBOQ_Timeplan.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_Timeplan.docm";
                File.Copy(source, dest, true);

                // Open document
                // Create an instance of Word, make it visible,
                // and open Doc1.doc.
                // Object for missing (or optional) arguments.

                Word.Application oWord = new Word.Application();
                oWord.Visible = true;
                Word.Documents oDocs = oWord.Documents;
                object oFile = dest;

                Word._Document oDoc = oDocs.Open(ref oFile, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projid + "'");
                HomeName = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
                ProjectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
                CustomerName = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
                CustomerProvince = dt.Rows[0]["cusprovince"] == null ? "" : dt.Rows[0]["cusprovince"].ToString();
                CustomerAddress = dt.Rows[0]["cusaddress"] == null ? "" : dt.Rows[0]["cusaddress"].ToString();
                ProjectStart = dt.Rows[0]["projectstart"] == null ? "" : Convert.ToDateTime(dt.Rows[0]["projectstart"]).ToString("dd/MM/yyyy");
                ContractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();
                Area = dt.Rows[0]["numMM"] == null ? "" : dt.Rows[0]["numMM"].ToString();
                Month = dt.Rows[0]["month"] == null ? "" : dt.Rows[0]["month"].ToString();
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : dt.Rows[0]["totalprice"].ToString();
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString(); ;

                DataTable dt_plan = ClassConfig.GetDataSQL("exec dbo.get_Plan '" + projid + "'");

                TotalDuration = dt_plan.Rows[0]["Total"] == null ? "" : dt_plan.Rows[0]["Total"].ToString();

                s01 = dt_plan.Rows[0]["s1"] == null ? "" : dt_plan.Rows[0]["s1"].ToString();
                s02 = dt_plan.Rows[0]["s2"] == null ? "" : dt_plan.Rows[0]["s2"].ToString();
                s03 = dt_plan.Rows[0]["s3"] == null ? "" : dt_plan.Rows[0]["s3"].ToString();
                s04 = dt_plan.Rows[0]["s4"] == null ? "" : dt_plan.Rows[0]["s4"].ToString();
                s05 = dt_plan.Rows[0]["s5"] == null ? "" : dt_plan.Rows[0]["s5"].ToString();
                s06 = dt_plan.Rows[0]["s6"] == null ? "" : dt_plan.Rows[0]["s6"].ToString();
                s07 = dt_plan.Rows[0]["s7"] == null ? "" : dt_plan.Rows[0]["s7"].ToString();
                s08 = dt_plan.Rows[0]["s8"] == null ? "" : dt_plan.Rows[0]["s8"].ToString();
                s09 = dt_plan.Rows[0]["s9"] == null ? "" : dt_plan.Rows[0]["s9"].ToString();
                s10 = dt_plan.Rows[0]["s10"] == null ? "" : dt_plan.Rows[0]["s10"].ToString();
                s11 = dt_plan.Rows[0]["s11"] == null ? "" : dt_plan.Rows[0]["s11"].ToString();
                s12 = dt_plan.Rows[0]["s12"] == null ? "" : dt_plan.Rows[0]["s12"].ToString();
                s13 = dt_plan.Rows[0]["s13"] == null ? "" : dt_plan.Rows[0]["s13"].ToString();
                s14 = dt_plan.Rows[0]["s14"] == null ? "" : dt_plan.Rows[0]["s14"].ToString();
                s15 = dt_plan.Rows[0]["s15"] == null ? "" : dt_plan.Rows[0]["s15"].ToString();
                s16 = dt_plan.Rows[0]["s16"] == null ? "" : dt_plan.Rows[0]["s16"].ToString();
                s17 = dt_plan.Rows[0]["s17"] == null ? "" : dt_plan.Rows[0]["s17"].ToString();
                s18 = dt_plan.Rows[0]["s18"] == null ? "" : dt_plan.Rows[0]["s18"].ToString();

                d01 = dt_plan.Rows[0]["d1"] == null ? "" : dt_plan.Rows[0]["d1"].ToString();
                d02 = dt_plan.Rows[0]["d2"] == null ? "" : dt_plan.Rows[0]["d2"].ToString();
                d03 = dt_plan.Rows[0]["d3"] == null ? "" : dt_plan.Rows[0]["d3"].ToString();
                d04 = dt_plan.Rows[0]["d4"] == null ? "" : dt_plan.Rows[0]["d4"].ToString();
                d05 = dt_plan.Rows[0]["d5"] == null ? "" : dt_plan.Rows[0]["d5"].ToString();
                d06 = dt_plan.Rows[0]["d6"] == null ? "" : dt_plan.Rows[0]["d6"].ToString();
                d07 = dt_plan.Rows[0]["d7"] == null ? "" : dt_plan.Rows[0]["d7"].ToString();
                d08 = dt_plan.Rows[0]["d8"] == null ? "" : dt_plan.Rows[0]["d8"].ToString();
                d09 = dt_plan.Rows[0]["d9"] == null ? "" : dt_plan.Rows[0]["d9"].ToString();
                d10 = dt_plan.Rows[0]["d10"] == null ? "" : dt_plan.Rows[0]["d10"].ToString();
                d11 = dt_plan.Rows[0]["d11"] == null ? "" : dt_plan.Rows[0]["d11"].ToString();
                d12 = dt_plan.Rows[0]["d12"] == null ? "" : dt_plan.Rows[0]["d12"].ToString();
                d13 = dt_plan.Rows[0]["d13"] == null ? "" : dt_plan.Rows[0]["d13"].ToString();
                d14 = dt_plan.Rows[0]["d14"] == null ? "" : dt_plan.Rows[0]["d14"].ToString();
                d15 = dt_plan.Rows[0]["d15"] == null ? "" : dt_plan.Rows[0]["d15"].ToString();
                d16 = dt_plan.Rows[0]["d16"] == null ? "" : dt_plan.Rows[0]["d16"].ToString();
                d17 = dt_plan.Rows[0]["d17"] == null ? "" : dt_plan.Rows[0]["d17"].ToString();
                d18 = dt_plan.Rows[0]["d18"] == null ? "" : dt_plan.Rows[0]["d18"].ToString();

                p01 = dt_plan.Rows[0]["p1"] == null ? "" : dt_plan.Rows[0]["p1"].ToString();
                p02 = dt_plan.Rows[0]["p2"] == null ? "" : dt_plan.Rows[0]["p2"].ToString();
                p03 = dt_plan.Rows[0]["p3"] == null ? "" : dt_plan.Rows[0]["p3"].ToString();
                p04 = dt_plan.Rows[0]["p4"] == null ? "" : dt_plan.Rows[0]["p4"].ToString();
                p05 = dt_plan.Rows[0]["p5"] == null ? "" : dt_plan.Rows[0]["p5"].ToString();
                p06 = dt_plan.Rows[0]["p6"] == null ? "" : dt_plan.Rows[0]["p6"].ToString();
                p07 = dt_plan.Rows[0]["p7"] == null ? "" : dt_plan.Rows[0]["p7"].ToString();
                p08 = dt_plan.Rows[0]["p8"] == null ? "" : dt_plan.Rows[0]["p8"].ToString();
                p09 = dt_plan.Rows[0]["p9"] == null ? "" : dt_plan.Rows[0]["p9"].ToString();
                p10 = dt_plan.Rows[0]["p10"] == null ? "" : dt_plan.Rows[0]["p10"].ToString();
                p11 = dt_plan.Rows[0]["p11"] == null ? "" : dt_plan.Rows[0]["p11"].ToString();
                p12 = dt_plan.Rows[0]["p12"] == null ? "" : dt_plan.Rows[0]["p12"].ToString();
                p13 = dt_plan.Rows[0]["p13"] == null ? "" : dt_plan.Rows[0]["p13"].ToString();
                p14 = dt_plan.Rows[0]["p14"] == null ? "" : dt_plan.Rows[0]["p14"].ToString();
                p15 = dt_plan.Rows[0]["p15"] == null ? "" : dt_plan.Rows[0]["p15"].ToString();
                p16 = dt_plan.Rows[0]["p16"] == null ? "" : dt_plan.Rows[0]["p16"].ToString();
                p17 = dt_plan.Rows[0]["p17"] == null ? "" : dt_plan.Rows[0]["p17"].ToString();
                p18 = dt_plan.Rows[0]["p18"] == null ? "" : dt_plan.Rows[0]["p18"].ToString();

                // Replace data in template document
                SearchReplace(oWord, "[project_name]", ProjectName);
                SearchReplace(oWord, "[contract_id]", ContractID);
                SearchReplace(oWord, "[customer_name]", CustomerName);
                SearchReplace(oWord, "[project_type]", HomeName);
                SearchReplace(oWord, "[area]", Area);
                SearchReplace(oWord, "[room_amount]", RoomAmount);
                SearchReplace(oWord, "[project_start]", ProjectStart);
                SearchReplace(oWord, "[customer_address]", CustomerAddress);
                SearchReplace(oWord, "[company_name]", CompanyName);
                SearchReplace(oWord, "[company_address]", CompanyAddress);
                SearchReplace(oWord, "[customer_national_id]", CustomerNationalID);
                SearchReplace(oWord, "[home_name]", HomeName);
                SearchReplace(oWord, "[total_price]", TotalPrice);
                SearchReplace(oWord, "[total_price_txt]", TotalPriceTxt);
                SearchReplace(oWord, "[company_sign]", CompanySign);
                SearchReplace(oWord, "[customer_province]", CustomerProvince);
                SearchReplace(oWord, "[month]", Month);
                SearchReplace(oWord, "[telephone] ", Telephone);

                SearchReplace(oWord, "[duration]", TotalDuration);
                SearchReplace(oWord, "[total]", TotalDuration);
                SearchReplace(oWord, "[amount1]", s01);
                SearchReplace(oWord, "[amount2]", s02);
                SearchReplace(oWord, "[amount3]", s03);
                SearchReplace(oWord, "[amount4]", s04);
                SearchReplace(oWord, "[amount5]", s05);
                SearchReplace(oWord, "[amount6]", s06);
                SearchReplace(oWord, "[amount7]", s07);
                SearchReplace(oWord, "[amount8]", s08);
                SearchReplace(oWord, "[amount9]", s09);
                SearchReplace(oWord, "[amount10]", s10);
                SearchReplace(oWord, "[amount11]", s11);
                SearchReplace(oWord, "[amount12]", s12);
                SearchReplace(oWord, "[amount13]", s13);
                SearchReplace(oWord, "[amount14]", s14);
                SearchReplace(oWord, "[amount15]", s15);
                SearchReplace(oWord, "[amount16]", s16);
                SearchReplace(oWord, "[amount17]", s17);
                SearchReplace(oWord, "[amount18]", s18);

                SearchReplace(oWord, "[start1]", d01);
                SearchReplace(oWord, "[start2]", d02);
                SearchReplace(oWord, "[start3]", d03);
                SearchReplace(oWord, "[start4]", d04);
                SearchReplace(oWord, "[start5]", d05);
                SearchReplace(oWord, "[start6]", d06);
                SearchReplace(oWord, "[start7]", d07);
                SearchReplace(oWord, "[start8]", d08);
                SearchReplace(oWord, "[start9]", d09);
                SearchReplace(oWord, "[start10]", d10);
                SearchReplace(oWord, "[start11]", d11);
                SearchReplace(oWord, "[start12]", d12);
                SearchReplace(oWord, "[start13]", d13);
                SearchReplace(oWord, "[start14]", d14);
                SearchReplace(oWord, "[start15]", d15);
                SearchReplace(oWord, "[start16]", d16);
                SearchReplace(oWord, "[start17]", d17);
                SearchReplace(oWord, "[start18]", d18);

                SearchReplace(oWord, "[p1]", p01);
                SearchReplace(oWord, "[p2]", p02);
                SearchReplace(oWord, "[p3]", p03);
                SearchReplace(oWord, "[p4]", p04);
                SearchReplace(oWord, "[p5]", p05);
                SearchReplace(oWord, "[p6]", p06);
                SearchReplace(oWord, "[p7]", p07);
                SearchReplace(oWord, "[p8]", p08);
                SearchReplace(oWord, "[p9]", p09);
                SearchReplace(oWord, "[p10]", p10);
                SearchReplace(oWord, "[p11]", p11);
                SearchReplace(oWord, "[p12]", p12);
                SearchReplace(oWord, "[p13]", p13);
                SearchReplace(oWord, "[p14]", p14);
                SearchReplace(oWord, "[p15]", p15);
                SearchReplace(oWord, "[p16]", p16);
                SearchReplace(oWord, "[p17]", p17);
                SearchReplace(oWord, "[p18]", p18);

                // Save to PDF
                // Run the macros.
                RunMacro(oWord, new Object[] { "Silent_saves_to_PDF" });

                // Quit Word and clean up.
                oDoc.Close(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDoc);
                oDoc = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oDocs);
                oDocs = null;
                oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWord);
                oWord = null;

                GC.Collect();

                return Path.GetFileName(dest.Replace(".docm", ".pdf"));
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }
    }
}
