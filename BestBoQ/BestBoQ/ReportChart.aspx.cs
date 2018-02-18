using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class ReportChart : System.Web.UI.Page
    {
        public string MaterialPct { get; set; }
        public string MaterialAmount { get; set; }
        public string OperationPct { get; set; }
        public string OperationAmount { get; set; }
        public string ManhourPct { get; set; }
        public string ManhourAmount { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            string projectID = Request["projectID"];
            DataTable dt = ClassConfig.GetDataSQL("EXECUTE [dbo].[get_Report] '<projectID>'".Replace("<projectID>", projectID));

            if (dt.Rows.Count > 0)
            {
                MaterialAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bMaterial"]), 2).ToString("N0");
                MaterialPct = dt.Rows[0]["pMaterial"].ToString();
                ManhourAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bPower"]), 2).ToString("N0");
                ManhourPct = dt.Rows[0]["pPower"].ToString();
                OperationAmount = Math.Round(Convert.ToDouble(dt.Rows[0]["bBenefit"]), 2).ToString("N0");
                OperationPct = dt.Rows[0]["pBenefit"].ToString();
            }
        }
    }
}