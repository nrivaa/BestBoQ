<%@ Page Title="" Language="C#" MasterPageFile="~/HomeNestedMaster.master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="BestBoQ.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <section id="home" class="section-padding">
        <div class="container">
            <div class="row">
                <div class="header-section text-center">
                    <h2>My Profile</h2>
                    <p>
                        ข้อมูลผู้ใช้งาน
                    </p>
                    <hr class="bottom-line" />
                </div>
                <div class="profileForm col-sm-6" role="form">
                    <div class="form-group">
                        <label>Username:</label>
                        <asp:Label ID="lbUsername" runat="server" Text="#N/A"></asp:Label>
                    </div>
                    <div class="form-group radio-type">
                        <label>ประเภท :</label>
                        <asp:RadioButtonList ID="rbType" runat="server" Enabled="false">
                            <asp:ListItem>บุคคล</asp:ListItem>
                            <asp:ListItem>บริษัท</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <div class="form-group">
                        <label>E-mail:</label>
                        <asp:Label ID="lbEmail" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" autocomplete="off" data-validation="email" data-inputmask="'alias': 'email'" ID="tbEmail" runat="server" placeholder="Email" Visible="false"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Mobile Number:</label>
                        <asp:Label ID="lbMobile" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="tbMobile" runat="server" placeholder="Mobile Number" Visible="false" data-validation="required"  data-inputmask="'mask': '999-999-9999'"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Name/Company Name:</label>
                        <asp:Label ID="lbName" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="tbName" runat="server" placeholder="Name/Company Name" Visible="false" data-validation="length" data-validation-length="min5" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Address:</label>
                        <asp:Label ID="lbAddress" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" TextMode="MultiLine" Rows="3"  ID="tbAddress" runat="server" placeholder="Address" Visible="false" data-validation="length" data-validation-length="min10" autocomplete="off"></asp:TextBox>
                    </div>
                    <div class="form-group" id="blockId">
                        <label>ID Card:</label>
                        <asp:Label ID="lbId" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="tbId" runat="server" placeholder="ID Card" Visible="false" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                    </div>
                    <div class="form-group" id="blockTax">
                        <label>TAX ID:</label>
                        <asp:Label ID="lbTax" runat="server" Text="#N/A"></asp:Label>
                        <asp:TextBox CssClass="form-control" ID="tbTax" runat="server" placeholder="TAX" Visible="false" data-inputmask="'mask': '9 9999 99999 99 9'"></asp:TextBox>
                    </div>
                    <div>
                        <asp:Button ID="btnEdit" CssClass="light-form-button light" runat="server" Text="Edit Profile" OnClick="btnEdit_Click" />
                        <asp:Button ID="btnComfirm" CssClass="light-form-button light" runat="server" OnClientClick=" return $('.profileForm').isValid()" Text="Update Profile" Visible="false" OnClick="btnComfirm_Click" />
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div id="light" class="white_content2 ud-khaosod-survey" style="display: block; visibility: visible;">
        <div class="survey-wrapper">
            <img src="Images/MrBestBoQ_Close.png" />
            <a class="close-survey" href="javascript:void(0)" onclick="$('.white_content2').hide()"></a>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $(document).ready(function () {
            var f = $('.radio-type input:checked').val();

            if (f == "บุคคล") {
                $("#blockId").show();
                $("#<%=tbId.ClientID%>").attr("data-validation", "required");
                    $("#blockTax").hide();
                    $("#<%=tbTax.ClientID%>").removeAttr("data-validation");
            }
            else {
                $("#blockId").hide();
                $("#<%=tbId.ClientID%>").removeAttr("data-validation");
                    $("#blockTax").show();
                    $("#<%=tbTax.ClientID%>").attr("data-validation", "required");
            }

            validateForm('.profileForm');

        });
    </script>
</asp:Content>

