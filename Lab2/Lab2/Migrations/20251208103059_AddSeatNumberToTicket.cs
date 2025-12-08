using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab2.Migrations
{
    /// <inheritdoc />
    public partial class AddSeatNumberToTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MovieId",
                table: "Ticket",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatNumber",
                table: "Ticket",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_MovieId",
                table: "Ticket",
                column: "MovieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ticket_Movie_MovieId",
                table: "Ticket");

            migrationBuilder.DropIndex(
                name: "IX_Ticket_MovieId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "MovieId",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Ticket");
        }
    }
}
