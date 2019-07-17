## Generate local cert 

```
$ ./localcert.sh
```

## Docker Build

```
$ docker build -t kubetest .
```

## Start Docker Container

```
$ docker run --rm -it -p 8080:80 -p 8081:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="@kubetest123" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/kubetest.pfx -v ${HOME}/.aspnet/https:/https/ kubetest
```

> Add -d arg to run in background