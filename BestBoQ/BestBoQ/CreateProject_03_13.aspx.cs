using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_13 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        public string section_price = "0";

        DataTable dt_old;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack)
            {
                getOldData();
                getSectionPrice();
                bindData();
            }

        }

        protected void bindData()
        {
            //Get data form DB to repeater
            string sql_command = " EXEC [dbo].[get_template_03_13] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
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
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_14?id=" + param_projid);
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }

        private void getSectionPrice()
        {
            string sql_price_command = " [dbo].[get_Last_Price] '" + param_projid + "'";
            DataTable dtPrice = ClassConfig.GetDataSQL(sql_price_command);
            if (dtPrice.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPrice.Rows)
                {
                    section_price = dr[12].ToString();
                }
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],RTRIM([railingType]) AS [railingType],[numMM] FROM [BESTBoQ].[dbo].[Project_03_13_Railing]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_railingType = (string)DataBinder.Eval(e.Item.DataItem, "railingType");
            TextBox tbnumMM = (TextBox)e.Item.FindControl("TextBox1");
            if (dt_old.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_old.Rows)
                {
                    if (dr["railingType"].ToString().Trim() == param_railingType.ToString().Trim())
                    {
                        tbnumMM.Text = dr["numMM"].ToString().Trim();
                    }
                }
            }
        }
    }
}