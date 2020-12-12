$("a[data-id]").each(function (el) {
    $(this).click(function () {
        var value = $(this).attr("data-id");
        var antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
        var data = { followedId: value };
        $.ajax({
            type: "POST",
            url: "/api/follows",
            data: JSON.stringify(data),
            headers: {
                'X-CSRF-TOKEN': antiForgeryToken
            },
            success: function (data) {
                $('#' + value).html(data.followers);
            },
            contentType: 'application/json'
        });
    })
});