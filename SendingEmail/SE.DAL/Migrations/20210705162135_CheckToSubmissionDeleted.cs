using Microsoft.EntityFrameworkCore.Migrations;

namespace SE.DAL.Migrations
{
    public partial class CheckToSubmissionDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Check",
                table: "SubmissionsResult");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Check",
                table: "SubmissionsResult",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
