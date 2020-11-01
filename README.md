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
 
## API description 
[TOC]
### Get all messages on server

GET/api/Messages

Get all messages from server.
#### Request example
curl --request GET \

     --url 'https://[appdomain]/api/Messages'

#### Responce example
[{"id":1,"text":"TestMessage1","creationDate":"01.11.2020 11:26:12","hostName":"myHost","hostIP":"192.168.1.10"},
{"id":2,"text":"TestMessage2","creationDate":"01.11.2020 11:35:20","hostName":"myHost","hostIP":"192.168.1.10"}]

### Get message by Id
GET/api/Messages/{id}

GET message from server by its Id.

#### Parameters
##### {id}
Message Id in data base.

Type - integer.

#### Request example
curl --request GET \

     --url 'https://[appdomain]/api/Messages/1
     
#### Responce example
If message is found - status 200 (Ok).

[{"id":1,"text":"TestMessage1","creationDate":"01.11.2020 11:26:12","hostName":"myHost","hostIP":"192.168.1.10"}]

If message is not found - status 404 (Not found).

### Create new message
POST/api/Messages?={someMessage}

Send new message to data base.

#### Parameters
##### {someMessage}
Message text. Type - string.

#### Request example
curl --request POST \

     --url 'https://[appdomain]/api/Messages?=MessageText

#### Responce example
If message created successfully - status 200(Ok):
{
    "id": 3,
    "text": "MessageText",
    "creationDate": "01.11.2020 14:50:15",
    "hostName": "myHost",
    "hostIP": "192.168.1.10"
}

### Delete message
DELETE/api/Messages/{id}

Delete message by its id.

#### Request example
curl --request DELETE \

     --url 'https://[appdomain]/api/Messages/3

#### Responce example
If message deleted successfully status - 200 (OK).

If message is not found - status 404 (Not found).
