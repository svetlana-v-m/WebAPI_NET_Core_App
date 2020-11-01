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

### Get all messages on server

Get all messages from server.

#### Request example
##### CURL
curl --location --request GET 'https://localhost:44350/api/Messages' \
##### HTTP
GET /api/Messages HTTP/1.1

Host: localhost:44350
##### C#

var client = new RestClient("https://localhost:44350/api/Messages");

client.Timeout = -1;

var request = new RestRequest(Method.GET);

request.AddParameter("text/plain", "",  ParameterType.RequestBody);

IRestResponse response = client.Execute(request);

Console.WriteLine(response.Content);

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
##### CURL
curl --location --request GET 'https://localhost:44350/api/Messages/1' \
##### HTTP
GET /api/Messages/1 HTTP/1.1

Host: localhost:44350
##### C#
var client = new RestClient("https://localhost:44350/api/Messages/1");

client.Timeout = -1;

var request = new RestRequest(Method.GET);

request.AddParameter("text/plain", "",  ParameterType.RequestBody);

IRestResponse response = client.Execute(request);

Console.WriteLine(response.Content);

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
##### CURL
curl --location --request POST 'https://localhost:44350/api/Messages' \

--header 'Content-Type: application/json' \

--data-raw '{
    "text": "MyMessage1"
}'

##### HTTP
POST /api/Messages HTTP/1.1

Host: localhost:44350

Content-Type: application/json

{
    "text": "MyMessage1"
}

##### C#
var client = new RestClient("https://localhost:44350/api/Messages");

client.Timeout = -1;

var request = new RestRequest(Method.POST);

request.AddHeader("Content-Type", "application/json");

request.AddParameter("application/json", "{\r\n    \"text\": \"MyMessage1\"\r\n}",  ParameterType.RequestBody);

IRestResponse response = client.Execute(request);

Console.WriteLine(response.Content);

#### Responce example
If message created successfully - status 200(Ok):
{
    "id": 3,
    "text": "MyMessage1",
    "creationDate": "01.11.2020 14:50:15",
    "hostName": "myHost",
    "hostIP": "192.168.1.10"
}

### Change message
PUT/api/Message/{id}

Change message with id={id}.

#### Parameters
##### {id}
Message Id in data base.

Type - integer.

#### Request example
##### CURL
curl --location --request PUT 'https://localhost:44350/api/Messages/1' \

--header 'Content-Type: application/json' \

--data-raw '{
        "id": 1,
        "text": "Changed Message Text",
        "creationDate": "01.11.2020 12:35:19",
        "hostName": "tosha",
        "hostIP": "192.168.1.109"
    }'
##### HTTP
PUT /api/Messages/1 HTTP/1.1

Host: localhost:44350

Content-Type: application/json

{
        "id": 1,
        "text": "Changed Message Text",
        "creationDate": "01.11.2020 12:35:19",
        "hostName": "tosha",
        "hostIP": "192.168.1.109"
    }
    
##### C#
var client = new RestClient("https://localhost:44350/api/Messages/1");

client.Timeout = -1;

var request = new RestRequest(Method.PUT);

request.AddHeader("Content-Type", "application/json");

request.AddParameter("application/json", "{\r\n        \"id\": 1,\r\n        \"text\": \"Changed Message Text\",\r\n        \"creationDate\": \"01.11.2020 12:35:19\",\r\n        \"hostName\": \"tosha\",\r\n        \"hostIP\": \"192.168.1.109\"\r\n    }",  ParameterType.RequestBody);

IRestResponse response = client.Execute(request);

Console.WriteLine(response.Content);
#### Responce example

If message created successfully - status 204(The server successfully processed the request, but is not returning any content).

### Delete message
DELETE/api/Messages/{id}

Delete message by its id.

#### Parameters
##### {id}
Message Id in data base.

Type - integer.

#### Request example
##### CURL
curl --location --request DELETE 'https://localhost:44350/api/Messages/3' \
##### HTTP
DELETE /api/Messages/3 HTTP/1.1

Host: localhost:44350
##### C#
var client = new RestClient("https://localhost:44350/api/Messages/3");

client.Timeout = -1;

var request = new RestRequest(Method.DELETE);

request.AddParameter("text/plain", "",  ParameterType.RequestBody);

IRestResponse response = client.Execute(request);

Console.WriteLine(response.Content);

#### Responce example
If message deleted successfully status - 200 (OK).

If message is not found - status 404 (Not found).
