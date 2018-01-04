<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_05_Wall.aspx.cs" Inherits="BestBoQ.CreateProj_03_05_Wall" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddWall" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddWall_SelectedIndexChanged"></asp:DropDownList>
            <asp:Image ID="imgWall" runat="server" />
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
