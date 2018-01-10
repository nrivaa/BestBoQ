﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_12 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack)
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
            try
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
                Response.Redirect("CreateProject_03_13?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }
    }
}