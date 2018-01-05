﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_01_Footing : System.Web.UI.Page
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
            string sql_command = " SELECT [footingType],[cost_pole],[weightSupport],[recomment],[picpath] " 
                               + " FROM[BESTBoQ].[dbo].[CFG_3_1_Footing] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
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
                TextBox tbPiles = (TextBox)item.FindControl("TextBox1");
                Label lbFootingType = (Label)item.FindControl("Label1");
                if(lbFootingType != null)
                {
                    string param_footingType = lbFootingType.Text.Trim();
                    if (tbPiles != null)
                    {
                        string param_numPole = tbPiles.Text.Trim();
                        string sql_command = " EXEC [dbo].[set_Project_03_01_Footing] "
                                           + " '" + param_projid + "','" + param_footingType + "','" + param_numPole + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}