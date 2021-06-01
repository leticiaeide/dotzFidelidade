FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./Dotz.Fidelidade.Api/Dotz.Fidelidade.Api.csproj", "Dotz.Fidelidade.Api/"]
COPY ["./Dotz.Fidelidade.CrossCutting/Dotz.Fidelidade.CrossCutting.csproj", "Dotz.Fidelidade.CrossCutting/"]
COPY ["./Dotz.Fidelidade.Data/Dotz.Fidelidade.Data.csproj", "Dotz.Fidelidade.Data/"]
COPY ["./Dotz.Fidelidade.Domain/Dotz.Fidelidade.Domain.csproj", "Dotz.Fidelidade.Domain/"]

RUN dotnet restore "Dotz.Fidelidade.Api/Dotz.Fidelidade.Api.csproj"
COPY . .
WORKDIR "Dotz.Fidelidade.Api"
RUN dotnet build "Dotz.Fidelidade.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotz.Fidelidade.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotz.Fidelidade.Api.dll"] 