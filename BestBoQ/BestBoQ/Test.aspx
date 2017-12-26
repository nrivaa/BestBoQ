﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="BestBoQ.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
     <!--Modal box-->
        <div class="modal fade" id="login" role="dialog">
            <div class="modal-dialog modal-sm">

                <!-- Modal content no 1-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title text-center form-title">Login</h4>
                    </div>
                    <div class="modal-body padtrbl">

                        <div class="login-box-body">
                            <p class="login-box-msg">Sign in to start your session</p>
                            <div class="form-group">
                                <form name="" id="loginForm">
                                    <div class="form-group has-feedback">
                                        <!----- username -------------->
                                        <input class="form-control" placeholder="Username" id="loginid" type="text" autocomplete="off" />
                                        <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                                        <!---Alredy exists  ! -->
                                        <span class="fa fa-user  form-control-feedback"></span>
                                    </div>
                                    <div class="form-group has-feedback">
                                        <!----- password -------------->
                                        <input class="form-control" placeholder="Password" id="loginpsw" type="password" autocomplete="off" />
                                        <span style="display: none; font-weight: bold; position: absolute; color: grey; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginpsw"></span>
                                        <!---Alredy exists  ! -->
                                        <span class="fa fa-lock form-control-feedback"></span>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="checkbox icheck">
                                                <label>
                                                    <input type="checkbox" id="loginrem">
                                                    Remember Me
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-xs-12">
                                            <button type="button" class="btn btn-green btn-block btn-flat" onclick="userlogin()">Sign In</button>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <!--/ Modal box-->

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
                        <img src="https://ununsplash.imgix.net/photo-1416339134316-0e91dc9ded92?q=75&fm=jpg&s=883a422e10fc4149893984019f63c818">
                    </div>
                    <div class="item slides">
                        <img src="https://ununsplash.imgix.net/photo-1416339684178-3a239570f315?q=75&fm=jpg&s=c39d9a3bf66d6566b9608a9f1f3765af">
                    </div>
                    <div class="item slides">
                        <img src="https://ununsplash.imgix.net/photo-1416339276121-ba1dfa199912?q=75&fm=jpg&s=9bf9f2ef5be5cb5eee5255e7765cb327">
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
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>การใช้งาน</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
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
            </div>
        </section>
        <!--/ feature-->
        <!--Organisations-->
        <section id="organisations" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <div class="orga-stru">
                                <h3>65%</h3>
                                <p>Say NO!!</p>
                                <i class="fa fa-male"></i>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <div class="orga-stru">
                                <h3>20%</h3>
                                <p>Says Yes!!</p>
                                <i class="fa fa-male"></i>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                            <div class="orga-stru">
                                <h3>15%</h3>
                                <p>Can't Say!!</p>
                                <i class="fa fa-male"></i>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="detail-info">
                            <hgroup>
                                <h3 class="det-txt">Is inclusive quality education affordable?</h3>
                                <h4 class="sm-txt">(Revised and Updated for 2016)</h4>
                            </hgroup>
                            <p class="det-p">Donec et lectus bibendum dolor dictum auctor in ac erat. Vestibulum egestas sollicitudin metus non urna in eros tincidunt convallis id id nisi in interdum.</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Organisations-->
        <!--Cta-->
        <section id="cta-2">
            <div class="container">
                <div class="row">
                    <div class="col-lg-12">
                        <h2 class="text-center">Subscribe Now</h2>
                        <p class="cta-2-txt">Sign up for our free weekly software design courses, we’ll send them right to your inbox.</p>
                        <div class="cta-2-form text-center">
                            <form action="#" method="post" id="workshop-newsletter-form">
                                <input name="" placeholder="Enter Your Email Address" type="email">
                                <input class="cta-2-form-submit-btn" value="Subscribe" type="submit">
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Cta-->
        <!--work-shop-->
        <section id="work-shop" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>Upcoming Workshop</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="service-box text-center">
                            <div class="icon-box">
                                <i class="fa fa-html5 color-green"></i>
                            </div>
                            <div class="icon-text">
                                <h4 class="ser-text">Mentor HTML5 Workshop</h4>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="service-box text-center">
                            <div class="icon-box">
                                <i class="fa fa-css3 color-green"></i>
                            </div>
                            <div class="icon-text">
                                <h4 class="ser-text">Mentor CSS3 Workshop</h4>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-6">
                        <div class="service-box text-center">
                            <div class="icon-box">
                                <i class="fa fa-joomla color-green"></i>
                            </div>
                            <div class="icon-text">
                                <h4 class="ser-text">Mentor Joomla Workshop</h4>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ work-shop-->
        <!--Faculity member-->
        <section id="faculity-member" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>Meet Our Faculty Member</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="pm-staff-profile-container">
                            <div class="pm-staff-profile-image-wrapper text-center">
                                <div class="pm-staff-profile-image">
                                    <img src="/theme/img/mentor.jpg" alt="" class="img-thumbnail img-circle" />
                                </div>
                            </div>
                            <div class="pm-staff-profile-details text-center">
                                <p class="pm-staff-profile-name">Bryan Johnson</p>
                                <p class="pm-staff-profile-title">Lead Software Engineer</p>

                                <p class="pm-staff-profile-bio">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec et placerat dui. In posuere metus et elit placerat tristique. Maecenas eu est in sem ullamcorper tincidunt. </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="pm-staff-profile-container">
                            <div class="pm-staff-profile-image-wrapper text-center">
                                <div class="pm-staff-profile-image">
                                    <img src="/theme/img/mentor.jpg" alt="" class="img-thumbnail img-circle" />
                                </div>
                            </div>
                            <div class="pm-staff-profile-details text-center">
                                <p class="pm-staff-profile-name">Bryan Johnson</p>
                                <p class="pm-staff-profile-title">Lead Software Engineer</p>

                                <p class="pm-staff-profile-bio">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec et placerat dui. In posuere metus et elit placerat tristique. Maecenas eu est in sem ullamcorper tincidunt. </p>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4">
                        <div class="pm-staff-profile-container">
                            <div class="pm-staff-profile-image-wrapper text-center">
                                <div class="pm-staff-profile-image">
                                    <img src="/theme/img/mentor.jpg" alt="" class="img-thumbnail img-circle" />
                                </div>
                            </div>
                            <div class="pm-staff-profile-details text-center">
                                <p class="pm-staff-profile-name">Bryan Johnson</p>
                                <p class="pm-staff-profile-title">Lead Software Engineer</p>

                                <p class="pm-staff-profile-bio">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec et placerat dui. In posuere metus et elit placerat tristique. Maecenas eu est in sem ullamcorper tincidunt. </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Faculity member-->
        <!--Testimonial-->
        <section id="testimonial" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2 class="white">See What Our Customer Are Saying?</h2>
                        <p class="white">
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line bg-white">
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <div class="text-comment">
                            <p class="text-par">"Proin gravida nibh vel velit auctor aliquet. Aenean sollicitudin, nec sagittis sem"</p>
                            <p class="text-name">Abraham Doe - Creative Dırector</p>
                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <div class="text-comment">
                            <p class="text-par">"Proin gravida nibh vel velit auctor aliquet. Aenean sollicitudin, nec sagittis sem"</p>
                            <p class="text-name">Abraham Doe - Creative Dırector</p>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Testimonial-->
        <!--Courses-->
        <section id="courses" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>Courses</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
                    </div>
                </div>
            </div>
            <div class="container">
                <div class="row">
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course01.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course02.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course03.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course04.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course05.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                    <div class="col-md-4 col-sm-6 padleft-right">
                        <figure class="imghvr-fold-up">
                            <img src="/theme/img/course06.jpg" class="img-responsive">
                            <figcaption>
                                <h3>Course Name</h3>
                                <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Magnam atque, nostrum veniam consequatur libero fugiat, similique quis.</p>
                            </figcaption>
                            <a href="#"></a>
                        </figure>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Courses-->
        <!--Pricing-->
        <section id="pricing" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>Our Pricing</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <div class="price-table">
                            <!-- Plan  -->
                            <div class="pricing-head">
                                <h4>Monthly Plan</h4>
                                <span class="fa fa-usd curency"></span>
                                <span class="amount">200</span>
                            </div>

                            <!-- Plean Detail -->
                            <div class="price-in mart-15">
                                <a href="#" class="btn btn-bg green btn-block">PURCHACE</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <div class="price-table">
                            <!-- Plan  -->
                            <div class="pricing-head">
                                <h4>Quarterly Plan</h4>
                                <span class="fa fa-usd curency"></span>
                                <span class="amount">800</span>
                            </div>

                            <!-- Plean Detail -->
                            <div class="price-in mart-15">
                                <a href="#" class="btn btn-bg yellow btn-block">PURCHACE</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 col-sm-4">
                        <div class="price-table">
                            <!-- Plan  -->
                            <div class="pricing-head">
                                <h4>Year Plan</h4>
                                <span class="fa fa-usd curency"></span>
                                <span class="amount">1200</span>
                            </div>

                            <!-- Plean Detail -->
                            <div class="price-in mart-15">
                                <a href="#" class="btn btn-bg red btn-block">PURCHACE</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!--/ Pricing-->
        <!--Contact-->
        <section id="contact" class="section-padding">
            <div class="container">
                <div class="row">
                    <div class="header-section text-center">
                        <h2>Contact Us</h2>
                        <p>
                            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Exercitationem nesciunt vitae,
                        <br>
                            maiores, magni dolorum aliquam.
                        </p>
                        <hr class="bottom-line">
                    </div>
                    <div id="sendmessage">Your message has been sent. Thank you!</div>
                    <div id="errormessage"></div>
                    <form action="" method="post" role="form" class="contactForm">
                        <div class="col-md-6 col-sm-6 col-xs-12 left">
                            <div class="form-group">
                                <input type="text" name="name" class="form-control form" id="name" placeholder="Your Name" data-rule="minlen:4" data-msg="Please enter at least 4 chars" />
                                <div class="validation"></div>
                            </div>
                            <div class="form-group">
                                <input type="email" class="form-control" name="email" id="email" placeholder="Your Email" data-rule="email" data-msg="Please enter a valid email" />
                                <div class="validation"></div>
                            </div>
                            <div class="form-group">
                                <input type="text" class="form-control" name="subject" id="subject" placeholder="Subject" data-rule="minlen:4" data-msg="Please enter at least 8 chars of subject" />
                                <div class="validation"></div>
                            </div>
                        </div>

                        <div class="col-md-6 col-sm-6 col-xs-12 right">
                            <div class="form-group">
                                <textarea class="form-control" name="message" rows="5" data-rule="required" data-msg="Please write something for us" placeholder="Message"></textarea>
                                <div class="validation"></div>
                            </div>
                        </div>

                        <div class="col-xs-12">
                            <!-- Button -->
                            <button type="submit" id="submit" name="submit" class="form contact-form-button light-form-button oswald light">SEND EMAIL</button>
                        </div>
                    </form>
                </div>
            </div>
        </section>
        <!--/ Contact-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
</asp:Content>
