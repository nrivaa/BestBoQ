using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class CreateHomeNestedMaster : System.Web.UI.MasterPage
    {
        string param_projid;
        public double totalPrice = 0.0;

        public double pctFee = 0.0;
        public double pctPromo = 0.0;
        public double otherPrice = 0.0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
                param_projid = Request.QueryString["id"].ToString();

            bindData();
        }

        protected void bindData()
        {
            string sql_command = " [dbo].[get_ProjInfo] '" + param_projid + "'";
            DataTable dt = ClassConfig.GetDataSQL(sql_command);

            if (dt.Rows.Count > 0) {
                foreach (DataRow dr in dt.Rows) {
                    Label1.Text = dr["projectname"].ToString();
                    Label2.Text = dr["customername"].ToString();
                    Label3.Text = dr["projecttype"].ToString();
                }
            }

            sql_command = " [dbo].[get_Last_Price] '" + param_projid + "'";
            dt = ClassConfig.GetDataSQL(sql_command);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    double totalPrice = 0.00;
                    double feePrice = 0.00;
                    double promoPrice = 0.00;
                    double otherPrice = 0.00;
                    double lastPrice = 0.00;

                    double pctFee = 0.00;
                    double pctPromo = 0.00;

                    Double.TryParse(dr["TotalMoney"].ToString(), out totalPrice);
                    Double.TryParse(dr["FreeMoney"].ToString(), out feePrice);
                    Double.TryParse(dr["PromoMoney"].ToString(), out promoPrice);
                    Double.TryParse(dr["OtherMoney"].ToString(), out otherPrice);
                    Double.TryParse(dr["LastMoney"].ToString(), out lastPrice);

                    Double.TryParse(dr["pct_free"].ToString(), out pctFee);
                    Double.TryParse(dr["pct_promo"].ToString(), out pctPromo);

                    this.pctFee = pctFee;
                    this.pctPromo = pctPromo;
                    this.otherPrice = otherPrice;

                    lbTotalPrice.Text = String.Format("{0:N2}", totalPrice);
                    lbFeePrice.Text = String.Format("{0:N2}", feePrice);
                    lbPromoPrice.Text = String.Format("{0:N2}", promoPrice);
                    lbOtherPrice.Text = String.Format("{0:N2}", otherPrice);
                    lbLastPrice.Text = String.Format("{0:N2}", lastPrice);

                    this.totalPrice = totalPrice;
                }
            }
        }
    }
}