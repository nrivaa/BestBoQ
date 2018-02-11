﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_07.aspx.cs" Inherits="BestBoQ.CreateProject_03_07" %>

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

            <div class="alert alert-success confident-message text-center nearly-done">
                <nav>
                    <ul class="pager">
                        <li class="previous"><a href="CreateProject_03_05?id=<%=param_projid%>"><span aria-hidden="true">&larr;</span></a></li>
                       <li data-toggle="tooltip" data-placement="bottom" title="1.ระบบงานฐานราก (Footing)">
                            <a href="CreateProject_03_01?id=<%=param_projid%>">1</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="2.ระบบงานคานคอดิน (Beam)">
                            <a href="CreateProject_03_02?id=<%=param_projid%>">2</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="3.ระบบงานพื้น (FL)">
                            <a href="CreateProject_03_03?id=<%=param_projid%>">3</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="4.ระบบงานหลังคา (Roof)">
                            <a href="CreateProject_03_04?id=<%=param_projid%>">4</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="5.ระบบงานผนัง (Wall)">
                            <a href="CreateProject_03_05?id=<%=param_projid%>">5</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="6.ระบบงานสุขภัณฑ์ห้องน้า (Toilet)">
                            <a href="CreateProject_03_07?id=<%=param_projid%>">6</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="7.ระบบงานสุขาภิบาล (Sanitation)">
                            <a href="CreateProject_03_08?id=<%=param_projid%>">7</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="8.ระบบงานประปา (Plumbing)">
                            <a href="CreateProject_03_09?id=<%=param_projid%>">8</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="9.งานฝ้า (Ceiling)">
                            <a href="CreateProject_03_10?id=<%=param_projid%>">9</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="10.งานระบบไฟฟ้า">
                            <a href="CreateProject_03_11?id=<%=param_projid%>">10</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="11.งานสี (Color)">
                            <a href="CreateProject_03_12?id=<%=param_projid%>">11</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="12.ราวระเบียง และราวบันได Railing">
                            <a href="CreateProject_03_13?id=<%=param_projid%>">12</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="13.Window & Door+Fitting">
                            <a href="CreateProject_03_14?id=<%=param_projid%>">13</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="14.ค่าดำเนินงาน/ส่วนลด">
                            <a href="CreateProject_03_15?id=<%=param_projid%>">14</a>
                        </li>
                        <li class="next"><a href="CreateProject_03_08?id=<%=param_projid%>"><span aria-hidden="true">&rarr;</span></a></li>
                    </ul>
                </nav>
            </div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>ระบบงานสุขภัณฑ์ห้องน้า (Toilet)</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="row">
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-xs-6 col-md-4">
                                        <div class="thumbnail">
                                            <asp:Image ID="imgPic" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                                            <div class="caption text-center">
                                                <h3>ห้องน้ำ Type -
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("toiletType")%>'></asp:Label>
                                                </h3>
                                                ราคา 
                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("cost")%>'></asp:Label>
                                                บาท/ชุด
                                                <div class="form-group has-feedback">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="TextBox1" data-inputmask="'alias': 'integer'" Text="0" data-validation="number,maxRoom,completeRoom" CssClass="form-control inputValue" runat="server" autocomplete="off"></asp:TextBox>
                                                        <span class="input-group-addon">ห้อง</span>
                                                        <input type="hidden" class="dataMaxRoom" value="<%# Eval("maxRoom")%>" />
                                                        <input type="hidden" class="dataCost" value="<%# Eval("cost")%>" />
                                                        <input type="hidden" class="dataflag" value="<%# Eval("flag")%>" />
                                                    </div>
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
                <a href="CreateProject_03_05?id=<%=param_projid%>" class="btn btn-default">Back to Previous Step</a>
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
    <script>
        var maxRoom = $('.form').find(".dataMaxRoom").val();

        initVaidateMaxRoom();
        calculatePrice();

        $(".inputValue").on("change", function () {
            calculatePrice();
        });

        function initVaidateMaxRoom() {
            $.formUtils.addValidator({
                name: 'maxRoom',
                validatorFunction: function (value, $el, config, language, $form) {
                    var sumRoom = 0;
                    $.each($('.form input[type=text]'), function () {
                        sumRoom = sumRoom + parseInt($(this).val());
                    });
                    return sumRoom <= maxRoom;
                },
                errorMessage: 'The maximum number of rooms has been reached (' + maxRoom + " Rooms)",
                errorMessageKey: 'badMaxRoom'
            });

            $.formUtils.addValidator({
                name: 'completeRoom',
                validatorFunction: function (value, $el, config, language, $form) {
                    var sumRoom = 0;
                    $.each($('.form input[type=text]'), function () {
                        sumRoom = sumRoom + parseInt($(this).val());
                    });
                    return sumRoom == maxRoom;
                },
                errorMessage: 'Total number of rooms not equal ' + maxRoom,
                errorMessageKey: 'badCompleteRoom'
            });

            // Setup form validation
            $.validate();
        }

        function calculatePrice() {
            var sectionPriceElem = $("#sectionPrice");
            var sumPrice = 0.0;

            if ($('.form').isValid()) {
                $.each($('.form input[type=text]'), function () {
                    var em = $(this);

                    var block = em.closest('.input-group')
                    var cost = parseFloat(block.find(".dataCost").val());
                    var weight = parseFloat(block.find(".dataWeight").val());

                    sumPrice = sumPrice + (parseFloat($(this).val()) * cost);
                });
            }

            updateTotalPriceFromSection(sectionPriceElem, sumPrice);
        }
    </script>
</asp:Content>
