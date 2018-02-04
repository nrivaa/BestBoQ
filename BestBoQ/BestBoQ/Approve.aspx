<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Approve.aspx.cs" Inherits="BestBoQ.Approve" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <h2>Account Management</h2>
                    <hr class="bottom-line" />
                </div>
                <div class="col-sm-12" role="form">
                    <div class="form-group">
                        <label>Username:</label>
                        <asp:DropDownList ID="ddName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddName_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row" visible="false" id="blockDetail" runat="server">
                    <div class="col-sm-6" role="form">
                        <h5>User Details</h5>
                        <div class="form-group radio-type">
                            <label class="control-label">Username:</label>
                            <asp:Label ID="lbUsername" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Type:</label>
                            <asp:Label ID="lbType" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="control-label">E-mail:</label>
                            <asp:Label ID="lbEmail" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Company:</label>
                            <asp:Label ID="lbCompany" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                        <div class="form-group">
                            <label class="control-label">Register date:</label>
                            <asp:Label ID="lbRegisterdate" runat="server" Text="" Visible="false"></asp:Label>
                        </div>
                         <div  class="form-group">
                             &nbsp;
                        </div>
                        <div>
                            <asp:Button ID="btnApprove" CssClass="light-form-button light" runat="server" Text="Submit" OnClick="btnApprove_Click" />
                        </div>
                    </div>
                    <div class="col-sm-6" role="form">
                        <h5>Permissions</h5>
                        <div class="form-group radio-type">
                            <label>CreateProject:</label>
                            <asp:CheckBox ID="cb1" runat="server" Visible="false" />
                        </div>
                        <div class="form-group radio-type">
                            <label>SummaryItem:</label>
                            <asp:CheckBox ID="cb2" runat="server" Visible="false" />
                        </div>
                        <div class="form-group radio-type">
                            <label>Contract:</label>
                            <asp:CheckBox ID="cb3" runat="server" Visible="false" />
                        </div>
                        <div class="form-group radio-type">
                            <label>Term:</label>
                            <asp:CheckBox ID="cb4" runat="server" Visible="false" />
                        </div>
                        <div class="form-group radio-type">
                            <label>MasterPlan:</label>
                            <asp:CheckBox ID="cb5" runat="server" Visible="false" />
                        </div>
                        <div class="form-group radio-type">
                            <label>Report:</label>
                            <asp:CheckBox ID="cb6" runat="server" Visible="false" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
