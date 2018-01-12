using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_13 : System.Web.UI.Page
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
            //Get data form DB to repeater
            string sql_command = " SELECT [railingType],[cost_m],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_13_Railing] ";
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
                    TextBox tbM = (TextBox)item.FindControl("TextBox1");
                    Label lbrailingType = (Label)item.FindControl("Label2");
                    if (lbrailingType != null)
                    {
                        string param_railingType = lbrailingType.Text.Trim();
                        if (tbM != null)
                        {
                            string param_numM = tbM.Text.Trim();
                            string sql_command = " EXEC [dbo].[set_Project_03_13_Railing] "
                                               + " '" + param_projid + "','" + param_railingType + "','" + param_numM + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                Response.Redirect("CreateProject_03_14?id=" + param_projid);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}