module.exports = function(config){
  config.set({

    basePath : '../',

    files : [
      'src/components/angular/angular.js',
      'src/components/angular-route/angular-route.js',
      'src/components/angular-mocks/angular-mocks.js',
      'src/app/common/**/*.js',
      'src/app/home/**/*.js',
      'test/unit/**/*.js'
    ],

    autoWatch : true,

    frameworks: ['jasmine'],

    browsers : ['Chrome'],

    plugins : [
            'karma-chrome-launcher',
            'karma-firefox-launcher',
            'karma-jasmine',
            'karma-junit-reporter'
            ],

    junitReporter : {
      outputFile: 'test_out/unit.xml',
      suite: 'unit'
    }

  });
};
