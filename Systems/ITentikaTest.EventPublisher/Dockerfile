FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/ITentikaTest.EventPublisher/ITentikaTest.EventPublisher.csproj", "Systems/ITentikaTest.EventPublisher/"]
COPY ["Data/ITentikaTest.Context.Entities/ITentikaTest.Context.Entities.csproj", "Data/ITentikaTest.Context.Entities/"]
COPY ["Shared/ITentikaTest.Common/ITentikaTest.Common.csproj", "Shared/ITentikaTest.Common/"]
COPY ["Shared/ITentikaTest.Settings/ITentikaTest.Settings.csproj", "Shared/ITentikaTest.Settings/"]
RUN dotnet restore "Systems/ITentikaTest.EventPublisher/ITentikaTest.EventPublisher.csproj"
COPY . .
WORKDIR "/src/Systems/ITentikaTest.EventPublisher"
RUN dotnet build "ITentikaTest.EventPublisher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ITentikaTest.EventPublisher.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ITentikaTest.EventPublisher.dll"]
