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
            string sql_command = " SELECT [homename] "
                               + " FROM [BESTBoQ].[dbo].[CFG_Home_Type] "
                               + " WHERE [hometype] = '"+ param_hometype + "' "
                               + " GROUP BY [homename] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddHomeType.DataSource = dt;
            ddHomeType.DataValueField = "homename";
            ddHomeType.DataTextField = "homename";
            ddHomeType.DataBind();

            ddHomeType.Items.Insert(0, new ListItem("--กรุณาเลือก--", "NA"));
        }
    }
}