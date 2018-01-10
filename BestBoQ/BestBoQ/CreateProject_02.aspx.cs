using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_02 : System.Web.UI.Page
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
                bindHomeGroup();
            }

        }


        private void bindHomeGroup()
        {
            string sql_command = " SELECT [hometype],[homegroup],[homegrouppic] "
                               + " FROM[BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " GROUP BY[hometype],[homegroup],[homegrouppic] ";
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
                    Label lbHomeId = (Label)item.FindControl("Label1");
                    if (lbHomeId != null)
                    {
                        string param_homeid = lbHomeId.Text.Trim();
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            //Execute 
                            string sql_command = "EXEC [set_Project_02_Home] '"
                                               + param_projid + "','"
                                               + param_homeid + "','"
                                               + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }

                //Response.Write("<script>alert('Insert Data 02 Success');</script>");
                Response.Redirect("CreateProject_03_01?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
                //Response.Write("<script>alert('Insert Data 02 Please Contract Admin');</script>");
            }


        }

        protected void imgPic_Click(object sender, ImageClickEventArgs e)
        {
            RepeaterItem row = (RepeaterItem)((ImageButton)sender).NamingContainer;
            Label lbHomeType = row.FindControl("Label1") as Label;
            if (lbHomeType != null)
            {
                string param_hometype = lbHomeType.Text.Trim();

                string sql_command = " SELECT [homeid],[homename],[homepic] "
                               + " FROM [BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " WHERE [homegroup] = N'" + param_hometype + "' "
                               + " GROUP BY [homeid],[homename],[homepic] ";
                DataTable dt = ClassConfig.GetDataSQL(sql_command);
                if (dt.Rows.Count > 0)
                {
                    Repeater2.DataSource = dt;
                    Repeater2.DataBind();
                }
            }
        }
    }
}