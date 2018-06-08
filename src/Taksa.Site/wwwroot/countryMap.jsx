

class CountryMap extends React.Component{


    constructor(props) {
        super(props);

        this.state = {
            address: "no"
        };

        this.mapRef = React.createRef();
    }

    updateAddress(address) {
        this.setState({
            address: address
            }
        );
    }

    render() {
        return (
            <div>
                <GoogleMapUkraine ref={this.mapRef} onProvinceSelected={(a) => this.updateAddress(a)}/>
                <div>{this.state.address}</div>
            </div>
        );
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