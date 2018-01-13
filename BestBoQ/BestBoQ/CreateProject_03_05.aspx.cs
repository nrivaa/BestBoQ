using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_05 : System.Web.UI.Page
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
                bindDD();
            }
        }

        protected void bindDD()
        {
            string sql_command = "SELECT [picpath],[wallType],[detail],[cost_mm] FROM [BESTBoQ].[dbo].[CFG_3_5_Wall]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected static double getMM(string param_projID)
        {
            string sql_command = "EXEC [dbo].[get_MM] '" + param_projID + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                return double.Parse((dt.Rows[0][0].ToString()), CultureInfo.InvariantCulture);
            }
            else
            {
                return 0;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string param_MM = getMM(param_projid).ToString();
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                    Label lbWall = (Label)item.FindControl("Label1");
                    if (lbWall != null)
                    {
                        string param_wall = lbWall.Text.Trim();
                        
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_05_Wall] "
                                   + " '" + param_projid + "','" + param_wall + "','" + param_MM + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "OnProgress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_06?id=" + param_projid);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}