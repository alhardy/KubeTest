#!/bin/bash

dotnet dev-certs https -ep ${HOME}/.aspnet/https/KubeTest.pfx -p @kubetest123
# windows - dotnet dev-certs https --trust