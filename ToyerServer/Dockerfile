FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Toyer.API/Toyer.API.csproj", "Toyer.API/" ] 
COPY ["Toyer.Data/Toyer.Data.csproj", "Toyer.Data/" ] 
COPY ["Toyer.Logic/Toyer.Logic.csproj", "Toyer.Logic/" ] 
RUN dotnet restore "Toyer.API/Toyer.API.csproj"
COPY . .
WORKDIR "/src/Toyer.API"
RUN dotnet build "Toyer.API.csproj" -c Release -o /app/build

FROM build AS publish 
RUN dotnet publish "Toyer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet",  "Toyer.API.dll"]