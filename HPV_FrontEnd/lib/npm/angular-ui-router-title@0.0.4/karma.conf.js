/* */ 
module.exports = function(karma) {
  var files = require('./files.conf');
  karma.set({
    basePath: '.',
    frameworks: ['jasmine'],
    files: [].concat(files.libs, files.src, files.test),
    logLevel: karma.LOG_DEBUG,
    browsers: ['Chrome']
  });
};
