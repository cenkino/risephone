# RisePhoneApp


## �al��t�rma ve Kullan�m
`docker compose up` : Bu komutu src i�erisinde �al��t�rarak projeyi aya�a kald�rabilirsiniz.

`docker compose down` : container'lar� silmek i�in kullanabilirsiniz.

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

### Kullan�lan teknolojiler:
   - .NET Core
   - MongoDB
   - RabbitMQ (Message Broker)
   - Ocelot
   - Docker compose