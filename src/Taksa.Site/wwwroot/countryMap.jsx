

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

    centerMapTo(address, mapRef) {
        this.state.geocoder.geocode({ 'address': address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                mapRef.setCenter(results[0].geometry.location);
                mapRef.fitBounds(results[0].geometry.viewport);
            }
        });
    }

    initMap() {
        this.state.theMap = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

        this.centerMapTo("Ukraine", this.state.theMap);

        this.setState({
            highlight_layer: new google.maps.FusionTablesLayer({
                query: {
                    select: 'geometry',
                    from: '1N2LBk4JHwWpOY4d9fobIn27lfnZ5MDy-NoqqRpk',
                    where: "ISO_2DIGIT IN ('UA')"
                },
                map: this.state.theMap,
                suppressInfoWindows: true
            })
        });

        //this.props.onMapInitialized(this.state.theMap);
    }

    componentDidMount() {
        this.initMap();
    }


    showProvince(province) {

        //this.state.highlight_layer.setMap(null);
        this.centerMapTo("Ukraine", this.state.theMap);

        this.state.highlight_layer.setOptions({
            query: {
                select: 'geometry',
                from: '14gH6F4xShEBbJD7oAZfvLAtB_U-u_aJeByNfs8Id',
                where: "name IN ('{0}')".replace("{0}", province)
            }
        });

        setTimeout(() => { this.centerMapTo(province, this.state.theMap) }, 600);
    }
}