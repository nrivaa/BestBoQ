using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BestBoQ
{
    public partial class boq : System.Web.UI.Page
    {
        public string TotalArea { get; set; }
        public string CountBedRoom { get; set; }
        public string CountBathRoom { get; set; }
        public string ProjectType { get; set; }
        public string TableStructure { get; set; }
        public string TableArchitecture { get; set; }
        public string TableSystem { get; set; }
        public string ProjectName { get; set; }
        public string ContactName { get; set; }
        public string CustomerName { get; set; }
        public string ContactAdd { get; set; }
        public string ProjectStart { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string projectID = Request["projectID"];
            string projectID = "000030";
            //string userID = Request["userID"];
            string userID = "348770";
            DataTable dt = ClassConfig.GetDataSQL("exec [dbo].[get_BOQ] '@pprojid'".Replace("@pprojid", projectID));
            DataTable dt2 = ClassConfig.GetDataSQL("exec [dbo].[get_AppendixA] '@pprojid'".Replace("@pprojid", projectID));
            DataTable dt3 = ClassConfig.GetDataSQL("exec [dbo].[get_Home_Proj] '@puserid'".Replace("@puserid", userID) + ", '@pprojid'".Replace("@pprojid", projectID));
            if(dt.Rows.Count> 0)
            {
                CountBedRoom = dt.AsEnumerable().Where(x => x.Field<string>("footingType") == "ห้องนอน").Select(x => x.Field<string>("Room")).FirstOrDefault().ToString();
                CountBathRoom = dt.AsEnumerable().Where(x => x.Field<string>("footingType") == "ห้องน้ำ").Select(x => x.Field<string>("Room")).FirstOrDefault().ToString();
            }
            if (dt2.Rows.Count > 0)
            {
                ProjectType = dt2.Rows[0]["hometype"].ToString();
                TotalArea = dt2.Rows[0]["numMM"].ToString();
            }
            if (dt3.Rows.Count > 0)
            {
                ProjectName = dt3.Rows[0]["projectname"].ToString();
                ContactName = dt3.Rows[0]["contractid"].ToString();
                CustomerName = dt3.Rows[0]["customername"].ToString();
                ContactAdd = dt3.Rows[0]["address"].ToString();
                ProjectStart = dt3.Rows[0]["projectstart"].ToString();
                img.ImageUrl = dt3.Rows[0]["homepic"].ToString();
            }

            {
                // Table Structure
                TableStructure += "<table class='table'>";
                TableStructure += "<thead><tr>";
                TableStructure += "<th>งาน</th>";
                TableStructure += "<th>จำนวน</th>";
                TableStructure += "<th>หน่วย</th>";
                TableStructure += "</tr></thead>";

                TableStructure += "<tbody>";

                // Footing
                var queryFooting = from table in dt.AsEnumerable()
                                   where table.Field<string>("Material") == "Footing"
                                   select table;

                TableStructure += "<tr>";
                TableStructure += "<td><b>งานเสาเข็มและฐานราก</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableStructure += "</tr>";

                foreach (var item in queryFooting)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>&nbsp;&nbsp;" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ต้น</td>";
                    TableStructure += "</tr>";
                }

                // Beam
                var queryBeam = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "Beam"
                                select table;

                TableStructure += "<tr>";
                TableStructure += "<td><b>งานคาน</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableStructure += "</tr>";

                foreach (var item in queryBeam)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>&nbsp;&nbsp;" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ตารางเมตร</td>";
                    TableStructure += "</tr>";
                }

                // Floor
                var queryFloor = from table in dt.AsEnumerable()
                                 where table.Field<string>("Material") == "Floor"
                                 select table;

                TableStructure += "<tr>";
                TableStructure += "<td><b>งานแผ่นพื้นสำเร็จรูป</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableStructure += "</tr>";

                foreach (var item in queryFloor)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>&nbsp;&nbsp;" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ตารางเมตร</td>";
                    TableStructure += "</tr>";
                }

                TableStructure += "</tbody>";

                // End
                TableStructure += "</table>";

            }

            {
                // Table Architecture
                TableArchitecture += "<table class='table'>";
                TableArchitecture += "<thead><tr>";
                TableArchitecture += "<th>งาน</th>";
                TableArchitecture += "<th>จำนวน</th>";
                TableArchitecture += "<th>หน่วย</th>";
                TableArchitecture += "</thead></tr>";

                TableArchitecture += "<tbody>";

                // Roof
                var queryRoof = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "Roof"
                                select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานหลังคาและวัสดุมุง</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryRoof)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                // Wall
                var queryWall = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "Wall"
                                select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานผนังก่อและฉาบ</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryWall)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                // Flooring
                var queryFlooring = from table in dt.AsEnumerable()
                                    where table.Field<string>("Material") == "Flooring"
                                    select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานผิวตกแต่งพื้น</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryFlooring)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("Room") + " (" + item.Field<string>("footingType") + ")" + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                // Toilet
                var queryToilet = from table in dt.AsEnumerable()
                                  where table.Field<string>("Material") == "Toilet"
                                  select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานสุขภัณฑ์</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryToilet)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<string>("Room") + "</td>";
                    TableArchitecture += "<td>ห้อง</td>";
                    TableArchitecture += "</tr>";
                }

                // Ceiling
                var queryCeiling = from table in dt.AsEnumerable()
                                   where table.Field<string>("Material") == "Ceiling"
                                   select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานฝ้าเพดาน</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryCeiling)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                // Color
                var queryColor = from table in dt.AsEnumerable()
                                 where table.Field<string>("Material") == "Color"
                                 select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานสี</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryColor)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                // Railing
                var queryRailing = from table in dt.AsEnumerable()
                                   where table.Field<string>("Material") == "Railing"
                                   select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานรั้ว</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                foreach (var item in queryRailing)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>ตารางเมตร</td>";
                    TableArchitecture += "</tr>";
                }

                TableArchitecture += "<tr>";
                TableArchitecture += "<td><b>งานประตูหน้าต่าง</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableArchitecture += "</tr>";

                // ประตูภายใน
                var queryDoor = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "ประตูภายใน"
                                select table;

                foreach (var item in queryDoor)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>บาน</td>";
                    TableArchitecture += "</tr>";
                }

                // ประตูห้องน้ำ
                var queryToiletDoor = from table in dt.AsEnumerable()
                                      where table.Field<string>("Material") == "ประตูห้องน้ำ"
                                      select table;

                foreach (var item in queryToiletDoor)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>บาน</td>";
                    TableArchitecture += "</tr>";
                }

                // หน้าต่าง
                var queryWindow = from table in dt.AsEnumerable()
                                  where table.Field<string>("Material") == "หน้าต่าง"
                                  select table;

                foreach (var item in queryWindow)
                {
                    TableArchitecture += "<tr>";
                    TableArchitecture += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableArchitecture += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableArchitecture += "<td>บาน</td>";
                    TableArchitecture += "</tr>";
                }

                TableArchitecture += "</tbody>";

                // End
                TableArchitecture += "</table>";

            }

            {
                // Table System
                TableSystem += "<table class='table'>";
                TableSystem += "<thead><tr>";
                TableSystem += "<th>งาน</th>";
                TableSystem += "<th>จำนวน</th>";
                TableSystem += "<th>หน่วย</th>";
                TableSystem += "</tr></thead>";

                TableSystem += "<tbody>";

                // Sanitation
                var querySanitation = from table in dt.AsEnumerable()
                                      where table.Field<string>("Material") == "Sanitation"
                                      select table;

                TableSystem += "<tr>";
                TableSystem += "<td><b>งานสุขาภิบาลและระบายน้ำ</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableSystem += "</tr>";

                foreach (var item in querySanitation)
                {
                    TableSystem += "<tr>";
                    TableSystem += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableSystem += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableSystem += "<td>ชุด</td>";
                    TableSystem += "</tr>";
                }

                // Plumbing
                var queryPlimbing = from table in dt.AsEnumerable()
                                    where table.Field<string>("Material") == "Plumbing"
                                    select table;

                TableSystem += "<tr>";
                TableSystem += "<td><b>งานประปา</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableSystem += "</tr>";

                foreach (var item in queryPlimbing)
                {
                    TableSystem += "<tr>";
                    TableSystem += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableSystem += "<td>" + item.Field<string>("Room") + "</td>";
                    TableSystem += "<td>ชุด</td>";
                    TableSystem += "</tr>";
                }

                // Electric
                var queryFooting = from table in dt.AsEnumerable()
                                   where table.Field<string>("Material") == "Electric"
                                   select table;

                TableSystem += "<tr>";
                TableSystem += "<td><b>งานไฟฟ้า</b></td><td>&nbsp;</td><td>&nbsp;</td>";
                TableSystem += "</tr>";

                foreach (var item in queryFooting)
                {
                    TableSystem += "<tr>";
                    TableSystem += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableSystem += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableSystem += "<td>ชุด</td>";
                    TableSystem += "</tr>";
                }

                TableSystem += "</tbody>";

                // End
                TableSystem += "</table>";

            }

        }
    }
}