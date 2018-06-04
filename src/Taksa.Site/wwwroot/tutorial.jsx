var CommentBox = React.createClass({
    render: function () {
        return (
            <div className="alert-success">
                Hello, world! I am a CommentBox.
            </div>
        );
    }
});
ReactDOM.render(
    <CommentBox />,
    document.getElementById('content')
);