/* */ 
(function(process) {
  var fs = require('fs'),
      merge = require('merge'),
      editorconfig = require('editorconfig'),
      DEFAULTS = require('./constants/defaults'),
      MESSAGES = require('./constants/messages'),
      PATTERNS = require('./constants/ignorePatterns'),
      MAPPINGS = require('./constants/editorconfig-mappings'),
      ValidationError = require('./ValidationError'),
      eol = '\\r?\\n',
      eolRegExp = new RegExp(eol),
      tabsRegExp = /^\t*(?!\s).*$/,
      tabsRegExpForBOM = /^\t*(?! |\t).*$/,
      tabsLeadingRegExp = /^(\t*).*$/,
      spacesRegExp = /^ *(?!\s).*$/,
      spacesRegExpForBOM = /^ *(?!\t).*$/,
      spacesLeadingRegExp = /^( *).*$/;
  ;
  function Validator(options) {
    this._options = merge({}, DEFAULTS, options || {});
    this._processedFiles = 0;
    this._invalid = {};
  }
  Validator.DEFAULTS = DEFAULTS;
  Validator.MESSAGES = MESSAGES;
  Validator.PATTERNS = PATTERNS;
  Validator.prototype.validate = function(path) {
    var self = this,
        stat;
    try {
      stat = fs.statSync(path);
    } catch (e) {
      this._fail(MESSAGES.PATH_INVALID.message.replace('{a}', path));
    }
    if (stat.isFile()) {
      this._cleanUp();
      this._path = path;
      this._loadSettings();
      this._loadFile();
      this._loadIgnores();
      this._validateNewlineMaximum();
      this._validateNewlinesEOF();
      this._lines.forEach(function(line, index) {
        self._validateIndentation(line, index);
        self._validateTrailingspaces(line, index);
      });
      this._done();
    } else {
      this._fail(MESSAGES.PATH_ISNT_FILE.message.replace('{a}', path));
    }
  };
  Validator.prototype.getProcessedFiles = function() {
    return this._processedFiles;
  };
  Validator.prototype._done = function() {
    this._processedFiles++;
    this._cleanUp();
  };
  Validator.prototype._cleanUp = function() {
    this._settings = null;
    this._data = undefined;
    this._lines = null;
    this._ignoredLines = null;
  };
  Validator.prototype._loadFile = function() {
    this._data = fs.readFileSync(this._path, this._settings.encoding);
    this._lines = this._data.split(eolRegExp);
  };
  Validator.prototype._loadSettings = function() {
    var config,
        key;
    this._settings = merge({}, this._options);
    if (typeof this._settings.editorconfig === 'string') {
      var stat;
      try {
        stat = fs.statSync(this._settings.editorconfig);
      } catch (e) {
        this._fail(MESSAGES.EDITORCONFIG_NOTFOUND.message.replace('{a}', this._settings.editorconfig));
      }
      if (stat.isFile()) {
        config = editorconfig.parse(this._path, {config: this._settings.editorconfig});
        if (typeof config === 'object') {
          for (key in config) {
            if (typeof MAPPINGS[key] === 'string') {
              switch (key) {
                case 'indent_style':
                  this._settings[MAPPINGS[key]] = config[key] + 's';
                  break;
                default:
                  this._settings[MAPPINGS[key]] = config[key];
                  break;
              }
            }
          }
        }
      } else {
        this._fail(MESSAGES.PATH_ISNT_FILE.message.replace('{a}', this._settings.editorconfig));
      }
    }
  };
  Validator.prototype._loadIgnores = function() {
    var self = this,
        ignores = [];
    ;
    this._ignoredLines = {};
    if (Array.isArray(this._settings.ignores)) {
      this._settings.ignores.forEach(function(ignore) {
        if (typeof ignore === 'string' && typeof PATTERNS[ignore] === 'object') {
          ignores.push(PATTERNS[ignore]);
        } else if (typeof ignore === 'object' && typeof ignore.test === 'function') {
          ignores.push(ignore);
        }
      });
    }
    if (ignores.length === 0) {
      ignores = false;
    }
    if (Array.isArray(ignores)) {
      ignores.forEach(function(expression) {
        var matches = self._data.match(expression) || [];
        matches.forEach(function(match) {
          if (eolRegExp.test(match)) {
            self._data = self._data.replace(match, function(matched) {
              var index = 1,
                  args,
                  indexOfMatch,
                  indexOfSecondLine,
                  totalLines;
              ;
              args = Array.prototype.slice.call(arguments);
              args.pop();
              indexOfMatch = args.pop();
              indexOfSecondLine = self._data.slice(0, indexOfMatch).split(eolRegExp).length;
              totalLines = matched.split(eolRegExp).length;
              while (index < totalLines) {
                self._ignoredLines[indexOfSecondLine + index - 1] = true;
                index++;
              }
              return Array(totalLines).join('\n');
            });
          }
        });
      });
    }
  };
  Validator.prototype._validateNewlineMaximum = function() {
    if (typeof this._settings.newlineMaximum === 'number') {
      if (this._settings.newlineMaximum > 0) {
        var self = this,
            newlinesAtBeginn = '^(?:' + eol + '){' + (this._settings.newlineMaximum + 1) + ',}',
            newlinesInFile = '(?:' + eol + '){' + (this._settings.newlineMaximum + 2) + ',}',
            validate = function(match, offset, original) {
              var substring = original.substr(0, offset),
                  newlines = substring.match(new RegExp(eol, 'g')),
                  amount = match.match(new RegExp(eol, 'g')).length,
                  atLine = 0,
                  message,
                  data,
                  line,
                  payload,
                  fromLine,
                  toLine,
                  foundIgnore;
              ;
              if (newlines) {
                atLine = newlines.length + 1;
                amount = amount - 1;
              }
              fromLine = atLine;
              toLine = atLine + amount;
              foundIgnore = false;
              while (fromLine <= toLine) {
                if (self._ignoredLines[fromLine]) {
                  foundIgnore = true;
                  amount--;
                }
                fromLine++;
              }
              if (foundIgnore) {
                amount--;
              }
              if (amount > self._settings.newlineMaximum) {
                message = MESSAGES.NEWLINE_MAXIMUM.message.replace('{a}', amount).replace('{b}', self._settings.newlineMaximum);
                data = {message: message};
                data = merge({}, MESSAGES.NEWLINE_MAXIMUM, data);
                line = atLine + 1;
                payload = {
                  amount: amount,
                  maximum: self._settings.newlineMaximum
                };
                self._report(data, line, payload);
              }
              return original;
            };
        ;
        this._data.replace(new RegExp(newlinesAtBeginn, 'g'), validate);
        this._data.replace(new RegExp(newlinesInFile, 'g'), validate);
      } else {
        this._fail(MESSAGES.NEWLINE_MAXIMUM_INVALIDVALUE.message.replace('{a}', this._settings.newlineMaximum));
      }
    }
  };
  Validator.prototype._validateNewlinesEOF = function() {
    if (this._settings.newline && this._lines.length > 1) {
      var index = this._lines.length - 1;
      ;
      if (this._lines[index].length > 0) {
        this._report(MESSAGES.NEWLINE, index + 1);
      }
      if (this._lines[index - 1].length === 0) {
        this._report(MESSAGES.NEWLINE_AMOUNT, index + 1);
      }
    }
  };
  Validator.prototype._validateTrailingspaces = function(line, index) {
    if (this._settings.trailingspaces && typeof line === 'string') {
      var matchSpaces = line.match(/\s*$/);
      if (matchSpaces.length > 0 && matchSpaces[0].length > 0) {
        if (this._options.trailingspacesToIgnores && this._ignoredLines[index + 1]) {
          return;
        }
        if (this._options.trailingspacesSkipBlanks && line.trim() === '') {
          return;
        }
        this._report(MESSAGES.TRAILINGSPACES, index + 1);
      }
    }
  };
  Validator.prototype._validateIndentation = function(line, index) {
    if (!this._ignoredLines[index] && typeof this._settings.indentation === 'string' && typeof line === 'string') {
      var spacesExpected,
          indent,
          message,
          data,
          payload;
      ;
      switch (this._settings.indentation) {
        case 'tabs':
          var tabsRegExpFinal = (this._settings.allowsBOM ? tabsRegExpForBOM : tabsRegExp);
          if (!tabsRegExpFinal.test(line)) {
            return this._report(MESSAGES.INDENTATION_TABS, index + 1);
          }
          this._guessIndentation(line, index);
          break;
        case 'spaces':
          var spacesRegExpFinal = (this._settings.allowsBOM ? spacesRegExpForBOM : spacesRegExp);
          if (!spacesRegExpFinal.test(line)) {
            this._report(MESSAGES.INDENTATION_SPACES, index + 1);
          } else {
            if (typeof this._settings.spaces === 'number') {
              indent = line.match(spacesLeadingRegExp)[1].length;
              if (indent % this._settings.spaces !== 0) {
                spacesExpected = Math.round(indent / this._settings.spaces) * this._settings.spaces;
                message = MESSAGES.INDENTATION_SPACES_AMOUNT.message.replace('{a}', spacesExpected).replace('{b}', indent);
                data = {message: message};
                data = merge({}, MESSAGES.INDENTATION_SPACES_AMOUNT, data);
                payload = {
                  expected: spacesExpected,
                  indent: indent
                };
                this._report(data, index + 1, payload);
              }
            }
          }
          this._guessIndentation(line, index);
          break;
      }
    }
  };
  Validator.prototype._guessIndentation = function(line, index) {
    if (!this._ignoredLines[index] && this._settings.indentationGuess && this._settings.indentation) {
      var indentation = this._settings.indentation,
          indentationPrevious,
          regExp = indentation === 'tabs' ? tabsLeadingRegExp : spacesLeadingRegExp,
          match = line.match(regExp),
          matchPrevious = 0,
          diff,
          message,
          data;
      ;
      match = match.length > 1 ? match[1].length : 0;
      if (index > 0) {
        matchPrevious = this._lines[index - 1].match(regExp);
        matchPrevious = matchPrevious.length > 1 ? matchPrevious[1].length : 0;
      }
      indentation = match;
      indentationPrevious = matchPrevious;
      if (this._settings.indentation === 'spaces') {
        indentation = match / this._settings.spaces;
        indentationPrevious = matchPrevious / this._settings.spaces;
      }
      if (indentation % 1 !== 0 || indentationPrevious % 1 !== 0) {
        return;
      }
      if (indentation - indentationPrevious <= 1) {
        return;
      }
      diff = indentation - indentationPrevious;
      diff = diff / Math.abs(diff);
      message = MESSAGES.INDENTATION_GUESS.message.replace('{a}', indentationPrevious + 1).replace('{b}', indentation);
      data = {message: message};
      data = merge({}, MESSAGES.INDENTATION_GUESS, data);
      this._report(data, index + 1, {
        indentation: indentation,
        expected: indentationPrevious + 1
      });
    }
  };
  Validator.prototype._fail = function(message) {
    throw new Error(message);
  };
  Validator.prototype._report = function(data, linenumber, payload) {
    var line,
        file,
        validationError;
    ;
    data = merge({}, data);
    data.line = linenumber;
    if (!this._invalid[this._path]) {
      this._invalid[this._path] = {};
    }
    file = this._invalid[this._path];
    if (!file[linenumber]) {
      file[linenumber] = [];
    }
    line = file[linenumber];
    validationError = new ValidationError(data, payload);
    line.push(validationError);
  };
  Validator.prototype.getInvalidLines = function(path) {
    if (!this._invalid[path]) {
      return {};
    }
    return this._invalid[path];
  };
  Validator.prototype.getInvalidFiles = function() {
    return this._invalid;
  };
  module.exports = Validator;
})(require('process'));
