<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_09_Plumbing.aspx.cs" Inherits="BestBoQ.CreateProj_03_09_Plumbing" %>

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
                        ท่อน้ำ Type -
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("plumbingType")%>'></asp:Label>
                        ท่อน้ำดี
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("pipe1")%>'></asp:Label>
                        ท่อน้ำเสีย
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("pipe2")%>'></asp:Label>
                        ท่อน้ำทิ้ง
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("pipe3")%>'></asp:Label>
                        <asp:RadioButton ID="RadioButton1" runat="server" />
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
