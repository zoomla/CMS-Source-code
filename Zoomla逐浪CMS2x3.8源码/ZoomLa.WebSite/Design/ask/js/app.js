// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
// 'starter.services' is found in services.js
// 'starter.controllers' is found in controllers.js
angular.module('starter', ['ionic', 'starter.controllers', 'starter.services'])

.run(function($ionicPlatform) {
  $ionicPlatform.ready(function() {
    // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
    // for form inputs)
    //if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
    //  cordova.plugins.Keyboard.hideKeyboardAccessoryBar(true);
    //  cordova.plugins.Keyboard.disableScroll(true);
    //}
    //if (window.StatusBar) {
    //  StatusBar.styleDefault();
    //}
	//if (window.cordova && window.cordova.InAppBrowser) {
    //    window.open = window.cordova.InAppBrowser.open;
    //  }
  });
})

.config(function($stateProvider, $urlRouterProvider,$ionicConfigProvider) {
	
	$ionicConfigProvider.platform.ios.tabs.style('standard'); 
	$ionicConfigProvider.platform.ios.tabs.position('bottom');
	$ionicConfigProvider.platform.android.tabs.style('standard');
	$ionicConfigProvider.platform.android.tabs.position('standard');
	$ionicConfigProvider.platform.ios.navBar.alignTitle('center'); 
	$ionicConfigProvider.platform.android.navBar.alignTitle('left');
	$ionicConfigProvider.platform.ios.backButton.previousTitleText('').icon('ion-ios-arrow-thin-left');
	$ionicConfigProvider.platform.android.backButton.previousTitleText('').icon('ion-android-arrow-back');        
	$ionicConfigProvider.platform.ios.views.transition('ios'); 
	$ionicConfigProvider.platform.android.views.transition('android');


  $stateProvider

  // setup an abstract state for the tabs directive
    .state('tab', {
        url: '/tab',
        cache: false,
        abstract: true,
        templateUrl: 'templates/tabs.html'
  })

  // Each tab has its own nav history stack:

.state('tab.index', {
url: '/index',
views: {
    'tab-view': {
	templateUrl: 'templates/ask_my.html',
	controller: 'AskMyCtrl'
  }
}
})
.state('tab.ask_add', {
  url: '/ask_add',
  views: {
	'tab-view': {
	    templateUrl: 'templates/ask_add.html',
	  controller: 'AskAddCtrl'
	}
  }
})
.state('tab.ask_edit', {
    url: '/ask_edit/:id',
    views: {
        'tab-view': {
            templateUrl: 'templates/ask_add.html',
            controller: 'AskEditCtrl'
        }
    }
})
.state('tab.ask_my', {
    url: '/ask_my',
    views: {
        'tab-view': {
            templateUrl: 'templates/ask_my.html',
            controller: 'AskMyCtrl'
        }
    }
})
.state('tab.ask_qlist', {
    url: '/ask_qlist/:askid',
    views: {
        'tab-view': {
            templateUrl: 'templates/ask_qlist.html',
            controller: 'AskQListCtrl'
        }
    }
})
.state('tab.ask_view', {
    url: '/ask_view/:askid',
    views: {
        'tab-view': {
            templateUrl: 'templates/ask_view.html',
            controller: 'AskViewCtrl'
        }
    }
})
.state('tab.question_add', {
    url: '/question_add/:askid',
  views: {
	'tab-view': {
	  templateUrl: 'templates/question_add.html',
	  controller: 'QuestionAddCtrl'
	}
  }
})
.state('tab.question_edit', {
    url: '/question_edit/:id',
    views: {
        'tab-view': {
            templateUrl: 'templates/question_add.html',
            controller: 'QuestionEditCtrl'
        }
    }
})
.state('tab.answer_my', {
    url: '/answer_my',
    views: {
        'tab-view': {
            templateUrl: 'templates/answer_my.html',
            controller: 'AnswerMyCtrl'
        }
    }
})
// if none of the above states are matched, use this as the fallback
$urlRouterProvider.otherwise('/tab/index');
});
