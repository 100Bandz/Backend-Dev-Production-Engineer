FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["InventoryTracker/InventoryTracker.csproj", "InventoryTracker/"]
RUN dotnet restore "InventoryTracker/InventoryTracker.csproj"
COPY . .
WORKDIR "/src/InventoryTracker"
RUN dotnet build "InventoryTracker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryTracker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryTracker.dll"]

FROM backenddevproductionenginner	
ARG Server=${Server}
ARG Database=${Database}
ARG User ID=${User ID}
ARG Password=${Password}
ENV Server defaultvalue1
ENV Database defaultvalue2
ENV User ID defaultvalue3
ENV Password defaultvalue4
