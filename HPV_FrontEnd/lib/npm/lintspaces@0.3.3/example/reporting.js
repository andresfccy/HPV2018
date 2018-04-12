/* */ 
var Validator = require('../lib/Validator'),
    validator = new Validator({
      newline: true,
      newlineMaximum: 1,
      trailingspaces: true,
      indentation: 'spaces',
      spaces: 4,
      ignores: ['js-comments']
    }),
    files;
;
validator.validate('./reporting.js.example');
files = validator.getInvalidFiles();
Object.keys(files).forEach(function(file) {
  var reports = files[file];
  Object.keys(reports).forEach(function(line) {
    var errors = reports[line];
    errors.forEach(function(error) {
      console.error('Error in Line ' + error.line + ' (' + error.code + ' / ' + error.type + ')' + ': ' + error.message);
      if (error.payload) {
        Object.keys(error.payload).forEach(function(payload) {
          console.error('\t' + payload + ': ' + error.payload[payload]);
        });
      }
    });
  });
});
