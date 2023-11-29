FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/ITentikaTest.EventProcessor/ITentikaTest.EventProcessor.csproj", "Systems/ITentikaTest.EventProcessor/"]
COPY ["Data/ITentikaTest.Context.Entities/ITentikaTest.Context.Entities.csproj", "Data/ITentikaTest.Context.Entities/"]
COPY ["Data/ITentikaTest.Context/ITentikaTest.Context.csproj", "Data/ITentikaTest.Context/"]
COPY ["Shared/ITentikaTest.Common/ITentikaTest.Common.csproj", "Shared/ITentikaTest.Common/"]
COPY ["Shared/ITentikaTest.Settings/ITentikaTest.Settings.csproj", "Shared/ITentikaTest.Settings/"]
COPY ["Data/Migrations/ITentikaTest.Context.Migrations.PostgreSQL/ITentikaTest.Context.Migrations.PostgreSQL.csproj", "Data/Migrations/ITentikaTest.Context.Migrations.PostgreSQL/"]
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
