using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cart_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "SizeId",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Carts");

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
