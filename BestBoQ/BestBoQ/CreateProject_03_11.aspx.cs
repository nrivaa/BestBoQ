using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_11 : System.Web.UI.Page
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
            string sql_command = " SELECT [itemNo],[detail],[formula],[cost],[unit],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_11_Electric] ";
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
                    TextBox tbPoint = (TextBox)item.FindControl("TextBox1");
                    Label lbitemNo = (Label)item.FindControl("Label1");
                    if (lbitemNo != null)
                    {
                        string param_point = tbPoint.Text.Trim();
                        string param_itemNo = lbitemNo.Text.Trim();
                        if (tbPoint != null)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_11_Electric] "
                                               + " '" + param_projid + "','" + param_itemNo + "','" + param_point + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }

                Response.Redirect("CreateProject_03_12?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}