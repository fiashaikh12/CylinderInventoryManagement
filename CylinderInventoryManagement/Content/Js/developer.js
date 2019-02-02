$(document).ready(function () {
    $('#purchaseCustomer').on('click', function () {
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

});
function SweetAlert(message,icon,buttonText) {
    swal({
        title: "",
        text: message,
        icon: icon,
        button: buttonText,
    });
}


$(function () {
    //var availableTags = [
    //    "ActionScript", "AppleScript", "Asp", "BASIC", "C", "C++",
    //    "Clojure", "COBOL", "ColdFusion", "Erlang", "Fortran",
    //    "Groovy", "Haskell", "Java", "JavaScript", "Lisp", "Perl",
    //    "PHP", "Python", "Ruby", "Scala", "Scheme"
    //];

    $("#userid").autocomplete({
        //source: availableTags
        source: function (request, response) {
            //debugger;
            $.ajax({
                url: "/Customer/SearchCustomerAuto",
                type: "POST",
                dataType: "json",
                data: { searchText: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        //original code
                        //return { label: item.FullName, value: item.FullName, id: item.TagId };
                        //updated code
                        return { label: item.label,value: item.id };
                    }));
                }

            });
            //alert(response);
        },
        select: function (event, ui) {
            event.preventDefault();
            var label = ui.item.label;
            var value = ui.item.value;
            $("#hdncustid").val(value);
            $("#userid").val(label);
            
            //store in session
            //alert(value);
        },
        messages: {
            noResults: "", results: ""
        }
    });
});