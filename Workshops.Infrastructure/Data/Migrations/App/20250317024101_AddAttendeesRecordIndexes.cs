using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workshops.Infrastructure.Data.Migrations.App
{
    /// <inheritdoc />
    public partial class AddAttendeesRecordIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AttendeesRecords_WorkshopId",
                schema: "Application",
                table: "AttendeesRecords");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeesRecords_WorkshopId_CollaboratorId",
                schema: "Application",
                table: "AttendeesRecords",
                columns: new[] { "WorkshopId", "CollaboratorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AttendeesRecords_WorkshopId_CollaboratorId",
                schema: "Application",
                table: "AttendeesRecords");

            migrationBuilder.CreateIndex(
                name: "IX_AttendeesRecords_WorkshopId",
                schema: "Application",
                table: "AttendeesRecords",
                column: "WorkshopId");
        }
    }
}
