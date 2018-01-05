using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_13_Railing : System.Web.UI.Page
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
            //Get data form DB to repeater
            string sql_command = " SELECT [railingType],[cost_m],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_13_Railing] ";
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
                TextBox tbM = (TextBox)item.FindControl("TextBox1");
                Label lbrailingType = (Label)item.FindControl("Label2");
                if (lbrailingType != null)
                {
                    string param_railingType = lbrailingType.Text.Trim();
                    if (tbM != null)
                    {
                        string param_numM = tbM.Text.Trim();
                        string sql_command = " EXEC [dbo].[set_Project_03_13_Railing] "
                                           + " '" + param_projid + "','" + param_railingType + "','" + param_numM + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}