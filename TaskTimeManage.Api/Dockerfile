#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskTimeManage.Api/TaskTimeManage.Api.csproj", "TaskTimeManage.Api/"]
COPY ["TaskTimeManage.Core/TaskTimeManage.Core.csproj", "TaskTimeManage.Core/"]
COPY ["TaskTimeManage.Domain/TaskTimeManage.Domain.csproj", "TaskTimeManage.Domain/"]
RUN dotnet restore "TaskTimeManage.Api/TaskTimeManage.Api.csproj"
COPY . .
WORKDIR "/src/TaskTimeManage.Api"
RUN dotnet build "TaskTimeManage.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskTimeManage.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskTimeManage.Api.dll"]