var styles = {
    map: {
        height: '1000px',
        width: '1000px'
    }
};

class App extends React.Component{

    constructor() {
        super();
        this.state = {
            theMap: null,
            selectedProvince: ""
        };
        this.mapRef = React.createRef();
    }

    updateMap(updatedMap) {
        this.setState(() => {
            theMap = updatedMap;
        });
    }

    goToProvince(province) {
        this.setState(() => {
            selectedProvince = province;
        });

        this.mapRef.current.showProvince(this.state.selectedProvince);
    }

    render() {
        return (
            <div>
                <Navigation onProvinceSelected={(p) => this.goToProvince(p)} />
                <CountryMap onMapInitialized={(m) => this.updateMap(m)}
                    ref={this.mapRef} />
            </div>
        );
    }
}

ReactDOM.render(
    <App />,
    document.getElementById('content')
);