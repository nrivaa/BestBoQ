<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="boq.aspx.cs" Inherits="BestBoQ.boq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/fonts.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="theme/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="theme/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="theme/css/imagehover.min.css" />
    <link href="lib/icheck/skins/minimal/green.css" rel="stylesheet" />
    <link href="lib/Inputmask/css/inputmask.css" rel="stylesheet" />
    <link href="//cdnjs.cloudflare.com/ajax/libs/jquery-form-validator/2.3.26/theme-default.min.css" rel="stylesheet" type="text/css" />
    <link href="css/pluton.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="theme/css/style.css" />
    <script src="theme/js/jquery.min.js"></script>
    <link href="css/projectdetail.css" rel="stylesheet" />
    <link href="css/createproject.css" rel="stylesheet" />
    <style>
        .nav-documents img {
            height: 35px;
        }
        .border-bottom {
            border-bottom: 1px solid #e1e1e1;
        }
        @media print {
            .mdb-colordarken-3 {
                background-color: #1c2a48 !important;
                color: white !important;
            }
        }
        .blue-greylighten-4 {
            background-color: #cfd8dc !important;
        }
        @media print {
            body {-webkit-print-color-adjust: exact !important;}
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<%--        <div style="left: 0; line-height: 200px; margin-top: -100px; position: absolute; text-align: center; top: 50%; width: 100%;">
            
        </div>--%>
        <%--<div style="page-break-after: always;"></div>--%>
        <div class="row">
            <div class="col-sm-12">
                <h1>Project Information By <img width="80" src="Images/logo_new.png" /></h1>
                <%--<br />--%>
                <h1>รายละเอียดโครงการ</h1>
                <div class="panel panel-default" style="border: 1px solid #e1e1e1;width:100%">
                    <div class="panel-body">
                        <div class="edit-booking-property-infos">
                            <section class="edit-booking-wrapper">
                                <div id="edit-booking-hotel-header">
                                    <h1 class="edit-booking-hotel-name">
                                        <%=ProjectName %>
                                    </h1>
                                    <i class="edit-booking-hotel-start ficon ficon-16 orange-yellow ficon-star-style ficon-star-4"></i>
                                    <p class="hotel-address-map">
                                        <%=CustomerName %>
                                    </p>
                                </div>
                            </section>
                        </div>
                        <div class="edit-booking-informations">
                            <div class="row">
                                <div class="col-xs-3">
                                    <asp:Image Width="100%" CssClass="img-rounded text-center" ID="img" runat="server" />
                                </div>
                                <div class="col-xs-9" >
                                    <div class="edit-booking-info-block edit-booking-booking-id-info border-bottom">
                                        <div class="edit-booking-info-label">
                                            <div class="edit-booking-booking-id-info-title">ชื่อโครงการ</div>
                                            <div class="edit-booking-booking-id-info-title">หมายเลขสัญญา</div>
                                        </div>
                                        <div class="edit-booking-content">
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=ProjectName %>
                                            </div>
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=ContactName %>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="edit-booking-info-block edit-booking-info-with-border edit-booking-checkin-out-info border-bottom" data-selenium="edit-booking-checkin-out-info">
                                        <div class="edit-booking-info-label">
                                            <div class="edit-booking-check-in-title">ชื่อลูกค้า</div>
                                            <div class="edit-booking-check-out-title">ที่อยู่ลูกค้า</div>
                                        </div>
                                        <div class="edit-booking-content ">
                                            <div class="edit-booking-check-in-content">
                                                <%=CustomerName %>
                                            </div>
                                            <div class="edit-booking-check-out-content">
                                                <div class="edit-booking-check-out-date">
                                                    <%=ContactAdd %> 
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="edit-booking-info-block edit-booking-info-with-border edit-booking-contact-info border-bottom">
                                        <div class="edit-booking-info-label">
                                            <div class="edit-booking-booking-id-info-title">ประเภทโครงการ</div>
                                            <div class="edit-booking-booking-id-info-title">วันเริ่มต้นโครงการ</div>
                                        </div>
                                        <div class="edit-booking-content">
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=ProjectType %>
                                            </div>
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=ProjectStart %>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="edit-booking-info-block edit-booking-info-with-border edit-booking-contact-info">
                                        <div class="edit-booking-info-label">
                                            <div class="edit-booking-booking-id-info-title">พื้นที่ใช้สอย</div>
                                            <div class="edit-booking-booking-id-info-title">จำนวนห้อง</div>
                                        </div>
                                        <div class="edit-booking-content">
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=TotalArea %> ตารางเมตร
                                            </div>
                                            <div class="edit-booking-booking-id-info-content">
                                                <%=CountBedRoom %> ห้องนอน <%=CountBathRoom %> ห้องน้ำ
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h1>บัญชีแสดงปริมาณวัสดุ</h1>
            <h3><img width="70" src="Images/MrBestBoQ_left.png" /> งานโครงสร้าง</h3>
            <%=TableStructure %>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h3><img width="70" src="Images/MrBestBoQ_left.png" /> งานสถาปัตย์</h3>
            <%=TableArchitecture %>
        </div>
        <div style="page-break-after: always;"></div>
        <div>
            <h3><img width="70" src="Images/MrBestBoQ_left.png" /> งานระบบ</h3>
            <%=TableSystem %>
        </div>
    </form>
</body>
</html>
