﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Project_Detail : System.Web.UI.Page
    {
        string userID;
        string s1, s2, s3, s4, s5, s6;
        public string param_projid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userID = Session["UserID"].ToString();
                if (Request.QueryString["id"] != null)
                {
                    param_projid = Request.QueryString["id"].ToString();
                    getData();
                }
                else
                {
                    Response.Redirect("Home.aspx");
                }
            }
        }

        protected void getData()
        {
            string sql = "[dbo].[get_Home_Proj] '" + userID + "','" + param_projid + "'";
            DataTable dt = new DataTable();
            dt = ClassConfig.GetDataSQL(sql);

            if (dt.Rows.Count > 0)
            {
                img.ImageUrl = dt.Rows[0]["homepic"].ToString();
                lbProjName.Text = dt.Rows[0]["projectname"].ToString();
                lbProjName2.Text = dt.Rows[0]["projectname"].ToString();
                lbProjType.Text = dt.Rows[0]["homename"].ToString();
                lbCusName.Text = dt.Rows[0]["customername"].ToString();
                lbCusName2.Text = dt.Rows[0]["customername"].ToString();
                lbCusAddress.Text = dt.Rows[0]["address"].ToString() + ' ' + dt.Rows[0]["province"].ToString() + ' ' + dt.Rows[0]["country"].ToString();
                lbProjStart.Text = dt.Rows[0]["projectstart"].ToString();
                lbContract.Text = dt.Rows[0]["contractid"].ToString();
                lbStatus.Text = dt.Rows[0]["status"].ToString();

                double totalPrice = 0.00;
                double feePrice = 0.00;
                double promoPrice = 0.00;
                double otherPrice = 0.00;
                double lastPrice = 0.00;

                Double.TryParse(dt.Rows[0]["Total"].ToString(), out totalPrice);
                Double.TryParse(dt.Rows[0]["Free"].ToString(), out feePrice);
                Double.TryParse(dt.Rows[0]["Promo"].ToString(), out promoPrice);
                Double.TryParse(dt.Rows[0]["Other"].ToString(), out otherPrice);
                Double.TryParse(dt.Rows[0]["Last"].ToString(), out lastPrice);

                lbTotalPrice.Text = String.Format("{0:N2}", totalPrice);
                lbFeePrice.Text = String.Format("{0:N2}", feePrice);
                lbPromoPrice.Text = String.Format("{0:N2}", promoPrice);
                lbOtherPrice.Text = String.Format("{0:N2}", otherPrice);
                lbLastPrice.Text = String.Format("{0:N2}", lastPrice);
            }
        }

        protected void getUserInfo()
        {
            string sql_command = "SELECT [status1],[status2],[status3],[status4],[status5],[status6] FROM [BESTBoQ].[dbo].[userinfo] WHERE [userid] = '" + userID + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);
            if(dt.Rows.Count >0)
            {
                s1 = dt.Rows[0]["status1"].ToString();
                s2 = dt.Rows[0]["status2"].ToString();
                s3 = dt.Rows[0]["status3"].ToString();
                s4 = dt.Rows[0]["status4"].ToString();
                s5 = dt.Rows[0]["status5"].ToString();
                s6 = dt.Rows[0]["status6"].ToString();
            }
        }

        protected void DownloadContract()
        {
            Contract contract = new Contract(this);
            Response.Redirect("~/PDFs/" + contract.CreatePDF(param_projid));
        }

        protected void DownloadAppendixA()
        {
            AppendixA appendix = new AppendixA(this);
            Response.Redirect("~/PDFs/" + appendix.CreatePDF(param_projid));
        }

        protected void lbtnBoq_Click(object sender, EventArgs e)
        {
            if (s1 == "true")
            {
               
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }

        protected void lbtnContractDoc_Click(object sender, EventArgs e)
        {
            if (s2 == "true")
            {
                DownloadContract();
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }

        protected void lbtnAppendixDoc_Click(object sender, EventArgs e)
        {
            if (s3 == "true")
            {
                DownloadContract();
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }

        protected void lbtnPlan_Click(object sender, EventArgs e)
        {
            if (s4 == "true")
            {
                
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }

        protected void lbtnReport_Click(object sender, EventArgs e)
        {
            if (s5 == "true")
            {
                
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }

        protected void lbtnAll_Click(object sender, EventArgs e)
        {
            if (s6 == "true")
            {
                
            }
            else
            {
                Response.Write("<script>alert('ท่านไม่มีสิทธิ์ใช่งานในModeนี้!!!!');</script>");
            }
        }
    }
}