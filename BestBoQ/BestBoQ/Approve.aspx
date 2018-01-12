<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="BestBoQ.Approve" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:DropDownList ID="ddName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddName_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div>
            <asp:Label ID="lbUsername" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbType" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbEmail" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbCompany" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbRegisterdate" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lb1" runat="server" Text="CreateProject" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb1" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Label ID="lb2" runat="server" Text="SummaryItem" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb2" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Label ID="lb3" runat="server" Text="Contract" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb3" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Label ID="lb4" runat="server" Text="Term" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb4" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Label ID="lb5" runat="server" Text="MasterPlan" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb5" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Label ID="lb6" runat="server" Text="Report" Visible="false"></asp:Label>
            <asp:CheckBox ID="cb6" runat="server" Visible="false"/>
        </div>
        <div>
            <asp:Button ID="btnApprove" runat="server" Text="Button" OnClick="btnApprove_Click"/>
        </div>
    </form>
</body>
</html>
