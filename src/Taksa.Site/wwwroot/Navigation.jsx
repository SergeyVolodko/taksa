class Navigation extends React.Component {

    constructor(props) {
        super(props);
        this.state = { selectedProvince: "empty" };
    }

    changeProvince() {
        this.setState({
                selectedProvince : "Odesa oblast"
            }, () => this.props.onProvinceSelected(this.state.selectedProvince)
        );
    }

    render() {
        return (
            <div onClick={() => this.changeProvince()}>Navigation :: {this.state.selectedProvince}</div>
        );
    }
}