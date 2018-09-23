using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Word = Microsoft.Office.Interop.Word;
using System.Web.Services;
using System.IO;
using System.Data;

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

        private void ReplaceBOQ(Word.Application app, DataRow dr, string tagType, string value) {
            string tagMaterial = GetColumn(dr, "Material").Trim();
            string tagTypeID = GetColumn(dr, "TypeID").Trim();
            string tag = "<" + tagMaterial + "_" + tagTypeID + "_" + tagType.Trim() + ">";
            string tag_mini = "<" + tagMaterial + "_" + tagType + ">";

            //if (Search(app, tag))
            //{
                SearchReplace(app, tag, GetColumn(dr, value));
            //}
            //else
            //{
                SearchReplace(app, tag_mini, GetColumn(dr, value));
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
        public string GenerateContract(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice, Telephone, space;

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
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : dt.Rows[0]["totalprice"].ToString();
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString();
                space = dt.Rows[0]["space"] == null ? "" : dt.Rows[0]["space"].ToString();

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
                TotalPrice = dt.Rows[0]["totalprice"] == null ? "" : dt.Rows[0]["totalprice"].ToString();
                CompanyName = dt.Rows[0]["companyname"] == null ? "" : dt.Rows[0]["companyname"].ToString();
                CompanyAddress = dt.Rows[0]["companyaddress"] == null ? "" : dt.Rows[0]["companyaddress"].ToString();
                CustomerNationalID = dt.Rows[0]["customernationalid"] == null ? "" : dt.Rows[0]["customernationalid"].ToString();
                TotalPriceTxt = ClassConfig.ThaiBaht(TotalPrice);
                CompanySign = dt.Rows[0]["projectowner"] == null ? "" : dt.Rows[0]["projectowner"].ToString();
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString();
                Telephone = dt.Rows[0]["telephone"] == null ? "" : dt.Rows[0]["telephone"].ToString(); ;

                DataTable dt_pay = ClassConfig.GetDataSQL("exec dbo.get_AppendixA '" + projid + "'");
                step01 = dt_pay.Rows[0]["step01"] == null ? "" : dt_pay.Rows[0]["step01"].ToString();
                step01Txt = ClassConfig.ThaiBaht(step01);
                step02 = dt_pay.Rows[0]["step02"] == null ? "" : dt_pay.Rows[0]["step02"].ToString();
                step02Txt = ClassConfig.ThaiBaht(step02);
                step03 = dt_pay.Rows[0]["step03"] == null ? "" : dt_pay.Rows[0]["step03"].ToString();
                step03Txt = ClassConfig.ThaiBaht(step03);
                step04 = dt_pay.Rows[0]["step04"] == null ? "" : dt_pay.Rows[0]["step04"].ToString();
                step04Txt = ClassConfig.ThaiBaht(step04);
                step05 = dt_pay.Rows[0]["step05"] == null ? "" : dt_pay.Rows[0]["step05"].ToString();
                step05Txt = ClassConfig.ThaiBaht(step05);
                step06 = dt_pay.Rows[0]["step06"] == null ? "" : dt_pay.Rows[0]["step06"].ToString();
                step06Txt = ClassConfig.ThaiBaht(step06);
                step07 = dt_pay.Rows[0]["step07"] == null ? "" : dt_pay.Rows[0]["step07"].ToString();
                step07Txt = ClassConfig.ThaiBaht(step07);
                step08 = dt_pay.Rows[0]["step08"] == null ? "" : dt_pay.Rows[0]["step08"].ToString();
                step08Txt = ClassConfig.ThaiBaht(step08);
                step09 = dt_pay.Rows[0]["step09"] == null ? "" : dt_pay.Rows[0]["step09"].ToString();
                step09Txt = ClassConfig.ThaiBaht(step09);
                step10 = dt_pay.Rows[0]["step10"] == null ? "" : dt_pay.Rows[0]["step10"].ToString();
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

                    source = Server.MapPath(".") + @"\templates\BestBOQ_summary"+ template + ".docm";
                    dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.docm";
                    File.Copy(source, dest, true);
                }else
                {
                    source = Server.MapPath(".") + @"\templates\BestBOQ_summary1.docm";
                    dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + "_summary.docm";
                    File.Copy(source, dest, true);
                }
                benefit = dt_report.Rows[0]["bBenefit"] == null ? "" : dt_report.Rows[0]["bBenefit"].ToString();
                power = dt_report.Rows[0]["bPower"] == null ? "" : dt_report.Rows[0]["bPower"].ToString();
                material = dt_report.Rows[0]["bMaterial"] == null ? "" : dt_report.Rows[0]["bMaterial"].ToString();
                benefit_pct = dt_report.Rows[0]["pBenefit"] == null ? "" : dt_report.Rows[0]["pBenefit"].ToString();
                power_pct = dt_report.Rows[0]["pPower"] == null ? "" : dt_report.Rows[0]["pPower"].ToString();
                material_pct = dt_report.Rows[0]["pMaterial"] == null ? "" : dt_report.Rows[0]["pMaterial"].ToString();

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
    }
}
