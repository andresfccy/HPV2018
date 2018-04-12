/* */ 
var markPass = exports.mark = require('./passes/mark');
var refPass = exports.ref = require('./passes/ref');
var annotatorPass = exports.annotator = require('./passes/annotator');
var pdoAnnotatorPass = exports.pdo = require('./passes/pdo');
var ddoAnnotatorPass = exports.ddo = require('./passes/ddo');
var routeAnnotatorPass = exports.route = require('./passes/route');
module.exports = function(astral) {
  [markPass, annotatorPass, refPass, pdoAnnotatorPass, ddoAnnotatorPass, routeAnnotatorPass].forEach(function(pass) {
    astral.register(pass);
  });
};
