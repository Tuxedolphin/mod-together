using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomsAndTimetableFKs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OriginalTimetableId",
                table: "TimeTables",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "TimeTables",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.Sql(@"INSERT INTO ""Rooms"" (""Id"") SELECT ""Id"" FROM ""TimeTables""");
            migrationBuilder.Sql(@"UPDATE ""TimeTables"" SET ""RoomId"" = ""Id""");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_OriginalTimetableId",
                table: "TimeTables",
                column: "OriginalTimetableId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_RoomId",
                table: "TimeTables",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_Rooms_RoomId",
                table: "TimeTables",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeTables_TimeTables_OriginalTimetableId",
                table: "TimeTables",
                column: "OriginalTimetableId",
                principalTable: "TimeTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_Rooms_RoomId",
                table: "TimeTables");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeTables_TimeTables_OriginalTimetableId",
                table: "TimeTables");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_OriginalTimetableId",
                table: "TimeTables");

            migrationBuilder.DropIndex(
                name: "IX_TimeTables_RoomId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "OriginalTimetableId",
                table: "TimeTables");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "TimeTables");
        }
    }
}
