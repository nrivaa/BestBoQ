<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Appendix.aspx.cs" Inherits="BestBoQ.Appendix" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .indented {
            padding-left: 50pt;
            padding-right: 50pt;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <img src="theme/img/logo.png" style="display:block; margin-left:auto; margin-right:auto;"/>
            <h1 style="text-align:center;">ภาคผนวก ก: การชำระเงินค่าก่อสร้าง</h1>
            <h2 style="text-align:center;">เงื่อนไขการชำระเงินค่าก่อสร้าง<%=ProjectType%> พื้นที่ใช้สอย <%=Area %> ตารางเมตร</h2>
            <p class="indented">ทั้ง “ผู้ว่าจ้าง” และ “ผู้รับจ้าง” ได้ตกลงราคาค่าก่อสร้าง รวมทั้งค่าวัสดุ อุปกรณ์ และค่าแรงทั้งหมด เป็นจำนวนเงินทั้งสิ้น <%=AmountNumber %> บาท (<%=AmountText %>)</p>
            <p class="indented">โดย “ผู้ว่าจ้าง” ตกลงแบ่งชำระเงินค่าก่อสร้าง เป็นงวดๆดังนี้</p>
            <table style="margin-left:auto; margin-right:auto;">
                <tr>
                    <td>งวดที่ 1 <%=Term1Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term1Amount %> บาท</td>
                </tr>
                <tr>
                    <td>เซ็นสัญญาว่าจ้างก่อสร้าง</td>
                    <td>(<%=Term1Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 2 <%=Term2Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term2Amount %> บาท</td>
                </tr>
                <tr>
                    <td>ฐานรากงานคานคอดินแล้วเสร็จ</td>
                    <td>(<%=Term2Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 3 <%=Term3Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term3Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานโครงสร้างทั้งหมดแล้วเสร็จ</td>
                    <td>(<%=Term3Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 4 <%=Term4Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term4Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานโครงหลังคา/ยิงไม้เชิงชาย/งานพื้นคสลแล้วเสร็จ</td>
                    <td>(<%=Term4Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 5 <%=Term5Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term5Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานก่อผนัง/วางระบบไฟ/วางระบบน้ำแล้วเสร็จ</td>
                    <td>(<%=Term5Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 6 <%=Term6Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term6Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานฉาบผนัง/งานมุงหลังคาแล้วเสร็จ</td>
                    <td>(<%=Term6Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 7 <%=Term7Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term7Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานสีรองพื้น/งานโครงฝ้า/ร้อยสายไฟ/วางระบบประปาแล้วเสร็จ</td>
                    <td>(<%=Term7Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 8 <%=Term8Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term8Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานกระเบื้องแล้วเสร็จ/งานประตูหน้าต่างแล้วเสร็จ/งานฝ้าแล้วเสร็จ</td>
                    <td>(<%=Term8Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 9 <%=Term9Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term9Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานสี/งานไฟ/งานสุขภัณฐ์/และงานอื่นๆแล้วเสร็จ</td>
                    <td>(<%=Term9Text %>)</td>
                </tr>
                <tr>
                    <td>งวดที่ 10 <%=Term10Pct %> ของค่าก่อสร้างทั้งหมด เป็นจำนวนเงิน</td>
                    <td><%=Term10Amount %> บาท</td>
                </tr>
                <tr>
                    <td>งานเก็บรายละเอียดส่งงาน</td>
                    <td>(<%=Term10Text %>)</td>
                </tr>
            </table>
            <p class="indented">“ผู้รับจ้าง” ตกลงเริ่มทำการก่อสร้าง ในวันที่ <%=StartDate %> และจะดำเนินการให้แล้วเสร็จสมบูรณ์ภายในวันที่ <%=StopDate %> (เป็นระยะเวลา <%=Period %> เดือน)</p>
            <p class="indented">**บริษัทฯ ขอสงวนสิทธิ์ในการเปลี่ยนแปลงเงื่อนไขการชำระเงินได้ตามความเหมาะสม**</p>

            <table cellpadding="10" style="margin-left:auto; margin-right:auto; text-align:center;">
                <tr>
                    <td>ลงชื่อ .............................................. ผู้ว่าจ้าง
                    </td>
                    <td>ลงชื่อ .............................................. ผู้รับจ้าง 
                    </td>
                </tr>
                <tr>
                    <td>(<%=ClientName %>)      
                    </td>
                    <td>(<%=VendorName %>)
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=ClientComapny %>
                    </td>
                    <td>
                        <%=VendorCompany %>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
