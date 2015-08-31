var map;
var infowindow;
var service;

//start Map Initialization
function initMap(location) {
    console.log(location);

    var map =
        {
            //getting current locations using coordinates
            center: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
    map = new google.maps.Map(document.getElementById('map'), map);
    
    //Create the auto-complete object and associate it with the UI input control.
    var input = /** @type {!HTMLInputElement} */(
        document.getElementById('pac-input'));

    var types = document.getElementById('type-selector');
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(types);

    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo('bounds', map);

    var infowindow = new google.maps.InfoWindow();

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

    //marker for the current location
    var marker = new google.maps.Marker(
        {
            position: new google.maps.LatLng(location.coords.latitude, location.coords.longitude),
            map: map
        });
    
    


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
        if (status === google.maps.places.PlacesServiceStatus.OK) {
            for (var i = 0; i < results.length; i++) {
                createMarker(results[i]);
            }
        }
    }

    //function to create marker
    function createMarker(results) {
        var placeLoc = results.geometry.location;
        var marker = new google.maps.Marker({
            position: results.geometry.location,
            map: map,
            //icon: 'Images/rest.png',                       
        });

        //click listener for markers
        google.maps.event.addListener(marker, 'click', function () {
            infowindow.setContent(place.name);
            infowindow.open(map, this);
        });
    }

    service.nearbySearch(request, callback);
}

$(document).ready(function (location) {
    navigator.geolocation.getCurrentPosition(initMap);
});