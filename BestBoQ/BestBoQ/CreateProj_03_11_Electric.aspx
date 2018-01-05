<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_11_Electric.aspx.cs" Inherits="BestBoQ.CreateProj_03_11_Electric" %>

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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemNo")%>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("unit")%>'></asp:Label>
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
