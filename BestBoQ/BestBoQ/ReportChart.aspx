<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportChart.aspx.cs" Inherits="BestBoQ.ReportChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ["corechart"] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            var data = google.visualization.arrayToDataTable([
                ['Task', 'Hours per Day'],
                ['ค่าวัสดุ 50% (50M)', 50],
                ['ค่าแรง 30% (30M)', 30],
                ['ค่าดำเนินงาน 20% (20M)', 20]
            ]);

            var options = {
                title: 'รายการแยกต้นทุน/ค่าของ/ค่าแรง/ค่าดำเนินงาน',
                pieHole: 0.4,
                legend: { position: 'bottom' },
                pieSliceText: 'label'
            };

            var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
            chart.draw(data, options);
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <%--<div id="donutchart" style="height: 500px;"></div>--%>
        <script src="https://code.highcharts.com/highcharts.js"></script>
        <script src="https://code.highcharts.com/modules/exporting.js"></script>

        <div id="container" <%-- style="min-width: 310px; height: 400px; max-width: 600px; margin: 0 auto"--%>></div>

        <script type="text/javascript">

            Highcharts.chart('container', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: 0,
                    plotShadow: false
                },
                title: {
                    //useHTML: true,
                    text: 'รายการแยกต้นทุน/ค่าของ/ค่าแรง/ค่าดำเนินงาน',
                    style:
                    {
                        fontSize: '32px',
                        fontWeight: 'bold'
                    }
                    //align: 'center',
                    //verticalAlign: 'top'
                    //y: 40
                },
                tooltip: {
                    enabled: false
                    //pointFormat: '{series.name}'
                },
                plotOptions: {
                    pie: {
                        dataLabels: {
                            enabled: true,
                            //distance: -50,
                            style: {
                                fontWeight: 'bold',
                                color: 'black',
                                fontSize: '20px'
                            },
                            useHTML: true
                        }
                        //startAngle: -90,
                        //endAngle: 90,
                        //center: ['50%']
                    }
                },
                series: [{
                    type: 'pie',
                    name: 'Browser share',
                    innerSize: '50%',
                    data: [
                        ['<div style="text-align:center;"><div>ค่าวัสดุ 50M</div><div>(50%)</div></div>', 50],
                        ['<div style="text-align:center;"><div>ค่าแรง 30M</div><div>(30%)</div></div>', 30],
                        ['<div style="text-align:center;"><div>ค่าดำเนินงาน 20M</div><div>(20%)</div></div>', 20]
                    ]
                }]
            });
        </script>
    </form>
</body>
</html>
