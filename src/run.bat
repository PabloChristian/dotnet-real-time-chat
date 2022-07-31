dotnet clean

dotnet restore

dotnet build

dotnet tool install --global dotnet-ef

dotnet ef database update --startup-project .\Real.Time.Chat.Api

start dotnet watch run --project .\Real.Time.Chat.Api

start dotnet watch run --project .\Real.Time.Chat.Web

start dotnet watch run --project .\Real.Time.Chat.MessageBus

echo "Chrome will start in"

timeout 10

start chrome https://localhost:5002/login

start chrome -incognito https://localhost:5002/login

echo "Project started and running";