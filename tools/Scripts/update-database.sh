dotnet ef database update \
  --startup-project ../../src/SequentialOutbox.Migrations/SequentialOutbox.Migrations.csproj \
  -- "Host=127.0.0.1;Username=postgres;Password=example;Database=store-db"