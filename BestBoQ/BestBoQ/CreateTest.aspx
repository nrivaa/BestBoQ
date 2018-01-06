<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateTest.aspx.cs" Inherits="BestBoQ.CreateTest" %>
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
            <div class="alert alert-success confident-message text-center secure-booking-starts-here"><strong>All your personal information is secure when you book with Agoda. </strong></div>
            <div class="alert alert-success confident-message text-center nearly-done" data-bind="html: progressTracker.nearlyDoneText"><strong>Nearly done! Just a few more details and we can confirm your reservation.</strong></div>
        </div>
    </div>
    <h3>Enter project details</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">

    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="hidden-xs">
                        <h4 class="m-b-0">
                            <a class="hotel-name" target="_blank" href="//www.agoda.com/xi-hotel/hotel/hong-kong-hk.html?checkIn=2018-2-21&amp;los=2&amp;Rooms=1&amp;Adults=2&amp;Childs=0&amp;currency=THB&amp;origin=TH&amp;cid=-1&amp;gclid=-1" title="Xi Hotel">Xi Hotel</a>
                            <i class="ficon text-warning ficon-s ficon-star-3 text-orange-yellow" data-original-title="" title=""></i>
                            <i id="pppIcon" class="ficon ficon-thumb-up text-success"></i>
                        </h4>
                        <address class="m-t-1 m-b-2" data-bind="text: bookingDetails.hotelDetails.address">7 Minden Avenue, Tsim Sha Tsui, Hong Kong</address>
                    </div>
                    <dl class="dl-horizontal dl-horizontal-left">
                        <dt data-bind="text: bookingDetails.checkinText() + ' :'">Check-in :</dt>
                        <dd id="checkin" data-bind="text: bookingDetails.checkin">Wednesday, February 21, 2018</dd>
                        <dt data-bind="text: bookingDetails.checkoutText() + ' :'">Check-out :</dt>
                        <dd id="checkout" data-bind="text: bookingDetails.checkout">Friday, February 23, 2018</dd>
                        <dt data-bind="text: bookingDetails.stayText() + ' :'">Stay :</dt>
                        <dd id="occupancyDetails" class="occupancy-details" data-bind="text: bookingDetails.occupancy">2 nights, 1 room, 2 adults</dd>
                        <dt data-bind="text: bookingDetails.locationText() + ' :'">Neighborhood :</dt>
                        <dd id="location"><span data-bind="text: bookingDetails.location">Tsim Sha Tsui, Hong Kong</span> </dd>
                    </dl>
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-xs-6">
                <input value="Back" class="btn btn-default" type="button">
            </div>
            <div class="col-xs-6 text-right">
                <input value="Next" class="btn btn-green" type="button">
            </div>
        </div>

    </div>
    <div class="your-booking-includes">
        <div class="well confident-message">
            <h5 class="your-booking-includes-title">Remarks</h5>
            <div data-bind="foreach: { data: yourBookingIncludes.features() }">
                <div class="col-xs-4 your-booking-includes-details"><i class="fa fa-address-book fa-2x" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">Airport transfer</span> </div>
                <div class="col-xs-4 your-booking-includes-details"><i class="ficon ficon-24 ficon-train-new" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">150 meters to public transportation</span> </div>
                <div class="col-xs-4 your-booking-includes-details"><i class="ficon ficon-24 ficon-pin-heart-of-the-city" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">Heart of the City</span> </div>
                <div class="col-xs-4 your-booking-includes-details"><i class="ficon ficon-24 ficon-free-wifi-in-all-rooms" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">Free Wi-Fi in all rooms</span> </div>
                <div class="col-xs-4 your-booking-includes-details"><i class="ficon ficon-24 ficon-mini-bar" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">Mini bar</span> </div>
                <div class="col-xs-4 your-booking-includes-details"><i class="ficon ficon-24 ficon-mini-bar" data-bind="css: $data.symbol"></i><span data-bind="text: $data.name">Bar</span> </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
    <div>
        <div id="default-banner">
            <i class="ficon ficon-16"></i>
            <div>
                <div>
                    <div>
                        <i class="ficon ficon-16 ficon-right ficon-info-with-circle info-icon"></i>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <div class="panel panel-default banner urgency">
                <div>
                    <div>
                        <div>
                            <span>Don’t miss out! There are only 3 rooms left at this price!</span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="room-details">
                        <div class="room-details-info-container">
                            <h5 id="roomdetails-title" class="m-b-4 m-t-0">Your Project Details</h5>
                            <div>
                                <dl class="dl-horizontal dl-horizontal-left">

                                    <dt id="roomdetails-room-text">Room :</dt>
                                    <dd>1 x Deluxe Double Room City View</dd>
                                    <dd id="roomdetails-change-room">
                                        <a target="_blank" href="//www.agoda.com/xi-hotel/hotel/hong-kong-hk.html?checkIn=2018-2-21&amp;los=2&amp;Rooms=1&amp;Adults=2&amp;Childs=0&amp;currency=THB&amp;origin=TH&amp;cid=-1&amp;gclid=-1#property-room-grid-root"><span>Change room</span> <i class="ficon ficon-16 ficon-arrow-right text ficon-txtdeco-none"></i></a>
                                    </dd>
                                </dl>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default banner discount">
            <div>
                <div>
                    <div>
                        <span>Early Booking Saver. Rate includes 20% discount!</span>
                    </div>
                    <div>
                        <span><strong>Well done!</strong> You have found the lowest rate at this property</span>
                    </div>
                </div>
            </div>
        </div>

        <div class="panel panel-default price-details">
            <div class="panel-body">
                <div class="base-price">
                    <dl class="dl-horizontal dd-align-right room-price">

                        <dt>Original price (1 room x 2 nights)</dt>
                        <dd><del class="crossed-out-rate text-right" id="crossedOutRate0">฿ 24,705.48</del> </dd>

                        <dt class="description" id="roomDescription0">Price (1 room x 2 nights)</dt>
                        <dd><span class="amount">฿ 7,465.42</span> </dd>

                    </dl>

                    <dl class="dl-horizontal dd-align-right">
                        <dt>Booking fees</dt>
                        <dd><strong class="text-primary">FREE</strong> </dd>
                    </dl>
                </div>
            </div>
            <div class="panel-footer">
                <p class="m-b-0"></p>
                <dl class="dl-horizontal dd-align-right price-conclusion total-price">
                    <dt><strong class="h5">Price</strong> <i class="ficon ficon-16 ficon-right ficon-info-with-circle text-primary" title=""></i></dt>
                    <dd><span><strong id="totalAmount" class="total-amount h5">฿ 8,211.96</strong>
                    </span></dd>
                </dl>
                <p></p>

                <p id="hotelLevelChargeSummary" class="m-b-0">
                    <small id="hotelLevelIncludedChargeSummary" class="charge-summary"><strong>Included in price:</strong> <span>Service charge 10%</span> </small>
                    <div class="divider-dotted-top"><small>Smart choice! You saved ฿ 16,493.52</small> </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
</asp:Content>


