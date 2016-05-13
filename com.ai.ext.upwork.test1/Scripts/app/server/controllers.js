// Define the module for our AngularJS application.
var app = angular.module("app", []);

// I control the main demo.

app.controller("ClicksTrackerController", function ($scope, clickService) {
    // I contain the list of friends to be rendered.
    $scope.clicks = [];

    var ClickTracker = {
        CampaignName: "",
        Clicks: "",
        Conversions: "",
        Impressions: "",
        AffiliateName: ""
    };

    // I contain the ngModel values for form interaction.
    //var Product = $scope.form = {
    $scope.form = {
        CampaignName: "",
        Clicks: "",
        Conversions: "",
        Impressions: "",
        AffiliateName: ""
    };

    loadRemoteData();

    // ---
    // PUBLIC METHODS.
    // ---

    // I process the add-friend form.
    $scope.addClick = function () {
        var _clicksTracker = {
            CampaignName: $scope.form.CampaignName,
            Clicks: $scope.form.Clicks,
            Conversions: $scope.form.Conversions,
            Impressions: $scope.form.Impressions,
            AffiliateName: $scope.form.AffiliateName
        };

        // If the data we provide is invalid, the promise will be rejected,
        // at which point we can tell the user that something went wrong. In
        // this case, I'm just logging to the console to keep things very
        // simple for the demo.
        clickService.addClick(_clicksTracker)
            .then(
                loadRemoteData,
                function (errorMessage) {
                    console.warn(errorMessage);
                }
            )
        ;
        // Reset the form once values have been consumed.

        $scope.form.CampaignName = "";
        $scope.form.Clicks = "";
        $scope.form.Conversions = "";
        $scope.form.Impressions = "";
        $scope.form.AffiliateName = "";
    };

    // I remove the given friend from the current collection.
    $scope.removeClick = function (clicksTracker) {
        // Rather than doing anything clever on the client-side, I'm just
        // going to reload the remote data.
        clickService.removeClick(clicksTracker)
            .then(loadRemoteData)
        ;
    };

    // ---
    // PRIVATE METHODS.
    // ---

    // I apply the remote data to the local scope.
    function applyRemoteData(newClicks) {
        $scope.clicks = newClicks;
    }

    // I load the remote data from the server.
    function loadRemoteData() {
        // The clickService returns a promise.
        clickService.getClicks()
            .then(function (clicks) {
                applyRemoteData(clicks);
            }
            )
        ;
    }
}
);

// -------------------------------------------------- //
// -------------------------------------------------- //

// I act a repository for the remote product collection.
app.service("clickService", function ($http, $q) {
    // Return public API.
    return ({
        addClick: addClick,
        getClicks: getClicks,
        removeClick: removeClick
    });

    // ---
    // PUBLIC METHODS.
    // ---

    // I add a friend with the given name to the remote collection.
    function addClick(ClicksTracker) {
        var request = $http({
            method: "post",
            url: "api/ClicksTracker/",
            /*params: {action: "add"},*/
            data: ClicksTracker
        });
        return (request.then(handleSuccess, handleError));
    }

    // I get all of the friends in the remote collection.
    function getClicks() {

        var request = $http({
            method: "get",
            url: "api/ClicksTracker/"
            /*
            params: {
                action: "get"
            }
            */
        });

        return (request.then(handleSuccess, handleError));
    }

    // I remove the friend with the given ID from the remote collection.
    function removeClick(clicksTracker) {

        var request = $http({
            method: "delete",
            url: "api/ClicksTracker/",
            params: {
                Id: clicksTracker.Id
            },
            /*
            data: {
                id: product.Id
            }
            */
        });
        return (request.then(handleSuccess, handleError));
    }

    // ---
    // PRIVATE METHODS.
    // ---

    // I transform the error response, unwrapping the application dta from
    // the API response payload.
    function handleError(response) {

        // The API response from the server should be returned in a
        // nomralized format. However, if the request was not handled by the
        // server (or what not handles properly - ex. server error), then we
        // may have to normalize it on our end, as best we can.
        if (!angular.isObject(response.data) || !response.data.message) {
            return ($q.reject("An unknown error occurred."));
        }

        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));

    }
    // I transform the successful response, unwrapping the application data
    // from the API response payload.
    function handleSuccess(response) {
        return (response.data);
    }

}
);