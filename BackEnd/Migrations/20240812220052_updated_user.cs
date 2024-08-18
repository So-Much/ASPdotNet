using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class updated_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Users",
                newName: "Name");

            migrationBuilder.Sql(
    "ALTER TABLE \"Users\" ALTER COLUMN \"Roles\" TYPE integer[] USING ARRAY[\"Roles\"];"
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "UserName");

            migrationBuilder.Sql(
    "ALTER TABLE \"Users\" ALTER COLUMN \"Roles\" TYPE integer[] USING ARRAY[\"Roles\"];"
);
        }
    }
}
