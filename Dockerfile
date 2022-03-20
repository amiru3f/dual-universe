FROM mcr.microsoft.com/dotnet/sdk:6.0.201-bullseye-slim-arm64v8

WORKDIR  /app

COPY . .
RUN dotnet build
ENTRYPOINT [  "dotnet", "run" ]