
var foodPlace;
var id;
var map;
var service;
var infowindow;
var add;
//var restaurants = {
//    PlaceID: details.id,
//    Name: details.name,
//    PriceLevel: details.price_level,
//    WebSite: details.website,
//    Rating: details.rating
//};
var restaurantsUri = '/api/Restaurants/';

function ajaxHelper(uri, method, data) {
    //self.error(''); // Clear error message
    return $.ajax({
        type: method,
        url: uri,
        dataType: 'json',
        contentType: 'application/json',
        data: data ? JSON.stringify(data) : null
    }).fail(function (jqXHR, textStatus, errorThrown) {
        //self.error(errorThrown);
       
        alert(method);
    });
}

function initialize() {

    add = new google.maps.LatLng(42.331427, -83.0457538);

    map = new google.maps.Map(document.getElementById('map'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        center: add,
        zoom: 15
    });
   
    var request = {
        location: add,
        radius: '500',
        query: 'burgers'
    };
    infowindow = new google.maps.InfoWindow();
    service = new google.maps.places.PlacesService(map);
    service.textSearch(request, callback);
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
            infowindow.setContent(details.name + "<br />" + details.formatted_address + "<br />" + details.website + "<br />" + details.rating + "<br />" + details.formatted_phone_number + "<br />" + details.price_level + "<br />" + details.opening_hours.periods[1].open);
            infowindow.open(map, marker);
        });
    });         
}


function codeAddress() {
    var geocoder = new google.maps.Geocoder();
    add = document.getElementById('address').value;
    geocoder.geocode({ 'address': add }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            start = map.setCenter(results[0].geometry.location);
        }
    });

    var request = {
        location: add,
        radius: '500',
        query: 'burgers'
    };
    infowindow = new google.maps.InfoWindow();
    service = new google.maps.places.PlacesService(map);
    service.textSearch(request, callback);

}
google.maps.event.addDomListener(window, 'load', initialize);

