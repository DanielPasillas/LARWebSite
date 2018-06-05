/*
 *  Asis Incorporated S.A. de C.V.
 */

(function () {

    


})();

//Return a XHR AJAX Object.
//Validate the compatibility
function getXHR() {
    if (window.XMLHttpRequest) {
        // Chrome, Firefox, IE7+, Opera, Safari
        return new XMLHttpRequest();
    }
    // IE6
    try {
        // The latest stable version. It has the best security, performance, 
        // reliability, and W3C conformance. Ships with Vista, and available 
        // with other OS's via downloads and updates. 
        return new ActiveXObject('MSXML2.XMLHTTP.6.0');
    } catch (e) {
        try {
            // The fallback.
            return new ActiveXObject('MSXML2.XMLHTTP.3.0');
        } catch (e) {
            alert('This browser is not AJAX enabled.');
            return null;
        }
    }
}
//-----------------------------------