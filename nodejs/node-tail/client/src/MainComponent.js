import React from 'react';

export class MainComponent extends React.Component {
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
        this.counter = 0;

        this.state = { 
            logs: Array.from({ length: 10 }).map((_, key) => ({ key, val: '' }))
        };
    }

    async handleClick() {
        this.counter += 1;
        await fetch('/api/publish', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ counter: this.counter })
        })
    }

    componentDidMount() {
        console.log('componentDidMount')

        this.ws = new WebSocket('ws://localhost:3000/ws');
        this.ws.onmessage = message => {
            console.log('ws.onmessage');

            this.setState(state => {
                const log = state.logs[0];
                const logs = [];

                state.logs.forEach((log, idx) => {
                    if (idx) {
                        logs.push(log)
                    }
                });
                log.val = `${message.type}: ${message.data}`;
                logs.push(log);
                
                return { logs }
            });

        };

    }

    componentWillUnmount() {
        console.log('componentWillUnmount')
        this.ws.close();
    }
    
    render() {
        const buttonStyle = { fontSize: 'larger' };
        const logDivs = this.state.logs.map(log => <div key={log.key}>{log.val}</div>);
        return (
            <div className="main">
                <div id="output" className="scrollable">
                    {logDivs}
                </div>
                <div className="footer">
                    <button onClick={this.handleClick} type="button" style={buttonStyle}>Send</button>
                </div>
            </div>
        );
    }
}
