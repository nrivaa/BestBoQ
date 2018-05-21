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

        [WebMethod]
        public string GenerateContract(string projid)
        {
            //try
            {
                string HomeName, ProjectName, CustomerName, CustomerProvince, CustomerAddress, ProjectStart, ContractID, Area, Month, TotalPrice;

                // No data yet
                string CompanyName, CompanyAddress, CustomerNationalID, TotalPriceTxt, CompanySign, RoomAmount;


        //string DocID = "AJ-BKK-AWN 2558/0001-01".Replace('/', '_');

        // Create document (Copy from template)

        string source = Server.MapPath(".") + @"\templates\BestBOQ_contract.docm";
                string dest = Server.MapPath(".") + @"\GeneratedDocument\" + projid + ".docm";
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
                RoomAmount = dt.Rows[0]["roomamount"] == null ? "" : dt.Rows[0]["roomamount"].ToString(); ;

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

                return dest.Replace(".docm", ".pdf");
            }
            //catch (Exception ex)
            //{

            //    return ex.Message + ex.StackTrace;
            //}
        }
    }
}
