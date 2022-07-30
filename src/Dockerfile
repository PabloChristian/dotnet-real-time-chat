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
COPY ["./RealTimeChat.sln", "./"]
COPY ["./Real.Time.Chat.Bot/Real.Time.Chat.Bot.csproj", "./Real.Time.Chat.Bot/" ]
COPY ["./Real.Time.Chat.Shared.Kernel/Real.Time.Chat.Shared.Kernel.csproj", "./Real.Time.Chat.Shared.Kernel/" ]
COPY ["./realtime.chat.domain.core/Real.Time.Chat.Domain.Core.csproj", "./realtime.chat.domain.core/" ]
COPY ["./Real.Time.Chat.Infra.Ioc/Real.Time.Chat.Infra.Ioc.csproj", "./Real.Time.Chat.Infra.Ioc/" ]
COPY ["./Real.Time.Chat.Infra.Bus/Real.Time.Chat.Infra.Bus.csproj", "./Real.Time.Chat.Infra.Bus/" ]
COPY ["./Real.Time.Chat.Infra.Data/Real.Time.Chat.Infra.Data.csproj", "./Real.Time.Chat.Infra.Data/" ]
COPY ["./Real.Time.Chat.Application/Real.Time.Chat.Application.csproj", "./Real.Time.Chat.Application/" ]
COPY ["./Real.Time.Chat.Web.API/Real.Time.Chat.Web.API.csproj", "./Real.Time.Chat.Web.API/" ]
COPY ["./Real.Time.Chat.Infra.Identity/Real.Time.Chat.Infra.Identity.csproj", "./Real.Time.Chat.Infra.Identity/"]
#RUN dotnet restore "./Real.Time.Chat.Web.API/Real.Time.Chat.Web.API.csproj"
COPY ./ .

#RUN dotnet build "./Real.Time.Chat.Web.API/Real.Time.Chat.Web.API.csproj" --packages ./.nuget/packages -c Release -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./Real.Time.Chat.Web.API/Real.Time.Chat.Web.API.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Real.Time.Chat.Web.API.dll", "--server.urls", "http://*:5001"]