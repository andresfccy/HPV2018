/* */ 
var deepCompare = require('./deep-compare');
var deepApply = module.exports = function(candidate, standards, cb) {
  for (var prop in candidate) {
    if (candidate.hasOwnProperty(prop)) {
      if (candidate[prop] instanceof Array) {
        for (var i = 0; i < candidate[prop].length; i += 1) {
          deepApply(candidate[prop][i], standards, cb);
        }
      } else if (typeof candidate[prop] === 'object') {
        deepApply(candidate[prop], standards, cb);
      }
    }
  }
  standards.forEach(function(standard) {
    if (deepCompare(candidate, standard)) {
      cb(candidate);
    }
  });
};
