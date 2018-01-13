<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Project_Detail.aspx.cs" Inherits="BestBoQ.Project_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/projectdetail.css" rel="stylesheet" />
    <link href="css/createproject.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <%--  <h2>My Project</h2>
                    <p></p>
                    <hr class="bottom-line" />--%>
                </div>
            </div>

            <div class="row ">
                <div class="col-sm-6 text-left">
                    <a href="Home.aspx" class="agoda-action-button af-btn btn-greenline">Back to Home</a>
                </div>
                <div class="col-sm-6 text-right">
                    <button type="button" class="agoda-action-button af-btn btn-green mmb-add-to-my-calendar-btn">Finish Project</button>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="row">

                        <div class="col-sm-8">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="edit-booking-property-infos">
                                        <div class="edit-booking-action edit-booking-resend-voucher-container">
                                            <a href="CreateProject.aspx?id=<%=param_projid%>" class="af-btn btn-greenline">Modify</a>
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
                                                <asp:Image width="100%" CssClass="img-rounded text-center" ID="img" runat="server" />
                                            </div>
                                            <div class="col-xs-9">
                                                <div class="edit-booking-info-block edit-booking-booking-id-info">
                                                    <div class="edit-booking-info-label">
                                                        <div class="edit-booking-booking-id-info-title">Project Name</div>
                                                        <div class="edit-booking-booking-id-info-title"></div>
                                                        <div class="edit-booking-booking-id-info-title">Contract Name</div>
                                                    </div>
                                                    <div class="edit-booking-content">
                                                        <div class="edit-booking-booking-id-info-content">113142054</div>
                                                        <div class="edit-booking-state" data-selenium="edit-booking-state"><i class="fa fa-check-circle mmb-booking-status-icon-green"></i><span class="mmb-bi-state-green">Completed</span></div>
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
                                                        <div class="edit-booking-check-in-content">June 30, 2017</div>
                                                        <div class="edit-booking-check-out-content">
                                                            <div class="edit-booking-check-out-date">
                                                                <asp:Label ID="lbCusAddress" runat="server" Text="#N/A"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="edit-booking-num-staying-days">3 night(s)</div>
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
                                        <h5 class="m-b-4 m-t-0"><i class="fa fa-credit-card-alt" aria-hidden="true"></i> Payment details</h5>
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
                                        <h5 id="roomdetails-title" class="m-b-4 m-t-0"><i class="fa fa-file" aria-hidden="true"></i> Documents</h5>
                                        <ul class="nav nav-pills nav-stacked nav-generate">
                                          <li role="presentation"><a href="#">BOQ <small>(สรุปรายการวัสดุ)</small></a></li>
                                        <li role="presentation"><a href="#">Contract <small>(สัญญาว่าจ้าง)</small></a></li>
                                          <li role="presentation"><a href="#">Payment Term <small>(งวดงานก่อสร้าง)</small></a></li>
                                            <li role="presentation"><a href="#">Plan <small>(แผนงานก่อสร้าง)</small></a></li>
                                             <li role="presentation"><a href="#">Report <small>(รายการแยกต้นทุน)</small></a></li>
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
</asp:Content>
