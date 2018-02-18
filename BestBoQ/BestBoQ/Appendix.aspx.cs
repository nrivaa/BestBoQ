using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Appendix : System.Web.UI.Page
    {
        public string ProjectType { get; set; }
        public string Area { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ProjectType = "อาคารที่พักอาศัย 2 ชั้น";
            Area = "350";
        }
    }
}