import React from 'react';
import PropTypes from 'prop-types';
import ConnectionOverlay from './ConnectionOverlay';

export class MainComponent extends React.Component {
    constructor(props) {
        super(props);
        this.clientId = Math.random().toString(16).substr(2);

        this.state = { 
            connected: false,
            categories: {},
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
                return this.appendLog(state, JSON.parse(message.data))
            })
        }
    }

    componentWillUnmount() {
        this.ws.close();
    }

    appendLog(oldState, message) {
        const category = message.container_name || 'unknown';
        const text = message.log || 'unknown';

        const newState = {
            categories: oldState.categories,
            logs: []
        }
        if (newState.categories[category]) {
            newState.categories[category].count += 1
        } else {
            newState.categories[category] = { count: 1, selected: true }
        }
        const log = oldState.logs[0];
        oldState.logs.forEach((log, idx) => {
            if (idx) {
                newState.logs.push(log);
            }
        });
        log.text = text;
        newState.logs.push(log);
        return newState;
    }

    toggleCategory(category) {
        this.setState(state => {
            state.categories[category].selected = !state.categories[category].selected;
            return { categories: state.categories }
        })
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

        const catEls = Object.keys(this.state.categories).map(key => {
            const val = this.state.categories[key]
            return <li key={key}><input type="checkbox" checked={val.selected} onChange={() => this.toggleCategory(key)} /> {val.count} {key}</li>
        });

        const logEls = this.state.logs.map(l => <li key={l.key}>{l.text}</li>);
        return (
            <div style={{ flexGrow: 1, display: 'flex' }}>
                <ul style={categoriesStyle}>
                    {catEls}
                </ul>
                <ul style={logsStyle}>
                    {logEls}
                </ul>
                <ConnectionOverlay connected={this.state.connected} />
            </div>
        );
    }
}
