<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractCover.aspx.cs" Inherits="BestBoQ.ContractCover" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>สัญญาจ้างเหมาก่อสร้าง
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=HomeName %>
                    </td>
                </tr>
                <tr>
                    <td>........................................................................................
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=Customer %>
                    </td>
                </tr>
                <tr>
                    <td>(ผู้ว่าจ้าง)
                    </td>
                </tr>
                <tr>
                    <td>กับ
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=CompanyName %>
                    </td>
                </tr>
                <tr>
                    <td>(ผู้รับจ้าง)
                    </td>
                </tr>
                <tr>
                    <td>สัญญาเลขที่ <%=ContractID %>
                    </td>
                </tr>
                <tr>
                    <td>__________________________________
                    </td>
                </tr>
                <tr>
                    <td>สัญญาจ้างเหมาก่อสร้าง
                    </td>
                </tr>
                <tr>
                    <td>
                        <%=ProjectName %>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
