<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FrogetPassword.aspx.cs" Inherits="BestBoQ.FrogetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="navcontent" runat="server">
    <ul class="nav navbar-nav navbar-right">
        <li>
            <a href="Default.aspx#feature">รู้จัก bestboq</a>
        </li>
        <li>
            <a href="Default.aspx#feature2">bestboq ทำอะไรได้บ้าง?</a>
        </li>
        <li>
            <a href="Howto.aspx">เรียนรู้การใช้งาน</a>
        </li>
        <li>
            <a href="Default.aspx?cmd=register">สมัครใช้งาน</a>
        </li>
        <li class="btn-trial">
            <a href="Default.aspx?cmd=login">เข้าสู่ระบบ</a>
        </li>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">
    <section style="padding: 100px 0px;">
        <div class="kleanity-page-title-wrap  kleanity-style-medium kleanity-left-align">
            <div class="kleanity-header-transparent-substitute"></div>
            <div class="kleanity-page-title-overlay"></div>
            <div class="kleanity-page-title-container container">
                <div class="kleanity-page-title-content kleanity-item-pdlr">
                    <h2>ระบบแจ้งลืมรหัสผ่าน</h2>
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
                            <asp:TextBox ID="tbFuser" CssClass="form-control" runat="server" placeholder="Username" data-inputmask-regex="[a-za-zA-Z0-9]*" autocomplete="off"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-user  form-control-feedback"></span>
                        </div>
                        <%--<div class="form-group has-feedback">
                            <!----- Email -------------->
                            <asp:TextBox ID="tbFemail" CssClass="form-control" runat="server" placeholder="Email" data-validation="email" data-inputmask="'alias': 'email'" autocomplete="off"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-envelope  form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <!----- id card -------------->
                            <asp:TextBox ID="tbFidcard" CssClass="form-control" runat="server" placeholder="id Card or Tax id" data-validation="required" autocomplete="off" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>--%>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Button ID="btnForget" CssClass="btn btn-green btn-block btn-flat" runat="server" Text="แจ้งลืมรหัส" OnClick="btnForget_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <%--                <div class="col-md-6">
                    <div class="form-group">
                        <div class="form-group has-feedback">
                            <!----- Password -------------->
                            <asp:TextBox ID="tbFpassword" CssClass="form-control" runat="server" placeholder="Password" type="password" autocomplete="off" Visible="false"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>
                        <div class="form-group has-feedback">
                            <!----- Confirm Password -------------->
                            <asp:TextBox ID="tbFcpassword" CssClass="form-control" runat="server" placeholder="Confirm Password" type="password" autocomplete="off" Visible="false"></asp:TextBox>
                            <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                            <!---Alredy exists  ! -->
                            <span class="fa fa-lock  form-control-feedback"></span>
                        </div>
                    </div>
                     <div class="row">
                            <div class="col-xs-12">
                                <asp:Button ID="btnReNew" CssClass="btn btn-red btn-block btn-flat" runat="server" Text="Confirm New Password" Visible="false" OnClick="btnReNew_Click" />
                            </div>
                        </div>
                </div>--%>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label ID="lbResult" runat="server" Text="#NA" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
