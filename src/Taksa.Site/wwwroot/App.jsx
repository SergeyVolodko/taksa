var styles = {
    map: {
        height: '1000px',
        width: '1000px'
    },

    countryProvince: {
        fillColor: 'lightgray',
        fillOpacity: 0.2,
        strokeColor: '#0057B8',
        strokeWeight: 1,
        strokeOpacity: 0.75
    },

    provinceOnHover: {
        fillColor: '#FFD700',
        strokeColor: '#0057B8',
        strokeWeight: 2
    },

    provinceSelected: {
        fillOpacity: 0,
        strokeOpacity: 0.5
    }
};

class App extends React.Component{

    constructor() {
        super();
        this.state = {
            selectedProvince: ""
        };
    }

    goToProvince(province) {
        this.setState({
            selectedProvince : province
        },
            () => this.mapRef.current.showProvince(this.state.selectedProvince)
        );
    }

    render() {
        return (
            <div>
                <Navigation onProvinceSelected={(p) => this.goToProvince(p)} />
                <CountryMap />
            </div>
        );
    }
}

ReactDOM.render(
    <App />,
    document.getElementById('content')
);