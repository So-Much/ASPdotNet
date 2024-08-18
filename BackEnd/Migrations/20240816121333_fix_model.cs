using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class fix_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Social_medias",
                table: "Contacts",
                newName: "SocialMedias");

            migrationBuilder.RenameColumn(
                name: "Phone_number",
                table: "Contacts",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocialMedias",
                table: "Contacts",
                newName: "Social_medias");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contacts",
                newName: "Phone_number");
        }
    }
}
