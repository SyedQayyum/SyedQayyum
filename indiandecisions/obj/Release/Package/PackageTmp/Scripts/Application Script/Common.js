

$(document).ready(function () {

    $('.datepicker').datepicker({
        format: 'mm/dd/yyyy',
    });


    function setCookie(name, value, expireAfterDays) {
        localStorage.setCacheItem(name, value, { days: expireAfterDays });
    }

    function getCookieValue(name) {
        localStorage.getCacheItem("usersFavoriteColor");
    }


    $('.active-toggle').bootstrapToggle({
            on: 'Active',
            off: 'In-Active'
        });
    
});

