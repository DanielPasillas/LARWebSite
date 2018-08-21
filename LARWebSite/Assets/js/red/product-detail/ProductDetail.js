/*
 *  Pasillas Freelance Group - Xiara Technologies
 *  Autor: daniel.pasillas
 *  Fecha:01.Agosto.2018
 *  Descripción: In this file we will include functions to load data asynchronisly, such as the related products.
 *  These methods will improve the page loading.
 *  
 */


//Fast Call Functions
(function () {

    //Launch dynamic gallery for products images.
    $(".red-load-dynamic").on('click', function () {
        
        var _imgArray = $(".red-load-lightviewgallery").attr("data-red-images");

        var _stringImages = '';

        $.each(JSON.parse(_imgArray), function (i, index) {
            if ((i + 1) === JSON.parse(_imgArray).length) {
                _stringImages += "{'src' : '" + window.location.origin + "/assets/images/product_b/" + index + "', 'thumb': '" + window.location.origin + "/assets/images/product_b/" + index + "'}";
            } else {
                _stringImages += "{'src' : '" + window.location.origin + "/assets/images/product_b/" + index + "', 'thumb': '" + window.location.origin + "/assets/images/product_b/" + index + "'},";
            }
        });

        console.log(_stringImages);

       
        $(this).lightGallery({
            dynamic: true,
            dynamicEl: "[" + _stringImages + "]"
        })
        
    });

})();


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

                //Check whether the view is responsive or not.
                var _isResponsiveRelatedProducts = $(".red-container-pane").attr("data-validate-responsive");

                //Call the AJAX function.
                loadRelatedProductsAjax(_idBrand, _idCategory, _idSubCategory, _isResponsiveRelatedProducts);

                //Reset data-ajax-async-loaded property to "false"
                _self.attr("data-ajax-async-loaded", true);
            }
        }, 1000);
    }

});
//--------------------------------------------------

function loadRelatedProductsAjax(idBrand, idCategory, idSubCategory, isResponsive) {
    try {
        $.ajax({
            url: window.location.origin + '/products/relateditems',
            type: 'post',
            cache: false,
            data: { brand: idBrand, category: idCategory, subcategory: idSubCategory, responsive: isResponsive },
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
//--------------------------------------------------

