# TodoAPI

# Please follow below steps to run API

# Create your image
docker run --name postgres -e POSTGRES_PASSWORD=123456 -p 5432:5432 -d postgres

# Add migration

- with Package Manager Console : Go to ToDoListAPI.Persistence > add-migration "migration_name"
- with CLI : dotnet ef migrations add "migration_name"

# Update database
- with Package Manager Console : Go to ToDoListAPI.Persistence > update-database
- with CLI : dotnet ef database update

# Run Project and check API
- https://localhost:5001/swagger/index.html