<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BestBoQ._Default" %>

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

        .carousel-inner > .item > img, .carousel-inner > .item > a > img {
            width: 100%;
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

        .banner2 {
            background: url('Images/Default/bg-banner.jpg') no-repeat center top;
            background-size: cover;
            min-height: 580px;
            position: relative;
        }

        #feature2 img {
            width: 100%;
            max-width: 100%;
            display: block;
        }

        #feature2 h2, .pm-staff-profile-name {
            color: #8dc73f;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="navcontent" runat="server">
    <ul class="nav navbar-nav navbar-right">
        <li>
            <a href="#feature">รู้จัก bestboq</a>
        </li>
        <li>
            <a href="#feature2">bestboq ทำอะไรได้บ้าง?</a>
        </li>
        <li>
            <a href="Howto.aspx">เรียนรู้การใช้งาน</a>
        </li>
        <li>
            <a href="#" data-target="#registerForm" data-toggle="modal">สมัครใช้งาน</a>
        </li>
        <li class="btn-trial">
            <a href="#" data-target="#login" data-toggle="modal">เข้าสู่ระบบ</a>
        </li>
    </ul>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="server">

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
                                    <a href="FrogetPassword.aspx">ลืมรหัสผ่าน?</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--/ Modal Login box-->
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
                                            <asp:TextBox ID="tbUsername" autocomplete="off" data-validation="required,length,validUsername" CssClass="form-control" runat="server" placeholder="Username" data-validation-length="max20" data-inputmask-regex="[a-za-zA-Z0-9]*"></asp:TextBox>
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
                                        <asp:TextBox ID="tbCompany" Style="display: none" runat="server" data-validation="length" data-validation-length="min5" CssClass="form-control" autocomplete="off" placeholder="ชื่อบริษัท"></asp:TextBox>
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
                                    <div class="form-group">
                                        <asp:TextBox ID="tbNation" Style="display: none" runat="server" CssClass="form-control" autocomplete="off" placeholder="เลขทะเบียนนิติบุคคล" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
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
    <section id="feature" style="padding-top: 5px">
        <div class="banner2">
            <div class="bg-color">
                <div class="container">
                    <div class="row">
                        <div class="banner-text text-center">
                            <div class="text-border">
                                <h2 class="text-dec">bestboq คืออะไร?</h2>
                            </div>
                            <div class="intro-para text-center quote">
                                <%--<p class="big-text">เราคือมืออาชีพในการก่อสร้างบ้าน อาคาร สิ่งปลูกสร้างต่างๆ</p>  width="500" height="282"--%>
                                <iframe class="row" width="500" height="282" src="https://www.youtube.com/embed/qvFheWwvET8?rel=0&amp;showinfo=0" 
                                      frameborder="0" 
                                      position: relative;
                                      allow="autoplay; encrypted-media" 
                                      allowfullscreen="">
                                  </iframe>
                                <p class="small-text">
                                    BESTBOQ คือ โปรแกรมช่วยให้ ธุรกิจรับเหมาก่อสร้าง,ธุรกิจรับสร้างบ้าน , ธุรกิจพัฒนาอสังหาริมทรัพย์ หรือแม้กระทั่งลูกค้าที่จะสร้างบ้าน<br />
                                    ประหยัดค่าใช้จ่ายในการจ้างพนักงานเป็นจำนวนมากและทำให้การทำธุรกิจเป็นเรื่องง่ายและรวดเร็วมากขึ้นกว่าเดิม
                                    <br />
                                    และโปรแกรมBest BOQ สามารถทำงานได้หลายฟังชั่นงาน อาทิเช่น งานประมาณการวัสดุ BOQ ,สัญญาว่าจ้าง,Payment,วางแผนงานก่อสร้าง<br />
                                    วิเคราะห์ต้นทุน, เอกสารตกลงเลือกวัสดุร่วมกันและใบเสนอราคาก่อสร้าง
                                    <br />
                                    ซึ่งโปรแกรม Best BOQ จะสามารถลดกระบวนการทำงานที่ยุ่งยากและลดเวลาในการทำเอกสารทำให้ได้งานที่รวดเร็วและมีคุณภาพและที่สำคัญต้นทุนต่ำ<br />
                                    ที่สำคัญสามารถลดความขัดแย้งที่จะเกิดข้นในอนาคตระหว่าง ผู้รับเหมาและเจ้าของบ้าน เป็นต้น
                                    <span id="feature3"></span>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--/ feature-->

    <!-- feature2-->
    <section id="feature2" class="section-padding">
        <div class="fusion-row" style="max-width: 1100px; margin: 0 auto;">
            <h1 style="font-size: 2em; color: #000000; text-align: center;" data-inline-fontsize="true" data-inline-lineheight="true" data-fontsize="44" data-lineheight="48">bestboq ทำอะไรได้บ้าง?</h1>
            <br />
            <br />
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/BOQ.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">รายการวัสดุ (BOQ)</p>
                            <p class="pm-staff-profile-bio">แสดงราคากลางและวัสดุก่อสร้างที่ตกลงร่วมกันระหว่างผู้รับจ้างและผู้ว่าจ้างในการก่อสร้าง ซึ่งสามารถใช้เป็นข้อตกลงร่วมกันระหว่างทำการเสนอราคาหรือว่าก่อสร้าง และ รายละเอียดด้านในจะเป็นรายการที่แสดงปริมาณงานและราคาวัสดุก่อสร้างที่ถอดมาจากแบบก่อสร้างทั้งหมด</p>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/contract.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">สัญญาว่าจ้าง</p>
                            <p class="pm-staff-profile-bio">สัญญาว่าจ้างก่อสร้าง ประกอบด้วยหัวข้อหลักๆ ดังต่อไปนี้ รายละเอียดระหว่างผู้ว่างจ้าง และผู้รับจ้าง , ขอบเขตงาน ,ระยะเวลาการก่อสร้าง ,ข้อตกลงอื่นๆ เกี่ยวกับรายละเอียดแบบ การเปลี่ยนแปลงหรือยกเลิกสัญญาว่าจ้าง การผิดสัญญาการทำงาน และระยะกำหนดการทำงาน</p>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/Payment.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">งวดงานก่อสร้าง</p>
                            <p class="pm-staff-profile-bio">งวดงานก่อสร้าง คือข้อตกลงการชำระเงินตามปริมาณงานที่ทำไปแล้วเสร็จในเเต่ละขั้นตอน การเเบ่งงวดงานจำเป็นต้องมีความรู้เเละอาศัยประสบการณ์ในเรื่องการก่อสร้างและมีความยุติธรรมระหว่างผู้รับจ้างและผู้ว่าจ้าง </p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/Plan.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">แผนงานก่อสร้าง</p>
                            <p class="pm-staff-profile-bio">แผนงานก่อสร้าง เป็นขั้นตอนที่สำคัญ ในการบริหารงานก่อสร้าง ให้มีประสิทธิภาพ หากไม่มีการวางแผนงานก่อสร้างไว้ หรือวางแผนไว้ไม่ถูกต้อง การควบคุมโครงการ จะทำได้ลำบาก เนื่องจาก ไม่มีข้อมูลที่จะใช้ตรวจสอบ ความก้าวหน้าของโครงการ ทำให้ไม่สามารถแก้ไขปัญหาหรืออุปสรรค ต่าง ๆ </p>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/Report.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">รายการต้นทุนและกำไร</p>
                            <p class="pm-staff-profile-bio">คือการวิเคราะห์ ค่าวัสดุ,ค่าแรงและผลกำไรที่จะได้รับในแต่ละโครงการซึ่งจะเป็นส่วนช่วยให้ผู้รับจ้างและผู้ว่าจ้างในการตัดสินและวางแผนการทำงานให้มีประสิทธิภาพมากยิ่งขึ้น </p>
                        </div>
                    </div>
                </div>

                <div class="col-lg-4 col-md-4 col-sm-4">
                    <div class="pm-staff-profile-container">
                        <div class="pm-staff-profile-image-wrapper text-center">
                            <div class="pm-staff-profile-image">
                                <img src="Images/IconDocument/Quotation.png" alt="" class="img-thumbnail img-circle" />
                            </div>
                        </div>
                        <div class="pm-staff-profile-details text-center">
                            <p class="pm-staff-profile-name">ใบเสนอราคา</p>
                            <p class="pm-staff-profile-bio">Bestboq สามารถสรุปราคาประเมินของโครงการ และออกใบเสนอราคาที่ผู้ใช้งานสามารถนำไปใช้ได้ทันที </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--/ feature2-->

    <div id="light" class="white_content2 ud-khaosod-survey" style="display: block; visibility: visible;">
        <div class="survey-wrapper">
            <img src="Images/MrBestBoQ_Close.png" />
            <a class="close-survey" href="javascript:void(0)" onclick="$('.white_content2').hide()"></a>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="script" runat="server">
    <script src="theme/js/contactform.js"></script>
    <script>
        $(document).ready(function () {

            initPassword();

            validateForm('.contactForm');

            $('.radio-type input').on('ifChecked', function (event) {
                var f = $(this).val();
                if (f == "บุคคล") {
                    $("#<%=tbID.ClientID%>").show();
                    $("#<%=tbID.ClientID%>").attr("data-validation", "required");
                    $("#<%=tbTax.ClientID%>").hide();
                    $("#<%=tbNation.ClientID%>").hide();
                    $("#<%=tbTax.ClientID%>").removeAttr("data-validation");
                    $("#<%=tbCompany.ClientID%>").hide();
                    $("#<%=tbCompany.ClientID%>").removeAttr("data-validation");
                }
                else {
                    $("#<%=tbID.ClientID%>").hide();
                    $("#<%=tbID.ClientID%>").removeAttr("data-validation");
                    $("#<%=tbTax.ClientID%>").show();
                    $("#<%=tbTax.ClientID%>").attr("data-validation", "required");
                    $("#<%=tbNation.ClientID%>").show();
                    $("#<%=tbNation.ClientID%>").attr("data-validation", "required");
                    $("#<%=tbCompany.ClientID%>").show();
                    $("#<%=tbCompany.ClientID%>").removeAttr("data-validation");
                }
                validateForm('.contactForm');
            });

            initVaidateUsername();
            var validUsername = true;
            function initVaidateUsername() {
                $.formUtils.addValidator({
                    name: 'validUsername',
                    validatorFunction: function (value, $el, config, language, $form) {
                        $.post("service.asmx/checkUser", { username: value }, function (data) {
                            if (data == "Yes") {
                                validUsername = false;
                            }
                            else {
                                validUsername = true;
                            }
                            return validUsername;
                        });
                        return validUsername;
                    },
                    errorMessage: 'Username has been already taken.',
                    errorMessageKey: 'invalidUsername'
                });
                $.validate();
            }

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

            if (getUrlParameter("cmd") == "register") {
                $("#registerForm").modal().show();
            }
            else if (getUrlParameter("cmd") == "login") {
                $("#login").modal().show();
            }
            else if (getUrlParameter("cmd") == "forget") {
                $("#forget").modal().show();
            }

        });
    </script>
</asp:Content>
