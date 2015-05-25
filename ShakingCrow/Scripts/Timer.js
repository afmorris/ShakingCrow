// A simple templating method for replacing placeholders enclosed in curly braces.
if (!String.prototype.supplant) {
    String.prototype.supplant = function (o) {
        return this.replace(/{([^{}]*)}/g,
            function (a, b) {
                var r = o[b];
                return typeof r === 'string' || typeof r === 'number' ? r : a;
            }
        );
    };
}

function millisecondsToStr(duration) {
    var milliseconds = parseInt((duration % 1000) / 100)
            , seconds = parseInt((duration / 1000) % 60)
            , minutes = parseInt((duration / (1000 * 60)) % 60)
            , hours = parseInt((duration / (1000 * 60 * 60)) % 24);

    hours = (hours < 10) ? "0" + hours : hours;
    minutes = (minutes < 10) ? "0" + minutes : minutes;
    seconds = (seconds < 10) ? "0" + seconds : seconds;

    return hours + ":" + minutes + ":" + seconds + "." + milliseconds;
}

$(function () {

    var timer = $.connection.timerHub;

    function init() {
        timer.server.getTime().done(function (time) {
            $('#timer').text(millisecondsToStr(time));
        });
    }

    $.extend(timer.client, {
        updateTimer: function (time) {
            $('#timer').text(millisecondsToStr(time));
        },

        initialize: function () {
            $('#addBone').prop('disabled', false);
            $('#startTimer').prop('disabled', true);
            $('#stopTimer').prop('disabled', true);
            $('#saveBone').prop('disabled', true);
        },

        setupNewBone: function () {
            $('#addBone').prop('disabled', true);
            $('#startTimer').prop('disabled', false);
            $('#stopTimer').prop('disabled', false);
            $('#saveBone').prop('disabled', true);
        },

        timerStarted: function () {
            $('#startTimer').prop('disabled', true);
            $('#stopTimer').prop('disabled', false);
        },

        timerStopped: function () {
            $('#startTimer').prop('disabled', false);
            $('#stopTimer').prop('disabled', true);
            $('#saveBone').prop('disabled', false);
        }
    });

    // Start the connection
    $.connection.hub.start()
        .then(init)
        .then(function () {
            return timer.server.getTimerState();
        }).done(function (state) {
            if (state == 'Started') {
                timer.client.timerStarted();
            } else {
                timer.client.timerStopped();
                timer.client.initialize();
            }

            $('#addBone').on('click', function () {
                timer.client.setupNewBone();
            });

            $('#saveBone').on('click', function () {
                timer.server.saveBone();
            });

            $('#startTimer').click(function () {
                timer.server.startTimer();
            });

            $('#stopTimer').click(function () {
                timer.server.stopTimer();
            });
        });

});