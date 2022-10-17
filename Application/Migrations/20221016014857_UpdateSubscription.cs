using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application.Migrations
{
    public partial class UpdateSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionUserId",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscription");

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionUserName",
                table: "Subscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Subscription",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriptionUserName",
                table: "Subscription");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Subscription");

            migrationBuilder.AddColumn<int>(
                name: "SubscriptionUserId",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Subscription",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
