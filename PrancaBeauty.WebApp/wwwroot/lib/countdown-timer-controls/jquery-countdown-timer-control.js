/*
A jQuery plugin of countdown timer with/without control button, based on the https://github.com/caike/jQuery-Simple-Timer and its original LICENSE https://raw.githubusercontent.com/caike/jQuery-Simple-Timer/master/LICENSE.md
*/

/*
MIT License

Copyright (c) 2019 lighthanabi

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

(function (factory) {
  // Using as a CommonJS module
  if(typeof module === "object" && typeof module.exports === "object") {
    // jQuery must be provided as argument when used
    // as a CommonJS module.
    //
    // For example:
    //   let $ = require("jquery");
    //   require("jquery-simple-timer")($);
    module.exports = function(jq) {
      factory(jq, window, document);
    }
  } else {
    // Using as script tag
    //
    // For example:
    //   <script src="jquery.simple.timer.js"></script>
    factory(jQuery, window, document);
  }
}(function($, window, document, undefined) {
  // Polyfill new JS features for older browser
  Number.isFinite = Number.isFinite || function(value) {
    return typeof value === 'number' && isFinite(value);
  }

  var timer;

  var Timer = function(targetElement){
    this._options = {};
    this.targetElement = targetElement;
    return this;
  };

  Timer.start = function(userOptions, targetElement){
    timer = new Timer(targetElement);
    mergeOptions(timer, userOptions);
    return timer.start(userOptions);
  };

  function mergeOptions(timer, opts) {
    opts = opts || {};

    // Element that will be created for hours, minutes, and seconds.
    timer._options.elementContainer = opts.elementContainer || 'div';

    var classNames = opts.classNames || {};

    timer._options.classNameSeconds       = classNames.seconds  || 'jst-seconds'
      , timer._options.classNameMinutes   = classNames.minutes  || 'jst-minutes'
      , timer._options.classNameHours     = classNames.hours    || 'jst-hours'
      , timer._options.classNameClearDiv  = classNames.clearDiv || 'jst-clearDiv'
      , timer._options.classNameTimeout   = classNames.timeout || 'jst-timeout';
  }

  Timer.prototype.start = function(options) {

    var that = this;

    var createSubElements = function(timerBoxElement){
      var seconds = document.createElement(that._options.elementContainer);
      seconds.className = that._options.classNameSeconds;

      var minutes = document.createElement(that._options.elementContainer);
      minutes.className = that._options.classNameMinutes;

      var hours = document.createElement(that._options.elementContainer);
      hours.className = that._options.classNameHours;

      var clearElement = document.createElement(that._options.elementContainer);
      clearElement.className = that._options.classNameClearDiv;

      return timerBoxElement.
        append(hours).
        append(minutes).
        append(seconds).
        append(clearElement);
    };

    this.targetElement.each(function(_index, timerBox) {
      var that = this;
      var timerBoxElement = $(timerBox);
      var cssClassSnapshot = timerBoxElement.attr('class');

      timerBoxElement.on('complete', function() {
        clearInterval(timerBoxElement.intervalId);
      });

      timerBoxElement.on('complete', function() {
        timerBoxElement.onComplete(timerBoxElement);
      });

      timerBoxElement.on('complete', function(){
        timerBoxElement.addClass(that._options.classNameTimeout);
      });

      timerBoxElement.on('complete', function(){
        if(options && options.loop === true) {
          timer.resetTimer(timerBoxElement, options, cssClassSnapshot);
        }
      });

      timerBoxElement.on('resetime', function() {
		  
		clearInterval(timerBoxElement.intervalId);
		console.log(timerBoxElement);
		timerBoxElement.removeClass(that._options.classNameTimeout);
		timerBoxElement.data('timeLeft', 0);
		timerBoxElement.paused = false;
		options.secondsLeft=0;
		var secondsLeft = options.secondsLeft || that.fetchSecondsLeft(timerBoxElement);
		var endTime = secondsLeft + that.currentTime();
		var timeLeft = endTime - that.currentTime();
		that.setFinalValue(that.formatTimeLeft(timeLeft), timerBoxElement);
      });
	  
      timerBoxElement.on('start', function() {
		var secondsLeft = options.secondsLeft || that.fetchSecondsLeft(timerBoxElement);
		var refreshRate = options.refreshRate || 1000;
		var endTime = secondsLeft + that.currentTime();
		var timeLeft = endTime - that.currentTime();
		that.setFinalValue(that.formatTimeLeft(timeLeft), timerBoxElement);
	
		intervalId = setInterval((function() {
			timeLeft = endTime - that.currentTime();
			// When timer has been idle and only resumed past timeout,
			// then we immediatelly complete the timer.
			if(timeLeft < 0 ){
				timeLeft = 0;
			}
			timerBoxElement.data('timeLeft', timeLeft);
			that.setFinalValue(that.formatTimeLeft(timeLeft), timerBoxElement);
		}.bind(this)), refreshRate);
	
		timerBoxElement.intervalId = intervalId;
      });
	  
	 timerBoxElement.on('pauseResume', function() {
	    if(timerBoxElement.paused){
			console.log('resume');
          timerBoxElement.trigger('resume');
        }else{
			console.log('pause');
          timerBoxElement.trigger('pause');
        }
	  });
	  
      timerBoxElement.on('pause', function() {
        clearInterval(timerBoxElement.intervalId);
        timerBoxElement.paused = true;
      });

      timerBoxElement.on('resume', function() {
        timerBoxElement.paused = false;
		options.secondsLeft=timerBoxElement.data('timeLeft');
		timerBoxElement.trigger('start');
      });

      createSubElements(timerBoxElement);
      return this.startCountdown(timerBoxElement, options);
    }.bind(this));
  };

  Timer.prototype.fetchSecondsLeft = function(element){
    var secondsLeft = element.data('seconds-left');
    var minutesLeft = element.data('minutes-left');

    if(Number.isFinite(secondsLeft)){
      return parseInt(secondsLeft, 10);
    } else if(Number.isFinite(minutesLeft)) {
      return parseFloat(minutesLeft) * 60;
    }else {
      throw 'Missing time data';
    }
  };

  Timer.prototype.startCountdown = function(element, options) {
    options = options || {};


    var intervalId = null;
    var defaultComplete = function(){
      clearInterval(intervalId);
      return this.clearTimer(element);
    }.bind(this);

    element.onReset = options.onReset || false;
	if(element.onReset != false){
		options.onReset.hide();
		element.onReset.on('click', function() {
			console.log('reset');
			element.trigger('resetime');
			if(element.onStart)
				element.onStart.show();
			if(element.onPause)
				element.onPause.hide();
			if(element.onReset)
				element.onReset.hide();
		});
	}
    element.onComplete = options.onComplete || defaultComplete;
    element.onPause = options.onPause || false;
    if(element.onPause != false){
		options.onPause.hide();
      element.onPause.on('click', function() {
        if(element.paused){
			console.log('resume');
          element.trigger('resume');
        }else{
			console.log('pause');
          element.trigger('pause');
        }
      });
    }
	
	element.onStart=options.onStart ||false;
	if(element.onStart != false){
	element.onStart.on('click', function(){
		console.log('start btn');
		element.trigger('start');
		if(element.onStart)
			element.onStart.hide();
		if(element.onPause)
			element.onPause.show();
		if(element.onReset)
			element.onReset.show();
	});
	};
	
	
  };


  /**
   * Resets timer and add css class 'loop' to indicate the timer is in a loop.
   * $timerBox {jQuery object} - The timer element
   * options {object} - The options for the timer
   * css - The original css of the element
   */
  Timer.prototype.resetTimer = function($timerBox, options, css) {
    var interval = 0;
    if(options.loopInterval) {
		interval = parseInt(options.loopInterval, 10) * 1000;
    }
    setTimeout(function() {
		$timerBox.trigger('reset');
		$timerBox.attr('class', css + ' loop');
		$timerBox.trigger('start');
		//timer.startCountdown($timerBox, options);
    }, interval);
  }

  Timer.prototype.clearTimer = function(element){
    element.find('.jst-seconds').text('00');
    element.find('.jst-minutes').text('00:');
    element.find('.jst-hours').text('00:');
  };

  Timer.prototype.currentTime = function() {
    return Math.round((new Date()).getTime() / 1000);
  };

  Timer.prototype.formatTimeLeft = function(timeLeft) {

    var lpad = function(n, width) {
      width = width || 2;
      n = n + '';

      var padded = null;

      if (n.length >= width) {
        padded = n;
      } else {
        padded = Array(width - n.length + 1).join(0) + n;
      }

      return padded;
    };

    var hours = Math.floor(timeLeft / 3600);
    timeLeft -= hours * 3600;

    var minutes = Math.floor(timeLeft / 60);
    timeLeft -= minutes * 60;

    var seconds = parseInt(timeLeft % 60, 10);

    if (+hours === 0 && +minutes === 0 && +seconds === 0) {
      return [];
    } else {
      return [lpad(hours), lpad(minutes), lpad(seconds)];
    }
  };

  Timer.prototype.setFinalValue = function(finalValues, element) {

    if(finalValues.length === 0){
      this.clearTimer(element);
      element.trigger('complete');
      return false;
    }

    element.find('.' + this._options.classNameSeconds).text(finalValues.pop());
    element.find('.' + this._options.classNameMinutes).text(finalValues.pop() + ':');
    element.find('.' + this._options.classNameHours).text(finalValues.pop() + ':');
  };


  $.fn.startTimer = function(options) {
    this.TimerObject = Timer;
    Timer.start(options, this);
    return this;
  };
  
  

}));
