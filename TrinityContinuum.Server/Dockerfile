# See https://aka.ms/customizecontainer to learn how to customize your debug container 
# and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY ./publish-arm64/ .

ENTRYPOINT ["dotnet", "TrinityContinuum.Server.dll"]