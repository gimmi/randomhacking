import React from 'react'
import ReactDOM from 'react-dom'

class Clock extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            date: new Date(),
            counter: 0
        };

        ['handleClick'].forEach(f => this[f] = this[f].bind(this));
    }

    componentDidMount() {
        this.timerId = setInterval(() => this.tick(), 1000)
    }

    componentWillUnmount() {
        clearInterval(this.timerId)
    }

    tick() {
        this.setState((state, props) => ({
            date: new Date(),
            counter: state.counter + 1
        }))
    }

    handleClick() {
        console.log(this.state.counter);
    };

    render() {
        return (
            <div>
                <h1>Hello, world!</h1>
                <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
                <h2>Counter is {this.state.counter}.</h2>
                <button onClick={this.handleClick}>Click Me</button>
            </div>
        );
    }
}


function tick() {
    const element = <Clock />;
    ReactDOM.render(
        element,
        document.getElementById('root')
    );
}

setInterval(tick, 1000);


