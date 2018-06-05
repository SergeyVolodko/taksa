

class CountryMap extends React.Component{

    constructor(props) {
        super(props);

        this.state = {
            address: "no",
            highlight_layer : null
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

    initMap() {
        var theMap = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
        });

        var geocoder = new google.maps.Geocoder();
        geocoder.geocode({ 'address': "Ukraine" }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                theMap.setCenter(results[0].geometry.location);
                theMap.fitBounds(results[0].geometry.viewport);
            }
        });

        this.setState({
            highlight_layer: new google.maps.FusionTablesLayer({
                query: {
                    select: 'geometry',
                    from: '1N2LBk4JHwWpOY4d9fobIn27lfnZ5MDy-NoqqRpk',
                    where: "ISO_2DIGIT IN ('UA')"
                },
                map: theMap,
                suppressInfoWindows: true
            })
            }
        );

        this.props.onMapInitialized(theMap);
    }

    componentDidMount() {
        this.initMap();
    }

    showProvince(province) {

        //this.state.highlight_layer.setMap(null);


        this.state.highlight_layer.setOptions({
            query: {
                select: 'geometry',
                from: '14gH6F4xShEBbJD7oAZfvLAtB_U-u_aJeByNfs8Id',
                where: "name IN ('{0}')".replace("{0}", province)
            }
        });
    }
}