FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/ITentikaTest.WebAPI/ITentikaTest.WebAPI.csproj", "Systems/ITentikaTest.WebAPI/"]
RUN dotnet restore "Systems/ITentikaTest.WebAPI/ITentikaTest.WebAPI.csproj"
COPY . .
WORKDIR "/src/Systems/ITentikaTest.WebAPI"
RUN dotnet build "ITentikaTest.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITentikaTest.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITentikaTest.WebAPI.dll"]
