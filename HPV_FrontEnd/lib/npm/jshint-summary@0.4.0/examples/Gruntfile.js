/* */ 
module.exports = function(grunt) {
  grunt.loadNpmTasks('grunt-contrib-jshint');
  grunt.initConfig({jshint: {
      options: {
        reporter: require('../index'),
        jshintrc: '.jshintrc',
        statistics: true,
        fileCol: 'yellow,bold'
      },
      target: ['app/*.js']
    }});
  grunt.registerTask('default', ['jshint']);
};
