using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

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

                        //log data to database
                        string dqlLog = " INSERT INTO [BESTBoQ].[dbo].[Log_Usage] ( [user],[login_date] ) " +
                                        " VALUES(N'"+ param_username + "', GETDATE())";
                        ClassConfig.GetDataSQL(dqlLog);

                        //log data to database
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
                    string param_nation = tbNation.Text.Trim();
                    string param_filename = string.Empty;

                    //Debug Val form control
                    //Response.Write("<script>alert('Username : " + param_type + "');</script>");

                    //Execute Command
                    try
                    {

                        //if (FuLogoImageCompany.HasFile)
                        //{
                        //    try
                        //    {
                        //        string fileuploadDir = Server.MapPath("~/Images/Logo/" + param_username + "/");

                        //        if (!System.IO.Directory.Exists(fileuploadDir))
                        //        {
                        //            System.IO.Directory.CreateDirectory(fileuploadDir);
                        //        }

                        //        string filename = Path.GetFileName(FuLogoImageCompany.FileName);
                        //        FuLogoImageCompany.SaveAs(Server.MapPath("~/Images/Logo/" + param_username + "/") + filename);
                        //        param_filename = "Images/Logo/" + param_username + "/" + filename;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Response.Write("<script>alert('การอัพโหลดรูปภาพมีปัญหา กรุณาตรวจสอบไฟล์ภาพและเลือกภาพใหม่อีกครั้ง');</script>");
                        //    }
                        //}

                        if (FuLogoImageCompany.HasFile)
                        {
                            try
                            {
                                string fn = System.IO.Path.GetFileName(FuLogoImageCompany.PostedFile.FileName);
                                string fileuploadDir = Server.MapPath("~/Images/Logo/" + Session["Username"] + "/");
                                string fileuploadDirFilename = Server.MapPath("~/Images/Logo/" + Session["Username"] + "/" + fn);

                                string fileExtention = FuLogoImageCompany.PostedFile.ContentType;
                                int fileLenght = FuLogoImageCompany.PostedFile.ContentLength;

                                param_filename = "Images/Logo/" + Session["Username"] + "/" + fn;

                                if (fileExtention == "image/png" || fileExtention == "image/jpeg" || fileExtention == "image/x-png")
                                {
                                    if (!System.IO.Directory.Exists(fileuploadDir))
                                    {
                                        System.IO.Directory.CreateDirectory(fileuploadDir);
                                    }

                                    System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(FuLogoImageCompany.PostedFile.InputStream);
                                    System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 57);

                                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                                    System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                                    myEncoderParameters.Param[0] = myEncoderParameter;

                                    objImage.Save(fileuploadDirFilename, jpgEncoder, myEncoderParameters);
                                }
                                else
                                {
                                    Response.Write("<script>alert('กรุณาอัพโหลดได้เฉพาะไฟล์สกุล .jpg, .jpeg, .png เท่านั้น');</script>");
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Write(ex.Message.ToString());
                                Response.Write("<script>alert('การอัพโหลดรูปภาพมีปัญหา กรุณาตรวจสอบไฟล์ภาพและเลือกภาพใหม่อีกครั้ง');</script>");
                            }
                        }

                        string param_command = " EXEC [dbo].[set_Register_Logo] N'"
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
                                           + param_alias + "',N'"
                                           + param_nation + "',N'"
                                           + param_filename + "' ";
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

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            newImage.SetResolution(360, 360);
            using (var g = Graphics.FromImage(newImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
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