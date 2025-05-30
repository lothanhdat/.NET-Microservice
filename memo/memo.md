dotnet --version
dotnet new webapi -n PlatformService

code -r PlanformService

dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

docker build -t lothanhdat95/platformservice .
docker push lothanhdat95/platformservice
docker run -p 8080:8080 -d lothanhdat95/platformservice
docker ps
docker stop a472f4598e63
docker start a472f4598e63

kubectl version
kubectl apply -f platforms-depl.yaml
kubectl get deployments
kubectl get pods
kubectl delete deployment platforms-depl

kubectl apply -f platforms-np-srv.yaml
kubectl get services
kubectl delete service platforms-np-srv.yaml
https://youtu.be/DgVjEo3OGBI?t=13009



----
💡 Nói đơn giản:
✅ Dùng async/await
➡ Thread không bị block → được trả lại Thread Pool, có thể phục vụ request khác trong thời gian chờ I/O.
➡ Khi gọi await client.GetAsync(...), thread ASP.NET được trả về pool. Sau khi có kết quả, hệ thống cấp lại thread để xử lý tiếp → tăng khả năng phục vụ đồng thời (scalability).

❌ Không dùng async (code đồng bộ)
➡ Thread bị block → bị “ngồi chờ” trong suốt thời gian xử lý I/O (ví dụ: gọi DB, đọc file…).
➡ Thread của ASP.NET sẽ bị giữ chặt → không thể xử lý request nào khác.
----


docker build -t lothanhdat95/commandservice .
docker push lothanhdat95/commandservicedocker push lothanhdat95/commandservice
docker run -p 8080:8080 -d lothanhdat95/commandservice

kubectl rollout restart deployment platforms-depl
kubectl rollout restart deployment commands-depl
kubectl delete deployment platforms-depl
kubectl delete deployment commands-depl


kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.12.2/deploy/static/provider/cloud/deploy.yaml
thêm 127.0.0.1 acme.com vào host
kubectl apply -f ingress-srv.yaml

kubectl get storageclass
kubectl apply -f local-pvc.yaml
kubectl get pvc
kubectl create secret generic mssql --from-literal=SA_PASSWORD="pa55Word!" 
kubectl apply -f mssql-plat-depl.yaml

# luu y dat pass dung policy
dotnet tool install --global dotnet-ef --version 8.*

dotnet ef migrations add initialmigration

dotnet add package RabbitMQ.Client

----
chạy lại platforms-depl
có edit một chút trong appsetting của platformservice

dotnet add package Grpc.AspNetCore vao platform
dotnet add package Grpc.Tools vao command service 
dotnet add package Google.Protobuf vao command service
dotnet add package Grpc.Net.Client vao command service
