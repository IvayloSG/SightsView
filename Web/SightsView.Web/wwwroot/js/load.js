﻿var pageNumber = 1;

$(document).ready(function () {
    loadAction()
});

function onClickLoad() {
    loadAction()
};

function loadAction() {
    let antiForgeryToken = $('#antiForgeryForm input[name=__RequestVerificationToken]').val();
    let elementId = Number(document.getElementById("elementId").defaultValue);
    let data = {
        elementId: elementId,
        pageNumber: pageNumber,
    };
    $.ajax({
        type: "POST",
        url: "/api/load",
        data: JSON.stringify(data),
        headers: {
            'X-CSRF-TOKEN': antiForgeryToken
        },
        success: function (data) {
            renderCreations(data);
        },
        error: function (data) {
            console.log(data);
        },
        contentType: 'application/json'
    });

    function renderCreations(data) {
        let container = document.getElementById("gallery");

        data.creations.forEach((creation) => {
            $("#gallery").append(`<div class='pics mb-3 animation all 1'><a href='Creations/Load/${String(creation.id)}'><img class='img-fluid' src='${String(creation.creationDataUrl)}' alt='Card image cap'></a></div>`)
        })

        pageNumber++;
    }
}