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
        log.text = message.data;
        logs.push(log);
        return { logs };
    }
    
    render() {
        const categoriesStyle = {
            padding: '.5em',
            margin: 0,
            borderRight: '1px solid lightgray',
            display: 'flex',
            flexDirection: 'column'
        }
        const logsStyle = {
            flexGrow: 1,
            display: 'flex',
            flexDirection: 'column',
            padding: '.5em',
            justifyContent: 'flex-end',
            overflow: 'hidden',
            fontFamily: 'monospace',
        }

        const logDivs = this.state.logs.map(l => <LogEntryComponent key={l.key} log={l} />);
        return (
            <div style={{ flexGrow: 1, display: 'flex' }}>
                <ul style={categoriesStyle}>
                    <li><input type="checkbox" /> 123 container-one</li>
                    <li><input type="checkbox" /> 456 container-two</li>
                    <li><input type="checkbox" /> 456 container-three</li>
                </ul>
                <div id="output" style={logsStyle} onClick={this.handleClick}>
                    {logDivs}
                </div>
                <ConnectionOverlay connected={this.state.connected} />
            </div>
        );
    }
}
