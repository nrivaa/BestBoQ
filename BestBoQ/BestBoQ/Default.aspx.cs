using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;

namespace BestBoQ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Username"] != null)
            {
                Response.Redirect("Home.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_username = tbLoginUsername.Text.Trim();
            string param_password = ClassConfig.CalculateMD5Hash(tbLoginPassword.Text.Trim() + "AISNQM");

            //Execute Command
            try
            {
                string sql_command = "SELECT [status1],[status2],[status3],[status4],[status5],[status6],[userid] FROM [BESTBoQ].[dbo].[userinfo] "
                               + " WHERE [username] = '" + param_username + "' AND [password] = '" + param_password + "' ";
                DataTable dt = ClassConfig.GetDataSQL(sql_command);

                if (dt.Rows.Count < 1)
                {
                    //No Data in userinfo
                    Response.Redirect("Default?r=loginPermission");
                    //Response.Write("<script>alert('กรุณาลงทะเบียนก่อนเข้าใช้งาน');</script>");
                }
                else
                {
                    if (dt.Rows[0][0].ToString() != "true")
                    {
                        //Not Yet Approve
                        Response.Redirect("Default?r=userPermission");
                        //Response.Write("<script>alert('username ของท่านยังไม่ได้รับการอนุมัติ กรุณาติดต่อผู้ดูแล');</script>");
                    }
                    else
                    {
                        Session["Username"] = param_username;
                        Session["UserID"] = dt.Rows[0][6].ToString();
                        Session.Timeout = 24 * 60;

                        string sqlChk = "SELECT * FROM [BESTBoQ].[dbo].[FlagNew] WHERE [userid] = '"+ Session["UserID"].ToString() + "'";
                        DataTable dtFlag = ClassConfig.GetDataSQL(sqlChk);

                        if(dtFlag.Rows.Count > 0)
                        {
                            Response.Redirect("ChangePassword.aspx",false);
                        }
                        else
                        {
                            Response.Redirect("Home.aspx");
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                //SQL Error or Network Error
                Response.Write("<script>alert('ระบบมีปัญหา กรุณาติดต่อผู้ดูแล');</script>");
            }
        }


        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //Check password == conform password
            if (tbPassword.Value.Trim() != tbRepassword.Value.Trim())
            {
                Response.Write("<script>alert('Password <> Confirm Password');</script>");
            }
            else
            {
                //Check Username ว่ามีแล้วหรือยังใน DB
                if (chkusername())
                {
                    //Get Val form control
                    string param_username = tbUsername.Text.Trim();
                    string param_password = ClassConfig.CalculateMD5Hash(tbPassword.Value.Trim() + "AISNQM");
                    string param_type = rbType.SelectedValue.ToString().Trim();
                    string param_email = tbEmail.Text.Trim();
                    string param_mobile = tbMobile.Text.Trim();
                    string param_name = tbName.Text.Trim();
                    string param_company = tbCompany.Text.Trim();
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
                                           + param_company + "',N'"
                                           + param_address + "',N'"
                                           + param_id + "',N'"
                                           + param_tax + "',N'"
                                           + param_alias + "' ";
                        ClassConfig.GetDataSQL(param_command);

                        Response.Redirect("Default?r=regisComplete");
                        //Response.Write("<script>alert('Register Success');</script>");
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
            if (dt.Rows.Count < 1)
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