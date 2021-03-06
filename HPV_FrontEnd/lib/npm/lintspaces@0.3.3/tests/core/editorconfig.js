/* */ 
var Validator = require('../../lib/Validator'),
    validator,
    options;
;
exports.tests = {
  'should override the settings by editorconfig': function(test) {
    options = {
      trailingspaces: false,
      newlineMaximum: false,
      indentation: 'spaces',
      spaces: 2,
      newline: false,
      ignores: ['js-comments'],
      editorconfig: '.editorconfig'
    };
    validator = new Validator(options);
    validator._path = __dirname + '/editorconfig.js';
    validator._loadSettings();
    test.ok(validator._settings.newline !== options.newline);
    test.equal(validator._settings.newline, true);
    test.ok(validator._settings.indentation !== options.indentation);
    test.equal(validator._settings.indentation, 'tabs');
    test.ok(validator._settings.spaces !== options.spaces);
    test.equal(validator._settings.spaces, 'tab');
    test.ok(validator._settings.trailingspaces !== options.trailingspaces);
    test.equal(validator._settings.trailingspaces, true);
    test.equal(validator._settings.newlineMaximum, options.newlineMaximum);
    test.done();
  },
  'should load specific settings by extension': function(test) {
    options = {editorconfig: '.editorconfig'};
    validator = new Validator(options);
    validator._path = __dirname + '/fixures/file.example';
    validator._loadSettings();
    test.equal(validator._settings.trailingspaces, false);
    test.equal(validator._settings.newline, false);
    validator = new Validator(options);
    validator._path = __dirname + '/fixures/file.other-example';
    validator._loadSettings();
    test.equal(validator._settings.trailingspaces, true);
    test.equal(validator._settings.newline, true);
    test.done();
  },
  'should throw an exception if editorconfig has no valid path': function(test) {
    test.throws(function() {
      validator = new Validator({editorconfig: '.'});
      validator.validate(__filename);
    }, Error);
    test.throws(function() {
      validator = new Validator({editorconfig: __dirname});
      validator.validate(__filename);
    }, Error);
    test.throws(function() {
      validator = new Validator({editorconfig: __dirname + '/path/that/doesnt/existis/.editorconfig'});
      validator.validate(__filename);
    }, Error);
    test.done();
  }
};
