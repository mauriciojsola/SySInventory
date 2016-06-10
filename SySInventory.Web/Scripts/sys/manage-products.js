sys = {};
sys.manageProducts = (function (my, $) {

    my.escapeRegExp = function (string) {
        return string.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, "\\$1");
    }

    String.prototype.replaceAll = function (find, replace) {
        return this.replace(new RegExp(my.escapeRegExp(find), 'g'), replace);
    }

    String.prototype.capitaliseFirstLetter = function () {
        return this.charAt(0).toUpperCase() + this.slice(1);
    }

    my.clearErrors = function clearErrors() {
        // clear any errors
        $("#gridFilters div.has-error").removeClass("has-error");
        $("label.error").remove();
    };

    my.displayErrors = function (errors) {
        $.each(errors, function (index, value) {
            var fieldKey = value.Key.replaceAll('.', '_').capitaliseFirstLetter();
            $.each(value.Value, function (vIndex, vValue) {
                $("<label for='" + fieldKey + "' class='error control-label'></label>")
                        .html(vValue).appendTo($("#" + fieldKey).parent());
                $("#" + fieldKey).parent("div.form-group").addClass("has-error");
            });
        });
    };


    my.init = function (urls) {
        $(function () {

            $.ajaxSetup({
                cache: false
            });

            $('.grid-mvc').gridmvc();

            pageGrids.productsList.ajaxify({
                getData: urls.getDataUrl,
                getPagedData: urls.getPagedDataUrl
            });

            pageGrids.productsList.onGridError(function (result) {
                my.clearErrors();
                my.displayErrors($.parseJSON(result.data.responseText));
            });

            pageGrids.productsList.onGridLoaded(function (result) {
                my.clearErrors();
            });
        });
    };

    return my;
}(sys.manageProducts || {}, jQuery));