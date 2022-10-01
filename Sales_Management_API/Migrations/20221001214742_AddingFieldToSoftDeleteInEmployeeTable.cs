using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales_Management_API.Migrations
{
    public partial class AddingFieldToSoftDeleteInEmployeeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employees");
        }
    }
}
