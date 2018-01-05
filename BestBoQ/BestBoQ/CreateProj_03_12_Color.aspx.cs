using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_12_Color : System.Web.UI.Page
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
            //Get data form DB to repeater
            string sql_command = " SELECT [colorID],[colorGrade],[cost_mm],[detail],[picpath] "
                                      + " FROM [BESTBoQ].[dbo].[CFG_3_12_Color] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

            //Get data MM to show
            string sql_getMM = "EXEC [dbo].[get_MM12] '" + param_projid + "'";
            DataTable dt_MM = ClassConfig.GetDataSQL(sql_getMM);
            if (dt_MM.Rows.Count > 0)
            {
                lb1.Text = "รวมแนวตั้ง :  ผนังภายใน+ภายนอก = " + dt_MM.Rows[0]["MM1"].ToString().Trim();
                lb2.Text = "รวมแนวนอน : ฝ้าภายใน+ภายนอก = " + dt_MM.Rows[0]["MM2"].ToString().Trim();
                lb3.Text = "อื่นๆ (คาน+ไม่ฝ้าปั้นลม+ไม้เชิงชาย) = " + dt_MM.Rows[0]["MM3"].ToString().Trim();
                lbTotal.Text = "ตรม.รวม = " + dt_MM.Rows[0]["MMTotal"].ToString().Trim();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbcolorID = (Label)item.FindControl("Label1");
                if (lbcolorID != null)
                {
                    string param_colorID = lbcolorID.Text.Trim();
                    string param_MMTotal = lbTotal.Text.Trim().Replace("ตรม.รวม = ", "");
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_12_Color] "
                                           + " '" + param_projid + "','" + param_colorID + "','" + param_MMTotal + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}