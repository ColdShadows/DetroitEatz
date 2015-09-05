var map;
var infowindow;
var service;
var i = 0;
var markers = [];

//start Map Initialization
function initMap(location) {
    console.log(location);

    var map =
        {
            //getting current locations using coordinates
            center: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
            zoom: 14,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
    // create map object and apply properties
    map = new google.maps.Map(document.getElementById('map'), map);

    //Create the auto-complete object and associate it with the UI input control.
    var input = /** @type {!HTMLInputElement} */(
        document.getElementById('pac-input'));

    var types = document.getElementById('type-selector');
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(types);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    //var infowindow = new google.maps.InfoWindow();

    //Add auto-complete listener
    autocomplete.addListener('place_changed', function () {
        infowindow.open();
        marker.setVisible(false);
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("Autocomplete's returned place contains no geometry");
            return;
        }
        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        }
        else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);  // Why 17? Because it looks good.
        }
        marker.setIcon(/** @type {google.maps.Icon} */(
            {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(35, 35)
            }));
        marker.setPosition(place.geometry.location);
        marker.setVisible(true);

        var address = '';
        if (place.address_components) {
            address =
                [
                  (place.address_components[0] && place.address_components[0].short_name || ''),
                  (place.address_components[1] && place.address_components[1].short_name || ''),
                  (place.address_components[2] && place.address_components[2].short_name || '')
                ].join(' ');
        }

        infowindow.setContent('<div><strong>' + place.name + '</strong><br>' + address);
        infowindow.open(map, marker);
    });

    // Sets a listener on a radio button to change the filter type on Places
    // Autocomplete.
    function setupClickListener(id, types) {
        var radioButton = document.getElementById(id);
        radioButton.addEventListener('click', function () {
            autocomplete.setTypes(types);
        });
    }

    setupClickListener('changetype-all', []);
    setupClickListener('changetype-address', ['address']);
    setupClickListener('changetype-restaurant', ['restaurant']);
    setupClickListener('changetype-geocode', ['geocode']);
    //autocomplete method ends   

    //draw circle on map
    var circleOptions =
        {
            strokeColor: "#FF0000",
            strokeOpacity: 0.8,
            strokeWeight: 1.5,
            fillColor: "#ADFF2F",
            fillOpacity: 0.35,
            map: map,
            center: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
            radius: 1000, //radius in meters
            //draggable: true,
            geodesic: true
            //editable: true
        };
    var circle = new google.maps.Circle(circleOptions);
    //end circle

    //trafficLayer
    var trafficLayer = new google.maps.TrafficLayer();
    $('#toggle_traffic').click(function () {
        if (trafficLayer.getMap()) {
            trafficLayer.setMap(null);
        }
        else {
            trafficLayer.setMap(map);
        }
    });
    //End trafficLayer

    //transitLayer
    var transitLayer = new google.maps.TransitLayer();
    $('#toggle_transit').click(function () {
        if (transitLayer.getMap()) {
            transitLayer.setMap(null);
        }
        else {
            transitLayer.setMap(map);
        }
    });
    //end transitLayer

    //bikeLayer
    var bikeLayer = new google.maps.BicyclingLayer();
    $('#toggle_bike').click(function () {
        if (bikeLayer.getMap()) {
            bikeLayer.setMap(null);
        }
        else {
            bikeLayer.setMap(map);
        }
    });
    //end bikeLayer

    //marker for the current location
    var marker = new google.maps.Marker(
        {
            position: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
            map: map
        });

    //Places services to locate the restaurants in the given radius
    var service = new google.maps.places.PlacesService(map);
    service.nearbySearch({
        location: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
        radius: '1000',
        types: ['restaurant']
    }, callback);

    service = new google.maps.places.PlacesService(map);

    //creating loop to get the markers for all the locations
    function callback(results, status) {
        //var i = 0;


        var interval = setInterval(function () {

            setMarker(results[i]);
            i++;
            if (i === results.length) clearInterval(interval);
        }, 200);
    }

    // A new Info Window is created and set content
    var infowindow = new google.maps.InfoWindow({
        content: content,

        // Assign a maximum value for the width of the infowindow allows
        // greater control over the various content elements
        maxWidth: 350
    });

    //function to create marker    
    function setMarker(results) {
        marker = new google.maps.Marker(
            {
                position: results.geometry.location,
                icon: '/Images/rest.png',
                map: map,
                animation: google.maps.Animation.DROP,
            });


        var interval = { reference: results.reference };
        service.getDetails(interval, function (details, status) {
            google.maps.event.addListener(marker, 'click', function (results) {
                infowindow.setContent(details.name + "<br />" + details.formatted_address + "<br />" + details.website + "<br />" + details.rating + "<br />" + details.formatted_phone_number + "<br />" + details.price_level);
                infowindow.open(map, this);
            });
        });
    }

    aps.event.addDomListener(window, 'load', initialize);
}

$(document).ready(function (location) {
    navigator.geolocation.getCurrentPosition(initMap);
});