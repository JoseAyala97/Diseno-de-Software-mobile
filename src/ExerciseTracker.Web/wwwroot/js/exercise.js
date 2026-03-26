$(function () {
    $("#exercise-form").on("submit", function (event) {
        event.preventDefault();

        const form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: "POST",
            data: form.serialize(),
            headers: { "X-Requested-With": "XMLHttpRequest" }
        }).done(function (response) {
            if (response.success) {
                alert(response.message);
                window.location.reload();
            }
        });
    });
});
