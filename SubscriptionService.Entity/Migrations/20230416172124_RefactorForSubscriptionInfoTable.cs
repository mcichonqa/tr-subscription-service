using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SubscriptionService.Entity.Migrations
{
    /// <inheritdoc />
    public partial class RefactorForSubscriptionInfoTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "SubscriptionInfo",
                newName: "IsEnabled");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionName",
                table: "SubscriptionInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionName",
                table: "SubscriptionInfo");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                table: "SubscriptionInfo",
                newName: "Status");
        }
    }
}
