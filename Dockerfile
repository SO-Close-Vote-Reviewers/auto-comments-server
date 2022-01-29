FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY SOCVR.AutoComments.Server/SOCVR.AutoComments.Server.csproj ./
RUN dotnet restore SOCVR.AutoComments.Server.csproj
COPY SOCVR.AutoComments.Server/ .
RUN dotnet publish SOCVR.AutoComments.Server.csproj --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "SOCVR.AutoComments.Server.dll"]
