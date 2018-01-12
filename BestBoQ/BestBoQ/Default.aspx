<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BestBoQ._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="navcontent" runat="server">
    <ul class="nav navbar-nav navbar-right">
        <li>
            <a href="#feature">Features</a>
        </li>
        <li>
            <a href="#register">Register</a>
        </li>
        <li class="btn-trial">
            <a href="#" data-target="#login" data-toggle="modal">Login</a>
        </li>
    </ul>
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
                        <p class="login-box-msg">Sign in to BestBoQ</p>
                        <div class="form-group">
                            <div class="form-group has-feedback">
                                <!----- username -------------->
                                <asp:TextBox ID="tbLoginUsername" CssClass="form-control" runat="server" placeholder="Username" autocomplete="off"></asp:TextBox>
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
                    <img src="https://ununsplash.imgix.net/photo-1416339134316-0e91dc9ded92?q=75&fm=jpg&s=883a422e10fc4149893984019f63c818" />
                </div>
                <div class="item slides">
                    <img src="https://ununsplash.imgix.net/photo-1416339684178-3a239570f315?q=75&fm=jpg&s=c39d9a3bf66d6566b9608a9f1f3765af" />
                </div>
                <div class="item slides">
                    <img src="https://ununsplash.imgix.net/photo-1416339276121-ba1dfa199912?q=75&fm=jpg&s=9bf9f2ef5be5cb5eee5255e7765cb327" />
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
                    <h2>Features</h2>
                    <p>
                        ขั้นตอนการใช้งานโปรแกรม BESTBoQ
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
        </div>
    </section>
    <!--/ feature-->
    <!--Contact-->
    <section id="register" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <h2>Register</h2>
                    <p>
                        สมัครสมาชิก Nature Estate เพื่อใช้งานโปรแกรม BESTBoQ
                    </p>
                    <hr class="bottom-line" />
                </div>
                <div id="sendmessage">Your message has been sent. Thank you!</div>
                <div id="errormessage"></div>
                <div class="contactForm" role="form">
                    <div class="col-md-6 col-sm-6 col-xs-12 left">
                        <div class="form-group">
                            <label>ข้อมูลทั่วไป</label>
                            <div class="input-group">
                                <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                                <asp:TextBox ID="tbUsername" autocomplete="off" data-validation="required" CssClass="form-control" runat="server" placeholder="Username"></asp:TextBox>

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
                                <input type="password" id="tbRepassword" runat="server" autocomplete="off" data-validation="required" name="pass_confirmation" class="form-control" placeholder="Confirm Password" />

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
                    </div>

                    <div class="col-md-6 col-sm-6 col-xs-12 right">
                        <label>ข้อมูลเกี่ยวกับบริษัท/บุคคล</label>
                        <div class="form-group form-group-radio">
                            <label class="col-sm-2 control-label lable_type">ประเภท</label>
                            <div class="col-sm-10">
                                <asp:RadioButtonList class="radio-type" ID="rbType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True">บุคคล</asp:ListItem>
                                    <asp:ListItem>บริษัท</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
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
                    </div>

                    <div class="col-xs-12">
                        <!-- Button -->
                        <asp:Button ID="btnRegister" runat="server" Text="Register" OnClientClick=" return $('.contactForm').isValid()" CssClass="form contact-form-button light-form-button oswald light" OnClick="btnRegister_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--/ Contact-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script src="theme/js/contactform.js"></script>
    <script>
        $(document).ready(function () {

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

        });
    </script>
</asp:Content>
