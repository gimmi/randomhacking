angular.module('app', []);

angular.module('app').controller('appMainCtrl', function ($scope) {
    $scope.model = {
        message: '',
        complexObj: null
    };

    $scope.run = function () {
        Backend.greet('World').then(function (ret) {
            $scope.$apply(function () {
                $scope.model.message = ret;
            });
        });
        
        Backend.roundtripComplexObj({ stringProp: 'str', intProp: 123 }).then(function (ret) {
            console.dir(ret);
        }, function (err) {
            console.dir(err);
        });
    };
});
