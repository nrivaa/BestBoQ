using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_15 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack) {
                getOldData();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                //Get val form control
                string param_free = tbfree.Text.Trim();
                string param_promotion = tbpromotion.Text.Trim();
                string param_other = tbother.Text.Trim();
                string param_detail = tbdetail.Text.Trim();

                string sql_command = " EXEC [dbo].[set_Project_03_15_Money] "
                                   + " '" + param_projid + "','" + param_free + "','" + param_promotion + "','" + param_other + "',N'" + param_detail + "','" + userID + "' ";
                ClassConfig.GetDataSQL(sql_command);

                //Update Status
                ClassConfig.UpdateStatus(param_projid, "Complete", userID);

                //Redirect
                Response.Redirect("Project_Detail.aspx?id=" + param_projid);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
                //Response.Write("<script>alert('Insert Data 01 Please Contract Admin');</script>");
            }

        }

        protected void getOldData()
        {
            string sql_command = "SELECT [projectid],[pct_free],[pct_promotion],[bht_other],[cost],[detail_other] FROM [BESTBoQ].[dbo].[Project_03_15_Money] WHERE [projectid] = '"+param_projid+"'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
            {
                tbfree.Text = dt.Rows[0]["pct_free"].ToString();
                tbpromotion.Text = dt.Rows[0]["pct_promotion"].ToString();
                tbother.Text = dt.Rows[0]["bht_other"].ToString();
                tbdetail.Text = dt.Rows[0]["detail_other"].ToString();
            }
        }

    }
}