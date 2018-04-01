using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class ContractDocument : System.Web.UI.Page
    {
        public string HomeName { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerProvince { get; set; }
        public string CustomerAddress { get; set; }
        public string ProjectStart { get; set; }
        public string ContractID { get; set; }
        public string Area { get; set; }
        public string Month { get; set; }
        public string TotalPrice { get; set; }

        // No data yet
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CustomerNationalID { get; set; }
        public string TotalPriceTxt { get; set; }
        public string CompanySign { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string projectID = Request["projectID"];
            projectID = "000001";

            DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projectID + "'");
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
        }
    }
}