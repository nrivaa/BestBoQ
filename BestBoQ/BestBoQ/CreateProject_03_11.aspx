<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_11.aspx.cs" Inherits="BestBoQ.CreateProject_03_11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="progress" runat="server">
    <div class="row">
        <div class="col-md-12 progress-tracker-container">
            <ul class="progress-tracker">
                <li class="step-0-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">1. Enter project details</span> <small class="visible-xs-block visible-sm-block mobile">Details</small> </li>
                <li class="step-1-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">2. Select project type</span> <small class="visible-xs-block visible-sm-block mobile">Type</small> </li>
                <li class="step-2-title progress-tracker-active"><span class="visible-md-block visible-lg-block desktop">3. Choose project materials</span> <small class="visible-xs-block visible-sm-block mobile">Materials</small> </li>
                <li class="step-3-title"><span class="visible-md-block visible-lg-block desktop">4. Review project summary</span> <small class="visible-xs-block visible-sm-block mobile">Summary!</small> </li>
            </ul>

            <div class="alert alert-success confident-message text-center nearly-done" data-bind="html: progressTracker.nearlyDoneText"><strong>11. งานระบบไฟฟ้า</strong></div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>งานระบบไฟฟ้า</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="row">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="col-xs-6 col-md-4">
                                        <div class="thumbnail">
                                            <div class="caption">
                                                <div class="form-group has-feedback">
                                                    <label for="TextBox1" class="control-label">
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("itemNo")%>'></asp:Label>. 
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("detail")%>'></asp:Label></label>
                                                    <div class="input-group">
                                                        <asp:TextBox ID="TextBox1" data-inputmask="'alias': 'integer'" Text="0" CssClass="form-control" data-validation="number" runat="server"></asp:TextBox>
                                                        <span class="input-group-addon">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("unit")%>'></asp:Label></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-6">
                <a href="CreateProject_03_10?id=<%=param_projid%>" class="btn btn-default">Back to Previous Step</a>
            </div>
            <div class="col-xs-6 text-right">
                <asp:Button ID="btnSubmit" OnClientClick=" return $('.form').isValid()" OnClick="btnSubmit_Click" CssClass="btn btn-green" runat="server" Text="Next" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
</asp:Content>
