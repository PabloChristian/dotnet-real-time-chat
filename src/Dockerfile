FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.13-amd64 AS base
WORKDIR /app
EXPOSE 5001/tcp

RUN apk add libgdiplus --update-cache --repository http://dl-3.alpinelinux.org/alpine/edge/testing/ --allow-untrusted && \
    apk add terminus-font && \
    apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT false
ENV ASPNETCORE_ENVIRONMENT=Production
#ENV ConnectionStrings:RealTimeChatConnection="server=realtime-db;database=realtime;user=sa;password=dev@1234;convert zero datetime=True;"s

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.13-amd64 AS build-env
COPY ["./Real.Time.Chat.sln", "./"]
COPY ["./Real.Time.Chat.Bot/Real.Time.Chat.Bot.csproj", "./Real.Time.Chat.Bot/" ]
COPY ["./Real.Time.Chat.Shared.Kernel/Real.Time.Chat.Shared.Kernel.csproj", "./Real.Time.Chat.Shared.Kernel/" ]
COPY ["./Real.Time.Chat.Infrastructure/Real.Time.Chat.Infrastructure.csproj", "./Real.Time.Chat.Infrastructure/" ]
COPY ["./Real.Time.Chat.Domain/Real.Time.Chat.Domain.csproj", "./Real.Time.Chat.Domain/" ]
COPY ["./Real.Time.Chat.Application/Real.Time.Chat.Application.csproj", "./Real.Time.Chat.Application/" ]
COPY ["./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj", "./Real.Time.Chat.Api/" ]
#RUN dotnet restore "./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj"
COPY ./ .

#RUN dotnet build "./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj" --packages ./.nuget/packages -c Release -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Real.Time.Chat.Api.dll", "--server.urls", "http://*:5001"]