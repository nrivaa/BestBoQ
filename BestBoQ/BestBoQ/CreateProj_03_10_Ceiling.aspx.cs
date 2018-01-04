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
    public partial class CreateProj_03_10_Ceiling : System.Web.UI.Page
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
            string sql_command_indoor = " SELECT [ceilingType],[ceilingPart],[cost_mm],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_10_Ceiling] WHERE [ceilingType] = 'INDOOR' ";
            DataTable dt_indoor = ClassConfig.GetDataSQL(sql_command_indoor);
            if (dt_indoor.Rows.Count > 0)
            {
                Repeater1.DataSource = dt_indoor;
                Repeater1.DataBind();
            }

            string sql_command_outdoor = " SELECT [ceilingType],[ceilingPart],[cost_mm],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_10_Ceiling] WHERE [ceilingType] = 'OUTDOOR' ";
            DataTable dt_outdoor = ClassConfig.GetDataSQL(sql_command_outdoor);
            if (dt_outdoor.Rows.Count > 0)
            {
                Repeater2.DataSource = dt_outdoor;
                Repeater2.DataBind();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbindoorType = (Label)item.FindControl("Label2");
                if (lbindoorType != null)
                {
                    string param_indoorType = lbindoorType.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_10_Ceiling] "
                                           + " '" + param_projid + "','INDOOR','" + param_indoorType + "','" + getMM3() + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }

            foreach (RepeaterItem item in Repeater2.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton2");
                Label lboutdoorType = (Label)item.FindControl("Label4");
                if (lboutdoorType != null)
                {
                    string param_outdoorType = lboutdoorType.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_10_Ceiling] "
                                           + " '" + param_projid + "','OUTDOOR','" + param_outdoorType + "','" + (double.Parse(getMM3(), CultureInfo.InvariantCulture)*0.45).ToString() + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }

        }

        protected string getMM3()
        {
            string sql_command = "EXEC [dbo].[get_MM3] '"+param_projid+"'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }

        }

    }
}