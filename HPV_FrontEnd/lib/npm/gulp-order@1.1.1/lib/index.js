/* */ 
(function() {
  var Minimatch,
      path,
      through;
  through = require('through');
  Minimatch = require('minimatch').Minimatch;
  path = require('path');
  module.exports = function(patterns, options) {
    var files,
        matchers,
        onEnd,
        onFile,
        rank,
        relative;
    if (patterns == null) {
      patterns = [];
    }
    if (options == null) {
      options = {};
    }
    files = [];
    matchers = patterns.map(function(pattern) {
      if (pattern.indexOf("./") === 0) {
        throw new Error("Don't start patterns with `./` - they will never match. Just leave out `./`");
      }
      return Minimatch(pattern);
    });
    onFile = function(file) {
      return files.push(file);
    };
    relative = function(file) {
      if (options.base != null) {
        return path.relative(options.base, file.path);
      } else {
        return file.relative;
      }
    };
    rank = function(s) {
      var index,
          matcher,
          _i,
          _len;
      for (index = _i = 0, _len = matchers.length; _i < _len; index = ++_i) {
        matcher = matchers[index];
        if (matcher.match(s)) {
          return index;
        }
      }
      return matchers.length;
    };
    onEnd = function() {
      files.sort(function(a, b) {
        var aIndex,
            bIndex;
        aIndex = rank(relative(a));
        bIndex = rank(relative(b));
        if (aIndex === bIndex) {
          return String(relative(a)).localeCompare(relative(b));
        } else {
          return aIndex - bIndex;
        }
      });
      files.forEach((function(_this) {
        return function(file) {
          return _this.emit("data", file);
        };
      })(this));
      return this.emit("end");
    };
    return through(onFile, onEnd);
  };
}).call(this);
