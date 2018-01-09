using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_08 : System.Web.UI.Page
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
            string sql_command = "SELECT [sanitationType],[cost],[detail],[picpath] FROM [BESTBoQ].[dbo].[CFG_3_8_Sanitation]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddSanitation.DataSource = dt;
            ddSanitation.DataValueField = "sanitationType";
            ddSanitation.DataTextField = "detail";
            ddSanitation.DataBind();

            ddSanitation.Items.Insert(0, new ListItem("--กรุณาเลือกระบบงานสุขาภิบาล--", ""));
        }

        protected void ddSanitation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_sanitationType = ddSanitation.SelectedValue.ToString().Trim();
            imgSanitation.ImageUrl = "images/08Sanitation/" + param_sanitationType + ".png";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string param_sanitationType = ddSanitation.SelectedValue.ToString().Trim();
                string sql_command = " EXEC [dbo].[set_Project_03_08_Sanitation] "
                                   + " '" + param_projid + "','" + param_sanitationType + "','" + userID + "' ";
                ClassConfig.GetDataSQL(sql_command);
                Response.Redirect("CreateProject_03_09?id=" + param_projid);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}