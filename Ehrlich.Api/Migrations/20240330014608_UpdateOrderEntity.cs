using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ehrlich.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems");

            migrationBuilder.AddColumn<int>(
                name: "OrderNumber",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderedItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderedItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedItems_Orders_OrderId",
                table: "OrderedItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
