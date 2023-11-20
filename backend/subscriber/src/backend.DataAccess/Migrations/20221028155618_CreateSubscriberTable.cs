using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.DataAccess.Migrations
{
    public partial class CreateSubscriberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    SubscriptionDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Subscribers",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber", "SubscriptionDate" },
                values: new object[,]
                {
                    { new Guid("e4066b77-977f-40af-94e1-a65ef4033061"), "razvan-andrei.canuci@student.tuiasi.ro", "Andrei", "0707070707", new DateTime(2022, 10, 27, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("fb17d1d3-e1e3-4cfc-924c-61219a1faa57"), "alex_alexutz@niezz.com", "Alexandru", "0712345678", new DateTime(2022, 10, 27, 11, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_Email",
                table: "Subscribers",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");
        }
    }
}
