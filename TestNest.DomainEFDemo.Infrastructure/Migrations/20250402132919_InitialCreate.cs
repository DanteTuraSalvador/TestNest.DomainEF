using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestNest.DomainEFDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Identifications",
                columns: table => new
                {
                    IdTypeId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IdTypeName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifications", x => x.IdTypeId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NationalityName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityId)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    GuestId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    FirstName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    AddressLine = table.Column<string>(type: "NVARCHAR(200)", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "NVARCHAR(100)", maxLength: 100, nullable: false),
                    NationalityId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IdTypeId = table.Column<Guid>(type: "UNIQUEIDENTIFIER", nullable: false),
                    IdNumber = table.Column<string>(type: "NVARCHAR(50)", maxLength: 50, nullable: false),
                    GuestType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guests", x => x.GuestId)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_Guests_Identifications_IdTypeId",
                        column: x => x.IdTypeId,
                        principalTable: "Identifications",
                        principalColumn: "IdTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Guests_Nationalities_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationalities",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_IdTypeId",
                table: "Guests",
                column: "IdTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Guests_NationalityId",
                table: "Guests",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Identifications_IdTypeName",
                table: "Identifications",
                column: "IdTypeName")
                .Annotation("SqlServer:Clustered", true);

            migrationBuilder.CreateIndex(
                name: "IX_Nationalities_NationalityName",
                table: "Nationalities",
                column: "NationalityName",
                unique: true)
                .Annotation("SqlServer:Clustered", true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Identifications");

            migrationBuilder.DropTable(
                name: "Nationalities");
        }
    }
}
