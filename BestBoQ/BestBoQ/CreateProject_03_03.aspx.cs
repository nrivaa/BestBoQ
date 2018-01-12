using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_03 : System.Web.UI.Page
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
            string sql_command = "SELECT [floorType],[cost_mm],[detail],[picpath],[visible] FROM[BESTBoQ].[dbo].[CFG_3_3_Floor]";
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
                    TextBox tbNumMM = (TextBox)item.FindControl("TextBox2");
                    Label lbFloorType = (Label)item.FindControl("Label1");
                    if (lbFloorType != null)
                    {
                        string param_floorType = lbFloorType.Text.Trim();
                        string param_numRoom = string.Empty;
                        if (tbNumRoom != null)
                        {
                            param_numRoom = tbNumRoom.Text.Trim();
                        }
                        else
                        {
                            param_numRoom = "0";
                        }
                        if (tbNumMM != null)
                        {
                            string param_numMM = tbNumMM.Text.Trim();
                            string sql_command = " EXEC [dbo].[set_Project_03_03_Floor] "
                                               + " '" + param_projid + "','" + param_floorType + "','" + param_numMM + "','" + param_numRoom + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                Response.Redirect("CreateProject_03_04?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}