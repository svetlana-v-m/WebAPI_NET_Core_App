# Web приложение с API

 Одностраничное приложение, представляющее собой текстовое поле для ввода сообщения и кнопку отправки сообщения на сервер. 
 Не авторизовавшись, сообщения, хранящиеся на сервере просматривать нельзя. Авторизация через JWT-токен.
 После авторизации на странице становится видна таблица с сообщениями, в которой можно увидеть само сообщение, дату и время его создания, IP-адрес отправителя и имя хоста, с которого было отправлено сообщение. 

 ## Back-end
 
 + ASP.NET Core
 + Entity Framework
 + SQL
 
 ## Front-end
 
 + Html
 + CSS
 + Bootstrap.  

# REST API 

## Get all messages on server
GET/api/messages
Get all messages from server.
### Example
curl --request GET \
  --url 'https://[appdomain]/api/messages'
  --header 'Accept: application/json'
  
### Responces
200
***
Success

Content Type      | Value
:----------------:|:----------------:
application/json  |Array<Message>
