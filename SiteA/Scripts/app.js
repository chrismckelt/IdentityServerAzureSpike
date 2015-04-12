var site = siteA;

function setSiteProperties()
{
  document.title = site.name;
  $('#SiteNameLink').val(site.name);
  $('#SiteAHeader').val(site.name);
}

$(document).ready(function () {
    console.log(site.name + " loaded");

    setSiteProperties();

    $('.jumbotron').css('background-color', 'beige');

});
