<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="BestBoQ.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Username : 
            <asp:Label ID="lbUsername" runat="server" Text="#N/A"></asp:Label>
        </div>
        <div>
            <asp:RadioButtonList ID="rbType" runat="server" Enabled="false">
                <asp:ListItem>บุคคล</asp:ListItem>
                <asp:ListItem>บริษัท</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            Email : 
            <asp:Label ID="lbEmail" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbEmail" runat="server" placeholder="Email" Visible="false"></asp:TextBox>
        </div>
        <div>
            Mobile Number : 
            <asp:Label ID="lbMobile" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbMobile" runat="server" placeholder="Mobile Number" Visible="false"></asp:TextBox>
        </div>
        <div>
            Name/Company Name : 
            <asp:Label ID="lbName" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbName" runat="server" placeholder="Name/Company Name" Visible="false"></asp:TextBox>
        </div>
        <div>
            Address : 
            <asp:Label ID="lbAddress" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbAddress" runat="server" placeholder="Address" Visible="false"></asp:TextBox>
        </div>
        <div>
            ID Card : 
            <asp:Label ID="lbId" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbId" runat="server" placeholder="ID Card" Visible="false"></asp:TextBox>
        </div>
        <div>
            TAX ID : 
            <asp:Label ID="lbTax" runat="server" Text="#N/A"></asp:Label>
            <asp:TextBox ID="tbTax" runat="server" placeholder="TAX" Visible="false"></asp:TextBox>
        </div>
        <div>
            <asp:Button ID="btnEdit" runat="server" Text="Edit Profile" OnClick="btnEdit_Click"/>
            <asp:Button ID="btnComfirm" runat="server" Text="Confirm Profile" Visible="false" OnClick="btnComfirm_Click" />
        </div>
    </form>
</body>
</html>
