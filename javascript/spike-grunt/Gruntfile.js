module.exports = function(grunt) {
    grunt.initConfig({
        connect: {
            server: {
                options: {
                    // https://github.com/gruntjs/grunt-contrib-connect#options
                    base: 'public',
                    keepalive: true,
                    open: true,
                    debug: true,
                    middleware: function (connect, options, middlewares) {
                        var proxyMiddleware = require('grunt-connect-proxy/lib/utils').proxyRequest;
                        return [proxyMiddleware].concat(middlewares);
                    }
                },
                proxies: [{
                    context: '/ip',
                    host: 'httpbin.org',
                    port: 80
                }]
            }
        }
    });

    // grunt connect
    grunt.loadNpmTasks('grunt-contrib-connect');

    // See https://github.com/drewzboto/grunt-connect-proxy/pull/140
    grunt.loadNpmTasks('grunt-connect-proxy');

    // grunt
    grunt.registerTask('default', ['configureProxies:server', 'connect:server']);
};
