using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_11 : System.Web.UI.Page
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
                bindDD();
            }

        }

        protected void getOldData()
        {
            string sql_command = "SELECT [projectid], RTRIM([type]) AS [type] FROM [BESTBoQ].[dbo].[Project_03_11_Electric_Type]  WHERE [projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void bindDD()
        {
            string sql_command = " EXEC [dbo].[get_template_03_11] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                RepeaterDetail.DataSource = dt;
                RepeaterDetail.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                    Label lbType = (Label)item.FindControl("Label1");
                    if (lbType != null)
                    {
                        string param_type = lbType.Text.Trim();
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_11_Electric_New] "
                                   + " '" + param_projid + "','" + userID + "','" + param_type + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }

                Response.Redirect("CreateProject_03_12?id=" + param_projid);
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
                    section_price = dr[10].ToString();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_type = (string)DataBinder.Eval(e.Item.DataItem, "type");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            if (dt_old.Rows.Count > 0)
            {
                if (param_type.Trim().ToString() == dt_old.Rows[0]["type"].ToString())
                {
                    rb.Checked = true;
                }
            }
        }

        protected void RepeaterDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Repeater r2 = (Repeater)e.Item.FindControl("Repeater2");
            string param_type = (string)DataBinder.Eval(e.Item.DataItem, "type");

            string sql_command = " EXEC [dbo].[get_template_03_11_Detail] '" + param_projid + "','" + param_type + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                r2.DataSource = dt;
                r2.DataBind();
            }

        }
    }
}