/* */ 
var gulp = require('gulp');
var jshint = require('gulp-jshint');
var summary = require('../index');
gulp.task('jshint', function() {
  return gulp.src('app/*.js').pipe(jshint('.jshintrc')).pipe(jshint.reporter('jshint-summary', {
    statistics: true,
    fileCol: 'yellow,bold'
  }));
});
gulp.task('default', ['jshint']);
