# RisePhoneApp


## Çalýþtýrma ve Kullaným
`docker compose up` : Bu komutu src içerisinde çalýþtýrarak projeyi ayaða kaldýrabilirsiniz.

`docker compose down` : container'larý silmek için kullanabilirsiniz.

Api Gateway: `http://localhost:5000/`

### Contact Endpoints (BaseUrl:localhost:5001)

`[HttpGET] [BaseUrl]/Contacts`

`[HttpPost] [BaseUrl]/Contacts`

`[HttpGET] [BaseUrl]/Contacts/{id}`

`[HttpDelete] [BaseUrl]/Contacts/{id}`

`[HttpPost] [BaseUrl]/Contacts/info`

`[HttpDelete] [BaseUrl]/Contacts/info/{id}`

Swagger Endpoint: http://localhost:5001/swagger/index.html

### Report Endpoints (BaseUrl:localhost:5002)

`[HttpGET] [BaseUrl]/Reports`

`[HttpPost] [BaseUrl]/Reports`

`[HttpGET] [BaseUrl]/Reports/{id}`

Swagger Endpoint: http://localhost:5002/swagger/index.html

### Kullanýlan teknolojiler:
   - .NET Core
   - MongoDB
   - RabbitMQ (Message Broker)
   - Ocelot
   - Docker compose