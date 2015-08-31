/*
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
var types;
var radius;
var restaurantsUri = '/api/Restaurants/';


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
       
        alert(method);
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
        radius: 2000,
        //types: ['restaurant']
        //types: '@Encoder.JavaScriptEncode(ViewBag.foodChoice, false)'
        //types: '@ViewBag.foodChoice'
    };
    infowindow = new google.maps.InfoWindow();
    service = new google.maps.places.PlacesService(map);
    service.search(request, callback);
}

function callback(results, status) {
    if (status === google.maps.places.PlacesServiceStatus.OK) {
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
            infowindow.setContent(details.name + "<br />" + details.formatted_address + "<br />" + details.website + "<br />" + details.rating + "<br />" + details.formatted_phone_number + "<br />" + details.price_level + "<br />" + details.opening_hours.periods[1].open.time);
            infowindow.open(map, marker);

google.maps.event.addDomListener(window, 'load', initialize);
$(function () {
    //$('<tr>', { text: 'Hello World' }).appendTo($('table'));
    $('<td>', { text: 'Hello World' }).appendTo($('table'));
    $('<td>', { text: 'Hello World' }).appendTo($('table'));
    //$('<tr>', { text: 'Hello World' }).appendTo($('table'));
    $('<td>', { text: 'Hello World' }).appendTo($('table'));
    $('<td>', { text: 'Hello World' }).appendTo($('table'));
    $('<tr>', { text: 'Hello World' }).appendTo($('table'));
    $('<td>', { text: placeholder}).appendTo($('table')); 
});
$(document).ready(function () {
     //Send an AJAX request       
    $.getJSON(restaurantsUri)
        .done(function (data) {             
             //On success, 'data' contains a list of products.             
            $.each(data, function (key, item) {               
                 //Add a list item for the product.               
                $('<li>', { text: formatItem(item) }).appendTo($('#products'));
            });
        });
});

function formatItem(item) {
    return item.Name + ': ' + item.PriceLevel;
}
 
//};
    //ko.applyBindings(new ViewModel());
