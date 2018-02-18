using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class Timeline2 : System.Web.UI.Page
    {
        public string TestVariable { get { return "TestVariable"; } }
        public string Phase1Start { get; set; }
        public string Phase1Stop { get; set; }
        public string Phase2Start { get; set; }
        public string Phase2Stop { get; set; }
        public string Phase3Start { get; set; }
        public string Phase3Stop { get; set; }
        public string Phase4Start { get; set; }
        public string Phase4Stop { get; set; }
        public string Phase5Start { get; set; }
        public string Phase5Stop { get; set; }
        public string Phase6Start { get; set; }
        public string Phase6Stop { get; set; }
        public string Phase7Start { get; set; }
        public string Phase7Stop { get; set; }
        public string Phase8Start { get; set; }
        public string Phase8Stop { get; set; }
        public string Phase9Start { get; set; }
        public string Phase9Stop { get; set; }
        public string Phase10Start { get; set; }
        public string Phase10Stop { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string ProjectName { get; set; }
        public string TotalDate { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string projectID = Request["projectID"];
            DataTable dt = ClassConfig.GetDataSQL("EXECUTE [dbo].[get_Plan] '<projectID>'".Replace("<projectID>", projectID));

            if (dt.Rows.Count > 0)
            {
                DateTime startTime = Convert.ToDateTime(dt.Rows[0]["start"].ToString());
                Year = startTime.Year.ToString();
                Month = startTime.Month.ToString();
                Day = startTime.Day.ToString();

                TotalDate = dt.Rows[0]["Total"].ToString();

                ProjectName = dt.Rows[0]["projectName"].ToString();

                int stop1 = Convert.ToInt16(dt.Rows[0]["s1"]);
                int stop2 = stop1 + Convert.ToInt16(dt.Rows[0]["s2"]);
                int stop3 = stop2 + Convert.ToInt16(dt.Rows[0]["s3"]);
                int stop4 = stop3 + Convert.ToInt16(dt.Rows[0]["s4"]);
                int stop5 = stop4 + Convert.ToInt16(dt.Rows[0]["s5"]);
                int stop6 = stop5 + Convert.ToInt16(dt.Rows[0]["s6"]);
                int stop7 = stop6 + Convert.ToInt16(dt.Rows[0]["s7"]);
                int stop8 = stop7 + Convert.ToInt16(dt.Rows[0]["s8"]);
                int stop9 = stop8 + Convert.ToInt16(dt.Rows[0]["s9"]);
                int stop10 = stop9 + Convert.ToInt16(dt.Rows[0]["s10"]);

                Phase1Start = 0.ToString();
                Phase1Stop = stop1.ToString();

                Phase2Start = (stop1 - Math.Round(Convert.ToDouble(dt.Rows[0]["s1"]) * 0.2)).ToString();
                Phase2Stop = stop2.ToString();

                Phase3Start = (stop2 - Math.Round(Convert.ToDouble(dt.Rows[0]["s2"]) * 0.2)).ToString();
                Phase3Stop = stop3.ToString();

                Phase4Start = (stop3 - Math.Round(Convert.ToDouble(dt.Rows[0]["s3"]) * 0.2)).ToString();
                Phase4Stop = stop4.ToString();

                Phase5Start = (stop4 - Math.Round(Convert.ToDouble(dt.Rows[0]["s4"]) * 0.2)).ToString();
                Phase5Stop = stop5.ToString();

                Phase6Start = (stop5 - Math.Round(Convert.ToDouble(dt.Rows[0]["s5"]) * 0.2)).ToString();
                Phase6Stop = stop6.ToString();

                Phase7Start = (stop6 - Math.Round(Convert.ToDouble(dt.Rows[0]["s6"]) * 0.2)).ToString();
                Phase7Stop = stop7.ToString();

                Phase8Start = (stop7 - Math.Round(Convert.ToDouble(dt.Rows[0]["s7"]) * 0.2)).ToString();
                Phase8Stop = stop8.ToString();

                Phase9Start = (stop8 - Math.Round(Convert.ToDouble(dt.Rows[0]["s8"]) * 0.2)).ToString();
                Phase9Stop = stop9.ToString();

                Phase10Start = (stop9 - Math.Round(Convert.ToDouble(dt.Rows[0]["s9"]) * 0.2)).ToString();
                Phase10Stop = stop10.ToString();

            }

        }
    }
}