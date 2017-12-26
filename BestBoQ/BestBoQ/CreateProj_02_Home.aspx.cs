using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_02_Home : System.Web.UI.Page
    {
        string projectID = "000001";
        string userID = "967882";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindHomeGroup();
                ddHomeType.Items.Insert(0, new ListItem("--กรุณาเลือก--", "NA"));
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

            ddHomeGroup.Items.Insert(0, new ListItem("--กรุณาเลือกประเภทของงาน--", "NA"));
        }

        protected void ddHomeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string param_hometype = ddHomeGroup.SelectedValue.ToString().Trim();
            string sql_command = " SELECT [homeid],[homename] "
                               + " FROM [BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " WHERE [hometype] = '"+ param_hometype + "' "
                               + " GROUP BY [homeid],[homename] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddHomeType.DataSource = dt;
            ddHomeType.DataValueField = "homeid";
            ddHomeType.DataTextField = "homename";
            ddHomeType.DataBind();

            ddHomeType.Items.Insert(0, new ListItem("--กรุณาเลือก--", "NA"));
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //Get val form control
                string param_homeid = ddHomeType.SelectedValue.ToString().Trim();

                //Execute 
                string sql_command = "EXEC [sp_set_Project_02_Home] '"
                                   + projectID + "','"
                                   + param_homeid + "','"
                                   + userID + "' ";
                ClassConfig.GetDataSQL(sql_command);

                Response.Write("<script>alert('Insert Data 02 Success');</script>");
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Insert Data 02 Please Contract Admin');</script>");
                throw;
            }
            
            
        }
    }
}