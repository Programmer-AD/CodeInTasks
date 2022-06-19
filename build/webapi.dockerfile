FROM mcr.microsoft.com/dotnet/sdk:6.0 AS builder
WORKDIR /app-src
COPY ./backend/Shared/src ./Shared/src
COPY ./backend/WebApi/src ./WebApi/src
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore ./WebApi/src/CodeInTasks.Web
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish ./WebApi/src/CodeInTasks.Web -o /app -c Release --no-restore -p:DebugType=None

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
VOLUME /app/logs
EXPOSE 80
COPY --from=builder /app /app
ENTRYPOINT dotnet CodeInTasks.Web.dll
