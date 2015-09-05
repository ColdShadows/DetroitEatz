var foodPlace;
var id;
var map;
var service;
var infowindow;
var restaurantsUri = '/api/Restaurants/';
var food = {
    Name: 'Ben',
    PriceLevel: '3',
    WebSite: 'http://google.com',
    Rating: '2'
};

//Start geolocation

if (navigator.geolocation) {

    function error(err) {
        console.warn('ERROR(' + err.code + '): ' + err.message);
    }

    function success(pos) {
        userCords = pos.coords;

        //return userCords;
    }

    // Get the user's current position
    navigator.geolocation.getCurrentPosition(success, error);
    //console.log(pos.latitude + " " + pos.longitude);
}
else {
    alert('Geolocation is not supported in your browser');
}
//End Geo location

//Adding info window option
infowindow = new google.maps.InfoWindow({
    content: "holding..."
});



/*
$(document).ready(function () {
   // alert('Ouch');
    ajaxHelper(restaurantsUri, 'POST', food);
    ajaxHelper(restaurantsUri, 'GET');
    alert('hyt');
    });
*/
function ajaxHelper(uri, method, data) {
    //self.error(''); // Clear error message
    return $.ajax({
        type: method,
        url: uri, dataType: 'json',
        contentType: 'application/json',
        data: data ? JSON.stringify(data) : null
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //self.error(errorThrown);
        alert('Error');
    });
}

function initialize() {
    var detroit = new google.maps.LatLng(location.coords.latitude, location.coords.longitude);

    map = new google.maps.Map(document.getElementById('map'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        center: detroit,
        zoom: 15
    });

    var request = {
        location: detroit,
        radius: 500,
        types: ['restaurant']
    };
    infowindow = new google.maps.InfoWindow();
    service = new google.maps.places.PlacesService(map);
    service.search(request, callback);
}

function callback(results, status) {
    if (status == google.maps.places.PlacesServiceStatus.OK) {
        for (var i = 0; i < results.length; i++) {
            createMarker(results[i]);
            //id = i + 1;
            foodPlace = {              
                PlaceID: results[i].id,
                Name: results[i].name,
                PriceLevel: results[i].price_level,
                WebSite: results[i].website,
                Rating: results[i].rating
                
                
            }
            ajaxHelper('/api/Restaurants/', 'POST', foodPlace).done(function (item) {
                self.restaurants.push(item);
            });
        }
    }
}

//implementing search by text function
function reallyDoSearch() {
    var type = document.getElementById('type').value;
    var keyword = document.getElementById('keyword').value;
    var rankBy = document.getElementById('rankBy').value;

    var search = {};

    if (keyword) {
        search.keyword = keyword;
    }

    if (type != 'establishment') {
        search.types = [type];
    }

    if (rankBy == 'distance' && (search.types || search.keyword)) {
        search.rankBy = google.maps.places.RankBy.DISTANCE;
        search.location = map.getCenter();
        centerMarker = new google.maps.Marker({
            position: search.location,
            animation: google.maps.Animation.DROP,
            map: map
        });
    } else {
        search.bounds = map.getBounds();
    }

    places.search(search, function (results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                var icon = 'icons/number_' + (i + 1) + '.png';
                markers.push(new google.maps.Marker({
                    position: results[i].geometry.location,
                    animation: google.maps.Animation.DROP,
                    icon: icon
                }));
                google.maps.event.addListener(markers[i], 'click', getDetails(results[i], i));
                window.setTimeout(dropMarker(i), i * 100);
                addResult(results[i], i);
            }
        }

    });
}
        function createMarker(place) {
            var placeLoc = place.geometry.location;
            var marker = new google.maps.Marker({
                map: map,
                position: place.geometry.location
            });

            var request = { reference: place.reference };
            service.getDetails(request, function (details, status) {
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.setContent(details.name + "<br />" + details.formatted_address + "<br />" + details.website + "<br />" + details.rating + "<br />" + details.formatted_phone_number + "<br />" + details.price_level);
                    infowindow.open(map, marker);
                });
            });
        }

        google.maps.event.addDomListener(window, 'load', initialize);


        var ViewModel = function () {

            var self = this;
            self.users = ko.observableArray();
            self.restaurants = ko.observableArray();//database fetch of restaurants
            self.favoriteDetail = ko.observable();
            self.error = ko.observable();
            /*
                    self.getRestaurants = function (formElement) {
                        ajaxHelper(restaurantsUri, 'GET').done(function (data) {
                            self.restaurants(data);
                        });
                    }
                    */
        }
        ko.applyBindings(new ViewModel());

        function test() {
            alert('Yep!');
        }