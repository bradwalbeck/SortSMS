// app.js
angular.module('sortApp', [])

.controller('mainController', function ($scope, $http) {
    $scope.searchBody = '';
    $scope.searchType = '';
    $scope.searchContact_name = '';

    $http.get('smsDataFull.json')
       .then(function (res) {
           $scope.smses = res.data;
       });

    // DATA MODEL
    //{
    //    "protocol": "0",
    //    "address": "4994145111",
    //    "date": "1434217951744",
    //    "type": "1",
    //    "subject": "null",
    //    "body": "Hey there, it's Michael :p",
    //    "toa": "null",
    //    "sc_toa": "null",
    //    "service_center": "null",
    //    "read": "1",
    //    "status": "-1",
    //    "locked": "0",
    //    "date_sent": "1434217948000",
    //    "readable_date": "Jun 13, 2015 11:52:31 AM",
    //    "contact_name": "Michael"
    //}

    // MINIMUM DATA MODEL
    //{
    //    "date": "1434217951744",
    //    "type": "1",
    //    "body": "Hey there, it's Michael :p",
    //    "readable_date": "13 Jun 2015 11:52:31 AM",
    //    "contact_name": "Michael"
    //}

});