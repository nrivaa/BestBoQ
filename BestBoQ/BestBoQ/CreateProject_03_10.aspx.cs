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
    public partial class CreateProject_03_10 : System.Web.UI.Page
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
            string sql_command_indoor = " EXEC [dbo].[get_template_03_10] '" + param_projid + "','INDOOR'";
            DataTable dt_indoor = ClassConfig.GetDataSQL(sql_command_indoor);
            if (dt_indoor.Rows.Count > 0)
            {
                Repeater1.DataSource = dt_indoor;
                Repeater1.DataBind();
            }

            string sql_command_outdoor = " EXEC [dbo].[get_template_03_10] '" + param_projid + "','OUTDOOR'";
            DataTable dt_outdoor = ClassConfig.GetDataSQL(sql_command_outdoor);
            if (dt_outdoor.Rows.Count > 0)
            {
                Repeater2.DataSource = dt_outdoor;
                Repeater2.DataBind();
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
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
                                               + " '" + param_projid + "','OUTDOOR','" + param_outdoorType + "','" + (double.Parse(getMM3(), CultureInfo.InvariantCulture) * 0.45).ToString() + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_11?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }

        protected string getMM3()
        {
            string sql_command = "EXEC [dbo].[get_MM3] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0][0].ToString();
            }
            else
            {
                return "0";
            }

        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[ceilingType],[ceilingPart] FROM [BESTBoQ].[dbo].[Project_03_10_Ceiling]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        private void getSectionPrice()
        {
            string sql_price_command = " [dbo].[get_Last_Price] '" + param_projid + "'";
            DataTable dtPrice = ClassConfig.GetDataSQL(sql_price_command);
            if (dtPrice.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPrice.Rows)
                {
                    section_price = dr[9].ToString();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_ceilingType = (string)DataBinder.Eval(e.Item.DataItem, "ceilingType");
            string param_ceilingPart = (string)DataBinder.Eval(e.Item.DataItem, "ceilingPart");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            if (dt_old.Rows.Count > 0)
            {
                foreach(DataRow dr in dt_old.Rows)
                {
                    if (param_ceilingType.ToString() == dr["ceilingType"].ToString() && param_ceilingPart.ToString() == dr["ceilingPart"].ToString())
                    {
                        rb.Checked = true;
                    }
                }
                
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_ceilingType = (string)DataBinder.Eval(e.Item.DataItem, "ceilingType");
            string param_ceilingPart = (string)DataBinder.Eval(e.Item.DataItem, "ceilingPart");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton2");
            if (dt_old.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_old.Rows)
                {
                    if (param_ceilingType.ToString() == dr["ceilingType"].ToString() && param_ceilingPart.ToString() == dr["ceilingPart"].ToString())
                    {
                        rb.Checked = true;
                    }
                }

            }
        }
    }
}