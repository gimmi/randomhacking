import React from 'react';
import PropTypes from 'prop-types';

function LogEntryComponent(props) {
    const style = { paddingTop: '.3em' };
    return <div style={style}>{props.log.text}</div>
}

LogEntryComponent.propTypes = {
    log: PropTypes.shape({
        text: PropTypes.string.isRequired
    })
}

function ConnectionOverlay(props) {
    const overlayStyle = {
        position: 'fixed',
        display: props.connected ? 'none' : 'block',
        width: '100%',
        height: '100%',
        top: 0,
        left: 0,
        right: 0,
        bottom: 0,
        backgroundColor: 'rgba(0, 0, 0, .8)',
        zIndex: 2,
        cursor: 'pointer'
    }
    const textStyle = {
        position: 'absolute',
        top: '50%',
        left: '50%',
        fontSize: '2em',
        color: 'white',
        transform: 'translate(-50%,-50%)'
    }
    return (
        <div style={overlayStyle}>
            <div style={textStyle}>Disconnected, hit refresh</div>
        </div>
    )
}

ConnectionOverlay.propTypes = {
    connected: PropTypes.bool.isRequired
}

export class MainComponent extends React.Component {
    constructor(props) {
        super(props);
        this.handleClick = this.handleClick.bind(this);
        this.clientId = Math.random().toString(16).substr(2);
        this.counter = 0;

        this.state = { 
            connected: false,
            logs: Array.from({ length: 100 }).map((_, key) => ({ key, text: '' }))
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
        this.ws = new WebSocket('ws://localhost:3000/ws?id=' + encodeURIComponent(this.clientId));
        this.ws.onopen = () => this.setState({ connected: true });
        this.ws.onclose = () => this.setState({ connected: false });
        this.ws.onerror = () => this.setState({ connected: false });
        this.ws.onmessage = message => {
            this.setState(state => {
                return this.appendLog(state.logs, message)
            })
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
                <ConnectionOverlay connected={this.state.connected} />
            </div>
        );
    }
}
