using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoItems.Context.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "TodoItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "TodoItem",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
