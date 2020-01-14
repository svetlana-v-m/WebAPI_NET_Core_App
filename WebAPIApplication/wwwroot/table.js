var tokenKey = "accessToken";

// нажимаем на ссылку Удалить в таблице
$("body").on("click", ".removeLink", function (e) {
    e.preventDefault;
    var id = $(this).data("id");
    DeleteMessage(id);
});

//отменить авторизацию
$('#logOut').click(function (e) {
    e.preventDefault();
    $('.authorize').css('display', 'block');
    $('.loginDiv').css('display', 'none');
    $('.userInfo').css('display', 'none');
    $('.table').css('display', 'none');
    reset("loginForm");
    sessionStorage.removeItem(tokenKey);
});

// создание строки для таблицы
var row = function (message) {
    return "<tr data-rowid='" + message.id + "'><td>" + message.id + "</td>" +
        "<td>" + message.text + "</td> <td>" + message.creationDate + + "</td>" +
        "<td>" + message.hostName + "</td> <td>" + message.hostIP + "</td>" +
        "<td><a class='removeLink' data-id='" + message.id + "'>Удалить</a></td></tr>";
}

// Получение всех сообщений
function GetMessages() {
    $.ajax({
        url: '/api/messages',
        type: 'GET',
        dataType: 'json',
        beforeSend: function (xhr) {
            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (messages) {
            let rows = "";
            $.each(messages, function (index, message) {
                // добавляем полученные элементы в таблицу
                rows += row(message);
            });
            $("table tbody").empty().append(rows);
        }
    });
}

// Получение одного сообщения
function GetMessage(id) {

    $.ajax({
        url: '/api/messages/' + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowMessage(data);
        },
        error: function (x, y, z) {
            alert(x + '\n' + y + '\n' + z);
        }
    });
}

// Удаление сообщения
function DeleteMessage(id) {
    $.ajax({
        url: "api/messages/" + id,
        contentType: "application/json",
        method: "DELETE",
        beforeSend: function (xhr) {

            var token = sessionStorage.getItem(tokenKey);
            xhr.setRequestHeader("Authorization", "Bearer " + token);
        },
        success: function (message) {
            $("tr[data-rowid='" + message.id + "']").remove();
        }
    })
}