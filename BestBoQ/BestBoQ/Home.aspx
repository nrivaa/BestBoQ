<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BestBoQ.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Button ID="btnProfile" runat="server" Text="My Profile" PostBackUrl="~/Profile.aspx"/>
            </div>
            <div>
                <asp:Button ID="btnCreate" runat="server" Text="New Project" PostBackUrl="~/CreateProj_01_Desc.aspx"/>
            </div>
        </div>
    </form>
</body>
</html>
