using System;
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

        protected void DownloadContract()
        {
            Contract contract = new Contract(this);
            Response.Redirect(contract.CreatePDF(param_projid));
        }

        protected void DownloadAppendixA()
        {
            AppendixA appendix = new AppendixA(this);
            Response.Redirect(appendix.CreatePDF(param_projid));
        }

    }
}