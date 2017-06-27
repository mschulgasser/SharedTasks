$(function () {
    var hub = $.connection.myHub;
    $.connection.hub.start();
    hub.client.reload = function () {
        $("table tr:gt(0)").remove();
        $.get("/home/gettasks", function (data) {
            data.tasks.forEach(function (t) {
                var html = `<tr><td>` + t.title + `</td><td>`;
                if (t.userId === null || t.userId === 0) {
                    html += `<button class="btn btn-primary reserve" data-task-id="` + t.id + `">I'll do this one</button>`;
                }
                else if (t.userId == data.userId) {
                    html += `<button class="btn btn-success complete" data-task-id="` + t.id + `">I'm done</button>`;
                }
                else {
                    html += `<button disabled class="btn btn-danger">` + t.userFirstName + " " + t.userLastName + ` is doing this one</button>`
                }
                html += `</td></tr>`;
                $("table").append(html);
            });
        });
    };
    $("table").on('click', ".complete", function () {
        var taskId = $(this).data('task-id');
        hub.server.completedTask(taskId);
    });
    $("table").on('click', ".reserve", function () {
        var taskId = $(this).data('task-id');
        hub.server.reserveTask(taskId);
    });
});