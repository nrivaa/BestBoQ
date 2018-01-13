using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_09 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        DataTable dt_old;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
                userID = Session["UserID"].ToString();
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            if (!IsPostBack)
            {
                getOldData();
                bindData();
            }

        }

        protected void bindData()
        {
            string sql_command = " SELECT RTRIM([plumbingType]) AS [plumbingType],[pipe1],[pipe2],[pipe3],[cost/room],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_9_Plumbing] ";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                    Label lbplumbingType = (Label)item.FindControl("Label1");
                    if (lbplumbingType != null)
                    {
                        string param_plumbingType = lbplumbingType.Text.Trim();
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_09_Plumbing] "
                                               + " '" + param_projid + "','" + param_plumbingType + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_10?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],RTRIM([plumbingType]) AS [plumbingType] FROM [BESTBoQ].[dbo].[Project_03_09_Plumbing]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_plumbingType = (string)DataBinder.Eval(e.Item.DataItem, "plumbingType");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            if (dt_old.Rows.Count > 0)
            {
                if (param_plumbingType.ToString() == dt_old.Rows[0]["plumbingType"].ToString())
                {
                    rb.Checked = true;
                }
            }
        }
    }
}