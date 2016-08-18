module.exports = function (grunt) {
    grunt.initConfig({
        connect: {
            server: {
                options: {
                    // https://github.com/gruntjs/grunt-contrib-connect#options
                    base: 'public',
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
        },
        ts: {
            default: {
                src: 'public/**/*.ts',
                options: { // TODO copy from tsconfig.json
                    module: 'amd'
                }
            }
        },
        watch: {
            scripts: {
                files: 'public/**/*.ts',
                tasks: 'ts'
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-connect');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-connect-proxy'); // See https://github.com/drewzboto/grunt-connect-proxy/pull/140
    grunt.loadNpmTasks("grunt-ts");

    grunt.registerTask('default', ['ts', 'configureProxies:server', 'connect:server', 'watch:scripts']);
};
