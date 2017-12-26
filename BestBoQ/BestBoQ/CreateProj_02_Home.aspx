<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_02_Home.aspx.cs" Inherits="BestBoQ.CreateProj_02_Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddHomeGroup" runat="server" OnSelectedIndexChanged="ddHomeGroup_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div>
            <asp:DropDownList ID="ddHomeType" runat="server"></asp:DropDownList>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
