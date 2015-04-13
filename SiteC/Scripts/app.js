site = siteC;

$(document).ready(function() {
    console.log("implicit javascript client setup");

    var config = {
        client_id: "SiteC_ImplicitFlow",
        redirect_uri: window.location.protocol + "//" + window.location.host + "/?WelcomeBack=you",
        post_logout_redirect_uri: window.location.protocol + "//" + window.location.host + "/?LoggedOut=you",
        response_type: "id_token token",
        scope: "openid profile email read write",
        authority: "https://identity.demo.local/core"
    };
    var client = new OidcClient(config);
    window.client = client;

    var id_token;

    function show(data) {
        if (typeof data === 'object') {
            data = JSON.stringify(data, null, 2);
            show(data);
        }
        else {
            $("#result").text(data);
        }
    }

    if (window.location.hash) {
        client.processResponseAsync().then(function (result) {
            show(result);
            id_token = result.id_token;
        }, function (err) {
            show(err.message || err);
        });
    }

    $("#token").click(function () {
        $("#result").text('Working ' + new Date().toTimeString());
        client.createTokenRequestAsync().then(function (request) {
            window.location = request.url;
        });
    });

    $("#signout").click(function () {
        client.createLogoutRequestAsync(id_token).then(function (url) {
            window.location = url;
        });
    });
});
