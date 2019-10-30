using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getOldData();
            }
            
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            changeControl("Edit");
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }

        protected void btnComfirm_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_email = tbEmail.Text.Trim();
            string param_mobile = tbMobile.Text.Trim();
            string param_name = tbName.Text.Trim();
            string param_address = tbAddress.Text.Trim();
            string param_id = tbId.Text.Trim();
            string param_tax = tbTax.Text.Trim();
            string param_national = tbNational.Text.Trim();
            string param_filename = string.Empty;

            //Execute Command
            try
            {
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
                            System.Drawing.Bitmap objImage = ScaleImage(bmpPostedImage, 70);


                            //ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            //System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            //EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            //EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                            //myEncoderParameters.Param[0] = myEncoderParameter;

                            objImage.Save(fileuploadDirFilename, ImageFormat.Jpeg);
                        }
                        else {
                            Response.Write("<script>alert('กรุณาอัพโหลดได้เฉพาะไฟล์สกุล .jpg, .jpeg, .png เท่านั้น');</script>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message.ToString());
                        Response.Write("<script>alert('การอัพโหลดรูปภาพมีปัญหา กรุณาตรวจสอบไฟล์ภาพและเลือกภาพใหม่อีกครั้ง');</script>");
                    }
                }

                string sql_command = "EXEC [dbo].[set_Update_Register_Logo] N'"
                                        + Session["UserID"] + "',N'"
                                        + param_email + "',N'"
                                        + param_mobile + "',N'"
                                        + param_name + "',N'"
                                        + param_address + "',N'"
                                        + param_id + "',N'"
                                        + param_tax + "',N'"
                                        + param_national + "',N'"
                                        + param_filename + "' ";

                //string sql_command = " UPDATE [BESTBoQ].[dbo].[userinfo] "
                //                   + " SET [email] = N'" + param_email + "' , "
                //                   + " [mobile] = N'"+param_mobile+"' , "
                //                   + " [name] = N'"+param_name+"' ,"
                //                   + " [address] = N'" + param_address + "' ,"
                //                   + " [idcard] = N'" + param_id + "' ,"
                //                   + " [taxname] = N'" + param_tax + "' ,"
                //                   + " [idnation] = N'" + param_national + "' "
                //                   + " WHERE [userid] = '" + Session["UserID"] + "'";

                ClassConfig.GetDataSQL(sql_command);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Update Profile Fail Please Contract Admin');</script>");
                throw;
            }

            getOldData();
            changeControl("Confirm");
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

        public static System.Drawing.Bitmap ScaleImage(System.Drawing.Bitmap image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            //newImage.SetResolution(360, 360);
            using (var g = Graphics.FromImage(newImage))
            {
                //g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        protected void getOldData()
        {
            string sql_command = " EXEC [dbo].[get_userinfo_Logo] '" + Session["UserID"] + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count > 0)
            {
                //Set Data to Label
                lbUsername.Text = dt.Rows[0]["username"].ToString().Trim();
                rbType.SelectedValue = dt.Rows[0]["type"].ToString().Trim();
                lbEmail.Text = dt.Rows[0]["email"].ToString().Trim();
                lbMobile.Text = dt.Rows[0]["mobile"].ToString().Trim();
                lbName.Text = dt.Rows[0]["name"].ToString().Trim();
                lbAddress.Text = dt.Rows[0]["address"].ToString().Trim();
                lbId.Text = dt.Rows[0]["idcard"].ToString().Trim();
                lbTax.Text = dt.Rows[0]["taxname"].ToString().Trim();
                lbNational.Text = dt.Rows[0]["idnation"].ToString().Trim();

                //Set Date to Textbox
                tbEmail.Text = dt.Rows[0]["email"].ToString().Trim();
                tbMobile.Text = dt.Rows[0]["mobile"].ToString().Trim();
                tbName.Text = dt.Rows[0]["name"].ToString().Trim();
                tbAddress.Text = dt.Rows[0]["address"].ToString().Trim();
                tbId.Text = dt.Rows[0]["idcard"].ToString().Trim();
                tbTax.Text = dt.Rows[0]["taxname"].ToString().Trim();
                tbNational.Text = dt.Rows[0]["idnation"].ToString().Trim();

                imgLogoCompany.ImageUrl = dt.Rows[0]["pathpic"].ToString();
            }
        }

        protected void changeControl(string flag)
        {
            if(flag == "Edit")
            {
                //Show Data form textbox
                tbEmail.Visible = true;
                tbMobile.Visible = true;
                tbName.Visible = true;
                tbAddress.Visible = true;
                tbId.Visible = true;
                tbTax.Visible = true;
                tbNational.Visible = true;

                //Hide Data form label
                lbEmail.Visible = false;
                lbMobile.Visible = false;
                lbName.Visible = false;
                lbAddress.Visible = false;
                lbId.Visible = false;
                lbTax.Visible = false;
                lbNational.Visible = false;

                //Show btn Confirm
                btnComfirm.Visible = true;

                //Hide btn Edit
                btnEdit.Visible = false;

                // Logo Image
                //imgLogoCompany.Visible = false;
                FuLogoImageCompany.Visible = true;
            }
            else
            {
                //Hide Data form textbox
                tbEmail.Visible = false;
                tbMobile.Visible = false;
                tbName.Visible = false;
                tbAddress.Visible = false;
                tbId.Visible = false;
                tbTax.Visible = false;
                tbNational.Visible = false;

                //Show Data form label
                lbEmail.Visible = true;
                lbMobile.Visible = true;
                lbName.Visible = true;
                lbAddress.Visible = true;
                lbId.Visible = true;
                lbTax.Visible = true;
                lbNational.Visible = true;


                //Hide btn Confirm
                btnComfirm.Visible = false;

                //Show btn Edit
                btnEdit.Visible = true;

                // Logo Image
                //imgLogoCompany.Visible = true;
                FuLogoImageCompany.Visible = false;
            }
        }

        
    }
}