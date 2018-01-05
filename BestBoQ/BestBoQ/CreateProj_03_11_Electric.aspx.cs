using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_11_Electric : System.Web.UI.Page
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
        }
    }
}