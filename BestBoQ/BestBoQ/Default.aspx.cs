using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_username = tbUsername.Text.Trim();
            string param_password = ClassConfig.CalculateMD5Hash(tbPassword.Text.Trim() + "AISNQM");

            //Execute Command
            try
            {
                string sql_command = " SELECT [status] FROM [BESTBoQ].[dbo].[userinfo] "
                               + " WHERE [username] = '" + param_username + "' AND [password] = '" + param_password + "' ";
                DataTable dt = ClassConfig.GetDataSQL(sql_command);

                if (dt.Rows.Count < 1)
                {
                    //No Data in userinfo
                    Response.Write("<script>alert('กรุณาลงทะเบียนก่อนเข้าใช้งาน');</script>");
                }
                else
                {
                    if (dt.Rows[0][0].ToString() != "Approve")
                    {
                        //Not Yet Approve
                        Response.Write("<script>alert('username ของท่านยังไม่ได้รับการอนุมัติ กรุณาติดต่อผู้ดูแล');</script>");
                    }
                    else
                    {
                        Session["Username"] = param_username;
                        Session.Timeout = 24 * 60;

                        Response.Redirect("Default.aspx");
                    }
                }
            }
            catch (Exception)
            {
                //SQL Error or Network Error
                Response.Write("<script>alert('ระบบมีปัญหา กรุณาติดต่อผู้ดูแล');</script>");
                throw;
            }
        }
    }
}