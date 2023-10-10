using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1Generated.Migrations
{
    public partial class AddCategorieTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categorie_CategorieId",
                table: "Produits");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Produits",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categorie_CategorieId",
                table: "Produits",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produits_Categorie_CategorieId",
                table: "Produits");

            migrationBuilder.AlterColumn<int>(
                name: "CategorieId",
                table: "Produits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produits_Categorie_CategorieId",
                table: "Produits",
                column: "CategorieId",
                principalTable: "Categorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
