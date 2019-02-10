﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject : System.Web.UI.Page
    {
        string userID;
        string param_projid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack)
            {
                bindProjectType();
                bindCountry();
                ddProvince.Items.Insert(0, new ListItem("กรุณาเลือกจังหวัด", ""));
                getOldData();
            }
        }

        protected void bindProjectType()
        {
            string sql_command = "SELECT [homegroup] FROM [BESTBoQ].[dbo].[CFG_Home_Type] GROUP BY [homegroup]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddProjectType.DataSource = dt;
            ddProjectType.DataTextField = "homegroup";
            ddProjectType.DataValueField = "homegroup";
            ddProjectType.DataBind();

            ddProjectType.Items.Insert(0, new ListItem("กรุณาเลือกประเภทของโครงการ", ""));
        }

        protected void bindCountry()
        {
            string sql_command = "SELECT [country] FROM [BESTBoQ].[dbo].[CFG_Province] GROUP BY [country]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddCountry.DataSource = dt;
            ddCountry.DataTextField = "country";
            ddCountry.DataValueField = "country";
            ddCountry.DataBind();

            ddCountry.Items.Insert(0, new ListItem("กรุณาเลือกประเทศ", ""));
        }

        protected void ddCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_country = ddCountry.SelectedValue.ToString().Trim();
            string sql_command = "SELECT [province] FROM [BESTBoQ].[dbo].[CFG_Province] WHERE [country] = N'" + param_country + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddProvince.DataSource = dt;
            ddProvince.DataTextField = "province";
            ddProvince.DataValueField = "province";
            ddProvince.DataBind();

            ddProvince.Items.Insert(0, new ListItem("กรุณาเลือกจังหวัด", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_projectname = tbProjectName.Text.Trim();
            string param_projecttype = ddProjectType.SelectedValue.ToString().Trim();
            string param_customer = tbCustomerName.Text.Trim();
            string[] param_time = tbStartProject.Text.Trim().Split('/');
            string param_startproject = param_time[2] + param_time[1] + param_time[0];
            string param_country = ddCountry.SelectedValue.ToString().Trim();
            string param_province = ddProvince.SelectedValue.ToString().Trim();
            string param_address = tbAddress.Text.Trim();
            string param_spec = rbCostFunction.SelectedValue.ToString().Trim();
            string param_phoneCus = tbTelephoneCus.Text.Trim();
            string param_emailCus = tbEmailCus.Text.Trim();
            string param_idCus = tbCusID.Text.Trim();
            string[] param_birth = tbBirthdate.Text.Trim().Split('/');
            string param_birthDate  = param_birth[2] + param_birth[1] + param_birth[0];

            //Execute Command
            try
            {
                string param_command = " EXEC [dbo].[set_Project_01_Desc] N'"
                                   + param_projid + "',N'"
                                   + param_projectname + "',N'"
                                   + param_customer + "',N'"
                                   + param_projecttype + "',N'"
                                   + param_country + "',N'"
                                   + param_province + "',N'"
                                   + param_address + "', '"
                                   + param_startproject + "',N'"
                                   + userID + "' ,N'"
                                   + param_phoneCus + "' ,N'"
                                   + param_emailCus + "' ,'"
                                   + param_birthDate + "' ,N'"
                                   + param_idCus + "' ";
                DataTable dtResult = ClassConfig.GetDataSQL(param_command);

                if (dtResult.Rows.Count > 0)
                { 
                    string id = dtResult.Rows[0]["projectid"].ToString();

                    //Update Status
                    ClassConfig.UpdateStatus(id, "On Progress", userID);

                   

                    param_command = "EXEC [dbo].[set_Project_Spec] '"
                                    + id + "','"
                                    + param_spec + "','"
                                    + userID + "' ";
                    dtResult = ClassConfig.GetDataSQL(param_command);

                    Response.Redirect("CreateProject_02?id=" + id);
                }

                //Response.Write("<script>alert('Insert Data 01 Success');</script>");
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
                //Response.Write("<script>alert('Insert Data 01 Please Contract Admin');</script>");
            }

        }


        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[projectname],[customername],[projecttype] "
                               + " ,[country],[province],[address],[projectstart]"
                               + " ,[contractid],[userid],[transdate],[customerphone],[customeremail] " 
                               + " ,[birthdate], [customerid] "
                               + " FROM [BESTBoQ].[dbo].[Project_01_Desc] WHERE [projectid] = '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
            {
                tbProjectName.Text = dt.Rows[0]["projectname"].ToString().Trim();
                tbCustomerName.Text = dt.Rows[0]["customername"].ToString().Trim();
                ddProjectType.SelectedValue = dt.Rows[0]["projecttype"].ToString().Trim();
                ddCountry.SelectedValue = dt.Rows[0]["country"].ToString().Trim();

                string param_country = dt.Rows[0]["country"].ToString().Trim();
                string sql = "SELECT [province] FROM [BESTBoQ].[dbo].[CFG_Province] WHERE [country] = N'" + param_country + "'";
                DataTable dt1 = ClassConfig.GetDataSQL(sql);
                ddProvince.DataSource = dt1;
                ddProvince.DataTextField = "province";
                ddProvince.DataValueField = "province";
                ddProvince.DataBind();

                ddProvince.SelectedValue = dt.Rows[0]["province"].ToString().Trim();
                tbAddress.Text = dt.Rows[0]["address"].ToString().Trim();
                tbStartProject.Text = dt.Rows[0]["projectstart"].ToString().Trim();

                tbTelephoneCus.Text = dt.Rows[0]["customerphone"].ToString().Trim();
                tbEmailCus.Text = dt.Rows[0]["customeremail"].ToString().Trim();

                tbBirthdate.Text = dt.Rows[0]["birthdate"].ToString().Trim();
                tbCusID.Text = dt.Rows[0]["customerid"].ToString().Trim();
            }


            sql_command = "SELECT [spec] FROM [BESTBoQ].[dbo].[Project_Spec] WHERE [projectid] = '" + param_projid + "'";
            dt = ClassConfig.GetDataSQL(sql_command);

            rbCostFunction.SelectedValue = "";

            if (dt.Rows.Count > 0)
            {
                string spec = dt.Rows[0]["spec"].ToString().ToLower().Trim();
                if (spec.Equals("min") || spec.Equals("max"))
                {
                    rbCostFunction.SelectedValue = spec;
                }
            }
        }

    }
}