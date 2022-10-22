using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Note",
                columns: table => new
                {
                    NoteID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    IsArchive = table.Column<bool>(nullable: false),
                    IsPin = table.Column<bool>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false),
                    userid = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Note", x => x.NoteID);
                    table.ForeignKey(
                        name: "FK_Note_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Note_userid",
                table: "Note",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Note");
        }
    }
}
