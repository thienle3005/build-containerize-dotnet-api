
# to build image 
```console
cd CloudCustomers.API
docker build --rm -t productive-dev/cloud-customer:latest .
```
# to run container
```console
cd CloudCustomers.API
docker run --rm -p 5000:5000 -p 5001:5001 -e ASPNETCORE_HTTP_PORT=https://+:5001 -e ASPNETCORE_URLS=http://+:5000 productive-dev/cloud-customer 
```
# default url
http://localhost:5000/users