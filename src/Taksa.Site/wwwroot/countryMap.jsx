
var styles = {
    map: {
        height: '500px',
        width: '500px'
    }
};

var theMap;

var CountryMap = React.createClass({
    initMap : function(){
        theMap = new google.maps.Map(document.getElementById('map'), {
            center: { lat: -34.397, lng: 150.644 },
            zoom: 8
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
