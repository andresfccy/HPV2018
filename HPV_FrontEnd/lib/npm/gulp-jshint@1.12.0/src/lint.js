/* */ 
(function(process) {
  var path = require('path');
  var PluginError = require('gulp-util').PluginError;
  var RcLoader = require('rcloader');
  var jshintcli = require('jshint/src/cli');
  var minimatch = require('minimatch');
  var _ = require('lodash');
  module.exports = function createLintFunction(userOpts) {
    userOpts = userOpts || {};
    var jshint = require('jshint').JSHINT;
    if (userOpts.linter) {
      if (_.isString(userOpts.linter)) {
        jshint = require(userOpts.linter).JSHINT;
      } else {
        jshint = userOpts.linter;
      }
      if (!_.isFunction(jshint)) {
        throw new PluginError('gulp-jshint', 'Invalid linter "' + userOpts.linter + '". Must be a function or requirable package.');
      }
      delete userOpts.linter;
    }
    var rcLoader = new RcLoader('.jshintrc', userOpts, {loader: function(path) {
        var cfg = jshintcli.loadConfig(path);
        delete cfg.dirname;
        return cfg;
      }});
    var reportErrors = function(file, out, cfg) {
      var filePath = file.path ? path.relative(process.cwd(), file.path) : 'stdin';
      out.results = jshint.errors.map(function(err) {
        if (!err)
          return;
        return {
          file: filePath,
          error: err
        };
      }).filter(Boolean);
      out.opt = cfg;
      out.data = [jshint.data()];
      out.data[0].file = filePath;
    };
    return function lint(file, cb) {
      if (!file.isBuffer()) {
        return cb(null, file);
      }
      rcLoader.for(file.path, function(err, cfg) {
        if (err)
          return cb(err);
        var globals = {};
        if (cfg.globals) {
          globals = cfg.globals;
          delete cfg.globals;
        }
        if (cfg.overrides) {
          _.forEach(cfg.overrides, function(options, pattern) {
            if (!minimatch(file.path, pattern, {
              nocase: true,
              matchBase: true
            }))
              return;
            if (options.globals) {
              globals = _.assign(globals, options.globals);
              delete options.globals;
            }
            _.assign(cfg, options);
          });
          delete cfg.overrides;
        }
        var out = file.jshint || (file.jshint = {});
        var str = _.isString(out.extracted) ? out.extracted : file.contents.toString('utf8');
        out.success = jshint(str, cfg, globals);
        if (!out.success)
          reportErrors(file, out, cfg);
        return cb(null, file);
      });
    };
  };
})(require('process'));
