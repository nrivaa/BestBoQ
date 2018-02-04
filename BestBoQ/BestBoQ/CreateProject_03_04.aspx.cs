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
        public string section_price = "0";
        public string section_price_A6 = "0";

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
                getSectionPrice();
                bindDD();
            }
        }

        protected void bindDD()
        {
            string sql_command = " EXEC [dbo].[get_template_03_04] '" + param_projid + "'";
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
                    //RadioButtonList rbSelect = (RadioButtonList)item.FindControl("RadioButtonList1");

                    Label lbRoofStyle = (Label)item.FindControl("Label1");
                    Label lbRoofType = (Label)item.FindControl("Label2");
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");

                    if (lbRoofStyle != null && lbRoofType != null)
                    {
                        string param_roofStyle = lbRoofStyle.Text.Trim();
                        string param_roofType = lbRoofType.Text.Trim();

                        if (rbSelect != null && rbSelect.Checked == true)
                        {

                            string sql_command = " EXEC [dbo].[set_Project_03_04_Roof] "
                                   + " '" + param_projid + "','" + param_roofStyle + "','" + param_roofType + "','" + userID + "','All'";
                            ClassConfig.GetDataSQL(sql_command);
                        }

                        //    foreach (ListItem li in rbSelect.Items)
                        //    {
                        //if (rbSelect != null && li.Selected == true)
                        //{
                        //string param_roofType = li.Value.ToString().Trim();
                        //string sql_command = " EXEC [dbo].[set_Project_03_04_Roof] "
                        //         + " '" + param_projid + "','" + param_roofStyle + "','" + param_roofType + "','" + userID + "'";
                        //ClassConfig.GetDataSQL(sql_command);
                        //}
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

        private void getSectionPrice()
        {
            string sql_price_command = " [dbo].[get_Last_Price] '" + param_projid + "'";
            DataTable dtPrice = ClassConfig.GetDataSQL(sql_price_command);
            if (dtPrice.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPrice.Rows)
                {
                    section_price = dr[3].ToString();
                }
            }

            string sql_a6_command = " [dbo].[get_A6] '" + param_projid + "'";
            DataTable dtA6 = ClassConfig.GetDataSQL(sql_a6_command);
            if (dtA6.Rows.Count > 0)
            {
                foreach (DataRow dr in dtA6.Rows)
                {
                    section_price_A6 = dr[0].ToString();
                }
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[roofStyle],[roofType] FROM [BESTBoQ].[dbo].[Project_03_04_Roof]  WHERE[projectid] = '" + param_projid + "' AND [floorType] <> 'A6' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);

        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

            string param_roofStyle = (string)DataBinder.Eval(e.Item.DataItem, "roofStyle");
            string param_roofType = (string)DataBinder.Eval(e.Item.DataItem, "roofType");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");


            if (dt_old.Rows.Count > 0)
            {
                if (param_roofStyle.Trim().ToString() == dt_old.Rows[0]["roofStyle"].ToString().Trim())
                {
                    if (param_roofType.Trim().ToString() == dt_old.Rows[0]["roofType"].ToString().Trim()) {
                        rb.Checked = true;
                    }
                }
            }
            
            //string param_roofStyle = (string)DataBinder.Eval(e.Item.DataItem, "roofStyle");
            //RadioButtonList rb = (RadioButtonList)e.Item.FindControl("RadioButtonList1");

            //string sql_command = " SELECT RTRIM([roofType]) AS [roofType],[detail] FROM [BESTBoQ].[dbo].[CFG_3_4_Roof] "
            //                   + " WHERE [roofStyle] = '" + param_roofStyle.Trim() + "' "
            //                   + " GROUP BY [roofType],[detail] ";
            //DataTable dt_type = ClassConfig.GetDataSQL(sql_command);

            //rb.CssClass = param_roofStyle.Trim() == "Slap" ? "not-include" : "";
            //if (param_roofStyle.Trim() == "Slap")
            //{
            //    rb.SelectedIndex = 0;
            //}
            //rb.DataSource = dt_type;
            //rb.DataTextField = "detail";
            //rb.DataValueField = "roofType";
            //rb.DataBind();


            //if (dt_old.Rows.Count > 0)
            //{
            //    if (param_roofStyle.Trim() == dt_old.Rows[0]["roofStyle"].ToString().Trim())
            //    {
            //        rb.Items.FindByValue(dt_old.Rows[0]["roofType"].ToString().Trim()).Selected = true;
            //    }
            //}
            
        }
    }
}