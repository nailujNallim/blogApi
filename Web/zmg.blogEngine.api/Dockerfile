FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["Web/zmg.blogEngine.api/zmg.blogEngine.api.csproj", "Web/zmg.blogEngine.api/"]
RUN dotnet restore "Web/zmg.blogEngine.api/zmg.blogEngine.api.csproj"
COPY . .
WORKDIR "/src/Web/zmg.blogEngine.api"
RUN dotnet build "zmg.blogEngine.api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "zmg.blogEngine.api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "zmg.blogEngine.api.dll"]
