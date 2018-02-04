<%@ Page Title="" Language="C#" MasterPageFile="~/CreateHomeNestedMaster.master" AutoEventWireup="true" CodeBehind="CreateProject_03_03.aspx.cs" Inherits="BestBoQ.CreateProject_03_03" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-small {
            padding: 1px 5px !important;
        }
    </style>
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

            <div class="alert alert-success confident-message text-center nearly-done" data-bind="html: progressTracker.nearlyDoneText"><strong>3. ระบบงานพื้น (FL)</strong></div>

            <div class="alert alert-danger alert-dismissible fade in" id="alertError" style="display: none" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close" style="top: 0; right: 0;">
                    <span aria-hidden="true">×</span>
                </button>
                <strong>พบข้อผิดพลาด!</strong> <span id="alert-message">กรุณาลองอีกครั้งหรือติดต่อผู้ดูแลระบบ</span>
            </div>
        </div>
    </div>
    <h3>Choose project materials <small>ระบบงานพื้น (FL)</small></h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body_left" runat="server">
    <div class="panel-body">
        <div class="booking-details">
            <div class="media media-ribbon media-xs-responsive">
                <div class="media-body">
                    <div class="form" role="form">
                        <div class="row">
                            <div class="col-sm-12 col-lg-6 text-center">
                                <div class="thumbnail">
                                    <img src="Images/03Floor/pic_floor.png" />
                                </div>
                            </div>
                            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                                <ItemTemplate>
                                    <div class="col-sm-12 col-lg-6">
                                        <div class="thumbnail">
                                            <%--<asp:Image ID="imgPic" runat="server" />--%>
                                            <div class="caption text-center">
                                                <h3>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("floorType")%>'></asp:Label>
                                                </h3>
                                                <label for="TextBox2" class="control-label">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                                                </label>
                                                <div class="row">
                                                    <div class="col-lg-<%#Eval("visible").ToString() == "true" ? "6": ""%>">
                                                        <div class="form-group has-feedback" style='<%#Eval("visible").ToString() == "true" ? "": "display:none"%>'>
                                                            <div class="input-group">
                                                                <asp:TextBox ID="TextBox2" data-inputmask="'alias': 'integer'" Text="0" data-validation="number" CssClass="form-control" runat="server"></asp:TextBox>
                                                                <span class="input-group-addon">ห้อง</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-<%#Eval("visible").ToString() == "true" ? "6": "12"%>">
                                                        <div class="form-group has-feedback">
                                                            <div class="input-group">
                                                                <asp:TextBox ID="TextBox3" data-inputmask="'alias': 'decimal'" data-validation-allowing="float" Text="0" data-validation="number,validFlooring" CssClass="form-control input-value" runat="server"></asp:TextBox>
                                                                <span class="input-group-addon">ตรม.</span>
                                                                <input type="hidden" class="dataCostMain" value="<%# Eval("cost")%>" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 blockFlooring">
                                                        ประเภทพื้น: <span class="lable-flooring-selected" >N/A</span>
                                                        <input type="button" class="btn btn-green btn-xs btn-small" value="เลือก" data-toggle="modal" data-target="#myModal<%# Eval("floorType").ToString().Trim()%>" />

                                                        <!-- Modal -->
                                                        <div class="modal fade" id="myModal<%# Eval("floorType").ToString().Trim()%>" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                                            <div class="modal-dialog" role="document">
                                                                <div class="modal-content">
                                                                    <div class="modal-header text-left">
                                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                                        <h4 class="modal-title" id="myModalLabel">เลือกประเภทพื้น (<%# Eval("detail") %>)</h4>
                                                                    </div>
                                                                    <div class="modal-body">
                                                                        <div class="row">
                                                                            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
                                                                                <ItemTemplate>
                                                                                    <div class="col-xs-6 col-md-4">
                                                                                        <div class="thumbnail">
                                                                                            <asp:Image ID="imgPic" ImageUrl='<%# Eval("picpath")%>' runat="server" />
                                                                                            <div class="caption text-center">
                                                                                                <h3>
                                                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("detail")%>'></asp:Label>
                                                                                                    <small>
                                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("flooringType")%>'></asp:Label></small>
                                                                                                </h3>
                                                                                                <div class="form-group has-feedback">
                                                                                                    <asp:RadioButton ID="RadioButton1" runat="server" />  
                                                                                                    <input type="hidden" class="dataCost" value="<%# Eval("cost")%>" />
                                                                                                    <input type="hidden" class="dataflag" value="<%# Eval("flag")%>" />
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </div>
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-default" data-dismiss="modal">ยกเลิก</button>
                                                                        <button type="button" class="btn btn-green btnSelectFlooring">เลือก</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <p class="text-right"><small>ราคารวม <span id="sectionPrice" data-value="<%=section_price %>">0</span> บาท</small></p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-xs-6">
                <a href="CreateProject_03_02?id=<%=param_projid%>" class="btn btn-default">Back to Previous Step</a>
            </div>
            <div class="col-xs-6 text-right">
                <asp:Button ID="btnSubmit" OnClientClick="return $('.form').isValid()" OnClick="btnSubmit_Click" CssClass="btn btn-green" runat="server" Text="Next" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="body_right" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {

            processPrice();
            initVaidateFlooring();

            function initVaidateFlooring() {
                $.formUtils.addValidator({
                    name: 'validFlooring',
                    validatorFunction: function (value, $el, config, language, $form) {
                        var val = parseInt(value);
                        if (val > 0) {
                            var floorVal = "N/A";
                            floorVal = $el.closest('.caption').find('.lable-flooring-selected').html();
                            return (floorVal != "N/A" && floorVal);
                        }
                        else {
                            return true;
                        }
                    },
                    errorMessage: 'Please select floor',
                    errorMessageKey: 'invalidFlooring'
                });
                // Setup form validation
                $.validate();
            }

            $(".btnSelectFlooring").click(function () {
                var elem_modal = $(this).closest(".modal");
                var val_selected = elem_modal.find("input[type=radio]:checked").closest(".thumbnail").find("h3 span").html();
                $(this).closest(".blockFlooring").find(".lable-flooring-selected").html(val_selected);
                processPrice();
                elem_modal.modal('hide');
            });

            $('.form input[type=text]').on("change", function () {
                processPrice();
            });


            function processPrice() {

                var sumPrice = 0.0;
                var sectionPriceElem = $("#sectionPrice");

                $('.modal').each(function () {
                    var em = $(this);
                    var em_radio_checked = em.find("input[type=radio]:checked")

                    if (em_radio_checked.length > 0) {
                        var em_block_flooring = em_radio_checked.closest(".thumbnail");
                        var val_flooring_selected = em_block_flooring.find("h3 span").html();

                        var cost = parseFloat(em_block_flooring.find(".dataCost").val());

                        var em_block = em.closest('.blockFlooring');
                        var em_text = em_block.find('.lable-flooring-selected').html(val_flooring_selected);

                        var em_main = $(this).closest(".caption");
                        var cost_main = parseFloat(em_main.find(".dataCostMain").val());
                        var MM = em_main.find(".input-value").val();

                        sumPrice = sumPrice + parseFloat(cost * MM) + parseFloat(cost_main * MM);
                    }
                    
                });

                updateTotalPriceFromSection(sectionPriceElem, sumPrice);
            }
            
        });
    </script>
</asp:Content>
