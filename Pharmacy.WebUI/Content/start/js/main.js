$(document).ready(function () {
    $(".topnav > li:nth-child(" + $(".spanMenuIndex").text() + ")").addClass("current");
    $(".sidebarnav > li:nth-child(" + $(".spanSidebarIndex").text() + ")").addClass("current");
})
function funcnewMarka(sender) {
    
    if ($(sender).is(':checked')) {
        $(sender).parent().parent().siblings().children('.form_el').children('#BrandID').toggle();
        $(sender).parent().parent().siblings().children('.form_el').children('#BrandHideID').toggle();
    }
    else {
        $(sender).parent().parent().siblings().children('.form_el').children('#BrandID').toggle();
        $(sender).parent().parent().siblings().children('.form_el').children('#BrandHideID').toggle();
    }
}
function ajaxCityDistrict(tiklayan) {

    $.ajax({
        url: "/AJAX/getDistrict",
        type: "GET",
        data: { "plaka": $(tiklayan).val() },
        success: function (gelenVeri) {
            $(".District").empty();
            $(".District").append("<option value='" + 0 + "'>İlçe Seçiniz</option>");
            $.each(gelenVeri, function (index, item) {
                $(".District").append("<option value='" + item.ID + "'>" + item.name + "</option>");
            })
        },
        error: function (hata) {
            alert(hata.status)
        }
    });
}
function ajaxAddressChange(tiklayan) {

    $.ajax({
        url: "/AJAX/getAddress",
        type: "GET",
        data: { "ID": $(tiklayan).val() },
        success: function (gelenVeri) {
            $("#address").val(gelenVeri.name);
            $("#city").val(gelenVeri.city);
            $("#District").val(gelenVeri.district);
        },
        error: function (hata) {
            alert(hata.status)
        }
    });
}