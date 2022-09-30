using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sales_Management_API.Migrations
{
    public partial class AddForeignKeyDepartmentIntoServiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Services",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Services_DepartmentId",
                table: "Services",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Departments_DepartmentId",
                table: "Services",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Departments_DepartmentId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_DepartmentId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Services");
        }
    }
}
