var webpack = require('webpack');
var path = require('path');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: {
        vendor: [
            'angular',
            'angular-route',
            'angular-aria',
            'angular-animate',
            'angular-material',
            'angular-material/angular-material.css'
        ],
        app: "./src/App.js"
    },
    devtool: 'source-map',
    output: {
        path: './dist',
        filename: "[name].js"
    },
    module: {
        loaders: [
            { test: /\.css$/, loader: "style-loader!css-loader" }
        ]
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: ['app', 'vendor']
        }),
        new HtmlWebpackPlugin({ 
            template: './src/index.html', 
            inject: 'head'
        })
    ]
};