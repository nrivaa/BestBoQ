<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="BestBoQ.Timeline" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="https://code.highcharts.com/highcharts.js"></script>
    <script src="https://code.highcharts.com/modules/xrange.js"></script>
    <script src="https://code.highcharts.com/modules/exporting.js"></script>
</head>
<body>
    <div id="container"></div>
    <script type="text/javascript">
        Highcharts.chart('container', {
            chart: {
                type: 'xrange'
            },
            title: {
                text: 'แผนงานการก่อสร้าง'
            },
            xAxis: {
                type: 'datetime',
                minorTickInterval: 'auto'
            },
            yAxis: {
                title: {
                    text: ''
                },
                categories: ['1. เซ็นสัญญาว่าจ้างก่อสร้าง', '2. ฐานรากงานคานคอดินแล้วเสร็จ', '3. งานโครงสร้างทั้งหมดแล้วเสร็จ', '4. งานโครงสร้าง/ยิงไม้เชิงขาย/งานพื้นคสลแล้วเสร็จ', '5. งานก่อผนัง/วางระบบไฟ/วางระบบน้ำแล้วเสร็จ', '6. งานฉาบผนัง/งานมุงหลังคาแล้วเสร็จ', '7. งานสีรองพื้น/งานโครงฝ้า/ร้อยสายไฟ/วางระบบประปาแล้วเสร็จ', '8. งานกระเบื้องแล้วเสร็จ/งานประตูหน้าต่างแล้วเสร็จ/งานฝ้าแล้วเสร็จ', '9. งานสี/งานไฟ/งานสุขภัณฑ์/และงานอื่นๆ แล้วเสร็จ', '10. งานเก็บรายละเอียดส่งงาน'],
                reversed: true,
            },
            series: [{
                name: 'Project 1',
                borderColor: 'gray',
                pointWidth: 20,
                data: [{
                    x: Date.UTC(2018, 2, 4),
                    x2: Date.UTC(2018, 2, 18),
                    y: 0,

                }, {
                    x: Date.UTC(2018, 2, 14),
                    x2: Date.UTC(2018, 4, 4),
                    y: 1
                }, {
                    x: Date.UTC(2018, 4, 1),
                    x2: Date.UTC(2018, 4, 8),
                    y: 2
                }, {
                    x: Date.UTC(2018, 4, 4),
                    x2: Date.UTC(2018, 5, 14),
                    y: 3
                }, {
                    x: Date.UTC(2018, 5, 10),
                    x2: Date.UTC(2018, 6, 18),
                    y: 4
                },
                {
                    x: Date.UTC(2018, 6, 14),
                    x2: Date.UTC(2018, 7, 9),
                    y: 5
                },
                {
                    x: Date.UTC(2018, 7, 5),
                    x2: Date.UTC(2018, 7, 30),
                    y: 6
                },
                {
                    x: Date.UTC(2018, 7, 26),
                    x2: Date.UTC(2018, 9, 3),
                    y: 7
                },
                {
                    x: Date.UTC(2018, 9, 1),
                    x2: Date.UTC(2018, 9, 24),
                    y: 8
                },
                {
                    x: Date.UTC(2018, 9, 20),
                    x2: Date.UTC(2018, 11, 13),
                    y: 9
                }],
                dataLabels: {
                    enabled: false
                }
            }]
        });

    </script>
</body>
</html>
