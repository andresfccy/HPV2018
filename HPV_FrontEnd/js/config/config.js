System.config({
  baseURL: "prosperidad",
  defaultJSExtensions: true,
  transpiler: "babel",
  babelOptions: {
    "optional": [
      "runtime",
      "optimisation.modules.system"
    ]
  },
  paths: {
    "github:*": "../lib/github/*",
    "npm:*": "../lib/npm/*"
  },

  map: {
    "angular": "github:angular/bower-angular@1.5.0",
    "angular-animate": "github:angular/bower-angular-animate@1.5.0",
    "angular-bootstrap": "github:angular-ui/bootstrap-bower@1.1.2",
    "angular-cookies": "npm:angular-cookies@1.5.0",
    "angular-growl": "npm:angular-growl@0.1.0",
    "angular-growl-v2": "npm:angular-growl-v2@0.7.5",
    "angular-i18n": "npm:angular-i18n@1.5.3",
    "angular-local-storage": "npm:angular-local-storage@0.2.6",
    "angular-material": "github:angular/bower-material@1.0.5",
    "angular-md5": "npm:angular-md5@0.1.10",
    "angular-messages": "github:angular/bower-angular-messages@1.5.0",
    "angular-moment": "npm:angular-moment@1.0.0-beta.4",
    "angular-resource": "npm:angular-resource@1.5.0",
    "angular-route": "npm:angular-route@1.5.0",
    "angular-sanitize": "github:angular/bower-angular-sanitize@1.5.0",
    "angular-translate": "npm:angular-translate@2.9.1",
    "angular-translate-loader-partial": "github:angular-translate/bower-angular-translate-loader-partial@2.10.0",
    "angular-ui-bootstrap": "npm:angular-ui-bootstrap@1.2.2",
    "angular-ui-router": "github:angular-ui/ui-router@0.2.18",
    "angular-ui-router-title": "npm:angular-ui-router-title@0.0.4",
    "angular-ui-select": "github:angular-ui/ui-select@0.14.9",
    "babel": "npm:babel-core@5.8.35",
    "babel-runtime": "npm:babel-runtime@5.8.35",
    "bootstrap": "npm:bootstrap@3.3.6",
    "core-js": "npm:core-js@1.2.6",
    "font-awesome": "npm:font-awesome@4.5.0",
    "jquery": "npm:jquery@2.2.0",
    "moment": "npm:moment@2.11.2",
    "ng-file-upload": "npm:ng-file-upload@12.0.4",
    "ngstorage": "npm:ngstorage@0.3.10",
    "twbs/bootstrap": "github:twbs/bootstrap@3.3.6",
    "ui-select": "npm:ui-select@0.16.1",
    "github:angular-translate/bower-angular-translate-loader-partial@2.10.0": {
      "angular": "github:angular/bower-angular@1.5.0",
      "angular-translate": "github:angular-translate/bower-angular-translate@2.10.0"
    },
    "github:angular-translate/bower-angular-translate@2.10.0": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular-ui/ui-router@0.2.18": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular-ui/ui-select@0.14.9": {
      "angular": "github:angular/bower-angular@1.5.0",
      "css": "github:systemjs/plugin-css@0.1.20"
    },
    "github:angular/bower-angular-animate@1.5.0": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular/bower-angular-aria@1.5.0": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular/bower-angular-messages@1.5.0": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular/bower-angular-sanitize@1.5.0": {
      "angular": "github:angular/bower-angular@1.5.0"
    },
    "github:angular/bower-material@1.0.5": {
      "angular": "github:angular/bower-angular@1.5.0",
      "angular-animate": "github:angular/bower-angular-animate@1.5.0",
      "angular-aria": "github:angular/bower-angular-aria@1.5.0",
      "css": "github:systemjs/plugin-css@0.1.20"
    },
    "github:jspm/nodelibs-assert@0.1.0": {
      "assert": "npm:assert@1.3.0"
    },
    "github:jspm/nodelibs-path@0.1.0": {
      "path-browserify": "npm:path-browserify@0.0.0"
    },
    "github:jspm/nodelibs-process@0.1.2": {
      "process": "npm:process@0.11.3"
    },
    "github:jspm/nodelibs-util@0.1.0": {
      "util": "npm:util@0.10.3"
    },
    "github:twbs/bootstrap@3.3.6": {
      "jquery": "github:components/jquery@2.2.1"
    },
    "npm:angular-moment@1.0.0-beta.4": {
      "moment": "npm:moment@2.11.2"
    },
    "npm:angular-translate@2.9.1": {
      "angular": "npm:angular@1.5.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:angular-ui-router-title@0.0.4": {
      "systemjs-json": "github:systemjs/plugin-json@0.1.0"
    },
    "npm:assert@1.3.0": {
      "util": "npm:util@0.10.3"
    },
    "npm:babel-runtime@5.8.35": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:bootstrap@3.3.6": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:core-js@1.2.6": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "path": "github:jspm/nodelibs-path@0.1.0",
      "process": "github:jspm/nodelibs-process@0.1.2",
      "systemjs-json": "github:systemjs/plugin-json@0.1.0"
    },
    "npm:font-awesome@4.5.0": {
      "css": "github:systemjs/plugin-css@0.1.20"
    },
    "npm:inherits@2.0.1": {
      "util": "github:jspm/nodelibs-util@0.1.0"
    },
    "npm:moment@2.11.2": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:ng-file-upload@12.0.4": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:path-browserify@0.0.0": {
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:process@0.11.3": {
      "assert": "github:jspm/nodelibs-assert@0.1.0"
    },
    "npm:ui-select@0.16.1": {
      "fs": "github:jspm/nodelibs-fs@0.1.2",
      "process": "github:jspm/nodelibs-process@0.1.2"
    },
    "npm:util@0.10.3": {
      "inherits": "npm:inherits@2.0.1",
      "process": "github:jspm/nodelibs-process@0.1.2"
    }
  }
});
