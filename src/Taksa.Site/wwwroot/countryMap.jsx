

class CountryMap extends React.Component{

    constructor(props) {
        super(props);

        this.state = {address: "no"};
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

        var world_geometry = new google.maps.FusionTablesLayer({
            query: {
                select: 'geometry',
                from: '1N2LBk4JHwWpOY4d9fobIn27lfnZ5MDy-NoqqRpk',
                where: "ISO_2DIGIT IN ('UA')"
            },
            map: theMap,
            suppressInfoWindows: true
        });

        this.props.onMapInitialized(theMap);
    }

    componentDidMount() {
        this.initMap();
    }

    showProvince(province) {
        //this.setState(() => {
        //    address: province
        //});

        alert(province);
    }
}