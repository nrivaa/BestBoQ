using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_03 : System.Web.UI.Page
    {
        string userID;
        public string param_projid;
        public string section_price = "0";

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
                bindData();
            }
        }
        protected void bindData()
        {
            string sql_command = " EXEC [dbo].[get_template_03_03] '" + param_projid + "'";
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
                    TextBox tbNumRoom = (TextBox)item.FindControl("TextBox2");
                    TextBox tbNumMM = (TextBox)item.FindControl("TextBox3");
                    Label lbFloorType = (Label)item.FindControl("Label1");
                    Repeater rpFlooringType = (Repeater)item.FindControl("Repeater2");

                    if (lbFloorType != null)
                    {
                        string param_floorType = lbFloorType.Text.Trim();
                        string param_numRoom = string.Empty;
                        if (tbNumRoom != null)
                        {
                            param_numRoom = tbNumRoom.Text.Trim();
                        }
                        else
                        {
                            param_numRoom = "0";
                        }
                        if (tbNumMM != null)
                        {
                            string param_numMM = tbNumMM.Text.Trim();

                            string param_flooringType = "";
                            foreach (RepeaterItem itemF in rpFlooringType.Items)
                            {
                                RadioButton rbSelect = (RadioButton)itemF.FindControl("RadioButton1");
                                Label lbFlooringType = (Label)itemF.FindControl("Label2");
                                if (rbSelect != null && rbSelect.Checked == true)
                                {
                                    param_flooringType = lbFlooringType.Text;
                                }
                            }

                            string sql_command = " EXEC [set_Project_03_03-06_Floor_New] "
                                           + " '" + param_projid + "','" + param_flooringType + "','" + param_floorType + "','" + param_numMM + "','" + param_numRoom + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }

                //Function Min Max
                string sql_min_max = " EXEC [dbo].[set_min_max_all] '" + param_projid + "', '" + userID + "' ";
                ClassConfig.GetDataSQL(sql_min_max);

                //Update Status
                ClassConfig.UpdateStatus(param_projid, "On Progress", userID);

                //Redirect
                Response.Redirect("CreateProject_03_04?id=" + param_projid);
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
                    section_price = (Double.Parse(dr[2].ToString()) + Double.Parse(dr[5].ToString())).ToString();
                }
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[floorType],[numMM],[numRoom] FROM [BESTBoQ].[dbo].[Project_03_03_Floor] WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string param_floorType = (string)DataBinder.Eval(e.Item.DataItem, "floorType");
            TextBox tbNumRoom = (TextBox)e.Item.FindControl("TextBox2");
            TextBox tbNumM = (TextBox)e.Item.FindControl("TextBox3");
            Repeater rp = (Repeater)e.Item.FindControl("Repeater2");

            string sql_command = " EXEC [dbo].[get_template_03_06] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                rp.DataSource = dt;
                rp.DataBind();
            }

            if (dt_old.Rows.Count > 0)
            {
                foreach (DataRow dr in dt_old.Rows)
                {
                    if (dr["floorType"].ToString().Trim() == param_floorType.Trim())
                    {
                        tbNumRoom.Text = dr["numRoom"].ToString().Trim();
                        tbNumM.Text = dr["numMM"].ToString().Trim();
                    }
                }
            }
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var repeater = (Repeater)sender;
            var parentItem = (RepeaterItem)repeater.NamingContainer;
            var parentDataItem = parentItem.DataItem;

            string param_floorType = (string)DataBinder.Eval(parentDataItem, "floorType");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            Label lbFlooringType = (Label)e.Item.FindControl("Label2");
            
            //rb.ID = param_floorType.Trim();

            string sql_command_06 = " SELECT [projectid],[flooringType],[numMM],[floorType] FROM [BESTBoQ].[dbo].[Project_03_06_Flooring] WHERE [projectid] = '" + param_projid + "' AND floorType ='"  + param_floorType + "' AND floorType <> 'A6'";
            DataTable dt_old_06 = ClassConfig.GetDataSQL(sql_command_06);

            if (dt_old_06.Rows.Count > 0)
            {
                if (param_floorType.Trim().ToString() == dt_old_06.Rows[0]["floorType"].ToString().Trim())
                {
                    if (lbFlooringType.Text.Trim() == dt_old_06.Rows[0]["flooringType"].ToString().Trim()) {
                        rb.Checked = true;
                    }
                }
            }

        }
    }
}