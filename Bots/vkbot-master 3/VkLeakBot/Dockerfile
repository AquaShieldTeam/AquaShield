FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore

# Копируем весь код
COPY . ./
RUN dotnet publish -c Release -o out

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# Порт по умолчанию для Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "VkLeakBot.dll"]