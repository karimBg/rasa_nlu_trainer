using Microsoft.EntityFrameworkCore.Migrations;

namespace rasa_nlu_db_storage.Migrations
{
    public partial class SeedDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NluModel",
                column: "Id",
                value: 1);

            migrationBuilder.InsertData(
                table: "RasaNLUDatas",
                columns: new[] { "Id", "NluModelId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "CommonExamples",
                columns: new[] { "Id", "Intent", "RasaNLUDataId", "Text" },
                values: new object[] { 1, "Greeting", 1, "Hello" });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CommonExampleId", "End", "EntityName", "Start", "Value" },
                values: new object[] { 1, 1, 20, "Name", 1, "Karim" });

            migrationBuilder.InsertData(
                table: "Entities",
                columns: new[] { "Id", "CommonExampleId", "End", "EntityName", "Start", "Value" },
                values: new object[] { 2, 1, 26, "Place", 9, "Monastir" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Entities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CommonExamples",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RasaNLUDatas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NluModel",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
