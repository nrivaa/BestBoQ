<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BestBoQ.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <h2>My Project</h2>
                    <p>
                    </p>
                    <hr class="bottom-line" />
                </div>
            </div>
            <div>
                <asp:GridView ID="gvData" CssClass="table table-condensed" GridLines="None" runat="server" AutoGenerateColumns="False">
                    <%--OnRowCommand="gvData_RowCommand"--%>
                    <Columns>
                        <%--                        
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <%--                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Image ID="imgType" runat="server" ImageUrl='<%# "~/" + Eval("type") %>' Height="40px" Width="40px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="HomeType">
                            <ItemTemplate>
                                <asp:Image ID="imgOnly" runat="server" ImageUrl='<%#  Eval("homepic") %>' Height="70px" Width="70px" />
                                <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("projectid") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="projectname" HeaderText="ProjectName" />
                        <asp:BoundField DataField="customername" HeaderText="CustomerName" />
                        <asp:BoundField DataField="contractid" HeaderText="contractName" />
<%--                        <asp:BoundField DataField="Priority" HeaderText="Priority" />
                        <asp:BoundField DataField="region" HeaderText="Region" />
                        <asp:BoundField DataField="zone" HeaderText="Zone" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="user" HeaderText="user" />--%>
                        <asp:BoundField DataField="transdate" HeaderText="CreateDate" />
                        <asp:HyperLinkField DataNavigateUrlFields="projectid" DataNavigateUrlFormatString="Project_Detail.aspx?id={0}" HeaderText="View" Text="View" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
