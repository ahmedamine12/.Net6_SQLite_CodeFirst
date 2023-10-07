using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1Generated.Migrations
{
    public partial class new_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Produits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Produits",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
