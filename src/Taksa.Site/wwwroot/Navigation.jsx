class Navigation extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            selectedProvince: "empty",
            provinces: ukraineProvinces
        };
    }

    changeProvince(e) {
        this.setState({
                selectedProvince: e.target.value
            }, () => this.props.onProvinceSelected(this.state.selectedProvince)
        );
    }

    render() {
        return (
            <div>Navigation :: {this.state.selectedProvince}

                <select value={this.state.selectedProvince} onChange={(e) => this.changeProvince(e)}>
                    {this.state.provinces.map(p => <option value={p.id}> {p.name} </option>)}
                </select>

               
            </div>
        );
    }
}