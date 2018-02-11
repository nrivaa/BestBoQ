<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BestBoQ._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .ticker-headline {
            overflow: hidden;
            text-overflow: ellipsis;
            white-space: nowrap;
            padding: 15px 0;
            margin: 0;
            font-size: 18px;
        }

        .carousel.vertical .carousel-inner {
            height: 100%;
            width: auto;
        }

            .carousel.vertical .carousel-inner > .item {
                width: auto;
                /*padding-right: 50px;*/
                -webkit-transition: 0.6s ease-in-out top;
                transition: 0.6s ease-in-out top;
            }

        @media all and (transform-3d), (-webkit-transform-3d) {
            .carousel.vertical .carousel-inner > .item {
                -webkit-transition: 0.6s ease-in-out;
                transition: 0.6s ease-in-out;
            }

                .carousel.vertical .carousel-inner > .item.next, .carousel.vertical .carousel-inner > .item.active.right {
                    -webkit-transform: translate3d(0, 100%, 0);
                    transform: translate3d(0, 100%, 0);
                    top: 0;
                }

                .carousel.vertical .carousel-inner > .item.prev, .carousel.vertical .carousel-inner > .item.active.left {
                    -webkit-transform: translate3d(0, -100%, 0);
                    transform: translate3d(0, -100%, 0);
                    top: 0;
                }

                    .carousel.vertical .carousel-inner > .item.next.left, .carousel.vertical .carousel-inner > .item.prev.right, .carousel.vertical .carousel-inner > .item.active {
                        -webkit-transform: translate3d(0, 0, 0);
                        transform: translate3d(0, 0, 0);
                        top: 0;
                    }
        }

        .carousel.vertical .carousel-inner > .active,
        .carousel.vertical .carousel-inner > .next,
        .carousel.vertical .carousel-inner > .prev {
            display: block;
        }

        .carousel.vertical .carousel-inner > .active {
            top: 0;
        }

        .carousel.vertical .carousel-inner > .next,
        .carousel.vertical .carousel-inner > .prev {
            position: absolute;
            top: 0;
            width: 100%;
        }

        .carousel.vertical .carousel-inner > .next {
            top: 100%;
        }

        .carousel.vertical .carousel-inner > .prev {
            top: -100%;
        }

            .carousel.vertical .carousel-inner > .next.left,
            .carousel.vertical .carousel-inner > .prev.right {
                top: 0;
            }

        .carousel.vertical .carousel-inner > .active.left {
            top: -100%;
        }

        .carousel.vertical .carousel-inner > .active.right {
            top: 100%;
        }

        .carousel.vertical .carousel-control {
            left: auto;
            width: 50px;
            background-image: linear-gradient(to right,rgba(0,0,0,.0001) 0,rgba(0,0,0,.5) 100%);
        }

            .carousel.vertical .carousel-control.up {
                top: 0;
                right: 0;
                bottom: 50%;
            }

            .carousel.vertical .carousel-control.down {
                top: 50%;
                right: 0;
                bottom: 0;
            }

            .carousel.vertical .carousel-control .icon-prev,
            .carousel.vertical .carousel-control .icon-next,
            .carousel.vertical .carousel-control .glyphicon-chevron-up,
            .carousel.vertical .carousel-control .glyphicon-chevron-down {
                position: absolute;
                top: 50%;
                z-index: 5;
                display: inline-block;
            }

            .carousel.vertical .carousel-control .icon-prev,
            .carousel.vertical .carousel-control .glyphicon-chevron-up {
                left: 50%;
                margin-left: -10px;
                top: 50%;
                margin-top: -10px;
            }

            .carousel.vertical .carousel-control .icon-next,
            .carousel.vertical .carousel-control .glyphicon-chevron-down {
                left: 50%;
                margin-left: -10px;
                top: 50%;
                margin-top: -10px;
            }

            .carousel.vertical .carousel-control .icon-up,
            .carousel.vertical .carousel-control .icon-down {
                width: 20px;
                height: 20px;
                line-height: 1;
                font-family: serif;
            }

            .carousel.vertical .carousel-control .icon-prev:before {
                content: '\2039';
            }

            .carousel.vertical .carousel-control .icon-next:before {
                content: '\203a';
            }

        /* Slider */
        .slick-slide {
            margin: 0px 20px;
        }

            .slick-slide img {
                /*width: 70%;*/
                width: 200px;
                padding: 0 30px;
                background-color: #FFF;
                border: 1px solid white;
                border-radius: 10px;
                /*height: 60px;*/
            }

        .slick-slider {
            position: relative;
            display: block;
            box-sizing: border-box;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            -webkit-touch-callout: none;
            -khtml-user-select: none;
            -ms-touch-action: pan-y;
            touch-action: pan-y;
            -webkit-tap-highlight-color: transparent;
        }

        .slick-list {
            position: relative;
            display: block;
            overflow: hidden;
            margin: 0;
            padding: 0;
        }

            .slick-list:focus {
                outline: none;
            }

            .slick-list.dragging {
                cursor: pointer;
                cursor: hand;
            }

        .slick-slider .slick-track,
        .slick-slider .slick-list {
            -webkit-transform: translate3d(0, 0, 0);
            -moz-transform: translate3d(0, 0, 0);
            -ms-transform: translate3d(0, 0, 0);
            -o-transform: translate3d(0, 0, 0);
            transform: translate3d(0, 0, 0);
        }

        .slick-track {
            position: relative;
            top: 0;
            left: 0;
            display: block;
        }

            .slick-track:before,
            .slick-track:after {
                display: table;
                content: '';
            }

            .slick-track:after {
                clear: both;
            }

        .slick-loading .slick-track {
            visibility: hidden;
        }

        .slick-slide {
            display: none;
            float: left;
            height: 100%;
            min-height: 1px;
        }

        [dir='rtl'] .slick-slide {
            float: right;
        }

        .slick-slide img {
            display: block;
        }

        .slick-slide.slick-loading img {
            display: none;
        }

        .slick-slide.dragging img {
            pointer-events: none;
        }

        .slick-initialized .slick-slide {
            display: block;
        }

        .slick-loading .slick-slide {
            visibility: hidden;
        }

        .slick-vertical .slick-slide {
            display: block;
            height: auto;
            border: 1px solid transparent;
        }

        .slick-arrow.slick-hidden {
            display: none;
        }


        #msform {
            /*width: 70%;*/
            margin: 10px auto;
            text-align: center;
            position: relative;
        }

            #msform fieldset {
                /*background: white;*/
                border: 0 none;
                /*border-radius: 3px;*/
                /*box-shadow: 0 0 15px 1px rgba(0, 0, 0, 0.4);*/
                /*padding: 20px 30px;*/
                box-sizing: border-box;
                /*width: 80%;*/
                margin: 0 10%;
                /*stacking fieldsets above each other*/
                position: relative;
            }
                /*Hide all except first fieldset*/
                #msform fieldset:not(:first-of-type) {
                    display: none;
                }
            /*inputs*/
            /*#msform input, #msform textarea {
                padding: 15px;
                border: 1px solid #ccc;
                border-radius: 3px;
                margin-bottom: 10px;
                width: 100%;
                box-sizing: border-box;
                font-family: montserrat;
                color: #2C3E50;
                font-size: 13px;
            }*/
            /*buttons*/
            #msform .action-button {
                width: 100px;
                background: #8dc73f;
                font-weight: bold;
                color: white;
                border: 0 none;
                border-radius: 1px;
                cursor: pointer;
                padding: 10px 5px;
                margin: 10px 5px;
            }

                #msform .action-button:hover, #msform .action-button:focus {
                    box-shadow: 0 0 0 2px white, 0 0 0 3px #8dc73f;
                }
        /*headings*/
        .fs-title {
            font-size: 20px;
            text-transform: uppercase;
            color: #2C3E50;
            margin-bottom: 10px;
        }

        .fs-subtitle {
            font-weight: normal;
            font-size: 15px;
            color: #666;
            margin-bottom: 20px;
        }
        /*progressbar*/
        #progressbar {
            /*margin-bottom: 30px;*/
            overflow: hidden;
            padding: 0;
            /*CSS counters to number the steps*/
            counter-reset: step;
        }

            #progressbar li {
                list-style-type: none;
                color: #4B4B4C;
                text-transform: uppercase;
                width: 50%;
                float: left;
                position: relative;
            }

                #progressbar li:before {
                    content: counter(step);
                    counter-increment: step;
                    width: 20px;
                    line-height: 20px;
                    display: block;
                    color: #FFF;
                    background: #4B4B4C;
                    border-radius: 3px;
                    margin: 0 auto 5px auto;
                    z-index: 2;
                }
                /*progressbar connectors*/
                #progressbar li:after {
                    content: '';
                    /*width: 100%;*/
                    width: 93%;
                    height: 2px;
                    background: #4B4B4C;
                    position: absolute;
                    left: -46.5%;
                    /*left: -50%;*/
                    top: 9px;
                    /*z-index: -1;*/ /*put it behind the numbers*/
                }

                #progressbar li:first-child:after {
                    content: none;
                }

                #progressbar li.active:before, #progressbar li.active:after {
                    background: #8dc73f;
                    color: white;
                }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navcontent" runat="server">
    <ul class="nav navbar-nav navbar-right">
        <li>
            <a href="#feature">รู้จัก bestboq</a>
        </li>
        <li>
            <a href="#" data-target="#registerForm" data-toggle="modal">สมัครใช้งาน</a>
        </li>
        <li class="btn-trial">
            <a href="#" data-target="#login" data-toggle="modal">เข้าสู่ระบบ</a>
        </li>
    </ul>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <!--Modal Login box-->
    <div class="modal fade" id="login" role="dialog">
        <div class="modal-dialog modal-sm modal-small">
            <!-- Modal content no 1-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center form-title">เข้าสู่ระบบ</h4>
                </div>
                <div class="modal-body padtrbl">
                    <div class="login-box-body">
                        <p cla ss="login-box-msg">เข้าสู่ระบบ BestBoQ</p>
                        <div class="form-group">
                            <div class="form-group has-feedback">
                                <!----- username -------------->
                                <asp:TextBox ID="tbLoginUsername" CssClass="form-control" runat="server" placeholder="Username" data-inputmask-regex="[a-za-zA-Z0-9]*" autocomplete="off"></asp:TextBox>
                                <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                                <!---Alredy exists  ! -->
                                <span class="fa fa-user  form-control-feedback"></span>
                            </div>
                            <div class="form-group has-feedback">
                                <!----- password -------------->
                                <asp:TextBox ID="tbLoginPassword" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password" autocomplete="off"></asp:TextBox>
                                <span style="display: none; font-weight: bold; position: absolute; color: grey; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginpsw"></span>
                                <!---Alredy exists  ! -->
                                <span class="fa fa-lock form-control-feedback"></span>
                            </div>
                            <div class="row">
                                <%--<div class="col-xs-12">
                                    <div class="checkbox icheck">
                                        <label>
                                            <input type="checkbox" id="loginrem" />
                                            Remember Me
                                        </label>
                                    </div>
                                </div>--%>
                                <div class="col-xs-12">
                                    <asp:Button ID="btnLogin" CssClass="btn btn-green btn-block btn-flat" runat="server" Text="เข้าสู่ระบบ" OnClick="btnLogin_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <a href="#" data-target="#forget" data-toggle="modal">ลืมรหัสผ่าน?</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/ Modal Login box-->

    <!--Modal Forget password box-->
    <div class="modal fade" id="forget" role="dialog">
        <div class="modal-dialog modal-sm modal-small">
            <!-- Modal content no 1-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center form-title">ลืมรหัสผ่าน</h4>
                </div>
                <div class="modal-body padtrbl">
                    <div class="login-box-body">
                        <p cla ss="login-box-msg">ระบบจะส่ง password ไปให้คุณตาม email ที่ท่านสมัครไว้</p>
                        <div class="form-group">
                            <div class="form-group has-feedback">
                                <!----- username -------------->
                                <asp:TextBox ID="tbFuser" CssClass="form-control" runat="server" placeholder="Username" data-inputmask-regex="[a-za-zA-Z0-9]*" autocomplete="off"></asp:TextBox>
                                <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                                <!---Alredy exists  ! -->
                                <span class="fa fa-user  form-control-feedback"></span>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:Button ID="btnForget" CssClass="btn btn-green btn-block btn-flat" runat="server" Text="แจ้งลืมรหัส" OnClick="btnForget_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/ Modal Forget password box-->

    <!--Modal Register box-->
    <div class="modal fade" id="registerForm" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content no 1-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title text-center form-title">สมัครใช้งาน</h4>
                </div>
                <div class="modal-body padtrbl">
                    <div class="register-box-body">
                        <div class="contactForm" role="form">
                            <!-- multistep form -->
                            <div id="msform">
                                <!-- progressbar -->
                                <ul id="progressbar">
                                    <li class="active">ข้อมูลทั่วไป</li>
                                    <li>ข้อมูลเกี่ยวกับบริษัท/บุคคล</li>
                                </ul>
                                <!-- fieldsets -->
                                <fieldset id="msform-step1">
                                    <h2 class="fs-title">ข้อมูลทั่วไป</h2>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                            <asp:TextBox ID="tbUsername" autocomplete="off" data-validation="required" CssClass="form-control" runat="server" placeholder="Username" data-inputmask-regex="[a-za-zA-Z0-9]*"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                            <input type="password" id="tbPassword" runat="server" autocomplete="off" data-validation="required" name="pass_confirmation" class="form-control" placeholder="Password" />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                                            <input type="password" id="tbRepassword" runat="server" autocomplete="off" data-validation="required,confirmPassword" name="pass_confirmation" class="form-control" placeholder="Confirm Password" />

                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-envelope"></span></span>
                                            <asp:TextBox ID="tbEmail" CssClass="form-control" autocomplete="off" data-validation="email" runat="server" placeholder="Email" data-inputmask="'alias': 'email'"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="glyphicon glyphicon-phone"></span></span>
                                            <asp:TextBox ID="tbMobile" CssClass="form-control" autocomplete="off" data-validation="required" runat="server" placeholder="Mobile Number" data-inputmask="'mask': '999-999-9999'"></asp:TextBox>
                                        </div>
                                    </div>
                                    <input type="button" name="next" class="next btn btn-green btn-block btn-flat" value="Next" />
                                </fieldset>
                                <fieldset>
                                    <h2 class="fs-title">ข้อมูลเกี่ยวกับบริษัท/บุคคล</h2>
                                    <div class="form-group form-group-radio">
                                        <label class="col-sm-3 control-label lable_type text-left">ประเภท</label>
                                        <div class="col-sm-9">
                                            <asp:RadioButtonList class="radio-type" ID="rbType" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True">บุคคล</asp:ListItem>
                                                <asp:ListItem>บริษัท</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group">
                                        <asp:TextBox ID="tbName" runat="server" data-validation="length" data-validation-length="min5" CssClass="form-control" autocomplete="off" placeholder="ชื่อ-นามสกุล"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbCompany" runat="server" data-validation="length" data-validation-length="min5" CssClass="form-control" autocomplete="off" placeholder="ชื่อบริษัท"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbAlias" runat="server" data-validation="length" data-validation-length="min3" CssClass="form-control" autocomplete="off" placeholder="Alias Name"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbAddress" runat="server" data-validation="length" data-validation-length="min10" CssClass="form-control" autocomplete="off" placeholder="Address"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbID" runat="server" CssClass="form-control" data-validation="required" autocomplete="off" placeholder="เลขประจำตัวประชาชน" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="tbTax" Style="display: none" runat="server" CssClass="form-control" autocomplete="off" placeholder="เลขประจำตัวผู้เสียภาษี" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <input type="button" name="previous" class="previous btn btn-greenline btn-block btn-flat" value="Previous" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClientClick=" return $('.contactForm').isValid()" CssClass="btn btn-green btn-block btn-flat" OnClick="btnRegister_Click" />
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/ Modal Login box-->

    <!--Banner-->
    <div class="banner">
        <div class="carousel fade-carousel slide" data-ride="carousel" data-interval="4000" id="bs-carousel">
            <!-- Overlay -->
            <div class="overlay"></div>

            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#bs-carousel" data-slide-to="0" class="active"></li>
                <li data-target="#bs-carousel" data-slide-to="1"></li>
                <li data-target="#bs-carousel" data-slide-to="2"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item slides active">
                    <img src="Images/Default/home01.jpg" />
                </div>
                <div class="item slides">
                    <img src="Images/Default/home02.jpg" />
                </div>
                <div class="item slides">
                    <img src="Images/Default/home03.jpg" />
                </div>
            </div>
            <!-- Controls -->
            <a class="left carousel-control" href="#bs-carousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#bs-carousel" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
    </div>
    <!--/ Banner-->
    <!--Feature-->
    <section id="feature" class="section-padding">
        <div id="carousel-example-vertical" data-interval="7000" class="carousel vertical slide">
            <div class="carousel-inner" role="listbox">
                <div class="item active">
                    <img src="Images/HowTo/1.png" />
                </div>
                <div class="item">
                    <img src="Images/HowTo/2.png" />
                </div>
                <div class="item">
                    <img src="Images/HowTo/3.png" />
                </div>
            </div>

            <!-- Controls -->
            <a class="up carousel-control" href="#carousel-example-vertical" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-up" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="down carousel-control" href="#carousel-example-vertical" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-down" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>
        <%--<div class="carousel fade-carousel slide" data-ride="carousel" data-interval="4000" id="bs-carousel1">
            <!-- Overlay -->
            <div class="overlay"></div>

            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#bs-carousel1" data-slide-to="0" class="active"></li>
                <li data-target="#bs-carousel1" data-slide-to="1"></li>
                <li data-target="#bs-carousel1" data-slide-to="2"></li>
            </ol>

            <!-- Wrapper for slides -->
            <div class="carousel-inner">
                <div class="item slides active">
                    <img src="Images/HowTo/1.png" />
                </div>
                <div class="item slides">
                    <img src="Images/HowTo/2.png" />
                </div>
                <div class="item slides">
                    <img src="Images/HowTo/3.png" />
                </div>
            </div>
            <!-- Controls -->
            <a class="left carousel-control" href="#bs-carousel1" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
                <span class="sr-only">Previous</span>
            </a>
            <a class="right carousel-control" href="#bs-carousel1" role="button" data-slide="next">
                <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
                <span class="sr-only">Next</span>
            </a>
        </div>--%>
        <%--<div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <h2>รู้จัก bestboq</h2>
                    <p>
                        Features
                    </p>
                    <hr class="bottom-line" />
                </div>
                <div class="feature-info">
                    <div class="fea">
                        <div class="col-md-4">
                            <div class="heading pull-right">
                                <h4>Latest Technologies</h4>
                                <p>Donec et lectus bibendum dolor dictum auctor in ac erat. Vestibulum egestas sollicitudin metus non urna in eros tincidunt convallis id id nisi in interdum.</p>
                            </div>
                            <div class="fea-img pull-left">
                                <i class="fa fa-css3"></i>
                            </div>
                        </div>
                    </div>
                    <div class="fea">
                        <div class="col-md-4">
                            <div class="heading pull-right">
                                <h4>Toons Background</h4>
                                <p>Donec et lectus bibendum dolor dictum auctor in ac erat. Vestibulum egestas sollicitudin metus non urna in eros tincidunt convallis id id nisi in interdum.</p>
                            </div>
                            <div class="fea-img pull-left">
                                <i class="fa fa-drupal"></i>
                            </div>
                        </div>
                    </div>
                    <div class="fea">
                        <div class="col-md-4">
                            <div class="heading pull-right">
                                <h4>Award Winning Design</h4>
                                <p>Donec et lectus bibendum dolor dictum auctor in ac erat. Vestibulum egestas sollicitudin metus non urna in eros tincidunt convallis id id nisi in interdum.</p>
                            </div>
                            <div class="fea-img pull-left">
                                <i class="fa fa-trophy"></i>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        </section>
        <!--/ feature-->
        <section id="cta-2">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <h2 class="text-left">ผู้สนับสนุนหลัก</h2>
                        <div class="cta-2-form text-center">
                            <div class="container">
                                <section class="customer-logos slider">
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image1.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image2.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image3.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image4.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image5.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image6.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image7.png" />
                                    </div>
                                    <div class="slide">
                                        <img src="https://www.solodev.com/assets/carousel/image8.png" />
                                    </div>
                                </section>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--Contact-->
        <section id="register">
            <div class="container">
                <div class="row text-center">
                    <ul class="social-links">
                        <li>
                            <a href="https://www.facebook.com/natureestatethailand/">
                                <span class="icon-facebook-circled"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                <span class="icon-twitter-circled"></span>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <span class="icon-linkedin-circled"></span>
                            </a>
                        </li>
                        <li>
                            <a href="">
                                <span class="icon-pinterest-circled"></span>
                            </a>
                        </li>
                        <li>
                            <a href="https://www.natureestate.co.th">
                                <span class="icon-dribbble-circled"></span>
                            </a>
                        </li>
                        <li>
                            <a href="#">
                                <span class="icon-gplus-circled"></span>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </section>
        <!--/ Contact-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="theme/js/contactform.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/slick-carousel/1.6.0/slick.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-easing/1.3/jquery.easing.min.js"></script>
    <script>
        $(document).ready(function () {

            initPassword();
            renderLogoSponsor();
            validateForm('.contactForm');

            $('.radio-type input').on('ifChecked', function (event) {
                var f = $(this).val();
                if (f == "บุคคล") {
                    $("#<%=tbID.ClientID%>").show();
                    $("#<%=tbID.ClientID%>").attr("data-validation", "required");
                    $("#<%=tbTax.ClientID%>").hide();
                    $("#<%=tbTax.ClientID%>").removeAttr("data-validation");
                }
                else {
                    $("#<%=tbID.ClientID%>").hide();
                    $("#<%=tbID.ClientID%>").removeAttr("data-validation");
                    $("#<%=tbTax.ClientID%>").show();
                    $("#<%=tbTax.ClientID%>").attr("data-validation", "required");
                }
                validateForm('.contactForm');
            });

            function initPassword() {
                $.formUtils.addValidator({
                    name: 'confirmPassword',
                    validatorFunction: function (value, $el, config, language, $form) {
                        var emPass = $('#<%=tbPassword.ClientID%>');
                        var emPassConfim = $('#<%=tbRepassword.ClientID%>');
                        return emPass.val() == emPassConfim.val();
                    },
                    errorMessage: 'Password does not match',
                    errorMessageKey: 'confirmPassword'
                });
            }

            function renderLogoSponsor() {
                $('.customer-logos').slick({
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    autoplay: true,
                    autoplaySpeed: 1000,
                    arrows: false,
                    dots: false,
                    pauseOnHover: false,
                    responsive: [{
                        breakpoint: 768,
                        settings: {
                            slidesToShow: 3
                        }
                    }, {
                        breakpoint: 520,
                        settings: {
                            slidesToShow: 2
                        }
                    }]
                });
            }



            //jQuery time
            var current_fs, next_fs, previous_fs; //fieldsets
            var left, opacity, scale; //fieldset properties which we will animate
            var animating; //flag to prevent quick multi-click glitches

            $(".next").click(function () {
                if (animating) return false;
                animating = true;

                var isValid = $("#msform-step1").isValid();
                console.log(isValid);
                if (isValid) {
                    current_fs = $(this).parent();
                    next_fs = $(this).parent().next();

                    //activate next step on progressbar using the index of next_fs
                    $("#progressbar li").eq($("fieldset").index(next_fs)).addClass("active");

                    //show the next fieldset
                    next_fs.show();
                    //hide the current fieldset with style
                    current_fs.animate({ opacity: 0 }, {
                        step: function (now, mx) {
                            //as the opacity of current_fs reduces to 0 - stored in "now"
                            //1. scale current_fs down to 80%
                            scale = 1 - (1 - now) * 0.2;
                            //2. bring next_fs from the right(50%)
                            left = (now * 50) + "%";
                            //3. increase opacity of next_fs to 1 as it moves in
                            opacity = 1 - now;
                            current_fs.css({
                                'transform': 'scale(' + scale + ')',
                                'position': 'absolute'
                            });
                            next_fs.css({ 'left': left, 'opacity': opacity });
                        },
                        duration: 800,
                        complete: function () {
                            current_fs.hide();
                            animating = false;
                        },
                    });
                }
                else {
                    animating = false;
                }
            });

            $(".previous").click(function () {
                if (animating) return false;
                animating = true;

                //current_fs = $(this).parent();
                //previous_fs = $(this).parent().prev();

                current_fs = $(this).closest("fieldset");
                previous_fs = current_fs.prev();

                //de-activate current step on progressbar
                $("#progressbar li").eq($("fieldset").index(current_fs)).removeClass("active");

                //show the previous fieldset
                previous_fs.show();
                //hide the current fieldset with style
                current_fs.animate({ opacity: 0 }, {
                    step: function (now, mx) {
                        //as the opacity of current_fs reduces to 0 - stored in "now"
                        //1. scale previous_fs from 80% to 100%
                        scale = 0.8 + (1 - now) * 0.2;
                        //2. take current_fs to the right(50%) - from 0%
                        left = ((1 - now) * 50) + "%";
                        //3. increase opacity of previous_fs to 1 as it moves in
                        opacity = 1 - now;
                        current_fs.css({ 'left': left });
                        previous_fs.css({ 'transform': 'scale(' + scale + ')', 'opacity': opacity, 'position': 'initial' });
                    },
                    duration: 800,
                    complete: function () {
                        current_fs.hide();
                        animating = false;
                    },
                    //this comes from the custom easing plugin
                    easing: 'easeInOutBack'
                });
            });

        });
    </script>
</asp:Content>
