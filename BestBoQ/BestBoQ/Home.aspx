<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="BestBoQ.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        table.dataTable tbody td {
            vertical-align: middle;
        }
    </style>
    <link href="css/main.css" rel="stylesheet" />
    <style>
        .white_content_home {
            position: fixed;
            top:90px;
            right: 130px;
            z-index: 1030;
            bottom: 0%;
            padding: 16px;
            visibility: visible;
        }

        @media (max-width: 750px) {
            .white_content_home {
                
            visibility: hidden;
            }
        }

            .white_content_home .survey-wrapper {
                position: relative;
            }

               
                    .white_content_home .survey-wrapper img {
                        width: 140px;
                    }

                .white_content_home .survey-wrapper .close-survey {
                    display: inline-block;
                    position: absolute;
                    width: 30px;
                    height: 30px;
                    /*top: 30px;*/
                    right: 5px;
                }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <%--<div class="alert alert-danger alert-dismissible fade in" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                <h4>Something wrong!</h4>
                <p>คุณไม่มี Permission</p>
            </div>--%>
            <div class="row">
                <div class="header-section text-center">
                    <h2>My Project</h2>
                    <p>
                    </p>
                    <hr class="bottom-line" />
                </div>
            </div>
            <%--            <div class="row">
                <div class="col-md-12">
                    <div class="mmb-bgc-booking-wrap">
                        <div class="mmb-bic-clickable" data-url="/account/editbooking.html?bookingId=h68jmktOPC%2fIHnBnt4dvTQ%3d%3d&amp;page=1&amp;state=Past&amp;sort=BookingDate" data-selenium="mmb-bic">

                            <section id="mmb-tell-friends-component" class="tell-friends" data-selenium="mmb-tell-friends-component">
                                <div class="hotel-picture">
                                    <div class="mmb-tell-friends-head">
                                        <input id="requestVerificationToken" name="requestVerificationToken" type="hidden" value="ks8Meq-mrtRPL6YvTsEHQuHNoAxivNZXvc5V_hpP_ulqwL_boAx-dqq-uEwezcjZ2Tt6Rq517Dr_aZ3hBLmYxwcWpjU1:tdpK-GSbVOAQc1SbFfmYZVGSoO7dLMXagmLeoyk90MgrbRNYVeriY9m1_3OVx_KLwdJpbWpfAcaeIy8DLtQ43ME5HKym7djf5LJ1glbaALNF59VH0">

                                        <figure>

                                            <img data-selenium="hotel-img" srcset="//pix6.agoda.net/hotelImages/159/159218/159218_17060510590053465447.jpg?s=450x338 1x, //pix6.agoda.net/hotelImages/159/159218/159218_17060510590053465447.jpg?s=450x338 2x" src="//pix6.agoda.net/hotelImages/159/159218/159218_17060510590053465447.jpg?s=450x338" class="hotel-img unfold media-object no-lazyload-img" height="221" width="231" alt="Butterfly on Prat Boutique Hotel">
                                        </figure>
                                    </div>
                                </div>
                            </section>


                            <div class="info-container">
                                <div class="booking-info-top">
                                    <div class="check-dates">
                                        <div class="check-info">
                                            <div class="check-title" data-selenium="check-in-label">Check in</div>
                                            <div class="check-date">
                                                <div class="check-day" data-selenium="check-in-day">30</div>
                                                <div class="check-mon-weekday">
                                                    <div class="check-mon" data-selenium="check-in-month-year">Jun 17</div>
                                                    <div class="check-weekday" data-selenium="check-in-weekday">Fri</div>
                                                </div>
                                            </div>
                                            <div class="check-time" data-selenium="check-in-time"></div>
                                        </div>
                                        <div class="check-spacer"></div>
                                        <div class="check-info">
                                            <div class="check-title" data-selenium="check-out-label">Check out</div>
                                            <div class="check-date">
                                                <div class="check-day" data-selenium="check-out-day">3</div>
                                                <div class="check-mon-weekday">
                                                    <div class="check-mon" data-selenium="check-out-month-year">Jul 17</div>
                                                    <div class="check-weeekday" data-selenium="check-out-weekday">Mon</div>
                                                </div>
                                            </div>
                                            <div class="check-time" data-selenium="check-out-time"></div>
                                        </div>
                                    </div>
                                </div>
                                <div class="hotel-info-top">
                                    <ul>
                                        <li class="hotel-title" data-selenium="hotel-name">Butterfly on Prat Boutique Hotel</li>
                                        <li class="booking-id" data-selenium="booking-id">Booking ID: <span data-selenium="booking-id-value">113142054</span></li>
                                        <li class="state">
                                            <i class="ficon ficon-check-circle ficon-16 mmb-booking-status-icon-gray"></i>
                                            <span class="booking-status-label mmb-bi-state-gray" data-selenium="booking-status-label">COMPLETED</span>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                            <div class="info-container">
                                <div class="hotel-info-bottom">
                                    <ul>

                                        <li class="hotel-info-text hotel-info-text-wraping" data-selenium="room-type">Family Room with 4G Pocket Wi-Fi Device</li>
                                        <li class="freebies-wrapper"></li>
                                    </ul>
                                </div>
                                <div class="booking-info-bottom">
                                    <div class="price">
                                        <div class="price-currency" data-selenium="payment-currency">THB</div>
                                        <div class="price-ammount" data-selenium="payment-ammount">14,350.02</div>
                                    </div>
                                    <div class="edit-booking">
                                        <input type="button" class="btn btn-primary" value="View details" onclick="event.stopPropagation(); window.location = '/account/editbooking.html?bookingId=h68jmktOPC%2fIHnBnt4dvTQ%3d%3d&amp;page=1&amp;state=Past&amp;sort=BookingDate';" data-selenium="action-buttom">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>--%>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="gvData" CssClass="table table-condensed" GridLines="None" runat="server" AutoGenerateColumns="False">
                        <%--OnRowCommand="gvData_RowCommand"--%>
                        <Columns>
                            <%--                        
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <%--                        <asp:TemplateField HeaderText="Type">
                            <ItemTemplate>
                                <asp:Image ID="imgType" runat="server" ImageUrl='<%# "~/" + Eval("type") %>' Height="40px" Width="40px" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Home Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Image ID="imgOnly" runat="server" AlternateText='<%#Eval("projecttype") %>' ImageUrl='<%#Eval("homepic") %>' Height="70px" Width="70px" />
                                    <asp:HiddenField ID="hdfID" runat="server" Value='<%#Eval("projectid") %>' />
                                    <small style="display: none"><%#Eval("projecttype") %></small>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="projecttype" HeaderText="Project Type" />
                            <asp:BoundField DataField="projectname" HeaderText="Project Name" />
                            <asp:BoundField DataField="customername" HeaderText="Customer Name" />
                            <asp:BoundField DataField="contractid" HeaderText="Contract Name" />
                            <%--<asp:BoundField DataField="Priority" HeaderText="Priority" />
                        <asp:BoundField DataField="region" HeaderText="Region" />
                        <asp:BoundField DataField="zone" HeaderText="Zone" />
                        <asp:BoundField DataField="Status" HeaderText="Status" />
                        <asp:BoundField DataField="user" HeaderText="user" />--%>
                            <asp:BoundField DataField="transdate" HeaderText="Create Date" />
                            <asp:HyperLinkField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" DataNavigateUrlFields="projectid" DataNavigateUrlFormatString="Project_Detail.aspx?id={0}" HeaderText="View" Text="<i class='fa fa-book fa-2x' aria-hidden='true'></i>" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </section>

    <div id="light" class="white_content_home ud-khaosod-survey" style="display: block;">
        <div class="survey-wrapper">
            <img src="Images/MrBestBoQ_right.png" />
            <a class="close-survey" href="javascript:void(0)" onclick="$('.white_content_home').hide()"></a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {
            $("#<%=gvData.ClientID%>").DataTable({
                order: [[3, "desc"]],
                columnDefs: [
                    { orderable: false, targets: -1 },
                    { orderable: false, targets: 0 }
                ]
            });
        });
    </script>
</asp:Content>
