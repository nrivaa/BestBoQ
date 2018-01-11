using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Home : System.Web.UI.Page
    {
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userID = Session["UserID"].ToString();
                getData();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            
        }

        protected void getData()
        {
            string sql = "[dbo].[get_Home_Proj] '" + userID + "','%'";
            DataTable dt = new DataTable();
            dt = ClassConfig.GetDataSQL(sql);
            gvData.DataSource = dt;
            gvData.DataBind();

            if (dt.Rows.Count > 0)
            {
                gvData.UseAccessibleHeader = true;
                gvData.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}