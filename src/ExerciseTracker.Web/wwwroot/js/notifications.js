$(function () {
    $("#reminder-form").on("submit", function (event) {
        event.preventDefault();

        const form = $(this);
        $.post(form.attr("action"), form.serialize(), function (response) {
            if (response.success) {
                alert(response.message);
                window.location.reload();
            }
        });
    });

    $(".mark-read").on("click", function () {
        const button = $(this);
        $.post("/Notification/MarkAsRead", {
            id: button.data("id"),
            __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').first().val()
        }, function (response) {
            if (response.success) {
                button.remove();
            }
        });
    });
});
