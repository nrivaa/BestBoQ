using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_15_Money : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_free = tbfree.Text.Trim();
            string param_promotion = tbpromotion.Text.Trim();
            string param_other = tbother.Text.Trim();
            string param_detail = tbdetail.Text.Trim();

            string sql_command = " EXEC [dbo].[set_Project_03_15_Money] "
                               + " '" + param_projid + "','" + param_free + "','" + param_promotion + "','" + param_other + "',N'" + param_detail + "','" + userID + "' ";
            ClassConfig.GetDataSQL(sql_command);

        }
    }
}