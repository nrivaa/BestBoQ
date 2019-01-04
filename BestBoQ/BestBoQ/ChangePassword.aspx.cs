using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Username"] == null)
            //{
            //    Response.Redirect("Home.aspx");
            //}
        }

        protected void btnReNew_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_username = tbCuser.Text.Trim();
            string param_old = tbCold.Text.Trim();
            string param_new = tbCnew.Text.Trim();
            string param_confirm = tbCcon.Text.Trim();
            string param_password;

            string sql_chk = "SELECT * FROM [BESTBoQ].[dbo].[userinfo] WHERE [username] = '" + param_username 
                           + "' AND [password] = N'"+ ClassConfig.CalculateMD5Hash(param_old + "AISNQM") + "'  ";
            DataTable dt_chk = ClassConfig.GetDataSQL(sql_chk);

            if(dt_chk.Rows.Count >0)
            {
                string user_id = dt_chk.Rows[0]["userid"].ToString();

                if (param_new == param_confirm)
                {
                    string sql_delete_flag = "DELETE [BESTBoQ].[dbo].[FlagNew] WHERE [userid] = '" + user_id + "'";
                    ClassConfig.GetDataSQL(sql_delete_flag);

                    param_password = ClassConfig.CalculateMD5Hash(param_new + "AISNQM");
                    string sql_command = " UPDATE [BESTBoQ].[dbo].[userinfo]  "
                                       + " SET [password] = '" + param_password + "' "
                                       + " WHERE [username] = N'" + param_username
                                       + "' AND [password] = N'" + ClassConfig.CalculateMD5Hash(tbCold.Text.Trim() + "AISNQM") + "' ";
                    DataTable dt = ClassConfig.GetDataSQL(sql_command);

                    Response.Redirect("Default?r=forgetComplete");
                }
            }
        }
    }
}