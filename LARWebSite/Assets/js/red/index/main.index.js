/*
 *  -- Pasillas Freelance Group  -- 
 *  Autor: daniel.pasillas
 *  Fecha: 14.Julio.2018
 *  Ultima modificación: 14.Julio.2018
 *  Usuario modificación: daniel.pasillas
 *  Versión: 1.0
 *  Descripción: Javascript File which includes the main functions from the La Red Caza y Pesca Web Site. 
 *  In additin, we define some procedures that will let us to run some tasks for the front end interface,
 *  sush as typehaead or autocompletion function, input search validation and so on.
 */


/* Input validation */
(function () {

    $("#red-form-search").submit(function (e) {

        if ($(".red-search-input").val()) {
            return true;
        }
        else {

            e.preventDefault();
            $(".red-search-input").focus();
        }
    });
})();
//-----------------------------------------

/* Autocompletion for searching */
(function () {
   
})();

