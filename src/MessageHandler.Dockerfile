FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.15-amd64 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15-amd64 AS build-env
COPY ["./Real.Time.Chat.sln", "./"]
COPY ["./Real.Time.Chat.Shared.Kernel/Real.Time.Chat.Shared.Kernel.csproj", "./Real.Time.Chat.Shared.Kernel/" ]
COPY ["./Real.Time.Chat.Domain/Real.Time.Chat.Domain.csproj", "./Real.Time.Chat.Domain/"]
COPY ["./Real.Time.Chat.Bot/Real.Time.Chat.Bot.csproj", "./Real.Time.Chat.Bot/"]
COPY ["./Real.Time.Chat.Infrastructure/Real.Time.Chat.Infrastructure.csproj", "./Real.Time.Chat.Infrastructure/"]
COPY ["./Real.Time.Chat.MessageHandler/Real.Time.Chat.MessageHandler.csproj", "./Real.Time.Chat.MessageHandler/"]
#RUN dotnet restore "./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj"
COPY ./ .

#RUN dotnet build "./Real.Time.Chat.Api/Real.Time.Chat.Api.csproj" --packages ./.nuget/packages -c Release -o /app/build

#RUN dotnet test

FROM build-env AS publish
RUN dotnet publish "./Real.Time.Chat.MessageHandler/Real.Time.Chat.MessageHandler.csproj" -c Production -o /app/publish


FROM base AS final
WORKDIR /app/build
RUN chmod +x ./

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Real.Time.Chat.MessageHandler.dll"]