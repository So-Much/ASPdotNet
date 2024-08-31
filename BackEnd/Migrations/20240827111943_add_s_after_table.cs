using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class add_s_after_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blog_Users_FK_UserId",
                table: "Blog");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Post_FK_PostId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_FK_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Post_Blog_FK_BlogId",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_Post_FK_BlogId",
                table: "Posts",
                newName: "IX_Posts_FK_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_FK_UserId",
                table: "Comments",
                newName: "IX_Comments_FK_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_FK_PostId",
                table: "Comments",
                newName: "IX_Comments_FK_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_FK_UserId",
                table: "Blogs",
                newName: "IX_Blogs_FK_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blogs_Users_FK_UserId",
                table: "Blogs",
                column: "FK_UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_FK_PostId",
                table: "Comments",
                column: "FK_PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_FK_UserId",
                table: "Comments",
                column: "FK_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Blogs_FK_BlogId",
                table: "Posts",
                column: "FK_BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Blogs_Users_FK_UserId",
                table: "Blogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_FK_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_FK_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Blogs_FK_BlogId",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_FK_BlogId",
                table: "Post",
                newName: "IX_Post_FK_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_FK_UserId",
                table: "Comment",
                newName: "IX_Comment_FK_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_FK_PostId",
                table: "Comment",
                newName: "IX_Comment_FK_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_FK_UserId",
                table: "Blog",
                newName: "IX_Blog_FK_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Blog_Users_FK_UserId",
                table: "Blog",
                column: "FK_UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Post_FK_PostId",
                table: "Comment",
                column: "FK_PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_FK_UserId",
                table: "Comment",
                column: "FK_UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Blog_FK_BlogId",
                table: "Post",
                column: "FK_BlogId",
                principalTable: "Blog",
                principalColumn: "Id");
        }
    }
}
