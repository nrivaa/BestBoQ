﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_05.aspx.cs" Inherits="BestBoQ.CreateProject_03_05" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="progress" runat="server">
    <div class="row">
        <div class="col-md-12 progress-tracker-container">
            <ul class="progress-tracker">
                <li class="step-0-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">1. Enter project details</span> <small class="visible-xs-block visible-sm-block mobile">Details</small> </li>
                <li class="step-1-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">2. Select project type</span> <small class="visible-xs-block visible-sm-block mobile">Type</small> </li>
                <li class="step-2-title progress-tracker-active"><span class="visible-md-block visible-lg-block desktop">3. Choose project materials</span> <small class="visible-xs-block visible-sm-block mobile">Materials</small> </li>
                <li class="step-3-title"><span class="visible-md-block visible-lg-block desktop">5. ระบบงานผนัง (Wall)</span> <small class="visible-xs-block visible-sm-block mobile">Summary!</small> </li>
            </ul>

            <div class="alert alert-success confident-message text-center nearly-done" data-bind="html: progressTracker.nearlyDoneText"><strong>5. ระบบงานผนัง (Wall)</strong></div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>ระบบงานผนัง (Wall)</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="row">
                            <div class="col-sm-4 text-center">
                                <asp:Image ID="imgWall" runat="server"  />
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group has-feedback">
                                    <label for="tbProjectName" class="control-label">ประเภทผนัง</label>
                                    <asp:DropDownList ID="ddWall" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddWall_SelectedIndexChanged" CssClass="form-control" data-validation="required"></asp:DropDownList>
                                    <p class="text-right"><small>ราคารวม 0 บาท</small></p>
                                </div>
                            </div>
                        </div>
                        <br />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6">
                <a href="CreateProject_03_04?id=<%=param_projid %>" class="btn btn-default">Back to Previous Step</a>
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