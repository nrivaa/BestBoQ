using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class FrogetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lbResult.Visible = false;
            }
        }

        protected void btnForget_Click(object sender, EventArgs e)
        {
            string param_username = tbFuser.Text.Trim();
            string param_email = tbFemail.Text.Trim();
            string param_id = tbFidcard.Text.Trim();

            string sql_check = " SELECT * FROM [BESTBoQ].[dbo].[userinfo] "
                             + " WHERE [username] = '" + param_username + "' AND [email] = '" + param_email + "' AND ([idcard] = '" + param_id + "' OR [taxname] = '" + param_id + "')";
            DataTable dt = ClassConfig.GetDataSQL(sql_check);
            if (dt.Rows.Count > 0)
            {
                tbFpassword.Visible = true;
                tbFcpassword.Visible = true;
                btnForget.Visible = false;
                btnReNew.Visible = true;
                lbResult.Visible = false;
                lbResult.Text = "";
            }
            else
            {
                //Response.Redirect("FrogetPassword?r=forgetFail");
                tbFpassword.Visible = false;
                tbFcpassword.Visible = false;
                btnForget.Visible = true;
                btnReNew.Visible = false;
                lbResult.Visible = true;
                lbResult.Text = "Information ที่ท่านใส่มาไม่ถูกต้อง";
            }
        }

        protected void btnReNew_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_username = tbFuser.Text.Trim();
            string param_email = tbFemail.Text.Trim();
            string param_id = tbFidcard.Text.Trim();
            string param_password = ClassConfig.CalculateMD5Hash(tbFcpassword.Text.Trim() + "AISNQM");

            //Execute Command
            try
            {
                string sql_command = " UPDATE [BESTBoQ].[dbo].[userinfo]  "
                                   + " SET [password] = '" + param_password + "' "
                                   + " WHERE [username] = '" + param_username + "' AND [email] = '" + param_email + "' AND ([idcard] = '" + param_id + "' OR [taxname] = '" + param_id + "')";
                DataTable dt = ClassConfig.GetDataSQL(sql_command);

                Response.Redirect("Default?r=forgetComplete");

            }
            catch
            {

            }
        }
    }
}