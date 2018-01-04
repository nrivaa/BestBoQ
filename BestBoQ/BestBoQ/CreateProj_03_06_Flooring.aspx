<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_06_Flooring.aspx.cs" Inherits="BestBoQ.CreateProj_03_06_Flooring" %>

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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("flooringType")%>'></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        ตรม.
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
