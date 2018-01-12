using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace BestBoQ
{
    public partial class TestHTMLtoPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Contract contract = new Contract(this);
            //Response.Redirect("/pdfs/" + contract.CreatePDF());

            AppendixA appendixa = new AppendixA(this);
            Response.Redirect(appendixa.CreatePDF("000006"));
        }
    }
}