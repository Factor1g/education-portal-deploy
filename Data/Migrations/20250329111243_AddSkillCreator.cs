using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillCreator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkillCreatorId",
                table: "Skills",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_SkillCreatorId",
                table: "Skills",
                column: "SkillCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_AspNetUsers_SkillCreatorId",
                table: "Skills",
                column: "SkillCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_AspNetUsers_SkillCreatorId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_SkillCreatorId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "SkillCreatorId",
                table: "Skills");
        }
    }
}
