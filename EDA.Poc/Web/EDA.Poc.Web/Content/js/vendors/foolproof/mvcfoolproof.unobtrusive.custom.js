(function ($) {

    jQuery.validator.addMethod("maiorquezeroif", function (value, element, params) {
        var dependentProperty = foolproof.getId(element, params["dependentproperty"]);
        var dependentValue = $("[Name=" + dependentProperty + "]").val();

        if (dependentValue != null && dependentValue.toString().replace(/^\s\s*/, '').replace(/\s\s*$/, '') != "") {
            if (value != null && !isNaN(parseInt(value.toString())) && parseInt(value) > 0)
                return true;
        }
        else
            return true;

        return false;
    });

    var setValidationValues = function (options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    };

    var $Unob = $.validator.unobtrusive;

    $Unob.adapters.add("maiorquezeroif", ["dependentproperty"], function (options) {
        setValidationValues(options, "maiorquezeroif", {
            dependentproperty: options.params.dependentproperty
        });
    });

}(jQuery));



