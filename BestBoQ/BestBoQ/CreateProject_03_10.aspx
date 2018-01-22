﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_10.aspx.cs" Inherits="BestBoQ.CreateProject_03_10" %>

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

            <div class="alert alert-success confident-message text-center nearly-done" data-bind="html: progressTracker.nearlyDoneText"><strong>10. งานฝ้า (Ceiling)</strong></div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>งานฝ้า (Ceiling)</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <h3>INDOOR</h3>
                        <div class="row">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-xs-6 col-md-4">
                                        <div class="thumbnail">
                                            <asp:Image ID="imgPic1" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                                            <div class="caption text-center">
                                                <h3>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("ceilingPart")%>'></asp:Label>
                                                    <br />
                                                    <small><asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label></small>
                                                </h3>
                                                <div class="form-group has-feedback">
                                                    <asp:RadioButton ID="RadioButton1" runat="server" />
                                                    <input type="hidden" class="dataMM" value="<%# Eval("numMM")%>" />
                                                    <input type="hidden" class="dataCost" value="<%# Eval("cost")%>" />
                                                    <input type="hidden" class="dataflag" value="<%# Eval("flag")%>" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <h3>OUTDOOR</h3>
                        <div class="row">
                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-xs-6 col-md-4">
                                        <div class="thumbnail">
                                            <asp:Image ID="imgPic2" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                                            <div class="caption text-center">
                                                <h3>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ceilingPart")%>'></asp:Label>
                                                    <br />
                                                    <small><asp:Label ID="Label3" runat="server" Text='<%# Eval("detail")%>'></asp:Label></small>
                                                </h3>
                                                <div class="form-group has-feedback">
                                                    <asp:RadioButton ID="RadioButton2" runat="server" />
                                                    <input type="hidden" class="dataMM" value="<%# Eval("numMM")%>" />
                                                    <input type="hidden" class="dataCost" value="<%# Eval("cost")%>" />
                                                    <input type="hidden" class="dataflag" value="<%# Eval("flag")%>" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                         <div class="row">
                            <div class="col-sm-12">
                                <p class="text-right"><small>ราคารวม <span id="sectionPrice" data-value="<%=section_price %>">0</span> บาท</small></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-6">
                <a href="CreateProject_03_09?id=<%=param_projid%>" class="btn btn-default">Back to Previous Step</a>
            </div>
            <div class="col-xs-6 text-right">
                <asp:Button ID="btnSubmit" OnClientClick=" return checkValidateWithRadio();" OnClick="btnSubmit_Click" CssClass="btn btn-green" runat="server" Text="Next" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {

            calculatePrice();

            $('.form input[type=radio]').on('ifChecked', function (event) {
                calculatePrice();
            });

            function calculatePrice() {
                var sectionPriceElem = $("#sectionPrice");
                var sumPrice = 0.0;

                $.each($('.form input[type=radio]'), function () {
                    var em = $(this);

                    if (em.is(':checked')) {
                        var block = em.closest('.form-group');
                        var cost = parseFloat(block.find(".dataCost").val());
                        var MM = parseFloat(block.find(".dataMM").val());
                        sumPrice = sumPrice + parseFloat(cost * MM);
                    }
                });

                updateTotalPriceFromSection(sectionPriceElem, sumPrice);
            }
        });
    </script>
</asp:Content>
