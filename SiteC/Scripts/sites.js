var siteA = {
    name: "SiteA",
    url: "http://sitea.demo.local:9556/",
    enabled: true,
    title: "Site A - Hybrid  - MVC/API",
    colour: "#FAFAFA"
};
var siteB = {
    name: "siteB",
    url: "http://siteb.demo.local:9557/",
    enabled: true,
    title: "Site B - Hybrid  - MVC/API",
    colour: "#F5F6CE"
};
var siteC =
{
    name: "siteC",
    url: "http://sitec.demo.local:9558/",
    enabled: true,
    title: "Site C - Hybrid  - JavaScript",
    colour: "#A9F5BC"
};
var siteD =
{
    name: "siteD",
    url: "http://sited.demo.local:9559/",
    enabled: true,
    title: "Site D - Code Flow  - MVC/API",
    colour: "#F8E0F7"
};
var siteE = {
    name: "siteE",
    url: "http://sited.demo.local:9560/",
    enabled: true,
    title: "Site E - Implicit  - JavaScript",
    colour: "#A9E2F3"
};


var site = siteA;


// app.js should be rendered prior to below execution
function setSiteProperties() {
    document.title = site.name;
    $("#SiteNameLink").val(site.name);
    $("#SiteHeader").val(site.name);
}

$(document).ready(function() {
    console.log(site.name + " loaded");

    setSiteProperties();

    $(".jumbotron").css("background-color", site.colour);

    switch (site.name) {
    case(siteA.name || siteB.name || siteD.name):
    {
        $("#codegrantflow").hide();
        $("#implicitgrantflow").hide();
        break;
    }
        case siteD.name:
            {
                $("#codegrantflow").hide();
                $("#implicitgrantflow").hide();
                break;
            }
    default:
    {
        $("#hybridflow").hide();
    }
    }

});