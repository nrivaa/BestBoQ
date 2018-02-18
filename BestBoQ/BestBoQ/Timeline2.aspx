<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline2.aspx.cs" Inherits="BestBoQ.Timeline2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script src="https://github.highcharts.com/gantt/highcharts-gantt.src.js"></script>

        <div id="container" style="width: 2000px"></div>
    </form>

    <script type="text/javascript">
        var today = new Date(<%=Year%>,<%=Month%>,<%=Day%>),
            day = 1000 * 60 * 60 * 24;

        // Set to 00:00:00:000 today
        today.setUTCHours(0);
        today.setUTCMinutes(0);
        today.setUTCSeconds(0);
        today.setUTCMilliseconds(0);
        today = today.getTime();

        // THE CHART
        Highcharts.ganttChart('container', {
            title: {
                text: 'แผนงานการก่อสร้าง'
            },
            xAxis: {
                currentDateIndicator: false,
                min: today - 3 * day,
                max: today + (<%=TotalDate%> + 3)  * day
            },

            /*
            plotOptions: {
                gantt: {
                    pathfinder: {
                        type: 'simpleConnect'
                    }
                }
            },
            */

            series: [{
                name: '<%=ProjectName%>',
                data: [{
                    taskName: '1. เซ็นสัญญาว่าจ้างก่อสร้าง',
                    id: 'phase1',
                    start: today - <%=Phase1Start%> * day,
                    end: today + <%=Phase1Stop%> * day
                }, {
                    taskName: '2. ฐานรากงานคานคอดินแล้วเสร็จ',
                    id: 'phase2',
                    start: today + <%=Phase2Start%> * day,
                    end: today + <%=Phase2Stop%> * day
                }, {
                    taskName: '3. งานโครงสร้างทั้งหมดแล้วเสร็จ',
                    id: 'phase3',
                    start: today + <%=Phase3Start%> * day,
                    end: today + <%=Phase3Stop%> * day
                }, {
                    taskName: '4. งานโครงสร้าง/ยิงไม้เชิงขาย/งานพื้นคสลแล้วเสร็จ',
                    id: 'phase4',
                    start: today + <%=Phase4Start%> * day,
                    end: today + <%=Phase4Stop%> * day
                }, {
                    taskName: '5. งานก่อผนัง/วางระบบไฟ/วางระบบน้ำแล้วเสร็จ',
                    id: 'phase5',
                    start: today + <%=Phase5Start%> * day,
                    end: today + <%=Phase5Stop%> * day
                }, {
                    taskName: '6. งานฉาบผนัง/งานมุงหลังคาแล้วเสร็จ',
                    id: 'phase6',
                    start: today + <%=Phase6Start%> * day,
                    end: today + <%=Phase6Stop%> * day
                }, {
                    taskName: '7. งานสีรองพื้น/งานโครงฝ้า/ร้อยสายไฟ/วางระบบประปาแล้วเสร็จ',
                    id: 'phase7',
                    start: today + <%=Phase7Start%> * day,
                    end: today + <%=Phase7Stop%> * day
                }, {
                    taskName: '8. งานกระเบื้องแล้วเสร็จ/งานประตูหน้าต่างแล้วเสร็จ/งานฝ้าแล้วเสร็จ',
                    id: 'phase8',
                    start: today + <%=Phase8Start%> * day,
                    end: today + <%=Phase8Stop%> * day
                }, {
                    taskName: '9. งานสี/งานไฟ/งานสุขภัณฑ์/และงานอื่นๆ แล้วเสร็จ',
                    id: 'phase9',
                    start: today + <%=Phase9Start%> * day,
                    end: today + <%=Phase9Stop%> * day
                }, {
                    taskName: '10. งานเก็บรายละเอียดส่งงาน',
                    id: 'phase10',
                    start: today + <%=Phase10Start%> * day,
                    end: today + <%=Phase10Stop%> * day
                }]
            }]
        });
    </script>
</body>
</html>
