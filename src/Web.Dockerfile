FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.13-amd64 AS base
WORKDIR /app
EXPOSE 5002/tcp

RUN apk add libgdiplus --update-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted && \
    apk add terminus-font && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.13-amd64 AS build-env
COPY ["./Real.Time.Chat.sln", "./"]
COPY ["./Real.Time.Chat.Bot/Real.Time.Chat.Bot.csproj", "./Real.Time.Chat.Bot/" ]
COPY ["./Real.Time.Chat.Shared.Kernel/Real.Time.Chat.Shared.Kernel.csproj", "./Real.Time.Chat.Shared.Kernel/" ]
COPY ["./Real.Time.Chat.Web/Real.Time.Chat.Web.csproj", "./Real.Time.Chat.Web/" ]
RUN dotnet restore "./Real.Time.Chat.Web/Real.Time.Chat.Web.csproj"
COPY ./ .

RUN dotnet build "./Real.Time.Chat.Web/Real.Time.Chat.Web.csproj" --packages ./.nuget/packages -c Release -o /app/web

FROM build-env AS publish
RUN dotnet publish "./Real.Time.Chat.Web/Real.Time.Chat.Web.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app/web
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Real.Time.Chat.Web.dll", "--server.urls", "http://*:5002"]