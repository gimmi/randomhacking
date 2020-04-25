import React from 'react';
import PropTypes from 'prop-types';
import ConnectionOverlay from './ConnectionOverlay';

export class MainComponent extends React.Component {
    constructor(props) {
        super(props);
        this.clientId = Math.random().toString(16).substr(2);

        this.state = { 
            connected: false,
            logs: Array.from({ length: 100 }).map((_, key) => ({ key, text: '' }))
        };
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
            margin: 0,
            flexGrow: 1,
            display: 'flex',
            flexDirection: 'column',
            padding: '.5em',
            justifyContent: 'flex-end',
            overflow: 'hidden',
            fontFamily: 'monospace',
        }

        const logDivs = this.state.logs.map(l => <li key={l.key}>{l.text}</li>);
        return (
            <div style={{ flexGrow: 1, display: 'flex' }}>
                <ul style={categoriesStyle}>
                    <li><input type="checkbox" /> 123 container-one</li>
                    <li><input type="checkbox" /> 456 container-two</li>
                    <li><input type="checkbox" /> 456 container-three</li>
                </ul>
                <ul style={logsStyle}>
                    {logDivs}
                </ul>
                <ConnectionOverlay connected={this.state.connected} />
            </div>
        );
    }
}
