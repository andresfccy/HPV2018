/* */ 
'use strict';
var through = require('through2');
var path = require('path');
var gutil = require('gulp-util');
var PluginError = gutil.PluginError;
var File = gutil.File;
var Concat = require('concat-with-sourcemaps');
module.exports = function(file, opt) {
  if (!file) {
    throw new PluginError('gulp-concat', 'Missing file option for gulp-concat');
  }
  opt = opt || {};
  if (typeof opt.newLine !== 'string') {
    opt.newLine = gutil.linefeed;
  }
  var isUsingSourceMaps = false;
  var latestFile;
  var latestMod;
  var fileName;
  var concat;
  if (typeof file === 'string') {
    fileName = file;
  } else if (typeof file.path === 'string') {
    fileName = path.basename(file.path);
  } else {
    throw new PluginError('gulp-concat', 'Missing path in file options for gulp-concat');
  }
  function bufferContents(file, enc, cb) {
    if (file.isNull()) {
      cb();
      return;
    }
    if (file.isStream()) {
      this.emit('error', new PluginError('gulp-concat', 'Streaming not supported'));
      cb();
      return;
    }
    if (file.sourceMap && isUsingSourceMaps === false) {
      isUsingSourceMaps = true;
    }
    if (!latestMod || file.stat && file.stat.mtime > latestMod) {
      latestFile = file;
      latestMod = file.stat && file.stat.mtime;
    }
    if (!concat) {
      concat = new Concat(isUsingSourceMaps, fileName, opt.newLine);
    }
    concat.add(file.relative, file.contents, file.sourceMap);
    cb();
  }
  function endStream(cb) {
    if (!latestFile || !concat) {
      cb();
      return;
    }
    var joinedFile;
    if (typeof file === 'string') {
      joinedFile = latestFile.clone({contents: false});
      joinedFile.path = path.join(latestFile.base, file);
    } else {
      joinedFile = new File(file);
    }
    joinedFile.contents = concat.content;
    if (concat.sourceMapping) {
      joinedFile.sourceMap = JSON.parse(concat.sourceMap);
    }
    this.push(joinedFile);
    cb();
  }
  return through.obj(bufferContents, endStream);
};
