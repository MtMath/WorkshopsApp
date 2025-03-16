.PHONY: migrate migrate-identity migrate-rollback migration migrate-identity migration-rollback optimize help

.DEFAULT_GOAL := help

CONTEXT ?= AppDbContext

up:
	@echo "Starting workshops compose..."
	@docker compose up --build -d
	@echo "Workshops compose started successfully."

down:
	@echo "Stopping workshops compose..."
	@docker compose down
	@echo "Workshops compose stopped successfully."

workshops:
	@echo "Starting workshops..."
	@docker compose up --build -d workshops
	@echo "Workshops started successfully."
	
sql:
	@echo "Starting SQL Server..."
	@docker compose up -d sqlserver
	@echo "SQL Server started successfully."

migrate:
	@echo Applying migrations using context: $(CONTEXT) ...
	@dotnet ef database update --context $(CONTEXT) --project ./Workshops.Infrastructure --startup-project ./Workshops.Web --verbose
	@echo Migrations applied successfully.

migrate-identity:
	@echo Applying migrations using context: AppIdentityDbContext ...
	@dotnet ef database update --context AppIdentityDbContext --project ./Workshops.Infrastructure --startup-project ./Workshops.Web --verbose
	@echo Migrations applied successfully.

migrate-rollback:
	@echo "Reverting to migration $(MIGRATION)..."
	@dotnet ef database update $(MIGRATION) --context AppIdentityDbContext --project ./Workshops.Infrastructure --startup-project ./Workshops.Web
	@echo "Rollback completed successfully."

migration:
	@echo "Creating migration $(NAME) for $(CONTEXT) ..."
	@dotnet ef migrations add $(NAME) --context $(CONTEXT) --output-dir ./Data/Migrations/App --project ./Workshops.Infrastructure --startup-project ./Workshops.Web --verbose
	@echo "Migration created successfully."

migration-identity:
	@echo "Creating migration $(NAME)..."
	@dotnet ef migrations add $(NAME) --context AppIdentityDbContext --output-dir ./Data/Migrations/Identity --project ./Workshops.Infrastructure --startup-project ./Workshops.Web --verbose
	@echo "Migration created successfully."

migration-rollback:
	@echo "Removing last migration..."
	@dotnet ef migrations remove --project ./Workshops.Infrastructure --startup-project ./Workshops.Web
	@echo "Last migration removed successfully."

drop-db:
	@echo "Dropping $(CONTEXT) database..."
	@dotnet ef database drop --context $(CONTEXT) --project ./Workshops.Infrastructure --startup-project ./Workshops.Web
	@echo "Database dropped successfully."

help:
	@echo "Usage: make [command]"
	@echo "Commands:"
	@echo "  migrate CONTEXT=<context>: Apply all pending migrations for context (default: AppDbContext)"
	@echo "  migrate-identity Apply all pending migrations for identity context"
	@echo "  migrate-rollback MIGRATION=<name>: Rollback to a specific migration"
	@echo "  migration NAME=<name>: Create new migration with specified name"
	@echo "  migration-rollback: Remove last migration (if not applied)"
	@echo "  help: Show this help message"