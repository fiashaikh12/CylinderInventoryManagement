$(document).ready(function () {
    $('#purchaseCustomer').on('click', function () {
        var userId = parseInt($('#UserId option:selected').val());
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
            SweetAlert("Select customer","warning","OK");
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