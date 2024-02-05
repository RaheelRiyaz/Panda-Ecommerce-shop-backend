using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cod_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "COD",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "COD",
                table: "Products");
        }
    }
}
