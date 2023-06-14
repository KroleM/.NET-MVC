using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Przychodnia.Database.Migrations
{
    public partial class M4_DoctorPicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoURL",
                table: "Doctor");

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Doctor",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "PictureFormat",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PictureFormat",
                table: "Doctor");

            migrationBuilder.AddColumn<string>(
                name: "PhotoURL",
                table: "Doctor",
                type: "nvarchar(max)",
                nullable: true);

        }
    }
}
