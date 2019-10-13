<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_15.aspx.cs" Inherits="BestBoQ.CreateProject_03_15" %>

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

            <div class="alert alert-success confident-message text-center nearly-done">
                <nav>
                    <ul class="pager">
                        <li class="previous hidden"><a href="CreateProject_03_14?id=<%=param_projid%>"><span aria-hidden="true">&larr;</span></a></li>
                        <li data-toggle="tooltip" data-placement="bottom" title="1.ระบบงานฐานราก (Footing)">
                            <a href="CreateProject_03_01?id=<%=param_projid%>">1</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="2.ระบบงานคานคอดิน (Beam)">
                            <a href="CreateProject_03_02?id=<%=param_projid%>">2</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="3.ระบบงานพื้น (FL)">
                            <a href="CreateProject_03_03?id=<%=param_projid%>">3</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="4.ระบบงานหลังคา (Roof)">
                            <a href="CreateProject_03_04?id=<%=param_projid%>">4</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="5.ระบบงานผนัง (Wall)">
                            <a href="CreateProject_03_05?id=<%=param_projid%>">5</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="6.ระบบงานสุขภัณฑ์ห้องน้า (Toilet)">
                            <a href="CreateProject_03_07?id=<%=param_projid%>">6</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="7.ระบบงานสุขาภิบาล (Sanitation)">
                            <a href="CreateProject_03_08?id=<%=param_projid%>">7</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="8.ระบบงานประปา (Plumbing)">
                            <a href="CreateProject_03_09?id=<%=param_projid%>">8</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="9.งานฝ้า (Ceiling)">
                            <a href="CreateProject_03_10?id=<%=param_projid%>">9</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="10.งานระบบไฟฟ้า">
                            <a href="CreateProject_03_11?id=<%=param_projid%>">10</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="11.งานสี (Color)">
                            <a href="CreateProject_03_12?id=<%=param_projid%>">11</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="12.ราวระเบียง และราวบันได Railing">
                            <a href="CreateProject_03_13?id=<%=param_projid%>">12</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="13.Window & Door+Fitting">
                            <a href="CreateProject_03_14?id=<%=param_projid%>">13</a>
                        </li>
                        <li data-toggle="tooltip" data-placement="bottom" title="14.ค่าดำเนินงาน/ส่วนลด">
                            <a href="CreateProject_03_15?id=<%=param_projid%>">14</a>
                        </li>
                    </ul>
                </nav>
            </div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>ค่าดำเนินงาน/ส่วนลด</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="row">
                            <div class="col-sm-6 col-md-4">
                                <div class="form-group has-feedback">
                                    <label for="tbfree" class="control-label">ค่าดำเนินการ</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="tbfree" CssClass="form-control" autocomplete="off" data-inputmask="'alias': 'decimal'" data-validation="number" data-validation-allowing="range[0;100],float" Text="0" runat="server" placeholder="ค่าดำเนินการ" data-validation-error-msg="Please enter a number between 0 and 100"></asp:TextBox>
                                        <span class="input-group-addon">%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-md-4">
                                <div class="form-group has-feedback">
                                    <label for="tbpromotion" class="control-label">ส่วนลด</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="tbpromotion" data-inputmask="'alias': 'decimal'" CssClass="form-control" Text="0" autocomplete="off" runat="server" placeholder="ส่วนลด"></asp:TextBox>
                                        <span class="input-group-addon">บาท</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6 col-md-4">
                                <div class="form-group has-feedback">
                                    <label for="tbpromotion" class="control-label">อื่นๆ (โปรดระบุุ)</label>
                                    <asp:TextBox ID="tbdetail" CssClass="form-control" autocomplete="off" runat="server" placeholder="อื่นๆ"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-4">
                                <div class="form-group has-feedback">
                                    <label for="tbother" class="control-label">จำนวนเงิน</label>
                                    <div class="input-group">
                                        <asp:TextBox ID="tbother" data-inputmask="'alias': 'decimal'" data-validation="number" data-validation-allowing="float,negative" CssClass="form-control" Text="0" autocomplete="off" runat="server" placeholder="จำนวนเงิน"></asp:TextBox>
                                        <span class="input-group-addon">บาท</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <h4>งวดการชำระเงินค่าก่อสร้าง</h4>
                                <div class="table-responsive">
                                <table class="table table-striped" id="tablePaymentTerm">
                                    <thead>
                                        <tr>
                                            <th>ลำดับ</th>
                                            <th>รายการ</th>
                                            <th style="width: 25%">จำนวน (%)</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("term")%>'></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="tbDetail" CssClass="form-control" runat="server" Text='<%# Eval("detail")%>' autocomplete="off" data-validation="required" ></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <div class="form-group has-feedback">
                                                       <%-- <div class="input-group text-left">--%>
                                                        <asp:TextBox ID="tbPCT" CssClass="form-control tbPCT" runat="server" Text='<%# Eval("pct")%>' 
                                                             autocomplete="off" data-inputmask="'alias': 'decimal'"  data-validation="number,maxPCT" data-validation-allowing="range[0;100],float"></asp:TextBox>
                                                             <%--<span class="input-group-addon">%</span>--%>
                                                           <%-- </div>--%>
                                                            </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                </div>
                                <p class="text-right">
                                    ยังเหลือค่าอีกร้อยละ <asp:Label ID="tbRemainPCT" ForeColor="Red" runat="server" Text='100'></asp:Label> ที่ยังไม่ได้ใส่ในตาราง
                                </p>
                            </div></div> </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        <a href="CreateProject_03_14?id=<%=param_projid%>" class="btn btn-greenline">Back to Previous Step</a>
                    </div>
                    <div class="col-xs-6 text-right">
                        <button type="button" id="btnSubmit" data-toggle="modal" data-target="#myModal" class="btn btn-green">ประเมิน</button>
                    </div>
                </div>
            </div>

            <%--  <!-- Button trigger modal -->
