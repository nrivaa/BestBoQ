using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_01 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        public string section_price;

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
            string sql_command = " EXEC [dbo].[get_template_03_01] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                Repeater2.DataSource = dt;
                Repeater2.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    TextBox tbPiles = (TextBox)item.FindControl("TextBox1");
                    Label lbFootingType = (Label)item.FindControl("Label1");
                    if (lbFootingType != null)
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
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_02?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT[projectid],[footingType],[numpole] FROM[BESTBoQ].[dbo].[Project_03_01_Footing] WHERE[projectid] = '" + param_projid + "' ";
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
                    section_price = dr[0].ToString();
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_homeid = (string)DataBinder.Eval(e.Item.DataItem, "footingType");
            TextBox tbPiles = (TextBox)e.Item.FindControl("TextBox1");
            if (dt_old.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_old.Rows)
                {
                    if(dr["footingType"].ToString().Trim() == param_homeid.Trim())
                    {
                        tbPiles.Text = dr["numpole"].ToString().Trim();
                    }
                }
            }
        }
    }
}