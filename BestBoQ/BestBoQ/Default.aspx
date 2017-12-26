<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BestBoQ._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">

    <nav class="navbar navbar-default navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a class="navbar-brand" href="Default.aspx">
                    <img src="/theme/img/logo.png" />
                </a>
            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
                <ul class="nav navbar-nav navbar-right">
                    <li>
                        <a href="#feature">การใช้งาน</a>
                    </li>
                    <li>
                        <a href="#register">สมัครใช้งาน</a>
                    </li>
                    <li class="dropdown">
                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Signed in as <%=Session["Username"] %><span class="caret"></span></a>
                        <ul class="dropdown-menu">
                            <li><a href="Login.aspx">Login</a></li>
                            <li><a href="Register.aspx">Register</a></li>
                            <li><a href="Profile.aspx">Profile</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="Home.aspx">Home</a></li>
                            <li><a href="CreateProj_01_Desc.aspx">Create Project 01 Description</a></li>
                            <li><a href="CreateProj_02_Home.aspx">Create Project 02 Home</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="GenerateContract.aspx">Generate Contract</a></li>
                            <li role="separator" class="divider"></li>
                            <li><a href="Logout.aspx">Logout</a></li>
                        </ul>
                    </li>
                    <li class="btn-trial">
                        <a href="#" data-target="#login" data-toggle="modal">เข้าสู่ระบบ</a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
    <!--/ Navigation bar-->

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
                        <p class="login-box-msg">Sign in to BestBoQ</p>
                        <div class="form-group">
                            <div class="form-group has-feedback">
                                <!----- username -------------->
                                <asp:TextBox ID="tbUsername" CssClass="form-control" runat="server" placeholder="Username" autocomplete="off"></asp:TextBox>
                                <span style="display: none; font-weight: bold; position: absolute; color: red; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginid"></span>
                                <!---Alredy exists  ! -->
                                <span class="fa fa-user  form-control-feedback"></span>
                            </div>
                            <div class="form-group has-feedback">
                                <!----- password -------------->
                                <asp:TextBox ID="tbPassword" CssClass="form-control" runat="server" placeholder="Password" TextMode="Password" autocomplete="off"></asp:TextBox>
                                <span style="display: none; font-weight: bold; position: absolute; color: grey; position: absolute; padding: 4px; font-size: 11px; background-color: rgba(128, 128, 128, 0.26); z-index: 17; right: 27px; top: 5px;" id="span_loginpsw"></span>
                                <!---Alredy exists  ! -->
                                <span class="fa fa-lock form-control-feedback"></span>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="checkbox icheck">
                                        <label>
                                            <input type="checkbox" id="loginrem" />
                                            Remember Me
                                        </label>
                                    </div>
                                </div>
                                <div class="col-xs-12">
                                    <asp:Button ID="btnLogin" CssClass="btn btn-green btn-block btn-flat" runat="server" Text="เข้าสู่ระบบ" OnClick="btnLogin_Click" />
                                </div>
                            </div>
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
    <!--Contact-->
    <section id="register" class="section-padding">
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
