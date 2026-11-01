FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY TuEmpresa.Modulo.Api/TuEmpresa.Modulo.Api.csproj TuEmpresa.Modulo.Api/
RUN dotnet restore TuEmpresa.Modulo.Api/TuEmpresa.Modulo.Api.csproj
COPY TuEmpresa.Modulo.Api/ TuEmpresa.Modulo.Api/
RUN dotnet publish TuEmpresa.Modulo.Api/TuEmpresa.Modulo.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "TuEmpresa.Modulo.Api.dll"]
