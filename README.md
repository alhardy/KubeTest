## Generate local cert 

```
$ ./localcert.sh
```

## Docker Build

```
$ cd src/KubeTest
$ docker build -t play/kubetest:v1 .
```

Or

```
$ docker-compose build
```

## Start Docker Container

### WINDOWS
```
$ docker run --rm -it -p 8080:80 -p 8081:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Password="@kubetest123" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/KubeTest.pfx -v ${HOME}/.aspnet/https:/https/ -v ${env:APPDATA}/microsoft/UserSecrets:/root/.microsoft/usersecrets/ play/kubetest:v1
```

### LINUX
```
$ docker run --rm -it -p 8080:80 -p 8081:443 -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_HTTPS_PORT=8001 -e ASPNETCORE_ENVIRONMENT=Development -e ASPNETCORE_Kestrel__Certificates__Default__Password="@kubetest123" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/KubeTest.pfx -v ${HOME}/.aspnet/https:/https/ -v $HOME/.microsoft/usersecrets:/root/.microsoft/usersecrets/ play/kubetest:v1
```

> Add -d arg to run in background
> NOTES on HTTPS XPLAT: https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-https-development.md

# Deploy to Kubernetes (minikube)

Since this tutorial uses Minikube, instead of pushing our Docker image to a registry, we can simply build the image using the same Docker host as the Minikube VM, so that the images are automatically present. In other words, to point the docker client towards minikube's docker environment, we need to make sure we are using the Minikube Docker daemon:

```
$ eval $(minikube docker-env)
```

Later, when we no longer wish to use the Minikube host, we can undo this change by running:

```
$ eval $(minikube docker-env -u)
```

Create the deployment:

```
$ alias k=kubectl
$ cd k8s/
$ k create -f deploy.yml
```

Get logs

```
$ k logs kubetest
```

Test the deployment

```
$ k port-forward kubetest 8888:80
$ curl localhost:8888
```
