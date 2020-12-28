var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();
connection.on("NewMessage",
    function (message) {
        var chatInfo = `<div>[${message.user}]: <br /> ${escapeHtml(message.text)}  <hr style=" margin-top: 1rem; margin-bottom: 0rem;" /> </div>`;
        $("#messagesList").append(chatInfo);
    });
$("#sendButton").click(function () {
    var message = $("#messageInput").val();
    connection.invoke("Send", message);
    $("#messageInput").val("");
});
connection.start().catch(function (err) {
    return console.error(err.toString());
});
function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;");
}