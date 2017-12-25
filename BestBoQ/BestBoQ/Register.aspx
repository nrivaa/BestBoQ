<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="BestBoQ.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="tbUsername" runat="server" placeholder="Username"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbPassword" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbRepassword" runat="server" placeholder="Confirm Password" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButtonList ID="rbType" runat="server">
                <asp:ListItem>บุคคล</asp:ListItem>
                <asp:ListItem>บริษัท</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbMobile" runat="server" placeholder="Mobile Number"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbName" runat="server" placeholder="บริษัท/ชื่อ-นามสกุล"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbAlias" runat="server" placeholder="AliasName"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbAddress" runat="server" placeholder="Address"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbID" runat="server" placeholder="ID Card Number"></asp:TextBox>
        </div>
        <div>
            <asp:TextBox ID="tbTax" runat="server" placeholder="เลขประจำตัวผู้เสียภาษี"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click"  />
        </div>
    </form>
</body>
</html>
