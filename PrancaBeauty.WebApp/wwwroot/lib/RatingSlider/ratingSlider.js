var step = 10;

function setPathData(path, value) {
    var firstStep = 6 / step * value;
    var secondStep = 2 / step * value;
    path.attr('d', 'M1,' + (7 - firstStep) + ' C6.33333333,' + (2 + secondStep) + ' 11.6666667,' + (1 + firstStep) + ' 17,' + (1 + firstStep) + ' C22.3333333,' + (1 + firstStep) + ' 27.6666667,' + (2 + secondStep) + ' 33,' + (7 - firstStep));
}

function LoadRatingSlider(_SliderId, _Step=10) {
    step = _Step;
    $('#' + _SliderId).each(function () {
        var self = $(this);
        var slider = self.slider({
            create: function () {
                self.find('.text strong').text(self.slider('value'));
                setPathData(self.find('.smiley').find('svg path'), self.slider('value'));
            },
            slide: function (event, ui) {
                self.find('.text strong').text(ui.value);
                setPathData(self.find('.smiley').find('svg path'), ui.value);
            },
            range: 'min',
            min: 1,
            max: step,
            value: 1,
            step: 1
        });

    });
}