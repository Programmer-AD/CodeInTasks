FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app-src
COPY ./ ./
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore ./WebApi/src/CodeInTasks.Seeding.Runner
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./WebApi/src/CodeInTasks.Seeding.Runner -o /app -c Release --no-restore -p:DebugType=None

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
VOLUME /app/logs
COPY --from=builder /app /app
ENTRYPOINT dotnet CodeInTasks.Seeding.Runner.dll
