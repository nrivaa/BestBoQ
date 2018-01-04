<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_10_Ceiling.aspx.cs" Inherits="BestBoQ.CreateProj_03_10_Ceiling" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            INDOOR
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="row">   
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("ceilingPart")%>'></asp:Label>
                        <asp:Image ID="imgPic1" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:RadioButton ID="RadioButton1" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            OUTDOOR
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("ceilingPart")%>'></asp:Label>
                        <asp:Image ID="imgPic2" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:RadioButton ID="RadioButton2" runat="server" />
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
