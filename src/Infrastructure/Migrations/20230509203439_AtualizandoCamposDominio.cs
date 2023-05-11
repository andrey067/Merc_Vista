using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AtualizandoCamposDominio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Acaoes",
                newName: "EmpresaNome");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Acaoes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CodidoAcao",
                table: "Acaoes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodidoAcao",
                table: "Acaoes");

            migrationBuilder.RenameColumn(
                name: "EmpresaNome",
                table: "Acaoes",
                newName: "Ativo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Acaoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");
        }
    }
}
