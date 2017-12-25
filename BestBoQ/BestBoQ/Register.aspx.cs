using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //Check password == conform password
            if(tbPassword.Text.Trim() != tbRepassword.Text.Trim())
            {
                Response.Write("<script>alert('Password <> Confirm Password');</script>");
            }
            else 
            {
                //Check Username ว่ามีแล้วหรือยังใน DB
                if(chkusername())
                {
                    //Get Val form control
                    string param_username = tbUsername.Text.Trim();
                    string param_password = ClassConfig.CalculateMD5Hash(tbPassword.Text.Trim() + "AISNQM");
                    string param_type = rbType.SelectedValue.ToString().Trim();
                    string param_email = tbEmail.Text.Trim();
                    string param_mobile = tbMobile.Text.Trim();
                    string param_name = tbName.Text.Trim();
                    string param_alias = tbAlias.Text.Trim();
                    string param_address = tbAddress.Text.Trim();
                    string param_id = tbID.Text.Trim();
                    string param_tax = tbTax.Text.Trim();

                    //Debug Val form control
                    //Response.Write("<script>alert('Username : " + param_type + "');</script>");

                    //Execute Command
                    try
                    {
                        string param_command = " EXEC [dbo].[set_Register] N'"
                                           + param_username + "',N'"
                                           + param_password + "',N'"
                                           + param_type + "',N'"
                                           + param_email + "',N'"
                                           + param_mobile + "',N'"
                                           + param_name + "',N'"
                                           + param_address + "',N'"
                                           + param_id + "',N'"
                                           + param_tax + "',N'"
                                           + param_alias + "' ";
                        ClassConfig.GetDataSQL(param_command);

                        Response.Write("<script>alert('Register Success');</script>");
                    }
                    catch (Exception)
                    {
                        Response.Write("<script>alert('Register Fail Please Contract Admin');</script>");
                        throw;
                    }
                }
                else
                {
                    Response.Write("<script>alert('มี Username นี้อยู่ในระบบแล้ว');</script>");
                }
                
            } 
        }

        private bool chkusername()
        {
            string param_username = tbUsername.Text.Trim();
            string sql_command = " SELECT * FROM [BESTBoQ].[dbo].[userinfo] "
                               + " WHERE[username] = '" + param_username + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count < 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }
}