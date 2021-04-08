#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
COPY publish /app
WORKDIR /app
EXPOSE 80
#ENV ASPNETCORE_URLS=http://*:5200;
ENTRYPOINT ["dotnet", "Hahn.ApplicationProcess.Web.dll"]
