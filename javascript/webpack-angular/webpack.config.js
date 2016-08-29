var webpack = require('webpack');
var path = require('path');

module.exports = {
    entry: {
        vendor: ['angular', 'angular-route'],
        app: "./src/App.js"
    },
    devtool: 'source-map',
    output: {
        path: path.join(__dirname, 'dist'),
        filename: "[name].js"
    },
    plugins: [
        new webpack.optimize.CommonsChunkPlugin({
            name: ['app', 'vendor']
        })
    ]
};