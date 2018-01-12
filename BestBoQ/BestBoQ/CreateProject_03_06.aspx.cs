using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_06 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack)
            {
                bindData();
            }

        }

        protected void bindData()
        {
            string sql_command = " SELECT [flooringType],[cost_mm],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_6_Flooring] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    TextBox tbNumMM = (TextBox)item.FindControl("TextBox1");
                    Label lbflooringType = (Label)item.FindControl("Label2");
                    if (lbflooringType != null)
                    {
                        string param_flooringType = lbflooringType.Text.Trim();
                        if (tbNumMM != null && tbNumMM.Text.Trim() != "0")
                        {
                            string param_numMM = tbNumMM.Text.Trim();
                            string sql_command = " EXEC [dbo].[set_Project_03_06_Flooring] "
                                               + " '" + param_projid + "','" + param_flooringType + "','" + param_numMM + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                Response.Redirect("CreateProject_03_07?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}