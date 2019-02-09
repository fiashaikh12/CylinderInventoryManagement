$(document).ready(function () {
    $('.purchaseCustomer').on('click', function () {
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
            var userId = parseInt($("#hdncustid").val());
            var depAmount = $(this).closest('tr').find("#item_DepositAmount").val();
            var purQuantity = parseInt($(this).closest('tr').find("#item_PurchaseQuantity").val());
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
    //$(document).on('change', '#userid', function () {
    //    if ($('#hdncustid').val()) {
    //        $.ajax({
    //            url: "/Customer/GetPurchasedCylinder",
    //            type: "POST",
    //            dataType: "json",
    //            data: { userId: $("#hdncustid").val() },
    //            success: function (data) {
    //                console.log(data)
    //            }
    //        });
    //    }
    //});


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
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    async: true,
                    data: JSON.stringify({ CustomerReport: obj }),
                    success: function (data) {
                        console.log(data);
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
                        return { label: item.label, value: item.id };
                    }));
                }

            });
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#hdncustid").val(value);
            $("#userid").val(label);
            $.ajax({
                url: "/Customer/GetPurchasedCylinder",
                type: "POST",
                dataType: "json",
                data: { userId: value },
                success: function (data) {
                    console.log(data)
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});


