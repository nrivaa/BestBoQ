using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class ContractCover : System.Web.UI.Page
    {
        public string HomeName { get; set; }
        public string Customer { get; set; }
        public string CompanyName { get; set; }
        public string ContractID { get; set; }
        public string ProjectName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string projectID = Request["projectID"];

            DataTable dt = ClassConfig.GetDataSQL("exec dbo.get_Contract_Info '" + projectID + "'");
            HomeName = dt.Rows[0]["homename"] == null ? "" : dt.Rows[0]["homename"].ToString();
            ProjectName = dt.Rows[0]["projectname"] == null ? "" : dt.Rows[0]["projectname"].ToString();
            Customer = dt.Rows[0]["customername"] == null ? "" : dt.Rows[0]["customername"].ToString();
            CompanyName = "";
            ContractID = dt.Rows[0]["contractid"] == null ? "" : dt.Rows[0]["contractid"].ToString();

        }
    }
}