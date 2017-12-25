<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_01_Desc.aspx.cs" Inherits="BestBoQ.CreateProj_01_Desc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="tbProjectName" runat="server" placeholder="ชื่อโครงการ"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbCustomerName" runat="server" placeholder="ชื่อลูกค้า"></asp:TextBox>
        </div>
        <div>
            <asp:DropDownList ID="ddProjectType" runat="server"></asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="tbStartProject" runat="server" placeholder="วันที่เริ่มโครงการ"></asp:TextBox>
        </div>
        <div>
            <asp:DropDownList ID="ddCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCountry_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <asp:DropDownList ID="ddProvince" runat="server"></asp:DropDownList>
        </div>
        <div>
            <asp:TextBox ID="tbAddress" runat="server" placeholder="สถานที่ตั้งโครงการ"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="บันทึกและถัดไป" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
