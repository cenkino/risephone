# RisePhoneApp


## �al��t�rma ve Kullan�m
 `docker network create net_backendservices` : Gerekli docker network'� olu�turur
 
 `docker network create net_apigateway` : Gerekli docker network'� olu�turur


`docker compose up` : Bu komutu ana dizinde(docker-compose.yml) �al��t�rarak projeyi aya�a kald�rabilirsiniz.

Api Gateway: `http://localhost:5000/`

## API Gateway (BaseUrl: localhost:5000)

`[HttpGET] [BaseUrl]/Contacts`

`[HttpPost] [BaseUrl]/Contacts`

`[HttpGET] [BaseUrl]/Contacts/{id}`

`[HttpDelete] [BaseUrl]/Contacts/{id}`

`[HttpPost] [BaseUrl]/Contacts/info`

`[HttpDelete] [BaseUrl]/Contacts/info/{id}`

###

`[HttpGET] [BaseUrl]/Reports`

`[HttpPost] [BaseUrl]/Reports`

`[HttpGET] [BaseUrl]/Reports/{id}`

Swagger Endpoint: http://localhost:5000/swagger/index.html


## Contact Endpoints (BaseUrl: localhost:5001)

`[HttpGET] [BaseUrl]/Contacts`

`[HttpPost] [BaseUrl]/Contacts`

`[HttpGET] [BaseUrl]/Contacts/{id}`

`[HttpDelete] [BaseUrl]/Contacts/{id}`

`[HttpPost] [BaseUrl]/Contacts/info`

`[HttpDelete] [BaseUrl]/Contacts/info/{id}`

Swagger Endpoint: http://localhost:5001/swagger/index.html


## Report Endpoints (BaseUrl: localhost:5002)

`[HttpGET] [BaseUrl]/Reports`

`[HttpPost] [BaseUrl]/Reports`

`[HttpGET] [BaseUrl]/Reports/{id}`

Swagger Endpoint: http://localhost:5002/swagger/index.html


## Kullan�lan teknolojiler:
   - .NET Core
   - MongoDB
   - RabbitMQ (Message Broker)
   - Ocelot
   - Docker compose
   
## Sonland�rma
`docker compose down` : Container'lar� silmek i�in kullanabilirsiniz. 