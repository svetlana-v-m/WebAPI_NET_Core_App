$(document).ready(function () {
    var tokenKey = "accessToken";
    
    // отправка сообщения
    $("form").submit(function (e) {
        e.preventDefault();
        var id = this.elements["id"].value;
        var text = this.elements["messagetext"].value;
        //var creationDate = this.elements["creationdate"].value;
        //var hostName = this.elements["hostname"].value;
        //var hostIp = this.elements["hostip"].value;
        if (id == 0)
            CreateMessage(text);
    });

    // очистка текстбокса
    $("#reset").click(function (e) {
        e.preventDefault();
        reset("messageForm");
    });

    // показать/скрыть форму авторизации
    $("#loginDivLink").click(function (e) {
        e.preventDefault();
        showHide("loginDivId");
    });

    //кнопка авторизоваться
    $('#submitLogin').click(function (e) {
        e.preventDefault();
        var loginData = {
            grant_type: 'password',
            username: $('#Login').val(),
            password: $('#passwordLogin').val()
        };

        $.ajax({
            type: 'POST',
            url: '/token',
            data: loginData
        }).success(function (data) {
            $('.userName').text(data.username);
            $('.userInfo').css('display', 'block');
            $('.authorize').css('display', 'none');
            $('.table').css('display', 'block');
            
            // сохраняем в хранилище sessionStorage токен доступа
            sessionStorage.setItem(tokenKey, data.access_token);
            GetMessages();
            
            console.log(data.access_token);
        }).fail(function (data) {
            console.log(data);
        });
    });
    
});

//скрыть-показать элемент страницы
function showHide(element_id) {
    //Если элемент с id-шником element_id существует
    if (document.getElementById(element_id)) {
        //Записываем ссылку на элемент в переменную obj
        var obj = document.getElementById(element_id);
        //Если css-свойство display не block, то:
        if (obj.style.display != "block") {
            obj.style.display = "block"; //Показываем элемент
        }
        else obj.style.display = "none"; //Скрываем элемент
    }
    //Если элемент с id-шником element_id не найден, то выводим сообщение
    else alert("Элемент с id: " + element_id + " не найден!");
}

// Добавление сообщения
function CreateMessage(messageText) {
    $.ajax({
        url: "api/messages",
        contentType: "application/json",
        method: "POST",
        data: JSON.stringify({
            text: messageText
        }),
        success: function (message) {
            reset("messageForm");
            $("table tbody").append(row(message));
        }
    })
}

// сброс формы
function reset(formName) {
    var form = document.forms[formName];
    form.reset();
    form.elements["id"].value = 0;
}

