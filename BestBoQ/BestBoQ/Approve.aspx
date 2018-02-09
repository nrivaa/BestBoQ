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
                        <div class="form-group">
                            &nbsp;
                        </div>
                        <div>
                            <%--<asp:Button ID="btnApprove" CssClass="light-form-button light" runat="server" Text="Submit" OnClick="btnApprove_Click" />--%>
                        </div>
                    </div>
                    <div class="col-sm-6" role="form">
                        <h5>Permissions</h5>
                        <div class="form-group radio-type">
                            <label>CreateProject:</label>
                            <asp:CheckBox ID="cb1" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan1" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan1" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan1" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan1_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group radio-type">
                            <label>BoQ:</label>
                            <asp:CheckBox ID="cb2" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan2" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan2" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan2" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan2_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group radio-type">
                            <label>Contract:</label>
                            <asp:CheckBox ID="cb3" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan3" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan3" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan3" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan3_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group radio-type">
                            <label>Term:</label>
                            <asp:CheckBox ID="cb4" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan4" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan4" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan4" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan4_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group radio-type">
                            <label>MasterPlan:</label>
                            <asp:CheckBox ID="cb5" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan5" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan5" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan5" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan5_Click" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group radio-type">
                            <label>Report:</label>
                            <asp:CheckBox ID="cb6" runat="server" Visible="false" />
                            <div class="row">
                                <div class="col-sm-6">
                                    <asp:TextBox ID="tbStartCan6" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มใช้งาน"></asp:TextBox>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="tbPeriodCan6" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="จำนวนวัน"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnCan6" CssClass="btn btn-success" runat="server" Text="Update" OnClick="btnCan6_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="lib/datetimepicker/js/moment-with-locales.js"></script>
    <script src="lib/datetimepicker/js/bootstrap-datetimepicker.min.js"></script>
    <script>
        $(document).ready(function () {

            $('#<%=tbStartCan1.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
            $('#<%=tbStartCan2.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
            $('#<%=tbStartCan3.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
            $('#<%=tbStartCan4.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
            $('#<%=tbStartCan5.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
            $('#<%=tbStartCan6.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });
        });
    </script>
</asp:Content>
