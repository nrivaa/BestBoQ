<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Howto.aspx.cs" Inherits="BestBoQ.Howto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            /*box-sizing: border-box;*/
            outline-color: #ade3ff;
            outline-style: none;
        }

        /* Dropdowns - I */
        .dropdown .dropdown-toggle .down-arrow {
            display: inline-block;
            margin-top: 6px;
            margin-left: 5px;
            width: 16px;
            height: 9px;
            background-image: url("http://www.htic.es/assets/down-arrow.png")
        }

        .dropdown .dropdown-toggle .caret {
            display: none;
        }

        .dropdown.open .down-arrow {
            behavior: url(-ms-transform.htc);
            -webkit-transform: rotate(-180deg);
            -moz-transform: rotate(-180deg);
            -o-transform: rotate(-180deg);
            -ms-transform: rotate(-180deg);
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=2);
            transform: rotate(-180deg);
        }

        .dropdown button,
        .dropdown-menu {
            background-color: #f6f8f8;
        }

        .dropdown-menu {
            margin-top: 0px;
            background-color: #f6f8f8;
            border-radius: 0 0 0 0;
        }

            .dropdown-menu > li > a:hover {
                background-color: #96a4ad;
            }
        /* Dropdowns - F */

        div.clearfix {
            margin-top: 15px;
        }

        .ellipsis {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .margin-top-15 {
            margin-top: 15px;
        }

        body {
            background-color: #F7F9F9;
            margin: 0;
            padding: 0;
            width: 100%;
            height: 100%;
        }

            body > div {
                margin-top: 10px;
            }

        a {
            /*font-size: 0.9em;*/
            color: #72B032;
            /*color: #86BB4F;*/
        }

            a, a:hover, a:focus, a:visited {
                text-decoration: none;
            }

        /** General HTML style - F */

        canvas {
            margin-bottom: 15px;
        }

        container {
            padding-bottom: 15px;
        }

        /*#monthDrop*/
        .dropdown button .btn-default {
            width: 100%;
            border-color: red;
        }

        .balance {
            line-height: 1em;
            font-size: 2.5em
        }

        .title {
            color: #a0aeb6;
            font-size: 1em;
            margin-bottom: 10px;
        }

        /** Rounded divs - I */
        div.rounded,
        div.top-rounded,
        div.bottom-rounded {
            border: solid 1px #dce1e5;
        }

        div.rounded {
            margin-bottom: 20px;
            -webkit-border-radius: 4px 4px 4px 4px;
            -moz-border-radius: 4px 4px 4px 4px;
            border-radius: 4px 4px 4px 4px;
        }

        div.top-rounded {
            -webkit-border-radius: 4px 4px 0px 0px;
            -moz-border-radius: 4px 4px 0px 0px;
            border-radius: 4px 4px 0px 0px;
        }

        div.bottom-rounded {
            border-top-style: none;
            -webkit-border-radius: 0px 0px 4px 4px;
            -moz-border-radius: 0px 0px 4px 4px;
            border-radius: 0px 0px 4px 4px;
        }
        /** Rounded divs - F */


        /** Dropdown draft - I */
        .dropdown span.caret {
            float: right;
            margin-top: 8px;
        }

        .dropdown button {
            text-align: left;
        }

        .dropdown-menu {
            width: 100%;
        }
        /** Dropdown draft - F */

        /** Let tabls-left class be available in bootstrap 3.3.7 - I **/
        .tabs-left > .nav-tabs {
            border-bottom: 0;
        }


        .tabs-left > .nav-tabs > li {
            float: none;
        }

            .tabs-left > .nav-tabs > li > a,
            .tabs-left > .nav-tabs > li > div {
                margin-right: 0;
                margin-bottom: 3px;
            }

        .tabs-left > .nav-tabs {
            float: left;
            margin-right: 19px;
            border-right: 1px solid #ddd;
        }

            .tabs-left > .nav-tabs > li > a,
            .tabs-left > .nav-tabs > li > div {
                margin-right: -1px;
                -webkit-border-radius: 4px 0 0 4px;
                -moz-border-radius: 4px 0 0 4px;
                border-radius: 4px 0 0 4px;
            }

                .tabs-left > .nav-tabs > li > a:hover,
                .tabs-left > .nav-tabs > li > a:focus,
                .tabs-left > .nav-tabs > li > div:hover,
                .tabs-left > .nav-tabs > li > div:focus {
                    border-color: #eeeeee #dddddd #eeeeee #eeeeee;
                    background-color: #eeeeee;
                }

            .tabs-left > .nav-tabs .active > a,
            .tabs-left > .nav-tabs .active > a:hover,
            .tabs-left > .nav-tabs .active > a:focus,
            .tabs-left > .nav-tabs .active > div,
            .tabs-left > .nav-tabs .active > div:hover,
            .tabs-left > .nav-tabs .active > div:focus {
                border-color: #ddd transparent #ddd #ddd;
                *border-right-color: #ffffff;
            }
        /** Let tabls-left class be available in bootstrap 3.3.7 - F **/

        .active .account-type {
            color: #fff;
        }

        .account-type {
            color: #8dc73f;
            font-size: 1em;
            font-weight: bold;
            line-height: 18px;
        }

        .account-amount {
            color: #A0AEB6;
            font-size: 1.1em;
            line-height: 16px;
        }

        .account-link {
            font-size: 0.85em;
            /*line-height: 1em;*/
        }

        /* TABS */
        .tabs-left > .nav-tabs {
            margin-right: 0px;
            padding: 0;
            height: 100%; /* 700px - Debe ser el mismo height que el que tenga .tab-content */
        }

        /* CONTENIDO DE LOS TABS */
        .tab-content {
            background-color: #FFFFFF;
            border: solid 1px #DCE1E5;
            border-left-style: none;
            height: 100%;
            margin-bottom: 15px;
            min-height: 700px;
            border-radius: 0px 4px 4px 4px;
            -moz-border-radius: 0px 4px 4px 4px;
            -webkit-border-radius: 0px 4px 4px 4px;
        }

            .tab-content > div {
                margin-top: 26px;
            }

        /* Formato del tab activo */
        .tabs-left > .nav-tabs .active > a,
        .tabs-left > .nav-tabs .active > a:hover,
        .tabs-left > .nav-tabs .active > a:focus,
        .tabs-left > .nav-tabs .active div,
        .tabs-left > .nav-tabs .active div:hover,
        .tabs-left > .nav-tabs .active div:focus {
            background-color: #8dc73f;
            border-bottom-style: none;
            border-left-style: none;
            /*border-bottom: 1px solid #DCE1E5;*/
            /*border-left: 1px solid #DCE1E5;*/
            border-bottom-left-radius: 0px;
            border-right-style: none;
            margin-right: -1px;
            -webkit-box-shadow: -4px 0px 9px -1px rgba(0,0,0,0.05);
            -moz-box-shadow: -4px 0px 9px -1px rgba(0,0,0,0.05);
            box-shadow: -4px 0px 9px -1px rgba(0,0,0,0.05);
        }

        /* Formato de los tabs en general */
        .tabs-left > .nav-tabs > li:nth-child(1) > a,
        .tabs-left > .nav-tabs > li:nth-child(1) > a:hover,
        .tabs-left > .nav-tabs > li:nth-child(1) > a:focus,
        .tabs-left > .nav-tabs > li:nth-child(1) > div,
        .tabs-left > .nav-tabs > li:nth-child(1) > div:hover,
        .tabs-left > .nav-tabs > li:nth-child(1) > div:focus {
            cursor: pointer;
            border-top-left-radius: 4px;
        }

        .tabs-left > .nav-tabs > li.active:nth-child(1) > a,
        .tabs-left > .nav-tabs > li.active:nth-child(1) > a:hover,
        .tabs-left > .nav-tabs > li.active:nth-child(1) > a:focus,
        .tabs-left > .nav-tabs > li.active:nth-child(1) > div,
        .tabs-left > .nav-tabs > li.active:nth-child(1) > div:hover,
        .tabs-left > .nav-tabs > li.active:nth-child(1) > div:focus {
            border-top-left-radius: 4px;
            border-bottom-style: none;
            border-left: 1px solid #DCE1E5;
        }

        .tabs-left > .nav-tabs > li > a,
        .tabs-left > .nav-tabs > li > div {
            /*display:block;*/
            /*display: table;*/
            /*border: solid 1px transparent;*/
            margin-right: -1px;
            margin-bottom: -1px;
            border: solid 1px #DCE1E5;
            border-radius: 0px;
        }

        /* Style of the div element acting as tab content */
        .tabbable.tabs-left > .nav-tabs > li > div > div {
            display: block;
            width: 100%;
            padding: 1em;
        }

        .kleanity-container {
            max-width: 1280px;
            margin-left: auto;
            margin-right: auto;
        }


        .kleanity-page-title-wrap {
            background-image: url(https://www.builk.com/th/wp-content/uploads/2016/06/new-home-x-text-03.jpg);
            background-position: center;
            background-size: cover;
            position: relative;
        }

            .kleanity-page-title-wrap.kleanity-style-medium .kleanity-page-title-content {
                padding-top: 60px;
                padding-bottom: 60px;
            }

        .kleanity-body-front .gdlr-core-container, .kleanity-body-front .kleanity-container {
            padding-left: 20px;
            padding-right: 20px;
        }

        .kleanity-page-title-wrap .kleanity-page-title-overlay {
            background-color: #2d2d2d;
            opacity: 0.04;
            position: absolute;
            top: 0px;
            right: 0px;
            bottom: 0px;
            left: 0px;
        }

        .kleanity-page-title {
            font-size: 1em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="navcontent" runat="server">
    <ul class="nav navbar-nav navbar-right">
        <li>
            <a href="Default.aspx#feature">รู้จัก bestboq</a>
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
                    <h2>คู่มือเริ่มต้นการใช้งาน bestboq</h2>
                </div>
            </div>
        </div>
        <br />
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div id="MyAccountsTab" class="tabbable tabs-left">
                        <!-- Account selection for desktop - I -->
                        <ul class="nav nav-tabs col-md-3">
                            <li class="active">
                                <div data-target="#lA" data-toggle="tab">
                                    <div class="ellipsis">
                                        <span class="account-type">1.ลงทะเบียน</span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div data-target="#lB" data-toggle="tab">
                                    <div>
                                        <span class="account-type">2.เข้าสู่ระบบ</span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div data-target="#lC" data-toggle="tab">
                                    <div>
                                        <span class="account-type">3.สร้างโครงการ</span>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div data-target="#lD" data-toggle="tab">
                                    <div>
                                        <span class="account-type">4.ปริ้นท์เอกสาร</span>
                                    </div>
                                </div>
                            </li>
                        </ul>
                        <div class="tab-content col-md-9">
                            <div class="tab-pane active" id="lA">
                                <div class="col-md-offset-1">
                                    ลงทะเบียน
                                </div>
                            </div>
                            <div class="tab-pane" id="lB">
                                <div class="col-md-offset-1">
                                    เข้าสู่ระบบ
                                </div>
                            </div>
                            <div class="tab-pane" id="lC">
                                <div class="col-md-offset-1">
                                    สร้างโครงการ
                                </div>
                            </div>
                            <div class="tab-pane" id="lD">
                                <div class="col-md-offset-1">
                                    ปริ้นท์เอกสาร
                                </div>
                            </div>
                        </div>
                        <!-- Account selection for desktop - F -->
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
</asp:Content>
