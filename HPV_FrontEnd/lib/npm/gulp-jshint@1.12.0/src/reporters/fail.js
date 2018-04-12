/* */ 
(function(process) {
  var path = require('path');
  var stream = require('../stream');
  var PluginError = require('gulp-util').PluginError;
  var _ = require('lodash');
  module.exports = function(opts) {
    opts = opts || {};
    var fails = false;
    var buffer = opts.buffer !== false ? [] : false;
    var messages = {
      error: 0,
      warning: 0,
      info: 0
    };
    return stream(function through(file) {
      if (file.jshint && file.jshint.results) {
        messages = _.reduce(file.jshint.results, function(result, err) {
          return {
            error: result.error + Number(err.error.code[0] === 'E'),
            warning: result.warning + Number(err.error.code[0] === 'W'),
            info: result.info + Number(err.error.code[0] === 'I')
          };
        }, messages);
      }
      if (file.jshint && !file.jshint.success && !file.jshint.ignored) {
        (fails = fails || []).push(path.relative(process.cwd(), file.path));
      }
      (buffer || this).push(file);
    }, function flush() {
      var failOnWarning = !opts.ignoreWarning && messages.warning;
      var failOnInfo = !opts.ignoreInfo && messages.info;
      if (fails && (messages.error || failOnWarning || failOnInfo)) {
        this.emit('error', new PluginError('gulp-jshint', {
          message: 'JSHint failed for: ' + fails.join(', '),
          showStack: false
        }));
      }
      if (buffer) {
        buffer.forEach(function(file) {
          this.push(file);
        }, this);
      }
    });
  };
})(require('process'));
