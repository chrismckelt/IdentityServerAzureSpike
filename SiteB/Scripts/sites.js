var identityServer = {
    name: "identityServer",
    url: "https://identity.demo.local/",
    coreUrl: "https://identity.demo.local/core/",
    title: "Identity Server"
};

var siteA = {
    name: "siteA",
    url: "http://sitea.demo.local:9556/",
    title: "Site A",
    description: "Hybrid MVC/API",
    colour: "#FAFAFA"
};
var siteB = {
    name: "siteB",
    url: "http://siteb.demo.local:9557/",
    title: "Site B",
    description: "Hybrid MVC/API",
    colour: "#F5F6CE"
};
var siteC =
{
    name: "siteC",
    url: "http://sitec.demo.local:9558/",
    title: "Site C",
    description: "Implicit JavaScript",
    colour: "#A9F5BC"
};
var siteD =
{
    name: "siteD",
    url: "http://sited.demo.local:9559/",
    title: "Site D",
    description: "Code Flow MVC/API",
    colour: "#F8E0F7"
};
var siteE = {
    name: "siteE",
    url: "http://sited.demo.local:9560/",
    title: "Site E",
    description: "Javascript form post implicit",
    colour: "#A9E2F3"
};

// app.js overwrites this value
var site = siteA;

$(document).ready(function() {
    console.log(site.name + " loaded");
    setSiteProperties();
    $("#SiteDescriptions").dump(site);

    $(".jumbotron").css("background-color", site.colour);

});

function setSiteProperties() {
    document.title = site.name;
    $("#SiteNameLink").val(site.name);
    $("#SiteHeader").append(site.name.replace('site', ' '));
    $("#SiteHeader").append(' - ');
    $("#SiteHeader").append(site.description);
}


// pretty print javascript objects
(function($) {
    $.fn.dump = function(variable) {
        return this.each(function() {
            if (typeof variable == 'object') {
                var string = $.dump.objectToString(variable, 0);
                $(this).html(string);
            } else {
                $(this).html('<pre>' + variable.toString() + '</pre>');
            }
        });
    }

    $.dump = {
        objectToString: function(variable, i) {
            var string = '';
            if (typeof variable == 'object' && i < 3) { // 3 is to prevent endless recursion, set higher for more depth
                string += '( <ul style="list-style:none;">';
                var key;
                for (key in variable) {
                    if (variable.hasOwnProperty(key)) {
                        string += '<li>' + key + ' => ';
                        string += $.dump.objectToString(variable[key], i + 1);
                        string += '</li>';
                    }
                }
                string += '</ul> )';
            } else {
                string = variable.toString();
            }
            return string;
        }
    }
})(jQuery);
