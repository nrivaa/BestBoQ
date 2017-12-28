using System;
using System.Collections.Generic;
using System.Data;
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

        protected void btnComfirm_Click(object sender, EventArgs e)
        {
            //Get val form control
            string param_email = tbEmail.Text.Trim();
            string param_mobile = tbMobile.Text.Trim();
            string param_name = tbName.Text.Trim();
            string param_address = tbAddress.Text.Trim();
            string param_id = tbId.Text.Trim();
            string param_tax = tbTax.Text.Trim();

            //Execute Command
            try
            {
                string sql_command = " UPDATE [BESTBoQ].[dbo].[userinfo] "
                                   + " SET [email] = N'" + param_email + "' , "
                                   + " [mobile] = N'"+param_mobile+"' , "
                                   + " [name] = N'"+param_name+"' ,"
                                   + " [address] = N'" + param_address + "' ,"
                                   + " [idcard] = N'" + param_id + "' ,"
                                   + " [taxname] = N'" + param_tax + "' "
                                   + " WHERE [userid] = '" + Session["UserID"] + "'";
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

        protected void getOldData()
        {
            string sql_command = " SELECT * FROM [BESTBoQ].[dbo].[userinfo] "
                               + " WHERE [userid] = '" + Session["UserID"] + "'";
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

                //Set Date to Textbox
                tbEmail.Text = dt.Rows[0]["email"].ToString().Trim();
                tbMobile.Text = dt.Rows[0]["mobile"].ToString().Trim();
                tbName.Text = dt.Rows[0]["name"].ToString().Trim();
                tbAddress.Text = dt.Rows[0]["address"].ToString().Trim();
                tbId.Text = dt.Rows[0]["idcard"].ToString().Trim();
                tbTax.Text = dt.Rows[0]["taxname"].ToString().Trim();
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

                //Hide Data form label
                lbEmail.Visible = false;
                lbMobile.Visible = false;
                lbName.Visible = false;
                lbAddress.Visible = false;
                lbId.Visible = false;
                lbTax.Visible = false;

                //Show btn Confirm
                btnComfirm.Visible = true;

                //Hide btn Edit
                btnEdit.Visible = false;
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

                //Show Data form label
                lbEmail.Visible = true;
                lbMobile.Visible = true;
                lbName.Visible = true;
                lbAddress.Visible = true;
                lbId.Visible = true;
                lbTax.Visible = true;

                //Hide btn Confirm
                btnComfirm.Visible = false;

                //Show btn Edit
                btnEdit.Visible = true;
            }
        }

        
    }
}