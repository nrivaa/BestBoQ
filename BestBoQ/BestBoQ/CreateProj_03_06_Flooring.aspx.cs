using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_06_Flooring : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }
        }

        protected void bindData()
        {
            string sql_command = " SELECT [flooringType],[cost_mm],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_6_Flooring] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox tbNumMM = (TextBox)item.FindControl("TextBox1");
                Label lbflooringType = (Label)item.FindControl("Label2");
                if (lbflooringType != null)
                {
                    string param_flooringType = lbflooringType.Text.Trim();
                    if (tbNumMM != null && tbNumMM.Text.Trim() != "0")
                    {
                        string param_numMM = tbNumMM.Text.Trim();
                        string sql_command = " EXEC [dbo].[set_Project_03_06_Flooring] "
                                           + " '" + param_projid + "','" + param_flooringType + "','" + param_numMM + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }

    }
}