using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class lable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_Notes_NoteID",
                table: "CollaboratorTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_Users_userid",
                table: "CollaboratorTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userid",
                table: "Notes");

            migrationBuilder.CreateTable(
                name: "Lable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelName = table.Column<string>(nullable: true),
                    userid = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lable", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_Lable_Notes_NoteID",
                        column: x => x.NoteID,
                        principalTable: "Notes",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Lable_Users_userid",
                        column: x => x.userid,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lable_NoteID",
                table: "Lable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_Lable_userid",
                table: "Lable",
                column: "userid");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_Notes_NoteID",
                table: "CollaboratorTable",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "NoteID",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_Users_userid",
                table: "CollaboratorTable",
                column: "userid",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userid",
                table: "Notes",
                column: "userid",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_Notes_NoteID",
                table: "CollaboratorTable");

            migrationBuilder.DropForeignKey(
                name: "FK_CollaboratorTable_Users_userid",
                table: "CollaboratorTable");

            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Users_userid",
                table: "Notes");

            migrationBuilder.DropTable(
                name: "Lable");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_Notes_NoteID",
                table: "CollaboratorTable",
                column: "NoteID",
                principalTable: "Notes",
                principalColumn: "NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_CollaboratorTable_Users_userid",
                table: "CollaboratorTable",
                column: "userid",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Users_userid",
                table: "Notes",
                column: "userid",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
