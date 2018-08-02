﻿/*
 *  Pasillas Freelance Group
 *  Autor: daniel.pasillas
 *  Fecha:01.Agosto.2018
 *  Descripción: In this file we will include functions to load data asynchronisly, such as the related products.
 *  These methods will improve the page loading.
 *  
 */

//Function to load the related product section.
//This function will be executed when the #related-products div is shown.
$(window).scroll(function () {

    var _self = $("#related-products");

    var top_of_element = _self.offset().top;
    var bottom_of_element = _self.offset().top + _self.outerHeight();
    var bottom_of_screen = $(window).scrollTop() + window.innerHeight;
    var top_of_screen = $(window).scrollTop();

    if ((bottom_of_screen > top_of_element) && (top_of_screen < bottom_of_element)) {
        setTimeout(function () {
            if (_self.attr("data-ajax-async-loaded") === "false") {

                var _idBrand = _self.attr("data-brand");
                var _idCategory = _self.attr("data-category");
                var _idSubCategory = _self.attr("data-subcategory");

                //Call the AJAX function.
                loadRelatedProductsAjax(_idBrand, _idCategory, _idSubCategory);

                //Reset data-ajax-async-loaded property to "false"
                _self.attr("data-ajax-async-loaded", true);
            }
        }, 1000);
    }

});
//--------------------------------------------------

function loadRelatedProductsAjax(idBrand, idCategory, idSubCategory) {
    try {
        $.ajax({
            url: window.location.origin + '/products/relateditems',
            type: 'get',
            cache: false,
            data: { brand : idBrand, category : idCategory, subcategory : idSubCategory },
            dataType: 'html',
            success: function (response) {
                $("#red-content-related-products").html(response);
            },
            error: function (xhr, j, p) {
                console.log(xhr.responseText);
            }
        });
    } catch (e) {
        console.log(e);
    }
}
