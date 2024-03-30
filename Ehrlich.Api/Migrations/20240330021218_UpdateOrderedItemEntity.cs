using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderedItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderedItemId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderedItemId",
                table: "OrderedItems");
        }
    }
}
