var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: {
        vendor: [
            'core-js/shim.js',
            'zone.js/dist/zone.js',
            'reflect-metadata',
            'rxjs',
            '@angular/core',
            '@angular/forms',
            '@angular/router',
            '@angular/platform-browser',
            '@angular/platform-browser-dynamic',
        ],
        app: './src/main.js'
    },
    devtool: 'source-map',
    output: {
        path: './dist',
        filename: '[name].js'
    },
    resolve: {
        extensions: ['', '.js', '.ts']
    },
    module: {
        loaders: [
            { test: /\.html$/, loader: 'raw' },
            { test: /\.ts$/, loader: 'ts-loader' }
        ]
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: ['app', 'vendor']
        }),
        new HtmlWebpackPlugin({
            template: './src/index.html',
            filename: 'index.html'
        })
    ]
};