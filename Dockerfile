FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
RUN apt-get update && apt-get install -y git

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY SOCVR.Website.Server/SOCVR.Website.Server.csproj ./
RUN dotnet restore SOCVR.Website.Server.csproj
COPY SOCVR.Website.Server/ .
RUN dotnet build "SOCVR.Website.Server.csproj" --no-restore -c Release -o /app

FROM build AS publish
RUN dotnet publish "SOCVR.Website.Server.csproj" --no-restore -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SOCVR.Website.Server.dll"]

ARG AppVersion
ENV Meta__AppVersion ${AppVersion:-NotSet}