<button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#myModal">
  Launch demo modal
</button>--%>

            <!-- Modal -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="background-color: #26A65B;">
                        <div class="modal-body text-center">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <p>
                                <img width="150" src="Images/MrBestBoQ.png" />
                            </p>
                            <p style="color: aliceblue">คุณต้องการจะยืนยันการประเมินโครงการใช่หรือไม่?</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnYes" OnClientClick="return $('.form').isValid()"  OnClick="btnSubmit_Click" CssClass="btn btn-success" runat="server" Text="ใช่" />
                            <button type="button" class="btn btn-default" data-dismiss="modal">ไม่ใช่</button>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
    <script>
        var tbfree = '#<%=tbfree.ClientID%>';
        var tbpromotion = '#<%=tbpromotion.ClientID%>';
        var tbother = '#<%=tbother.ClientID%>';
        var tbRemainPCT = '#<%=tbRemainPCT.ClientID%>';
        

        var totalPrice = $("#totalPrice");
        var totalPriceValue = parseFloat(totalPrice.data("value"));

        var feePriceElem = $("#feePrice");
        var promoPriceElem = $("#promoPrice");
        var otherPriceElem = $("#otherPrice");
        var lastPriceElem = $("#lastPrice");

        initVaidateMaxPCT();
        
        function initVaidateMaxPCT() {
            $.formUtils.addValidator({
                name: 'maxPCT',
                validatorFunction: function (value, $el, config, language, $form) {

                    $.validate();

                    var remainVal = 0;

                    var inputPaymentTerm = $("#tablePaymentTerm .tbPCT");

                    inputPaymentTerm.each(function() {
                        remainVal = remainVal + parseFloat($(this).val());
                    });

                    return remainVal == 100;
                },
                errorMessage: 'จำนวนรวมทั้งหมดยังขาดหรือเกิน 100%',
                errorMessageKey: 'badMaxPCT'
            });

            // Setup form validation
            $.validate();
        }

        calulateTerm();

        $("input").change(function () {

            var val = $(this).val();
            var id = $(this).attr("id");

            var feePrice = $(tbfree).val();
            var promoPrice = $(tbpromotion).val();
            var otherPrice = $(tbother).val();

            if (val >= 0 || "#" + id == tbother) { // isValid

                var fee = calPriceValuePercent(totalPriceValue, parseFloat(feePrice));
                var promo = parseFloat(promoPrice);
                var other = parseFloat(otherPrice);
                var last = totalPriceValue + parseFloat(fee) - parseFloat(promo) + parseFloat(other);

                feePriceElem.html(convertFloatToString(fee));
                promoPriceElem.html(convertFloatToString(promo));
                otherPriceElem.html(convertFloatToString(other));
                lastPriceElem.html(convertFloatToString(last));
            }

            calulateTerm();
            $('.form').isValid();
        });


        function calulateTerm() {
            var remainVal = 100;

            var inputPaymentTerm = $("#tablePaymentTerm .tbPCT");

            inputPaymentTerm.each(function() {
                remainVal = remainVal - parseFloat($(this).val());
            });

            $(tbRemainPCT).text(remainVal);
        }
    </script>
</asp:Content>
