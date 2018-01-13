using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_02 : System.Web.UI.Page
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
            string sql_command = " SELECT [beamType],[cost_m],[weightSupport],[recomment],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_2_Beam] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                Repeater2.DataSource = dt;
                Repeater2.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    TextBox tbNumM = (TextBox)item.FindControl("TextBox1");
                    Label lbBeaType = (Label)item.FindControl("Label1");
                    if (lbBeaType != null)
                    {
                        string param_beamType = lbBeaType.Text.Trim();
                        if (tbNumM != null)
                        {
                            string param_numM = tbNumM.Text.Trim();
                            string sql_command = " EXEC [dbo].[set_Project_03_02_Beam] "
                                               + " '" + param_projid + "','" + param_beamType + "','" + param_numM + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "OnProgress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_03?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}