﻿function functAddCart(productID, quantity) {
    $.ajax({
        type: "POST",
        url: "/Member/Home/AddCart",
        data: { "productID": productID, "quantity": quantity },
        success: function () {
           
        },
        error: function (e) { alert(e.responseText) },
    });
    $(".productCount").text(parseInt($(".productCount").text()) + 1);
    //$("#openModal").modal();
}
function functAddCartDe(productID, sender) {
    var quantity = $(sender).parent().siblings(".meto").children(".qty").children(".quantityVal").val();
    $.ajax({
        type: "POST",
        url: "/Member/Home/AddCart",
        data: { "productID": productID, "quantity": quantity },
        success: function () {

        },
        error: function (e) { alert(e.responseText) },
    });
    $(".productCount").text(parseInt($(".productCount").text()) + 1);
    //$("#openModal").modal();

}
function deleteCart(productID) {
    $.ajax({
        type: "POST",
        url: "/Member/Home/DeleteCart",
        data: { "productID": productID },
        success: function () { location.reload(); },
        error: function (e) { alert(e.responseText) },
    });
}
function updateCart(productID, quantity, sender, pm) {
    if (parseInt($(sender).parent().find(".cartDetailValue").val()) + parseInt(pm) != 0) {

         $.ajax({
        type: "POST",
             url: "/Member/Home/UpdateCart",
             data: { "productID": productID, "quantity": parseInt($(sender).parent().find(".cartDetailValue").val()) + parseInt(pm) },
        success: function () { location.href = '/Cart'; },
        error: function (e) { alert(e.responseText) },
    });
    }

   
}
//active visible find
function ajaxCartAdd(sender) {
    if (parseInt($(".kontrol").val()) == 0) {
        var total = 0;
        $.ajax({
            url: "/AJAX/CardDetail",
            type: "GET",
            success: function (gelenVeri) {
                $(".cartshow").empty();
                $.each(gelenVeri, function (index, item) {
                    $(".cartshow").append("<div class='clearfix sc_product'> <a href='#' class='product_thumb'><img src='" + item.FPath + "' alt='" + item.ProductName + "' width='60px' height='60px'></a><a href='#' class='product_name'>" + item.ProductName + "</a>  <p> Adet : " + item.Quantity + "</p><button onclick='deleteCart(" + item.ProductID +");' class='close'></button>");

                    $(".cartshow").append("</div>");
                    $(".cartshow").append("<hr />");
                    total += item.Quantity * item.Price;
                })
                $(".totaladd").empty();
                $(".totaladd").append("<span class='price'>Toplam: </span>" + total +" TL");
            },
                    error: function (hata) {
                        alert(hata.status)
                    }
            });

        $(sender).addClass("active");
        $(sender).siblings(".shopping_cart").addClass("active visible");
        $(".kontrol").val("1");
    }
    else {
        $(sender).removeClass("active");
        $(sender).siblings(".shopping_cart").removeClass("active visible");
        $(".kontrol").val("0");
    }
   
}
function functAddFavorite(sender,_productID) {
    var productID = parseInt(_productID);
    $.ajax({
        type: "POST",
        url: "/AJAX/addFavorite",
        data: { "productID": productID},
        success: function (gelenVeri) {
            if (parseInt(gelenVeri)== 0) {

                $(sender).removeClass("favcss");
                $(sender).children("span").text("Favorilerden Çıkar");
            }
            else {
                $(sender).addClass("favcss");
                $(sender).children("span").text("Favorilere Ekle");
            }
           
        },
        error: function (e) { alert(e.responseText) },
    });
}