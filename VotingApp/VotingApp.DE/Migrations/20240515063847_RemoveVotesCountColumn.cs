using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingApp.DE.Migrations
{
    /// <inheritdoc />
    public partial class RemoveVotesCountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VotesCount",
                table: "Candidates");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "VotesCount",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
