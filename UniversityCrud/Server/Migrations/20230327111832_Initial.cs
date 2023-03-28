using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityCrud.Server.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "DateCreated", "DateUpdated", "Description", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5660), new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5690), "Department of Information Technology", true, "Information Technology" },
                    { 2, new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5690), new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5690), "Department of Information Technology and Management", true, "Information Technology and Management" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "CourseId", "DateCreated", "DateOfBirth", "DateUpdated", "Email", "FirstName", "IsActive", "LastName" },
                values: new object[] { 100000, 1, new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5760), new DateTime(1992, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 3, 27, 16, 48, 32, 293, DateTimeKind.Local).AddTicks(5760), "rpathithasan0@gmail.com", "Pathithasan", true, "Rajakopal" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Title",
                table: "Courses",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Email",
                table: "Students",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
