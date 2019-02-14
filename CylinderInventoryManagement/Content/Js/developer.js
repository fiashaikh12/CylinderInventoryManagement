
$(document).ready(function () {
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
        var IsValid = false;
        $('.validate-form').find('.textbox').each(function () {
            if ($(this).val() == "" || $(this).val() == null || $(this).val() == undefined || $(this).val()==0) {
                $(this).next().removeClass('not-required').addClass('required');
                IsValid = false;
            }
            else {
                $(this).next().removeClass('required').addClass('not-required');
                IsValid = true;
            }
        })
        return IsValid;
    };
    $('.submit').on('click', function () {
        var userId = parseInt($("#hdncustid").val());
        var challanNum, depositAmt, returnAmt;
        challanNum = $('#ChallanNumber').val();
        depositAmt = $('#DepositAmount').val();
        returnAmt = $('#ReturnDeposit').val();
        if (validateInput()) {
            var strPurArray = [];
            var strDepArray;
            strDepArray = { UserId: userId, DepositType: "G", DepositAmount: depositAmt };
            $('input[type=checkbox]').each(function () {
                if (this.checked) {
                    var prodId = parseInt($(this).closest('tr').find('td:eq(1)').text());
                    var purQty = parseInt($(this).closest('tr').find(".purchaseQuantity").val());
                    var rtQty = parseInt($(this).closest('tr').find(".returnQuantity").val());
                    var holdingQty = parseInt($(this).closest('tr').find('td:eq(9)').text());
                    var quantity = parseInt($(this).closest('tr').find('td:eq(7)').text());
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
                        strPurArray.push({ UserId: userId, ProductId: prodId, HoldingStock: holdingQty, PurchaseQuantity: purQty, ReturnQuantity: rtQty, ChallanNumber: challanNum })
                    }
                }
            });
            if (strPurArray.length > 0 && strDepArray) {
                $.ajax({
                    url: "/Customer/CustomerPurchaseReturnAsync",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ customerPurchaseReturn: strPurArray }),
                    success: function (data) {
                        console.log(data);
                        if (data.Status == 1) {
                            $.ajax({
                                url: "/Customer/CustomerDepositAsync",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                async: true,
                                data: JSON.stringify({ customerDeposiit: strDepArray }),
                                success: function (data) {
                                    console.log(data);
                                    if (data.Status == 1) {
                                        alert(21);
                                    }
                                    else {
                                        alert(22);
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }
    })


    $('#btnsubmitreport').click(function () {
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
                    url: "/Customer/SearchCustomerReport",
                    type: "GET",
                    contentType: 'application/html;charset=utf-8',
                    dataType: "html",
                    //data: JSON.stringify({ CustomerReport: obj }),
                    data: { userId: parseInt($("#hdncustid").val()), fromdate: temp[0].trim(), todate: temp[1].trim()},
                    success: function (data) {
                        $('.customer-cylinder1').removeClass('hidden');
                        $('#purchased-cylinder1').html(data);
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
            //debugger;
            $.ajax({
                url: "/Customer/SearchCustomerAuto",
                type: "POST",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { label: item.label, value: item.id, address: item.address, mobile: item.mobile, depositamount: item.depositamount};
                        //+return { label: item.label, value: item.id, name: item.Mobile + ',' + item.DepositAmount + ',' + item.address};
                        //address = customer.Address, mobile = customer.Mobile, depositamount = customer.DepositAmount
                    }));
                }

            });
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#lbl_custname").empty().append(ui.item.label);
            $("#lbl_mobile").empty().append(ui.item.mobile);
            $("#lbl_depositamount").empty().append(ui.item.depositamount);
            $("#lbl_address").empty().append(ui.item.address);
            $("#hdndepositamount").val(ui.item.depositamount);
            
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
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});

function printCustomerReport() {

    var divToPrint = document.getElementById('tblCustomerReportprint');

    var newWin = window.open('', 'Print-Window');

    newWin.document.open();

    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');

    newWin.document.close();

    setTimeout(function () { newWin.close(); }, 10);

}


