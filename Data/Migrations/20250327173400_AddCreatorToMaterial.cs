using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatorToMaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Materials",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MatCreatorId",
                table: "Materials",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AspNetUsers_MatCreatorId",
                table: "Materials",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AspNetUsers_MatCreatorId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Materials_MatCreatorId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Materials");
        }
    }
}
