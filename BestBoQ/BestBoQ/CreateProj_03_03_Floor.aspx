<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_03_Floor.aspx.cs" Inherits="BestBoQ.CreateProj_03_03_Floor" %>

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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("floorType")%>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server" Visible='<%# Eval("visible").ToString() == "true" ? true : false%>'></asp:TextBox>
                        <asp:Label ID="Label3" runat="server" Text="ห้อง" Visible='<%# Eval("visible").ToString() == "true" ? true : false%>'></asp:Label>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        ตรม.
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
