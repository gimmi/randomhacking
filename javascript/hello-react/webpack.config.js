const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    entry: {
        app: ['babel-polyfill', './src/app']
    },
    devtool: 'source-map',
    output: {
        path: path.resolve(__dirname, 'dist'),
        filename: '[name].js'
    },
    module: {
        rules: [{
            test: /\.js$/,
            include: path.resolve(__dirname, 'src'),
            use: [{
                loader: 'babel-loader',
                options: {
                    presets: [
                        ['env', {
                            "modules": false // See https://github.com/babel/babel-loader/issues/138
                        }],
                        ['react']
                    ]
                }
            }, {
                loader: 'eslint-loader'
            }]
        }]
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: './src/index.html', 
            filename: 'index.html',
            chunks: ['app']
        })
    ],
    devServer: {
        inline: false
    }
};
