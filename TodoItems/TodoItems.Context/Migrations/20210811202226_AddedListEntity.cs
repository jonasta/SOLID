using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoItems.Context.Migrations
{
    public partial class AddedListEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TodoListId",
                table: "TodoItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoList", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_TodoListId",
                table: "TodoItem",
                column: "TodoListId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_TodoList_TodoListId",
                table: "TodoItem",
                column: "TodoListId",
                principalTable: "TodoList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_TodoList_TodoListId",
                table: "TodoItem");

            migrationBuilder.DropTable(
                name: "TodoList");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_TodoListId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "TodoListId",
                table: "TodoItem");
        }
    }
}
