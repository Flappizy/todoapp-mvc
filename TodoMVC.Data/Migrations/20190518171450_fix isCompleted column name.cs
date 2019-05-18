using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoMVC.Data.Migrations
{
    public partial class fixisCompletedcolumnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRequired",
                table: "TodoItems",
                newName: "isCompleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isCompleted",
                table: "TodoItems",
                newName: "isRequired");
        }
    }
}
