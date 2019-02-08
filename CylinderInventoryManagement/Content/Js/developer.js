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
    $(document).on('click', '.return-cylinder', function () {
        if ($("#hdncustid").val() != "" && $("#hdncustid").val() != undefined) {
            var userId = parseInt($("#hdncustid").val());
            var productId = parseInt($('#item_ProductId').val());
            var depAmount = $(this).closest('tr').find(".depositAmount").val();
            var purQuantity = parseInt($(this).closest('tr').find(".purchaseQuantity").val());
            var quantity = parseInt($(this).closest('tr').find('td:eq(4)').text());

            var obj = {};
            obj.DepositAmount = depAmount;
            obj.ProductId = productId;
            obj.Quantity = purQuantity;
            obj.UserId = userId;
            $.ajax({
                type: "POST",
                url: "/Product/CustomerReturn",
                data: JSON.stringify({ clsCustomerReturn: obj }),
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: function (response) {
                    if (response.Status == 1) {
                        SweetAlert("Cylinder returned successfull", "success", "OK");
                    }
                    else {
                        SweetAlert("Somwthing went wrong", "error", "OK");
                    }
                },
                failure: function (response) {
                    console.log(response.responseText);
                },
                error: function (response) {
                    console.log(response.responseText);
                }
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
                type: "GET",
                contentType: 'application/html;charset=utf-8',
                dataType: "html",
                data: { userId: value },
                success: function (data) {
                    $('#purchased-cylinder').html(data);
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});