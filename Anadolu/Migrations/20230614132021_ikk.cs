using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anadolu.Migrations
{
    /// <inheritdoc />
    public partial class ikk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Discounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ProductPriceAfterDiscount",
                table: "Discounts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "ProductPriceAfterDiscount",
                table: "Discounts");
        }
    }
}
