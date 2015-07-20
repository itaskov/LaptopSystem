//$(document).ready(function () {
//    / <reference path="jquery-1.6.2-vsdoc.js" / >
//    / <reference path="jquery-ui.js" / >
//    $(document).ready(function () {
//        $('*[data-autocomplete-url]')
//            .each(function () {
//                $(this).autocomplete({
//                    source: $(this).data("autocomplete-url")
//                });
//            });
//    });



//$('#byModel').autocomplete({
//    source:
//        '@Url.Action("Autocomplete")',
//    minLength: 1

//});
//});

$(document).ready(function () {
    $('.pager-search').click(function () {
        var model = $('#byModel').val();
        var manufacturer = $('#byManufacturer').val();
        var price = $('#byPrice').val();
        $(this).attr('href', function () {
            return this.href += '&byModel=' + encodeURIComponent(model)
                                + '&byManufacturer=' + encodeURIComponent(manufacturer)
                                + '&byPrice=' + encodeURIComponent(price);
        });
    });
});
