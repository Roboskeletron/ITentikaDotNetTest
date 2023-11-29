FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Systems/ITentikaTest.EventProcessor/ITentikaTest.EventProcessor.csproj", "Systems/ITentikaTest.EventProcessor/"]
RUN dotnet restore "Systems/ITentikaTest.EventProcessor/ITentikaTest.EventProcessor.csproj"
COPY . .
WORKDIR "/src/Systems/ITentikaTest.EventProcessor"
RUN dotnet build "ITentikaTest.EventProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITentikaTest.EventProcessor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITentikaTest.EventProcessor.dll"]
