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
                ddHomeType.Items.Insert(0, new ListItem("--กรุณาเลือก--", ""));
            }

        }


        private void bindHomeGroup()
        {
            string sql_command = " SELECT [hometype],[homegroup] "
                               + " FROM[BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " GROUP BY[hometype],[homegroup] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddHomeGroup.DataSource = dt;
            ddHomeGroup.DataValueField = "hometype";
            ddHomeGroup.DataTextField = "homegroup";
            ddHomeGroup.DataBind();

            ddHomeGroup.Items.Insert(0, new ListItem("--กรุณาเลือกประเภทของงาน--", ""));
        }

        protected void ddHomeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_hometype = ddHomeGroup.SelectedValue.ToString().Trim();
            string sql_command = " SELECT [homeid],[homename] "
                               + " FROM [BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " WHERE [hometype] = '" + param_hometype + "' "
                               + " GROUP BY [homeid],[homename] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddHomeType.DataSource = dt;
            ddHomeType.DataValueField = "homeid";
            ddHomeType.DataTextField = "homename";
            ddHomeType.DataBind();

            ddHomeType.Items.Insert(0, new ListItem("--กรุณาเลือก--", ""));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //Get val form control
                string param_homeid = ddHomeType.SelectedValue.ToString().Trim();

                //Execute 
                string sql_command = "EXEC [set_Project_02_Home] '"
                                   + param_projid + "','"
                                   + param_homeid + "','"
                                   + userID + "' ";
                ClassConfig.GetDataSQL(sql_command);

                //Response.Write("<script>alert('Insert Data 02 Success');</script>");
                Response.Redirect("CreateProject_03_01?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
                //Response.Write("<script>alert('Insert Data 02 Please Contract Admin');</script>");
                throw;
            }


        }
    }
}