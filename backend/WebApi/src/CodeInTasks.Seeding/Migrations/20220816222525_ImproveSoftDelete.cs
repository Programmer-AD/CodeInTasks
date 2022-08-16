using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeInTasks.Seeding.Migrations
{
    public partial class ImproveSoftDelete : Migration
    {
        private const string columnIsDeleted = "IsDeleted";
        private const string columnDeletedAt = "DeletedAt";

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ChangeSoftDeleteColumn(migrationBuilder, "UserData");

            ChangeSoftDeleteColumn(migrationBuilder, "TaskModel");

            ChangeSoftDeleteColumn(migrationBuilder, "Solution");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RevertSoftDeleteColumn(migrationBuilder, "UserData");

            RevertSoftDeleteColumn(migrationBuilder, "TaskModel");

            RevertSoftDeleteColumn(migrationBuilder, "Solution");
        }

        private static void ChangeSoftDeleteColumn(MigrationBuilder migrationBuilder, string tableName)
        {

            migrationBuilder.AddColumn<DateTime>(
                name: columnDeletedAt,
                table: tableName,
                type: "datetime2",
                nullable: true);

            migrationBuilder.Sql($@"
                UPDATE [{tableName}]
                SET [{columnDeletedAt}] = GETUTCDATE()
                WHERE [{columnIsDeleted}] = TRUE");

            migrationBuilder.DropColumn(
                name: columnIsDeleted,
                table: tableName);
        }

        private static void RevertSoftDeleteColumn(MigrationBuilder migrationBuilder, string tableName)
        {
            migrationBuilder.AddColumn<bool>(
                name: columnIsDeleted,
                table: tableName,
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql($@"
                UPDATE [{tableName}]
                SET [{columnIsDeleted}] = TRUE
                WHERE [{columnDeletedAt}] IS NOT NULL");

            migrationBuilder.DropColumn(
                name: columnDeletedAt,
                table: tableName);
        }
    }
}
