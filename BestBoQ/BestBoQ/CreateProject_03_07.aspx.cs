using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_07 : System.Web.UI.Page
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
            string sql_command = " SELECT [toiletType],[cost_room],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_7_Toilet] ";
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
                    TextBox tbNumRoom = (TextBox)item.FindControl("TextBox1");
                    Label lbtoiletType = (Label)item.FindControl("Label1");
                    if (lbtoiletType != null)
                    {
                        string param_toiletType = lbtoiletType.Text.Trim();
                        if (tbNumRoom != null && tbNumRoom.Text.Trim() != "0")
                        {
                            string param_numRoom = tbNumRoom.Text.Trim();
                            string sql_command = " EXEC [dbo].[set_Project_03_07_Toilet] "
                                               + " '" + param_projid + "','" + param_toiletType + "','" + param_numRoom + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "OnProgress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_08?id=" + param_projid);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}