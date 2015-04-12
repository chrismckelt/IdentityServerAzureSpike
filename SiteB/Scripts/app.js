var site = siteB;

function setSiteProperties() {
    document.title = site.name;
    $('#SiteNameLink').val(site.name);
    $('#SiteHeader').val(site.name);
}

$(document).ready(function () {
    console.log(site.name + " loaded");

    setSiteProperties();

    $('.jumbotron').css('background-color', 'mintcream');

});
