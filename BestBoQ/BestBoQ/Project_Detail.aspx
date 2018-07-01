<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Project_Detail.aspx.cs" Inherits="BestBoQ.Project_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/projectdetail.css" rel="stylesheet" />
    <link href="css/createproject.css" rel="stylesheet" />
    <style>
        .nav-documents img {
            height: 35px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">

            <div class="row">
                <div class="col-md-12 progress-tracker-container">
                    <ul class="progress-tracker">
                        <li class="step-0-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">1. Enter project details</span> <small class="visible-xs-block visible-sm-block mobile">Details</small> </li>
                        <li class="step-1-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">2. Select project type</span> <small class="visible-xs-block visible-sm-block mobile">Type</small> </li>
                        <li class="step-2-title progress-tracker-visited"><span class="visible-md-block visible-lg-block desktop">3. Choose project materials</span> <small class="visible-xs-block visible-sm-block mobile">Materials</small> </li>
                        <li class="step-3-title  progress-tracker-active"><span class="visible-md-block visible-lg-block desktop">4. Review project summary</span> <small class="visible-xs-block visible-sm-block mobile">Summary!</small> </li>
                    </ul>

                    <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                            <span aria-hidden="true">×</span>
                        </button>
                        <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
                    </div>
                </div>
            </div>
            <br />
            <div class="row ">
                <div class="col-xs-6 text-left">
                    <a href="Home.aspx" class="agoda-action-button af-btn btn-greenline"><i class="fa fa-caret-left" aria-hidden="true"></i>&nbsp;&nbsp;Back to Home</a>
                </div>
                <%--<div class="col-xs-6 text-right">
                    <button type="button" class="agoda-action-button af-btn btn-green mmb-add-to-my-calendar-btn"><i class="fa fa-flag" aria-hidden="true"></i>&nbsp;Finish Project</button>
                </div>--%>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="edit-booking-property-infos">
                                        <div id="div-edit" class="edit-booking-action edit-booking-resend-voucher-container">
                                            <a href="CreateProject.aspx?id=<%=param_projid%>" class="af-btn btn-greenline"><i class="fa fa-pencil" aria-hidden="true"></i>&nbsp;&nbsp;Modify</a>
                                        </div>
                                        <section class="edit-booking-wrapper">
                                            <div id="edit-booking-hotel-header">
                                                <h1 class="edit-booking-hotel-name">
                                                    <asp:Label ID="lbProjName" runat="server" Text="#N/A"></asp:Label>
                                                </h1>
                                                <i class="edit-booking-hotel-start ficon ficon-16 orange-yellow ficon-star-style ficon-star-4"></i>
                                                <p class="hotel-address-map">
                                                    <asp:Label ID="lbCusName" runat="server" Text="#N/A"></asp:Label>
                                                </p>
                                            </div>
                                        </section>
                                    </div>
                                    <div class="edit-booking-informations">
                                        <div class="row">
                                            <div class="col-xs-3">
                                                <asp:Image Width="100%" CssClass="img-rounded text-center" ID="img" runat="server" />
                                            </div>
                                            <div class="col-xs-9">
                                                <div class="edit-booking-info-block edit-booking-booking-id-info">
                                                    <div class="edit-booking-info-label">
                                                        <div class="edit-booking-booking-id-info-title">Project Name</div>
                                                        <div class="edit-booking-booking-id-info-title">&nbsp;</div>
                                                        <div class="edit-booking-booking-id-info-title">Contract Name</div>
                                                    </div>
                                                    <div class="edit-booking-content">
                                                        <div class="edit-booking-booking-id-info-content">
                                                            <asp:Label ID="lbProjName2" runat="server" Text="#N/A"></asp:Label>
                                                        </div>
                                                        <div class="edit-booking-state" id="summary-status">
                                                            <i class="fa"></i><span>
                                                                <asp:Label ID="lbStatus" runat="server" Text="#N/A"></asp:Label>&nbsp;</span>
                                                        </div>
                                                        <div class="edit-booking-booking-id-info-content">
                                                            <asp:Label ID="lbContract" runat="server" Text="#N/A"></asp:Label>
                                                        </div>

                                                    </div>
                                                    <div class="edit-booking-block-separator"></div>
                                                </div>
                                                <div class="edit-booking-info-block edit-booking-info-with-border edit-booking-checkin-out-info" data-selenium="edit-booking-checkin-out-info">
                                                    <div class="edit-booking-info-label">
                                                        <div class="edit-booking-check-in-title">Customer Name</div>
                                                        <div class="edit-booking-check-out-title">Customer Address</div>
                                                    </div>
                                                    <div class="edit-booking-content ">
                                                        <div class="edit-booking-check-in-content">
                                                            <asp:Label ID="lbCusName2" runat="server" Text="#N/A"></asp:Label>
                                                        </div>
                                                        <div class="edit-booking-check-out-content">
                                                            <div class="edit-booking-check-out-date">
                                                                <asp:Label ID="lbCusAddress" runat="server" Text="#N/A"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="edit-booking-action edit-booking-change-date-action"></div>
                                                    <div class="edit-booking-block-separator"></div>
                                                </div>
                                                <div class="edit-booking-info-block edit-booking-info-with-border edit-booking-contact-info">
                                                    <div class="edit-booking-info-label">
                                                        <div class="edit-booking-booking-id-info-title">Project Type</div>
                                                        <div class="edit-booking-booking-id-info-title">Project Start</div>
                                                    </div>
                                                    <div class="edit-booking-content">
                                                        <div class="edit-booking-booking-id-info-content">
                                                            <asp:Label ID="lbProjType" runat="server" Text="#N/A"></asp:Label>
                                                        </div>
                                                        <div class="edit-booking-booking-id-info-content">
                                                            <asp:Label ID="lbProjStart" runat="server" Text="#N/A"></asp:Label>
                                                        </div>

                                                    </div>
                                                    <div class="edit-booking-block-separator"></div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <aside class="col-sm-4">
                            <div>
                                <div class="panel panel-default price-details">
                                    <div class="panel-body">
                                        <h5 class="m-b-4 m-t-0"><i class="fa fa-credit-card-alt" aria-hidden="true"></i>&nbsp;&nbsp;Payment details</h5>
                                        <dl class="dl-horizontal dd-align-right room-price">
                                            <dt>ค่าแรงและวัสดุรวม</dt>
                                            <dd>฿ <span id="totalPrice">
                                                <asp:Label ID="lbTotalPrice" runat="server" Text="0.00"></asp:Label></span></dd>
                                            <dt>ค่าดำเนินการ</dt>
                                            <dd>฿ <span id="feePrice">
                                                <asp:Label ID="lbFeePrice" runat="server" Text="0.00"></asp:Label></span></dd>
                                            <dt>ส่วนลด</dt>
                                            <dd>฿ <span id="promoPrice">
                                                <asp:Label ID="lbPromoPrice" runat="server" Text="0.00"></asp:Label></span></dd>
                                            <dt>อื่นๆ</dt>
                                            <dd>฿ <span id="otherPrice">
                                                <asp:Label ID="lbOtherPrice" runat="server" Text="0.00"></asp:Label></span></dd>
                                        </dl>
                                    </div>
                                    <div class="panel-footer">
                                        <p class="m-b-0"></p>
                                        <dl class="dl-horizontal dd-align-right price-conclusion total-price">
                                            <dt><strong class="h5">รวมสุทธิ</strong></dt>
                                            <dd>
                                                <span>
                                                    <strong class="total-amount h5">฿
                                                <span id="lastPrice">
                                                    <asp:Label ID="lbLastPrice" runat="server" Text="0.00"></asp:Label>
                                                </span>
                                                    </strong>
                                                </span>
                                            </dd>
                                        </dl>
                                    </div>
                                </div>

                                <div class="panel panel-default project-details">
                                    <div class="panel-body">
                                        <h5 id="roomdetails-title" class="m-b-4 m-t-0"><i class="fa fa-file" aria-hidden="true"></i>&nbsp;&nbsp;Documents</h5>
                                        <ul class="nav nav-pills nav-stacked nav-generate nav-documents">
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnBoq" OnClick="lbtnBoq_Click" runat="server">
                                                <img src="Images/IconDocument/BOQ.png" />&nbsp;&nbsp;
                                                BOQ <small>(สรุปรายการวัสดุ)</small>
                                                </asp:LinkButton>
                                            </li>
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnContractDoc" OnClick="lbtnContractDoc_Click" runat="server">
                                                    <img src="Images/IconDocument/contract.png" />&nbsp;&nbsp;
                                                    Contract <small>(สัญญาว่าจ้าง)</small>
                                                </asp:LinkButton>
                                            </li>
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnAppendixDoc" OnClick="lbtnAppendixDoc_Click" runat="server">
                                                <img src="Images/IconDocument/Payment.png" />&nbsp;&nbsp;
                                                Payment Term <small>(งวดงานก่อสร้าง)</small>
                                                </asp:LinkButton></li>
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnPlan" OnClick="lbtnPlan_Click" runat="server">
                                                <img src="Images/IconDocument/Plan.png" />&nbsp;&nbsp;
                                                Plan <small>(แผนงานก่อสร้าง)</small>
                                                </asp:LinkButton>
                                            </li>
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnReport" OnClick="lbtnReport_Click" runat="server">
                                                <img src="Images/IconDocument/Report.png" />&nbsp;&nbsp;
                                                Report <small>(รายการแยกต้นทุน)</small>
                                                </asp:LinkButton>
                                            </li>
                                            <li role="presentation">
                                                <asp:LinkButton ID="lbtnAll" OnClick="lbtnAll_Click" runat="server">
                                                <img src="Images/IconDocument/All.png" />&nbsp;&nbsp;
                                                All Report <small>(รวมทุกเอกสาร)</small>
                                                </asp:LinkButton>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </aside>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        var status = '<%=lbStatus.Text%>';
        var x = document.getElementById("div-edit");

        if (status.toLowerCase() == "complete") {
            $("#summary-status i").addClass("mmb-booking-status-icon-green");
            $("#summary-status i").addClass("fa-check-circle");
            $("#summary-status span").addClass("mmb-bi-state-green");
            x.style.display = "none";
        }
        else if (status.toLowerCase() == "on progress" || status.toLowerCase() == "onprogress") {
            $("#summary-status i").addClass("mmb-booking-status-icon-orange");
            $("#summary-status i").addClass("fa-clock-o");
            $("#summary-status span").addClass("mmb-bi-state-orange");
            x.style.display = "block";
        }
    </script>
</asp:Content>
