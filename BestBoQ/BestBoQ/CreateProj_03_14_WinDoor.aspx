<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_14_WinDoor.aspx.cs" Inherits="BestBoQ.CreateProj_03_14_WinDoor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>ประตูภายใน</h2>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Image ID="imgPic" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("windoorType")%>'></asp:Label>
                        ราคา 
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cost_item")%>'></asp:Label>
                        ชุด
                        <asp:RadioButton ID="RadioButton1" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <h2>ประตูห้องน้ำ</h2>
            <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Image ID="imgPic" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("windoorType")%>'></asp:Label>
                        ราคา 
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cost_item")%>'></asp:Label>
                        ชุด
                        <asp:RadioButton ID="RadioButton1" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <h2>หน้าต่าง</h2>
            <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                    <div class="row">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                        <asp:Image ID="imgPic" CssClass="img-responsive img-thumbnail" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("windoorType")%>'></asp:Label>
                        ราคา 
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("cost_item")%>'></asp:Label>
                        ชุด
                        <asp:RadioButton ID="RadioButton1" runat="server" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div>
            <h2>สรุปจำนวนประตูหน้าต่างที่ต้องใช้</h2>
            <br />
            จำนวนประตูภายในที่ต้องใช้ : 
            <asp:Label ID="lbdoor1" runat="server"></asp:Label>
            <br />
            จำนวนประตูห้องน้ำที่ต้องใช้ : 
            <asp:Label ID="lbdoor2" runat="server"></asp:Label>
            <br />
            จำนวนหน้าต่างที่ต้องใช้ : 
            <asp:Label ID="lbwin1" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Next" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
