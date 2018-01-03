using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_04_Roof : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDropdown();
            }
        }

        protected void bindDropdown()
        {
            string sql_command = "SELECT [roofStyle] FROM [BESTBoQ].[dbo].[CFG_3_4_Roof] GROUP BY [roofStyle]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddRoofStyle.DataSource = dt;
            ddRoofStyle.DataTextField = "roofStyle";
            ddRoofStyle.DataValueField = "roofStyle";
            ddRoofStyle.DataBind();

            ddRoofStyle.Items.Insert(0, new ListItem("--กรุณาเลือกประเภทหลังคา--", "NA"));
        }

        protected void ddRoofStype_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_roofStyle = ddRoofStyle.SelectedValue.ToString().Trim();
            string sql_command = " SELECT [roofType],[detail] FROM[BESTBoQ].[dbo].[CFG_3_4_Roof] "
                               + " WHERE[roofStyle] = '"+ param_roofStyle + "' "
                               + " GROUP BY[roofType],[detail] ";
            DataTable dt_type = ClassConfig.GetDataSQL(sql_command);
            ddRoofType.DataSource = dt_type;
            ddRoofType.DataTextField = "detail";
            ddRoofType.DataValueField = "roofType";
            ddRoofType.DataBind();

            ddRoofType.Items.Insert(0, new ListItem("--กรุณาเลือกชนิดหลังคา--", "NA"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string param_roofStyle = ddRoofStyle.SelectedValue.ToString().Trim();
            string param_roofType = ddRoofType.SelectedValue.ToString().Trim();
            string sql_command = " EXEC [dbo].[set_Project_03_04_Roof] "
                               + " '" + param_projid + "','" + param_roofStyle + "','" + param_roofType + "','" + userID + "'";
            ClassConfig.GetDataSQL(sql_command);
        }
    }
}