$(function () {
    $("#stats-form").on("submit", function (event) {
        event.preventDefault();

        $.get("/Stats/GetSummary", {
            startDate: $("#startDate").val(),
            endDate: $("#endDate").val()
        }).done(function (response) {
            $('[data-stat="sessions"]').text(response.totalSessions);
            $('[data-stat="minutes"]').text(response.totalMinutes);
            $('[data-stat="average"]').text(response.averageMinutesPerSession.toFixed(1));
            $('[data-stat="frequent"]').text(response.mostFrequentExercise);
        });
    });
});
