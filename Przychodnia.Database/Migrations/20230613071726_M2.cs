using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Database.Migrations
{
    public partial class M2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkTitle",
                table: "Feed");

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Feed",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feed_IconId",
                table: "Feed",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feed_Icon_IconId",
                table: "Feed",
                column: "IconId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feed_Icon_IconId",
                table: "Feed");

            migrationBuilder.DropTable(
                name: "Icon");

            migrationBuilder.DropIndex(
                name: "IX_Feed_IconId",
                table: "Feed");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Feed");

            migrationBuilder.AddColumn<string>(
                name: "LinkTitle",
                table: "Feed",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
