using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_04 : System.Web.UI.Page
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
                bindDropdown();
            }
        }

        protected void bindDropdown()
        {
            string sql_command = "SELECT [picpath],[roofStyle] FROM [BESTBoQ].[dbo].[CFG_3_4_Roof] GROUP BY [picpath],[roofStyle]";
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
                    RadioButtonList rbSelect = (RadioButtonList) item.FindControl("RadioButtonList1");

                    Label lbRoofStyle = (Label)item.FindControl("Label1");

                    if (lbRoofStyle != null)
                    {
                        string param_roofStyle = lbRoofStyle.Text.Trim();

                        foreach (ListItem li in rbSelect.Items)
                        {
                            if (rbSelect != null && li.Selected == true)
                            {
                                string param_roofType = li.Value.ToString().Trim();

                                string sql_command = " EXEC [dbo].[set_Project_03_04_Roof] "
                                       + " '" + param_projid + "','" + param_roofStyle + "','" + param_roofType + "','" + userID + "'";
                                ClassConfig.GetDataSQL(sql_command);
                            }
                        }
                    }
                }
                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_05?id=" + param_projid);
            }
            catch (Exception)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#alertError').show();", true);
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[roofStyle],[roofType] FROM [BESTBoQ].[dbo].[Project_03_04_Roof]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Item)
            //{
                string param_roofStyle = (string) DataBinder.Eval(e.Item.DataItem, "roofStyle");
                RadioButtonList rb = (RadioButtonList) e.Item.FindControl("RadioButtonList1");

                //string param_roofStyle = lbRoofStyle.Text.ToString();

                string sql_command = " SELECT RTRIM([roofType]) AS [roofType],[detail] FROM [BESTBoQ].[dbo].[CFG_3_4_Roof] "
                                   + " WHERE [roofStyle] = '" + param_roofStyle.Trim() + "' "
                                   + " GROUP BY [roofType],[detail] ";
                DataTable dt_type = ClassConfig.GetDataSQL(sql_command);
                rb.DataSource = dt_type;
                rb.DataTextField = "detail";
                rb.DataValueField = "roofType";
                rb.DataBind();

            if(param_roofStyle.Trim() == dt_old.Rows[0]["roofStyle"].ToString().Trim())
            {
                rb.Items.FindByValue(dt_old.Rows[0]["roofType"].ToString().Trim()).Selected = true;
            }
            
            //}
        }
    }
}