/* */ 
var deepApply = require('./deep-apply');
var annotateInjectable = require('./annotate-injectable');
var signatures = require('../signatures/simple');
var annotateAST = module.exports = function(syntax) {
  deepApply(syntax, signatures, function(chunk) {
    var originalFn,
        newParam,
        type;
    try {
      type = chunk.callee.property.name;
    } catch (e) {}
    var argIndex = 1;
    if (type === 'config' || type === 'run') {
      argIndex = 0;
    }
    if (type === 'constant' || type === 'value') {
      return;
    }
    chunk.arguments[argIndex] = annotateInjectable(chunk.arguments[argIndex]);
  });
};
