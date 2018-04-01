<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="boq.aspx.cs" Inherits="BestBoQ.boq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="left: 0; line-height: 200px; margin-top: -100px; position: absolute; text-align: center; top: 50%; width: 100%;">
            <h1>Project Information</h1>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>รายละเอียดโครงการ</h1>
            <h4>Project Name : <%=ProjectName %></h4>
            <h4>Contact Name : <%=ContactName %></h4>
            <h4>Customer Name : <%=CustomerName %></h4>
            <h4>Customer Address : <%=ContactAdd %></h4>
            <h4>Project Type : <%=ProjectType %></h4>
            <h4>พื้นที่ใช้สอย: <%=TotalArea %> ตารางเมตร</h4>
            <h4>จำนวนห้อง: <%=CountBedRoom %> ห้องนอน <%=CountBathRoom %> ห้องน้ำ</h4>
            <h4>Project Start : <%=ProjectStart %></h4>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>Bill of quantity</h1>
            <h2>บัญชีแสดงปริมาณวัสดุ</h2>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>งานโครงสร้าง</h1>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>Bill of quantity</h1>
            <%=TableStructure %>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>งานสถาปัตย์</h1>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>Bill of quantity</h1>
            <%=TableArchitecture %>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>งานระบบ</h1>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>Bill of quantity</h1>
            <%=TableSystem %>
        </div>
        <div style="page-break-after: always;"></div>
    </form>
</body>
</html>
