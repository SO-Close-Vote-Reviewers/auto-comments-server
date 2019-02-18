FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && apt-get install -y git

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY SOCVR.AutoCommentsServer/SOCVR.AutoCommentsServer.csproj ./
RUN dotnet restore SOCVR.AutoCommentsServer.csproj
COPY SOCVR.AutoCommentsServer/ .
RUN dotnet publish SOCVR.AutoCommentsServer.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SOCVR.AutoCommentsServer.dll"]
