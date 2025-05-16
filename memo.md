dotnet --version
dotnet new webapi -n PlatformService

code -r PlanformService

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

docker build -t lothanhdat95/platformservice .
docker run -p 8080:8080 -d lothanhdat95/platformservice
docker ps
docker stop a472f4598e63
docker start a472f4598e63
docker push lothanhdat95/platformservice

kubectl version
kubectl apply -f platforms-depl.yaml
kubectl get deployments
kubectl get pods
kubectl delete deployment platforms-depl

kubectl apply -f platforms-np-srv.yaml
kubectl get services
kubectl delete service platforms-np-srv.yaml
https://youtu.be/DgVjEo3OGBI?t=13009