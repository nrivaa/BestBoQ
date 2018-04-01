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

        protected void Page_Load(object sender, EventArgs e)
        {
            string projectID = Request["projectID"];
            projectID = "000001";
            DataTable dt = ClassConfig.GetDataSQL("exec [dbo].[get_BOQ] '@pprojid'".Replace("@pprojid", projectID));
            DataTable dt2 = ClassConfig.GetDataSQL("exec [dbo].[get_AppendixA] '@pprojid'".Replace("@pprojid", projectID));

            CountBedRoom = dt.AsEnumerable().Where(x => x.Field<string>("footingType") == "ห้องนอน").Select(x=>x.Field<string>("Room")).FirstOrDefault().ToString();
            CountBathRoom = dt.AsEnumerable().Where(x => x.Field<string>("footingType") == "ห้องน้ำ").Select(x=>x.Field<string>("Room")).FirstOrDefault().ToString();
            ProjectType = dt2.Rows[0]["hometype"].ToString();
            TotalArea = dt2.Rows[0]["numMM"].ToString();

            {
                // Table Structure
                TableStructure += "<table>";
                TableStructure += "<tr>";
                TableStructure += "<td>งาน</td>";
                TableStructure += "<td>จำนวน</td>";
                TableStructure += "<td>หน่วย</td>";
                TableStructure += "</tr>";

                // Footing
                var queryFooting = from table in dt.AsEnumerable()
                                   where table.Field<string>("Material") == "Footing"
                                   select table;

                TableStructure += "<tr>";
                TableStructure += "<td>งานเสาเข็มและฐานราก</td>";
                TableStructure += "</tr>";

                foreach (var item in queryFooting)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ต้น</td>";
                    TableStructure += "</tr>";
                }

                // Beam
                var queryBeam = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "Beam"
                                select table;

                TableStructure += "<tr>";
                TableStructure += "<td>งานคาน</td>";
                TableStructure += "</tr>";

                foreach (var item in queryBeam)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ตารางเมตร</td>";
                    TableStructure += "</tr>";
                }

                // Floor
                var queryFloor = from table in dt.AsEnumerable()
                                 where table.Field<string>("Material") == "Floor"
                                 select table;

                TableStructure += "<tr>";
                TableStructure += "<td>งานแผ่นพื้นสำเร็จรูป</td>";
                TableStructure += "</tr>";

                foreach (var item in queryFloor)
                {
                    TableStructure += "<tr>";
                    TableStructure += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableStructure += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableStructure += "<td>ตารางเมตร</td>";
                    TableStructure += "</tr>";
                }

                // End
                TableStructure += "</table>";

            }

            {
                // Table Architecture
                TableArchitecture += "<table>";
                TableArchitecture += "<tr>";
                TableArchitecture += "<td>งาน</td>";
                TableArchitecture += "<td>จำนวน</td>";
                TableArchitecture += "<td>หน่วย</td>";
                TableArchitecture += "</tr>";

                // Roof
                var queryRoof = from table in dt.AsEnumerable()
                                where table.Field<string>("Material") == "Roof"
                                select table;

                TableArchitecture += "<tr>";
                TableArchitecture += "<td>งานหลังคาและวัสดุมุง</td>";
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
                TableArchitecture += "<td>งานผนังก่อและฉาบ</td>";
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
                TableArchitecture += "<td>งานผิวตกแต่งพื้น</td>";
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
                TableArchitecture += "<td>งานสุขภัณฑ์</td>";
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
                TableArchitecture += "<td>งานฝ้าเพดาน</td>";
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
                TableArchitecture += "<td>งานสี</td>";
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
                TableArchitecture += "<td>งานรั้ว</td>";
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
                TableArchitecture += "<td>งานประตูหน้าต่าง</td>";
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

                // End
                TableArchitecture += "</table>";

            }

            {
                // Table System
                TableSystem += "<table>";
                TableSystem += "<tr>";
                TableSystem += "<td>งาน</td>";
                TableSystem += "<td>จำนวน</td>";
                TableSystem += "<td>หน่วย</td>";
                TableSystem += "</tr>";

                // Sanitation
                var querySanitation = from table in dt.AsEnumerable()
                                      where table.Field<string>("Material") == "Sanitation"
                                      select table;

                TableSystem += "<tr>";
                TableSystem += "<td>งานสุขาภิบาลและระบายน้ำ</td>";
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
                TableSystem += "<td>งานประปา</td>";
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
                TableSystem += "<td>งานไฟฟ้า</td>";
                TableSystem += "</tr>";

                foreach (var item in queryFooting)
                {
                    TableSystem += "<tr>";
                    TableSystem += "<td>" + item.Field<string>("footingType").ToString() + "</td>";
                    TableSystem += "<td>" + item.Field<double>("numpole") + "</td>";
                    TableSystem += "<td>ชุด</td>";
                    TableSystem += "</tr>";
                }

                // End
                TableSystem += "</table>";

            }

        }
    }
}