using Microsoft.EntityFrameworkCore.Migrations;

namespace SE.DAL.Migrations
{
    public partial class CheckToSubmissionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Check",
                table: "SubmissionsResult",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Check",
                table: "SubmissionsResult");
        }
    }
}
