using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class HomeNestedMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null) {
                Response.Redirect("Default.aspx");
            }
            else
            {
                chkPermission();
            }
        }

        private void chkPermission()
        {
            string sql = "SELECT * FROM [BESTBoQ].[dbo].[Super_User] WHERE [userid] = '"+ Session["UserID"].ToString() + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql);
            if (dt.Rows.Count > 0)
            {
                //Have Permission
                ddApprove.Visible = true;
            }
            else
            {
                //No Permission
                ddApprove.Visible = false;
            }
        }
    }
}