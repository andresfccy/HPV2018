/* */ 
var esprima = require('esprima'),
    escodegen = require('escodegen'),
    run = require('../dynamic');
var fnDecl = /^[ ]*function[ ]?\(\)[ ]?\{\n/m,
    trailingBrace = /[ ]*\}(?![\s\S]*\})/m;
var stringifyFnBody = exports.stringifyFnBody = function(fn) {
  var out = fn.toString().replace(fnDecl, '').replace(trailingBrace, '');
  return normalize(out);
};
var normalize = exports.normalize = function normalize(fn) {
  var out = fn.toString();
  out = escodegen.generate(esprima.parse(out, {tolerant: true}), {format: {indent: {style: '  '}}});
  return out;
};
exports.annotate = function(code) {
  code = stringifyFnBody(code);
  return normalize(run(code));
};
