using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_08 : System.Web.UI.Page
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


        protected void bindDD()
        {
            string sql_command = " EXEC [dbo].[get_template_03_08] '" + param_projid + "'";
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
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                    Label lbSanitationType = (Label)item.FindControl("Label1");
                    if (lbSanitationType != null)
                    {
                        string param_sanitationType = lbSanitationType.Text.Trim();
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_08_Sanitation] "
                                   + " '" + param_projid + "','" + param_sanitationType + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_09?id=" + param_projid);
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
                    section_price = dr[7].ToString();
                }
            }
        }


        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],RTRIM([sanitationType]) AS [sanitationType] FROM [BESTBoQ].[dbo].[Project_03_08_Sanitation]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_sanitationType = (string)DataBinder.Eval(e.Item.DataItem, "sanitationType");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            if (dt_old.Rows.Count > 0)
            {
                if (param_sanitationType.Trim().ToString() == dt_old.Rows[0]["sanitationType"].ToString())
                {
                    rb.Checked = true;
                }
            }
        }
    }
}