<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Project_Detail.aspx.cs" Inherits="BestBoQ.Project_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                  <%--  <h2>My Project</h2>
                    <p></p>
                    <hr class="bottom-line" />--%>
                </div>
            </div>
            <div>
                <div>
                    <asp:Image ID="img" runat="server" />
                </div>
                <div>
                    Project Name :
           
                    <asp:Label ID="lbProjName" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Customer Name :
           
                    <asp:Label ID="lbCusName" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Customer Address :
           
                    <asp:Label ID="lbCusAddress" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Project Type :
           
                    <asp:Label ID="lbProjType" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Project Start :
           
                    <asp:Label ID="lbProjStart" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Contract Name :
           
                    <asp:Label ID="lbContract" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    Total Price :
           
                    <asp:Label ID="lbTatalPrice" runat="server" Text="#N/A"></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnBoq" runat="server" Text="Create BOQ" />
                    <asp:Button ID="btnContract" runat="server" Text="Create Contract" />
                    <asp:Button ID="btnPayment" runat="server" Text="Create PayMent" />
                    <asp:Button ID="btnPlan" runat="server" Text="Create Plan" />
                    <asp:Button ID="btnReport" runat="server" Text="Create REport" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
