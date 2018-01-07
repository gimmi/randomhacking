import React from 'react'
import { BrowserRouter, Route, Link } from 'react-router-dom'
import { Home } from './home'
import { About } from './about'

export function App() {
    return (
        <BrowserRouter>
            <div>
                <ul>
                    <li><Link to="/">Home</Link></li>
                    <li><Link to="/about">About</Link></li>
                </ul>
                <Route exact path="/" component={Home} />
                <Route exact path="/about" component={About} />
            </div>
        </BrowserRouter>
    )
}
