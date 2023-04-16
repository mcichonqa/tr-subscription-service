using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionService.Entity.Migrations
{
    /// <inheritdoc />
    public partial class AddingClientIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "SubscriptionInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "SubscriptionInfo");
        }
    }
}
