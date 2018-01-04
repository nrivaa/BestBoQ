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
    public partial class CreateProj_03_05_Wall : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDD();
            }
        }

        protected void bindDD()
        {
            string sql_command = "SELECT [wallType],[detail] FROM [BESTBoQ].[dbo].[CFG_3_5_Wall]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddWall.DataSource = dt;
            ddWall.DataValueField = "wallType";
            ddWall.DataTextField = "detail";
            ddWall.DataBind();

            ddWall.Items.Insert(0, new ListItem("--กรุณาเลือกชนิดผนัง--", "NA"));
        }

        protected static double getMM(string param_projID)
        {
            string sql_command = "EXEC [dbo].[get_MM] '"+ param_projID + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
            {
                return double.Parse((dt.Rows[0][0].ToString()), CultureInfo.InvariantCulture);
            }
            else
            {
                return 0;
            }
            
        }

        protected void ddWall_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_wall = ddWall.SelectedValue.ToString().Trim();
            imgWall.ImageUrl = "images/05Wall/"+ param_wall + ".png";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string param_wall = ddWall.SelectedValue.ToString().Trim();
            string param_MM = getMM(param_projid).ToString();
            string sql_command = " EXEC [dbo].[set_Project_03_05_Wall] "
                               + " '" + param_projid + "','" + param_wall + "','" + param_MM + "','" + userID + "' ";
            ClassConfig.GetDataSQL(sql_command);
        }
    }
}