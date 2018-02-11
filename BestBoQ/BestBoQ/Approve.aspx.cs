using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Approve : System.Web.UI.Page
    {
        string userID;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["UserID"] != null)
            userID = "967882";
            if (!IsPostBack)
            {
                ClearInfo();
                bindData();
                ddName.Items.Insert(0, new ListItem("--กรุณาเลือกชื่อ User--", ""));
            }
            
        }

        protected void bindData()
        {
            string sql_command = "SELECT * FROM [BESTBoQ].[dbo].[userinfo]";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            ddName.DataSource = dt;
            ddName.DataValueField = "userid";
            ddName.DataTextField = "username";
            ddName.DataBind();
        }

        protected void ClearInfo()
        {
            lbUsername.Text = string.Empty;
            lbType.Text = string.Empty;
            lbEmail.Text = string.Empty;
            lbCompany.Text = string.Empty;
            lbRegisterdate.Text = string.Empty;
            cb1.Checked = false;
            cb2.Checked = false;
            cb3.Checked = false;
            cb4.Checked = false;
            cb5.Checked = false;
            cb6.Checked = false;

            lbUsername.Visible = false;
            lbType.Visible = false;
            lbEmail.Visible = false;
            lbCompany.Visible = false;
            lbRegisterdate.Visible = false;
            cb1.Visible = false;
            cb2.Visible = false;
            cb3.Visible = false;
            cb4.Visible = false;
            cb5.Visible = false;
            cb6.Visible = false;

            //lb1.Visible = false;
            //lb2.Visible = false;
            //lb3.Visible = false;
            //lb4.Visible = false;
            //lb5.Visible = false;
            //lb6.Visible = false;
        }

        protected void ddName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearInfo();
            string param_userid = ddName.SelectedValue.ToString().Trim();
            string sql_command = "SELECT * FROM [BESTBoQ].[dbo].[userinfo] WHERE[userid] = '"+ param_userid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count >0)
            {
                blockDetail.Visible = true;
                lbUsername.Text = dt.Rows[0]["username"].ToString();
                lbType.Text = dt.Rows[0]["type"].ToString();
                lbEmail.Text = dt.Rows[0]["email"].ToString();
                lbCompany.Text = dt.Rows[0]["companyname"].ToString();
                lbRegisterdate.Text = dt.Rows[0]["registerdate"].ToString();

                if(dt.Rows[0]["status1"].ToString() == "true")
                {
                    cb1.Checked = true;
                }
                if (dt.Rows[0]["status2"].ToString() == "true")
                {
                    cb2.Checked = true;
                }
                if (dt.Rows[0]["status3"].ToString() == "true")
                {
                    cb3.Checked = true;
                }
                if (dt.Rows[0]["status4"].ToString() == "true")
                {
                    cb4.Checked = true;
                }
                if (dt.Rows[0]["status5"].ToString() == "true")
                {
                    cb5.Checked = true;
                }
                if (dt.Rows[0]["status6"].ToString() == "true")
                {
                    cb6.Checked = true;
                }

                lbUsername.Visible = true;
                lbType.Visible = true;
                lbEmail.Visible = true;
                lbCompany.Visible = true;
                lbRegisterdate.Visible = true;
                cb1.Visible = true;
                cb2.Visible = true;
                cb3.Visible = true;
                cb4.Visible = true;
                cb5.Visible = true;
                cb6.Visible = true;
            }


            string period_command = "SELECT * FROM [BESTBoQ].[dbo].[userperiod] WHERE [userid] ='" + param_userid + "'";
            DataTable dt_period = ClassConfig.GetDataSQL(period_command);
            if(dt_period.Rows.Count > 0)
            {
                tbStartCan1.Text = dt_period.Rows[0]["status1_T"].ToString();
                tbStartCan2.Text = dt_period.Rows[0]["status2_T"].ToString();
                tbStartCan3.Text = dt_period.Rows[0]["status3_T"].ToString();
                tbStartCan4.Text = dt_period.Rows[0]["status4_T"].ToString();
                tbStartCan5.Text = dt_period.Rows[0]["status5_T"].ToString();
                tbStartCan6.Text = dt_period.Rows[0]["status6_T"].ToString();

                tbPeriodCan1.Text = dt_period.Rows[0]["status1_P"].ToString();
                tbPeriodCan2.Text = dt_period.Rows[0]["status2_P"].ToString();
                tbPeriodCan3.Text = dt_period.Rows[0]["status3_P"].ToString();
                tbPeriodCan4.Text = dt_period.Rows[0]["status4_P"].ToString();
                tbPeriodCan5.Text = dt_period.Rows[0]["status5_P"].ToString();
                tbPeriodCan6.Text = dt_period.Rows[0]["status6_P"].ToString();
            }
        }

        protected void Mgt_func(string step, string can, string time, string period)
        {
            string param_userid = ddName.SelectedValue.ToString().Trim();
            string sql_command = "EXEC [dbo].[set_userperiod] '" + param_userid + "','" + step + "','" + can + "','" + time + "','" + period + "','" + userID + "' ";
            ClassConfig.GetDataSQL(sql_command);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string param_userid = ddName.SelectedValue.ToString().Trim();
            string param_can1, param_can2, param_can3, param_can4, param_can5, param_can6;
            if(cb1.Checked == true)
            {
                param_can1 = "true";
            }
            else
            {
                param_can1 = "false";
            }
            if (cb2.Checked == true)
            {
                param_can2 = "true";
            }
            else
            {
                param_can2 = "false";
            }
            if (cb3.Checked == true)
            {
                param_can3 = "true";
            }
            else
            {
                param_can3 = "false";
            }
            if (cb4.Checked == true)
            {
                param_can4 = "true";
            }
            else
            {
                param_can4 = "false";
            }
            if (cb5.Checked == true)
            {
                param_can5 = "true";
            }
            else
            {
                param_can5 = "false";
            }
            if (cb6.Checked == true)
            {
                param_can6 = "true";
            }
            else
            {
                param_can6 = "false";
            }

            string sql_command = " EXEC [dbo].[set_Approve] '" 
                               + param_can1 + "','" + param_can2 + "','" + param_can3 + "','" 
                               + param_can4 + "','" + param_can5 + "','" + param_can6 + "','" 
                               + param_userid + "'";
            ClassConfig.GetDataSQL(sql_command);

        }

        protected void btnCan1_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb1.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("1", param_can, tbStartCan1.Text, tbPeriodCan1.Text);
        }

        protected void btnCan2_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb2.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("2", param_can, tbStartCan2.Text, tbPeriodCan2.Text);
        }

        protected void btnCan3_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb3.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("3", param_can, tbStartCan3.Text, tbPeriodCan3.Text);
        }

        protected void btnCan4_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb4.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("4", param_can, tbStartCan4.Text, tbPeriodCan4.Text);
        }

        protected void btnCan5_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb5.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("5", param_can, tbStartCan5.Text, tbPeriodCan5.Text);
        }

        protected void btnCan6_Click(object sender, EventArgs e)
        {
            string param_can;
            if (cb6.Checked == true)
            {
                param_can = "true";
            }
            else
            {
                param_can = "false";
            }

            Mgt_func("6", param_can, tbStartCan6.Text, tbPeriodCan6.Text);
        }
    }
}