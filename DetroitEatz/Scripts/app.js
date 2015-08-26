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
    var detroit = new google.maps.LatLng(42.331427, -83.0457538);

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
            /*
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
            */
        }
    }
}

function createMarker(place) {

    var marker = new google.maps.Marker({
        map: map,
        position: place.geometry.location
    });

    var request = { reference: place.reference };
    service.getDetails(request, function (details, status) {
        foodPlace = {
            PlaceID: details.id,
            Name: details.name,
            PriceLevel: details.price_level,
            WebSite: details.website,
            Rating: details.rating
        };

        ajaxHelper('/api/Restaurants/', 'POST', foodPlace);//.done(function (item) {
            //self.restaurants.push(item);
        //});

        google.maps.event.addListener(marker, 'click', function () {
            infowindow.setContent(details.name + "<br />" + details.formatted_address + "<br />" + details.website + "<br />" + details.rating + "<br />" + details.formatted_phone_number + "<br />" + details.price_level);
            infowindow.open(map, marker);
            });
        });
    
    
}

google.maps.event.addDomListener(window, 'load', initialize);



};
ko.applyBindings(new ViewModel());

