using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRUD_DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    MobilePhone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirthDate", "JobTitle", "MobilePhone", "Name" },
                values: new object[] { new Guid("10e06c31-0e5d-4a81-b52f-7435a84ddbda"), new DateTime(2022, 9, 21, 9, 56, 22, 959, DateTimeKind.Local).AddTicks(740), "A1", "+375(33)2345678", "Dima" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirthDate", "JobTitle", "MobilePhone", "Name" },
                values: new object[] { new Guid("68eab7c2-b0df-4ef8-a147-98b327372a7e"), new DateTime(2022, 9, 21, 9, 56, 22, 960, DateTimeKind.Local).AddTicks(34), "Global", "+375(44)2345678", "Roma" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "BirthDate", "JobTitle", "MobilePhone", "Name" },
                values: new object[] { new Guid("52d095f2-49f5-4ebb-be36-4541d92b90ef"), new DateTime(2022, 9, 21, 9, 56, 22, 960, DateTimeKind.Local).AddTicks(53), "MTC", "+375(29)2345678", "Pasha" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
