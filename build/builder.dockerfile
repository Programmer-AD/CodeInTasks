FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app-src
RUN apt-get update && apt-get install docker -y
COPY ./ ./
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore ./Builder/src/CodeInTasks.Builder
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./Builder/src/CodeInTasks.Builder -o /app -c Release --no-restore -p:DebugType=None

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
VOLUME /app/data
COPY --from=builder /app /app
ENTRYPOINT dotnet CodeInTasks.Builder.dll
