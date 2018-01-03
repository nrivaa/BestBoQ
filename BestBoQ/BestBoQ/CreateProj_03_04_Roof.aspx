<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_04_Roof.aspx.cs" Inherits="BestBoQ.CreateProj_03_04_Roof" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddRoofStyle" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddRoofStype_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <asp:DropDownList ID="ddRoofType" runat="server"></asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
