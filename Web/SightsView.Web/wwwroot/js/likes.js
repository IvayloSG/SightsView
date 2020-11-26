function goBack() {
    window.history.back();
}

function likeAction() {
    let antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    let creationId = document.getElementById("creationId").defaultValue;
    let data = { creationId: creationId };
    $.ajax({
        type: "POST",
        url: "/api/likes",
        data: JSON.stringify(data),
        headers: {
            'X-CSRF-TOKEN': antiForgeryToken
        },
        success: function (data) {
            $('#likes').html(data.likes);
        },
        contentType: 'application/json'
    });
}
