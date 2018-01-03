<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_02_Beam.aspx.cs" Inherits="BestBoQ.CreateProj_03_02_Beam" %>

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
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("beamType")%>'></asp:Label>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        ต้น
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("beamType")%>'></asp:Label>
                        จากมาตรฐานงานออกแบบ รับน้ำหนักต่อต้น <asp:Label ID="Label3" runat="server" Text='<%# Eval("weightSupport")%>'></asp:Label> KG/SQM <asp:Label ID="Label4" runat="server" Text='<%# Eval("recomment")%>'></asp:Label>
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
