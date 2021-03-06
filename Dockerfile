#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM  mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["./ColoredLive.Core/ColoredLive.Core.csproj", "ColoredLive.Core/"]
RUN dotnet restore "ColoredLive.Core/ColoredLive.Core.csproj"
COPY ["./ColoredLive.Core", "ColoredLive.Core/"]

COPY ["./ColoredLive.DAL/ColoredLive.DAL.csproj", "ColoredLive.DAL/"]
RUN dotnet restore "ColoredLive.DAL/ColoredLive.DAL.csproj"
COPY ["./ColoredLive.DAL", "ColoredLive.DAL/"]

COPY ["./ColoredLive.BL/ColoredLive.BL.csproj", "ColoredLive.BL/"]
RUN dotnet restore "ColoredLive.BL/ColoredLive.BL.csproj"
COPY ["./ColoredLive.BL", "ColoredLive.BL/"]


COPY ["./ColoredLive.Service.Core/ColoredLive.Service.Core.csproj", "ColoredLive.Service.Core/"]
RUN dotnet restore "ColoredLive.Service.Core/ColoredLive.Service.Core.csproj"
COPY ["./ColoredLive.Service.Core", "ColoredLive.Service.Core/"]

COPY ["./ColoredLive.MainService/ColoredLive.MainService.csproj", "ColoredLive.MainService/"]
RUN dotnet restore "./ColoredLive.MainService/ColoredLive.MainService.csproj"
COPY ["./ColoredLive.MainService", "ColoredLive.MainService/"]

WORKDIR "/src/ColoredLive.MainService"
RUN dotnet build "ColoredLive.MainService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ColoredLive.MainService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ColoredLive.MainService.dll"]