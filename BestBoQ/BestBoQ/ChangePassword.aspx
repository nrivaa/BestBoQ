<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="BestBoQ.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section style="padding: 100px 0px;">
        <div class="kleanity-page-title-wrap  kleanity-style-medium kleanity-left-align">
            <div class="kleanity-header-transparent-substitute"></div>
            <div class="kleanity-page-title-overlay"></div>
            <div class="kleanity-page-title-container container">
                <div class="kleanity-page-title-content kleanity-item-pdlr">
                    <h2>ระบบเปลี่ยนรหัสผ่าน</h2>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <div class="form-group has-feedback">
                            <!----- username -------------->
                            <asp:TextBox ID="tbCuser" CssClass="form-control" runat="server" placeholder="Username" data-inputmask-regex="[a-za-zA-Z0-9]*" autocomplete="off"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-user  form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <!----- Old Password -------------->
                            <asp:TextBox ID="tbCold" CssClass="form-control" runat="server" placeholder="Old Password" type="password" autocomplete="off" ></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <!----- Password -------------->
                            <asp:TextBox ID="tbCnew" CssClass="form-control" runat="server" placeholder="New Password" type="password" autocomplete="off" ></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <!----- Confirm Password -------------->
                            <asp:TextBox ID="tbCcon" CssClass="form-control" runat="server" placeholder="Confirm New Password" type="password" autocomplete="off" ></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <asp:Button ID="btnChange" CssClass="btn btn-success btn-block" runat="server" Text="Change Password" OnClick="btnReNew_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
