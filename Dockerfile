FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY ./MyVideoAPI/*.csproj ./
RUN dotnet restore

COPY ./MyVideoAPI ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
COPY --from=build /app/out .

EXPOSE 5000
ENTRYPOINT ["dotnet", "MyVideoAPI.dll"]
CMD ["dotnet", "ef", "database", "update"]
