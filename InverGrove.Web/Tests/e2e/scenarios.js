'use strict';

/* https://github.com/angular/protractor/blob/master/docs/getting-started.md */

describe('my app', function () {

    browser.get('app/index.html');

    it('should automatically redirect to /home when location hash/fragment is empty', function () {
        expect(browser.getLocationAbsUrl()).toMatch("/home");
    });


    describe('home', function () {

        beforeEach(function () {
            browser.get('app/index.html#/home');
        });


        it('should render view1 when user navigates to /view1', function () {
            expect(element.all(by.css('[ng-view] p')).first().getText()).
                toMatch(/This is the partial for home/);
        });

    });


    describe('contact', function() {

    })
});
