using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateProj_03_14_WinDoor : System.Web.UI.Page
    {
        string userID = "967882";
        string param_projid = "000002";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindData();
            }
        }

        protected void bindData()
        {
            string sql_command_door1 = " SELECT [windoorPart],[windoorType],[cost_item],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_14_WinDoor] WHERE [windoorPart] = N'ประตูภายใน' ";
            DataTable dt_door1 = ClassConfig.GetDataSQL(sql_command_door1);
            if (dt_door1.Rows.Count > 0)
            {
                Repeater1.DataSource = dt_door1;
                Repeater1.DataBind();
            }

            string sql_command_door2 = " SELECT [windoorPart],[windoorType],[cost_item],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_14_WinDoor] WHERE [windoorPart] = N'ประตูห้องน้ำ' ";
            DataTable dt_door2 = ClassConfig.GetDataSQL(sql_command_door2);
            if (dt_door2.Rows.Count > 0)
            {
                Repeater2.DataSource = dt_door2;
                Repeater2.DataBind();
            }

            string sql_command_win1 = " SELECT [windoorPart],[windoorType],[cost_item],[detail],[picpath] "
                               + " FROM [BESTBoQ].[dbo].[CFG_3_14_WinDoor] WHERE [windoorPart] = N'หน้าต่าง' ";
            DataTable dt_win1 = ClassConfig.GetDataSQL(sql_command_win1);
            if (dt_win1.Rows.Count > 0)
            {
                Repeater3.DataSource = dt_win1;
                Repeater3.DataBind();
            }

            //Get Num Door & Window 
            string sql_command_get = " EXEC [dbo].[get_MM14] '" + param_projid + "' ";
            DataTable dt_get = ClassConfig.GetDataSQL(sql_command_get);
            if (dt_get.Rows.Count > 0)
            {
                lbdoor1.Text = dt_get.Rows[0]["door1"].ToString();
                lbdoor2.Text = dt_get.Rows[0]["door2"].ToString();
                lbwin1.Text = dt_get.Rows[0]["win1"].ToString();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in Repeater1.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbwindoorType = (Label)item.FindControl("Label2");
                if (lbwindoorType != null)
                {
                    string param_windoorType = lbwindoorType.Text.Trim();
                    string param_Num = lbdoor1.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_14_WinDoor] "
                                           + " '"+param_projid+ "',N'ประตูภายใน','"+ param_windoorType + "','"+ param_Num + "','" + userID+"' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }

            foreach (RepeaterItem item in Repeater2.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbwindoorType = (Label)item.FindControl("Label2");
                if (lbwindoorType != null)
                {
                    string param_windoorType = lbwindoorType.Text.Trim();
                    string param_Num = lbdoor2.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_14_WinDoor] "
                                           + " '" + param_projid + "',N'ประตูห้องน้ำ','" + param_windoorType + "','" + param_Num + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }

            foreach (RepeaterItem item in Repeater3.Items)
            {
                RadioButton rbSelect = (RadioButton)item.FindControl("RadioButton1");
                Label lbwindoorType = (Label)item.FindControl("Label2");
                if (lbwindoorType != null)
                {
                    string param_windoorType = lbwindoorType.Text.Trim();
                    string param_Num = lbwin1.Text.Trim();
                    if (rbSelect != null && rbSelect.Checked == true)
                    {
                        string sql_command = " EXEC [dbo].[set_Project_03_14_WinDoor] "
                                           + " '" + param_projid + "',N'หน้าต่าง','" + param_windoorType + "','" + param_Num + "','" + userID + "' ";
                        ClassConfig.GetDataSQL(sql_command);
                    }
                }
            }
        }
    }
}