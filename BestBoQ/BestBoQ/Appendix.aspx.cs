using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Appendix : System.Web.UI.Page
    {
        public string ProjectName { get; set; }
        public string ProjectType { get; set; }
        public string Area { get; set; }
        public string AmountNumber { get; set; }
        public string AmountText { get; set; }
        public string StartDate { get; set; }
        public string StopDate { get; set; }
        public string Period { get; set; }
        public string VendorName { get; set; }
        public string VendorCompany { get; set; }
        public string ClientName { get; set; }
        public string ClientComapny { get; set; }

        public string Term1Pct { get; set; }
        public string Term2Pct { get; set; }
        public string Term3Pct { get; set; }
        public string Term4Pct { get; set; }
        public string Term5Pct { get; set; }
        public string Term6Pct { get; set; }
        public string Term7Pct { get; set; }
        public string Term8Pct { get; set; }
        public string Term9Pct { get; set; }
        public string Term10Pct { get; set; }

        public string Term1Amount { get; set; }
        public string Term2Amount { get; set; }
        public string Term3Amount { get; set; }
        public string Term4Amount { get; set; }
        public string Term5Amount { get; set; }
        public string Term6Amount { get; set; }
        public string Term7Amount { get; set; }
        public string Term8Amount { get; set; }
        public string Term9Amount { get; set; }
        public string Term10Amount { get; set; }

        public string Term1Text { get; set; }
        public string Term2Text { get; set; }
        public string Term3Text { get; set; }
        public string Term4Text { get; set; }
        public string Term5Text { get; set; }
        public string Term6Text { get; set; }
        public string Term7Text { get; set; }
        public string Term8Text { get; set; }
        public string Term9Text { get; set; }
        public string Term10Text { get; set; }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            string projectID = Request["projectID"];
            projectID = "000001";
            dt = ClassConfig.GetDataSQL("exec [dbo].[get_AppendixA] '@pprojid'".Replace("@pprojid", projectID));

            if (dt.Rows[0]["hometype"].ToString() != "")
            {
                ProjectType = dt.Rows[0]["hometype"].ToString();
                Area = dt.Rows[0]["numMM"].ToString();
                
                AmountNumber = Convert.ToDouble(dt.Rows[0]["Total"]).ToString("N0");
                AmountText = ThaiBaht(dt.Rows[0]["Total"].ToString());
                StartDate = Convert.ToDateTime(dt.Rows[0]["start"].ToString()).ToString(@"dd/MM/yyyy");
                StopDate = Convert.ToDateTime(dt.Rows[0]["stop"].ToString()).ToString(@"dd/MM/yyyy");
                Period = dt.Rows[0]["Month"].ToString();
                VendorName = dt.Rows[0]["name"].ToString();
                VendorCompany = dt.Rows[0]["company"].ToString();
                ClientName = dt.Rows[0]["customername"].ToString();
                ClientComapny = "";

                Term1Pct = "20%";
                Term2Pct = "15%";
                Term3Pct = "10%";
                Term4Pct = "10%";
                Term5Pct = "5%";
                Term6Pct = "5%";
                Term7Pct = "10%";
                Term8Pct = "5%";
                Term9Pct = "15%";
                Term10Pct = "5%";

                Term1Amount = dt.Rows[0]["step01"].ToString();
                Term2Amount = dt.Rows[0]["step02"].ToString();
                Term3Amount = dt.Rows[0]["step03"].ToString();
                Term4Amount = dt.Rows[0]["step04"].ToString();
                Term5Amount = dt.Rows[0]["step05"].ToString();
                Term6Amount = dt.Rows[0]["step06"].ToString();
                Term7Amount = dt.Rows[0]["step07"].ToString();
                Term8Amount = dt.Rows[0]["step08"].ToString();
                Term9Amount = dt.Rows[0]["step09"].ToString();
                Term10Amount = dt.Rows[0]["step10"].ToString();

                Term1Text = ThaiBaht(dt.Rows[0]["step01"].ToString());
                Term2Text = ThaiBaht(dt.Rows[0]["step02"].ToString());
                Term3Text = ThaiBaht(dt.Rows[0]["step03"].ToString());
                Term4Text = ThaiBaht(dt.Rows[0]["step04"].ToString());
                Term5Text = ThaiBaht(dt.Rows[0]["step05"].ToString());
                Term6Text = ThaiBaht(dt.Rows[0]["step06"].ToString());
                Term7Text = ThaiBaht(dt.Rows[0]["step07"].ToString());
                Term8Text = ThaiBaht(dt.Rows[0]["step08"].ToString());
                Term9Text = ThaiBaht(dt.Rows[0]["step09"].ToString());
                Term10Text = ThaiBaht(dt.Rows[0]["step10"].ToString());

                //MaterialAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bMaterial"]), 2).ToString("N0");
                //MaterialPct = dt.Rows[0]["pMaterial"].ToString();
                //ManhourAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bPower"]), 2).ToString("N0");
                //ManhourPct = dt.Rows[0]["pPower"].ToString();
                //OperationAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bBenefit"]), 2).ToString("N0");
                //OperationPct = dt.Rows[0]["pBenefit"].ToString();
            }
        }
    }
}