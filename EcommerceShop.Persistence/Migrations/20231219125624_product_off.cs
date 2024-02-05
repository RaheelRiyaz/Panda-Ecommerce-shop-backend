using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class product_off : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Off",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Off",
                table: "Products");
        }
    }
}
