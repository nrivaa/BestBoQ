﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject.aspx.cs" Inherits="BestBoQ.CreateProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="progress" runat="server">
    <div class="row">
        <div class="col-md-12 progress-tracker-container">
            <ul class="progress-tracker">
                <li class="step-0-title progress-tracker-active"><span class="visible-md-block visible-lg-block desktop">1. Enter project details</span> <small class="visible-xs-block visible-sm-block mobile">Details</small> </li>
                <li class="step-1-title"><span class="visible-md-block visible-lg-block desktop">2. Select project type</span> <small class="visible-xs-block visible-sm-block mobile">Type</small> </li>
                <li class="step-2-title"><span class="visible-md-block visible-lg-block desktop">3. Choose project materials</span> <small class="visible-xs-block visible-sm-block mobile">Materials</small> </li>
                <li class="step-3-title"><span class="visible-md-block visible-lg-block desktop">4. Review project summary</span> <small class="visible-xs-block visible-sm-block mobile">Summary!</small> </li>
            </ul>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
            </div>
        </div>
    </div>
    <h3>Enter project details</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="form-group has-feedback">
                            <label for="tbProjectName" class="control-label">ชื่อโครงการ</label>
                            <asp:TextBox ID="tbProjectName" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="ชื่อโครงการ"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbCustomerName" class="control-label">ชื่อลูกค้า</label>
                            <asp:TextBox ID="tbCustomerName" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="ชื่อลูกค้า"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbCusID" class="control-label">หมายเลขบัตรประชาชนลูกค้า</label>
                            <asp:TextBox ID="tbCusID" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="เลขประจำตัวประชาชน" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbBirthdate" class="control-label">วันเดือนปีเกิดลูกค้า</label>
                            <asp:TextBox ID="tbBirthdate" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันเดือนปีเกิดลูกค้า"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbTelephoneCus" class="control-label">เบอร์โทรศัพท์ลูกค้า</label>
                            <asp:TextBox ID="tbTelephoneCus" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="Mobile Number" data-inputmask="'mask': '999-999-9999'"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbEmailCus" class="control-label">Email ลูกค้า</label>
                            <asp:TextBox ID="tbEmailCus" CssClass="form-control" autocomplete="off" data-validation="email" runat="server" placeholder="Email" data-inputmask="'alias': 'email'"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="ddProjectType" class="control-label">ประเภทโครงการ</label>
                            <asp:DropDownList ID="ddProjectType" CssClass="form-control" data-validation="required" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbStartProject" class="control-label">วันที่เริ่มโครงการ</label>
                            <asp:TextBox ID="tbStartProject" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="วันที่เริ่มโครงการ"></asp:TextBox>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="ddCountry" class="control-label">ประเทศ</label>
                            <asp:DropDownList ID="ddCountry" CssClass="form-control" data-validation="required" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddCountry_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="ddProvince" class="control-label">จังหวัด</label>
                            <asp:DropDownList ID="ddProvince" CssClass="form-control" data-validation="required" runat="server"></asp:DropDownList>
                        </div>
                        <div class="form-group has-feedback">
                            <label for="tbAddress" class="control-label">สถานที่ตั้งโครงการ</label>
                            <asp:TextBox ID="tbAddress" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="สถานที่ตั้งโครงการ"></asp:TextBox>
                        </div>
                         <div class="form-group has-feedback">
                            <label for="rbCostFunction" class="control-label">ระบบช่วยเลือกวัสดุ</label>
                            <asp:RadioButtonList ID="rbCostFunction" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Value="" Selected="True">เลือกด้วยตัวเอง</asp:ListItem>
                                <asp:ListItem Value="min">ถูกที่สุด</asp:ListItem>
                                <asp:ListItem Value="max">แพงที่สุด</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6">
                <a href="Home" onclick="return confirm('คุณต้องการกลับไปหน้าหลักใช่หรือไม่?')" class="btn btn-default">Back to Home</a>
            </div>
            <div class="col-xs-6 text-right">
                <asp:Button ID="btnSubmit" OnClientClick=" return $('.form').isValid()" OnClick="btnSubmit_Click" CssClass="btn btn-green" runat="server" Text="Next" />
            </div>
        </div>
    </div>
    <%--<div class="your-booking-includes">
        <div class="well confident-message">
            <h5 class="your-booking-includes-title">Remarks</h5>
            <div class="col-xs-12 your-booking-includes-details"><i class="fa fa-address-book fa-2x"></i><span>Airport transfer</span></div>
        </div>
    </div>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
    <script>
        var radioOne = true;
    </script>
    <script>
        $(document).ready(function () {

            $('#<%=tbStartProject.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });

            $('#<%=tbBirthdate.ClientID%>').datetimepicker({
                locale: 'th',
                format: 'L'
            });

            updateSummaryDetails();

            $("input, select").change(function () {
                updateSummaryDetails();
            });

            function updateSummaryDetails() {
                var name = $('#<%=tbProjectName.ClientID%>').val();
                var customer = $('#<%=tbCustomerName.ClientID%>').val();
                var type = $('#<%=ddProjectType.ClientID%>').val();

                setSummaryDetailInLable('summary-detail-project-name', name);
                setSummaryDetailInLable('summary-detail-project-customer', customer);
                setSummaryDetailInLable('summary-detail-project-type', type);
            }

            function setSummaryDetailInLable(id, val) {
                $('#' + id).html(val ? val : 'N/A');
            }
        });
    </script>
</asp:Content>

