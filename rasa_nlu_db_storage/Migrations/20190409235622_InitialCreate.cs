using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rasa_nlu_db_storage.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NluModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NluModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RasaNLUDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NluModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RasaNLUDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RasaNLUDatas_NluModel_NluModelId",
                        column: x => x.NluModelId,
                        principalTable: "NluModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommonExamples",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RasaNLUDataId = table.Column<int>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    Intent = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommonExamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommonExamples_RasaNLUDatas_RasaNLUDataId",
                        column: x => x.RasaNLUDataId,
                        principalTable: "RasaNLUDatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommonExampleId = table.Column<int>(nullable: true),
                    Start = table.Column<int>(nullable: false),
                    End = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    EntityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_CommonExamples_CommonExampleId",
                        column: x => x.CommonExampleId,
                        principalTable: "CommonExamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommonExamples_RasaNLUDataId",
                table: "CommonExamples",
                column: "RasaNLUDataId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_CommonExampleId",
                table: "Entities",
                column: "CommonExampleId");

            migrationBuilder.CreateIndex(
                name: "IX_RasaNLUDatas_NluModelId",
                table: "RasaNLUDatas",
                column: "NluModelId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "CommonExamples");

            migrationBuilder.DropTable(
                name: "RasaNLUDatas");

            migrationBuilder.DropTable(
                name: "NluModel");
        }
    }
}
