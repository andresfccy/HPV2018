/* */ 
var PluginError = require('gulp-util').PluginError;
var stream = require('../stream');
var _ = require('lodash');
exports.failReporter = require('./fail');
exports.loadReporter = function(reporter) {
  if (typeof reporter === 'function')
    return reporter;
  if (typeof reporter === 'object' && typeof reporter.reporter === 'function')
    return reporter.reporter;
  if (typeof reporter === 'string') {
    try {
      return exports.loadReporter(require('jshint/src/reporters/' + reporter));
    } catch (err) {}
  }
  if (typeof reporter === 'string') {
    try {
      return exports.loadReporter(require(reporter));
    } catch (err) {}
  }
};
exports.reporter = function(reporter, reporterCfg) {
  reporterCfg = reporterCfg || {};
  if (reporter === 'fail') {
    return exports.failReporter(reporterCfg);
  }
  var rpt = exports.loadReporter(reporter || 'default');
  if (typeof rpt !== 'function') {
    throw new PluginError('gulp-jshint', 'Invalid reporter');
  }
  return stream(function(file, cb) {
    if (file.jshint && !file.jshint.success && !file.jshint.ignored) {
      var opt = _.defaults({}, reporterCfg, file.jshint.opt);
      rpt(file.jshint.results, file.jshint.data, opt);
    }
    cb(null, file);
  });
};
