<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_12_Color.aspx.cs" Inherits="BestBoQ.CreateProj_03_12_Color" %>

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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("colorID")%>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Image ID="imgPic1" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:RadioButton ID="RadioButton1" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:Label ID="lb1" runat="server" ></asp:Label>
            <br />
            <asp:Label ID="lb2" runat="server" ></asp:Label>
            <br />
            <asp:Label ID="lb3" runat="server" ></asp:Label>
            <br />
            <asp:Label ID="lbTotal" runat="server" ></asp:Label>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>
