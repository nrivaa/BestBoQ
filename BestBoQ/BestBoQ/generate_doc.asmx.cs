﻿using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
        [WebMethod]
        public void SendEmail(string id)
        {
            System.Diagnostics.Process.Start(@"C:\BestBoQ\GmailServiceApp\GmailServiceApp.exe", id);
        }

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
            string textReplace;
            if (columnname.Contains("Money"))
            {
                textReplace = dt.Rows[0][columnname] == null ? "" : Convert.ToDecimal(dt.Rows[0][columnname].ToString()).ToString("#,##0");
            }
            else
            {
                textReplace = dt.Rows[0][columnname] == null ? "" : dt.Rows[0][columnname].ToString();
            }
            SearchReplace(app, search, textReplace);
        }

        private string GetColumn(DataRow dr, string columnname)
        {
            return dr[columnname] == null ? "" : dr[columnname].ToString();
        }

        private void ReplaceHeaderPicture(Word.Application app, Word._Document oDoc, string projid)
        {
            DataTable dt_pic = ClassConfig.GetDataSQL("exec dbo.[get_BOQ_Picture_New] '" + projid + "'");
            object replaceAll = Word.WdReplace.wdReplaceAll;
            // Loop through all sections
            foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Sections)
            {
                Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;

                //Replace Picture
                List<Word.Range> ranges = new List<Word.Range>();
                //var test = headerRange.ShapeRange;

                foreach (DataRow dr in dt_pic.Rows)
                {
                    foreach (Word.Shape s in headerRange.ShapeRange)
                    {
                        //if (s.AlternativeText == dr["Title"].ToString())
                        if (s.Title.Trim() == dr["Title"].ToString().Trim())
                        {
                            Word.InlineShape inlineShapeRange = s.ConvertToInlineShape();

                            Word.Range toreplace = inlineShapeRange.Range;
                            inlineShapeRange.Delete();
                            string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                            var replaceShape = toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);

                            headerRange.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                    }

                    foreach (Word.InlineShape s in headerRange.InlineShapes)
                    {
                        //Debug.WriteLine("Inlineshape name(out): " + s.AlternativeText);
                        //Debug.WriteLine("row: {0}, doc: {1}", dr["Title"].ToString().Trim(), s.Title.Trim());

                        if (s.Title.Trim() == dr["Title"].ToString().Trim() || s.Title.Trim() == dr["Title"].ToString().Trim())
                        {
                            //Debug.WriteLine("Inlineshape name: " + s.AlternativeText);

                            Word.Range toreplace = s.Range;
                            s.Delete();
                            string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                            var replacedRange = toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);

                            if (dr["Title"].ToString().Trim() == "logo")
                            {
                                //replacedRange.AlternativeText = "logo";
                                replacedRange.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                            }
                        }
                    }
                }

                ////Replace Picture
                //List<Word.Range> ranges = new List<Word.Range>();
                //foreach (DataRow dr in dt_pic.Rows)
                //{
                //    foreach (Word.InlineShape s in headerRange.InlineShapes)
                //    {
                //        if (s.Title == dr["Title"].ToString() && s.Type == Word.WdInlineShapeType.wdInlineShapePicture)
                //        {
                //            Word.Range toreplace = s.Range;
                //            s.Delete();
                //            string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                //            toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);
                //        }
                //    }
                //}
            }
        }

        private void ReplaceFooter(Word.Application app, Word._Document oDoc, string search, string replace)
        {

            object replaceAll = Word.WdReplace.wdReplaceAll;
            // Loop through all sections
            foreach (Microsoft.Office.Interop.Word.Section section in oDoc.Sections)
            {
                Microsoft.Office.Interop.Word.Range footerRange = section.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                footerRange.Find.Text = search;
                footerRange.Find.Replacement.Text = replace;
                footerRange.Find.Execute(ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref replaceAll, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                ////Get all Footers
                //Microsoft.Office.Interop.Word.HeadersFooters footers = section.Footers;
                ////Section headerfooter loop for all types enum WdHeaderFooterIndex. wdHeaderFooterEvenPages/wdHeaderFooterFirstPage/wdHeaderFooterPrimary; 
                //foreach (Microsoft.Office.Interop.Word.HeaderFooter footer in footers)
                //{

                //    Word.Fields fields = footer.Range.Fields;

                //    foreach (Word.Field field in fields)
                //    {
                //        SearchReplace(app, search, replace);
                //        //if (field.Type == WdFieldType.wdFieldDate)
                //        //{
                //        //    field.Select();
                //        //    field.Delete();
                //        //    app.Selection.TypeText("[DATE]");
                //        //}
                //        //else if (field.Type == WdFieldType.wdFieldFileName)
                //        //{
                //        //    field.Select();
                //        //    field.Delete();
                //        //    app.Selection.TypeText("[FILE NAME]");

                //        //}
                //    }
                //}
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
            if (!tagTypeID.Contains("Slap"))
            {
                SearchReplace(app, tag_mini, GetColumn(dr, value));
            }
            //}

        }

        [WebMethod]
        public string GenerateDocumentPack(string projid)
        {
            string filename;
            string dest = Server.MapPath(".") + @"\GeneratedDocument\";
            if (File.Exists(dest + projid + "_BestBOQ_DocPack.pdf"))
            {
                filename = dest + projid + "_BestBOQ_DocPack.pdf";
            }
            else
            {
                string docBoq = dest + GenerateBOQ(projid);
                string docContract = dest + GenerateContract(projid);
                string docPayment = dest + GeneratePayment(projid);
                string docTimeplan = dest + GenerateTimeplan(projid);
                string docSummary = dest + GenerateSummary(projid);
                string docQuotation = dest + GenerateQuotation(projid);

                // Get some file names
                List<string> files = new List<string>();
                files.Add(docBoq);
                files.Add(docContract);
                files.Add(docPayment);
                files.Add(docTimeplan);
                files.Add(docSummary);
                files.Add(docQuotation);

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
                filename = projid + "_BestBOQ_DocPack.pdf";
                outputDocument.Save(dest + filename);
                // ...and start a viewer.
                // Process.Start(filename);
            }

            return filename;
        }

        [WebMethod]
        public string GenerateContract(string projid)
        {
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_contract.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone, space, place, auth, type, text;

                // No data yet
                string CompanyName, CompanyName_r, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount, CustomerAge, CustomerId, CompanyNationalID;

                string step01, step02, step03, step04, step05, step06, step07, step08, step09, step10, startDate, stopDate;
                string step01Txt, step02Txt, step03Txt, step04Txt, step05Txt, step06Txt, step07Txt, step08Txt, step09Txt, step10Txt;
                string step01Pct, step02Pct, step03Pct, step04Pct, step05Pct, step06Pct, step07Pct, step08Pct, step09Pct, step10Pct;
                string step01Detail, step02Detail, step03Detail, step04Detail, step05Detail, step06Detail, step07Detail, step08Detail, step09Detail, step10Detail;
                string lastText01, lastText02, lastText03, lastText04, lastText05, lastText06, lastText07, lastText08, lastText09, lastText10;

                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)
                DataTable dt_template = ClassConfig.GetDataSQL("EXEC [dbo].[get_Floor] '" + projid + "'");
                String floor = "1";
                if (dt_template.Rows.Count > 0)
                {
                    floor = dt_template.Rows[0][0].ToString().Trim();
                }

                string source = Server.MapPath(".") + @"\templates\BestBOQ_contract_" + floor + ".docm";
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

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info_new '" + projid + "'");
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
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();
                space = dt.Rows[0]["space"] == null ? "" : dt.Rows[0]["space"].ToString();
                place = dt.Rows[0]["place"] == null ? "" : dt.Rows[0]["place"].ToString();
                auth = dt.Rows[0]["auth"] == null ? "" : dt.Rows[0]["auth"].ToString();
                type = dt.Rows[0]["type"] == null ? "" : dt.Rows[0]["type"].ToString();
                text = dt.Rows[0]["text"] == null ? "" : dt.Rows[0]["text"].ToString();
                CustomerId = dt.Rows[0]["customerid"] == null ? "" : dt.Rows[0]["customerid"].ToString();
                CustomerAge = dt.Rows[0]["customerage"] == null ? "" : dt.Rows[0]["customerage"].ToString();
                CompanyNationalID = dt.Rows[0]["companynationalid"] == null ? "" : dt.Rows[0]["companynationalid"].ToString();

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
                SearchReplace(oWord, "[company_name_r]", CompanyName_r);
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
                SearchReplace(oWord, "[auth]", auth);
                SearchReplace(oWord, "[customer_Age]", CustomerAge);
                SearchReplace(oWord, "[customer_Id]", CustomerId);
                SearchReplace(oWord, "[type]", type);
                SearchReplace(oWord, "[text]", text);
                SearchReplace(oWord, "[company_national_id]", CompanyNationalID);

                //New Requirement 2020 Last Page Text
                string sql_lastPage = "EXEC [dbo].[get_Project_03_17_Text] '" + projid + "' ";
                DataTable dt_last = ClassConfig.GetDataSQL(sql_lastPage);
                if(dt_last.Rows.Count >0)
                {
                    lastText01 = dt_last.Rows[0]["text01"].ToString().Trim();
                    lastText02 = dt_last.Rows[0]["text02"].ToString().Trim();
                    lastText03 = dt_last.Rows[0]["text03"].ToString().Trim();
                    lastText04 = dt_last.Rows[0]["text04"].ToString().Trim();
                    lastText05 = dt_last.Rows[0]["text05"].ToString().Trim();
                    lastText06 = dt_last.Rows[0]["text06"].ToString().Trim();
                    lastText07 = dt_last.Rows[0]["text07"].ToString().Trim();
                    lastText08 = dt_last.Rows[0]["text08"].ToString().Trim();
                    lastText09 = dt_last.Rows[0]["text09"].ToString().Trim();
                    lastText10 = dt_last.Rows[0]["text10"].ToString().Trim();
                }
                else
                {
                    lastText01 = "";
                    lastText02 = "";
                    lastText03 = "";
                    lastText04 = "";
                    lastText05 = "";
                    lastText06 = "";
                    lastText07 = "";
                    lastText08 = "";
                    lastText09 = "";
                    lastText10 = "";
                }
                SearchReplace(oWord, "[attach_list01]", lastText01);
                SearchReplace(oWord, "[attach_list02]", lastText02);
                SearchReplace(oWord, "[attach_list03]", lastText03);
                SearchReplace(oWord, "[attach_list04]", lastText04);
                SearchReplace(oWord, "[attach_list05]", lastText05);
                SearchReplace(oWord, "[attach_list06]", lastText06);
                SearchReplace(oWord, "[attach_list07]", lastText07);
                SearchReplace(oWord, "[attach_list08]", lastText08);
                SearchReplace(oWord, "[attach_list09]", lastText09);
                SearchReplace(oWord, "[attach_list10]", lastText10);

                //Replace Picture
                ReplacePicture(projid, oDoc);

                //Replace Header Picture
                ReplaceHeaderPicture(oWord, oDoc, projid);

                // Replace footer
                ReplaceFooter(oWord, oDoc, "[company_name]", CompanyName);
                ReplaceFooter(oWord, oDoc, "[company_address]", CompanyAddress);
                ReplaceFooter(oWord, oDoc, "[telephone] ", Telephone);

                DataTable dt_pay = ClassConfig.GetDataSQL("exec dbo.get_AppendixA_New '" + projid + "'");
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

                DataTable dtText = ClassConfig.GetDataSQL("[BESTBoQ].[dbo].[get_template_03_16] '" + projid + "' ");
                step01Pct = dtText.Rows[0]["pct"] == null ? "" : dtText.Rows[0]["pct"].ToString() + "%";
                step02Pct = dtText.Rows[1]["pct"] == null ? "" : dtText.Rows[1]["pct"].ToString() + "%";
                step03Pct = dtText.Rows[2]["pct"] == null ? "" : dtText.Rows[2]["pct"].ToString() + "%";
                step04Pct = dtText.Rows[3]["pct"] == null ? "" : dtText.Rows[3]["pct"].ToString() + "%";
                step05Pct = dtText.Rows[4]["pct"] == null ? "" : dtText.Rows[4]["pct"].ToString() + "%";
                step06Pct = dtText.Rows[5]["pct"] == null ? "" : dtText.Rows[5]["pct"].ToString() + "%";
                step07Pct = dtText.Rows[6]["pct"] == null ? "" : dtText.Rows[6]["pct"].ToString() + "%";
                step08Pct = dtText.Rows[7]["pct"] == null ? "" : dtText.Rows[7]["pct"].ToString() + "%";
                step09Pct = dtText.Rows[8]["pct"] == null ? "" : dtText.Rows[8]["pct"].ToString() + "%";
                step10Pct = dtText.Rows[9]["pct"] == null ? "" : dtText.Rows[9]["pct"].ToString() + "%";

                step01Detail = dtText.Rows[0]["Detail"] == null ? "" : dtText.Rows[0]["Detail"].ToString();
                step02Detail = dtText.Rows[1]["Detail"] == null ? "" : dtText.Rows[1]["Detail"].ToString();
                step03Detail = dtText.Rows[2]["Detail"] == null ? "" : dtText.Rows[2]["Detail"].ToString();
                step04Detail = dtText.Rows[3]["Detail"] == null ? "" : dtText.Rows[3]["Detail"].ToString();
                step05Detail = dtText.Rows[4]["Detail"] == null ? "" : dtText.Rows[4]["Detail"].ToString();
                step06Detail = dtText.Rows[5]["Detail"] == null ? "" : dtText.Rows[5]["Detail"].ToString();
                step07Detail = dtText.Rows[6]["Detail"] == null ? "" : dtText.Rows[6]["Detail"].ToString();
                step08Detail = dtText.Rows[7]["Detail"] == null ? "" : dtText.Rows[7]["Detail"].ToString();
                step09Detail = dtText.Rows[8]["Detail"] == null ? "" : dtText.Rows[8]["Detail"].ToString();
                step10Detail = dtText.Rows[9]["Detail"] == null ? "" : dtText.Rows[9]["Detail"].ToString();

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

                SearchReplace(oWord, "[Term1Pct]", step01Pct);
                SearchReplace(oWord, "[Term2Pct]", step02Pct);
                SearchReplace(oWord, "[Term3Pct]", step03Pct);
                SearchReplace(oWord, "[Term4Pct]", step04Pct);
                SearchReplace(oWord, "[Term5Pct]", step05Pct);
                SearchReplace(oWord, "[Term6Pct]", step06Pct);
                SearchReplace(oWord, "[Term7Pct]", step07Pct);
                SearchReplace(oWord, "[Term8Pct]", step08Pct);
                SearchReplace(oWord, "[Term9Pct]", step09Pct);
                SearchReplace(oWord, "[Term10Pct]", step10Pct);
                SearchReplace(oWord, "[Term1Detail]", step01Detail);
                SearchReplace(oWord, "[Term2Detail]", step02Detail);
                SearchReplace(oWord, "[Term3Detail]", step03Detail);
                SearchReplace(oWord, "[Term4Detail]", step04Detail);
                SearchReplace(oWord, "[Term5Detail]", step05Detail);
                SearchReplace(oWord, "[Term6Detail]", step06Detail);
                SearchReplace(oWord, "[Term7Detail]", step07Detail);
                SearchReplace(oWord, "[Term8Detail]", step08Detail);
                SearchReplace(oWord, "[Term9Detail]", step09Detail);
                SearchReplace(oWord, "[Term10Detail]", step10Detail);

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
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_payment.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone;

                // No data yet
                string CompanyName, CompanyName_r, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;
                string step01, step02, step03, step04, step05, step06, step07, step08, step09, step10, startDate, stopDate;
                string step01Txt, step02Txt, step03Txt, step04Txt, step05Txt, step06Txt, step07Txt, step08Txt, step09Txt, step10Txt;
                string step01Pct, step02Pct, step03Pct, step04Pct, step05Pct, step06Pct, step07Pct, step08Pct, step09Pct, step10Pct;
                string step01Detail, step02Detail, step03Detail, step04Detail, step05Detail, step06Detail, step07Detail, step08Detail, step09Detail, step10Detail;

                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)
                DataTable dt_template = ClassConfig.GetDataSQL("EXEC [dbo].[get_Floor] '" + projid + "'");
                String floor = "1";
                if (dt_template.Rows.Count > 0)
                {
                    floor = dt_template.Rows[0][0].ToString().Trim();
                }
                string source = Server.MapPath(".") + @"\templates\BestBOQ_PaymentTerm_" + floor + ".docm";
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

                DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info'" + projid + "'");
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
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString(); ;

                DataTable dt_pay = ClassConfig.GetDataSQL("exec dbo.get_AppendixA_New '" + projid + "'");
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

                DataTable dtText = ClassConfig.GetDataSQL("[BESTBoQ].[dbo].[get_template_03_16] '" + projid + "' ");
                step01Pct = dtText.Rows[0]["pct"] == null ? "" : dtText.Rows[0]["pct"].ToString() + "%";
                step02Pct = dtText.Rows[1]["pct"] == null ? "" : dtText.Rows[1]["pct"].ToString() + "%";
                step03Pct = dtText.Rows[2]["pct"] == null ? "" : dtText.Rows[2]["pct"].ToString() + "%";
                step04Pct = dtText.Rows[3]["pct"] == null ? "" : dtText.Rows[3]["pct"].ToString() + "%";
                step05Pct = dtText.Rows[4]["pct"] == null ? "" : dtText.Rows[4]["pct"].ToString() + "%";
                step06Pct = dtText.Rows[5]["pct"] == null ? "" : dtText.Rows[5]["pct"].ToString() + "%";
                step07Pct = dtText.Rows[6]["pct"] == null ? "" : dtText.Rows[6]["pct"].ToString() + "%";
                step08Pct = dtText.Rows[7]["pct"] == null ? "" : dtText.Rows[7]["pct"].ToString() + "%";
                step09Pct = dtText.Rows[8]["pct"] == null ? "" : dtText.Rows[8]["pct"].ToString() + "%";
                step10Pct = dtText.Rows[9]["pct"] == null ? "" : dtText.Rows[9]["pct"].ToString() + "%";

                step01Detail = dtText.Rows[0]["Detail"] == null ? "" : dtText.Rows[0]["Detail"].ToString();
                step02Detail = dtText.Rows[1]["Detail"] == null ? "" : dtText.Rows[1]["Detail"].ToString();
                step03Detail = dtText.Rows[2]["Detail"] == null ? "" : dtText.Rows[2]["Detail"].ToString();
                step04Detail = dtText.Rows[3]["Detail"] == null ? "" : dtText.Rows[3]["Detail"].ToString();
                step05Detail = dtText.Rows[4]["Detail"] == null ? "" : dtText.Rows[4]["Detail"].ToString();
                step06Detail = dtText.Rows[5]["Detail"] == null ? "" : dtText.Rows[5]["Detail"].ToString();
                step07Detail = dtText.Rows[6]["Detail"] == null ? "" : dtText.Rows[6]["Detail"].ToString();
                step08Detail = dtText.Rows[7]["Detail"] == null ? "" : dtText.Rows[7]["Detail"].ToString();
                step09Detail = dtText.Rows[8]["Detail"] == null ? "" : dtText.Rows[8]["Detail"].ToString();
                step10Detail = dtText.Rows[9]["Detail"] == null ? "" : dtText.Rows[9]["Detail"].ToString();

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
                SearchReplace(oWord, "[company_name_r]", CompanyName_r);
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

                SearchReplace(oWord, "[Term1Pct]", step01Pct);
                SearchReplace(oWord, "[Term2Pct]", step02Pct);
                SearchReplace(oWord, "[Term3Pct]", step03Pct);
                SearchReplace(oWord, "[Term4Pct]", step04Pct);
                SearchReplace(oWord, "[Term5Pct]", step05Pct);
                SearchReplace(oWord, "[Term6Pct]", step06Pct);
                SearchReplace(oWord, "[Term7Pct]", step07Pct);
                SearchReplace(oWord, "[Term8Pct]", step08Pct);
                SearchReplace(oWord, "[Term9Pct]", step09Pct);
                SearchReplace(oWord, "[Term10Pct]", step10Pct);
                SearchReplace(oWord, "[Term1Detail]", step01Detail);
                SearchReplace(oWord, "[Term2Detail]", step02Detail);
                SearchReplace(oWord, "[Term3Detail]", step03Detail);
                SearchReplace(oWord, "[Term4Detail]", step04Detail);
                SearchReplace(oWord, "[Term5Detail]", step05Detail);
                SearchReplace(oWord, "[Term6Detail]", step06Detail);
                SearchReplace(oWord, "[Term7Detail]", step07Detail);
                SearchReplace(oWord, "[Term8Detail]", step08Detail);
                SearchReplace(oWord, "[Term9Detail]", step09Detail);
                SearchReplace(oWord, "[Term10Detail]", step10Detail);

                //Replace Form Content
                //DataTable dt_form = ClassConfig.GetDataSQL("exec dbo.[get_BOQ_Picture_New] '" + projid + "'");
                //foreach (DataRow dataRow in dt_form.Rows)
                //{
                //    SearchReplace(oWord, dataRow[0].ToString().Trim(), dataRow[1].ToString().Trim());
                //}

                //Replace Picture
                ReplacePicture(projid, oDoc);

                //Replace Header Picture
                ReplaceHeaderPicture(oWord, oDoc, projid);

                // Replace footer
                ReplaceFooter(oWord, oDoc, "[company_name]", CompanyName);
                ReplaceFooter(oWord, oDoc, "[company_address]", CompanyAddress);
                ReplaceFooter(oWord, oDoc, "[telephone]", Telephone);

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
            string CompanyName, CompanyName_r, CompanyAddress, Telephone;
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_boq.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
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

                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();

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

                // Get BOQ Dat
                DataTable dt_dat = ClassConfig.GetDataSQL("exec dbo.[get_BOQ_Dat] '" + projid + "'");
                GetColumnAndSearchReplace(dt_dat, "<3A1>", oWord, "<3A1>");
                GetColumnAndSearchReplace(dt_dat, "<3A2>", oWord, "<3A2>");
                GetColumnAndSearchReplace(dt_dat, "<3A3>", oWord, "<3A3>");
                GetColumnAndSearchReplace(dt_dat, "<3A4>", oWord, "<3A4>");
                GetColumnAndSearchReplace(dt_dat, "<3A5>", oWord, "<3A5>");
                GetColumnAndSearchReplace(dt_dat, "<3A6>", oWord, "<3A6>");
                GetColumnAndSearchReplace(dt_dat, "<4FR1>", oWord, "<4FR1>");
                GetColumnAndSearchReplace(dt_dat, "<4FR2>", oWord, "<4FR2>");
                GetColumnAndSearchReplace(dt_dat, "<4FR3>", oWord, "<4FR3>");
                GetColumnAndSearchReplace(dt_dat, "<4FR4>", oWord, "<4FR4>");
                GetColumnAndSearchReplace(dt_dat, "<4FR5>", oWord, "<4FR5>");
                GetColumnAndSearchReplace(dt_dat, "<4FR6>", oWord, "<4FR6>");
                GetColumnAndSearchReplace(dt_dat, "<4FR7>", oWord, "<4FR7>");
                GetColumnAndSearchReplace(dt_dat, "<4FR8>", oWord, "<4FR8>");
                GetColumnAndSearchReplace(dt_dat, "<4FR9>", oWord, "<4FR9>");
                GetColumnAndSearchReplace(dt_dat, "<5A6>", oWord, "<5A6>");
                GetColumnAndSearchReplace(dt_dat, "<60>", oWord, "<60>");
                GetColumnAndSearchReplace(dt_dat, "<61>", oWord, "<61>");
                GetColumnAndSearchReplace(dt_dat, "<62>", oWord, "<62>");
                GetColumnAndSearchReplace(dt_dat, "<63>", oWord, "<63>");
                GetColumnAndSearchReplace(dt_dat, "<67>", oWord, "<67>");
                GetColumnAndSearchReplace(dt_dat, "<7A>", oWord, "<7A>");
                GetColumnAndSearchReplace(dt_dat, "<7B>", oWord, "<7B>");
                GetColumnAndSearchReplace(dt_dat, "<7C>", oWord, "<7C>");
                GetColumnAndSearchReplace(dt_dat, "<F1>", oWord, "<F1>");
                GetColumnAndSearchReplace(dt_dat, "<F2>", oWord, "<F2>");
                GetColumnAndSearchReplace(dt_dat, "<F3>", oWord, "<F3>");
                GetColumnAndSearchReplace(dt_dat, "<F4>", oWord, "<F4>");
                GetColumnAndSearchReplace(dt_dat, "<F5>", oWord, "<F5>");
                GetColumnAndSearchReplace(dt_dat, "<B1>", oWord, "<B1>");
                GetColumnAndSearchReplace(dt_dat, "<B2>", oWord, "<B2>");
                GetColumnAndSearchReplace(dt_dat, "<B3>", oWord, "<B3>");
                GetColumnAndSearchReplace(dt_dat, "<B4>", oWord, "<B4>");
                GetColumnAndSearchReplace(dt_dat, "<B5>", oWord, "<B5>");
                GetColumnAndSearchReplace(dt_dat, "<13R1>", oWord, "<13R1>");
                GetColumnAndSearchReplace(dt_dat, "<13R2>", oWord, "<13R2>");
                GetColumnAndSearchReplace(dt_dat, "<13R3>", oWord, "<13R3>");

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
                for (int i = 1; i <= 17; i++)
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

                //Replace Picture
                ReplacePicture(projid, oDoc);


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

                // Replace footer
                ReplaceFooter(oWord, oDoc, "[company_name]", CompanyName);
                ReplaceFooter(oWord, oDoc, "[company_address]", CompanyAddress);
                ReplaceFooter(oWord, oDoc, "[telephone]", Telephone);

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
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, Telephone, TotalPrice;

                // No data yet
                string CompanyName, CompanyName_r, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;
                string benefit, power, material;
                string m_01, m_02, m_03, m_04, m_05, m_06, m_07, m_08, m_09, m_10, m_11, m_12, m_13, m_14, m_15, m_16, m_17, m_18;
                string c_01, c_02, c_03, c_04, c_05, c_06, c_07, c_08, c_09, c_10, c_11, c_12, c_13, c_14, c_15, c_16, c_17, c_18;

                // Create document (Copy from template)
                string source;
                string dest;
                //DataTable dt_report = ClassConfig.GetDataSQL("exec dbo.get_Report_new1 '" + projid + "'");

                //benefit = dt_report.Rows[0]["Total_Benefit"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["Total_Benefit"].ToString()).ToString("#,##0");
                //power = dt_report.Rows[0]["Total_Power"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["Total_Power"].ToString()).ToString("#,##0");
                //material = dt_report.Rows[0]["Total_Material"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["Total_Material"].ToString()).ToString("#,##0");
                //TotalPrice = dt_report.Rows[0]["Total"] == null ? "" : Convert.ToDecimal(dt_report.Rows[0]["Total"].ToString()).ToString("#,##0");

                DataTable dt_report_detail = ClassConfig.GetDataSQL("exec dbo.get_Report_new3 '" + projid + "'");
                m_01 = dt_report_detail.Rows[0]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[0]["Cost"].ToString()).ToString("#,##0");
                m_02 = dt_report_detail.Rows[1]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[1]["Cost"].ToString()).ToString("#,##0");
                m_03 = dt_report_detail.Rows[2]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[2]["Cost"].ToString()).ToString("#,##0");
                m_04 = dt_report_detail.Rows[3]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[3]["Cost"].ToString()).ToString("#,##0");
                m_05 = dt_report_detail.Rows[4]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[4]["Cost"].ToString()).ToString("#,##0");
                m_06 = dt_report_detail.Rows[5]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[5]["Cost"].ToString()).ToString("#,##0");
                m_07 = dt_report_detail.Rows[6]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[6]["Cost"].ToString()).ToString("#,##0");
                m_08 = dt_report_detail.Rows[7]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[7]["Cost"].ToString()).ToString("#,##0");
                m_09 = dt_report_detail.Rows[8]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[8]["Cost"].ToString()).ToString("#,##0");
                m_10 = dt_report_detail.Rows[9]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[9]["Cost"].ToString()).ToString("#,##0");
                m_11 = dt_report_detail.Rows[10]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[10]["Cost"].ToString()).ToString("#,##0");
                m_12 = dt_report_detail.Rows[11]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[11]["Cost"].ToString()).ToString("#,##0");
                m_13 = dt_report_detail.Rows[12]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[12]["Cost"].ToString()).ToString("#,##0");
                m_14 = dt_report_detail.Rows[13]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[13]["Cost"].ToString()).ToString("#,##0");
                m_15 = dt_report_detail.Rows[14]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[14]["Cost"].ToString()).ToString("#,##0");
                m_16 = dt_report_detail.Rows[15]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[15]["Cost"].ToString()).ToString("#,##0");
                m_17 = dt_report_detail.Rows[16]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[16]["Cost"].ToString()).ToString("#,##0");
                //m_18 = dt_report_detail.Rows[17]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[17]["Cost"].ToString()).ToString("#,##0");

                c_01 = dt_report_detail.Rows[0]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[0]["Power"].ToString()).ToString("#,##0");
                c_02 = dt_report_detail.Rows[1]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[1]["Power"].ToString()).ToString("#,##0");
                c_03 = dt_report_detail.Rows[2]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[2]["Power"].ToString()).ToString("#,##0");
                c_04 = dt_report_detail.Rows[3]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[3]["Power"].ToString()).ToString("#,##0");
                c_05 = dt_report_detail.Rows[4]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[4]["Power"].ToString()).ToString("#,##0");
                c_06 = dt_report_detail.Rows[5]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[5]["Power"].ToString()).ToString("#,##0");
                c_07 = dt_report_detail.Rows[6]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[6]["Power"].ToString()).ToString("#,##0");
                c_08 = dt_report_detail.Rows[7]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[7]["Power"].ToString()).ToString("#,##0");
                c_09 = dt_report_detail.Rows[8]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[8]["Power"].ToString()).ToString("#,##0");
                c_10 = dt_report_detail.Rows[9]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[9]["Power"].ToString()).ToString("#,##0");
                c_11 = dt_report_detail.Rows[10]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[10]["Power"].ToString()).ToString("#,##0");
                c_12 = dt_report_detail.Rows[11]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[11]["Power"].ToString()).ToString("#,##0");
                c_13 = dt_report_detail.Rows[12]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[12]["Power"].ToString()).ToString("#,##0");
                c_14 = dt_report_detail.Rows[13]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[13]["Power"].ToString()).ToString("#,##0");
                c_15 = dt_report_detail.Rows[14]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[14]["Power"].ToString()).ToString("#,##0");
                c_16 = dt_report_detail.Rows[15]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[15]["Power"].ToString()).ToString("#,##0");
                c_17 = dt_report_detail.Rows[16]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[16]["Power"].ToString()).ToString("#,##0");

                TotalPrice = dt_report_detail.Rows[17]["Total"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[17]["Total"].ToString()).ToString("#,##0");
                material = dt_report_detail.Rows[17]["Cost"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[17]["Cost"].ToString()).ToString("#,##0");
                power = dt_report_detail.Rows[17]["Power"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[17]["Power"].ToString()).ToString("#,##0");
                benefit = dt_report_detail.Rows[17]["Benefit"] == null ? "" : Convert.ToDecimal(dt_report_detail.Rows[17]["Benefit"].ToString()).ToString("#,##0");


                double pct_material = Convert.ToDouble(material) / Convert.ToDouble(TotalPrice);
                double pct_power = Convert.ToDouble(power) / Convert.ToDouble(TotalPrice);
                double pct_benefit = Convert.ToDouble(benefit) / Convert.ToDouble(TotalPrice);

                if (pct_material >= 0.5)
                {
                    if (pct_power > pct_benefit && (pct_power - pct_benefit) > 0.1)
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_01.docm";
                    }
                    else if ((pct_power > pct_benefit && (pct_power - pct_benefit) <= 0.1) || (pct_power < pct_benefit && (pct_benefit - pct_power) <= 0.1))
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_02.docm";
                    }
                    else if (pct_power < pct_benefit && (pct_benefit - pct_power) > 0.1)
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_03.docm";
                    }
                    else
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_03.docm";
                    }
                }
                else
                {
                    if (pct_power > pct_benefit && (pct_power - pct_benefit) > 0.1)
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_04.docm";
                    }
                    else if ((pct_power > pct_benefit && (pct_power - pct_benefit) <= 0.1) || (pct_power < pct_benefit && (pct_benefit - pct_power) <= 0.1))
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_05.docm";
                    }
                    else if (pct_power < pct_benefit && (pct_benefit - pct_power) > 0.1)
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_06.docm";
                    }
                    else
                    {
                        source = Server.MapPath(".") + @"\templates\BestBOQ_summary_new_V2_05.docm";
                    }
                }
                dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.docm";
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
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();


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
                SearchReplace(oWord, "[telephone] ", Telephone);

                SearchReplace(oWord, "[b_total]", benefit);
                SearchReplace(oWord, "[p_total]", power);
                SearchReplace(oWord, "[m_total]", material);
                SearchReplace(oWord, "[total]", TotalPrice);

                SearchReplace(oWord, "[m_01]", m_01);
                SearchReplace(oWord, "[m_02]", m_02);
                SearchReplace(oWord, "[m_03]", m_03);
                SearchReplace(oWord, "[m_04]", m_04);
                SearchReplace(oWord, "[m_05]", m_05);
                SearchReplace(oWord, "[m_06]", m_06);
                SearchReplace(oWord, "[m_07]", m_07);
                SearchReplace(oWord, "[m_08]", m_08);
                SearchReplace(oWord, "[m_09]", m_09);
                SearchReplace(oWord, "[m_10]", m_10);
                SearchReplace(oWord, "[m_11]", m_11);
                SearchReplace(oWord, "[m_12]", m_12);
                SearchReplace(oWord, "[m_13]", m_13);
                SearchReplace(oWord, "[m_14]", m_14);
                SearchReplace(oWord, "[m_15]", m_15);
                SearchReplace(oWord, "[m_16]", m_16);
                SearchReplace(oWord, "[m_17]", m_17);
                //SearchReplace(oWord, "[m_18]", m_18);

                SearchReplace(oWord, "[c_01]", c_01);
                SearchReplace(oWord, "[c_02]", c_02);
                SearchReplace(oWord, "[c_03]", c_03);
                SearchReplace(oWord, "[c_04]", c_04);
                SearchReplace(oWord, "[c_05]", c_05);
                SearchReplace(oWord, "[c_06]", c_06);
                SearchReplace(oWord, "[c_07]", c_07);
                SearchReplace(oWord, "[c_08]", c_08);
                SearchReplace(oWord, "[c_09]", c_09);
                SearchReplace(oWord, "[c_10]", c_10);
                SearchReplace(oWord, "[c_11]", c_11);
                SearchReplace(oWord, "[c_12]", c_12);
                SearchReplace(oWord, "[c_13]", c_13);
                SearchReplace(oWord, "[c_14]", c_14);
                SearchReplace(oWord, "[c_15]", c_15);
                SearchReplace(oWord, "[c_16]", c_16);
                SearchReplace(oWord, "[c_17]", c_17);
                //SearchReplace(oWord, "[c_18]", c_18);

                //Replace Picture
                ReplacePicture(projid, oDoc);

                // Replace footer
                ReplaceFooter(oWord, oDoc, "[company_name]", CompanyName);
                ReplaceFooter(oWord, oDoc, "[company_address]", CompanyAddress);
                ReplaceFooter(oWord, oDoc, "[telephone]", Telephone);

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
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_Timeplan.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone;

                // No data yet
                string CompanyName, CompanyName_r, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount, TotalDuration;
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
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
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

                //Replace Picture
                ReplacePicture(projid, oDoc);

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

        private void ReplacePicture(string projid, Word._Document oDoc)
        {
            //Replace Picture
            DataTable dt_pic = ClassConfig.GetDataSQL("exec dbo.[get_BOQ_Picture_New] '" + projid + "'");
            List<Word.Range> ranges = new List<Word.Range>();
            //var test = oDoc.Shapes;

            foreach (DataRow dr in dt_pic.Rows)
            {
                foreach (Word.Shape s in oDoc.Shapes)
                {
                    //Debug.WriteLine("row: {0}, doc: {1}", dr["Title"].ToString().Trim(), s.Title.Trim());
                    //if (s.AlternativeText.Trim() == dr["Title"].ToString().Trim())
                    if (s.Title.Trim() == dr["Title"].ToString().Trim())
                    {
                        //Debug.WriteLine("Shape name: " + s.AlternativeText);

                        Word.InlineShape inlineShapeRange = s.ConvertToInlineShape();

                        Word.Range toreplace = inlineShapeRange.Range;
                        inlineShapeRange.Delete();
                        string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                        var replacedRange = toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);

                        if (dr["Title"].ToString().Trim() == "logo")
                        {
                            //replacedRange.AlternativeText = "logo";
                            replacedRange.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                    }
                }

                foreach (Word.InlineShape s in oDoc.InlineShapes)
                {
                    //Debug.WriteLine("Inlineshape name(out): " + s.AlternativeText);
                    //Debug.WriteLine("row: {0}, doc: {1}", dr["Title"].ToString().Trim(), s.Title.Trim());

                    if (s.Title.Trim() == dr["Title"].ToString().Trim() || s.Title.Trim() == dr["Title"].ToString().Trim())
                    {
                        //Debug.WriteLine("Inlineshape name: " + s.AlternativeText);

                        Word.Range toreplace = s.Range;
                        s.Delete();
                        string picPath = Server.MapPath(".") + @"\" + dr["picpath"].ToString();
                        var replacedRange = toreplace.InlineShapes.AddPicture(picPath, ref oMissing, ref oMissing, ref oMissing);

                        if (dr["Title"].ToString().Trim() == "logo")
                        {
                            //replacedRange.AlternativeText = "logo";
                            replacedRange.Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                    }
                }
            }

            //foreach (Word.InlineShape s in oDoc.InlineShapes)
            //{
            //    if (s.AlternativeText == "logo")
            //    {
            //        Word.Range toFormatted = s.Range;
            //        toFormatted.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            //        //toFormatted.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            //    }
            //}
        }

        [WebMethod]
        public string GenerateQuotation(string projid)
        {
            string destpdf = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_quotation.pdf";
            if (File.Exists(destpdf))
            {
                return Path.GetFileName(destpdf);
            }
            else
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone, space, place, auth;

                // No data yet
                string CompanyName, CompanyName_r, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount, CustomerPhone, CustomerEmail, Vat, Now;


                //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

                // Create document (Copy from template)

                string source = Server.MapPath(".") + @"\templates\BestBOQ_quotation.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_quotation.docm";
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
                CompanyName_r = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyName = CompanyName_r == "" ? dt.Rows[0]["projectowner"].ToString() : CompanyName_r;
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();
                space = dt.Rows[0]["space"] == null ? "" : dt.Rows[0]["space"].ToString();
                place = dt.Rows[0]["place"] == null ? "" : dt.Rows[0]["place"].ToString();
                auth = dt.Rows[0]["auth"] == null ? "" : dt.Rows[0]["auth"].ToString();
                CustomerPhone = dt.Rows[0]["customerphone"] == null ? "" : dt.Rows[0]["customerphone"].ToString();
                CustomerEmail = dt.Rows[0]["customeremail"] == null ? "" : dt.Rows[0]["customeremail"].ToString();
                Vat = dt.Rows[0]["totalprice"] == null ? "" : (Convert.ToDouble(dt.Rows[0]["totalprice"].ToString()) * 0.07).ToString("#,##0.00");
                Now = DateTime.UtcNow.Date.ToString("dd/MM/yyyy");

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
                SearchReplace(oWord, "[company_name_r]", CompanyName_r);
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
                SearchReplace(oWord, "[auth]", auth);
                SearchReplace(oWord, "[customer_phone]", CustomerPhone);
                SearchReplace(oWord, "[customer_email]", CustomerEmail);
                SearchReplace(oWord, "[vat]", Vat);
                SearchReplace(oWord, "[date_now]", Now);

                //Replace Picture
                ReplacePicture(projid, oDoc);

                //Replace Header Picture
                ReplaceHeaderPicture(oWord, oDoc, projid);

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
    }
}
