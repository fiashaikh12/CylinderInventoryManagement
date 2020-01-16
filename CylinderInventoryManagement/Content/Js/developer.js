
$(document).ready(function () {
    $('.overlay').hide();
    $('#dverror').hide();
    $('#dvsucess').hide();
    $('.purchaseCustomer').on('click', function () {
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
            var userId = parseInt($("#hdncustid").val());
            var depAmount = $(this).closest('tr').find(".depositAmount").val();
            var purQuantity = parseInt($(this).closest('tr').find(".purchaseQuantity").val());
            var quantity = parseInt($(this).closest('tr').find('td:eq(4)').text());
            var category = $(this).closest('tr').find('td:eq(0)').text();
            var subcategory = $(this).closest('tr').find('td:eq(1)').text();

            var obj = {};
            obj.DepositAmount = depAmount;
            obj.SubCategoryId = subcategory;
            obj.CategoryId = category;
            obj.Quantity = purQuantity;
            obj.UserId = userId;
            if (userId > 0) {
                if (purQuantity > 0) {
                    if (purQuantity <= quantity) {
                        $.ajax({
                            type: "POST",
                            url: "/Product/CustomerPurchase",
                            data: JSON.stringify({ clsCustomerPurchase: obj }),
                            contentType: "application/json; charset=utf-8",
                            async: true,
                            dataType: "json",
                            success: function (response) {
                                if (response.Status == 1) {
                                    SweetAlert("Purchase successfull", "success", "OK");
                                }
                                else {
                                    SweetAlert("Purchase failed", "error", "OK");
                                }
                            },
                            failure: function (response) {
                                SweetAlert(response.responseText, "error", "OK");
                            },
                            error: function (response) {
                                SweetAlert(response.responseText, "error", "OK");
                            }
                        });
                    }
                    else {
                        SweetAlert("Purchase quantity cannot be greater than product quantity", "warning", "OK");
                    }
                }
                else {
                    SweetAlert("Please provide quantity", "warning", "OK");
                }
            }
            else {
                SweetAlert("Select customer", "warning", "OK");
            }
        }
        else {
            $("#userid").empty();
            SweetAlert("Search customer first", "warning", "OK");
        }

    })

    $('.user_deposit_details').on('click', function () {
        debugger
        var Id = parseInt($("#hdncustid").val());
        if (Id > 0) {
            $.ajax({
                url: "/Distributor/GetUserDepositDetails",
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                data: { userId: Id },
                success: function (data) {
                    //$('.customer-cylinder').removeClass('hidden');
                    $('#depositDetails').html(data);
                }
            });
        }
        else {
            SweetAlert("Unexpected error", "warning", "OK");
        }
    });

    $(document).on('keyup', '.textbox', function () {
        if ($(this).val() != "") {
            $(this).next().removeClass('required').addClass('not-required');
        }
        else {
            $(this).next().removeClass('not-required').addClass('required');
        }
    });
    $(".numeric-with-decimal").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
        if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    $(".numeric-without-decimal").on("keypress keyup blur", function (event) {
        $(this).val($(this).val().replace(/[^\d].+/, ""));
        if ((event.which < 48 || event.which > 57)) {
            event.preventDefault();
        }
    });

    var validateInput = function () {
        var IsValid = true;
        $('.validate-form').find('.textbox').each(function () {
            if ($(this).val() == "" || $(this).val() == null || $(this).val() == undefined) {
                $(this).next().removeClass('not-required').addClass('required');
                IsValid = false;
            }
            else {
                $(this).next().removeClass('required').addClass('not-required');
                //IsValid = true;
            }
        })
        return IsValid;
    };

    $('.btn_notesubmit').on('click', function () {
        var userId = parseInt($("#hdncustid").val());
        var notes = $("#txt_Notes").val();
        if (notes != "" && notes != undefined) {
            $.ajax({
                url: "/Customer/CustomerNote",
                type: "POST",
                dataType: "json",
                data: { UserId: userId, Notes: notes },
                success: function (data) {
                    if (data = 1) {
                        //$('#dverror').hide();
                        //$('#dvsucess').show();
                        //$(function () {
                        //    setTimeout(function () {
                        //        $("#dvsucess").hide('blind', {}, 500)
                        //    }, 5000);
                        //});
                        SweetAlert("Notes Update Successfully", "warning", "OK");
                    }
                    else {
                        //$('#dverror').show();
                        //$('#dvsucess').hide();
                        //$(function () {
                        //    setTimeout(function () {
                        //        $("#dverror").hide('blind', {}, 500)
                        //    }, 5000);
                        //});
                        SweetAlert("Something Went Wrong!!!", "error", "OK");
                    }
                }
            });
        }
        else {
            SweetAlert("Notes cannot be blank", "error", "OK");
        }
    });

    $('.submit').on('click', function () {
        $('.overlay').show();

        var userId = parseInt($("#hdncustid").val());
        var challanNum, depositAmt, returnAmt, ChallanD;
        challanNum = $('#ChallanNumber').val();
        depositAmt = $('#DepositAmount').val();
        returnAmt = $('#ReturnDeposit').val();
        ChallanD = $('#challandate_datepicker').val();

        var d = new Date(ChallanD.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        ChallanD = mm + "/" + dd + "/" + yy;

        var chkIsHolding = 0;
        if ($('#chkHolding').is(":checked") == true) {
            chkIsHolding = 1;
        }
        if (validateInput()) {
            var strPurArray = [];

            $('input[type=checkbox]').each(function () {
                if (this.checked) {

                    if (this.getAttribute('id') != "chkHolding") {
                        var prodId = parseInt($(this).closest('tr').find('td:eq(1)').text());
                        var purQty = parseInt($(this).closest('tr').find(".purchaseQuantity").val());
                        var rtQty = parseInt($(this).closest('tr').find(".returnQuantity").val());
                        var holdingQty = parseInt($(this).closest('tr').find('td:eq(5)').text());
                        var quantity = parseInt($(this).closest('tr').find('td:eq(4)').text());
                        if (purQty > quantity) {
                            SweetAlert("Purchase quantity cannot be more than available quantity", "warning", "OK");
                        }
                        else if (rtQty > holdingQty) {
                            SweetAlert("Return quantity cannot be more than holding stock", "warning", "OK");
                        }
                        //else if (purQty == 0 && rtQty == 0) {
                        //    $(this).closest('tr').find(".purchaseQuantity").next().removeClass('not-required').addClass('required');
                        //    $(this).closest('tr').find(".returnQuantity").next().removeClass('not-required').addClass('required');
                        //}
                        //else if () {
                        //    $(this).closest('tr').find(".returnQuantity").next().removeClass('not-required').addClass('required');
                        //}
                        else {
                            strPurArray.push({ UserId: userId, ProductId: prodId, HoldingStock: holdingQty, PurchaseQuantity: purQty, ReturnQuantity: rtQty, ChallanNumber: challanNum, IsHolding: chkIsHolding, ChallanDate: ChallanD })
                        }
                    }
                }
            });
            if (strPurArray.length > 0) {
                $.ajax({
                    url: "/Customer/CustomerPurchaseReturnAsync",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ customerPurchaseReturn: strPurArray }),
                    success: function (data) {
                        //console.log(data);
                        if (data.Status == 1) {
                            var strDepArray;
                            var strDepReturnArray;
                            if (depositAmt > 0) {
                                strDepArray = { UserId: userId, DepositType: "G", DepositAmount: depositAmt };
                                $.ajax({
                                    url: "/Customer/CustomerDepositAsync",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: true,
                                    data: JSON.stringify({ customerDeposiit: strDepArray }),
                                    success: function (data) {
                                        console.log(data);
                                        if (data.Status == 1) {
                                            //alert(21);
                                        }
                                        else {
                                            //alert(22);
                                        }
                                    }
                                });
                            }

                            if (returnAmt > 0) {
                                strDepReturnArray = { UserId: userId, DepositType: "R", DepositAmount: returnAmt };
                                $.ajax({
                                    url: "/Customer/CustomerDepositAsync",
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    async: true,
                                    data: JSON.stringify({ customerDeposiit: strDepArray }),
                                    success: function (data) {
                                        console.log(data);
                                        if (data.Status == 1) {
                                            //alert(21);
                                        }
                                        else {
                                            //alert(22);
                                        }
                                    }
                                });
                            }

                            $.ajax({
                                url: "/Customer/GetPurchasedCylinder",
                                type: "GET",
                                contentType: 'application/html;charset=utf-8',
                                dataType: "html",
                                data: { userId: userId },
                                success: function (data) {
                                    $('.customer-cylinder').removeClass('hidden');
                                    $('#purchased-cylinder').html(data);
                                }
                            });
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dverror').hide();
                            $('#dvsucess').show();
                            $(function () {
                                setTimeout(function () {
                                    $("#dvsucess").hide('blind', {}, 500)
                                }, 5000);
                            });
                            $('#ChallanNumber').val("");
                            $('#DepositAmount').val(0);
                            $('#ReturnDeposit').val(0);
                            $('#challandate_datepicker').val("");
                        }
                        else if (data.Status == 400) {
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dverror').show();
                            $('#dvsucess').hide();
                            $('#dverror_span').empty().append('Something went wrong');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverror").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                        else {
                            $('#dverror').show();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').hide();
                            $('#dverror_span').empty().append('Challan number already exists');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverror").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                    }

                });
                $('.overlay').hide();
            }
            else {
                $('.overlay').hide();
            }
        }
        else {
            $('.overlay').hide();
        }
    });

    $('.Dis-submit').on('click', function () {
        $('.overlay').show();

        var userId = parseInt($("#hdncustid").val());
        var challanNum, ChallanD;
        challanNum = $('#ChallanNumber').val();
        ChallanD = $('#challandate_datepicker').val();

        var d = new Date(ChallanD.split("/").reverse().join("-"));
        var dd = d.getDate();
        var mm = d.getMonth() + 1;
        var yy = d.getFullYear();
        ChallanD = mm + "/" + dd + "/" + yy;

        var chkIsHolding = 0;
        if ($('#chkHolding').is(":checked") == true) {
            chkIsHolding = 1;
        }
        if (validateInput()) {
            var strPurArray = [];

            $('input[type=checkbox]').each(function () {
                if (this.checked) {
                    if (this.getAttribute('id') != "chkHolding") {
                        var prodId = parseInt($(this).closest('tr').find('td:eq(1)').text());
                        var purQty = parseInt($(this).closest('tr').find(".purchaseQuantity").val());
                        var rtQty = parseInt($(this).closest('tr').find(".returnQuantity").val());
                        var defQty = parseInt($(this).closest('tr').find(".defectQuantity").val());
                        var holdingQty = parseInt($(this).closest('tr').find('td:eq(5)').text());
                        var quantity = parseInt($(this).closest('tr').find('td:eq(4)').text());
                        if (purQty > quantity) {
                            SweetAlert("Issue quantity cannot be more than available quantity", "warning", "OK");
                        }
                        else if ((rtQty + defQty) > holdingQty) {
                            SweetAlert("Return quantity and Defect quantity cannot be more than holding stock", "warning", "OK");
                        }
                        else {
                            strPurArray.push({ UserId: userId, ProductId: prodId, HoldingStock: holdingQty, PurchaseQuantity: purQty, ReturnQuantity: rtQty, ChallanNumber: challanNum, DefectQuantity: defQty, IsHolding: chkIsHolding, ChallanDate: ChallanD })
                        }
                    }
                }
            });
            if (strPurArray.length > 0) {
                $.ajax({
                    url: "/Distributor/DistributorPurchaseReturnAsync",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ customerPurchaseReturn: strPurArray }),
                    success: function (data) {
                        //console.log(data);
                        if (data.Status == 1) {

                            $.ajax({
                                url: "/Distributor/SearchDistributorAuto",
                                type: "GET",
                                contentType: 'application/html;charset=utf-8',
                                dataType: "html",
                                data: { userId: userId },
                                success: function (data) {
                                    $('.customer-cylinder').removeClass('hidden');
                                    $('#purchased-cylinder').html(data);

                                    $('#ChallanNumber').val('');
                                    $('#challandate_datepicker').val('');

                                }
                            });

                            $('#dverror').hide();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').show();
                            $(function () {
                                setTimeout(function () {
                                    $("#dvsucess").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                        else if (data.Status == 400) {
                            $('#dverror').show();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').hide();
                            $('#dverror_span').empty().append('Something went wrong');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverror").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                        else {
                            $('#dverror').show();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').hide();
                            $('#dverror_span').empty().append('Bill number already exists');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverror").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                    }

                });
                $('.overlay').hide();
            }
            else {
                $('.overlay').hide();
            }
        }
        else {
            $('.overlay').hide();
        }
    });

    $('#btnsubmitreport').click(function () {
        //alert($('#daterange-btn').text().trim());
        //var temp = $('#daterange-btn').text().trim().split('-');
        fetchCustomerreport();
    });

    $('#btndistsubmitreport').click(function () {
        //alert($('#daterange-btn').text().trim());
        //var temp = $('#daterange-btn').text().trim().split('-');
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
            if ($('#daterange-btn').text().trim() != 'Date range picker') {
                var temp = $('#daterange-btn').text().trim().split('-');
                var obj = {};
                obj.fromdate = temp[0].trim();
                obj.todate = temp[1].trim();
                obj.UserId = parseInt($("#hdncustid").val());
                $.ajax({
                    url: "/Distributor/SearchCustomerReport",
                    type: "GET",
                    contentType: 'application/html;charset=utf-8',
                    dataType: "html",
                    //data: JSON.stringify({ CustomerReport: obj }),
                    data: { userId: parseInt($("#hdncustid").val()), fromdate: temp[0].trim(), todate: temp[1].trim() },
                    success: function (data) {
                        $('.customer-cylinder1').removeClass('hidden');
                        $('#purchased-cylinder1').html(data);

                        $.ajax({
                            url: "/Customer/SearchCustomerReportCount",
                            type: "GET",
                            contentType: 'application/html;charset=utf-8',
                            dataType: "html",
                            //data: JSON.stringify({ CustomerReport: obj }),
                            data: { userId: parseInt($("#hdncustid").val()), fromdate: temp[0].trim(), todate: temp[1].trim() },
                            success: function (data) {
                                $('#purchased-cylinder1Count').html(data);


                            }

                        });
                    }

                });
            }
            else {
                SweetAlert("Select date", "warning", "OK");
            }
        }
        else {
            $("#userid").empty();
            SweetAlert("Search customer first", "warning", "OK");
        }
    });

    var categoryname = "";
    var fullissue = "";
    var returnqty = "";
    var challannumber = "";
    var productid = "";
    var Trailid = "";
    $(document).on('click', '.editladger', function () {
        categoryname = $(this).attr('data-Categoryname');
        fullissue = $(this).attr('data-Fullissue');
        returnqty = $(this).attr('data-ReturnStock');
        challannumber = $(this).attr('data-Challannumber');
        productid = $(this).attr('data-ProductId');
        Trailid = $(this).attr('data-trailid');
        $("#modal-Updateladger").modal('show');
        $("#tdchallannumber").empty().append(challannumber);
        $("#tdCategoryname").empty().append(categoryname);
        $("#tdTrailid").empty().append(Trailid);
        $("#tdProductid").empty().append(productid);
        $(".TDpurchaseQuantity").val(fullissue);
        $(".TDreturnQuantity").val(returnqty);
    });

    $(document).on('click', '.deleteladger', function () {
        
        challannumber = $(this).attr('data-Challannumber');
        $('#btn_deletelager').attr('data-challannumber', challannumber);
        $('#dverrordelete').hide();
        $('#dvsucessdelete').hide();
        $("#modal-Deleteladger").modal('show');
    });
    
    $(document).on('click', '#btn_editlager', function () {
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
            if (validateInput()) {
                $.ajax({
                    url: "/Customer/UpdateCustomerLadger",
                    type: "POST",
                    dataType: "json",
                    data: { Userid: $("#hdncustid").val(), ProductId: productid, FullIssue: $(".TDpurchaseQuantity").val(), Return: $(".TDreturnQuantity").val(), Trail_id: Trailid },
                    success: function (data) {
                        //console.log(data);
                        if (data.Status == 1) {

                            $('#dverror').hide();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').show();
                            $(function () {
                                setTimeout(function () {
                                    fetchCustomerreport();
                                    $("#dvsucess").hide('blind', {}, 500);
                                    $("#modal-Updateladger").modal('hide');
                                }, 5000);
                            });

                        }
                        else {
                            $('#dverror').show();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucess').hide();
                            $('#dverror_span').empty().append('Something went wrong');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverror").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                    }

                });
            }
            else {
                $('#dverror').show();
                $("html, body").animate({ scrollTop: 0 }, "fast");
                $('#dvsucess').hide();
                $('#dverror_span').empty().append('All fields are mandatory');

                $(function () {
                    setTimeout(function () {
                        $("#dverror").hide('blind', {}, 500)
                    }, 5000);
                });

            }
        }
        else {

            $('#dverror').show();
            $("html, body").animate({ scrollTop: 0 }, "fast");
            $('#dvsucess').hide();
            $('#dverror_span').empty().append('Search Company once again');

            $(function () {
                setTimeout(function () {
                    $("#dverror").hide('blind', {}, 500)
                }, 5000);
            });
        }
    });

    $(document).on('click', '#btn_deletelager', function () {
        var ChallanNumber = $(this).attr('data-challannumber');
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined && ChallanNumber != "" && ChallanNumber != undefined) {
            
                $.ajax({
                    url: "/Customer/DeleteCustomerLadger",
                    type: "POST",
                    dataType: "json",
                    data: { Userid: $("#hdncustid").val(), ChallanNumber: ChallanNumber },
                    success: function (data) {
                        //console.log(data);
                        if (data.Status == 1) {

                            $('#dverrordelete').hide();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucessdelete').show();
                            $(function () {
                                setTimeout(function () {
                                    fetchCustomerreport();
                                    $("#dvsucessdelete").hide('blind', {}, 500);
                                    $("#modal-Deleteladger").modal('hide');
                                }, 5000);
                            });

                        }
                        else {
                            $('#dverrordelete').show();
                            $("html, body").animate({ scrollTop: 0 }, "fast");
                            $('#dvsucessdelete').hide();
                            $('#dverror_span_delete').empty().append('Something went wrong');

                            $(function () {
                                setTimeout(function () {
                                    $("#dverrordelete").hide('blind', {}, 500)
                                }, 5000);
                            });
                        }
                    }

                });
        }
        else {

            $('#dverrordelete').show();
            $("html, body").animate({ scrollTop: 0 }, "fast");
            $('#dvsucessdelete').hide();
            $('#dverror_span_delete').empty().append('Search Company once again');

            $(function () {
                setTimeout(function () {
                    $("#dverrordelete").hide('blind', {}, 500)
                }, 5000);
            });
        }
    });
    $(document).on('click', '#btnsaveRatecard', function () {
        var custid = $('#CustomerId').val();
        var category = $('#CategoryId').val();
        var rate = $('#RateCard').val();
        var objrateCard = { CustomerId: custid, CategoryId: category, RateCard: rate };
        if (custid != '' && category != 0 && rate != '') {
            $.ajax({
                url: "/Customer/CreateRateCard",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ rateCard: objrateCard }),
                success: function (data) {
                    if (data == 1) {
                        $.ajax({
                            url: "/Customer/GetRateCardDetails",
                            type: "GET",
                            contentType: 'application/html;charset=utf-8',
                            dataType: "html",
                            data: { userId: custid },
                            success: function (data) {
                                //$('.rateCard').('show');
                                $('#rateCardDetails').html(data);
                            }
                        });
                        $('#dverror').hide();
                        $("html, body").animate({ scrollTop: 0 }, "fast");
                        $('#dvsucess').show();
                        $(function () {
                            setTimeout(function () {
                                $("#dvsucess").hide('blind', {}, 500);
                            }, 5000);
                        });

                    }
                    else {
                        $('#dverror').show();
                        $("html, body").animate({ scrollTop: 0 }, "fast");
                        $('#dvsucess').hide();
                        $('#dverror_span').empty().append('Something went wrong');

                        $(function () {
                            setTimeout(function () {
                                $("#dverror").hide('blind', {}, 500)
                            }, 5000);
                        });
                    }
                }
            });
        }
        else {

            $('#dverror').show();
            $("html, body").animate({ scrollTop: 0 }, "fast");
            $('#dvsucess').hide();
            $('#dverror_span').empty().append('all fields are mandatory');

            $(function () {
                setTimeout(function () {
                    $("#dverror").hide('blind', {}, 500)
                }, 5000);
            });
        }
    });

});
function SweetAlert(message, icon, buttonText) {
    swal({
        title: "",
        text: message,
        icon: icon,
        button: buttonText,
    });
}

$(function () {
    $("#userid").autocomplete({
        source: function (request, response) {

            $.ajax({
                url: "/Customer/SearchCustomerAuto",
                type: "POST",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    // debugger;
                    if (data != "") {
                        response($.map(data, function (item) {
                            return { label: item.label, value: item.id, address: item.address, mobile: item.mobile, depositamount: item.depositamount, notes: item.notes, alternatenumber: item.alternatenumber };
                            //+return { label: item.label, value: item.id, name: item.Mobile + ',' + item.DepositAmount + ',' + item.address};
                            //address = customer.Address, mobile = customer.Mobile, depositamount = customer.DepositAmount
                        }));
                    } else {
                        alert("User dosen't exists");

                    }
                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#lbl_custname").empty().append(ui.item.label);
            $("#lbl_mobile").empty().append(ui.item.mobile + " /" + ui.item.alternatenumber);
            $("#lbl_depositamount").empty().append(ui.item.depositamount);
            $("#lbl_address").empty().append(ui.item.address);
            $("#hdndepositamount").val(ui.item.depositamount);
            $("#txt_Notes").empty().append(ui.item.notes);
            $("#hdncustid").val(value);
            $("#userid").val(label);
            $.ajax({
                url: "/Customer/GetPurchasedCylinder",
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                data: { userId: value },
                success: function (data) {
                    $('.customer-cylinder').removeClass('hidden');
                    $('#purchased-cylinder').html(data);
                    $.ajax({
                        url: "/Customer/GetRateCardDetails",
                        type: "GET",
                        contentType: 'application/html;charset=utf-8',
                        dataType: "html",
                        data: { userId: value },
                        success: function (data) {
                            //$('.rateCard').('show');
                            $('#rateCardDetails').html(data);
                        }
                    });
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });

    $("#Username").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Customer/SearchCustomerAuto",
                type: "POST",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    if (data != "") {

                        response($.map(data, function (item) {
                            //console.log(item)
                            return { label: item.label, value: item.id, address: item.address, mobile: item.mobile, depositamount: item.depositamount, notes: item.notes, alternatenumber: item.alternatenumber };
                        }));
                    } else {
                        alert("User dosen't exists");

                    }


                }
            });
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#CustomerId").val(value);
            $("#Username").val(label);
            $.ajax({
                url: "/Customer/GetRateCardDetails",
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                data: { userId: value },
                success: function (data) {
                    //$('.rateCard').('show');
                    $('#rateCardDetails').html(data);
                }
            });
        },
        messages: {
            noResults: "Data not found", results: "Data not found"
        }
    });

    $("#DISuserid").autocomplete({
        source: function (request, response) {
            //debugger;
            $.ajax({
                url: "/Distributor/SearchDistributorAuto",
                type: "POST",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    if (data != "") {
                        response($.map(data, function (item) {
                            return { label: item.label, value: item.id, address: item.address, mobile: item.mobile, depositamount: item.depositamount, alternatenumber: item.alternatenumber };
                            //+return { label: item.label, value: item.id, name: item.Mobile + ',' + item.DepositAmount + ',' + item.address};
                            //address = customer.Address, mobile = customer.Mobile, depositamount = customer.DepositAmount
                        }));
                    } else {
                        alert("User dosen't exists");

                    }


                }

            });
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#lbl_custname").empty().append(ui.item.label);
            $("#lbl_mobile").empty().append(ui.item.mobile + " /" + ui.item.alternatenumber);
            $("#lbl_depositamount").empty().append(ui.item.depositamount);
            $("#lbl_address").empty().append(ui.item.address);
            $("#hdndepositamount").val(ui.item.depositamount);
            $("#hdncustid").val(value);
            $("#DISuserid").val(label);
            $.ajax({
                url: "/Distributor/GetPurchasedCylinder",
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                data: { userId: value },
                success: function (data) {
                    $('.customer-cylinder').removeClass('hidden');
                    $('#purchased-cylinder').html(data);
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});

function printCustomerReport() {

    var divToPrint = document.getElementById('printdiv');

    var newWin = window.open('', 'Print-Window');

    newWin.document.open();

    newWin.document.write('<html><head><style>table { page-break-inside:auto; } td { border: 1px solid lightgray; } tr { page-break-inside: auto; } #example1_length,#example1_filter,#example1_info,#example1_paginate,.btn {display: none;}</style></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

    newWin.document.close();

    setTimeout(function () { newWin.close(); }, 10);

}


var isShift = false;
var seperator = "/";
window.onload = function () {
    //Reference the Table.
    var tblForm = document.getElementById("challandate_datepicker");

    //Reference all INPUT elements in the Table.
    var inputs = document.getElementsByTagName("input");

    //Loop through all INPUT elements.
    for (var i = 0; i < inputs.length; i++) {
        //Check whether the INPUT element is TextBox.
        if (inputs[i].type == "text") {
            //Check whether Date Format Validation is required.
            if (inputs[i].className.indexOf("date-format") >= 1) {

                //Set Max Length.
                inputs[i].setAttribute("maxlength", 10);

                //Only allow Numeric Keys.
                inputs[i].onkeydown = function (e) {
                    return IsNumeric(this, e.keyCode);
                };

                //Validate Date as User types.
                inputs[i].onkeyup = function (e) {
                    ValidateDateFormat(this, e.keyCode);
                };
            }
        }
    }
};

function IsNumeric(input, keyCode) {
    if (keyCode == 16) {
        isShift = true;
    }
    //Allow only Numeric Keys.
    if (((keyCode >= 48 && keyCode <= 57) || keyCode == 8 || keyCode <= 37 || keyCode <= 39 || (keyCode >= 96 && keyCode <= 105)) && isShift == false) {
        if ((input.value.length == 2 || input.value.length == 5) && keyCode != 8) {
            input.value += seperator;
        }

        return true;
    }
    else {
        return false;
    }
};

function ValidateDateFormat(input, keyCode) {
    var dateString = input.value;
    if (keyCode == 16) {
        isShift = false;
    }
    var regex = /(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$/;

    //Check whether valid dd/MM/yyyy Date Format.
    if (regex.test(dateString) || dateString.length == 0) {
        ShowHideError(input, "none");
    } else {
        ShowHideError(input, "block");
    }
};

function ShowHideError(textbox, display) {
    var row = textbox.parentNode.parentNode;
    var errorMsg = row.getElementsByTagName("span")[0];
    if (errorMsg != null) {
        errorMsg.style.display = display;
        errorMsg.empty().append("Invalid Date. Only dd/MM/yyyy format allowed.");
    }
};

function fetchCustomerreport() {
    if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
        if ($('#daterange-btn').text().trim() != 'Date range picker') {
            var temp = $('#daterange-btn').text().trim().split('-');
            var obj = {};
            obj.fromdate = temp[0].trim();
            obj.todate = temp[1].trim();
            obj.UserId = parseInt($("#hdncustid").val());
            $.ajax({
                url: "/Customer/SearchCustomerReport",
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                //data: JSON.stringify({ CustomerReport: obj }),
                data: { userId: parseInt($("#hdncustid").val()), fromdate: temp[0].trim(), todate: temp[1].trim() },
                success: function (data) {
                    $('.customer-cylinder1').removeClass('hidden');
                    $('#purchased-cylinder1').html(data);

                    $.ajax({
                        url: "/Customer/SearchCustomerReportCount",
                        type: "GET",
                        contentType: 'application/html;charset=utf-8',
                        dataType: "html",
                        //data: JSON.stringify({ CustomerReport: obj }),
                        data: { userId: parseInt($("#hdncustid").val()), fromdate: temp[0].trim(), todate: temp[1].trim() },
                        success: function (data) {
                            $('#purchased-cylinder1Count').html(data);
                            
                        }

                    });

                }

            });
        }
        else {
            SweetAlert("Select date", "warning", "OK");
        }
    }
    else {
        $("#userid").empty();
        SweetAlert("Search customer first", "warning", "OK");
    }

}

