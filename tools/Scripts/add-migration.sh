dotnet ef migrations add \
  --startup-project ../../src/SequentialOutbox.Migrations/SequentialOutbox.Migrations.csproj \
  "$1" -- "Host=127.0.0.1;Username=postgres;Password=example;Database=store-db"