
var styles = {
    map: {
        height: '1000px',
        width: '1000px'
    }
};

var theMap;

var CountryMap = React.createClass({
    initMap : function(){
        theMap = new google.maps.Map(document.getElementById('map'), {
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
    },

    render: function () {
        return (
            <div id="map" style={styles.map}></div>
        );
    },

    componentDidMount: function () {
        this.initMap();
    }
});

ReactDOM.render(
    <CountryMap />,
    document.getElementById('content')
);
