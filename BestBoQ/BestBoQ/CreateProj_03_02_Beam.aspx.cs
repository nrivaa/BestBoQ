using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_02_Beam : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindData();
            }  
        }

        protected void bindData()
        {
            string sql_command = " SELECT [beamType],[cost_m],[weightSupport],[recomment],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_2_Beam] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                Repeater2.DataSource = dt;
                Repeater2.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox tbNumM = (TextBox)item.FindControl("TextBox1");
                Label lbBeaType = (Label)item.FindControl("Label1");
                if (lbBeaType != null)
                {
                    string param_beamType = lbBeaType.Text.Trim();
                    if (tbNumM != null)
                    {
                        string param_numM = tbNumM.Text.Trim();
                        string sql_command = " EXEC [dbo].[set_Project_03_02_Beam] "
                                           + " '" + param_projid + "','" + param_beamType + "','" + param_numM + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}