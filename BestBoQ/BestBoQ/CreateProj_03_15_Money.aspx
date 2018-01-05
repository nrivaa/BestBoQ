<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateProj_03_15_Money.aspx.cs" Inherits="BestBoQ.CreateProj_03_15_Money" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:textbox runat="server" id="tbfree" placeholder="โปรดระบุค่าดำเนินการเป็น %"></asp:textbox>
        </div>
        <div>
            <asp:textbox runat="server" id="tbpromotion" placeholder="โปรดระบุส่วนลดเป็น %"></asp:textbox>
        </div>
        <div>
            <asp:textbox runat="server" id="tbdetail" placeholder="โปรดระบุรายละเอียดอื่นๆ"></asp:textbox>
            <asp:textbox runat="server" id="tbother" placeholder="เป็นจำนวนเงิน"></asp:textbox>
        </div>
        <div>
            <asp:Button ID="btnSubmit" runat="server" Text="Done" OnClick="btnSubmit_Click"/>
        </div>
    </form>
</body>
</html>

