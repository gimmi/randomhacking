import React from 'react';

function LogEntryComponent(props) {
    const style = { paddingTop: '.3em' };
    return <div style={style}>{props.log.text}</div>
}

export class MainComponent extends React.Component {
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
        this.counter = 0;

        this.state = { 
            logs: Array.from({ length: 10 }).map((_, key) => ({ key, text: '' }))
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
        this.ws = new WebSocket('ws://localhost:3000/ws');
        this.ws.onmessage = message => {
            this.setState(state => this.appendLog(state.logs, message));
        }
    }

    componentWillUnmount() {
        this.ws.close();
    }

    appendLog(oldLogs, message) {
        const log = oldLogs[0];
        const logs = [];
        oldLogs.forEach((log, idx) => {
            if (idx) {
                logs.push(log);
            }
        });
        log.text = `${message.type}: ${message.data}`;
        logs.push(log);
        return { logs };
    }
    
    render() {
        const buttonStyle = { fontSize: 'larger' };
        const logDivs = this.state.logs.map(l => <LogEntryComponent key={l.key} log={l} />);
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
