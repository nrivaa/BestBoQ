using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_09_Plumbing : System.Web.UI.Page
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
            string sql_command = " SELECT [plumbingType],[pipe1],[pipe2],[pipe3],[cost/room],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_9_Plumbing] ";
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
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbplumbingType = (Label)item.FindControl("Label1");
                if (lbplumbingType != null)
                {
                    string param_plumbingType = lbplumbingType.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_09_Plumbing] "
                                           + " '" + param_projid + "','" + param_plumbingType + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}