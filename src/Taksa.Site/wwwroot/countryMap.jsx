

class CountryMap extends React.Component{


    constructor(props) {
        super(props);

        this.state = {
            address: "no",
            highlight_layer: null,
            geocoder: new google.maps.Geocoder(),
            theMap: null
        };
    }

    render() {
        return (
            <div>
                <div id="map" style={styles.map}></div>
                <div>{this.state.address}</div>
            </div>
        );
    }


    centerMapToGeometry(self, geometry) {
        self.state.theMap.setCenter(geometry.location);
        self.state.theMap.fitBounds(geometry.viewport);
    }

    centerMapTo(address, mapRef) {
        this.state.geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                mapRef.setCenter(results[0].geometry.location);
                mapRef.fitBounds(results[0].geometry.viewport);
            }
        });
    }

    displayAddress(e, geocoder) {
        geocoder.geocode({
            'latLng': e.latLng
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[0]) {
                    alert(results[0].formatted_address);
                }
            }
        });
    }

    selectProvince(e, self) {
        self.stopListenMapOnMouseOver(self);
        self.state.theMap.data.overrideStyle(e.feature, styles.provinceSelected);

        self.state.geocoder.geocode({
            'latLng': e.latLng
        }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                if (results[2]) {
                    //var province = results[2].formatted_address;
                    self.centerMapToGeometry(self, results[2].geometry);
                }
            }
        });
    }

    startListenMapOnMouseOver(self) {
        google.maps.event.clearListeners(self.state.theMap.data, 'mouseover');
        self.state.theMap.data.addListener('mouseover', function (e) {
            self.state.theMap.data.revertStyle();
            self.state.theMap.data.overrideStyle(e.feature, styles.provinceOnHover);
        });
    }

    startListenMapOnMouseOut(self) {
        self.state.theMap.data.addListener('mouseout', function (e) {
            self.state.theMap.data.revertStyle();
            self.startListenMapOnMouseOver(self);
        });
    }

    stopListenMapOnMouseOver(self) {
        google.maps.event.clearListeners(self.state.theMap.data, 'mouseover');
    }

    initMap() {
        this.state.theMap = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

        this.centerMapTo("Ukraine", this.state.theMap);

        this.state.theMap.data.loadGeoJson('./Ukraine.json');

        this.state.theMap.data.setStyle(styles.countryProvince);

        var geocoder = this.state.geocoder;
        this.state.theMap.data.addListener('dblclick', (e) => this.displayAddress(e, geocoder));

        this.state.theMap.data.addListener('click', (e) => this.selectProvince(e, this));

        this.startListenMapOnMouseOver(this);
        this.startListenMapOnMouseOut(this);
    }

    componentDidMount() {
        this.initMap();
    }

    showProvince(province) {

        ////this.state.highlight_layer.setMap(null);
        //this.centerMapTo("Ukraine", this.state.theMap);

        //this.state.highlight_layer.setOptions({
        //    query: {
        //        select: 'geometry',
        //        from: '14gH6F4xShEBbJD7oAZfvLAtB_U-u_aJeByNfs8Id',
        //        where: "name IN ('{0}')".replace("{0}", province)
        //    }
        //});

        //setTimeout(() => { this.centerMapTo(province, this.state.theMap) }, 600);
    }
}