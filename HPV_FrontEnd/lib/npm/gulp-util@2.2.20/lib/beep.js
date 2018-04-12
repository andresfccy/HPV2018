/* */ 
(function(process) {
  module.exports = function() {
    process.stdout.write('\x07');
  };
})(require('process'));
