using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_08_Sanitation : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
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

            ddSanitation.Items.Insert(0, new ListItem("--กรุณาเลือกระบบงานสุขาภิบาล--", "NA"));
        }

        protected void ddSanitation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_sanitationType = ddSanitation.SelectedValue.ToString().Trim();
            imgSanitation.ImageUrl = "images/08Sanitation/" + param_sanitationType + ".png";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string param_sanitationType = ddSanitation.SelectedValue.ToString().Trim();
            string sql_command = " EXEC [dbo].[set_Project_03_08_Sanitation] "
                               + " '" + param_projid + "','" + param_sanitationType + "','" + userID + "' ";
            ClassConfig.GetDataSQL(sql_command);
        }
    }
}