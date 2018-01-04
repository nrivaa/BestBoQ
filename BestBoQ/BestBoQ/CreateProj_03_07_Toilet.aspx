<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_07_Toilet.aspx.cs" Inherits="BestBoQ.CreateProj_03_07_Toilet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Image ID="imgPic" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        ห้องน้ำ Type -
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("toiletType")%>'></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        ห้อง
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
