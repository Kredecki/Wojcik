using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wojcik.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateExampleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.Guid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");
        }
    }
}
