var step = 5;

function setPathData(path, value) {
    var firstStep = 6 / step * value;
    var secondStep = 2 / step * value;
    path.attr('d', 'M1,' + (7 - firstStep) + ' C6.33333333,' + (2 + secondStep) + ' 11.6666667,' + (1 + firstStep) + ' 17,' + (1 + firstStep) + ' C22.3333333,' + (1 + firstStep) + ' 27.6666667,' + (2 + secondStep) + ' 33,' + (7 - firstStep));
}

function LoadRatingSlider(_SliderId, _SelectedRateId, _ElementToAddValue) {
    $('#' + _SliderId).each(function () {
        var self = $(this);
        var slider = self.slider({
            create: function () {
                $('#' + _ElementToAddValue).val(self.slider('value'));

                $('#' + _SelectedRateId).css('color', 'red');
                $('#' + _SelectedRateId).text(WithoutRate);

                setPathData(self.find('.smiley').find('svg path'), self.slider('value'));
            },
            slide: function (event, ui) {
                $('#' + _SelectedRateId).css('color', 'unset');
                switch (ui.value) {
                    case 0:
                        $('#' + _SelectedRateId).css('color', 'red');
                        $('#' + _SelectedRateId).text(WithoutRate);
                        break;
                    case 1:
                        $('#' + _SelectedRateId).text(VeryBad);
                        break;

                    case 2:
                        $('#' + _SelectedRateId).text(Bad);
                        break;

                    case 3:
                        $('#' + _SelectedRateId).text(Good);
                        break;

                    case 4:
                        $('#' + _SelectedRateId).text(VeryGood);
                        break;

                    case 5:
                        $('#' + _SelectedRateId).text(Excellent);
                        break;
                }
                $('#' + _ElementToAddValue).val(ui.value);
                setPathData(self.find('.smiley').find('svg path'), ui.value);
            },
            range: 'min',
            min: 0,
            max: step,
            value: 0,
            step: 1
        });

    });
}