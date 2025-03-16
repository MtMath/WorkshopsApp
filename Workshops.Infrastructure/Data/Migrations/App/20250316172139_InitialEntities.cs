using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshops.Infrastructure.Data.Migrations.App
{
    /// <inheritdoc />
    public partial class InitialEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Application");

            migrationBuilder.CreateTable(
                name: "Collaborators",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(126)", maxLength: 126, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workshops",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(126)", maxLength: 126, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RealizationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendeesRecords",
                schema: "Application",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkshopId = table.Column<int>(type: "int", nullable: false),
                    CollaboratorId = table.Column<int>(type: "int", nullable: false),
                    Attended = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendeesRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendeesRecords_Collaborators_CollaboratorId",
                        column: x => x.CollaboratorId,
                        principalSchema: "Application",
                        principalTable: "Collaborators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendeesRecords_Workshops_WorkshopId",
                        column: x => x.WorkshopId,
                        principalSchema: "Application",
                        principalTable: "Workshops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendeesRecords_CollaboratorId",
                schema: "Application",
                table: "AttendeesRecords",
                column: "CollaboratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeesRecords_WorkshopId",
                schema: "Application",
                table: "AttendeesRecords",
                column: "WorkshopId");

            migrationBuilder.CreateIndex(
                name: "IX_Workshops_Slug_RealizationDate",
                schema: "Application",
                table: "Workshops",
                columns: new[] { "Slug", "RealizationDate" },
                unique: true,
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendeesRecords",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Collaborators",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "Workshops",
                schema: "Application");
        }
    }
}
