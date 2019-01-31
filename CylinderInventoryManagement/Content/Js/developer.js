$(document).ready(function () {
    $('#updateCust').on('click', function () {
        var userId = parseInt($('#UserId option:selected').val());
        var depAmount = $(this).closest('tr').find("#item_DepositAmount").val();
        var purQuantity = $(this).closest('tr').find("#item_PurchaseQuantity").val();
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
            if (purQuantity > 0 && purQuantity < quantity) {
                $.ajax({
                    type: "POST",
                    url: "/Product/CustomerPurchase",
                    data: JSON.stringify({ clsCustomerPurchase: obj }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.Status == 1) {
                            alert("Purchase successfull");
                        }
                        else {
                            alert("Purchase failed");
                        }
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    },
                    error: function (response) {
                        alert(response.responseText);
                    }
                });
            }
            else {
                alert("Purchase quantity cannot be less than product quantity");
            }
        }
        else {
            alert("Select customer");
        }
    })
});