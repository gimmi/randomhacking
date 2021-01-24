var webpack = require('webpack');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: {
        app: './src/App',
        specs: './src/Specs'
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
            { test: /\.ts$/, loader: 'ts-loader' }
        ]
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: ['specs', 'app']
        }),
        new HtmlWebpackPlugin({
            filename: 'index.html',
            excludeChunks: ['specs']
        }),
        new HtmlWebpackPlugin({
            filename: 'specs.html'
        })
    ]
};