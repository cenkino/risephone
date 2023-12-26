# RisePhoneApp


## Çalýþtýrma ve Kullaným
 `docker network create net_backendservices` : Gerekli docker network'ü oluþturur
 
 `docker network create net_apigateway` : Gerekli docker network'ü oluþturur


`docker compose up` : Bu komutu ana dizinde(docker-compose.yml) çalýþtýrarak projeyi ayaða kaldýrabilirsiniz.

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


## Kullanýlan teknolojiler:
   - .NET Core
   - MongoDB
   - RabbitMQ (Message Broker)
   - Ocelot
   - Docker compose
   
## Sonlandýrma
`docker compose down` : Container'larý silmek için kullanabilirsiniz. 