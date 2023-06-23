using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Database.Migrations
{
    public partial class M6DoctorDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PictureFormat",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "Doctor",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Doctor",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "VisitDateTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitDateTime", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorDateTime",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsBooked = table.Column<bool>(type: "bit", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    VisitDateTimeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorDateTime", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorDateTime_Doctor_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorDateTime_VisitDateTime_VisitDateTimeId",
                        column: x => x.VisitDateTimeId,
                        principalTable: "VisitDateTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketElement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BasketSessionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DoctorDateTimeId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketElement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketElement_DoctorDateTime_DoctorDateTimeId",
                        column: x => x.DoctorDateTimeId,
                        principalTable: "DoctorDateTime",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketElement_DoctorDateTimeId",
                table: "BasketElement",
                column: "DoctorDateTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDateTime_DoctorId",
                table: "DoctorDateTime",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorDateTime_VisitDateTimeId",
                table: "DoctorDateTime",
                column: "VisitDateTimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketElement");

            migrationBuilder.DropTable(
                name: "DoctorDateTime");

            migrationBuilder.DropTable(
                name: "VisitDateTime");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "PictureFormat",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Picture",
                table: "Doctor",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }
    }
}
