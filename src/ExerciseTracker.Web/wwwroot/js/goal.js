$(function () {
    $("#goal-form").on("submit", function (event) {
        event.preventDefault();

        const form = $(this);
        $.post(form.attr("action"), form.serialize(), function (response) {
            if (response.success) {
                alert(response.message);
                window.location.reload();
            }
        });
    });
});
