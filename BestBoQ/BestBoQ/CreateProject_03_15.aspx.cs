using System;
using System.Collections.Generic;
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

                Response.Redirect("Project_Detail.aspx?id=" + param_projid);

            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
                //Response.Write("<script>alert('Insert Data 01 Please Contract Admin');</script>");
            }

        }
    }
}