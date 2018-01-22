using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProject_03_12 : System.Web.UI.Page
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
            //Get data form DB to repeater
            string sql_command = " EXEC [dbo].[get_template_03_12] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

            ////Get data MM to show
            //string sql_getMM = "EXEC [dbo].[get_MM12] '" + param_projid + "'";
            //DataTable dt_MM = ClassConfig.GetDataSQL(sql_getMM);
            //if (dt_MM.Rows.Count > 0)
            //{
            //    lb1.Text = "รวมแนวตั้ง :  ผนังภายใน+ภายนอก = " + dt_MM.Rows[0]["MM1"].ToString().Trim();
            //    lb2.Text = "รวมแนวนอน : ฝ้าภายใน+ภายนอก = " + dt_MM.Rows[0]["MM2"].ToString().Trim();
            //    lb3.Text = "อื่นๆ (คาน+ไม่ฝ้าปั้นลม+ไม้เชิงชาย) = " + dt_MM.Rows[0]["MM3"].ToString().Trim();
            //    lbTotal.Text = "ตรม.รวม = " + dt_MM.Rows[0]["MMTotal"].ToString().Trim();
            //}
        }

        protected double getMM()
        {
            string sql_command = "EXEC [dbo].[get_MM12] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if (dt.Rows.Count > 0)
            {
                return double.Parse((dt.Rows[0]["MMTotal"].ToString()), CultureInfo.InvariantCulture);
            }
            else
            {
                return 0;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem item in Repeater1.Items)
                {
                    RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                    Label lbcolorID = (Label)item.FindControl("Label1");
                    if (lbcolorID != null)
                    {
                        string param_colorID = lbcolorID.Text.Trim();
                        //string param_MMTotal = lbTotal.Text.Trim().Replace("ตรม.รวม = ", "");
                        double param_MMTotal = getMM();
                        if (rbSelect != null && rbSelect.Checked == true)
                        {
                            string sql_command = " EXEC [dbo].[set_Project_03_12_Color] "
                                               + " '" + param_projid + "','" + param_colorID + "','" + param_MMTotal + "','" + userID + "' ";
                            ClassConfig.GetDataSQL(sql_command);
                        }
                    }
                }
                Response.Redirect("CreateProject_03_13?id=" + param_projid);
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
                    section_price = dr[11].ToString();
                }
            }
        }

        protected void getOldData()
        {
            string sql_command = " SELECT [projectid],[colorid] FROM [BESTBoQ].[dbo].[Project_03_12_Color]  WHERE[projectid] = '" + param_projid + "' ";
            dt_old = ClassConfig.GetDataSQL(sql_command);
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Int32 param_colorid = (Int32)DataBinder.Eval(e.Item.DataItem, "colorid");
            RadioButton rb = (RadioButton)e.Item.FindControl("RadioButton1");
            if (dt_old.Rows.Count > 0)
            {
                if (param_colorid.ToString() == dt_old.Rows[0]["colorid"].ToString())
                {
                    rb.Checked = true;
                }
            }
        }
    }
}