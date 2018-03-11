using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            renderSponsor();
        }

        protected void renderSponsor() {
            string sql = "SELECT * FROM [BESTBoQ].[dbo].[CFG_Sponsor] ";
            DataTable dt = ClassConfig.GetDataSQL(sql);
            rpSponsors.DataSource = dt;
            rpSponsors.DataBind();
        }
    }
}